/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file PropertyUtility.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.0.0 </version>
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
    ///  Utility class for State Property
    /// </summary>
    public static class PropertyUtility
    {
        /// <summary> Provide for user convenient </summary>
        public static readonly string Scale = "Scale";
        /// <summary> Provide for user convenient </summary>
        public static readonly string ScaleX = "ScaleX";
        /// <summary> Provide for user convenient </summary>
        public static readonly string ScaleY = "ScaleY";
        /// <summary> Provide for user convenient </summary>
        public static readonly string Position = "Position";
        /// <summary> Provide for user convenient </summary>
        public static readonly string PositionX = "PositionX";
        /// <summary> Provide for user convenient </summary>
        public static readonly string PositionY = "PositionY";
        /// <summary> Provide for user convenient </summary>
        public static readonly string PositionZ = "PositionZ";
        /// <summary> Provide for user convenient </summary>
        public static readonly string Size2D = "Size2D";
        /// <summary> Provide for user convenient </summary>
        public static readonly string SizeWidth = "SizeWidth";
        /// <summary> Provide for user convenient </summary>
        public static readonly string SizeHieght = "SizeHieght";
        /// <summary> Provide for user convenient </summary>
        public static readonly string Opacity = "Opacity";
        /// <summary> Provide for user convenient </summary>
        public static readonly string TextColor = "TextColor";
        
        internal static bool IsAnimatableProperty(string property)
        {
            if (property == "Scale" || property == "ScaleX" || property == "ScaleY" || property == "ScaleZ" || property == "Position" || property == "PositionX" || property == "PositionY" || property == "PositionZ" ||
                property == "Size2D" || property == "SizeWidth" || property == "SizeHieght" || property == "Opacity" || property == "TextColor")
            {
                return true;
            }

            return false;
        }
    }
}
