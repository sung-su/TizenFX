/// @file Interop.ImageUtility.cs
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
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    internal static partial class Interop
    {
        /// <summary>
        /// Interop Class for native ImageUtility calls.
        /// </summary>
        internal static partial class ImageUtility
        {
            /// <summary>
            /// Create a ImageUtility Object
            /// </summary>
            /// <returns>ImageUtility Handle</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_New", CallingConvention = CallingConvention.Cdecl)]
            internal extern static IntPtr New();

            /// <summary>
            /// Create an Image Utility Object and Load/Decode provided image
            /// </summary>
            /// <param name="imagePath"></param>
            /// <returns>ImageUtility Handle</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_New_WithImageUrl", CallingConvention = CallingConvention.Cdecl)]
            internal extern static IntPtr NewWithUrl(string imagePath);

            /// <summary>
            /// Create an Image Utility Object and set image's raw buffer.
            /// </summary>
            /// <param name="buffer"></param>
            /// <param name="bufferSize"></param>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <param name="pixelFormat"></param>
            /// <param name="releaseFunction"></param>
            /// <param name="clone"></param>
            /// <returns></returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_New_WithImageBuffer", CallingConvention = CallingConvention.Cdecl)]
            internal extern static IntPtr NewWithImageBuffer(IntPtr buffer, uint bufferSize, uint width, uint height, PixelFormat pixelFormat, ReleaseFunction releaseFunction, bool clone);

            /// <summary>
            /// Delete Image Utility handle
            /// </summary>
            /// <param name="handle"></param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_Dali_ImageUtility_delete", CallingConvention = CallingConvention.Cdecl)]
            internal extern static void Delete(IntPtr handle);

            /// <summary>
            /// Remove Image Utility url
            /// </summary>
            /// <param name="imagePath">ImageUtility Path</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_ImageUtility_RemoveTexture", CallingConvention = CallingConvention.Cdecl)]
            internal extern static void RemoveTexture(string imagePath);

            /// <summary>
            /// Get bytes per pixel format.
            /// </summary>
            /// <param name="pixelFormat">ImageUtility Path</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_ImageUtility_GetBytesPerPixelFormat", CallingConvention = CallingConvention.Cdecl)]
            internal extern static uint GetBytesPerPixelFormat(PixelFormat pixelFormat);

            /// <summary>
            /// Set Image URL, provided image will loaded
            /// </summary>
            /// <param name="handle"></param>
            /// <param name="imagePath"></param>
            /// <returns></returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_SetImageUrl", CallingConvention = CallingConvention.Cdecl)]
            internal extern static bool SetImageUrl(SafeHandle handle, string imagePath);

            /// <summary>
            /// Get Image URL
            /// </summary>
            /// <param name="handle"></param>
            /// <returns></returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_GetImageUrlPropertyValue", CallingConvention = CallingConvention.Cdecl)]
            internal extern static IntPtr GetImageUrl(SafeHandle handle);

            /// <summary>
            /// Get Image Size
            /// </summary>
            /// <param name="handle"></param>
            /// <returns> Size in Vector2</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_GetSize", CallingConvention = CallingConvention.Cdecl)]
            internal extern static IntPtr GetSize(SafeHandle handle);

            /// <summary>
            /// Get Pixel Format
            /// </summary>
            /// <param name="handle"></param>
            /// <returns> PixelFormat</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_GetPixelFormat", CallingConvention = CallingConvention.Cdecl)]
            internal extern static PixelFormat GetPixelFormat(SafeHandle handle);

            /// <summary>
            /// Get TextureURL
            /// </summary>
            /// <param name="handle"></param>
            /// <returns> Texture Url</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_GetTextureUrlPropertyValue", CallingConvention = CallingConvention.Cdecl)]
            internal extern static IntPtr GetTextureUrl(SafeHandle handle);

            /// <summary>
            /// Get BytePerPixel
            /// </summary>
            /// <param name="handle"></param>
            /// <returns> bytes per pixel</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_GetBytePerPixel", CallingConvention = CallingConvention.Cdecl)]
            internal extern static uint GetBytePerPixel(SafeHandle handle);

            /// <summary>
            /// Get Buffer
            /// </summary>
            /// <param name="handle"></param>
            /// <returns> raw buffer</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_GetBuffer", CallingConvention = CallingConvention.Cdecl)]
            internal extern static IntPtr GetBuffer(SafeHandle handle);

            /// <summary>
            /// Encode to File
            /// </summary>
            /// <param name="handle"></param>
            /// <param name="imagePath">filename Identify the filesytem location at which to write the encoded image</param>
            /// <returns>true on success</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_EncodeToFile", CallingConvention = CallingConvention.Cdecl)]
            internal extern static bool EncodeToFile(SafeHandle handle, string imagePath);

            /// <summary>
            /// Flip
            /// </summary>
            /// <param name="handle"></param>
            /// <param name="flipType"> supported flip type are VERTICAL, HORIZONTAL</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_Flip", CallingConvention = CallingConvention.Cdecl)]
            internal extern static void Flip(SafeHandle handle, FlipType flipType);

            /// <summary>
            /// Rotate
            /// </summary>
            /// <param name="handle"></param>
            /// <param name="rotationType">Supported rotation are 0, 90, 180 and 270 in Counterclockwise direction</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_Rotate", CallingConvention = CallingConvention.Cdecl)]
            internal extern static void Rotate(SafeHandle handle, RotationType rotationType);

            /// <summary>
            /// Rotate
            /// </summary>
            /// <param name="handle"></param>
            /// <param name="scaledWidth"> width to scale</param>
            /// <param name="scaledHeight"> height to scale</param>
            /// <param name="interpolation">Interpolation Type for scale operation</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_Scale", CallingConvention = CallingConvention.Cdecl)]
            internal extern static void Scale(SafeHandle handle, uint scaledWidth, uint scaledHeight, Interpolation interpolation);

            /// <summary>
            /// FastBoxBlur
            /// </summary>
            /// <param name="handle"></param>
            /// <param name="kernelSize">The size of the blur kernel (number of samples).</param>
            /// <param name="downSampleScale">The scale factor applied during the blur process</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_BoxBlur", CallingConvention = CallingConvention.Cdecl)]
            internal extern static void BoxBlur(SafeHandle handle, uint kernelSize, float downSampleScale);

            /// <summary>
            /// ImageFilter
            /// </summary>
            /// <param name="handle"></param>
            /// <param name="filterType"></param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_ImageUtility_ApplyFilterEffect", CallingConvention = CallingConvention.Cdecl)]
            internal extern static void ApplyFilterEffect(SafeHandle handle, int filterType);

            /// <summary>
            /// Creates a handle of FilterFinishedSignal
            /// </summary>
            /// <param name="handle">a handle of ImageBuffer to own the signal</param>
            /// <returns>a handle of FilterFinishedSignal</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_ImageUtility_FilterFinishedSignal", CallingConvention = CallingConvention.Cdecl)]
            internal extern static IntPtr FilterFinishedSignal(SafeHandle handle);

            /// <summary>
            /// Connects method to FilterFinishedSignal
            /// </summary>
            /// <param name="jarg1"></param>
            /// <param name="jarg2"></param>

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_ImageUtility_FilterFinishedSignal_Connect", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void FilterFinishSignalSignalConnect(HandleRef jarg1, HandleRef jarg2);

            /// <summary>
            /// Disconnects method from FilterFinishedSignal
            /// </summary>
            /// <param name="jarg1"></param>
            /// <param name="jarg2"></param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_ImageUtility_FilterFinishedSignal__Disconnect", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void FilterFinishSignalDisconnect(HandleRef jarg1, HandleRef jarg2);

            /// <summary>
            /// Deletes FilterFinishedSignal(unmanaged resource)
            /// </summary>
            /// <param name="handle">handle of signal</param>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_delete_ImageUtility_FilterFinishedSignal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void FilterFinishSignalDelete(HandleRef handle);
        }
    }
}