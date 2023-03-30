/// @file Interop.TVAsyncImageLoadHelper.cs
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
        /// <summary>
        /// Interop for native AsyncImageLoader.
        /// </summary>
        internal static class TVAsyncImageLoadHelper
        {
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_Dali_Ext_AsyncImageLoadHelper_New", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr New_();
            internal static IntPtr New() => IntPtr.Zero;

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_Dali_Ext_AsyncImageLoadHelper_delete", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Delete_(HandleRef asyncImageLoaderHandle);
            internal static void Delete(HandleRef asyncImageLoaderHandle) { }

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AsyncImageLoadHelper_Load", CallingConvention = CallingConvention.Cdecl)]
            internal static extern uint LoadImage_(HandleRef asyncImageLoaderHandle, string url, HandleRef imageSize, int fittingMode, int samplingMode, bool orientationCorrection);
            internal static uint LoadImage(HandleRef asyncImageLoaderHandle, string url, HandleRef imageSize, int fittingMode, int samplingMode, bool orientationCorrection) => 0;

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AsyncImageLoadHelper_Signal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr LoadCompleteSignal_(HandleRef asyncImageLoaderHandle);
            internal static IntPtr LoadCompleteSignal(HandleRef asyncImageLoaderHandle) => IntPtr.Zero;

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AsyncImageLoadHelper_Signal_Empty", CallingConvention = CallingConvention.Cdecl)]
            [return:MarshalAs(UnmanagedType.U1)]
            internal static extern bool SignalEmpty_(HandleRef asyncImageLoaderHandle);
            internal static bool SignalEmpty(HandleRef asyncImageLoaderHandle) => false;

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AsyncImageLoadHelper_CancelTask", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.U1)]
            internal static extern bool Cancel_(HandleRef asyncImageLoaderHandle, uint loadID);
            internal static bool Cancel(HandleRef asyncImageLoaderHandle, uint loadID) => false;

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AsyncImageLoadHelper_CancelTask", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void CancelAll_(HandleRef asyncImageLoaderHandle);
            internal static void CancelAll(HandleRef asyncImageLoaderHandle) { }

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AsyncImageLoadHelper_Connect", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Connect_(HandleRef signal, IntPtr funcPtr);
            internal static void Connect(HandleRef signal, IntPtr funcPtr) { }

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AsyncImageLoadHelper_Disconnect", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Disconnect_(HandleRef signal, IntPtr funcPtr);
            internal static void Disconnect(HandleRef signal, IntPtr funcPtr) { }
        }
    }
}
