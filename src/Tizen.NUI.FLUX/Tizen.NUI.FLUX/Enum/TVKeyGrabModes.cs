/// @file TVKeyGrabModes.cs
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
    /// Enumeration for TVKeyGrabModes of the window 
    /// </summary>
    public enum TVKeyGrabModes
    {
        /// <summary> Default </summary>
        None = 0,
        /// <summary> key is shared with other application. key can be sent to app running as background or service daemon. </summary>
        Shared,
        /// <summary> get key exclusively in case of the top most window. </summary>
        Topmost,
        /// <summary> get key exclusively whenever. this mode can be overwritten </summary>
        OverrideExclusive,
        /// <summary> get key exclusively whenever. this mode can not be overwritten </summary>
        Exclusive,
        /// <summary> get key if window is visible. </summary>
        Registered,
    }
}