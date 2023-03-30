/// @file Interop.GLSurfaceView.cs
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
        /// Interop Class for wraping native GLApplication calls.
        /// </summary>
        internal static partial class GLSurfaceView
        {
            /// <summary>
            /// Delegate that wrap Native GLInit callback.
            /// </summary>
            /// <param name="viewWidth">uint</param>
            /// <param name="viewHeight">uint</param>
            /// <returns>Return true to call GLDraw for rendering</returns>
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate bool GLInitCallback(uint viewWidth, uint viewHeight);

            /// <summary>
            /// Delegate that wrap Native GLDraw callback.
            /// </summary>
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate void GLDrawCallback();

            /// <summary>
            /// Delegate that wrap Native GLTerminateCallback callback.
            /// </summary>
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate void GLTerminateCallback();

            /// <summary>
            /// Create New Native GLSurfaceView.
            /// </summary>
            /// <returns>Native GLSurfaceView Object</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_GLSurfaceView_New")]
            internal static extern IntPtr New(uint viewWidth, uint viewHeight);

            /// <summary>
            /// Creates the EGL Context and Initialize the wayland-window System with the given external GLWindow.
            /// <param name="glSurfaceHandle">Native GLSurfaceView Object</param>
            /// <param name="initCallback">Function Pointer to be call after successful EGL Initilization</param>          
            /// <param name="drawCallback">Function Pointer to be call per frame</param>          
            /// <param name="terminateCallback">Function Pointer to be call for egl resource cleanup</param>          
            /// </summary>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_GLSurfaceView_RegisterGLCallback")]
            internal static extern void RegisterGLCallback(HandleRef glSurfaceHandle, GLInitCallback initCallback, GLDrawCallback drawCallback, GLTerminateCallback terminateCallback);

            /// <summary>
            /// Set RenderingMode.
            /// </summary>
            /// <returns>Native GLSurfaceView Object</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_GLSurfaceView_SetRenderingMode")]
            internal static extern void SetRenderingMode(HandleRef glSurfaceHandle, FLUX.GLSurfaceView.RenderMode mode);

            /// <summary>
            /// Set GLContentResizingMode. 
            /// </summary>
            /// <returns>Native GLSurfaceView Object</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_GLSurfaceView_SetGLContentResizingMode")]
            internal static extern void SetGLContentResizingMode(HandleRef glSurfaceHandle, FLUX.GLSurfaceView.GLContentResizeMode mode);

            /// <summary>
            /// Request Render
            /// </summary>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_GLSurfaceView_RequestRender")]
            internal static extern void RequestRender(HandleRef glSurfaceHandle);

            /// <summary>
            /// Reset surface size
            /// </summary>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_GLSurfaceView_ResetSurfaceSize")]
            internal static extern void ResetSurfaceSize(HandleRef glSurfaceHandle, int width, int height);

            /// <summary>
            /// SWIGUpcast
            /// </summary>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_GLSurfaceView_SWIGUpcast")]
            internal static extern IntPtr Upcast(IntPtr glSurfaceHandle);

            /// <summary>
            /// Delete native glSurface handle
            /// </summary>
            /// <param name="nativeHandle"></param>
            /// <returns></returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_delete_GLSurfaceView")]
            internal static extern void Delete(HandleRef nativeHandle);

        }
    }
}
