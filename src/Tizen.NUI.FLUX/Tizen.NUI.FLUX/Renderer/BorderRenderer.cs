/// @file BorderRenderer.cs
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
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Renders a solid color to the control's quad border fixed to a specified size.
    /// </summary>
    internal class BorderRenderer : Renderer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        internal BorderRenderer() : base(GeometryFactory.Instance.GetGeometry(GeometryFactory.GeometryType.BORDER), ShaderFactory.Instance.GetShader(ShaderFactory.ShaderType.BORDER))
        {
            DepthIndex = Ranges.ForegroundEffect;
            BlendMode = 2; // Blending is enabled.
        }

        private uint borderWidth = 0;
        /// <summary>
        /// Set the width of the border.
        /// </summary>
        internal uint BorderWidth
        {
            set
            {
                borderWidth = value;
                RegisterProperty("uBorderWidth", new PropertyValue(Convert.ToSingle(borderWidth)));
            }
            get
            {
                return borderWidth;
            }
        }

        private Color borderColor = Color.Transparent;
        /// <summary>
        /// Set the color of the border.
        /// </summary>
        internal Color BorderColor
        {
            set
            {
                borderColor = value;
                RegisterProperty("uBorderColor", new PropertyValue(borderColor));
            }
            get
            {
                return borderColor;
            }
        }
    }
}
