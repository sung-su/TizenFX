/// @file FluxDownloadManager.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version>10.10.0</version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2022 Samsung Electronics Co., Ltd All Rights Reserved
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

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Singleton class. Links DownloadManager to FLUX side.
    /// </summary>
    /// <code>
    /// FluxDownloadManager.Instance.DownloadManager.RequestDownload(this, resourceUrl, OnDownloadComplete);
    /// </code>
    internal class FluxDownloadManager
    {
        /// <summary>
        /// Returns the singleton instance of the FluxDownloadManager
        /// </summary>
        /// <returns> FluxDownloadManager singleton instance</returns>
        public static FluxDownloadManager Instance => instance;

        /// <summary>
        /// DownloadManager that manages resource download from remote server. Implements IDownloadManager interface.
        /// </summary>
        public IDownloadManager DownloadManager
        {
            get;
            set;
        } = null;

        private FluxDownloadManager()
        {
        }

        private static readonly FluxDownloadManager instance = new FluxDownloadManager();
    }
}
