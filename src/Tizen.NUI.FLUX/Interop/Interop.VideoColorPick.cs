/// @file Interop.VideoColorPick.cs
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


using System.Runtime.InteropServices;
using System;

namespace Tizen.NUI.FLUX
{
    internal static partial class Interop
    {
        internal class VideoColorPick
        {                        
            [DllImport(Libraries.UIFW_Misc, EntryPoint = "ua_videocolorpick_subscribe", CallingConvention = CallingConvention.Cdecl)]
            public extern static int native_videocolorpick_subscribe(IntPtr info);

            [DllImport(Libraries.UIFW_Misc, EntryPoint = "ua_videocolorpick_unsubscribe", CallingConvention = CallingConvention.Cdecl)]
            public extern static int native_videocolorpick_unsubscribe(int id);
        }
    }
}
