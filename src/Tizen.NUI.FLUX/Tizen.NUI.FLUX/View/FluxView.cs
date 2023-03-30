/// @file FluxView.cs
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// FluxView is the base class for all views in FLUX Application
    /// </summary>
    /// <code>
    /// view = new FluxView();
    /// view.UnitPositionX = 50;
    /// view.UnitPositionY = 50;
    /// view.UnitSizeWidth = 50;
    /// view.UnitSizeHeight = 50;
    /// view.BackgroundColor = Color.Red;
    /// </code>
    public partial class FluxView : View
    {
        /// <summary>
        /// Unit coordinate base View system
        /// </summary>
        public FluxView()
        {
            SecurityUtil.CheckPlatformPrivileges();
            RemovedFromWindow += OnRemovedFromWindow;
        }

        /// <summary>
        /// UI direction changed event handler, user can add/remove
        /// </summary>
        public event EventHandler<DirectionChangedEventArgs> UIDirectionChangedEvent;

        /// <summary>
        /// LayoutParam set, get property
        /// </summary>       
        public LayoutParam LayoutParam
        {
            set => SetValue(LayoutParamProperty, value);
            get => (LayoutParam)GetValue(LayoutParamProperty);
        }

        /// <summary>
        /// Unit size set, get property
        /// </summary>       
        public UnitSize UnitSize
        {
            set
            {
                if (value is UnitSize unitSize)
                {
                    if (IsUnitSizeWidthLocked == false && IsUnitSizeHeightLocked == false)
                    {
                        SetValue(UnitSizeProperty, unitSize);
                    }
                    else
                    {
                        UnitSizeWidth = unitSize.Width;
                        UnitSizeHeight = unitSize.Height;
                    }
                }
            }
            get => (GetValue(UnitSizeProperty) as UnitSize)?.NotifiableClone(SizePropertyChangedHandler);
        }

        /// <summary>
        /// Unit size width set, get property
        /// </summary>
        public int UnitSizeWidth
        {
            set
            {
                if (IsUnitSizeWidthLocked == true)
                {
                    RestrictedModeManager.Instance.NotifyRestrictedOperation("You can't change UnitSizeWidth due to UnitSizeWidthLock");
                }
                else
                {
                    SetValue(UnitSizeWidthProperty, value);
                }
            }
            get => (int)GetValue(UnitSizeWidthProperty);
        }

        /// <summary>
        /// Unit size height set, get property
        /// </summary>
        public int UnitSizeHeight
        {
            set
            {
                if (IsUnitSizeHeightLocked == true)
                {
                    RestrictedModeManager.Instance.NotifyRestrictedOperation("You can't change UnitSizeHeight due to UnitSizeHeightLock");
                }
                else
                {
                    SetValue(UnitSizeHeightProperty, value);
                }
            }
            get => (int)GetValue(UnitSizeHeightProperty);
        }

        /// <summary>
        /// Unit position set, get property
        /// </summary>       
        public UnitPosition UnitPosition
        {
            set
            {
                if (value != null)
                {
                    SetValue(UnitPositionProperty, value);
                }
            }
            get => (GetValue(UnitPositionProperty) as UnitPosition)?.NotifiableClone(PositionPropertyChangedHandler);
        }

        /// <summary>
        /// Unit position X set, get property
        /// </summary>
        public int UnitPositionX
        {
            set => SetValue(UnitPositionXProperty, value);
            get => (int)GetValue(UnitPositionXProperty);
        }

        /// <summary>
        /// Unit position Y set, get property
        /// </summary>
        public int UnitPositionY
        {
            set => SetValue(UnitPositionYProperty, value);
            get => (int)GetValue(UnitPositionYProperty);
        }
        /// <summary>
        /// Unit margin set, get property
        /// </summary>       
        public Extents UnitMargin
        {
            set
            {
                if (value != null)
                {
                    SetValue(UnitMarginProperty, value);
                }
            }
            get => (Extents)GetValue(UnitMarginProperty);
        }

        /// <summary>
        /// Gets or sets the maximum unit size of the fluxview for layout
        /// </summary>       
        public UnitSize MaximumUnitSize
        {
            set => SetValue(MaximumUnitSizeProperty, value);
            get => (UnitSize)GetValue(MaximumUnitSizeProperty);
        }

        /// <summary>
        /// Gets or sets the minimum unit size of the fluxview for layout
        /// </summary>      
        public UnitSize MinimumUnitSize
        {
            set => SetValue(MinimumUnitSizeProperty, value);
            get => (UnitSize)GetValue(MinimumUnitSizeProperty);
        }


        private uint privateBorderWidth
        {
            set
            {
                borderWidth = value;
                UpdateOutline();
            }
            get => borderWidth;
        }

        /// <summary>
        /// Set the Outline width
        /// </summary>
        public uint BorderWidth
        {
            set => SetValue(BorderWidthProperty, value);
            get => (uint)GetValue(BorderWidthProperty);
        }

        private Color privateBorderColor
        {
            set
            {
                borderColor = value;
                UpdateOutline();
            }
            get => borderColor;
        }

        /// <summary>
        /// Set the Border color
        /// </summary>
        public Color BorderColor
        {
            set => SetValue(BorderColorProperty, value);
            get => (Color)GetValue(BorderColorProperty);
        }


        /// <summary>
        /// Inherit UI Direction from parent.
        /// </summary>
        /// <version>10.10.0</version>
        public bool InheritUIDirection
        {
            get => (bool)GetValue(InheritUIDirectionProperty);
            set
            {
                if (value != inheritUIDirection)
                {
                    SetValue(InheritUIDirectionProperty, value);
                    if (inheritUIDirection == true)
                    {
                        if (GetParent() is FluxView parent)
                        {
                            UIDirection = parent.direction;
                        }
                        else
                        {
                            UIDirection = SystemProperty.Instance.UIDirection;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// get/set UI direction
        /// </summary>
        public UIDirection UIDirection
        {
            get => (UIDirection)GetValue(UIDirectionProperty);
            set
            {
                if (value != direction)
                {
                    SetValue(UIDirectionProperty, value);
                    Traverse(this, direction);
                    if (GetParent() is FluxView parent)
                    {
                        OnUIDirectionChange(this, new DirectionChangedEventArgs(parent.direction, direction));
                    }
                    else
                    {
                        //ToDo : Need to check to find the FluxView
                        OnUIDirectionChange(this, new DirectionChangedEventArgs(SystemProperty.Instance.UIDirection, direction));
                    }
                }
            }
        }

        /// <summary>
        /// Method to set the unit position 
        /// </summary>
        /// <param name="x">unit X</param>
        /// <param name="y">unit Y</param>
        /// <version>10.10.0</version>
        public void SetUnitPosition(int x, int y)
        {
            UnitPositionX = x;
            UnitPositionY = y;
        }

        /// <summary>
        /// Method to set the unit size 
        /// </summary>
        /// <param name="width">unit width</param>
        /// <param name="height">unit height</param>
        /// <version>10.10.0</version>
        public void SetUnitSize(int width, int height)
        {
            UnitSizeWidth = width;
            UnitSizeHeight = height;
        }

        private void UpdateUIDirection()
        {
            if (InheritUIDirection == true)
            {
                if (GetParent() is FluxView parent)
                {
                    if (UIDirection != parent.UIDirection)
                    {
                        CLog.Info("[%s1] parentUIDirection: %s2, myDirection: %s3 Update"
                            , s1: GetTypeName()
                            , s2: CLog.EnumToString(parent.UIDirection)
                            , s3: CLog.EnumToString(UIDirection)
                            );
                        UIDirection = parent.UIDirection;
                    }
                }
                else if (GetParent() is Layer layer)
                {
                    if (UIDirection != SystemProperty.Instance.UIDirection)
                    {
                        CLog.Info("[%s1] layerUIDirection: %s2, myDirection: %s3 Update"
                            , s1: GetTypeName()
                            , s2: CLog.EnumToString(SystemProperty.Instance.UIDirection)
                            , s3: CLog.EnumToString(UIDirection)
                            );
                        UIDirection = SystemProperty.Instance.UIDirection;
                    }
                }
            }
        }

        private bool IsRemovedViewFromWindow = false;
        private void OnRemovedFromWindow(object sender, EventArgs e)
        {
            AddedToWindow += OnAddedToWindow;
            IsRemovedViewFromWindow = true;
        }

        private void OnAddedToWindow(object sender, EventArgs e)
        {
            if (IsRemovedViewFromWindow != true)
            {
                return;
            }
            UpdateUIDirection();
            IsRemovedViewFromWindow = false;
            AddedToWindow -= OnAddedToWindow;
        }
        internal bool IsUnitSizeWidthLocked { get; set; } = false;
        internal bool IsUnitSizeHeightLocked { get; set; } = false;
        internal virtual bool IsComponent { get; set; } = false;

        /// <summary>
        /// UI direction changed event argument
        /// </summary>
        /// <code>
        /// FluxView view = new FluxView();
        /// view.UIDirectionChangedEvent += OnUIDirectionChagned;
        /// 
        /// private void OnUIDirectionChagned(object sender, DirectionChangedEventArgs e)
        /// {
        ///    Log.Error(""TV.FLUX.Example","# e.ParentUIDirection: " + e.ParentUIDirection + " e.OwnUIDirection: " + e.OwnUIDirection);
        /// }
        /// </code>
        public class DirectionChangedEventArgs : EventArgs
        {
            /// <summary>
            /// constructor
            /// </summary>
            /// <param name="parentUIDirection"> parent object's UI direction </param>
            /// <param name="ownUIDirection"> it's own UI direction </param>
            public DirectionChangedEventArgs(UIDirection parentUIDirection, UIDirection ownUIDirection)
            {
                ParentUIDirection = parentUIDirection;
                OwnUIDirection = ownUIDirection;
            }
            /// <summary>
            /// get ui direction of parent object
            /// </summary>
            public UIDirection ParentUIDirection { get; }
            /// <summary>
            /// get ui direction of own object
            /// </summary>
            public UIDirection OwnUIDirection { get; }
        }

        internal FluxView(global::System.IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) { }

        internal class SizeChangedEventArgs : EventArgs
        {
        }

        internal event EventHandler<SizeChangedEventArgs> SizeChanged;

        internal class PositionChangedEventArgs : EventArgs
        {
        }

        internal event EventHandler<PositionChangedEventArgs> PositionChanged;

        internal class PreSizeChangedEventArgs : EventArgs
        {
            public int Width { get; }
            public int Height { get; }
            public PreSizeChangedEventArgs(int width, int height)
            {
                Width = width;
                Height = height;
            }
        }

        internal event EventHandler<EventArgs> GeometryChanged;
        internal event EventHandler<PreSizeChangedEventArgs> PreSizeChanged;

        internal void SetProperty(string property, object value)
        {
            GetType().GetProperty(property, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)?.SetValue(this, value);
        }

        internal object GetProperty(string property)
        {
            return GetType().GetProperty(property, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)?.GetValue(this);
        }

        internal void SetProperty(Dictionary<string, object> map)
        {
            foreach (KeyValuePair<string, object> pair in map)
            {
                SetProperty(pair.Key, pair.Value);
            }
        }

        internal Dictionary<string, object> GetProperty(List<string> properties)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            foreach (string p in properties)
            {
                map.Add(p, GetProperty(p));
            }

            return map;
        }

        /// <summary>
        /// Set Color using string property name. 
        /// This is internal method. Don't use this 
        /// </summary>
        /// <param name="property">The target property name.</param>
        /// <param name="color">The color value.</param>
        /// <returns> if success then return true </returns>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool SetColorByPropertyInternal(string property, Color color)
        {
            bool result = false;
            switch (property)
            {
                case "BorderColor":
                    {
                        BorderColor = color;
                        result = true;
                    }
                    break;
                default:
                    break;
            }
            return result;
        }


        internal void SetPropertyBoost(string property, object value)
        {
            SetValue(GetBindableProperty(property), value);
        }

        internal object GetPropertyBoost(string property)
        {
            return GetValue(GetBindableProperty(property));
        }

        internal void SetPropertyBoost(Dictionary<string, object> map)
        {
            foreach (KeyValuePair<string, object> pair in map)
            {
                SetPropertyBoost(pair.Key, pair.Value);
            }
        }

        internal Dictionary<string, object> GetPropertyBoost(List<string> properties)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            foreach (string p in properties)
            {
                map.Add(p, GetPropertyBoost(p));
            }

            return map;
        }

        internal void OnUIDirectionChange(object sender, DirectionChangedEventArgs e)
        {
            UIDirectionChangedEvent?.Invoke(sender, e);
        }

        /// <summary>
        /// Dispose Function to clean up unmanaged resources.
        /// </summary>
        /// <param name="type">Disposing type</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                if (borderRenderer != null)
                {
                    borderRenderer.Dispose();
                    borderRenderer = null;
                }

                RemovedFromWindow -= OnRemovedFromWindow;
                if (IsRemovedViewFromWindow == true)
                {
                    AddedToWindow -= OnAddedToWindow;
                }
            }

            base.Dispose(type);
        }

        /// <summary>
        /// Update BorderRenderer properties
        /// </summary>
        protected virtual void UpdateOutline()
        {
            if ((borderWidth == 0) || (borderColor == null) || (borderColor?.A == 0.0))
            {
                if (isBorderRendererAdded)
                {
                    RemoveRenderer(borderRenderer);
                    isBorderRendererAdded = false;
                }
            }
            else
            {
                if (borderRenderer == null)
                {
                    borderRenderer = new BorderRenderer();
                }
                if (!isBorderRendererAdded)
                {
                    isBorderRendererAdded = true;
                    AddRenderer(borderRenderer);
                }
                if (borderRenderer.BorderColor != borderColor)
                {
                    borderRenderer.BorderColor = borderColor;
                }
                if (borderRenderer.BorderWidth != borderWidth)
                {
                    borderRenderer.BorderWidth = borderWidth;
                }
            }
        }

        private BorderRenderer borderRenderer = null;
        private bool isBorderRendererAdded = false;
        private uint borderWidth = 0;
        private Color borderColor = null;
        private LayoutParam layoutParam;
        private UnitSize maximumUnitSize, minimumUniSize;
        private UIDirection direction = SystemProperty.Instance.UIDirection;
        private bool inheritUIDirection = true;

        private void EmitPositionChangedEvent()
        {
            PositionChanged?.Invoke(this, new PositionChangedEventArgs());
            EmitGeometryChangedEvent();
        }

        private void EmitSizeChangedEvent()
        {
            SizeChanged?.Invoke(this, new SizeChangedEventArgs());
            EmitGeometryChangedEvent();
        }

        private void EmitPreSizeChangedEvent(int width, int height)
        {
            PreSizeChanged?.Invoke(this, new PreSizeChangedEventArgs(width, height));
        }

        private void EmitGeometryChangedEvent()
        {
            GeometryChanged?.Invoke(this, new EventArgs());
        }

        private Tizen.NUI.Binding.BindableProperty GetBindableProperty(string property)
        {
            return property switch
            {
                "Scale" => ScaleProperty,
                "ScaleX" => ScaleXProperty,
                "ScaleY" => ScaleYProperty,
                "Name" => NameProperty,
                "Position" => PositionProperty,
                "PositionX" => PositionXProperty,
                "PositionY" => PositionYProperty,
                "Position2D" => Position2DProperty,
                "Size" => SizeProperty,
                "SizeHeight" => SizeHeightProperty,
                "SizeWidth" => SizeWidthProperty,
                "Size2D" => Size2DProperty,
                "BackgroundColor" => BackgroundColorProperty,
                "BackgroundImage" => BackgroundImageProperty,
                "Opacity" => OpacityProperty,
                _ => null
            };
        }

        private void Traverse(View view, UIDirection parentDirection)
        {
            if (view == null)
            {
                return;
            }

            for (uint index = 0; index < view.ChildCount; index++)
            {
                View child = view.GetChildAt(index);
                if (child is FluxView fluxView)
                {
                    if (fluxView.InheritUIDirection == true)
                    {
                        fluxView.UIDirection = parentDirection;
                    }
                }
                else
                {
                    Traverse(child, parentDirection);
                }
            }
        }

        private void SizePropertyChangedHandler(UnitSize size)
        {
            UnitSize = size;
        }

        private void PositionPropertyChangedHandler(UnitPosition position)
        {
            UnitPosition = position;
        }
    }
}