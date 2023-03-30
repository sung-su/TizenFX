/// @file Shape.cs
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

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Shape is an abstract class for Shape Drawing.
    /// </summary>
    /// <code>
    /// vectorPrimitiveView = new VectorPrimitiveView();
    /// vectorPrimitiveView.Size2D = new Size2D(100, 100);
    /// Stroke strokeStyle = new Stroke(new SolidColor(Color.Blue), 3);
    /// Fill fillStyle = new Fill(new SolidColor(Color.Red));
    /// RectShape rectShape = new RectShape(strokeStyle, fillStyle);
    /// vectorPrimitiveView.SetShape(rectShape);
    /// </code>
    public abstract class Shape
    {
        /// <summary>
        /// Create a new shape to be used in Vector View
        /// </summary>
        /// <param name="strokeStyle">Describes the style of Stroke</param>
        /// <param name="fillStyle">Describes the style for Fill</param>
        public Shape(Stroke strokeStyle = null, Fill fillStyle = null)
        {
            StrokeStyle = strokeStyle;
            FillStyle = fillStyle;
        }

        /// <summary>
        /// Represents the Paint bound for the Shape
        /// </summary>
        public Stroke StrokeStyle
        {
            set; get;
        }

        /// <summary>
        /// Represents the Paint bound for the Shape
        /// </summary>
        public Fill FillStyle
        {
            set; get;
        }

        internal virtual void Draw(VectorView view)
        {
            // Draw Fill Style
            FillStyle?.Draw(view);

            // Draw Stroke Style
            StrokeStyle?.Draw(view);
        }

        internal virtual RenderBackend GetRenderBackend()
        {
            RenderBackend fillBackend = (FillStyle != null) ? FillStyle.GetRenderBackend() : RenderBackend.DirectRenderer;
            RenderBackend strokeBackend = (StrokeStyle != null) ? StrokeStyle.GetRenderBackend() : RenderBackend.DirectRenderer;

            return (fillBackend | strokeBackend);
        }
    }
}
