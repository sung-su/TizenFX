/// @file BorderShader.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.6.0 </version>
/// <SDK_Support> Y </SDK_Support>
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


using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    internal static partial class ShaderSource
    {
        internal static class BorderShader
        {
            private readonly static string VERTEX = @"
                attribute highp vec2 aPosition;
                attribute highp vec2 aDrift;
                uniform highp mat4 uMvpMatrix;
                uniform highp vec3 uSize;
                uniform highp float uBorderWidth;
                void main()
                {
                    vec2 position = (aPosition*uSize.xy) + (aDrift*uBorderWidth);
                    gl_Position = uMvpMatrix * vec4(position, 0.0, 1.0);
                }";

            private readonly static string FRAGMENT = @"
                uniform highp vec4 uBorderColor;
                uniform highp vec4 uColor;
                void main()
                {
                    gl_FragColor = uBorderColor;
                    gl_FragColor.a *= uColor.a;
                }";

            private readonly static string VERTEX_ANTI_ALIASING = @"
                attribute highp vec2 aPosition;
                attribute highp vec2 aDrift;
                uniform highp mat4 uMvpMatrix;
                uniform highp vec3 uSize;
                uniform highp float uBorderWidth;
                varying highp float vAlpha;
                void main()
                {
                    vec2 position = (aPosition*(uSize.xy+vec2(0.75))) + (aDrift*(uBorderWidth+1.5));
                    gl_Position = uMvpMatrix * vec4(position, 0.0, 1.0);
                    vAlpha = min( abs(aDrift.x), abs(aDrift.y) )*(uBorderWidth+1.5);
                }";

            private readonly static string FRAGMENT_ANTI_ALIASING = @"
                uniform highp vec4 uBorderColor;
                uniform highp vec4 uColor;
                uniform highp float uBorderWidth;
                varying highp float vAlpha;
                void main()
                {
                    gl_FragColor = uBorderColor;
                    gl_FragColor.a *= uColor.a;
                    gl_FragColor.a *= smoothstep(0.0, 1.5, vAlpha)*smoothstep( uBorderWidth+1.5, uBorderWidth, vAlpha );
                }";

            static internal Shader border = new Shader(VERTEX, FRAGMENT);
            static internal Shader borderAntiAliasing = new Shader(VERTEX_ANTI_ALIASING, FRAGMENT_ANTI_ALIASING);
        }
    }
}
