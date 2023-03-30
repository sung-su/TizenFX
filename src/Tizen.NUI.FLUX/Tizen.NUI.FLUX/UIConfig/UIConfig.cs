/// @file
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
using System.Threading;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// UIConfig provides information regarding UI related configuration such as resource, size, etc.
    /// Also, user can change some configuration based on ux guide.
    /// </summary>
    /// <code>    
    /// UIConfig.ScaleFactor = 1.0f;
    /// </code>
    public static class UIConfig
    {
        /// <summary>
        /// Gets or sets the scale factor of an application.
        /// The scale factor changes physical size of unit on the screen. The default value is 1.
        /// </summary>
        public static float ScaleFactor
        {
            get => appScaleFactor;
            internal set => appScaleFactor = value;
        }

        private static float appScaleFactor = 1.0f;

        /// <summary>
        /// UIPlate Radius value, it's Unit value.
        /// This value is product configurable value. The default value is 2 unit.
        /// </summary>
        public static int UIRadius
        {
            get;
            set;
        } = 2;

        /// <summary>
        /// whether it's supporting webp
        /// </summary>
        internal static bool WebpSupported
        {
            get;
            set;
        } = true;

        /// <summary>
        /// configuration for pointing mode.
        /// </summary>
        ///<code>
        /// UIConfig.SupportPointingMode = PointingMode.SupportTouch; 
        /// </code>
        /// <version>10.10.0</version>
        public static PointingMode SupportPointingMode
        {
            get => supportMode;
            set
            {
                supportMode = value;
            }
        }

        /// <summary>
        /// enum values for SupportPointingMode. 
        /// </summary>
        /// <version>10.10.0</version>
        public enum PointingMode
        {
            /// <summary>
            /// Not Support pointing mode
            /// </summary>
            SupportNone,
            /// <summary>
            /// Support Touch Only - in case of not supporting remote control
            /// </summary>
            SupportTouchOnly,

            /// <summary>
            /// Support all type like Touch/Mouse
            /// </summary>
            SupportPointing
        }

        /// <summary>
        /// This needs to be set if it has a different appid from the current appid.
        /// For example current app is com.samsung.tv.Tizen.TV.Filebrowser.Flip.Example.Decorator 
        /// but it uses resource under csapi-tv-filebrowser-flip 
        /// </summary>
        /// <version>10.10.2</version>
        public static string UserDefinedAppID { get; set; } = Tizen.Applications.Application.Current?.ApplicationInfo?.ApplicationId;

        /// <summary>
        /// configuration for Tizen Lite. false : Tizen Lite
        /// </summary>
        public static bool IsFullSmart = true;

        internal static PointingMode supportMode = PointingMode.SupportPointing;
        internal static readonly int MainThreadID = Thread.CurrentThread.ManagedThreadId;
    }
}
