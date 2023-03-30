﻿/// @file IVideoWindowControlExtension.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version>10.10.0</version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2022 Samsung Electronics Co., Ltd All Rights Reserved
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
    /// VideoWindowControl extension interface for player
    /// </summary>
    public interface IVideoWindowControlExtension
    {
        /// <summary>
        /// Set the display area for video and window
        /// </summary>
        /// <param name="videoAttribute">Video attribute including position and size</param>
        /// <param name="windowAttribute">Window attribute including position, size, degree, base screen resolution</param>
        /// <returns>Returns true in case of success</returns>
        bool SetDisplayArea(VideoAttribute videoAttribute, WindowAttribute windowAttribute);
    }
}