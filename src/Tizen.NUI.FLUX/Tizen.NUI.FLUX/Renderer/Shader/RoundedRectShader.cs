/// @file RoundedRectShader.cs
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
        internal static class RoundedRectShader
        {
            private readonly static string VERTEX = @"
                attribute highp vec2 aPosition;
                varying highp vec4 vTexCoord_primitiveSize;
                uniform highp mat4 uMvpMatrix;
                uniform highp vec3 uSize;
                uniform highp vec4 uPrimitiveOffset_blurSize;
                void main()
                {
                    vec2 primitiveOffset = (uPrimitiveOffset_blurSize.xy-vec2(uPrimitiveOffset_blurSize.z))/uSize.xy;
                    vTexCoord_primitiveSize.zw = uSize.xy+vec2(2.0)*uPrimitiveOffset_blurSize.z;
                    vec4 vertexPosition = vec4( (aPosition + vec2(0.5))*vTexCoord_primitiveSize.zw + (primitiveOffset + vec2(-0.5))*uSize.xy, 0.0, 1.0 );
                    vertexPosition = uMvpMatrix * vertexPosition;
                    vTexCoord_primitiveSize.xy = aPosition + vec2(0.5);
                    gl_Position = vertexPosition;
                }";

            private readonly static string ROUND_RECT = @"
                float roundedRect(vec2 p, vec2 startPosition, vec2 rectWidth, float rc, float smoothStep) 
                {
                    vec2 halfSize = vec2(0.5)*rectWidth;
                    vec2 center = startPosition + halfSize;
                    
                    p = max(abs(p- center) - halfSize + vec2(rc), 0.0);
                    float d = smoothstep(0.0, smoothStep, length(p) -rc);
                    d = 1.0 - d;
                    return d;
                }";

            private readonly static string FRAGMENT = @"
                precision highp float;
                varying highp vec4 vTexCoord_primitiveSize;
                uniform highp float uRadius;
                uniform highp vec4 uPrimitiveColor;
                uniform highp vec4 uRectBound;
                uniform highp vec4 uPrimitiveOffset_blurSize;

                " + ROUND_RECT + @"

                void main()
                {
                    vec2 fragCoord;
                    fragCoord.x = clamp(vTexCoord_primitiveSize.x, uRectBound.x, uRectBound.y);
                    fragCoord.y = clamp(vTexCoord_primitiveSize.y, uRectBound.z, uRectBound.w);
                    fragCoord = (vTexCoord_primitiveSize.zw-vec2(2.0))*fragCoord + vec2(1.0);
                    vec2 startP = vec2(uPrimitiveOffset_blurSize.w);
                    vec2 rectWidth = vTexCoord_primitiveSize.zw - vec2(2.0)*startP;
                    //rounded rect
                    float fillColor = roundedRect(fragCoord, startP , rectWidth, uRadius, uPrimitiveOffset_blurSize.w);

                    if(fillColor <= 0.0)
                    {
                        discard;
                    }
                    
                    gl_FragColor = vec4(uPrimitiveColor.rgb, uPrimitiveColor.a*fillColor);
                }";

            static internal Shader color = new Shader(VERTEX, FRAGMENT);
        }
    }
}
