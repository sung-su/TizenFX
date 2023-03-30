/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file LayoutItemParam.cs
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
using System.Reflection;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// 
    /// </summary>
    public class LayoutItemParam : LayoutParam
    {
        #region public Method
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public LayoutItemParam()
        {
            Initialize();
        }
        /// <summary>
        /// Constructor to instantiate the LayoutItemParams class.
        /// </summary>
        public LayoutItemParam(int priority, int weight, OmissionRules omissionRule, ResizePolicyTypes heightResizePolicyParam, ResizePolicyTypes widthResizePolicyType)
        {
            this.priority = priority;
            this.weight = weight;
            this.omissionRule = omissionRule;
            this.heightResizePolicyParam = heightResizePolicyParam;
            this.widthResizePolicyType = widthResizePolicyType;
        }

        private void Initialize()
        {
        }
        #endregion public Method

        #region public Property
        /// <summary>
        /// 
        /// </summary>
        public ResizePolicyTypes WidthResizePolicy
        {
            set => widthResizePolicyType = value;
            get => widthResizePolicyType;
        }

        /// <summary>
        /// 
        /// </summary>
        public ResizePolicyTypes HeightResizePolicy
        {
            set => heightResizePolicyParam = value;
            get => heightResizePolicyParam;
        }

        /// <summary>
        /// 
        /// </summary>
        public OmissionRules Omission
        {
            set => omissionRule = value;
            get => omissionRule;
        }

        /// <summary>
        /// The property of item in layout, default Weight is '1'.
        /// </summary>
        public int Weight
        {
            set => weight = value;
            get => weight;
        }

        /// <summary>
        /// The property of the item in layout, default is the min 
        /// </summary>
        public int Priority
        {
            get => priority;
            set
            {
                if (value > 100)
                {
                    throw new ArgumentOutOfRangeException("Priority value should not be greater than MAX_PRIORITY_VALUE(100)");
                }
                priority = value;
            }
        }
        /// <summary>
        /// The property is only used for the item which parent is rootlayout. Default value is false. 
        /// For example, the title bar in AppStore should enlarge its width to fill the UIArea, it means the title bar will cover the margin area of the RootLayout.
        /// In this case, the margin should be disabled, the value should be false.
        /// </summary>
        public bool ExpandWidthToUIArea
        {
            get => enableExpandWidthToUIArea;
            set =>
                // TODO Exception handle. 
                // If parent is not rootlayout, not set;
                enableExpandWidthToUIArea = value;
        }
        /// <summary>
        /// The property that enable get item's height by its width with aspect ratio. 
        /// The specail spec for Banner.
        /// </summary>
        public bool EnableGetHeightByWidth
        {
            get => enableGetHeightByWidth;
            set => enableGetHeightByWidth = value;
        }
        /// <summary>
        /// The information of the item in Table layout.
        /// </summary>
        /// <version> 9.9.0 </version>
        public TableLayoutItemInfo TableLayoutItemInfo
        {
            get => tableLayoutItemInfo;
            set => tableLayoutItemInfo = value;
        }
        #endregion public Property

        #region internal Property
        // The item's alignment in the BoxLayout.
        internal Aligns ItemAlign
        {
            get;
            set;
        } = Aligns.TopLeft;


        internal float SizeRatio
        {
            get;
            set;
        } = 0.0f;

        #endregion internal Property

        internal void SetProperty(string property, object value)
        {
            GetType().GetProperty(property, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)?.SetValue(this, value);
        }

        internal object GetProperty(string property)
        {
            return GetType().GetProperty(property, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)?.GetValue(this);
        }

        #region internal Field
        // for layout
        internal int originalWidth = Spec.INVALID_VALUE;
        internal int originalHeight = Spec.INVALID_VALUE;
        #endregion internal Field

        #region private Field
        private int priority = 1;
        private int weight = 1;
        private OmissionRules omissionRule = OmissionRules.Off;
        private ResizePolicyTypes heightResizePolicyParam = ResizePolicyTypes.Reserved;
        private ResizePolicyTypes widthResizePolicyType = ResizePolicyTypes.Reserved;
        private bool enableExpandWidthToUIArea = false;
        private bool enableGetHeightByWidth = false;
        private TableLayoutItemInfo tableLayoutItemInfo = new TableLayoutItemInfo();
        #endregion private Field
    }
}