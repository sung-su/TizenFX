/// @file Interop.BlurCapture.cs
/// 
/// Copyright (c) 2023 Samsung Electronics Co., Ltd All Rights Reserved
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
        /// <summary>
        /// Interop Class for wrapping native BlurCapture calls.
        /// </summary>
        internal static class BlurCapture
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate void InitCallback();

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BlurCaptureInitialize", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr BlurCaptureInitialize(InitCallback initCallback);
        }
    }
}
