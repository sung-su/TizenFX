/**
 *Copyright (c) 2022 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */

/// @file FluxBackground.BindableProperty.cs
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
/// 
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Binding;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// BindableProperty of FluxBackground.
    /// </summary>
    /// <code>
    /// FluxBackground fluxBG = new FluxBackground((int)SizeWidth, (int)SizeHeight);
    /// </code>
    public partial class FluxBackground : View
    {
        /// <summary>
        /// BindableProperty for ThemeBackgroundColorChip, it's used as an argument of SetBinding API to bind a value to FluxBackground object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty ThemeBackgroundColorChipProperty = BindableProperty.Create(nameof(ThemeBackgroundColorChip), typeof(string), typeof(FluxBackground), (typeof(string).IsValueType ? global::System.Activator.CreateInstance(typeof(string)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (FluxBackground)bindable;
            target.privateThemeBackgroundColorChip = (string)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (FluxBackground)bindable;
            return target.privateThemeBackgroundColorChip;
        });
    }
}
