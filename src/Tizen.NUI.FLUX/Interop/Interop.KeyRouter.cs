/// @file Interop.KeyRouter.cs
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
        internal static class KeyRouter
        {
            // InterOp
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyRouterInitialize", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool Initialize();

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyRouterFinalize", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Deinitialize();

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyRouterResetKeyList", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void ResetKeyList();

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyRouterAddKey", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool AddKey(string keyname, int mode);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyRouterAddAllKey", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool AddAllKey(int keyMode);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyRouterRemoveKey", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool RemoveKey(string keyName, int keyMode);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyRouterSetKeyList", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool SetKeyList(IntPtr ecoreWindow);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyRouterUnSetKeyList", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool UnSetKeyList(IntPtr ecoreWindow);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyRouterRegisterIgnoreAllKey", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool RegisterIgnoreAllKey(IntPtr ecoreWindow);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyRouterUnRegisterIgnoreAllKey", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool UnRegisterIgnoreAllKey(IntPtr ecoreWindow);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyRouterSetInputConfig", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool SetInputConfig(IntPtr ecoreWindow, int configMode, IntPtr data);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_KeyRouterUnSetInputConfig", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool UnSetInputConfig(IntPtr ecoreWindow, int configMode);
        }
    }
}
