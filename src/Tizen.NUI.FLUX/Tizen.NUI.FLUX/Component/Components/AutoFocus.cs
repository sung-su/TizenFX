/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file AutoFocus.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.6.0 </version>
/// <SDK_Support> Y </SDK_Support>
///
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

using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// IFocusManageable is used to provide the custom keyboard focus behavior for retrieving the next focusable view. 
    /// </summary>
    public interface IFocusManageable
    {
        /// <summary>
        /// when implement this interface, have to return view.
        /// if return null, Autofocus alg find next view base on coordinate.
        /// </summary>
        /// <param name="focusedView"> currnet focused view</param>
        /// <param name="direction"> Direction to get next focus</param>
        /// <returns> Return View instance to apply auto focus</returns>
        View GetNextFocusableFluxView(View focusedView, View.FocusDirection direction);
    }

    /// <summary>
    ///  Algorithm used to provide the custom keyboard focus algorithm for retrieving the next focusable view
    ///  UX Principls move focus to the nearest focusable component.
    /// </summary>
    public class AutoFocusAlgorithm : FocusManager.ICustomFocusAlgorithm
    {
        #region public Method
        /// <summary>
        ///  Singleton instance of  AutoFocusAlgorithm
        /// </summary>
        public static AutoFocusAlgorithm Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AutoFocusAlgorithm();
                }
                return instance;
            }
        }

        /// <summary>
        /// this value is for weight function of absolute distance ( x or y axis ).
        /// when focusmanager calculate distance, this weight multiply for absolute distance.
        /// </summary>
        /// <version> 6.6.1 </version>
        public int CustomWeight
        {
            set => customWeight = value;
            get => customWeight;
        }


        /// <summary>
        /// "current" is currnect focused view.
        ///  "proposed" shoud be null. that value is not used, just for match with base interface.
        /// </summary>
        /// <param name="current"> The view has focus </param>
        /// <param name="proposed"> The view to move focus </param>
        /// <param name="direction"> The key direction </param>
        /// <returns></returns>
        public View GetNextFocusableView(View current, View proposed, View.FocusDirection direction)
        {
            View NextView = null;

            if (!(current is View))
            {
                return current;
            }

            currentFocusedViewRect = new Rectangle(0, 0, 0, 0);
            GetViewRect(current, ref currentFocusedViewRect);
            candidateRect = GetCandidateRectByDirection(currentFocusedViewRect, direction);

            NextView = FindFocusableFluxView(current, direction, current);

            if (NextView == null)
            {
                NextView = current;
            }

            //FluxLogger.FatalP(" Return [ %s1 ] // [ %d1 ]", s1: NextView?.GetType().Name, d1: NextView?.ID ?? 0);
            return NextView;
        }
        #endregion public Method
        #region internal Method
        internal View FindFocusableSiblingsFluxView(View startingPointView, View.FocusDirection direction, View currentFocusedView)
        {

            if (currentFocusedView == null || currentFocusedView.CurrentSize == null || currentFocusedView.ScreenPosition == null || startingPointView == null)
            {
                return null;
            }

            View parentView = null;

            if (startingPointView.GetParent() is Layer)
            {
                //parentView = new FluxView();
                //parentView.Size2D = new Size2D(Window.Instance.Size.Width, Window.Instance.Size.Width);
                return null;
            }
            else
            {
                parentView = (startingPointView.GetParent() as View);
                if (parentView == null)
                {
                    return null;
                }
                View chainFocusableView = GetChainFocusableView(parentView, direction);
                if(chainFocusableView !=null)
                {
                    return chainFocusableView;
                }
            }
            return FindCandidateView(parentView, currentFocusedView, direction);
        }

        internal View FindNextFocusableView(View root, View current, View.FocusDirection direction)
        {
            if (root == null || current == null)
            {
                return null;
            }
            View nextFocusView = null;
            List<View> focusableViews = new List<View>();

            AddFocusableViews(root, current, focusableViews);
            nextFocusView = FindCandidateView(current, direction, focusableViews);

            focusableViews.Clear();
            focusableViews = null;

            return nextFocusView;
        }

        internal View FindNearestFocusableView(View root, Rectangle baseRect, View.FocusDirection direction)
        {
            if (root == null || baseRect == null)
            {
                return null;
            }
            List<View> focusableViews = new List<View>();

            AddFocusableViews(root, null, focusableViews);

            View candidateView = null;
            Rectangle candidateRect = GetCandidateRectByDirection(baseRect, direction);

            foreach (View view in focusableViews)
            {
                Rectangle newViewRect = new Rectangle(0, 0, 0, 0);
                GetViewRect(view, ref newViewRect);

                if (IsBetterCandidate(baseRect, newViewRect, candidateRect, direction))
                {
                    candidateView = view;
                    candidateRect = newViewRect;
                }
            }

            focusableViews.Clear();
            focusableViews = null;

            FluxLogger.InfoP("root: %s1 baseRect: %d1,%d2 %d3*%d4 direction: %s2 candidateView: [%d5]%s3"
                , s1: root?.GetTypeName(), d1: baseRect.X, d2: baseRect.Y, d3: baseRect.Width, d4: baseRect.Height, s2: FluxLogger.EnumToString(direction), d5: candidateView?.ID ?? 0, s3: candidateView?.Name);
            return candidateView;
        }

        #endregion internal Method
        #region private Method

        private View GetChainFocusableView(View view, View.FocusDirection direction)
        {
            return direction switch
            {
                View.FocusDirection.Left =>  view.LeftFocusableView,
                View.FocusDirection.Right =>  view.RightFocusableView,
                View.FocusDirection.Up =>  view.UpFocusableView,
                View.FocusDirection.Down =>  view.DownFocusableView,
                _ => null
            };
        }

        private void AddFocusableViews(View root, View current, List<View> focusableViewsList)
        {

            FluxLogger.InfoP("%s1", s1: root.Name);
            if (focusableViewsList == null)
            {
                return;
            }
            foreach (View child in root.Children)
            {
                if (child == null || !child.Visibility)
                {
                    continue;
                }
                if (child.Focusable && child != current)
                {
                    focusableViewsList.Add(child);
                }
                AddFocusableViews(child, current, focusableViewsList);
            }
        }

        private Rectangle GetCandidateRectByDirection(Rectangle rect, View.FocusDirection direction)
        {
            return direction switch
            {
                View.FocusDirection.Left => new Rectangle(rect.X + rect.Width + 1, rect.Y, rect.Width, rect.Height),
                View.FocusDirection.Right => new Rectangle(rect.X - rect.Width + 1, rect.Y, rect.Width, rect.Height),
                View.FocusDirection.Up => new Rectangle(rect.X, rect.Y + rect.Height + 1, rect.Width, rect.Height),
                View.FocusDirection.Down => new Rectangle(rect.X, rect.Y - rect.Height + 1, rect.Width, rect.Height),
                _ => new Rectangle(rect.X, rect.Y, rect.Width, rect.Height)
            };
        }

        private View FindCandidateView(View current, View.FocusDirection direction, List<View> focusableViewsList)
        {
            if (focusableViewsList == null)
            {
                return null;
            }

            View candidateView = null;
            Rectangle focusedRect = new Rectangle(0, 0, 0, 0);

            GetViewRect(current, ref focusedRect);
            Rectangle candidateRect = GetCandidateRectByDirection(focusedRect, direction);

            foreach (View view in focusableViewsList)
            {
                Rectangle newViewRect = new Rectangle(0, 0, 0, 0);
                GetViewRect(view, ref newViewRect);

                if (IsBetterCandidate(focusedRect, newViewRect, candidateRect, direction))
                {
                    candidateView = view;
                    candidateRect = newViewRect;
                }
            }
            return candidateView;
        }

        private AutoFocusAlgorithm()
        {
        }

        private View FindFocusableFluxView(View startingPointView, View.FocusDirection direction, View currentFocusedView)
        {
            if (startingPointView == null || currentFocusedView == null)
            {
                return null;
            }

            View NextFluxView = null;

            NextFluxView = (startingPointView as IFocusManageable)?.GetNextFocusableFluxView(currentFocusedView, direction);

            if (NextFluxView == null)
            {
                NextFluxView = FindFocusableSiblingsFluxView(startingPointView, direction, currentFocusedView);
            }

            if (NextFluxView == null)
            {
                // Recursive call ( target view change to parent )
                NextFluxView = FindFocusableFluxView((startingPointView.GetParent() as View), direction, currentFocusedView);
            }
            else
            {
                // NextFluxView able to has child. ( target view change to child )
                NextFluxView = FindFocusableChildFluxView(NextFluxView, direction, currentFocusedView);
            }
            return NextFluxView;
        }

        private View FindFocusableChildFluxView(View targetTraversalView, View.FocusDirection direction, View currentFocusedView)
        {
            View nextviewHasChild = targetTraversalView;

            while (nextviewHasChild != null)
            {
                nextviewHasChild = (targetTraversalView as IFocusManageable)?.GetNextFocusableFluxView(currentFocusedView, direction);

                if (nextviewHasChild != null)
                {
                    targetTraversalView = nextviewHasChild;
                }

                if (nextviewHasChild == null && targetTraversalView == null)
                {
                    break;
                }
            }
            return targetTraversalView;
        }

        private IEnumerable<View> GetChildrenByDirection(View view, View.FocusDirection direction)
        {
            if (direction != View.FocusDirection.Up)
            {
                for (int index = 0; index < (int)view.ChildCount; index++)
                {
                    yield return view.Children[index];
                }
            }
            else
            {
                for (int index = (int)view.ChildCount - 1; index >= 0; index--)
                {
                    yield return view.Children[index];
                }
            }
        }

        private View FindCandidateView(View parentView, View focusedView, View.FocusDirection direction)
        {
            View nextView = null;
            //FluxLogger.FatalP("parent >>>>>>>>>> [ %s1 ] // [ %d1 ]", s1: parentView?.GetType().Name, d1: parentView?.ID ?? 0);
            if (parentView == null)
            {
                return null;
            }

            foreach (View children in GetChildrenByDirection(parentView, direction))
            {
                View childView = children;
                Rectangle childViewRect = new Rectangle(0, 0, 0, 0);

                if (childView == null || childView == focusedView || !childView.Visibility)
                {
                    continue;
                }
                //FluxLogger.FatalP(" child [ %s1 ] // [ %d1 ] V [ %d2 ] F [ %d3 ] child [ %d4 ] ", s1: childView?.GetType().Name, d1: childView?.ID ?? 0, d2: Convert.ToInt32(!childView.Visibility), d3: Convert.ToInt32(childView.Focusable), d4: childView.GetChildCount());
                if (!childView.Focusable)
                {
                    if (childView.ChildCount == 0)
                    {
                        continue;
                    }
                    else
                    {
                        //FluxLogger.FatalP("Ori[(%d1)%s1] vs Candi[ (%d2)%s2 ] :: [ %d3 ]", d1: focusedView.ID, s1: focusedView.GetType().Name, d2: childView.ID, s2: childView.GetType().Name, d3: Convert.ToInt32(IsBetterCandidate(currentFocusedViewRect, childViewRect, candidateRect, direction)));
                        GetViewRect(childView, ref childViewRect);

                        if (IsBetterCandidate(currentFocusedViewRect, childViewRect, candidateRect, direction))
                        {
                            if (childView is IFocusManageable)
                            {
                                childView = (childView as IFocusManageable)?.GetNextFocusableFluxView(focusedView, direction);
                            }
                            else
                            {
                                childView = FindFocusableSiblingsFluxView(childView.Children[0], direction, focusedView);
                            }

                            if (childView == null)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                GetViewRect(childView, ref childViewRect);
                if (IsBetterCandidate(currentFocusedViewRect, childViewRect, candidateRect, direction))
                {
                    nextView = childView;
                    candidateRect = childViewRect;
                }
            }

            if (nextView == focusedView)
            {
                nextView = null;
            }
            //FluxLogger.FatalP("nexTarget [ %s1 ] // [ %d1 ]",s1: nextTarget?.GetType().Name,d1: nextTarget?.ID);
            //FluxLogger.FatalP("parent <<<<<<<<<< [ %s1 ] // [ %d1 ]", s1: parentView?.GetType().Name, d1: parentView?.ID ?? 0);
            return nextView;
        }


        // Because View's ScreenPosition will be affected by Scale, so use parent's ScreenPosition to reduce the error
        private void GetViewRect(View view, ref Rectangle rect)
        {
            View parent = view.GetParent() as View;

            if (parent == null)
            {
                GetRect(view, ref rect);
            }
            else
            {
                if (view.PositionUsesPivotPoint == true)
                {
                    rect.X = (int)(view.ScreenPosition.X - view.SizeWidth * view.PivotPoint.X);
                    rect.Y = (int)(view.ScreenPosition.Y - view.SizeHeight * view.PivotPoint.Y);
                }
                else
                {
                    float correctionFactor = 0.0f;
                    if ((view is Component component) && (component.AutoFocusRoundingOffEnabled == true))
                    {
                        correctionFactor = 0.5f;
                    }
                    rect.X = (int)(view.ScreenPosition.X + correctionFactor);
                    rect.Y = (int)(view.ScreenPosition.Y + correctionFactor);
                }

                rect.Width = (int)(view.SizeWidth * view.WorldScale.X);
                rect.Height = (int)(view.SizeHeight * view.WorldScale.Y);
            }
        }

        // This api didn't consider view's screenposition will be affected by scale, if the view may scale, please use GetViewRect.
        private void GetRect(View view, ref Rectangle rect)
        {
            if (view.PositionUsesPivotPoint == true)
            {
                rect.X = (int)(view.ScreenPosition.X - view.SizeWidth * view.PivotPoint.X);
                rect.Y = (int)(view.ScreenPosition.Y - view.SizeHeight * view.PivotPoint.Y);
            }
            else
            {
                rect.X = (int)view.ScreenPosition.X;
                rect.Y = (int)view.ScreenPosition.Y;
            }

            rect.Width = (int)(view.SizeWidth * view.WorldScale.X);
            rect.Height = (int)(view.SizeHeight * view.WorldScale.Y);
        }

        private Position2D GetTopLeftPosition(View view)
        {
            int ox = (int)(view.ParentOrigin.X * view.Parent?.SizeWidth ?? 0);
            int oy = (int)(view.ParentOrigin.Y * view.Parent?.SizeHeight ?? 0);
            int px = view.PositionUsesPivotPoint ? (int)(view.PivotPoint.X * view.SizeWidth) : 0;
            int py = view.PositionUsesPivotPoint ? (int)(view.PivotPoint.Y * view.SizeHeight) : 0;
            Position2D ret = new Position2D(ox + view.Position2D.X - px, oy + view.Position2D.Y - py);
            return ret;
        }


        private bool IsBetterCandidate(Rectangle focusedRect, Rectangle newViewRect, Rectangle candidateRect, View.FocusDirection direction)
        {
            if (!IsCandidate(focusedRect, newViewRect, direction))
            {
                return false;
            }

            if (!IsCandidate(focusedRect, candidateRect, direction))
            {
                return true;
            }

            if (BeamBeats(focusedRect, newViewRect, candidateRect, direction))
            {
                return true;
            }

            if (BeamBeats(focusedRect, candidateRect, newViewRect, direction))
            {
                return false;
            }
            return (GetWeightedDistanceFor(MajorAxisDistance(focusedRect, newViewRect, direction), MinorAxisDistance(focusedRect, newViewRect, direction))
                <= GetWeightedDistanceFor(MajorAxisDistance(focusedRect, candidateRect, direction), MinorAxisDistance(focusedRect, candidateRect, direction)));
        }

        private long GetWeightedDistanceFor(long majorAxisDistance, long minorAxisDistance)
        {
            return customWeight * majorAxisDistance * majorAxisDistance + minorAxisDistance * minorAxisDistance;
        }

        private bool BeamBeats(Rectangle focusedRect, Rectangle viewRect1, Rectangle viewRect2, View.FocusDirection direction)
        {
            bool rect1Beam = BeamsOverlap(focusedRect, viewRect1, direction);
            bool rect2Beam = BeamsOverlap(focusedRect, viewRect2, direction);
            if (rect2Beam || !rect1Beam)
            {
                return false;
            }

            if (!IsToDirectionOf(focusedRect, viewRect2, direction))
            {
                return true;
            }

            if (direction == View.FocusDirection.Left || direction == View.FocusDirection.Right)
            {
                return false;
            }

            return (MajorAxisDistance(focusedRect, viewRect1, direction) < MajorAxisDistanceToFarEdge(focusedRect, viewRect2, direction));
        }

        private int MajorAxisDistanceToFarEdge(Rectangle src, Rectangle dest, View.FocusDirection direction)
        {
            return Math.Max(1, MajorAxisDistanceToFarEdgeRaw(src, dest, direction));
        }

        private int MajorAxisDistanceToFarEdgeRaw(Rectangle src, Rectangle dest, View.FocusDirection direction)
        {
            switch (direction)
            {
                case View.FocusDirection.Left:
                    return src.X - dest.X;
                case View.FocusDirection.Right:
                    return dest.Right() - src.Right();
                case View.FocusDirection.Up:
                    return src.Y - dest.Y;
                case View.FocusDirection.Down:
                    return dest.Bottom() - src.Bottom();
                default:
                    return 0;
            }
        }

        private int MajorAxisDistance(Rectangle source, Rectangle dest, View.FocusDirection direction)
        {
            return Math.Max(0, MajorAxisDistanceRaw(source, dest, direction));
        }

        private int MinorAxisDistance(Rectangle src, Rectangle dest, View.FocusDirection direction)
        {
            switch (direction)
            {
                case View.FocusDirection.Left:
                case View.FocusDirection.Right:
                    return Math.Abs((src.Y + src.Height / 2) - ((dest.Y + dest.Height / 2)));
                case View.FocusDirection.Up:
                case View.FocusDirection.Down:
                    return Math.Abs((src.X + src.Width / 2) - ((dest.X + dest.Width / 2)));
                default:
                    return 0;
            }
        }

        private int MajorAxisDistanceRaw(Rectangle src, Rectangle dest, View.FocusDirection direction)
        {
            switch (direction)
            {
                case View.FocusDirection.Left:
                    return src.X - dest.Right();
                case View.FocusDirection.Right:
                    return dest.X - src.Right();
                case View.FocusDirection.Up:
                    return src.Y - dest.Bottom();
                case View.FocusDirection.Down:
                    return dest.Y - src.Bottom();
                default:
                    return 0;
            }
        }

        private bool IsCandidate(Rectangle focusedRect, Rectangle candidateRect, View.FocusDirection direction)
        {
            switch (direction)
            {
                case View.FocusDirection.Left:
                    return (focusedRect.Right() > candidateRect.Right() || focusedRect.X >= candidateRect.Right()) && focusedRect.X > candidateRect.X;
                case View.FocusDirection.Right:
                    return (focusedRect.X < candidateRect.X || focusedRect.Right() <= candidateRect.X) && focusedRect.Right() < candidateRect.Right();
                case View.FocusDirection.Up:
                    return (focusedRect.Bottom() > candidateRect.Bottom() || focusedRect.Y >= candidateRect.Bottom()) && focusedRect.Y > candidateRect.Y;
                case View.FocusDirection.Down:
                    return (focusedRect.Y < candidateRect.Y || focusedRect.Bottom() <= candidateRect.Y) && focusedRect.Bottom() < candidateRect.Bottom();
                default:
                    return false;
            }
        }


        private bool IsToDirectionOf(Rectangle src, Rectangle dest, View.FocusDirection direction)
        {
            switch (direction)
            {
                case View.FocusDirection.Left:
                    return src.X >= dest.Right();
                case View.FocusDirection.Right:
                    return src.Right() <= dest.X;
                case View.FocusDirection.Up:
                    return src.Y >= dest.Bottom();
                case View.FocusDirection.Down:
                    return src.Bottom() <= dest.Y;
                default:
                    return false;
            }
        }

        private bool BeamsOverlap(Rectangle rect1, Rectangle rect2, View.FocusDirection direction)
        {
            switch (direction)
            {
                case View.FocusDirection.Left:
                case View.FocusDirection.Right:
                    return (rect2.Bottom() > rect1.Y) && (rect2.Y < rect1.Bottom());
                case View.FocusDirection.Up:
                case View.FocusDirection.Down:
                    return (rect2.Right() > rect1.X) && (rect2.X < rect1.Right());
                default:
                    return false;
            }
        }
        #endregion private Method
        #region private Field
        private Rectangle currentFocusedViewRect = new Rectangle(0, 0, 0, 0);
        private Rectangle candidateRect = new Rectangle(0, 0, 0, 0);

        private static AutoFocusAlgorithm instance = null;
        // this value is huristic by Android.
        // https://android.googlesource.com/platform/frameworks/base/+/master/core/java/android/view/FocusFinder.java
        private int customWeight = 13;
        #endregion private Field
    }
}
