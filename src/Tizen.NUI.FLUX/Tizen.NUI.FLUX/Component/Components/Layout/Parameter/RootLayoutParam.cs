/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file RootLayoutParam.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.6.0 </version>
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
    /// 
    /// </summary>
    public class RootLayoutParam : FluxLayoutParam
    {
        /// <summary>
        /// RootLayoutParam constructor
        /// </summary>
        /// <version>10.10.0</version>
        public RootLayoutParam()
        {
        }

        /// <summary>
        /// RootLayoutParam Constructor with LayoutTypes type argument
        /// </summary>
        /// <param name="type">Layout Type</param>
        /// <version>10.10.0</version>
        public RootLayoutParam(LayoutTypes type) : base(type)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public Margin Margin
        {
            set => margin = value;
            get => margin;
        }

        private Margin margin = new Margin();
    }
}