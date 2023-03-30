/// @file TransformationUtil.cs
/// 
/// Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
/// PROPRIETARY/CONFIDENTIAL 
/// This software is the confidential and proprietary
/// information of SAMSUNG ELECTRONICS ("Confidential Information"). You shall
/// not disclose such Confidential Information and shall use it only in
/// accordance with the terms of the license agreement you entered into with
/// SAMSUNG ELECTRONICS. SAMSUNG make no representations or warranties about the
/// suitability of the software, either express or implied, including but not
/// limited to the implied warranties of merchantability, fitness for a
/// particular purpose, or non-infringement. SAMSUNG shall not be liable for any
/// damages suffered by licensee as a result of using, modifying or distributing
/// this software or its derivatives.

using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    internal static class TransformationUtil
    {
        private static bool TryGetParentSize(View view, out float parentSizeW, out float parentSizeH)
        {
            Container parent = view.GetParent();

            if (parent != null)
            {
                if (parent is View parentView)
                {
                    parentSizeW = parentView.SizeWidth;
                    parentSizeH = parentView.SizeHeight;
                    return true;
                }
                else if (parent is Layer)
                {
                    parentSizeW = Window.Instance.WindowSize.Width;
                    parentSizeH = Window.Instance.WindowSize.Height;
                    return true;
                }
            }

            parentSizeW = parentSizeH = -1;
            return false;
        }

        private static void GetXYDistanceFromPivotPoint(View view, out float xDistanceFromPivotPoint, out float yDistanceFromPivotPoint)
        {
            if (view != null && view.PositionUsesPivotPoint)
            {
                xDistanceFromPivotPoint = -(view.SizeWidth * view.PivotPoint.X);
                yDistanceFromPivotPoint = -(view.SizeHeight * view.PivotPoint.Y);
            }
            else
            {
                xDistanceFromPivotPoint = 0;
                yDistanceFromPivotPoint = 0;
            }
        }

        private static void GetTopLeftPosition(View view, out float x, out float y)
        {
            x = view.PositionX;
            y = view.PositionY;

            if (view.PositionUsesPivotPoint == false && view.ParentOrigin.EqualTo(ParentOrigin.TopLeft))
            {
                return;
            }

            if (TryGetParentSize(view, out float parentSizeW, out float parentSizeH) == false)
            {
                CLog.Error("Could not calculate the TopLeft position. Parent is invalid");
                return;
            }

            float xDistanceFromParent, yDistanceFromParent;

            GetXYDistanceFromPivotPoint(view, out float xDistanceFromPivotPoint, out float yDistanceFromPivotPoint);

            xDistanceFromParent = (parentSizeW * view.ParentOrigin.X);
            yDistanceFromParent = (parentSizeH * view.ParentOrigin.Y);

            x += xDistanceFromPivotPoint + xDistanceFromParent;
            y += yDistanceFromPivotPoint + yDistanceFromParent;
        }

        /// <summary>
        /// Returns Views screen coordinates
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static Vector4 GetScreenExtents(View view)
        {
            if (view == null)
            {
                return Vector4.Zero;
            }

            float x = 0;
            float y = 0;
            float width = 0;
            float height = 0;

            View currentView = view;
            while (currentView != null)
            {
                if (currentView.InheritPosition == false)
                {
                    // if any of the nodes has a value of false of InheritPosition, we can't know the final screen position (which means the final x,y after Animation is done)
                    x = view.ScreenPosition.X;
                    y = view.ScreenPosition.Y;
                    if (view.PositionUsesPivotPoint)
                    {
                        GetXYDistanceFromPivotPoint(view, out float xDistanceFromPivotPoint, out float yDistanceFromPivotPoint);
                        x += xDistanceFromPivotPoint;
                        y += yDistanceFromPivotPoint;
                    }
                    break;
                }

                GetTopLeftPosition(currentView, out float topLeftX, out float topLeftY);

                x += topLeftX;
                y += topLeftY;

                if (currentView.IsRoot())
                {
                    break;
                }

                currentView = currentView.GetParent() as View;
            }

            // adjust position to partial window
            x += Window.Instance.WindowPosition.X;
            y += Window.Instance.WindowPosition.Y;


            width = view.Size.Width * view.WorldScale.X;
            height = view.Size.Height * view.WorldScale.Y;
            Vector4 screenExtents = new Vector4(x, y, width, height);
            return screenExtents;
        }
    }
}
