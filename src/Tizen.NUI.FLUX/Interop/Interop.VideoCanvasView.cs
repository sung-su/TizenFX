/// @file Interop.VideoCanvasView.cs
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
        /// Interop Class for wraping native ColorBlendEffect calls.
        /// </summary>
        internal static class VideoCanvasView
        {
            [DllImport(Libraries.DaliExtension_VideoCanvas, EntryPoint = "CSharp_DaliExt_VideoCanvasView_New", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr VideoCanvasViewNew();

            [DllImport(Libraries.DaliExtension_VideoCanvas, EntryPoint = "CSharp_DaliExt_delete_VideoCanvasView", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VideoCanvasViewDelete(global::System.Runtime.InteropServices.HandleRef jarg);

            [DllImport(Libraries.DaliExtension_VideoCanvas, EntryPoint = "CSharp_Dali_new_VideoCanvasViewSignalType", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr VideoCanvasViewSignalTypeNew();

            [DllImport(Libraries.DaliExtension_VideoCanvas, EntryPoint = "CSharp_DaliExt_VideoCanvasView_UpdateDisplayAreaSignal_Connect", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VideoCanvasViewUpdateDisplayAreaSignalConnect(global::System.Runtime.InteropServices.HandleRef jarg1, global::System.Runtime.InteropServices.HandleRef jarg2);

            [DllImport(Libraries.DaliExtension_VideoCanvas, EntryPoint = "CSharp_DaliExt_delete_VideoCanvasViewSignalType", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VideoCanvasViewSignalTypeDelete(global::System.Runtime.InteropServices.HandleRef jarg);

            [DllImport(Libraries.DaliExtension_VideoCanvas, EntryPoint = "CSharp_DaliExt_VideoCanvasView_UpdateDisplayAreaSignal__Disconnect", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VideoCanvasViewUpdateDisplayAreaSignalDisconnect(global::System.Runtime.InteropServices.HandleRef jarg1, global::System.Runtime.InteropServices.HandleRef jarg2);

            [DllImport(Libraries.DaliExtension_VideoCanvas, EntryPoint = "CSharp_DaliExt_VideoCanvasView_SWIGUpcast", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr VideoCanvasViewUpcast(IntPtr jarg);

            [DllImport(Libraries.DaliExtension_VideoCanvas, EntryPoint = "CSharp_DaliExt_VideoCanvasView_UpdateDisplayAreaSignal", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr VideoCanvasViewUpdateDisplayAreaSignal(global::System.Runtime.InteropServices.HandleRef jarg1);

            [DllImport(Libraries.DaliExtension_VideoCanvas, EntryPoint = "CSharp_DaliExt_VideoCanvasView_SetCornerRadius", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetCornerRadius(global::System.Runtime.InteropServices.HandleRef jarg1, HandleRef jarg2);

            [DllImport(Libraries.DaliExtension_VideoCanvas, EntryPoint = "CSharp_DaliExt_VideoCanvasView_GetCornerRadius", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr GetCornerRadius(global::System.Runtime.InteropServices.HandleRef jarg1);
        }
    }
}
