/// @file Interop.KeyCombination.cs
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
        internal static class KeyCombination
        {
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyCombinationInitialize", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool Initialize();

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyCombinationFinalize", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Deinitialize();

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyCombinationAddHandler", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool AddHandler(KeyCombinations combination, KeyCombinationDelegate combinationAddCallback);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyCombinationRemoveHandler", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool RemoveHandler(KeyCombinations combination, KeyCombinationDelegate combinationRemoveCallback);

        }
    }

}

