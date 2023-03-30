/// @file TVKeyExtension.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 10.10.0 </version>
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

using System.ComponentModel;
using Tizen.NUI;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// class provides extension methods for NUI Key
    /// </summary>
    /// <code>
    /// key.IsWheelEvent();
    /// </code>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class TVKeyExtension
    {
        /// <summary>
        /// Checks Whether is Wheel Event
        /// </summary>
        /// <param name="key">Key value</param>
        /// <returns>true if is Wheel Event, otherwise false</returns>
        /// <code>
        /// key.IsWheelEvent();
        /// </code>
        public static bool IsWheelEvent(this Key key)
        {
            // This name depends on E20.
            return (key?.DeviceName == "detent_key_device");
        }

        /// <summary>
        /// Converts Key Pressed Name
        /// </summary>
        /// <param name="key">Key value</param>
        /// <returns>Converted KeyPressedName</returns>
        /// <code>
        /// string keyPressedName = key.KeyPressedNameInVerticalDirection();
        /// </code>
        public static string KeyPressedNameInVerticalDirection(this Key key)
        {
            if (IsWheelEvent(key) == true)
            {
                if (key.KeyPressedName == "Right")
                {
                    return "Down";
                }
                else if (key.KeyPressedName == "Left")
                {
                    return "Up";
                }
            }

            return key?.KeyPressedName;
        }
    }
}