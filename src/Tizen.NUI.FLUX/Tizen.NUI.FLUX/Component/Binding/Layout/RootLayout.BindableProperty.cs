/**
 *Copyright (c) 2022 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file RootLayout.BindableProperty.cs
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
using Tizen.NUI.Binding;


namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// BindableProperty of RootLayout.
    /// </summary>
    /// <code>
    /// RootLayout root = new RootLayout();
    /// root.Add(button);
    /// root.Add(textBox);
    /// root.UpdateLayout();
    /// </code>
    public partial class RootLayout : Layout
    {
        
        /// <summary>
        /// BindableProperty for LayoutParam, it's used as an argument of SetBinding API to bind a value to RootLayout object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty LayoutParamProperty = BindableProperty.Create(nameof(LayoutParam), typeof(RootLayoutParam), typeof(RootLayout), (typeof(RootLayoutParam).IsValueType ? global::System.Activator.CreateInstance(typeof(RootLayoutParam)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (RootLayout)bindable;
            target.privateLayoutParam = (RootLayoutParam)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (RootLayout)bindable;
            return target.privateLayoutParam;
        });
        
        /// <summary>
        /// BindableProperty for MarginMinimumSize, it's used as an argument of SetBinding API to bind a value to RootLayout object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty MarginMinimumSizeProperty = BindableProperty.Create(nameof(MarginMinimumSize), typeof(int), typeof(RootLayout), (typeof(int).IsValueType ? global::System.Activator.CreateInstance(typeof(int)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (RootLayout)bindable;
            target.privateMarginMinimumSize = (int)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (RootLayout)bindable;
            return target.privateMarginMinimumSize;
        });
        
        /// <summary>
        /// BindableProperty for BackgroundEnabled, it's used as an argument of SetBinding API to bind a value to RootLayout object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty BackgroundEnabledProperty = BindableProperty.Create(nameof(BackgroundEnabled), typeof(bool), typeof(RootLayout), (typeof(bool).IsValueType ? global::System.Activator.CreateInstance(typeof(bool)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (RootLayout)bindable;
            target.privateBackgroundEnabled = (bool)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (RootLayout)bindable;
            return target.privateBackgroundEnabled;
        });
    }
}
