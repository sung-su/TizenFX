/// @file Interop.BlurView.cs
/// 
/// Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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
        /// Interop Class for wraping native BgBlurView calls.
        /// </summary>
        internal static class BlurView
        {
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_New", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr New(uint type, uint style);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_delete_BgBlurView", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Delete(HandleRef nativeHandle);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_SWIGUpcast", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr Upcast(IntPtr nativeHandle);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_Activate", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Activate(HandleRef nativeHandle);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_Activate_2", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Activate(HandleRef nativeHandle, bool enableCaptureStore);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_Deactivate", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Deactivate(HandleRef nativeHandle);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_SetBlurStyle")]
            internal static extern void SetBlurStyle(HandleRef nativeHandle, uint style);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_SetBlurViewType")]
            internal static extern void SetBlurType(HandleRef nativeHandle, uint type);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_SetAlternateResource", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetAlternativeResourceUrl(HandleRef nativeHandle, string path);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_Resume", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Resume(HandleRef nativeHandle);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_Pause", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Pause(HandleRef nativeHandle);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_ShowStoredCapture", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void ShowStoredCapture(HandleRef nativeHandle);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_SetGradientAlphaUrl", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetGradientAlphaUrl(HandleRef nativeHandle, string path);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_SetBlurIntensity", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetBlurIntensity(HandleRef nativeHandle, float blurIntensity);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_SetGraphicCaptureInterval", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetGraphicCaptureInterval(HandleRef nativeHandle, uint interval);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_SetDimmingLevel", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetDimmingLevel(HandleRef nativeHandle, float dimmingLevel);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_SetAlternativeDimColor", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetAlternativeDimColor(HandleRef nativeHandle, HandleRef color);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_BgBlurView_GeometryUpdated", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void GeometryUpdated(HandleRef nativeHandle, float x, float y, float width, float height);
        }
    }
}
