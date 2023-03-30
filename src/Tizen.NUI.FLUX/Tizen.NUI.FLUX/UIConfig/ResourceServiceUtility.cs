/// @file
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.6.0 </version>
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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tizen.Applications;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Utility Class for Flux Common Icon
    /// </summary>
    /// <code>
    /// string ret = ResourceUtility.GetCommonServiceResourcePath("TVPlus", "../icons/ChannelList/ch_icon_default_tvplus.png");
    /// </code>
    /// <version> 10.10.0 </version>
    public static partial class ResourceUtility
    {
        /// <summary>
        /// A common icon is provided by the corresponding service, and the surrounding services allow easy access to the image path.
        /// </summary>
        /// <param name="serviceTag"> Service Tag Information </param>
        /// <param name="fileName"> File Name to Retrieve </param>
        /// <returns> File Path. If such path does not exist, return value is NULL. </returns>
        /// <version> 10.10.0 </version>
        public static string GetCommonServiceResourcePath(string serviceTag, string fileName)
        {
            if (string.IsNullOrEmpty(serviceTag) == true)
            {
                ConfigLogger.DebugP("serviceTag is null");
                return null;
            }
            if (string.IsNullOrEmpty(fileName) == true)
            {
                ConfigLogger.DebugP("fileName is null");
                return null;
            }

            ConfigLogger.DebugP("serviceTag:[%s1] , fileName:[%s2]", s1: serviceTag, s2: fileName);

            if (SaveServiceTagCache(serviceTag) == false)
            {
                ConfigLogger.DebugP("serviceTag:[%s1] does not exist", s1: serviceTag);
                return null;
            }

            if (serviceTagCacheDic[serviceTag].privilege == false)
            {
                ConfigLogger.DebugP("Caller does not have PLATFORM_PRIVILEGE");
                return null;
            }
            if (string.IsNullOrEmpty(serviceTagCacheDic[serviceTag].path) == true)
            {
                ConfigLogger.DebugP("Resource Path:[%s1] does not exist", s1: serviceTagCacheDic[serviceTag].path);
                return null;
            }

            string fullFilePath = serviceTagCacheDic[serviceTag].path + fileName;
            FileInfo fileInfo = new FileInfo(fullFilePath);
            ConfigLogger.DebugP("fileInfo:[%s1] ", s1: fileInfo.Name);
            if (fileInfo.Exists == false)
            {
                ConfigLogger.DebugP("fileInfo:[%s1] does not exist", s1: fileInfo.Name);
                ConfigLogger.DebugP("fullFilePath:[%s1]", s1: fullFilePath);
                return null;
            }

            return fullFilePath;
        }

        private class Info
        {
            public string path;
            public bool privilege;
            public Info(string path = "")
            {
                this.path = path;
            }
        }

        private static bool SaveServiceTagCache(string serviceTag)
        {
            if (serviceTagCacheDic.ContainsKey(serviceTag))
            {
                return true;
            }

            ApplicationInfoMetadataFilter metaDataFilter = new ApplicationInfoMetadataFilter();
            metaDataFilter.Filter.Add(SERVICE_RESOURCE_METADATA_KEY, serviceTag);

            ApplicationInfo app = ApplicationManager.GetInstalledApplicationsAsync(metaDataFilter).Result?.FirstOrDefault();

            if (app == null)
            {
                ConfigLogger.DebugP(" app is NUll ");
                return false;
            }

            ConfigLogger.DebugP("PkgId:[%s1] ", s1: app.PackageId);
            Package pkg = PackageManager.GetPackage(app.PackageId);
            Info info = new Info();
            info.path = pkg.RootPath + "/shared/";
            info.privilege = pkg.Privileges.Contains(PLATFORM_PRIVILEGE_NAME);
            ConfigLogger.DebugP("path:[%s1], privilege:[%s2]", s1: app.PackageId, s2: info.privilege.ToString());
            serviceTagCacheDic.Add(serviceTag, info);

            return true;
        }
        private static readonly string SERVICE_RESOURCE_METADATA_KEY = "http://developer.samsung.com/tizen/metadata/service_shared_icon";
        private static readonly string PLATFORM_PRIVILEGE_NAME = "http://tizen.org/privilege/internal/default/platform";
        private static Dictionary<string, Info> serviceTagCacheDic = new Dictionary<string, Info>();
    }
}