/// @file Interop.AifwPictureEnhancer.cs

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

/// <summary>
/// namespace for Tizen.TV.NUI package
/// </summary>
namespace Tizen.NUI.FLUX
{
    internal static partial class Interop
    {
        /// <summary>
        /// Interop Class for native AifwPictureEnhancer calls.
        /// </summary>
        internal static class AifwPictureEnhancer
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate void TextureUrlCallback(IntPtr textureUrl);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AifwPictureEnhancer_New", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr New(AIEnhancedImageView.PictureEnhancerType type);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AifwPictureEnhancer_delete", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Delete(HandleRef nativeHandle);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AifwPictureEnhancer_RunFileAsync", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void RunFileAsync(HandleRef nativeHandle, string imageUrl, TextureUrlCallback textureUrlCallback);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AifwPictureEnhancer_Upcast", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr UpCast(IntPtr nativeHandle);
        }
    }
}
