/// @file GradientColor.cs
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

using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    using Point = Vector2;
    /// <summary>
    /// GradientEffect is a class for Gradient specs.
    /// </summary>
    /// <code>
    /// LinearGradientColor gradientColor = new LinearGradientColor(new Position2D(100, 0), new Position2D(100, 100));
    /// gradientColor.AddGradientStop(new ColorStops(Color.Red, 0));
    /// gradientColor.AddGradientStop(new ColorStops(Color.Green, 0.5f));
    /// gradientColor.AddGradientStop(new ColorStops(Color.Blue, 1));
    /// </code>
    public abstract class GradientColor : Paint
    {
        /// <summary>
        /// Describes Type of Style present (Linear/Gradient)
        /// </summary>
        protected enum Style
        {
            /// <summary>
            /// Linear Style Gradient
            /// </summary>
            Linear,

            /// <summary>
            /// Linear Style Gradient
            /// </summary>
            Radial
        }

        internal List<ColorStops> mGradientColorStop = new List<ColorStops>();

        /// <summary>
        /// Gradient color stop
        /// </summary>
        /// <param name="color"> Represents a Gradient Point</param>
        public void AddGradientStop(ColorStops color)
        {
            mGradientColorStop.Add(color);
        }

        internal List<ColorStops> GradientColorStops => mGradientColorStop;
        internal override RenderBackend GetRenderBackend()
        {
            return RenderBackend.CairoRenderer;
        }

        internal void SetGradient(VectorView view, int style, Point startCenter, float startRadius, Point endCenter, float endRadius, List<ColorStops> gradientColors, byte alpha)
        {
            float[] pos = new float[gradientColors.Count];
            uint[] col = new uint[gradientColors.Count];

            for (int i = 0; i < gradientColors.Count; ++i)
            {
                pos[i] = gradientColors[i].Position;
                col[i] = VectorProperties.ColorToInt(gradientColors[i].Color);
            }

            Interop.VectorView.SetGradient(View.getCPtr(view), style, startCenter.X, startCenter.Y, startRadius, endCenter.X, endCenter.Y, endRadius, pos.Length, pos, col);
        }
    }
}
