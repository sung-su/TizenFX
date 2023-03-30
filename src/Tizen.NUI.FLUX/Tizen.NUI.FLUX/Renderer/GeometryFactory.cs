/// @file GeometryFactory.cs
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
    /// Factory class to create and cache different types of geometry.
    /// </summary>
    /// <code>
    /// Geometry geometry = GeometryFactory.Instance.GetGeometry(GeometryType.QUAD);
    /// </code>
    public class GeometryFactory
    {

        /// <summary>
        /// Geometry type for Geometry Factory.
        /// </summary>
        public enum GeometryType
        {
            /// <summary> Rectangle geometry </summary>
            QUAD = 0,

            /// <summary> NPatch geometry </summary>
            NPATCH = 1,

            /// <summary> NPatch border geometry </summary>
            NPATCH_BORDER,

            /// <summary> Border geometry </summary>
            BORDER
        }

        /// <summary>
        /// Get Instance of geometry factory class.
        /// </summary>
        public static GeometryFactory Instance { get; } = new GeometryFactory();

        private readonly Geometry[] builtInGeometryList;

        /// <summary>
        /// Create GeometryFactor class object. 
        /// </summary>
        private GeometryFactory()
        {
            builtInGeometryList = new Geometry[Enum.GetNames(typeof(GeometryType)).Length];
        }

        /// <summary>
        /// Create different types of geometry.
        /// </summary>
        /// <param name="type"> type of geometry </param>
        /// <returns> newly created geometry </returns>
        private Geometry CreateGeometry(GeometryType type)
        {
            Geometry geometry = null;

            if (type == GeometryType.QUAD)
            {
                geometry = new QuadGeometry();
            }
            else if (type == GeometryType.NPATCH)
            {
                Uint16Pair gridSize = new Uint16Pair(3, 3);
                geometry = new NPatchGeometry(gridSize);
            }
            else if (type == GeometryType.NPATCH_BORDER)
            {
                Uint16Pair gridSize = new Uint16Pair(3, 3);
                geometry = new NPatchBorderGeometry(gridSize);
            }
            else if (type == GeometryType.BORDER)
            {
                geometry = new BorderGeometry();
            }
            return geometry;
        }

        /// <summary>
        /// Get geometry of given type.
        /// </summary>
        /// <param name="type"> type of geometry </param>
        /// <returns> valid geomerty of given type on success, null on failure </returns>
        public Geometry GetGeometry(GeometryType type)
        {
            int index = Convert.ToInt32(type);
            if (index < 0 || index >= (Enum.GetNames(typeof(GeometryType)).Length))
            {
                CLog.Error("Geometry type is invalid");
                return null;
            }

            if (!builtInGeometryList[index])
            {
                builtInGeometryList[index] = CreateGeometry(type);
            }
            return builtInGeometryList[index];

        }
    }
}
