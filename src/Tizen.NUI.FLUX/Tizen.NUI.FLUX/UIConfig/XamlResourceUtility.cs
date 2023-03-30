/// @file
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
using System.IO;
using Tizen.NUI.Xaml;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Utility class for getting image resource paths in xaml file
    /// </summary>
    /// <code>
    /// XamlResourceUtility xamlResourceUtility = new XamlResourceUtility();
    /// xamlResourceUtility.AppID = "com.samsung.tv.coba.ambientcontentapp";
    /// xamlResourceUtility.FileName = "i_img_radiobutton_nf";
    /// string path = xamlResourceUtility.ProvideValue(new IServiceProviderImpl());
    /// </code>
    public class XamlResourceUtility : IMarkupExtension<string>
    {
        /// <summary>
        /// Name of the file which file path to be searched for
        /// </summary>
        public string FileName { get; set; } = "";

        /// <summary>
        /// Check app resource or common resource. In case of app resource, it is set to true.
        /// </summary>
        public bool IsAppResource { get; set; } = false;

        /// <summary>
        /// Application ID to be used for ResourceUtility.GetResourcePath(string appId, string fileName)
        /// </summary>
        public string AppID { get; set; } = UIConfig.UserDefinedAppID;

        /// <summary>
        /// Default size is ResourceSizes.Middle.for searching Common Resource Paths. Used for GetCommonResourcePath(string fileName, ResourceSizes size = ResourceSizes.Middle).
        /// </summary>
        public ResourceUtility.ResourceSizes Size { get; set; } = ResourceUtility.ResourceSizes.Middle;

        /// <summary>
        /// Method to return the path in string format. Function of IMarkupExtension interface.
        /// </summary>
        /// <code>
        /// <flc:ButtonBasicText x:Name="xButtonBasicText02" UnitSize="39,9" Text="New Button" IconResourceURL="{u:XamlResourceUtility FileName=logo_2.png}">
        /// </code>
        /// <param name="serviceProvider">Provides value for xaml</param>
        /// <returns>Resource path</returns>
        public string ProvideValue(IServiceProvider serviceProvider)
        {
            if (IsAppResource)
            {
                return GetAppResourcePath(FileName);
            }
            return GetCommonResourcePath(FileName);
        }

        /// <summary>
        /// Default function of IMarkupExtension interface
        /// </summary>
        /// <param name="serviceProvider">Provides value for xaml</param>
        /// <returns>Resource path</returns>
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            if (IsAppResource)
            {
                return GetAppResourcePath(FileName);
            }
            return GetCommonResourcePath(FileName);
        }

        private string GetAppResourcePath(string fileName)
        {
            string retPath = ResourceUtility.GetResourcePath(AppID, fileName);
            if (global::System.IO.File.Exists(retPath) == false)
            {
                retPath = Tizen.Applications.Application.Current.DirectoryInfo.Resource + @"images/" + fileName;
            }
            ConfigLogger.DebugP("GetAppResourcePath: [%s1]", s1: retPath);
            return retPath;
        }

        private string GetCommonResourcePath(string fileName)
        {
            string retPath = "";
            ConfigLogger.DebugP("FileName: [%s1], AppID:[%s2]", s1: FileName, s2: AppID);

            string fileNameWithoutExtension = global::System.IO.Path.GetFileNameWithoutExtension(fileName);
            try
            {
                if (CheckResourceExtension(fileName))
                {
                    retPath = ResourceUtility.GetCommonResourcePath(fileNameWithoutExtension, Size);
                }
                else
                {
                    retPath = ResourceUtility.GetCommonResourcePath(fileName, Size);
                }
                ConfigLogger.DebugP("GetAppResourcePath: [%s1]", s1: retPath);
            }
            catch (Exception)
            {
                ConfigLogger.DebugP("** Exception : FileName: [%s1], AppID:[%s2]", s1: retPath, s2: AppID);
            }

            ConfigLogger.DebugP("GetCommonResourcePath: [%s1]", s1: retPath);
            return retPath;
        }

        private static bool CheckResourceExtension(string fileName)
        {
            if (fileName == null)
            {
                return false;
            }

            FileInfo filename = new FileInfo(fileName);
            switch (filename.Extension?.ToLowerInvariant())
            {
                case ".svg":  /* FALL THROUGH */
                    return true;
                default:
                    return false;
            }
        }
    }
}