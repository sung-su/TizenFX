/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file Component.BindableProperty.cs
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
    /// Component is base class of flux components.
    /// </summary>
    /// <version> 6.6.0 </version>
    /// <code>
    /// Component component = new Component();
    /// component.UnitSize = new UnitSize(10,10);
    /// </code>
    public partial class Component : ComponentBase, IStatable
    {
        
        /// <summary>
        /// BindableProperty for State, it's used as an argument of SetBinding API to bind a value to Component object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public new static readonly BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(StateMachine), typeof(Component), (typeof(StateMachine).IsValueType ? global::System.Activator.CreateInstance(typeof(StateMachine)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (Component)bindable;
            target.privateState = (StateMachine)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (Component)bindable;
            return target.privateState;
        });
        
        /// <summary>
        /// BindableProperty for Disabled, it's used as an argument of SetBinding API to bind a value to Component object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty DisabledProperty = BindableProperty.Create(nameof(Disabled), typeof(bool), typeof(Component), (typeof(bool).IsValueType ? global::System.Activator.CreateInstance(typeof(bool)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (Component)bindable;
            target.privateDisabled = (bool)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (Component)bindable;
            return target.privateDisabled;
        });
        
        /// <summary>
        /// BindableProperty for Selected, it's used as an argument of SetBinding API to bind a value to Component object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty SelectedProperty = BindableProperty.Create(nameof(Selected), typeof(bool), typeof(Component), (typeof(bool).IsValueType ? global::System.Activator.CreateInstance(typeof(bool)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (Component)bindable;
            target.privateSelected = (bool)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (Component)bindable;
            return target.privateSelected;
        });
        
        /// <summary>
        /// BindableProperty for Checked, it's used as an argument of SetBinding API to bind a value to Component object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty CheckedProperty = BindableProperty.Create(nameof(Checked), typeof(bool), typeof(Component), (typeof(bool).IsValueType ? global::System.Activator.CreateInstance(typeof(bool)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (Component)bindable;
            target.privateChecked = (bool)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (Component)bindable;
            return target.privateChecked;
        });
        
        /// <summary>
        /// BindableProperty for IsItemForFastScroll, it's used as an argument of SetBinding API to bind a value to Component object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty IsItemForFastScrollProperty = BindableProperty.Create(nameof(IsItemForFastScroll), typeof(bool), typeof(Component), (typeof(bool).IsValueType ? global::System.Activator.CreateInstance(typeof(bool)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (Component)bindable;
            target.privateIsItemForFastScroll = (bool)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (Component)bindable;
            return target.privateIsItemForFastScroll;
        });
        
        /// <summary>
        /// BindableProperty for DefaultFocusMotionEnabled, it's used as an argument of SetBinding API to bind a value to Component object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty DefaultFocusMotionEnabledProperty = BindableProperty.Create(nameof(DefaultFocusMotionEnabled), typeof(bool), typeof(Component), (typeof(bool).IsValueType ? global::System.Activator.CreateInstance(typeof(bool)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (Component)bindable;
            target.privateDefaultFocusMotionEnabled = (bool)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (Component)bindable;
            return target.privateDefaultFocusMotionEnabled;
        });
        
        /// <summary>
        /// BindableProperty for DefaultSelectMotionEnabled, it's used as an argument of SetBinding API to bind a value to Component object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty DefaultSelectMotionEnabledProperty = BindableProperty.Create(nameof(DefaultSelectMotionEnabled), typeof(bool), typeof(Component), (typeof(bool).IsValueType ? global::System.Activator.CreateInstance(typeof(bool)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (Component)bindable;
            target.privateDefaultSelectMotionEnabled = (bool)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (Component)bindable;
            return target.privateDefaultSelectMotionEnabled;
        });
        
        /// <summary>
        /// BindableProperty for SendKeyInsteadExecute, it's used as an argument of SetBinding API to bind a value to Component object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty SendKeyInsteadExecuteProperty = BindableProperty.Create(nameof(SendKeyInsteadExecute), typeof(bool), typeof(Component), (typeof(bool).IsValueType ? global::System.Activator.CreateInstance(typeof(bool)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (Component)bindable;
            target.privateSendKeyInsteadExecute = (bool)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (Component)bindable;
            return target.privateSendKeyInsteadExecute;
        });
        
        /// <summary>
        /// BindableProperty for ThemeColor, it's used as an argument of SetBinding API to bind a value to Component object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty ThemeColorProperty = BindableProperty.Create(nameof(ThemeColor), typeof(ThemeColor), typeof(Component), (typeof(ThemeColor).IsValueType ? global::System.Activator.CreateInstance(typeof(ThemeColor)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (Component)bindable;
            target.privateThemeColor = (ThemeColor)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (Component)bindable;
            return target.privateThemeColor;
        });
        
        /// <summary>
        /// BindableProperty for PointingBehavior, it's used as an argument of SetBinding API to bind a value to Component object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 10.10.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static readonly BindableProperty PointingBehaviorProperty = BindableProperty.Create(nameof(PointingBehavior), typeof(PointingBehaviorMode), typeof(Component), (typeof(PointingBehaviorMode).IsValueType ? global::System.Activator.CreateInstance(typeof(PointingBehaviorMode)) : null), 
        propertyChanged : (bindable, oldValue, newValue) =>
        {
            var target = (Component)bindable;
            target.privatePointingBehavior = (PointingBehaviorMode)newValue;
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (Component)bindable;
            return target.privatePointingBehavior;
        });
        
    }
}
