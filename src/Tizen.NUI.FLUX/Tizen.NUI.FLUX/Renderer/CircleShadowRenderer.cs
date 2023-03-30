/// @file CircleShadowRenderer.cs
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
    /// Draws Circle Shadow
    /// </summary>
    internal class CircleShadowRenderer : PrimitiveShadowRenderer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        internal CircleShadowRenderer() : base(GeometryFactory.Instance.GetGeometry(GeometryFactory.GeometryType.QUAD), ShaderFactory.Instance.GetShader(ShaderFactory.ShaderType.CIRCLE_COLOR))
        {
            UpdateCircleUniforms();
        }

        private void UpdateCircleUniforms()
        {
            base.UpdateUniforms();

            // Update uniforms specific to Circle primitive if any
            //For default antialiasing.
            float edgeBlur = (blurSize > 2.0f) ? blurSize : 2.0f;
            // smooth step require both side.
            RegisterProperty("uPrimitiveOffset_blurSize", new PropertyValue(new Vector4(offset.X, Offset.Y, blurSize, edgeBlur)));
        }

        /// <summary>
        /// Update uniforms
        /// </summary>
        protected override void UpdateUniforms()
        {
            UpdateCircleUniforms();
        }
    }
}
