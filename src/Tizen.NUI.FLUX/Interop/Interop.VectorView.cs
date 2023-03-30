/// @file Interop.VectorView.cs

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
        internal static class VectorView
        {
            // ******  vector view  *******
            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_New", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr New();

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_delete_VectorView", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Delete(HandleRef vectorViewHandle);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_UpCast", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr UpCast(IntPtr cPtr);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_DownCast", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr DownCast(IntPtr cPtr);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_SetVectorRenderBackend", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetVectorRenderBackend(HandleRef vectorViewHandle, uint backend);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_SetViewBox", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetViewBox(HandleRef vectorViewHandle, float width, float height);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_Flush", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Flush(HandleRef vectorViewHandle);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_SetResourceUrl", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetResourceUrl(HandleRef vectorViewHandle, string url, int fType, float x1, float y1, float x2, float y2);

            // ******  shapes  *******
            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_AddRect", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void AddRect(HandleRef vectorViewHandle);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_AddRoundRect", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void AddRoundedRect(HandleRef vectorViewHandle, float[] radius);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_AddEllipse", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void AddEllipse(HandleRef vectorViewHandle);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_StartPath", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void StartPath(HandleRef vectorViewHandle);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_MoveTo", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void MoveTo(HandleRef vectorViewHandle, float x, float y);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_LineTo", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void LineTo(HandleRef vectorViewHandle, float x, float y);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_ArcTo", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void ArcTo(HandleRef vectorViewHandle, float xc, float yc, float rx, float ry, float sAngle, float eAngle, int orient);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_BezierTo", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void BezierTo(HandleRef vectorViewHandle, float cp1X, float cp1Y, float cp2X, float cp2Y, float x, float y);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_Close", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void Close(HandleRef vectorViewHandle);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_FinishPath", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void FinishPath(HandleRef vectorViewHandle);



            // ******  styles  *******
            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_AddPaintStyle", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetPaintStyle(HandleRef vectorViewHandle, uint paintStyle);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_SetPaintColor", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetPaintColor(HandleRef vectorViewHandle, HandleRef color);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_SetBackgroundColor", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetBackgroundColor(HandleRef vectorViewHandle, HandleRef color);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_SetDash", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetDash(HandleRef vectorViewHandle, float[] dashes, int numDashes, float offset);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_SetGradient", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetGradient(HandleRef vectorViewHandle, int style, float x1, float y1, float r1, float x2, float y2, float r2, int nStops, float[] positions, uint[] color);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_SetStrokeWidth", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetStrokeWidth(HandleRef vectorViewHandle, float strokeWidth);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_SetLineJoinStyle", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetLineJoinStyle(HandleRef vectorViewHandle, uint lineJoin);

            [DllImport(Libraries.DaliExtension_VectorView, EntryPoint = "CSharp_DaliExt_VectorView_SetLineCapStyle", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void SetLineCapStyle(HandleRef vectorViewHandle, uint lineCap);

        }
    }
}
