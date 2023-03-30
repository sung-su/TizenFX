/// @file Interop.WindowUtil.cs
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
using Tizen.NUI.FLUX.Component;
using Tizen.System;

namespace Tizen.NUI.FLUX
{
    internal static partial class Interop
    {
        internal static class WindowUtil
        {
            private static int screenSizeWidth = 0;
            private static int screenSizeHeight = 0;
            private static int? getScreenSizeMethodReturnedValue = null;

            internal enum ErrorType
            {
                Success = 0,
                InvalidWindow = 1,
                ConnectWaylandDisplay = 2,
                WaylandDisplay = 3,
                WaylandSurface = 4,
                WaylandRegistry = 5,
                WaylandInput = 6,
                WaylandSeat = 7
            };

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            internal delegate void NativeCaptureWindowSurfaceCallback(IntPtr userData, int result);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_GetWlWindowSurfaceID", CallingConvention = CallingConvention.Cdecl)]
            internal static extern int GetResourceId(IntPtr ecoreWindow);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_InitializeWindowUtility", CallingConvention = CallingConvention.Cdecl)]
            internal static extern int InitializeWindowUtility();

            internal static int GetScreenSize(out int width, out int height)
            {
                if (getScreenSizeMethodReturnedValue == null || screenSizeWidth == 0 || screenSizeHeight == 0)
                {
                    Tracer.TraceValue(0, "[FLUX] Interop_GetScreenSize S");
                    //getScreenSizeMethodReturnedValue = GetScreenSizePrivate(out screenSizeWidth, out screenSizeHeight);
                    if (Information.TryGetValue("http://tizen.org/feature/screen.width", out screenSizeWidth) && Information.TryGetValue("http://tizen.org/feature/screen.height", out screenSizeHeight))
                    {
                        FluxLogger.InfoP("screenSizeWidth: [%d1], screenSizeHeight: [%d2]", d1: screenSizeWidth, d2: screenSizeHeight);
                        getScreenSizeMethodReturnedValue = 0;
                    }
                    else
                    {
                        FluxLogger.ErrorP("screenSizeWidth: [%d1], screenSizeHeight: [%d2], getScreenSizeMethodReturnedValue: [d3]", d1: screenSizeWidth, d2: screenSizeHeight, d3: getScreenSizeMethodReturnedValue.Value);
                    }
                    Tracer.TraceValue(0, "[FLUX] Interop_GetScreenSize E");
                }

                width = screenSizeWidth;
                height = screenSizeHeight;

                return getScreenSizeMethodReturnedValue.Value;
            }

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_CaptureWindowSurfaceAsFile", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void CaptureWindowSurfaceAsFile(IntPtr ecoreWindow, NativeCaptureWindowSurfaceCallback captureCallback, string path, string filename);

            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_GetScreenSize", CallingConvention = CallingConvention.Cdecl)]
            private static extern int GetScreenSizePrivate(out int width, out int height);
        }
    }
}