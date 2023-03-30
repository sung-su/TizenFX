/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file ComponentEntry.cs
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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using Tizen.NUI;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// Flux component entry
    /// </summary>
    /// <code>
    /// ComponentEntry.Initialize();
    /// </code>
    public static class BaseComponentEntry
    {
        /// <summary>
        /// Define the  Responsive Rule Version of FLUXUS. Default is 1.0 version.
        /// </summary>
        /// <version> 10.10.0 </version>
        public static string ResponsiveRuleMinimumVersion
        {
            get => PolicyManager.Instance.Version.ToString();
            set
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.CurrentCulture, out float version))
                {
                    PolicyManager.Instance.Version = version;
                }
            }
        }

        /// <summary>
        /// Should called before using Flux Components
        /// </summary>
        public static void Initialize()
        {
            Initialize(1.0f, null);
        }

        /// <summary>
        ///  FLUX component initialize 
        /// </summary>
        /// <param name="appScaleFactor"> If user want to change FLUX scale factor , then please assign your factor using this API. Scale factor shuold be assigned as predefined value by UX" </param>
        /// <exception>You call this API at other thread</exception>
        /// <version> 9.9.0 </version>
        public static void Initialize(float appScaleFactor)
        {
            Initialize(appScaleFactor, null);
        }

        /// <summary>
        ///  FLUX component initialize 
        /// </summary>
        /// <param name="customColorPath"> If user want to replace color table, please give folder path. And reserved file name of color table is "principle_colortable.json" </param>
        /// <exception>You call this API at other thread</exception>
        /// <version> 8.8.0 </version>
        public static void Initialize(string customColorPath)
        {
            Initialize(1.0f, customColorPath);
        }

        /// <summary>
        /// Should be called when the application exit
        /// </summary>
        public static void CleanUp()
        {
            FluxLogger.ErrorP("CleanUp");
            componentEntryInitialized = false;
            RepeatKeyManager.Instance.CleanUp();

            ResourceUtility.IsFlux = false;
            if (!FeatureManager.IsSupportScreenReader)
            {
                TtsService.Instance.Terminate();
            }
            FocusManager.Instance.SetCustomAlgorithm(null);
            GestureDetector.Instance.Dispose();
            // TODO: Disable VirtualScreen feature
            //VirtualScreenLinker.Instance().RemoveVirtualScreenFluxResizeCallback(VirtualScreenWindowChanged);
        }

        // TODO: Disable VirtualScreen feature
        ///// <summary>
        ///// If your application window is added in VirtualScreen, application window is resized according to the ratio of VirtualScreen.
        ///// The ratio of virtualScreen is 16:9 , the ratio of application window is 16:9 
        ///// The ratio of virtualScreen is 9:16 , the ratio of application window is 9:16 
        ///// Default is true. If you don't want to according to the ratio of virtualScreen, set false.
        ///// </summary>
        ///// <version> 9.9.2 </version>
        //public static bool VirtualScreenAutoFitEnabled
        //{
        //    get => virtualScreenAutoFitEnabled;
        //    set
        //    {
        //        virtualScreenAutoFitEnabled = value;
        //        FluxLogger.InfoP("virtualScreenAutoFitEnabled:[%d1]", d1: virtualScreenAutoFitEnabled ? 1 : 0);
        //        if (virtualScreenAutoFitEnabled == true)
        //        {
        //            VirtualScreenLinker.Instance().SetVirtualScreenFluxResizeCallback(VirtualScreenWindowChanged);
        //        }
        //        else
        //        {
        //            VirtualScreenLinker.Instance().RemoveVirtualScreenFluxResizeCallback(VirtualScreenWindowChanged);
        //        }
        //    }
        //}

        internal static void Initialize(float scaleFactor, string customColorPath = null)
        {
            if (componentEntryInitialized == true)
            {
                return;
            }

            componentEntryInitialized = true;

#if Support_FLUXCore_ScaleFactorChange
            InitializeScaleFactor(scaleFactor);
#endif

            GestureOptions.Instance.SetDoubleTapTimeout(Constant.DOUBLETAP_TIMEOUT);
            GestureOptions.Instance.SetLongPressMinimumHoldingTime(Constant.MINIMUM_LONGPRESS_HOLDTIME);

            if (Window.IsInstalled() == false)
            {
                throw new Exception($"Window is not installed or ComponentEntry is called in {Thread.CurrentThread.ManagedThreadId})");
            }

            //Todo : MR_Son will add this after UIConfig for safe release.
            //if (UIConfig.MainThreadID != Thread.CurrentThread.ManagedThreadId)
            //{
            //    Log.Error("UIFW.UIConfig", $"UIConfig.MainThreadID {UIConfig.MainThreadID}, Thread.CurrentThread.ManagedThreadId {Thread.CurrentThread.ManagedThreadId}");
            //    throw new Exception($"ColorTable is called in user thread (MainThreadID {UIConfig.MainThreadID}, ManagedThreadId {Thread.CurrentThread.ManagedThreadId})");
            //}

            FeatureManager.Initialize();
            //Initialze the ThemeHelper to set root path of ResourceUtility
            FluxLogger.InfoP($"Current Theme : {ThemeHelper.Instance.CurrentTheme}");
            ResourceUtility.IsFlux = true;
            ResourceUtility.IsHighContrast = AccessibilityManager.Instance.HighContrast;

#if Support_FLUXCore_ThemeManager
            StyleManager.SetBrokenImage(StyleManager.BrokenImageType.Small, ResourceUtility.GetCommonResourcePath("i_img_sw_error/i_img_sw_error_s.9.webp"));
            StyleManager.SetBrokenImage(StyleManager.BrokenImageType.Normal, ResourceUtility.GetCommonResourcePath("i_img_sw_error/i_img_sw_error_m.9.webp"));
            StyleManager.SetBrokenImage(StyleManager.BrokenImageType.Large, ResourceUtility.GetCommonResourcePath("i_img_sw_error/i_img_sw_error_l.9.webp"));
            FluxLogger.InfoP("Set default broken image");
#endif
            if (customColorPath != null)
            {
                if (File.Exists(customColorPath + "principle_colortable.json") == true)
                {
                    ColorChipTable.Instance.UpdateColorChipTable(customColorPath + "/principle_colortable.json");
                }

                if (File.Exists(customColorPath + "app_custom_colortable.json") == true)
                {
                    ColorChipTable.Instance.UpdateColorChipTable(customColorPath + "/app_custom_colortable.json");
                }
            }

            // TODO: Need to check which assembly should be registred
            FluxApplication.RegisterAssembly(typeof(Layout).GetTypeInfo().Assembly);
            
            // Remove component register code, it should move to Component project
            //RegisterTemplate();
            //RegisterPreset();
            //RegisterColorPreset();
            FocusManager.Instance.SetCustomAlgorithm(AutoFocusAlgorithm.Instance);
            // need to call style initialization

            RepeatKeyManager.Instance.Initialize();
            // TODO: Disable VirtualScreen feature
            // VirtualScreenAutoFitEnabled = true;
            //It will be moved in FLUX core code.
            Window.Instance.AddAuxiliaryHint("wm.virtualscreen.flux.resize", "enable");
        }

        // TODO: Disable VirtualScreen feature
        //private static void VirtualScreenWindowChanged(int width, int height, bool followRatio)
        //{
        //    List<Window> windowList = Tizen.NUI.Application.GetWindowList();
        //    foreach (Window window in windowList)
        //    {
        //        bool result = WindowGeometryManager.VirtualScreen.Instance.CalculateWindowGeometry(window, width, height, followRatio);
        //        if (result == false)
        //        {
        //            FluxLogger.ErrorP("Failed to calculate virtual screen window geometry");
        //            continue;
        //        }
        //    }
        //}

