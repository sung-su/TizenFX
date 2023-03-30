/// @file SolidColor.cs
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

using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Solid Effect is a class for setting solid color in Paint.
    /// </summary>
    /// <code>
    /// SolidColor blue = new SolidColor(Color.Blue);
    /// Fill fillStyle = new Fill(blue);
    /// </code>
    public class SolidColor : Paint
    {
        /// <summary>
        /// Create a new solid color pattern for paint to be applied on a shape
        /// </summary>
        /// <param name="color">Solid Color of the Paint</param>
        public SolidColor(Color color)
        {
            Color = color ?? Color.White;
        }

        /// <summary>
        /// Solid Color of the Paint
        /// </summary>
        public Color Color
        {
            set; get;
        }

        internal override void Draw(VectorView view)
        {
            Interop.VectorView.SetPaintColor(View.getCPtr(view), Color.getCPtr(Color));
        }

        internal override RenderBackend GetRenderBackend()
        {
            return RenderBackend.DirectRenderer;
        }
    }
}
