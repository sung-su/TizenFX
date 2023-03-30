/// @file Interop.BlurUtility.cs
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
using System.Runtime.InteropServices;

namespace Tizen.NUI.FLUX
{

    internal static partial class Interop
    {
        internal static class BlurUtility
        {
            /// <summary>
            /// Used to create BlurredImage of an existing Image and returns PropertyValue that contains BlurImage Url that can be set on ImageView 
            /// </summary>
            /// <param name="jarg1">Original Image url</param>
            /// <param name="jarg2">The constant controlling the Gaussian function, must be greater than 0.0f</param>
            /// <param name="jarg3">The size of the Gaussian blur kernel (number of samples in horizontal / vertical blur directions) and its value should be greater than 1.</param>
            /// <param name="jarg4">The scale factor applied during the blur process, scaling the size of the source image to the size of the final blurred image output.</param>
            /// <returns>PropertyValue containing BlurImage url string</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_Dali_GetBlurImageUrl_PropertyValue", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr GetBlurImageUrl_PropertyValue(string jarg1, float jarg2, uint jarg3, float jarg4);

        }


    }
}
