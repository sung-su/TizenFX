/// @file PaddingTypeConverter.cs
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


using System;
using System.Globalization;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// class for converting padding in XMAL
    /// </summary>
    /// <version> 10.10.0 </version>
    /// <code>
    /// PaddingTypeConverter converter = new PaddingTypeConverter();
    /// string value = "2,3,1,1";
    /// Padding padding = (Padding)converter.ConvertFromInvariantString(value);
    /// </code>
    public class PaddingTypeConverter : Tizen.NUI.Binding.TypeConverter
    {
        /// <summary>
        /// class for converting padding in XAML
        /// </summary>
        /// <param name="value">Padding</param>
        /// <returns>Padding if value passed is valid parameter.</returns>
        /// <exception cref="InvalidOperationException">Thrown when string value is null.</exception>
        /// <version> 10.10.0 </version>


        public override object ConvertFromInvariantString(string value)
        {
            if (value != null)
            {
                string[] parts = value.Split(',');
                if (parts.Length == LengthOfArguments)
                {
                    return new Padding
                        (int.Parse(parts[0].Trim(), CultureInfo.InvariantCulture),
                        int.Parse(parts[1].Trim(), CultureInfo.InvariantCulture),
                        int.Parse(parts[2].Trim(), CultureInfo.InvariantCulture),
                        int.Parse(parts[3].Trim(), CultureInfo.InvariantCulture)
                        );
                }
                else
                {
                    FluxLogger.InfoP("The given string length(%d1) does not match for the required spec:%d2", d1: parts.Length, d2: LengthOfArguments);
                    return null;
                }
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(Padding)}");
        }

        private const int LengthOfArguments = 4;
    }
}
