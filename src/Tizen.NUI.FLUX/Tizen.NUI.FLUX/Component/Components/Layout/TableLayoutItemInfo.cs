/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file TableLayoutItemInfo.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 9.9.0 </version>
/// <SDK_Support> Y </SDK_Support>
///
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

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// The information of the item in the Table layout.
    /// </summary>
    public class TableLayoutItemInfo
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        public TableLayoutItemInfo()
        {
        }

        /// <summary>
        /// The column index of the item in Table layout.
        /// </summary>
        public int ColumnIndex
        {
            get;
            set;
        }
        /// <summary>
        /// The row index of the item in Table layout.
        /// </summary>
        public int RowIndex
        {
            get;
            set;
        }
        /// <summary>
        /// The column span of the item in Table layout.
        /// </summary>
        public int ColumnSpan
        {
            get;
            set;
        }
        /// <summary>
        /// The row span of the item in Table layout.
        /// </summary>
        public int RowSpan
        {
            get;
            set;
        }



        /// <summary>
        /// Update Cell Geomety Information of the item in Table layout.
        /// </summary>
        /// <param name="columnIndex"> ColumnIndex information </param>
        /// <param name="rowIndex"> RowIndex information </param>
        /// <param name="columnSpan"> ColumnSpan information </param>
        /// <param name="rowSpan"> RowSpan information </param>
        /// <version> 10.10.0 </version>
        public void UpdateCellGeomery(int columnIndex,int rowIndex, int columnSpan, int rowSpan)
        {
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
            ColumnSpan = columnSpan;
            RowSpan = rowSpan;
        }
    }
}