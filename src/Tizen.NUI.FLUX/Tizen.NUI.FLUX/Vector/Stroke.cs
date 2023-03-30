/// @file Stroke.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.6.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
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

using System;
using System.Collections.Generic;
using static Tizen.NUI.FLUX.VectorProperties;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Stroke is a class for stroke Attributes
    /// </summary>
    /// <code>
    /// Stroke strokeStyle = new Stroke(new SolidColor(Color.Blue), 3);
    /// </code>
    public class Stroke
    {
        /// <summary>
        /// Describes the CapStyle of the Stroke
        /// </summary>
        public enum Cap
        {
            /// <summary>
            /// Begin/end contours with no extension.
            /// </summary>
            Flat,

            /// <summary>
            /// Begin/end contours with a semi-circle extension.
            /// </summary>
            Round,

            /// <summary>
            /// Begin/end contours with a half square extension.
            /// </summary>
            Square,

            /// <summary>
            /// Begin/end contours with a half triangle extension.
            /// </summary>
            Triangle
        }

        /// <summary>
        /// Describes the Joining of two strokes. This is the treatment that is applied to corners in paths and rectangles.
        /// </summary>
        public enum Join
        {
            /// <summary>
            /// Connect path segments with a sharp join.
            /// </summary>
            Miter,

            /// <summary>
            /// Connect path segments with a round join.
            /// </summary>
            Round,

            /// <summary>
            ///  Connect path segments with a flat bevel join.
            /// </summary>
            Bevel,

            /// <summary>
            ///  Connect path segments with a sharp/flat bevel join.
            /// </summary>
            MiterOrBevel
        }

        private List<Dash> mDashList = new List<Dash>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paintColor">Paint Color to use</param>
        /// <param name="width">Width of the Stroke</param>
        /// <param name="strokeCap">CapStyle to be used at the end of open subpaths</param>
        /// <param name="strokeJoin">LineJoin to be used for the stroke</param>
        /// <param name="dashList">List of dashes if the stroke is to be dashed</param>
        /// <exception cref="ArgumentNullException">Thrown when paintColor instance is null.</exception>
        public Stroke(Paint paintColor, uint width = 1, Cap strokeCap = Cap.Square,
            Join strokeJoin = Join.Miter, List<Dash> dashList = null)
        {
            PaintColor = paintColor ?? throw new ArgumentNullException("PaintColor cannot be null");
            StrokeWidth = width;
            CapStyle = strokeCap;
            JoinStyle = strokeJoin;
            dashList?.ForEach(dash => mDashList.Add(dash));
        }

        /// <summary>
        /// Width of the Stroke
        /// </summary>
        public uint StrokeWidth { set; get; }

        /// <summary>
        /// CapStyle to be used for the stroke
        /// </summary>
        public Cap CapStyle { set; get; }

        /// <summary>
        /// LineJoin to be used for the stroke
        /// </summary>
        public Join JoinStyle { set; get; }

        /// <summary>
        /// Paint Color to use in the style
        /// </summary>
        public Paint PaintColor
        {
            set; get;
        }

        /// <summary>
        /// Set a dash list for applying dashes to stroke
        /// </summary>
        /// <param name="dashList">Dash list of stroke</param>
        public void SetDashes(List<Dash> dashList)
        {
            mDashList.Clear();
            dashList.ForEach(dash => mDashList.Add(dash));
        }

        internal void Draw(VectorView view)
        {
            Interop.VectorView.SetPaintStyle(View.getCPtr(view), (uint)PaintStyle.Stroke);
            Interop.VectorView.SetStrokeWidth(View.getCPtr(view), StrokeWidth);
            Interop.VectorView.SetLineJoinStyle(View.getCPtr(view), (uint)JoinStyle);
            Interop.VectorView.SetLineCapStyle(View.getCPtr(view), (uint)CapStyle);

            PaintColor.Draw(view);
            DrawDash(view, 0);
        }

        internal RenderBackend GetRenderBackend()
        {
            if (PaintColor.GetRenderBackend() == RenderBackend.CairoRenderer)
            {
                return RenderBackend.CairoRenderer;
            }
            if (JoinStyle != Join.Miter || CapStyle != Cap.Square)
            {
                return RenderBackend.CairoRenderer;
            }
            return RenderBackend.DirectRenderer;
        }

        private void DrawDash(VectorView view, float offset)
        {
            if (mDashList != null && mDashList.Count > 0) //TODO: tuple
            {
                float[] dashes = new float[2 * mDashList.Count];
                //TODO: Use CopyTo / ToArray
                for (int i = 0; i < mDashList.Count; i++)
                {
                    dashes[2 * i] = mDashList[i].Length;
                    dashes[2 * i + 1] = mDashList[i].Interval;
                }

                Interop.VectorView.SetDash(View.getCPtr(view), dashes, dashes.Length, offset);
            }
        }
    }
}
