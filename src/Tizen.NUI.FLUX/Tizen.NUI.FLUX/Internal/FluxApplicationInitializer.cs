/// @file FluxApplicationInitializer.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 8.8.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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
using System.IO;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.System;

namespace Tizen.NUI.FLUX
{
    internal static class FluxApplicationInitializer
    {
        private const string smartTypeKey = "PlatformSmartType";
        private const string smartTypeFull = "Full";
        private const string smartTypeEntry = "Entry";
        private const string animationEnabledKey = "FLUX_ANIMATION_ENABLE_MODE";

        private static View defaultFocusIndicator = null;

        public static void Initialize()
        {
            MakeFocusIndicatorInvisible();
            CheckAnimationEnableType();
            Interop.Misc.RegisterChinaLanguageLocale();

            Window.Instance.AddAvailableOrientation(Window.WindowOrientation.Landscape);
            Window.Instance.AddAvailableOrientation(Window.WindowOrientation.Portrait);
            Window.Instance.AddAvailableOrientation(Window.WindowOrientation.LandscapeInverse);
            Window.Instance.AddAvailableOrientation(Window.WindowOrientation.PortraitInverse);

            bool isReleaseImage = File.Exists("/etc/release");
            if (isReleaseImage)
            {
                Tizen.NUI.Accessibility.Accessibility.BridgeDisableAutoInit();
            }

            _ = SystemProperty.Instance; // to call its constructor that does initialization of SystemProperty
        }

        public static void CleanUp()
        {
            SystemProperty.Instance.CleanUp();
        }

        private static void MakeFocusIndicatorInvisible()
        {
            if (defaultFocusIndicator == null)
            {
                defaultFocusIndicator = new View();
            }

            FocusManager.Instance.FocusIndicator = defaultFocusIndicator;
        }

        private static void CheckAnimationEnableType()
        {
            string animationEnable = Environment.GetEnvironmentVariable(animationEnabledKey);
            animationEnable = animationEnable?.TrimEnd('\r', '\n');

            if (animationEnable == "1")
            {
                Environment.SetEnvironmentVariable(smartTypeKey, smartTypeFull);
                CLog.Error("Animation enabled by user");
            }
            else if (animationEnable == "0")
            {
                Environment.SetEnvironmentVariable(smartTypeKey, smartTypeEntry);
                CLog.Error("Animation disabled by user");
            }
            else
            {
                bool IsAnimationSupported = true;
                try
                {
                    if (Information.TryGetValue("http://tizen.org/feature/sensor.accelerometer", out bool isSupported))
                    {
                        IsAnimationSupported = isSupported;
                    }
                }
                catch (PlatformNotSupportedException e)
                {
                    CLog.Error("Failed to get FMS support_animation key: %s1", s1: e.Message);
                }
                Environment.SetEnvironmentVariable(smartTypeKey, IsAnimationSupported ? smartTypeFull : smartTypeEntry);
                CLog.Info("Animation enabled by platform type: %d1", d1: Convert.ToInt32(IsAnimationSupported));
            }
        }
    }
}