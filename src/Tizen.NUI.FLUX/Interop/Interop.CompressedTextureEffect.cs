/// @file Interop.CompressedTextureEffect.cs
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

namespace Tizen.NUI.FLUX
{
    internal static partial class Interop
    {
        internal static class CompressedTextureEffect
        {
            /// <summary>
            /// Compressed Texture effect is used to display compressed image inside ImageView.
            /// This support ETC1 and ASTC format texture with extension.ktx.
            /// VD Compressed Texture also support ETC1 + ALPHA.
            /// Following is guideling for ETC1 + ALPHA
            /// 1) Should Use Mali Texture Compression Tool or VDTool for creating
            /// ETC1 Compressed Texture with Alpha.We support Atlas Method.
            ///
            /// in which compressed ALPHA pixel of image is prepand to compressed RGB pixel of image.
            ///
            ///
            /// 2) For Alpha enable compressed image name should be "image_name.etc_a".
            ///
            /// 3) Extension should be '.ktx'.
            /// Usage exxample:
            ///
            /// IntPtr intPtr = Interop.CompressedTextureEffect.CreateCompressedTextureEffect("imagename.etc_a.ktx")
            /// PropertyMap map = new PropertyMap(intPtr, true);
            /// </summary>
            /// <param name="path"></param>
            /// <returns>IntPtr of PropertyMap</returns>
            [DllImport(Libraries.DaliExtension, EntryPoint = "Csharp_DaliExt_CreateCompressedTextureEffect", CallingConvention = CallingConvention.Cdecl)]
            internal static extern global::System.IntPtr CreateCompressedTextureEffect(string path); 
         }
    }
}