#if Support_FLUXCore_ScaleFactorChange
        private static void InitializeScaleFactor(float appScaleFactor)
        {
            FLUX.Interop.WindowUtil.GetScreenSize(out int width, out int height);
            //For 4K OSD 
            Size2D windowSize = Window.Instance.Size;
            if ((width == WidthFor4K && height == HeightFor4K) // For Device
                || (windowSize?.Width == WidthFor4K && windowSize?.Height == HeightFor4K)) // For Simulator
            {
                currentScaleFactor = ScaleFactorFor4K;
                ResourceUtility.IsHighDimension = true;
            }
            else
            {
                currentScaleFactor = ScaleFactorFor2K;
                ResourceUtility.IsHighDimension = false;
            }

            //Scale factor calculated in FLUX side is overridden here
            DisplayMetrics.Instance.ScaleFactor = currentScaleFactor * appScaleFactor;
            UIConfig.ScaleFactor = currentScaleFactor * appScaleFactor;

            FluxLogger.InfoP("width: [%d1], height: [%d2], currentScaleFactor: [%f1], appScaleFactor: [%f2]"
                , d1: width
                , d2: height
                , f1: currentScaleFactor
                , f2: appScaleFactor);
        }

        private const int WidthFor4K = 3840;
        private const int HeightFor4K = 2160;

        private const float ScaleFactorFor4K = 2.0f;
        private const float ScaleFactorFor2K = 1.0f;
#endif

        private static float currentScaleFactor = 1.0f;
        // TODO: Disable VirtualScreen feature
        //private static bool virtualScreenAutoFitEnabled = true;
        private static bool componentEntryInitialized = false;


    }
}
