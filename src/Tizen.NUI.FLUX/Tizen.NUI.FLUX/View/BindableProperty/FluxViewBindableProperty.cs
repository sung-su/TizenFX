/// @file FluxViewBindableProperty.cs
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

using System.ComponentModel;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// FluxView is the base class for all views in FLUX Application
    /// </summary>
    /// <code>
    /// view = new FluxView();
    /// view.UnitPosition = new UnitPosition(50, 50);
    /// view.UnitSizeWidth = 50;
    /// view.UnitSizeHeight = 50;
    /// view.BackgroundColor = Color.Red;
    /// </code>
    public partial class FluxView : View
    {
        /// <summary>
        /// BindablePoperty for LayoutParam, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty LayoutParamProperty = Tizen.NUI.Binding.BindableProperty.Create("LayoutParam", typeof(LayoutParam), typeof(FluxView), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.layoutParam = (LayoutParam)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return target.layoutParam;
        });

        /// <summary>
        /// BindablePoperty for UnitSize, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty UnitSizeProperty = Tizen.NUI.Binding.BindableProperty.Create("UnitSize", typeof(UnitSize), typeof(FluxView), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.SizeWidth = DisplayMetrics.Instance.UnitToPixel(((UnitSize)newValue).Width);
                target.SizeHeight = DisplayMetrics.Instance.UnitToPixel(((UnitSize)newValue).Height);
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return new UnitSize(DisplayMetrics.Instance.PixelToUnit(target.Size2D.Width), DisplayMetrics.Instance.PixelToUnit(target.Size2D.Height));
        });

        /// <summary>
        /// BindablePoperty for UnitSizeWidth, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty UnitSizeWidthProperty = Tizen.NUI.Binding.BindableProperty.Create("UnitSizeWidth", typeof(int), typeof(FluxView), default(int), propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.SizeWidth = DisplayMetrics.Instance.UnitToPixel((int)newValue);
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return DisplayMetrics.Instance.PixelToUnit((int)target.SizeWidth);
        });

        /// <summary>
        /// BindablePoperty for UnitSizeHeight, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty UnitSizeHeightProperty = Tizen.NUI.Binding.BindableProperty.Create("UnitSizeHeight", typeof(int), typeof(FluxView), default(int), propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.SizeHeight = DisplayMetrics.Instance.UnitToPixel((int)newValue);
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return DisplayMetrics.Instance.PixelToUnit((int)target.SizeHeight);
        });

        /// <summary>
        /// BindablePoperty for UnitPosition, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty UnitPositionProperty = Tizen.NUI.Binding.BindableProperty.Create("UnitPosition", typeof(UnitPosition), typeof(FluxView), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.PositionX = DisplayMetrics.Instance.UnitToPixel(((UnitPosition)newValue).X);
                target.PositionY = DisplayMetrics.Instance.UnitToPixel(((UnitPosition)newValue).Y);
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return new UnitPosition(DisplayMetrics.Instance.PixelToUnit(target.Position2D.X), DisplayMetrics.Instance.PixelToUnit(target.Position2D.Y));
        });

        /// <summary>
        /// BindablePoperty for UnitPositionX, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty UnitPositionXProperty = Tizen.NUI.Binding.BindableProperty.Create("UnitPositionX", typeof(int), typeof(FluxView), default(int), propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.PositionX = DisplayMetrics.Instance.UnitToPixel((int)newValue);
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return DisplayMetrics.Instance.PixelToUnit((int)target.PositionX);
        });

        /// <summary>
        /// BindablePoperty for UnitPositionY, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty UnitPositionYProperty = Tizen.NUI.Binding.BindableProperty.Create("UnitPositionY", typeof(int), typeof(FluxView), default(int), propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.PositionY = DisplayMetrics.Instance.UnitToPixel((int)newValue);
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return DisplayMetrics.Instance.PixelToUnit((int)target.PositionY);
        });

        /// <summary>
        /// BindablePoperty for UnitMargin, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty UnitMarginProperty = Tizen.NUI.Binding.BindableProperty.Create("UnitMargin", typeof(Extents), typeof(FluxView), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.Margin = new Extents((ushort)DisplayMetrics.Instance.UnitToPixel(((Extents)newValue).Start), (ushort)DisplayMetrics.Instance.UnitToPixel(((Extents)newValue).End), (ushort)DisplayMetrics.Instance.UnitToPixel(((Extents)newValue).Top), (ushort)DisplayMetrics.Instance.UnitToPixel(((Extents)newValue).Bottom));
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return new Extents((ushort)DisplayMetrics.Instance.PixelToUnit(target.Margin.Start), (ushort)DisplayMetrics.Instance.PixelToUnit(target.Margin.End), (ushort)DisplayMetrics.Instance.PixelToUnit(target.Margin.Top), (ushort)DisplayMetrics.Instance.PixelToUnit(target.Margin.Bottom));
        });

        /// <summary>
        /// BindablePoperty for MaximumUnitSize, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty MaximumUnitSizeProperty = Tizen.NUI.Binding.BindableProperty.Create("MaximumUnitSize", typeof(UnitSize), typeof(FluxView), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.maximumUnitSize = (UnitSize)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return target.maximumUnitSize;
        });

        /// <summary>
        /// BindablePoperty for MinimumUnitSize, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty MinimumUnitSizeProperty = Tizen.NUI.Binding.BindableProperty.Create("MinimumUnitSize", typeof(UnitSize), typeof(FluxView), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.minimumUniSize = (UnitSize)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return target.minimumUniSize;
        });

        /// <summary>
        /// BindablePoperty for BorderWith, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty BorderWidthProperty = Tizen.NUI.Binding.BindableProperty.Create("BorderWidth", typeof(uint), typeof(FluxView), default(uint), propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.privateBorderWidth = (uint)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return target.privateBorderWidth;
        });

        /// <summary>
        /// BindablePoperty for BorderColor, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty BorderColorProperty = Tizen.NUI.Binding.BindableProperty.Create("BorderColor", typeof(Color), typeof(FluxView), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.privateBorderColor = (Color)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return target.privateBorderColor;
        });

        /// <summary>
        /// BindablePoperty for UIDirection, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty UIDirectionProperty = Tizen.NUI.Binding.BindableProperty.Create("UIDirection", typeof(UIDirection), typeof(FluxView), SystemProperty.Instance.UIDirection, propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.direction = (UIDirection)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return target.direction;
        });


        /// <summary>
        /// BindablePoperty for UIDirection, it's used as an argument of SetBinding API to bind a value to FluxView object.
        /// This property need to be hidden as inhouse API.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly Tizen.NUI.Binding.BindableProperty InheritUIDirectionProperty = Tizen.NUI.Binding.BindableProperty.Create("InheritUIDirection", typeof(bool), typeof(FluxView), true, propertyChanged: (bindable, oldValue, newValue) =>
        {
            FluxView target = (FluxView)bindable;
            if (newValue != null)
            {
                target.inheritUIDirection = (bool)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            FluxView target = (FluxView)bindable;
            return target.inheritUIDirection;
        });
    }
}