/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file LayoutData.cs
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

namespace Tizen.NUI.FLUX.Component
{

    #region public Enum
    /// <summary>
    /// Layout's Type enum value
    /// </summary>
    public enum LayoutTypes
    {
        /// <summary>
        /// Arrange all items (component OR layout) in a single vertical direction
        /// Only one component or layout per row can be placed.
        /// Responsive Rule: Resizing
        /// </summary>
        FlexV,
        /// <summary>
        /// Align all descendants in a single horizontal direction.
        /// Responsive Rule
        /// - Rule1) Rearrange
        /// - Rule2) Resize: (Default)
        /// </summary>
        FlexH,
        /// <summary>
        /// Use Linear Layout if you need to define the same property and size for the same component in groups.
        /// Components are placed horizontally. (No group is placed vertically)
        /// </summary>
        Linear,
        /// <summary>
        /// Arrange item in Z-Order.
        /// </summary>
        Frame,
        /// <summary>
        /// Not applicable.
        /// </summary>
        Table,
        /// <summary>
        /// Align item in 9 direction, default is TopLeft.
        /// </summary>
        /// <version> 9.9.0 </version>
        Box,
        /// <summary>
        /// None type.
        /// </summary>
        None
    }

    /// <summary>
    /// Layout's resizerule for item.
    /// this policy is adjust to child,
    /// </summary>
    public enum ResizePolicyTypes
    {
        /// <summary> </summary>
        None,
        /// <summary> It is fixed. </summary>
        Fixed,
        /// <summary> It has default value, user also can change it. </summary>
        Reserved,
        /// <summary> It will hold unfixed space. </summary>
        Shared,
        /// <summary> It should be same to its parent. </summary>
        MatchParent,
        /// <summary> 
        /// It depends on its children. Its children should have value. 
        /// It can't work if its children is Shared/MatchParent, because the two properties means its children depends on its parent.
        /// </summary>
        Wrap
    }

    /// <summary>
    /// child(layout`s child) able to have this property. ( not parent proeprty )
    /// if child reduce until minimunsize, do omission. minimumsize is breakpoint on omissionrule.
    /// </summary>
    public enum OmissionRules
    {
        /// <summary>
        /// Default. 
        /// OmissionRule is Off.
        /// </summary>
        Off,
        /// <summary>
        ///  OmissionRule is On.
        /// </summary>
        On,
    }


    /// <summary>
    /// Layout's AlignRule for item.
    /// this policy is adjust to child,
    /// </summary>
    public enum Aligns
    {
        /// <summary>
        /// Default 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        TopLeft,
        /// <summary>
        /// 
        /// </summary>
        TopCenter,
        /// <summary>
        /// 
        /// </summary>
        TopRight,
        /// <summary>
        /// 
        /// </summary>
        CenterLeft,
        /// <summary>
        /// 
        /// </summary>
        Center,
        /// <summary>
        /// 
        /// </summary>
        CenterRight,
        /// <summary>
        /// 
        /// </summary>
        BottomLeft,
        /// <summary>
        /// 
        /// </summary>
        BottomCenter,
        /// <summary>
        /// 
        /// </summary>
        BottomRight,
    };

    /// <summary>
    /// This Property is work on FlexH type.If set off, responsible rule is resize.
    /// default value On. 
    /// </summary>
    public enum RearrangeRules
    {
        /// <summary>
        /// Default.Resposible rule is Rearrage.
        /// </summary>
        On,
        /// <summary>
        /// Resposible rule is Resize.
        /// </summary>
        Off
    }

    /// <summary>
    /// Margin area apply to layout.
    /// Margin area is only apply to rootlayout. but if use this property, layout able to apply margin area.
    /// default value is Disable. 
    /// </summary>
    /// <code>
    /// layout.LayoutParam.MarginAreaPolicy = MarginAreaPolicy.EnableAll;
    /// </code>
    /// <version> 8.8.0 </version>
    public enum MarginAreaPolicy
    {
        /// <summary>
        /// Default 
        /// </summary>
        Disable,
        /// <summary>
        /// Left,Right side margin apply to Layout
        /// </summary>
        EnableAll,
        /// <summary>
        /// Only right side margin apply to Layout
        /// </summary>
        EnableRight,
        /// <summary>
        /// Only left side margin apply to Layout
        /// </summary>
        EnableLeft
    }
    #endregion public Enum
}