/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file FluxLayoutParam.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.6.0 </version>
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

using System;
using System.ComponentModel;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// 
    /// </summary>
    public class FluxLayoutParam : LayoutItemParam
    {
        /// <summary>
        /// 
        /// </summary>
        public FluxLayoutParam()
        {
            Initialize();
        }

        /// <summary>
        /// FluxLayoutParam Constructor with LayoutTypes type argument
        /// </summary>
        /// <param name="type">Layout Type</param>
        /// <version>10.10.0</version>
        public FluxLayoutParam(LayoutTypes type)
        {
            Initialize();
            this.type = type;
        }

        private void Initialize()
        {
        }

        /// <summary>
        /// The property is to align item in horizontal and vertical.
        /// There are 2 cases in layout:
        /// 1. If the layout is Frame layout, the property belongs to the child of the Frame layout;
        /// 2. Other layout, the property belongs to the layout.
        /// </summary>
        public Aligns Align
        {
            set
            {
                itemAlign = value;
            }
            get
            {
                return itemAlign;
            }
        }

        /// <summary>
        /// The property of the layout, mean the gap between items in the layout.
        /// </summary>
        public int ItemGap
        {
            set
            {
                itemGap = value;
            }
            get
            {
                return itemGap;
            }
        }

        /// <summary>
        /// The property of the layout(FlexH), mean the gap when rearranged child item, between items per row.
        /// Default Value is same to ItemGap.
        /// </summary>
        /// <version> 8.8.0 </version>
        public int? RearrangeGap
        {
            get
            {
                return rearrangeGap;
            }
            set
            {
                rearrangeGap = value;
            }
        }

        /// <summary>
        /// The property of the layout.
        /// </summary>
        public Padding Padding
        {
            get
            {
                return padding;
            }
            set
            {
                padding = value;
            }
        }

        /// <summary>
        /// The property of the layout.
        /// </summary>
        public RearrangeRules Rearrange
        {
            get
            {
                return rearrangerule;
            }
            set
            {
                rearrangerule = value;
            }
        }

        /// <summary>
        /// The property of the layout.
        /// if set Enable, item`s start position is changed to ( margin value , 0 ) from (0,0)       
        /// </summary>
        /// <version> 8.8.0 </version>
        public MarginAreaPolicy MarginAreaPolicy
        {
            get
            {
                return marginAreaPolicy;
            }
            set
            {
                marginAreaPolicy = value;
            }
        }


        /// <summary>
        /// The property of Table Layout` column
        /// </summary>
        /// <deprecated> Deprecated since 9.9.0.  Please use TableLayoutInfo.</deprecated>
        [Obsolete("@Deprecated Col is deprecated. Please use TableLayoutInfo.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Col
        {
            set
            {
                col = value;
            }
            get
            {
                return col;
            }
        }

        /// <summary>
        /// The property of Table Layout` row
        /// </summary>
        /// <deprecated> Deprecated since 9.9.0.  Please use TableLayoutInfo.</deprecated>
        [Obsolete("@Deprecated Row is deprecated. Please use TableLayoutInfo.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Row
        {
            set
            {
                row = value;
            }
            get
            {
                return row;
            }
        }

        /// <summary>
        /// The property for the Layout enable arrange items for RTL
        /// </summary>
        public bool EnableRTL
        {
            get
            {
                return enableRTL;
            }
            set
            {
                enableRTL = value;
            }
        }

        /// <summary>
        /// The Information of Table layout.
        /// </summary>
        /// <version> 9.9.0 </version>
        public TableLayoutInfo TableLayoutInfo
        {
            get => tableLayoutInfo;
            set => tableLayoutInfo = value;
        }

        /// <summary>
        /// The method is for item coodinate set in Table Layout
        /// </summary>
        public int ItemCoodinate(FluxView item, int x, int y, int w, int h)
        {
            int ret = 0;

            return ret;
        }

        // Need to discuss [dy.chu]
        public LayoutTypes type = LayoutTypes.FlexH;

        #region internal Field
        internal bool isRootLayout = false;

        #endregion internal Field

        private Aligns itemAlign = Aligns.None;
        private RearrangeRules rearrangerule = RearrangeRules.On;
        internal MarginAreaPolicy marginAreaPolicy = MarginAreaPolicy.Disable;
        private Padding padding = new Padding();
        private int itemGap = 1;
        private int? rearrangeGap = null;
        private int col = 0;
        private int row = 0;
        private bool enableRTL = true;
        private TableLayoutInfo tableLayoutInfo = new TableLayoutInfo();
    }
}