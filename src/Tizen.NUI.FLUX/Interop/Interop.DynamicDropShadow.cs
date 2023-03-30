/// @file Interop.DynamicDropShadow.cs

/// Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved 
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
        /// Interop Class for native DynamicDropShadow calls.
        /// </summary>
        internal static class DynamicDropShadow
        {
            /// <summary>
            /// Create a DynamicDropShadow Object
            /// </summary>
            /// <param name="multiShadowViewHandle">multiShadowViewHandle Handle</param>
            /// <returns>DynamicDropShadow Handle</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_New", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr New(HandleRef multiShadowViewHandle );

            /// <summary>
            /// Delete DynamicDropShadow handle
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_Delete", CallingConvention = CallingConvention.Cdecl)]
            public static extern void Delete(HandleRef nativeHandle);

            /// <summary>
            /// Activate Shadow
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_Activate", CallingConvention = CallingConvention.Cdecl)]
            public static extern void Activate(HandleRef nativeHandle);

            /// <summary>
            /// Deactivate Shadow
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_Deactivate", CallingConvention = CallingConvention.Cdecl)]
            public static extern void Deactivate(HandleRef nativeHandle);

            /// <summary>
            /// Set Shadow Area
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            /// <param name="shadowArea">Shadow Area value</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_SetShadowArea", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetShadowArea(HandleRef nativeHandle, HandleRef shadowArea);

            /// <summary>
            /// Get Shadow Area
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            /// <returns>Shadow Area</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_GetShadowArea", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr GetShadowArea(HandleRef nativeHandle);

            /// <summary>
            /// Set Shadow Color
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            /// <param name="shadowId">Shadow Id of shadow</param>
            /// <param name="shadowColor">Shadow Color value</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_SetShadowColor", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetShadowColor(HandleRef nativeHandle, uint shadowId, HandleRef shadowColor);

            /// <summary>
            /// Get Shadow Color
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            /// <param name="shadowId">Shadow Id of shadow</param>
            /// <returns>Color of shadow</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_GetShadowColor", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr GetShadowColor(HandleRef nativeHandle, uint shadowId);

            /// <summary>
            /// Set Shadow Offset
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            /// <param name="shadowId">Shadow Id of shadow</param>
            /// <param name="shadowOffset">Shadow Offset value</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_SetShadowOffset", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetShadowOffset(HandleRef nativeHandle, uint shadowId, HandleRef shadowOffset);

            /// <summary>
            /// Get Shadow Offset
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            /// <param name="shadowId">Shadow Id of shadow</param>
            /// <returns>Offset of shadow</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_GetShadowOffset", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr GetShadowOffset(HandleRef nativeHandle, uint shadowId);

            /// <summary>
            /// Set Shadow BlurSize
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            /// <param name="shadowId">Shadow Id of shadow</param>
            /// <param name="blurSize">Blur Size value</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_SetShadowBlurSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetShadowBlurSize(HandleRef nativeHandle, uint shadowId, uint blurSize);

            /// <summary>
            /// Get Shadow BlurSize
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            /// <param name="shadowId">Shadow Id of shadow</param>
            /// <returns>Blur size of shadow</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_GetShadowBlurSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern uint GetShadowBlurSize(HandleRef nativeHandle, uint shadowId);

            /// <summary>
            /// Set Shadow BlurSigma value
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            /// <param name="shadowId">Shadow Id of shadow</param>
            /// <param name="blurSigma"> Blur sigma value</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_SetShadowBlurSigma", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetShadowBlurSigma(HandleRef nativeHandle, uint shadowId, float blurSigma);

            /// <summary>
            /// Get Shadow BlurSigma
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            /// <param name="shadowId">Shadow Id of shadow</param>
            /// <returns>Blur Sigma value of Shadow</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_GetShadowBlurSigma", CallingConvention = CallingConvention.Cdecl)]
            public static extern float GetShadowBlurSigma(HandleRef nativeHandle, uint shadowId);

            /// <summary>
            /// Add Shadow
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            /// <returns>ShadowId</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_CreateShadow", CallingConvention = CallingConvention.Cdecl)]
            public static extern uint AddShadow(HandleRef nativeHandle);

            /// <summary>
            /// Remove Shadow
            /// </summary>
            /// <param name="nativeHandle">DynamicDropShadow Handle</param>
            /// <param name="shadowId">Shadow Id of shadow to be removed. If 0 remove all shadows</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_DynamicDropShadow_RemoveShadow", CallingConvention = CallingConvention.Cdecl)]
            public static extern void RemoveShadow(HandleRef nativeHandle, uint shadowId);
        }
    }
}
