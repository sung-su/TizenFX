/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file TemplatePresetBase.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 8.8.0 </version>
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
using System.Collections.Generic;
using Tizen.NUI;
namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// This is TemplatePresetBase Class
    /// <code>
    /// TemplatePresetBase retPreset = null;
    /// </code>
    /// </summary>
    public class TemplatePresetBase : PresetBase
    {
        /// <summary>
        /// Constructor to instantiate the TemplatePresetBase class.
        /// </summary>
        public TemplatePresetBase()
        {
        }
        internal virtual void CreateRootLayout(Layer layer)
        {
            ElementName[RootLayoutName] = RootLayoutTreeNode;
            RootLayout rootLayout = new RootLayout(layer, LayoutTypes.FlexV)
            {
                BackgroundEnabled = true
            };
            rootLayout.LayoutParam.ItemGap = 0;
            elements[RootLayoutTreeNode] = rootLayout;
        }


        private const string RootLayoutName = "RootLayout";
        internal const string RootLayoutTreeNode = "1";
        internal const string HatTopAreaName = "HatTopArea";
        internal const string BodySkinAreaName = "BodySkinArea";
        internal const string FootButtonsAreaName = "FootButtonsArea";
        internal const string BodyAreaName = "BodyArea";
        internal const string TopName = "TOP";
        internal const string HatName = "HAT";
        internal const string NeckName = "NECK";
        internal const string BodyName = "BODY";
        internal const string SkinName = "SKIN";
        internal const string FootName = "FOOT";
        internal const string ButtonsName = "BUTTONS";
        internal const int AreaMinvalue = 0;
        internal const int AreaMaxvalue = 480;
        //It will be deleted soon
        internal Dictionary<string, Dictionary<string, object>> TemplateAreaProperty = new Dictionary<string, Dictionary<string, object>>();
        internal List<KeyValuePair<string, int>> AreaLayoutIndex = new List<KeyValuePair<string, int>>();
        internal Dictionary<string, ComponentBase> lnternalLayoutElements = new Dictionary<string, ComponentBase>();
    }

}
