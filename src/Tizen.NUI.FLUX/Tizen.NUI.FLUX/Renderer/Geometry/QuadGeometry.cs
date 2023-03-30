/// @file QuadGeometry.cs
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
using System.Collections.Generic;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Quad Geometry
    /// </summary>
    internal class QuadGeometry: GeometryBase
  {
    internal QuadGeometry():base()
    {
      CreateQuadGeometry();
    }

    /// <summary>
    /// Create quad geometry vertex data
    /// </summary>
    /// <returns> geometry </returns>
    private void CreateQuadGeometry()
    {
      float halfWidth = 0.5f;
      float halfHeight = 0.5f;

      List<VertexData> vertexData = new List<VertexData>();
      vertexData.Add(new VertexData(-halfWidth, -halfHeight));
      vertexData.Add(new VertexData(halfWidth, -halfHeight));
      vertexData.Add(new VertexData(-halfWidth, halfHeight));
      vertexData.Add(new VertexData(halfWidth, halfHeight));

      base.GenerateGeometry(vertexData);
      this.SetType(Geometry.Type.TRIANGLE_STRIP);
    }
  }
}
