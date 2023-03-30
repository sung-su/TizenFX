/// @file Interpolation.cs
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
    /// Enumeration for Image Scaling Interpolation type
    /// </summary>
    /// <version> 6.6.0 </version>
    public enum Interpolation : uint
    {
        /// <summary>
        /// Nearest neighbor sampling.
        /// This is the fastest and lowest quality mode.
        /// </summary>
        Nearest = 0,

        /// <summary>
        /// Resembles nearest neighbor for enlargement, and bilinear for reduction.
        /// Each pixel is rendered as a tiny parallelogram of solid color, 
        /// the edges of which are implemented with antialiasing.
        /// </summary>
        Tiles = 1,

        /// <summary>
        /// Best quality/speed balance. Use this mode by default.
        /// For enlargement, it is equivalent to point-sampling the ideal bilinear-interpolated image. 
        /// For reduction, it is equivalent to laying down small tiles and integrating over the coverage area.
        /// </summary>
        BiLinear = 2,

        /// <summary>
        /// This is slowest but highest quality mode.
        /// It is derived from the hyperbolic filters in Wolberg's "Digital Image Warping", 
        /// and is formally defined as the hyperbolic-filter sampling the ideal hyperbolic-filter
        /// interpolated image (the filter is designed to be idempotent for 1:1 pixel mapping).
        /// </summary>
        Hyper = 3
    }
}