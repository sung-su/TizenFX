/// @file Interop.FrameCallback.cs
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
        /// Interop Class for wraping native FrameCallback calls.
        /// </summary>
        internal static class FrameCallback
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate void NativeFrameCallback(float elapsedSeconds);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_FrameCallback_New", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr New();

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_FrameCallback_delete", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Delete(IntPtr nativeHandle);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_FrameCallback_AddCallback", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void AddCallback(IntPtr nativeHandle, NativeFrameCallback frameCallback);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_FrameCallback_RemoveCallback", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void RemoveCallback(IntPtr nativeHandle);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_FrameCallback_GetPosition", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool GetPosition(IntPtr nativeHandle, uint id, HandleRef jarg1);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_FrameCallback_BakePosition", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool BakePosition(IntPtr nativeHandle, uint id, HandleRef jarg1);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_FrameCallback_GetScale", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool GetScale(IntPtr nativeHandle, uint id, HandleRef jarg1);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_FrameCallback_BakeScale", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool BakeScale(IntPtr nativeHandle, uint id, HandleRef jarg1);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_FrameCallback_GetColor", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool GetColor(IntPtr nativeHandle, uint id, HandleRef jarg1);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_FrameCallback_BakeColor", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool BakeColor(IntPtr nativeHandle, uint id, HandleRef jarg1);
        }
    }
}