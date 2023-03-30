/// @file CircleShader.cs
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
        internal static class CircleShader
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

            private readonly static string CIRCLE_FUNC = @"
                float circle(vec2 p, float radius, vec2 center, float smoothStep)
                {
                    radius -= smoothStep;
                    p = p -vec2(center);
                    float dist = length(p);
                    return 1.0 - smoothstep(radius-smoothStep, radius+smoothStep, dist);
                }";

            private readonly static string FRAGMENT = @"
                precision highp float;
                varying highp vec4 vTexCoord_primitiveSize;
                uniform highp vec4 uPrimitiveOffset_blurSize;
                uniform highp vec4 uPrimitiveColor;

                " + CIRCLE_FUNC + @"

                void main()
                {
                    vec2 fragCoord = vTexCoord_primitiveSize.zw*vTexCoord_primitiveSize.xy;
                    vec2 center = vec2(0.5)*vTexCoord_primitiveSize.zw;

                    float radius = 0.5*vTexCoord_primitiveSize.z;
                    float fillColor = circle(fragCoord, radius, center, uPrimitiveOffset_blurSize.w);

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
