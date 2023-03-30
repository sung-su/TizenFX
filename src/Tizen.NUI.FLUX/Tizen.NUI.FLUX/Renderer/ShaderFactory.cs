/// @file ShaderFactory.cs
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



using System;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Shader factory for shader object
    /// retrive or store shader object for reuse.
    /// </summary>
    /// <code>
    /// Shader shader = ShaderFactory.Instance.GetShader(ShaderType.IMAGE);
    /// </code>
    public class ShaderFactory
    {
        /// <summary>
        /// Shader type for shader factory
        /// </summary>
        public enum ShaderType
        {
            /// <summary> Normal image shader </summary>
            IMAGE = 0,

            /// <summary> Npatch image shader </summary>
            NPATCH,

            /// <summary> Npatch border image shader </summary>
            NPATCH_BORDER,

            /// <summary> Border shader </summary>
            BORDER,

            /// <summary>
            /// Circle shader
            /// </summary>
            CIRCLE_COLOR,

            /// <summary>
            /// Rectangle shader
            /// </summary>
            RECTANGLE_COLOR,

            /// <summary>
            /// Roundede Rectanlge shader
            /// </summary>
            ROUNDED_RECT_COLOR
        };

        private readonly Shader[] builtInshaderList;

        private ShaderFactory()
        {
            builtInshaderList = new Shader[Enum.GetNames(typeof(ShaderType)).Length];
        }
        /// <summary>
        /// Get Instance of shader factory.
        /// </summary>
        public static ShaderFactory Instance { get; } = new ShaderFactory();

        /// <summary>
        /// Get shader for given type, 
        /// Custom Shader require valid id
        /// </summary>
        /// <param name="type"> shader type </param>
        /// <returns> valid shader object on success, null otherwise </returns>
        public Shader GetShader(ShaderType type)
        {
            if (!Enum.IsDefined(typeof(ShaderType), type))
            {
                CLog.Error("Shader type is invalid");
                return null;
            }

            int index = Convert.ToInt32(type);
            if (!builtInshaderList[index])
            {
                builtInshaderList[index] = type switch
                {
                    ShaderType.IMAGE => ShaderSource.QuadShader.texture,
                    ShaderType.BORDER => ShaderSource.BorderShader.border,
                    ShaderType.CIRCLE_COLOR => ShaderSource.CircleShader.color,
                    ShaderType.RECTANGLE_COLOR => ShaderSource.RectangleShader.color,
                    ShaderType.ROUNDED_RECT_COLOR => ShaderSource.RoundedRectShader.color,
                    ShaderType.NPATCH => ShaderSource.NPatchShader.texture,
                    ShaderType.NPATCH_BORDER => ShaderSource.NPatchShader.texture,
                    _ => throw new InvalidOperationException("Invalid shader type")
                };
            }
            return builtInshaderList[index];
        }
    }
}
