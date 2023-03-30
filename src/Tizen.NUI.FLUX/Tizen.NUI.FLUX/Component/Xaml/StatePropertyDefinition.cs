/**
 *Copyright (c) 2022 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file StatePropertyDefinition.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 10.10.0 </version>
/// <SDK_Support> Y </SDK_Support>
///
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


namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// Defines state-specific property that apply to Component.
    /// This is only intended for use by the XAML Application.
    /// </summary>
    /// <code>
    /// Refer to XAML samples
    /// </code>
    public class StatePropertyDefinition
    {
        /// <summary>
        /// name of state
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// name of property
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// value of the property
        /// </summary>
        public object PropertyValue { get; set; }
    }
}
