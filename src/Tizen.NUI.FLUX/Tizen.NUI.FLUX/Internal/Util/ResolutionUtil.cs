using System;

namespace Tizen.NUI.FLUX
{
    internal static class ResolutionUtil
    {
        private const string baseScreenResolutionMetadataKey = "http://tizen.org/metadata/app_ui_type/base_screen_resolution";
        private const string baseScreenResolutionMetadataValueMax = "max";
        private const string baseScreenResolutionMetadataValueResponsive = "responsive";
        private const int defaultWidth = 1920;
        private const int defaultHeight = 1080;

        private static string currentBaseResolutionMetadataValue = null;
        private static (int width, int height)? currentProductOsdResoultion = null;
        private static (int width, int height) defaultResolution = (defaultWidth, defaultHeight);

        public static (int width, int height) GetCurrentBaseResolution()
        {
            (int, int) ret;

            // refer to the guide of base_screen_resolution: http://wiki.vd.sec.samsung.net/x/J-4sAQ
            switch (GetBaseScreenResoultionMetadataValue())
            {
                case baseScreenResolutionMetadataValueMax:
                    ret = GetProductOsdResolution();
                    break;
                case baseScreenResolutionMetadataValueResponsive:
                default:
                    ret = defaultResolution;
                    break;
            }

            return ret;
        }

        public static (int width, int height) GetProductOsdResolution()
        {
            if (currentProductOsdResoultion == null)
            {
                Interop.WindowUtil.GetScreenSize(out int width, out int height);

                if (width == 0 || height == 0)
                {
                    CLog.Error("Size is invalid, width : %d1, height: %d2", d1: width, d2: height);
                    currentProductOsdResoultion = (defaultWidth, defaultHeight);
                }
                else
                {
                    currentProductOsdResoultion = (width, height);
                }
            }

            return currentProductOsdResoultion.Value;
        }

        private static string GetBaseScreenResoultionMetadataValue()
        {
            if (currentBaseResolutionMetadataValue == null)
            {
                Applications.Application currntApplication = Tizen.Applications.Application.Current ?? throw new InvalidOperationException("current application is null");
                if (currntApplication.ApplicationInfo.Metadata.TryGetValue(baseScreenResolutionMetadataKey, out string value))
                {
                    currentBaseResolutionMetadataValue = value.ToLowerInvariant();
                }
                else
                {
                    // set "undefined" to not satisfy the condition of the if statement. this is not an official metadata value
                    currentBaseResolutionMetadataValue = "undefined";
                }
            }

            return currentBaseResolutionMetadataValue;
        }
    }
}