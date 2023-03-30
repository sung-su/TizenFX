/// @file VectorPrimitiveView.cs
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

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// VectorPrimitiveView is a class for displaying a vector primitives.
    /// </summary>
    /// <code>
    /// vectorPrimitiveView = new VectorPrimitiveView();
    /// vectorPrimitiveView.Size2D = new Size2D(100, 100);
    /// Stroke strokeStyle = new Stroke(new SolidColor(Color.Red), 1);
    /// Fill fillStyle = new Fill(new SolidColor(Color.Blue));
    /// RectShape rectShape = new RectShape(strokeStyle, fillStyle);
    /// vectorPrimitiveView.SetShape(rectShape);
    /// </code>
    public class VectorPrimitiveView : VectorView
    {
        /// <summary>
        /// Set a shape to Vector View. ( Previously added shapes will be removed )
        /// </summary>
        /// <param name="shape">Shape set to the Vector view</param>
        public override void SetShape(Shape shape)
        {
            if (shape == null)
            {
                throw new ArgumentNullException("Shape is null");
            }
            Draw(shape);
        }

        /// <summary>
        /// Draws the Canvas. Will be called from Cairo thread
        /// </summary>
        private void Draw(Shape shape)
        {
            if (SwigCPtr.Handle == IntPtr.Zero)
            {
                throw new ArgumentNullException("Invalid VectorView Object");
            }
            Interop.VectorView.SetVectorRenderBackend(SwigCPtr, (uint)shape.GetRenderBackend());
            Interop.VectorView.SetViewBox(SwigCPtr, Size2D.Width, Size2D.Height);
            shape.Draw(this);
            Interop.VectorView.Flush(SwigCPtr);
        }
    }
}
