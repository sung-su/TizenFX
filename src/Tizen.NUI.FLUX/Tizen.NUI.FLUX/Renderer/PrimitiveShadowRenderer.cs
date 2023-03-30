/// @file PrimitiveShadowRenderer.cs
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


namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Abstract class for Primitive shadow
    /// </summary>
    internal abstract class PrimitiveShadowRenderer : Renderer
    {

        /// <summary>
        /// Constructor
        /// </summary>
        internal PrimitiveShadowRenderer(Geometry geometry, Shader shader) : base(geometry, shader)
        {
            DepthIndex = Ranges.BackgroundEffect;
            this.BlendMode = 2;
        }

        /// <summary>
        /// Update Uniforms
        /// </summary>
        protected virtual void UpdateUniforms()
        {
            RegisterProperty("uPrimitiveColor", new PropertyValue(color));
            RegisterProperty("uPrimitiveOffset_blurSize", new PropertyValue( new Vector4(offset.X, Offset.Y, blurSize, blurSize)));
      } 

        protected Vector2 offset = Vector2.Zero;
        /// <summary>
        /// Set shadow offset
        /// </summary>
        internal Vector2 Offset
        {
            set
            {
                offset = value;
                UpdateUniforms();
            }
            get
            {
                return offset;
            }
        }

        protected Color color = Color.Transparent;
        /// <summary>
        /// Set shadow Color
        /// </summary>
        internal Color Color
        {
            set
            {
                color = value;
                UpdateUniforms();
            }
            get
            {
                return color;
            }
        }

        protected float blurSize = 0.0f;
        /// <summary>
        /// Set BlurSize
        /// </summary>
        internal float BlurSize
        {
            set
            {
                blurSize = value;
                UpdateUniforms();
            }
            get
            {
                return blurSize;
            }
        }
    }
}
