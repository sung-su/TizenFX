/// @file RestrictedModeManager.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version>10.10.0</version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
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

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// the class to mange RestrictedMode
    /// </summary>
    /// <code>
    /// RestrictedModeManager.Instance.RestrictedMode = RestrictedModeManager.RestrictedModes.FatalLog;
    /// </code>
    public sealed class RestrictedModeManager
    {
        internal static readonly string subTag = "RESTRICTED";
        private static RestrictedModeManager manager;

        /// <summary>
        /// enum for restricted mode
        /// </summary>
        public enum RestrictedModes
        {
            /// <summary>
            /// Do nothing for invalid operation
            /// </summary>
            ModeNone,

            /// <summary>
            /// print dlog with backtrace
            /// </summary>
            FatalLog,

            /// <summary>
            /// throw InvalidOperationException
            /// </summary>
            Exception,
        };

        /// <summary>
        /// RestrictedModeManager instance (read only)
        /// </summary>
        public static RestrictedModeManager Instance
        {
            get
            {
                if (manager == null)
                {
                    manager = new RestrictedModeManager();
                }
                return manager;
            }
        }

        /// <summary>
        /// RestrictionMode (default - RestrictedModeNone)       
        /// </summary>
        public RestrictedModes RestrictedMode
        {
            get;
            set;
        } = RestrictedModes.ModeNone;

        internal void NotifyRestrictedOperation(string message)
        {
            if (RestrictedMode == RestrictedModes.FatalLog)
            {
                CLog.Fatal(message);
                CLog.Fatal(Environment.StackTrace);
            }
            else if (RestrictedMode == RestrictedModes.Exception)
            {
                throw new InvalidOperationException(message);
            }
        }

        private RestrictedModeManager()
        {
        }
    }
}
