/// @file GeometryBase.cs
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
using System.Runtime.InteropServices;
using Tizen.NUI;
using System.Collections.Generic;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Base class for Geometry providing utility function for dervide classes .
    /// </summary>
    /// <code>
    /// Geometry geometry = GeometryFactory.Instance.GetGeometry(GeometryType.QUAD);
    /// </code>
    internal class GeometryBase: Geometry
  {
    protected GeometryBase():base()
    {
    }

    /// <summary>
    /// Vertex Data
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    protected struct VertexData
    {
      public float x;
      public float y;
      public VertexData(float x, float y)
      {
        this.x = x;
        this.y = y;
      }
    }
    protected void AddVeritces(ref List<VertexData> vertexData, int x, int y)
    {
      vertexData.Add(new VertexData(x, y));
    }

    protected void AddQuadIndices(ref List<ushort> indices, uint rowIdx, uint nextRowIdx)
    {
      indices.Add(Convert.ToUInt16(rowIdx));
      indices.Add(Convert.ToUInt16(nextRowIdx + 1));
      indices.Add(Convert.ToUInt16(rowIdx + 1));

      indices.Add(Convert.ToUInt16(rowIdx));
      indices.Add(Convert.ToUInt16(nextRowIdx));
      indices.Add(Convert.ToUInt16(nextRowIdx + 1));
    }

    /// <summary>
    /// Create geometry and add vertex data.
    /// </summary>
    /// <param name="vertices"> list  of vertices </param>
    /// <param name="indices"> index list </param>
    /// <exception cref="OutOfMemoryException"> Failed to allocate native memory </exception>
    /// <exception cref="Exception" > Failed to marshel the data. </exception>
    /// <returns> geometry object </returns>
    protected void GenerateGeometry(List<VertexData> vertices, List<ushort> indices = null)
    {
      IntPtr vertexDataPtr = IntPtr.Zero;
      try
      {
        //Allocate Unmanaged Memory 
        //C# will not free memory automatically. 
        //The user must free it with Marsha.FreeGlobal                 
        int length = Marshal.SizeOf(vertices[0]);
        vertexDataPtr = Marshal.AllocHGlobal(length * vertices.Count);
        for (int i = 0; i < vertices.Count; i++)
        {
          Marshal.StructureToPtr(vertices[i], vertexDataPtr + i * length, true);
        }
        PropertyMap vertexFormat = new PropertyMap();
        vertexFormat.Add("aPosition", new PropertyValue((int)PropertyType.Vector2));
        PropertyBuffer vertexBuffer = new PropertyBuffer(vertexFormat);
        vertexBuffer.SetData(vertexDataPtr, Convert.ToUInt32(vertices.Count));
        this.AddVertexBuffer(vertexBuffer);
        if (indices != null && indices.Count > 0)
        {
          this.SetIndexBuffer(indices.ToArray(), Convert.ToUInt32(indices.Count));
        }
      }
      catch (OutOfMemoryException e)
      {
        throw new OutOfMemoryException("Failed to Allocate Memory:" + e.Message);
      }
      catch (ArgumentException e)
      {
        throw new Exception("Failed to Marshal:" + e.Message);
      }
      finally
      {
        if (IntPtr.Zero != vertexDataPtr)
        {
          Marshal.FreeHGlobal(vertexDataPtr);
        }
      }
    }
  }
}
