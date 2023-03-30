/// @file Interop.AnimationEX.cs
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
        internal static class AnimationEX
        {
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_New")]
            internal static extern IntPtr New(float duration, float delay);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_delete_AnimationEX")]
            internal static extern void   Delete(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_SetDuration")]
            internal static extern void   SetDuration(HandleRef animation, float duration);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_GetDuration")]
            internal static extern float GetDuration(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_SetDelay")]
            internal static extern void SetDelay(HandleRef animation, float duration);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_GetDelay")]
            internal static extern float GetDelay(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_SetLooping")]
            internal static extern void SetLooping(HandleRef animation, bool loop);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_SetLoopCount")]
            internal static extern void SetLoopCount(HandleRef animation, int count);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_GetLoopCount")]
            internal static extern int GetLoopCount(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_GetCurrentLoopCount")]
            internal static extern int GetCurrentLoopCount(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_IsLooping")]
            internal static extern bool IsLooping(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_SetDefaultAlphaFunction")]
            internal static extern void SetDefaultAlphaFunction(HandleRef animation, HandleRef alphaFunction);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_GetDefaultAlphaFunction")]
            internal static extern IntPtr GetDefaultAlphaFunction(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_SetCurrentProgress")]
            internal static extern void SetCurrentProgress(HandleRef animation, float progress);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_GetCurrentProgress")]
            internal static extern float GetCurrentProgress(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_SetSpeedFactor")]
            internal static extern void SetSpeedFactor(HandleRef animation, float factor);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_GetSpeedFactor")]
            internal static extern float GetSpeedFactor(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_SetPlayRange")]
            internal static extern void SetPlayRange(HandleRef animation, HandleRef range);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_GetPlayRange")]
            internal static extern IntPtr GetPlayRange(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_GetTime")]
            internal static extern float GetTime(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_Play")]
            internal static extern void Play(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_Pause")]
            internal static extern void Pause(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_Stop")]
            internal static extern void Stop(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_GetState")]
            internal static extern int GetState(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_Clear")]
            internal static extern void Clear(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_FinishedSignal")]
            internal static extern IntPtr FinishedSignal(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_UpdateSignal")]
            internal static extern IntPtr UpdateSignal(HandleRef animation);

            //TVAnimationEX  update signal interop
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_FinishedSignal_Empty")]
            internal static extern bool FinishedSignal_Empty(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEXSignal_FinishedSignal_GetConnectionCount")]
            internal static extern uint FinishedSignal_GetConnectionCount(HandleRef animation);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEXSignal_FinishedSignal_Connect")]
            internal static extern void FinishedSignal_Connect(HandleRef animation, HandleRef finishSignal);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEXSignal_FinishedSignal_Disconnect")]
            internal static extern void FinishedSignal_Disconnect(HandleRef animation, HandleRef finishSignal);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEXSignal_FinishedSignal_Emit")]
            internal static extern void FinishedSignal_Emit(IntPtr finishSignal);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_Dali_new_AnimationEX_FinishedSignal")]
            internal static extern IntPtr FinishedSignal_New();

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_delete_AnimationEX_FinishedSignal")]
            internal static extern void FinishedSignal_Delete(HandleRef finishSignal);

            //TVAnimationEX Update signal interop
            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEX_UpdateSignal_Empty")]
            internal static extern bool UpdateSignal_Empty(HandleRef updateSignal);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEXSignal_UpdateSignal_GetConnectionCount")]
            internal static extern uint UpdateSignal_GetConnectionCount(HandleRef updateSignal);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEXSignal_UpdateSignal_Connect")]
            internal static extern void UpdateSignal_Connect(HandleRef animation, HandleRef updateCallback);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_AnimationEXSignal_UpdateSignal_Disconnect")]
            internal static extern void UpdateSignal_Disconnect(HandleRef animation, HandleRef updateCallback);

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_Dali_new_AnimationEX_UpdateSignal")]
            internal static extern IntPtr UpdateSignal_New();

            [DllImport(Libraries.DaliExtension, EntryPoint = "CSharp_DaliExt_delete_AnimationEX_UpdateSignal")]
            internal static extern void UpdateSignal_Delete(HandleRef updateSignal);
        }
    }
}


