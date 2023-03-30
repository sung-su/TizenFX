/// @file VideoCanvasViewBindableProperty.cs
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
using Tizen.NUI.Binding;

namespace Tizen.NUI.FLUX
{

    /// <summary>
    /// This is VideoCanvasView which enables user to drill a transparent hole in window. 
    /// </summary>
    /// <code>
    /// videocanvasView = new VideoCanvasView();
    /// videocanvasView.Size2D = new Size2D(400, 300);
    /// videocanvasView.Position = new Position(100, 100, 0);    
    /// Window.Instance.GetDefaultLayer().Add(canvasView);    
    /// </code>
    public partial class VideoCanvasView : FluxView
    {

        /// <summary>
        /// BindableProperty for CornerRadius, it's used as an argument of SetBinding API to bind a value to VideoCanvasView object
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>
        /// 9.9.0
        /// </version>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
#pragma warning disable CS0108
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(Vector4), typeof(VideoCanvasView), (typeof(Vector4).IsValueType ? Activator.CreateInstance(typeof(Vector4)) : null),
#pragma warning restore CS0108
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var target = (VideoCanvasView)bindable;
            if (newValue != null)
            {
               target.privateCornerRadius = (Vector4)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var target = (VideoCanvasView)bindable;
            return target.privateCornerRadius;
        });
    }
}
