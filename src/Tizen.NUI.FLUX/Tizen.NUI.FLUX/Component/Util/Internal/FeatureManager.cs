/// @file FeatureManager.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 5.5.0 </version>
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
using Tizen.System;

namespace Tizen.NUI.FLUX.Component
{
    // TODO : this is sample which we can use, it should be refined
    internal static class FeatureManager
    {
        public static void Initialize()
        {
            CheckSmartType();
            CheckFluxAnimation();
        }

        internal static bool IsPortrait()
        {
            bool ret = false;

            Window.WindowOrientation orientation = Window.Instance.GetCurrentOrientation();

            if (orientation == Window.WindowOrientation.Portrait || orientation == Window.WindowOrientation.PortraitInverse)
            {
                ret = true;
            }
            else if (orientation == Window.WindowOrientation.Landscape || orientation == Window.WindowOrientation.LandscapeInverse)
            {
                Size2D windowSize = Window.Instance.Size;

                if (windowSize != null)
                {
                    // Need to check portrait spec
                    if (windowSize.Width <= 1080)
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        private static void CheckVectorFeature()
        {
            IsSupportVectorPrimitive = true;
            IsSupportVectorAnimation = true;
        }

        private static void CheckSmartType()
        {
            Information.TryGetValue("com.samsung/featureconf/uifw.theme_type", out string themeType);

            FluxLogger.InfoP("themeType : [%s1]", s1: themeType);
            if (themeType == "lite")
            {
                UIConfig.IsFullSmart = false;

                // Apply UIRadius to 0 at Tizen Lite Product
                UIConfig.UIRadius = 0;
            }
            else // default : "premium"
            {
                UIConfig.IsFullSmart = true;
            }
        }

        private static void CheckFluxAnimation()
        {
            try
            {
                Information.TryGetValue("com.samsung/featureconf/uifw.support_animation", out bool isSupportAnimation);
                IsSupportFluxAnimation = isSupportAnimation;
            }
            catch (PlatformNotSupportedException e)
            {
                FluxLogger.ErrorP("Failed to get FMS support_animation key:[%s1]", s1: e.Message);
            }
        }

        internal static bool IsTVProduct = false;
        internal static bool IsLFDProduct = false;
        internal static bool IsHTVProduct = false;
        internal static bool IsSupportVectorPrimitive = false;
        internal static bool IsSupportVectorAnimation = false;
        internal static bool IsSupportScreenReader = false;
        internal static bool IsSupportWall = false;
        internal static bool IsSupportFluxAnimation = false;

        internal const int DefaultRadius = 2;

    }
}
