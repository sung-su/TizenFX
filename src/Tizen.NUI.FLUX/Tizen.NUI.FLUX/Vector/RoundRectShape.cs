/// @file RoundRectShape.cs
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
    /// Rounded Rectangle is a Shape to be added to NUICanvas
    /// </summary>
    /// <code>
    /// vectorPrimitiveView = new VectorPrimitiveView();
    /// vectorPrimitiveView.Size2D = new Size2D(100, 100);
    /// Stroke strokeStyle = new Stroke(new SolidColor(Color.Red), 1);
    /// Fill fillStyle = new Fill(new SolidColor(Color.Blue));
    /// RoundRectShape roundRectShape = new RoundRectShape(5, 5, strokeStyle, fillStyle);
    /// vectorPrimitiveView.SetShape(roundRectShape);
    /// </code>
    public class RoundRectShape : RectShape
    {
        /// <summary>
        /// Create a new Rounded Rectangle Shape to be added to VectorPrimitiveView
        /// </summary>
        /// <param name="xRadius">X-Axis Radius</param>
        /// <param name="yRadius">Y-Axis Radius</param>
        /// <param name="strokeStyle">Style for drawing stroke of shape</param>
        /// <param name="fillStyle">Style for drawing fill of shape</param>
        public RoundRectShape(int xRadius, int yRadius, Stroke strokeStyle = null, Fill fillStyle = null)
            : base(strokeStyle, fillStyle)
        {
            mRadius[0] = mRadius[1] = mRadius[2] = mRadius[3] = new Vector2(xRadius, yRadius);
        }

        /// <summary>
        /// Radius of each vertex given in clockwise direction starting from top-left position
        /// i.e. 0 : topleft, 1 : top-right, 2 : bottom-left, 3 : bottom-left
        /// </summary>
        /// <param name="radiusArr"></param>
        /// <param name="strokeStyle"></param>
        /// <param name="fillStyle"></param>
        public RoundRectShape(Vector2[] radiusArr, Stroke strokeStyle = null, Fill fillStyle = null)
            : base(strokeStyle, fillStyle)
        {
            for (uint i = 0; i < radiusArr.Length; ++i)
            {
                mRadius[i] = radiusArr[i];
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        internal override void Draw(VectorView view)
        {
            base.Draw(view);
            float[] radiusArr = new float[mRadius.Length*2];
            for (uint i = 0; i < mRadius.Length; ++i)
            {
                //radiusArr[i] = Vector2.getCPtr(mRadius[i]).Handle;
                radiusArr[i*2] = mRadius[i].X;
                radiusArr[(i*2)+1] = mRadius[i].Y;
            }
            Interop.VectorView.AddRoundedRect(View.getCPtr(view), radiusArr);
        }

        private Vector2[] mRadius = new Vector2[4];
    }
}
