/**
 *Copyright (c) 2022 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file ComponentBase.BindableProperty.cs
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
    /// BindableProperty of ComponentBase.
    /// </summary>
    /// <code>
    /// Refer to Component class.
    /// </code>
    public partial class ComponentBase : FluxView
    {

        /// <summary>
        /// BindableProperty for LayoutParam, it's used as an argument of SetBinding API to bind a value to ComponentBase object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static new readonly BindableProperty LayoutParamProperty = BindableProperty.Create(nameof(LayoutParam), typeof(LayoutItemParam), typeof(ComponentBase), (typeof(LayoutItemParam).IsValueType ? global::System.Activator.CreateInstance(typeof(LayoutItemParam)) : null),
        propertyChanged: (bindable, oldValue, newValue) =>
       {
           var target = (ComponentBase)bindable;
           target.privateLayoutParam = (LayoutItemParam)newValue;
       },
        defaultValueCreator: (bindable) =>
        {
            var target = (ComponentBase)bindable;
            return target.privateLayoutParam;
        });

        /// <summary>
        /// BindableProperty for EnablePropagateState, it's used as an argument of SetBinding API to bind a value to ComponentBase object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty EnablePropagateStateProperty = BindableProperty.Create(nameof(EnablePropagateState), typeof(bool), typeof(ComponentBase), (typeof(bool).IsValueType ? global::System.Activator.CreateInstance(typeof(bool)) : null),
        propertyChanged: (bindable, oldValue, newValue) =>
       {
           var target = (ComponentBase)bindable;
           target.privateEnablePropagateState = (bool)newValue;
       },
        defaultValueCreator: (bindable) =>
        {
            var target = (ComponentBase)bindable;
            return target.privateEnablePropagateState;
        });

        /// <summary>
        /// BindableProperty for KeepHeightByRatio, it's used as an argument of SetBinding API to bind a value to ComponentBase object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty KeepHeightByRatioProperty = BindableProperty.Create(nameof(KeepHeightByRatio), typeof(bool), typeof(ComponentBase), (typeof(bool).IsValueType ? global::System.Activator.CreateInstance(typeof(bool)) : null),
        propertyChanged: (bindable, oldValue, newValue) =>
       {
           var target = (ComponentBase)bindable;
           target.privateKeepHeightByRatio = (bool)newValue;
       },
        defaultValueCreator: (bindable) =>
        {
            var target = (ComponentBase)bindable;
            return target.privateKeepHeightByRatio;
        });

        /// <summary>
        /// BindableProperty for KeepWidthByRatio, it's used as an argument of SetBinding API to bind a value to ComponentBase object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty KeepWidthByRatioProperty = BindableProperty.Create(nameof(KeepWidthByRatio), typeof(bool), typeof(ComponentBase), (typeof(bool).IsValueType ? global::System.Activator.CreateInstance(typeof(bool)) : null),
        propertyChanged: (bindable, oldValue, newValue) =>
       {
           var target = (ComponentBase)bindable;
           target.privateKeepWidthByRatio = (bool)newValue;
       },
        defaultValueCreator: (bindable) =>
        {
            var target = (ComponentBase)bindable;
            return target.privateKeepWidthByRatio;
        });

        /// <summary>
        /// BindableProperty for NeedUpdateLayout, it's used as an argument of SetBinding API to bind a value to ComponentBase object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty NeedUpdateLayoutProperty = BindableProperty.Create(nameof(NeedUpdateLayout), typeof(bool), typeof(ComponentBase), (typeof(bool).IsValueType ? global::System.Activator.CreateInstance(typeof(bool)) : null),
        propertyChanged: (bindable, oldValue, newValue) =>
       {
           var target = (ComponentBase)bindable;
           target.privateNeedUpdateLayout = (bool)newValue;
       },
        defaultValueCreator: (bindable) =>
        {
            var target = (ComponentBase)bindable;
            return target.privateNeedUpdateLayout;
        });
    }
}
