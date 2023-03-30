/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file ColorPresetTable.cs
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
using System.Collections.Generic;
namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// ColorPresetSet class
    /// </summary>
    public class ColorPresetTable
    {
        /// <summary>
        /// ColorPresetSet class
        /// </summary>
        /// <param name="state">State Information</param>
        /// <returns>ColorPreset according to state</returns>
        public string this[string state]
        {
            get
            {
                if (ColorSet.TryGetValue(state, out string value))
                {
                    return value;

                }
                return null;
            }
            set => ColorSet[state] = value;
        }
        /// <summary>
        /// Clone the ColorPresetSet object, each derived class need to override this method. 
        /// </summary>
        /// <returns>ColorPreset according to state</returns>
        public ColorPresetTable Clone()
        {
            ColorPresetTable color = new ColorPresetTable();
            foreach (KeyValuePair<string, string> pair in ColorSet)
            {
                color[pair.Key] = pair.Value;
            }
            return color;
        }

        private Dictionary<string, string> ColorSet = new Dictionary<string, string>();
    }
}
