/// @file NPatchGeometry.cs
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


using System.Collections.Generic;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Factory class to create and cache different types of geometry.
    /// </summary>
    /// <code>
    /// Geometry geometry = GeometryFactory.Instance.GetGeometry(GeometryType.QUAD);
    /// </code>
    internal class NPatchGeometry : GeometryBase
    {
        internal NPatchGeometry(Uint16Pair gridSize)
        {
            CreateNPatchGridGeometry(gridSize);
        }

        /// <summary>
        ///  Create NPatch vertex data
        /// </summary>
        /// <param name="gridSize"> grid size for the npatch</param>
        /// <returns> geometry </returns>
        private void CreateNPatchGridGeometry(Uint16Pair gridSize)
        {
            CLog.Debug("CreateNPatchGridGeometry");
            ushort gridWidth = gridSize.GetWidth();
            ushort gridHeight = gridSize.GetHeight();

            // Create vertices
            List<VertexData> verticesData = new List<VertexData>();

            for (int y = 0; y < gridHeight + 1; ++y)
            {
                for (int x = 0; x < gridWidth + 1; ++x)
                {
                    AddVeritces(ref verticesData, x, y);
                }
            }

            // Create indices
            List<ushort> indicesData = new List<ushort>();
            uint rowIdx = 0;
            uint nextRowIdx = gridWidth + 1u;
            for (int y = 0; y < gridHeight; ++y, ++nextRowIdx, ++rowIdx)
            {
                for (int x = 0; x < gridWidth; ++x, ++nextRowIdx, ++rowIdx)
                {
                    AddQuadIndices(ref indicesData, rowIdx, nextRowIdx);
                }
            }
            base.GenerateGeometry(verticesData, indicesData);

        }
    }
}
