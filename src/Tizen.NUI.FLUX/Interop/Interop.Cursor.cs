/// @file Interop.Cursor.cs
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
        internal static class Cursor
        {

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_CursorModuleInitialize", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool Initialize();

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_CursorModuleFinalize", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Deinitialize();

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_EnableMousePointer", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool EnableMousePointer(IntPtr ecoreWindow);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_DisableMousePointer", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool DisableMousePointer(IntPtr ecoreWindow);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_EnableTouchEvent", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool EnableTouchEvent(IntPtr ecoreWindow);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_DisableTouchEvent", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool DisableTouchEvent(IntPtr ecoreWindow);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_CursorEnableShowAlways", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool EnableShowAlways(IntPtr ecoreWindow);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_CursorDisableShowAlways", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool DisableShowAlways(IntPtr ecoreWindow);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_CursorSetType", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetType(string cursorName);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_CursorSetPosition", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool SetPosition(IntPtr ecoreWindow, int x, int y);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_CursorGetPosition", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void GetPosition(out int x, out int y);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_CursorSetConfig", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool SetConfig(IntPtr ecoreWindow, uint configType, IntPtr data);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_CursorUnSetConfig", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool UnSetConfig(IntPtr ecoreWindow, uint configType);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_CursorSetTheme", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool SetTheme(string cursorThemeName);
        }
    }
}


