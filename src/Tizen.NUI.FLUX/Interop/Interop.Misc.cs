/// @file Interop.Misc.cs
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
        internal static class Misc
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate bool ObjectDumpCallback(String data);

            [DllImport(Libraries.DaliExtension_Create, EntryPoint = "Csharp_DaliExt_RegisterChinaLanguageLocale", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool RegisterChinaLanguageLocale();

            [DllImport(Libraries.DaliExtension_Create, EntryPoint = "Csharp_DaliExt_RegisterNuiObjectdump", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool RegisterNuiObjectdump(ObjectDumpCallback Func);
        }
    }
}