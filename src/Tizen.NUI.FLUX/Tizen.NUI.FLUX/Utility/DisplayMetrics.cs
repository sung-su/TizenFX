/// @file DisplayMetrics.cs
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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// DisplayMetrics class
    /// DisplayMetrics support the unit coordinate system and related utility
    /// </summary>
    /// <code>    
    /// int convertedValue = DisplayMetrics.Instance.UnitToPixel(unitSize);
    /// convertedValue = DisplayMetrics.Instance.PixelToUnit(pixelSize);
    /// </code>
    public class DisplayMetrics
    {
        // Consider things //
        // 1Unit : 8px
        // Scale factor : dependent on inch size
        // OSDP resolution : 1920x1080, 3840x2160
        // 1Unit is 4px in Blend UX
        private const int basePPU = 4;
        private const int maxWidth = 3840;
        private const int maxHeight = 3840;
        private float scaleFactor = 1.0f;

        /// <summary>
        /// DisplayMetrics class
        /// DisplayMetrics support the unit coordinate system and related utility
        /// </summary>
        public static DisplayMetrics Instance { get; } = new DisplayMetrics();

        /// <summary>
        /// Convert from unit to pixel
        /// </summary>
        /// <param name="unit"> unit value </param>
        /// <returns> pixel value that is converted from unit </returns>
        public int UnitToPixel(int unit)
        {
            return (int)(FloatPPU * unit);
        }

        /// <summary>
        /// Convert from pixel to unit
        /// </summary>
        /// <param name="pixel"> pixel value </param>
        /// <returns> unit value that is converted from pixel </returns>
        public int PixelToUnit(int pixel)
        {
            return (int)(pixel / FloatPPU);
        }

        internal int PPU { get; private set; }

        internal float FloatPPU { get; private set; }

        internal int GetTVAngle(Window.WindowOrientation orientation)
        {
            return orientation switch
            {
                Window.WindowOrientation.Landscape => 0,
                Window.WindowOrientation.Portrait => 90,
                Window.WindowOrientation.LandscapeInverse => 180,
                Window.WindowOrientation.PortraitInverse => 270,
                _ => 0
            };
        }

        internal float ScaleFactor
        {
            set
            {
                if (scaleFactor == value)
                {
                    return;
                }

                float oldScaleFactor = scaleFactor;
                UpdateSceneTree(value);
                scaleFactor = value;
                CalculatePPU();

                EmitScaleFactorChangedEvent(oldScaleFactor, scaleFactor);
            }
            get => scaleFactor;
        }
        internal class ScaleFactorChangedEventArgs : EventArgs
        {
            internal float OldValue { get; }
            internal float NewValue { get; }
            internal ScaleFactorChangedEventArgs(float oldValue, float newValue)
            {
                OldValue = oldValue;
                NewValue = newValue;
            }
        }

        internal event EventHandler<ScaleFactorChangedEventArgs> ScaleFactorChanged;

        private DisplayMetrics()
        {
            Initialize();
        }

        private int UnitToPixel(int unit, float scale)
        {
            return (int)(scale * basePPU * unit);
        }

        private void Initialize()
        {
            CalculateScaleFactor();
            CalculatePPU();
        }

        //Scale factor calculated here is overridden in ComponentEntry.Initialize()
        private void CalculateScaleFactor()
        {
            Interop.WindowUtil.GetScreenSize(out int graphicResolutionWidth, out int graphicResolutionHeight);

            // TODO : spec is not fixed. For 4K OSD 
            if (graphicResolutionWidth == maxWidth || graphicResolutionHeight == maxHeight)
            {
                scaleFactor = 2.0f;
            }
            // TODO : Spec is not fixed. For flux-simulator 
            Size2D windowSize = Window.Instance.Size;
            if (windowSize != null)
            {
                if (windowSize.Width == maxWidth || windowSize.Height == maxHeight)
                {
                    scaleFactor = 2.0f;
                }
            }
            CLog.Info("DisplayMetrics Initialized.  [ScaleFactor: %f1], [PPU: %f2], [ScreenSize: %d1 x %d2]"
                , f1: scaleFactor
                , f2: basePPU * scaleFactor
                , d1: graphicResolutionWidth
                , d2: graphicResolutionHeight
                );
        }

        private void CalculatePPU()
        {
            FloatPPU = basePPU * scaleFactor;
            PPU = (int)(FloatPPU);
        }

        private void UpdateSceneTree(float scale)
        {
            TraverseWindow(scale);
        }

        private void TraverseWindow(float scale)
        {
            List<Window> windowList = Application.GetWindowList();

            foreach (Window win in windowList)
            {
                uint layerCount = win.GetLayerCount();
                for (uint index = 0; index < layerCount; index++)
                {
                    Layer layer = win.GetLayer(index);

                    if (layer != null)
                    {
                        TraverseObject(layer, 1, layer.Visibility, scale);
                    }
                }
            }
        }

        private void TraverseObject(Animatable obj, int depth, bool parentVisiblity, float scale)
        {
            if (obj == null)
            {
                return;
            }

            if (obj is View myView)
            {
                if (obj is FluxView fluxView)
                {
                    fluxView.Size2D = new Size2D(UnitToPixel(fluxView.UnitSize.Width, scale), UnitToPixel(fluxView.UnitSize.Height, scale));
                    fluxView.Position2D = new Position2D(UnitToPixel(fluxView.UnitPosition.X, scale), UnitToPixel(fluxView.UnitPosition.Y, scale));
                }
                //Traversing Child objects
                uint childCount = myView.ChildCount;
                for (uint index = 0; index < childCount; index++)
                {
                    TraverseObject(myView.GetChildAt(index), depth + 1, myView.Visibility && parentVisiblity, scale);
                }
            }
            else if (obj is Layer myLayer)
            {
                //Traversing Child objects
                uint childCount = myLayer.ChildCount;
                for (uint index = 0; index < childCount; index++)
                {
                    TraverseObject(myLayer.GetChildAt(index), depth + 1, myLayer.Visibility && parentVisiblity, scale);
                }
            }
            else
            {
                //A NUI Object that cannot have child objects.
                return;
            }
        }
        private void EmitScaleFactorChangedEvent(float oldValue, float newValue)
        {
            ScaleFactorChanged?.Invoke(this, new ScaleFactorChangedEventArgs(oldValue, newValue));
        }
    }
}