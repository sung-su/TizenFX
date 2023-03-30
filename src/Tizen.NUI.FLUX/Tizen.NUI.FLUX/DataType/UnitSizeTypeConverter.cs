/// @file UnitSizeTypeConverter.cs
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
using System.Globalization;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// class for converting unit size in XMAL
    /// </summary>
    /// <code>
    /// UnitSizeTypeConverter converter = new UnitSizeTypeConverter();    
    /// string value = "100,100";
    /// converter.ConvertFromInvariantString(value);
    /// UnitSize size = (UnitSize)converter.ConvertFromInvariantString(value);
    /// </code>
    public class UnitSizeTypeConverter : Binding.TypeConverter
    {
        /// <summary>
        /// class for converting unit size in XMAL
        /// </summary>
        /// <param name="value">UnitSize</param>
        /// <returns> UnitSize if value passed is valid parameter.</returns>
        /// <exception cref="InvalidOperationException">Thrown when string value is null.</exception>
        public override object ConvertFromInvariantString(string value)
        {
            if (value != null)
            {
                string[] parts = value.Split(',');
                if (parts.Length == 2)
                {
                    return new UnitSize(Int32.Parse(parts[0].Trim(), CultureInfo.InvariantCulture),
                                    Int32.Parse(parts[1].Trim(), CultureInfo.InvariantCulture));
                }
            }

            throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(UnitSize)}");
        }
    }
}