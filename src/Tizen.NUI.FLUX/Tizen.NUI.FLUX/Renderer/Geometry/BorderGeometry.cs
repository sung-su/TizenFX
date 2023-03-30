/// @file BorderGeometry.cs
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
using System.Runtime.InteropServices;
using System;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Border Geometry
    /// </summary>
    internal class BorderGeometry : GeometryBase
    {
        internal BorderGeometry()
        {
            CreateBorderGeometry();
        }
    
        private struct BorderVertex
        {
            public VertexData position;
            public VertexData drift;
            public BorderVertex(VertexData position, VertexData drift)
            {
                this.position = position;
                this.drift = drift;
            }
        };

        /// <summary>
        /// Create Border vertex data.
        /// </summary>
        private void CreateBorderGeometry()
        {
            const float halfWidth = 0.5f;
            const float halfHeight = 0.5f;
            BorderVertex[] BorderVertexData = new BorderVertex[16];
            BorderVertexData[0] = new BorderVertex(new VertexData(-halfWidth, -halfHeight), new VertexData(0.0f, 0.0f));
            BorderVertexData[1] = new BorderVertex(new VertexData(-halfWidth, -halfHeight), new VertexData(1.0f, 0.0f));
            BorderVertexData[2] = new BorderVertex(new VertexData(halfWidth, -halfHeight), new VertexData(-1.0f, 0.0f));
            BorderVertexData[3] = new BorderVertex(new VertexData(halfWidth, -halfHeight), new VertexData(0.0f, 0.0f));

            BorderVertexData[4] = new BorderVertex(new VertexData(-halfWidth, -halfHeight), new VertexData(0.0f, 1.0f));
            BorderVertexData[5] = new BorderVertex(new VertexData(-halfWidth, -halfHeight), new VertexData(1.0f, 1.0f));
            BorderVertexData[6] = new BorderVertex(new VertexData(halfWidth, -halfHeight), new VertexData(-1.0f, 1.0f));
            BorderVertexData[7] = new BorderVertex(new VertexData(halfWidth, -halfHeight), new VertexData(0.0f, 1.0f));

            BorderVertexData[8] = new BorderVertex(new VertexData(-halfWidth, halfHeight), new VertexData(0.0f, -1.0f));
            BorderVertexData[9] = new BorderVertex(new VertexData(-halfWidth, halfHeight), new VertexData(1.0f, -1.0f));
            BorderVertexData[10] = new BorderVertex(new VertexData(halfWidth, halfHeight), new VertexData(-1.0f, -1.0f));
            BorderVertexData[11] = new BorderVertex(new VertexData(halfWidth, halfHeight), new VertexData(0.0f, -1.0f));

            BorderVertexData[12] = new BorderVertex(new VertexData(-halfWidth, halfHeight), new VertexData(0.0f, 0.0f));
            BorderVertexData[13] = new BorderVertex(new VertexData(-halfWidth, halfHeight), new VertexData(1.0f, 0.0f));
            BorderVertexData[14] = new BorderVertex(new VertexData(halfWidth, halfHeight), new VertexData(-1.0f, 0.0f));
            BorderVertexData[15] = new BorderVertex(new VertexData(halfWidth, halfHeight), new VertexData(0.0f, 0.0f));

            int length = Marshal.SizeOf(BorderVertexData[0]);
            IntPtr vertexPnt = Marshal.AllocHGlobal(length * 16);

            try
            {
                for (int i = 0; i < 16; i++)
                {
                    Marshal.StructureToPtr(BorderVertexData[i], vertexPnt + i * length, true);
                }
        
                PropertyMap vertexFormat = new PropertyMap();
                vertexFormat.Add("aPosition", new PropertyValue((int)PropertyType.Vector2));
                vertexFormat.Add("aDrift", new PropertyValue((int)PropertyType.Vector2));
        
                PropertyBuffer vertexBuffer = new PropertyBuffer(vertexFormat);
                vertexBuffer.SetData(vertexPnt, 16u);
                this.AddVertexBuffer(vertexBuffer);
        
                // Create indices
                ushort[] indexData = new ushort[] { 1, 5, 2, 6, 3, 7, 7, 6, 11, 10, 15, 14, 14, 10, 13, 9, 12, 8, 8, 9, 4, 5, 0, 1 };
                this.SetIndexBuffer(indexData, 24u);
                this.SetType(Geometry.Type.TRIANGLE_STRIP);
            }

            finally
            {
                Marshal.FreeHGlobal(vertexPnt);
            }
        }
    }
}
