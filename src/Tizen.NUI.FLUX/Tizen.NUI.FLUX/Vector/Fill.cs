/// @file Fill.cs
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
using Tizen.NUI.BaseComponents;
using static Tizen.NUI.FLUX.VectorProperties;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Fill is a class for fill Attributes
    /// </summary>
    /// <code>
    /// Fill fillStyle = new Fill(new SolidColor(Color.Red));
    /// </code>
    public class Fill
    {
        /// <summary>
        /// Describes the order of filling paths
        /// </summary>
        public enum PathFillType
        {
            /// <summary>
            ///  Specifies that "inside" is computed by an odd number of edge crossings.
            /// </summary>
            EvenOdd,

            /// <summary>
            ///  Specifies that "inside" is computed by a non-zero sum of signed edge crossings.
            /// </summary>
            Winding
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="paintColor"></param>
        /// <param name="fillType"></param>
        public Fill(Paint paintColor, PathFillType fillType = PathFillType.Winding)
        {
            PaintColor = paintColor ?? throw new ArgumentNullException("PaintColor cannot be null");
            FillType = fillType;
        }

        /// <summary>
        /// Specifies the winding rule to fill the shape
        /// </summary>
        public PathFillType FillType
        {
            set; get;
        }

        /// <summary>
        /// Paint Color to use in the style
        /// </summary>
        public Paint PaintColor
        {
            set; get;
        }

        internal void Draw(VectorView view)
        {
            Interop.VectorView.SetPaintStyle(View.getCPtr(view), (uint)PaintStyle.Fill);
            PaintColor.Draw(view);
        }

        internal RenderBackend GetRenderBackend()
        {
            return PaintColor.GetRenderBackend();
        }
    }
}
