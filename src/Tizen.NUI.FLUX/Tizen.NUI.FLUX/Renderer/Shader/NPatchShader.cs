/// @file NPatchShader.cs
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
        internal static class NPatchShader
        {
            private readonly static string VERTEX = @"
                attribute highp vec2 aPosition;
                varying highp vec2 vTexCoord;
                uniform highp mat4 uModelMatrix;
                uniform highp mat4 uMvpMatrix;
                uniform highp vec3 uSize;
                uniform highp vec2 uFixed[3];
                uniform highp vec2 uStretchTotal;
                void main()
                {
                    highp vec2 scale = vec2( length( uModelMatrix[ 0 ].xyz ), length( uModelMatrix[ 1 ].xyz ) );
                    highp vec2 size = uSize.xy;
                    highp vec2 fixedFactor = vec2( uFixed[ int( ( aPosition.x + 1.0 ) * 0.5 ) ].x, uFixed[ int( ( aPosition.y  + 1.0 ) * 0.5 ) ].y );
                    highp vec2 stretch = floor( aPosition * 0.5 );
                    highp vec2 fixedTotal = uFixed[ 2 ];
                    highp vec4 vertexPosition = vec4( fixedFactor + ( size - fixedTotal ) * stretch, 0.0, 1.0 );
                    vertexPosition.xy -= size * vec2( 0.5, 0.5 );
                    vertexPosition = uMvpMatrix * vertexPosition;
                    vTexCoord = ( fixedFactor + stretch * uStretchTotal ) / ( fixedTotal + uStretchTotal );
                    gl_Position = vertexPosition;
                }";

            private readonly static string FRAGMENT_TEXTURE = @"
                varying highp vec2 vTexCoord;
                uniform sampler2D original_Texture;
                uniform highp vec4 uColor;
                void main()
                {
                    highp vec4 texColor = texture2D(original_Texture, vTexCoord);
                    gl_FragColor = texColor*uColor;
                }";

            static internal Shader texture = new Shader(VERTEX, FRAGMENT_TEXTURE);
        }
    }
}
