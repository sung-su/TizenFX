/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file RootLayout.cs
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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// This class is RootLayout of Layout.
    /// </summary>
    /// <code>
    /// RootLayout root = new RootLayout();
    /// root.Add(button);
    /// root.Add(textBox);
    /// root.UpdateLayout();
    /// </code>
    public partial class RootLayout : Layout
    {
        #region public Method

        /// <summary>
        /// Constructor to instantiate the RootLayout class.
        /// </summary>
        /// <param name="layer"> The layer that want to add this </param>
        public RootLayout(Layer layer)
        {
            Initialize(layer, LayoutTypes.FlexV, null);
        }

        /// <summary>
        /// Constructor to instantiate the RootLayout class.
        /// </summary>
        /// <param name="layer"> The layer that want to add this  </param>
        /// <param name="type"> The Layout's Type enum value </param>
        public RootLayout(Layer layer, LayoutTypes type)
        {
            Initialize(layer, type, null);
        }

        /// <summary>
        /// Constructor to instantiate the RootLayout class.
        /// </summary>
        /// <param name="layer"> The layer that want to add this </param>
        /// <param name="type"> The Layout's Type enum value  </param>
        /// <param name="enableDefaultBackground"> Enable / Disable default background image of principle.</param>
        public RootLayout(Layer layer, LayoutTypes type, bool enableDefaultBackground)
        {
            enableBackground = enableDefaultBackground;
            Initialize(layer, type, null);
        }

        /// <summary>
        /// Constructor to instantiate the RootLayout class.        
        /// </summary>
        /// <param name="layer"> The layer that want to add this </param>
        /// <param name="background"> User can choose TileBackground or CropBackground as BG of RootLayout</param>
        /// <param name="type"> The Layout's Type enum value </param>
        /// <code>
        /// RootLayout rootLayout = new RootLayout(Window.Instance.GetDefaultLayer(), new CropBackground("oobe_bg.webp")); 
        /// </code>
        /// <version> 8.8.0 </version>
        public RootLayout(Layer layer, FluxBackground background, LayoutTypes type = LayoutTypes.FlexV)
        {
            if (background == null)
            {
                enableBackground = true;
            }
            else
            {
                fluxBG = background;
            }

            Initialize(layer, type, null);
        }

        /// <summary>
        /// Constructor to instantiate the RootLayout class.        
        /// </summary>
        /// <param name="layer"> The layer that want to add this </param>
        /// <param name="size"> The fixed size of this layout </param>
        /// <param name="type"> The Layout's Type enum value </param>
        /// <param name="enableDefaultBackground">Default value is false</param>
        public RootLayout(Layer layer, UnitSize size, LayoutTypes type = LayoutTypes.FlexV, bool enableDefaultBackground = false)
        {
            enableBackground = enableDefaultBackground;
            Initialize(layer, type, size);
        }

        /// <summary>
        /// Constructor to instantiate the RootLayout class.        
        /// </summary>
        /// <param name="layer"> The layer that want to add this </param>
        /// <param name="size"> The fixed size of this layout </param>
        /// <param name="background">User can choose TileBackground or CropBackground as BG of RootLayout</param>
        /// <param name="type"> The Layout's Type enum value </param>
        /// <code>
        /// RootLayout rootLayout = new RootLayout(Window.Instance.GetDefaultLayer(), new UnitSize(1920,1080) , new CropBackground("oobe_bg.webp")); 
        /// </code>
        /// <version> 8.8.0 </version>
        public RootLayout(Layer layer, UnitSize size, FluxBackground background, LayoutTypes type = LayoutTypes.FlexV)
        {
            if (background == null)
            {
                enableBackground = true;
            }
            else
            {
                fluxBG = background;
            }

            Initialize(layer, type, size);
        }
        /// <summary>
        /// Function to update the layout.
        /// </summary>
        public override void UpdateLayout()
        {
            FluxLogger.InfoP("UpdateLayout CALL [[%s1]]", s1: updateLayoutCallType.Types.ToString());
            GridUpdate();
            UpdateDefaultItemGapinXaml();

            if (fluxBG != null)
            {
                fluxBG.ProcessDrawing(SizeWidth, SizeHeight);
            }
            PolicyManager.Instance.Margin.X = LayoutParam.Margin.LeftMargin;
            PolicyManager.Instance.Margin.Y = LayoutParam.Margin.RightMargin;
            base.UpdateLayout();

            updateLayoutFinished?.Invoke(this, updateLayoutCallType);
            updateLayoutCallType.RootLayoutSize = new UnitSize(UnitSize.Width, UnitSize.Height);
            updateLayoutCallType.Types = UpdateLayoutFinishedTypes.ManualCallByUser; // default value.
        }

        /// <summary>
        /// This Enum value is Event information Type. 
        /// user is able to know why called "updatelayout" from type.
        /// </summary>
        /// <code>
        /// updateLayoutCallType.Types = UpdateLayoutFinishedTypes.ManualCallByUser;
        /// </code>
        /// <version> 8.8.0 </version>
        public enum UpdateLayoutFinishedTypes
        {
            /// <summary>
            /// user call "UpdateLayout"
            /// </summary>
            ManualCallByUser,
            /// <summary>
            /// window Size Change.
            /// </summary>
            WindowSizeChange,
            /// <summary>
            /// UIDirection Changes. (RTL,LTR)
            /// </summary>
            UIDirectionChange,
        }
        #endregion public Method

        #region public Propertys

        /// <summary>
        ///  This class is Rootlayout's property class. RootLayout Only Property
        /// </summary>
        public new RootLayoutParam LayoutParam
        {
            set => SetValue(LayoutParamProperty, value);

            get => (RootLayoutParam)GetValue(LayoutParamProperty);
        }
        /// <summary>
        /// this property set marign minimum value of grid.
        /// </summary>
        public int MarginMinimumSize
        {
            set => SetValue(MarginMinimumSizeProperty, value);

            get => (int)GetValue(MarginMinimumSizeProperty);
        }
        /// <summary>
        /// If true, RootLayout's background will be shown. default value is true
        /// </summary>
        /// <version> 8.8.0 </version>
        public bool BackgroundEnabled
        {
            get => (bool)GetValue(BackgroundEnabledProperty);

            set => SetValue(BackgroundEnabledProperty, value);
        }

        /// <summary>
        /// this event information is used When angle changed in rootlayout.
        /// <code>
        /// AngleChangeEventArgs changedEvent = new AngleChangeEventArgs();
        /// </code>
        /// </summary>
        public class AngleChangeEventArgs : EventArgs
        {
            /// <summary>
            /// for RootLayout`s Size 
            /// </summary>
            public UnitSize RotatedUnitSize;
            /// <summary>
            /// for RootLayout`s angle
            /// </summary>
            public int Angle;
        }

        /// <summary>
        /// this event information is used When updatedlayout finished in rootlayout.
        /// <code>
        /// UpdateLayoutFinishedEventArgs updatedEvent = new UpdateLayoutFinishedEventArgs();
        /// </code>
        /// </summary>
        /// <version> 8.8.0 </version>
        public class UpdateLayoutFinishedEventArgs : EventArgs
        {
            /// <summary>
            /// RootLayout`s Size.
            /// </summary>
            public UnitSize RootLayoutSize;
            /// <summary>
            /// Called Type information.
            /// </summary>
            public UpdateLayoutFinishedTypes Types;
        }

        /// <summary>
        /// This event is used When angle changed in rootlayout. 
        /// <code>
        /// rootlayout.AngleChangeEventHandler +=  RootLayoutAngleChange;
        /// </code>
        /// </summary>
        public event EventHandler<AngleChangeEventArgs> AngleChangeEventHandler
        {
            add
            {
                rootLayoutAngleChange += value;
            }
            remove
            {
                rootLayoutAngleChange -= value;
            }
        }

        /// <summary>
        /// This event is called When called after updatedlayout finished in rootlayout.
        /// <code>
        /// rootlayout.UpdateLayoutFinishedEventArgs +=  UpdateLayoutFinished;
        /// </code>
        /// </summary>
        /// <version> 8.8.0 </version>
        public event EventHandler<UpdateLayoutFinishedEventArgs> UpdateLayoutFinished
        {
            add
            {
                updateLayoutFinished += value;
            }
            remove
            {
                updateLayoutFinished -= value;
            }
        }

        /// <summary>
        ///  Handler for portrait of window size ( with/height ratio  1.0f ) 
        /// <code>
        /// rootlayout.PortraitHandler += DoPortrait;
        /// </code>
        /// </summary>
        /// <version> 9.9.0 </version>
        public event EventHandler<Window.ResizedEventArgs> PortraitHandler
        {
            add
            {
                portraitHandler += value;
            }
            remove
            {
                portraitHandler -= value;
            }
        }

        /// <summary>
        ///  Handler for landscape of window size ( with/height ratio over 1.0f ) 
        /// <code>
        /// rootlayout.PortraitHandler += LandscapeHandler;
        /// </code>
        /// </summary>
        /// <version> 9.9.0 </version>
        public event EventHandler<Window.ResizedEventArgs> LandscapeHandler
        {
            add
            {
                landscapeHandler += value;
            }
            remove
            {
                landscapeHandler -= value;
            }
        }

        #endregion public Property

        #region private Property
        private RootLayoutParam privateLayoutParam
        {
            set
            {
                if (value is RootLayoutParam)
                {
                    base.LayoutParam = value;
                }
                else
                {
                    //TODO handle
                }
            }
            get => base.LayoutParam as RootLayoutParam;
        }

        private int privateMarginMinimumSize
        {
            set
            {
                grid.MarginMinSize = value;
                GridUpdate();
            }

            get => grid.MarginMinSize;
        }
        private bool privateBackgroundEnabled
        {
            get => enableBackground;
            set
            {
                enableBackground = value;
                if (enableBackground == true)
                {
                    if (fluxBG == null)
                    {
                        fluxBG = new FluxBackground((int)SizeWidth, (int)SizeHeight);
                    }

                    if (fluxBG != null)
                    {
                        BackgroundView = fluxBG;
                        Attach(fluxBG);
                        fluxBG.LowerToBottom();
                        fluxBG.ProcessDrawing(SizeWidth, SizeHeight);
                    }
                }
                else
                {
                    if (fluxBG != null)
                    {
                        Detach(fluxBG);
                    }
                }
            }
        }
        #endregion private Property

        #region private Method

        /// <summary>
        /// This method is called HighContrast state changed.
        /// Derived classes should override this if they wish to customize to handle the highContrast event.
        /// </summary>
        /// <param name="sender">The object who send this event</param>
        /// <param name="e">Event argument</param>
        protected override void OnHighContrastChanged(object sender, EventArgs e)
        {
            if (fluxBG != null)
            {
                fluxBG.ProcessDrawing(SizeWidth, SizeHeight);
            }
        }

        /// <summary>
        /// Dispose to instantiate the RootLayout class.        
        /// </summary>
        /// <param name="type">Type of Dispose</param>
        protected override void Dispose(DisposeTypes type) //TODO FIX
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.
                //this.SizeChanged -= RootLayout_SizeChanged;
                Window.Instance.Resized -= WindowResized;
                portraitHandler = null;
                landscapeHandler = null;
                grid.ChangeDelegate -= ChangeColumn;
                DestroyUtility.DestroyView(ref grid);
                windowLayer?.Remove(this);
                windowLayer = null;

                DestroyUtility.DestroyView(ref fluxBG);
                BackgroundView = null;
                //Release your own unmanaged resources here.
                //You should not access any managed member here except static instance.
                //because the execution order of Finalizes is non-deterministic.
                //Unreference this from if a static instance refer to this. 

                //You must call base.Dispose(type) just before exit.
                LayoutParam = null;
            }
            base.Dispose(type);
        }


        private void Initialize(Layer layer, LayoutTypes type, UnitSize size)
        {
            useWindowSize = (size == null) ? true : false;

            LayoutParam = new RootLayoutParam
            {
                type = type
            };
            UnitPositionX = 0;
            UnitPositionY = 0;
            if (layer == null)
            {
                FluxLogger.ErrorP("layer is null");
                return;
            }
            windowLayer = layer;

            CalculateUIArea(size, useWindowSize);
            windowLayer.Add(this);

            if (enableBackground == true && fluxBG == null)
            {
                fluxBG = new FluxBackground((int)SizeWidth, (int)SizeHeight);
            }

            if (fluxBG != null)
            {
                BackgroundView = fluxBG;
                Attach(fluxBG);
            }

            grid.ChangeDelegate += ChangeColumn;
            grid.Mode = PrintColomnModes.none;

            LayoutParam.isRootLayout = true;
            PolicyManager.Instance.UIAreaUnitSize = new UnitSize(UnitSize.Width, UnitSize.Height);

            updateLayoutCallType.RootLayoutSize = new UnitSize(UnitSize.Width, UnitSize.Height);
            updateLayoutCallType.Types = UpdateLayoutFinishedTypes.ManualCallByUser;

            Window.Instance.Resized += WindowResized;
        }

        private void WindowResized(object sender, Window.ResizedEventArgs e)
        {
            if (HasBody() == false)
            {
                return;
            }

            bool needUpdate = false;
            bool windowAngleChanged = false;
            if (sender is Window win)
            {
                Window.WindowOrientation curOrientation = win.GetCurrentOrientation();
                if (prevWindowOrientation != curOrientation)
                {
                    prevWindowOrientation = curOrientation;
                    windowAngleChanged = true;
                }
            }

            FluxLogger.InfoP("Window Resized [ [%d1] x [%d2] ] -> [ [%d3] x [%d4] ] | Angle Changed [%s1]", d1: preWidth
                                                                                                          , d2: preHeight
                                                                                                          , d3: e.WindowSize.Width
                                                                                                          , d4: e.WindowSize.Height
                                                                                                          , s1: windowAngleChanged.ToString());

            if (e.WindowSize.Width != preWidth || e.WindowSize.Height != preHeight || windowAngleChanged)
            {
                needUpdate = EmitAngleChangeEvent(e.WindowSize.Width, e.WindowSize.Height);

                if (useWindowSize == true)
                {
                    UnitSize.Width = DisplayMetrics.Instance.PixelToUnit(e.WindowSize.Width);
                    UnitSize.Height = DisplayMetrics.Instance.PixelToUnit(e.WindowSize.Height);
                }
                else
                {
                    UnitSize.Width = UnitSize.Width;
                    UnitSize.Height = UnitSize.Height;
                }

                GridUpdate();  //marign Update
                preWidth = e.WindowSize.Width;
                preHeight = e.WindowSize.Height;

                if (e.WindowSize.Width <= e.WindowSize.Height)
                {
                    FluxLogger.InfoP("PortraitHandler Invoke [ [%d1] x [%d2] ]", d1: e.WindowSize.Width, d2: e.WindowSize.Height);
                    portraitHandler?.Invoke(sender, e);
                }
                else
                {
                    FluxLogger.InfoP("LandscapeHandler Invoke [ [%d1] x [%d2] ]", d1: e.WindowSize.Width, d2: e.WindowSize.Height);
                    landscapeHandler?.Invoke(sender, e);
                }
            }

            //TODO: add check logic that really need updated.
            if (needUpdate == true)
            {
                PolicyManager.Instance.IsChangingSize = true;
                updateLayoutCallType.RootLayoutSize = new UnitSize(UnitSize.Width, UnitSize.Height);
                updateLayoutCallType.Types = UpdateLayoutFinishedTypes.WindowSizeChange;
                UpdateLayout();
                PolicyManager.Instance.IsChangingSize = false;
            }
        }

        private void UpdateDefaultItemGapinXaml()
        {
            if (IsCreateByXaml == true && IsFirstUpdateLayout == true)
            {
                LayoutParam.ItemGap = 0;
                IsFirstUpdateLayout = false;
            }
        }

        private bool EmitAngleChangeEvent(int width, int height)
        {
            int curAngle = GetCurrentAngle();
            AngleChangeEventArgs data = new AngleChangeEventArgs
            {
                Angle = curAngle,
                RotatedUnitSize = new UnitSize(DisplayMetrics.Instance.PixelToUnit(width), DisplayMetrics.Instance.PixelToUnit(height))
            };
            rootLayoutAngleChange?.Invoke(this, data);
            preAngle = curAngle;

            return true;
        }


        private void ChangeColumn(int culcnt, int marginsize, int width)
        {
            if (LayoutParam is RootLayoutParam)
            {
                LayoutParam.Margin.RightMargin = LayoutParam.Margin.LeftMargin = marginsize;
            }
        }

        private void CalculateUIArea(UnitSize size, bool useDefaultSize = true)
        {
            Size2D windowSize = Window.Instance.Size;
            if ((useDefaultSize == false && size == null) || windowSize == null)
            {
                return;
            }

            //TODO: calculate UI Area with formula
            if (useDefaultSize)
            {
                fovPosition.X = 0;
                fovPosition.Y = 0;
                fovSize.Width = DisplayMetrics.Instance.PixelToUnit(windowSize.Width);
                fovSize.Height = DisplayMetrics.Instance.PixelToUnit(windowSize.Height);
            }
            else
            {
                fovSize.Width = size.Width;
                fovSize.Height = size.Height;

                if (windowSize.Width < fovSize.Width)
                {
                    fovPosition.X = 0;
                }
                else
                {
                    fovPosition.X = (DisplayMetrics.Instance.PixelToUnit(windowSize.Width) - fovSize.Width) / 2;
                }

                if (windowSize.Height < fovSize.Height)
                {
                    fovPosition.Y = 0;
                }
                else
                {
                    fovPosition.Y = (DisplayMetrics.Instance.PixelToUnit(windowSize.Height) - fovSize.Height) / 2;
                }
            }

            if (fovSize.Width == 0 && fovSize.Height == 0)
            {
                fovSize.Width = 240;
                fovSize.Height = 135;
            }

            UnitPositionX = fovPosition.X;
            UnitPositionY = fovPosition.Y;
            grid.UnitSize = MaximumUnitSize = UnitSize = new UnitSize(fovSize.Width, fovSize.Height);
            if (MinimumUnitSize == null)
            {
                MinimumUnitSize = new UnitSize(0, 135);
            }
        }

        private void RefreshUIDirection()
        {
            uint childCount = 0;
            uint layerCount = Window.Instance.GetLayerCount();
            FluxLogger.InfoP("RefreshUIDirection, layerCount : [%d1]", d1: (int)layerCount);

            for (uint i = 0; i < layerCount; i++)
            {
                Layer layer = Window.Instance.GetLayer(i);
                if (layer != null)
                {
                    childCount = layer.GetChildCount();
                    FluxLogger.InfoP("layer : [%d1], childCount: [%d2]", d1: (int)i, d2: (int)childCount);
                    for (uint j = 0; j < childCount; j++)
                    {
                        View view = layer.GetChildAt(j);
                        if (view is RootLayout rootlayout)
                        {
                            if (rootlayout.currentDirection == UIDirection)
                            {
                                FluxLogger.InfoP("[%s1]:[%d1] skip UpdateLayout For RootLayout : [%s2]"
                                    , s1: rootlayout.GetType()?.Name
                                    , d1: (int)rootlayout.ID
                                    , s2: UIDirection.ToString());
                                continue;
                            }
                            rootlayout.currentDirection = UIDirection;
                            FluxLogger.InfoP("[%s1]:[%d1] call UpdateLayout For RootLayout : [%s2]"
                                    , s1: rootlayout.GetType()?.Name
                                    , d1: (int)rootlayout.ID
                                    , s2: UIDirection.ToString());

                            updateLayoutCallType.RootLayoutSize = new UnitSize(UnitSize.Width, UnitSize.Height);
                            updateLayoutCallType.Types = UpdateLayoutFinishedTypes.UIDirectionChange;
                            rootlayout.UpdateLayout();
                        }
                        else if (view is Layout layout)
                        {
                            FluxLogger.InfoP("call UpdateLayout For Layout");
                            layout.UpdateLayout();
                        }
                        else if (view is Component component)
                        {
                            FluxLogger.InfoP("call UpdateLayout For Component");
                            component.UpdateLayout();
                        }
                    }
                }
            }
        }

        internal override void UIDirectionChanged(object sender, DirectionChangedEventArgs e)
        {
            FluxLogger.InfoP("UIDirection = [%s1]", s1: UIDirection.ToString());
            RefreshUIDirection();
        }

        private void GridUpdate()
        {
            PolicyManager.Instance.UIAreaUnitSize = UnitSize;
            grid.UnitSize = UnitSize;

            grid.UpdateLayout();
        }

        private int GetCurrentAngle()
        {
            // TODO: It is replacement implementation of VConf (ROTATE_VCONF_KEY = "db/menu/broadcasting/audio_options/auto-stereo")
            return (int)Window.Instance.GetCurrentOrientation();
        }
        #endregion private Method

        #region private Field
        private bool IsFirstUpdateLayout = true;
        private bool useWindowSize = true;
        private bool enableBackground = false;
        private Layer windowLayer = null;
        private Grid grid = new Grid();
        private readonly UnitSize fovSize = new UnitSize(0, 0); // TODO remove
        private readonly UnitPosition fovPosition = new UnitPosition(0, 0);
        private EventHandler<AngleChangeEventArgs> rootLayoutAngleChange;
        private FluxBackground fluxBG = null;
        private EventHandler<UpdateLayoutFinishedEventArgs> updateLayoutFinished;
        private readonly UpdateLayoutFinishedEventArgs updateLayoutCallType = new UpdateLayoutFinishedEventArgs();

        private EventHandler<Window.ResizedEventArgs> portraitHandler;
        private EventHandler<Window.ResizedEventArgs> landscapeHandler;

        private int preWidth = -1, preHeight = -1, preAngle = -1;
        private Window.WindowOrientation prevWindowOrientation = Window.WindowOrientation.Landscape;

        //readonly private string ROTATE_VCONF_KEY = "db/menu/onscreendisplay/display_orientation/onscreen_menu_orientation";
        //For Developing
        //private readonly string ROTATE_VCONF_KEY = "db/menu/broadcasting/audio_options/auto-stereo";
        #endregion private Field
    }
}