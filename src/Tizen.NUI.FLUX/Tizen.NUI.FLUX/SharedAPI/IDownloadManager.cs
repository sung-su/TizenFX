/// @file IDownloadManager.cs
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

using System;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Interface for DownloadManager
    /// </summary>
    public interface IDownloadManager
    {
        /// <summary>
        /// Requests download of given url to server
        /// </summary>
        /// <param name="userData">User data object</param>
        /// <param name="url">Resource url</param>
        /// <param name="OnDownloadComplete">Callback for when download is complete. Takes in a user data object, success(true) or failure(false), requestedUrl, cachedUrl as parameters.</param>
        /// <returns>True if download is successful, false if not</returns>
        bool RequestDownload(object userData, string url, Action<object, bool, string, string> OnDownloadComplete);

        /// <summary>
        /// Discards the requested url download. OnDownloadComplete should not be called.
        /// </summary>
        /// <param name="userData">User data object</param>
        /// <param name="url">Resource url</param>
        /// <returns>True if request is successfully discarded, false if not</returns>
        bool DiscardRequest(object userData, string url);
    }
}