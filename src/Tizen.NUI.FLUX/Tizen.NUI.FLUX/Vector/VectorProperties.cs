/// @file VectorProperties.cs
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
    ///
    /// </summary>
    internal enum RenderBackend
    {
        /// <summary>
        /// Direct GL Rendering
        /// </summary>
        DirectRenderer = 0,

        /// <summary>
        /// Cairo SW Rendering
        /// </summary>
        CairoRenderer
    }

    /// <summary>
    /// Describes the Attributes used in Vector Objects
    /// </summary>
    internal class VectorProperties
    {
        /// <summary>
        /// Enumeration for CanvasCapStyle
        /// </summary>
        internal enum PaintStyle
        {
            None = 0,
            Fill = 1,
            Stroke = 2,
            //Shadow = 4,
            FillAndStroke = Fill | Stroke
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        internal static uint ColorToInt(Color color)
        {
            byte R = (byte)(color.R * 255);
            byte G = (byte)(color.G * 255);
            byte B = (byte)(color.B * 255);
            byte A = (byte)(color.A * 255);

            CLog.Debug("color : [%f1, %f2, %f3, %f4]"
                , f1: color.R * 255
                , f2: color.G * 255
                , f3: color.B * 255
                , f4: color.A * 255
                );
            uint col = ((uint)A << 24) | (uint)(R << 16) | ((uint)G << 8) | (B);
            return col;
        }
    }
}
