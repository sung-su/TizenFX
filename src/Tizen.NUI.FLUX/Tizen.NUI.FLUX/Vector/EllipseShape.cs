/// @file EllipseShape.cs
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

using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Add an elliptical shape to the view
    /// </summary>
    public class EllipseShape : Shape
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="strokeStyle">Style for drawing stroke of shape</param>
        /// <param name="fillStyle">Style for drawing fill of shape</param>
        /// <code>
        /// vectorPrimitiveView = new VectorPrimitiveView();
        /// vectorPrimitiveView.Size2D = new Size2D(100, 100);
        /// Fill fillStyle = new Fill(new SolidColor(Color.Red));
        /// EllipseShape ellipseShape = new EllipseShape(null, fillStyle);
        /// vectorPrimitiveView.SetShape(ellipseShape);
        /// </code>
        public EllipseShape(Stroke strokeStyle = null, Fill fillStyle = null)
            : base(strokeStyle, fillStyle)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        internal override void Draw(VectorView view)
        {
            base.Draw(view);
            Interop.VectorView.AddEllipse(View.getCPtr(view));
        }
    }
}
