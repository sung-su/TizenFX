/// @file NPatchBorderGeometry.cs
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
    /// NPatchBorder Geometry
    /// </summary>
    internal class NPatchBorderGeometry : GeometryBase
    {
        internal NPatchBorderGeometry(Uint16Pair gridSize)
        {
            CreateNPatchBorderGeometry(gridSize);
        }

        /// <summary>
        /// Create NPatchBorder vertex data.
        /// </summary>
        /// <param name="gridSize"> grid size for npathcborder. </param>
        /// <returns> geometry </returns>
        private void CreateNPatchBorderGeometry(Uint16Pair gridSize)
        {
            CLog.Debug("CreateNPatchBorderGeometry");
            ushort gridWidth = gridSize.GetWidth();
            ushort gridHeight = gridSize.GetHeight();

            // Create vertices
            List<VertexData> verticesData = new List<VertexData>();
            int y = 0;
            for (; y < 2; ++y)
            {
                for (int x = 0; x < gridWidth + 1; ++x)
                {
                    AddVeritces(ref verticesData, x, y);
                }
            }

            for (; y < gridHeight - 1; ++y)
            {
                //left
                AddVeritces(ref verticesData, 0, y);
                AddVeritces(ref verticesData, 1, y);

                //right
                AddVeritces(ref verticesData, gridWidth - 1, y);
                AddVeritces(ref verticesData, gridWidth, y);
            }

            //bottom
            for (; y < gridHeight + 1; ++y)
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

            for (int x = 0; x < gridWidth; ++x, ++nextRowIdx, ++rowIdx)
            {
                AddQuadIndices(ref indicesData, rowIdx, nextRowIdx);
            }

            if (gridHeight > 2)
            {
                rowIdx = gridWidth + 1u;
                nextRowIdx = (gridWidth + 1u) * 2u;

                uint increment = gridWidth - 1u;
                if (gridHeight > 3)
                {
                    increment = 2;
                    //second row left
                    AddQuadIndices(ref indicesData, rowIdx, nextRowIdx);

                    rowIdx = gridWidth * 2u;
                    nextRowIdx = (gridWidth + 1u) * 2u + 2u;
                    //second row right
                    AddQuadIndices(ref indicesData, rowIdx, nextRowIdx);

                    //left and right
                    rowIdx = nextRowIdx - 2;
                    nextRowIdx = rowIdx + 4;
                    y = 2;
                    for (; y < 2 * (gridHeight - 3); ++y, rowIdx += 2, nextRowIdx += 2)
                    {
                        AddQuadIndices(ref indicesData, rowIdx, nextRowIdx);
                    }
                }

                //second row left
                AddQuadIndices(ref indicesData, rowIdx, nextRowIdx);

                rowIdx += increment;
                nextRowIdx += gridWidth - 1u;
                //second row right
                AddQuadIndices(ref indicesData, rowIdx, nextRowIdx);
            }
            //bottom
            rowIdx = nextRowIdx - gridWidth + 1;
            nextRowIdx = rowIdx + gridWidth + 1;
            for (int x = 0; x < gridWidth; ++x, ++nextRowIdx, ++rowIdx)
            {
                AddQuadIndices(ref indicesData, rowIdx, nextRowIdx);
            }

            base.GenerateGeometry(verticesData, indicesData);
        }
    }
}
