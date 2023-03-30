/// @file Interop.ImageColorPick.cs
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


using System.Runtime.InteropServices;
using System;

namespace Tizen.NUI.FLUX
{
    internal static partial class Interop
    {

        /// <summary>
        /// Interop Class for wraping native ImageColorPick APIs.
        /// </summary>
        internal class ImageColorPick
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate void NativeColorPickValueAsyncCallback(IntPtr imagePath , IntPtr colorValue);

            /// <summary>
            /// AvergaeColor value of a provided image in sync mode.
            /// </summary>
            /// <param name="imagePath"></param>
            /// <returns></returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_GetColorpickValue", CallingConvention = CallingConvention.Cdecl)]
            public extern static IntPtr GetColorPickValueSync(string imagePath);

            /// <summary>
            /// AvergaeColor value of a provided image in async mode.
            /// </summary>
            /// <param name="imagePath"></param>
            /// <param name="colorPickValueCallback"></param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_GetColorpickValueAsync", CallingConvention = CallingConvention.Cdecl)]
            public extern static void GetColorPickValueAsync(string imagePath, NativeColorPickValueAsyncCallback colorPickValueCallback);

            /// <summary>
            /// AvergaeColor value of a provided image in sync mode.
            /// </summary>
            /// <param name="imagePath"></param>
            /// <param name="rectHandle">handle to rectangle</param>
            /// <remarks>if rect is empty than colorpick value of entire image is calculated else given rect's colorpick value is calculated </remarks>
            /// <returns></returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_GetColorpickValueRectSync", CallingConvention = CallingConvention.Cdecl)]
            public extern static IntPtr GetColorPickValueSync(string imagePath, IntPtr rectHandle);

            /// <summary>
            /// AvergaeColor value of a provided image in async mode.
            /// </summary>
            /// <param name="imagePath"></param>
            /// <param name="rectHandle">handle to rectangle</param>
            /// <remarks>if rect is empty than colorpick value of entire image is calculated else given rect's colorpick value is calculated </remarks>
            /// <param name="colorPickValueCallback"></param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_GetColorpickValueRectAsync", CallingConvention = CallingConvention.Cdecl)]
            public extern static void GetColorPickValueAsync(string imagePath, NativeColorPickValueAsyncCallback colorPickValueCallback, IntPtr rectHandle);
        }
    }
}


