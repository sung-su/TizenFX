/// @file ResourceUtility.cs
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


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Utility Class for Flux
    /// </summary>
    public static partial class ResourceUtility
    {
        /// <summary>
        /// Size of CommonResource files
        /// </summary>
        public enum ResourceSizes
        {
            /// <summary>
            ///  Small size of Common Resource
            /// </summary>
            Small,
            /// <summary>
            ///  Middle size of Common Resource
            /// </summary>
            Middle,

            /// <summary>
            ///  Large size of Common Resource
            /// </summary>
            Large
        }
        /// <summary>
        ///  If user set true, check RW resource first.
        /// </summary>
        public static bool IsHighDimension
        {
            get;
            set;
        } = false;

        /// <summary>
        /// A flag to check flux application or not in GetCommonResourcePath. 
        /// this should be used in FLUX Component only
        /// </summary>
        /// <summary>
        /// ResourceUtility.IsFlux = true;
        /// </summary>
        ///
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool IsFlux
        {
            get => isFlux;
            set
            {
                isFlux = value;

                // reset cache
                Cache.Clear();
            }
        }

        /// <summary>
        /// A flag to check Hicontrast Mode
        /// this should be used in FLUX Component only
        /// </summary>
        /// <summary>
        /// ResourceUtility.IsFlux = true;
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool IsHighContrast
        {
            get => isHighContrast;
            set
            {
                isHighContrast = value;

                // reset cache
                Cache.Clear();
            }
        }

        /// <summary>
        /// Provide the app resource path name for flux.
        /// </summary>
        /// <param name="appId">appid</param>
        /// <param name="fileName">file name</param>
        /// <returns></returns>
        public static string GetResourcePath(string appId, string fileName)
        {
            if (appId is null || fileName is null)
            {
                ConfigLogger.DebugP("appId:[%s1] or fileName : [%s2] is null", s1: appId, s2: fileName);

                return null;
            }

            string retPath = rootPath + "/" + appId + "/" + fileName;

            ConfigLogger.DebugP("appId:[%s1], fileName:[%s2]", s1: appId, s2: fileName);

            return retPath;
        }

        /// <summary>
        /// If you enter fileName , provide the common resource path.
        /// </summary>
        /// <code>
        /// imageBox.ResourceUrl  = ResourceUtility.GetCommonResourcePath("i_icon_function_apps");
        ///  /* ResourceSize should be defined by UX document */
        ///  imageBox.ResourceUrl = ResourceUtility.GetCommonResourcePath("i_icon_device_m_a",  ResourceSizes.Large);
        /// </code>
        /// <param name="fileName">Resource file name defined by UX Principle Guideline</param>
        /// <param name="size">File size defind by UX Guideline. Default is ResourceSizes.Middle. </param>
        /// <returns>CommonResource Path</returns>

        public static string GetCommonResourcePath(string fileName, ResourceSizes size = ResourceSizes.Middle)
        {
            if (fileName == null)
            {
                return null;
            }

            // TODO : Need to check originalFileName.FullName (`/opt/usr/home/owner/`) is correct or not.
            //FileInfo originalFileName = new FileInfo(fileName);
            string replacedFileName = null;

            //ConfigLogger.DebugP("** originalFileName : [%s1]", s1: originalFileName.FullName);


            if (UIConfig.WebpSupported == true)
            {
                //png --> webp
                replacedFileName = ChangeFileExtensionName(fileName, ".png", ".webp");
            }
            else
            {
                //webp --> png
                replacedFileName = ChangeFileExtensionName(fileName, ".webp", ".png");
            }

            string cachingPath = FindFileNameInCache(replacedFileName);

            if (cachingPath != null)
            {
                ConfigLogger.DebugP("** Return Caching Path : [%s1] -> [%s2]", s1: replacedFileName, s2: cachingPath);
                return cachingPath;
            }

            string finalFilePath = null;
            string basePath = null;

            if (NeedToCheckHighDimension(replacedFileName) == true)
            {
                basePath = GetProductBasePath(CommonPath + principle4KTag);

                finalFilePath = basePath + replacedFileName;

                ConfigLogger.DebugP("** Return HighDimension Path : [%s1] -> [%s2]", s1: basePath, s2: replacedFileName);

                return finalFilePath;
            }


            //check icons under VD_ICONS  first.
            basePath = rootPath + iconTag;

            if (isHighContrast == true)
            {
                basePath += highContrastTag;
            }

            finalFilePath = GetFinalFilePath(basePath, replacedFileName);

            if (finalFilePath != null)
            {
                //ConfigLogger.DebugP("** File is existed under VD_ICONS  : [%s1]", s1: finalFilePath);

                return finalFilePath;
            }

            //if icon is not found from VD_ICONS, check old path(../PRINCIPLE/) again.
            basePath = GetProductBasePath(CommonPath);
            finalFilePath = GetFinalFilePath(basePath, replacedFileName);

            if (finalFilePath == null)
            {
                throw new ArgumentNullException("Please check your filename [" + basePath + "/" + replacedFileName + "]");
            }

            return finalFilePath;
        }

        /// <summary>
        /// If you enter fileName , provide the common resource path.
        /// </summary>
        /// <code>
        /// imageBox.ResourceUrl  = ResourceUtility.GetCommonResourcePath("i_icon_function_apps");
        /// </code>
        /// <param name="fileName">Resource file name defined by UX Principle Guideline</param>
        /// <returns>CommonResource Path</returns>
        [Obsolete("GetCommonResourcePath(string fileName) is deprecated, please use GetCommonResourcePath(string fileName , ResourceSizes size) instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]

        public static string GetCommonResourcePath(string fileName)
        {
            return GetCommonResourcePath(fileName, ResourceSizes.Middle);
        }


        internal static string CommonPath => rootPath + principleTag;

        internal static string RootPath
        {
            get => rootPath;
            set
            {
                rootPath = value;
                ConfigLogger.DebugP("** rootPath[%s1]", s1: rootPath);

                if (string.IsNullOrEmpty(rootPath) == true)
                {
                    rootPath = defaultRootPath;
                }
                // reset cache
                Cache.Clear();
            }
        }

        private static string ChangeFileExtensionName(FileInfo originalFileName, string fromExtension, string toExtension)
        {
            string replacedFileName = originalFileName.FullName;

            if (string.Compare(originalFileName.Extension, fromExtension, StringComparison.OrdinalIgnoreCase) == 0)
            {
                replacedFileName = global::System.IO.Path.ChangeExtension(originalFileName.FullName, toExtension);
            }

            return replacedFileName;
        }

        private static string ChangeFileExtensionName(string originalFileName, string fromExtension, string toExtension)
        {
            string replacedFileName = originalFileName;
            if (originalFileName.EndsWith(fromExtension))
            {
                replacedFileName = global::System.IO.Path.ChangeExtension(originalFileName, toExtension);
            }

            return replacedFileName;
        }

        private static string GetFinalFilePath(string basePath, string fileName)
        {
            string finalFileName = fileName;

            if ((HasResourceExtension(fileName) == true && File.Exists(basePath + finalFileName)) || Directory.Exists(basePath + fileName) == true)
            {
                AddFileNameToCache(fileName, basePath + finalFileName);

                ConfigLogger.DebugP("[%s1]->[%s2]", s1: fileName, s2: basePath + finalFileName);
                return basePath + finalFileName;
            }


            finalFileName = fileName + ".svg";
            string result = basePath + finalFileName;

            if (File.Exists(result) == true)
            {
                ConfigLogger.DebugP("[%s1]->[%s2]", s1: fileName, s2: result);
                return result;
            }

            return null;
        }

        private static void AddFileNameToCache(string fileName, string filePath)
        {
            if (fileName != null && fileName != "" && filePath != null)
            {
                Cache[fileName] = filePath;
            }
        }

        private static string FindFileNameInCache(string fileName)
        {
            Cache.TryGetValue(fileName, out string filePath);
            //ConfigLogger.DebugP("in Cache: [%s1]", s1: filePath);
            return filePath;
        }

        private static string GetProductBasePath(string givenPath)
        {
            string retPath = null;

            if (IsFlux)
            {
                //ConfigLogger.DebugP("** isHighContrast: [%d1]", d1: Convert.ToInt32(isHighContrast));

                if (isHighContrast)
                {
                    retPath = givenPath + fluxHighContrastTag;
                }
                else
                {
                    retPath = givenPath + fluxTag;
                }
            }
            else
            {
                retPath = givenPath + nonFluxTag;
            }

            //ConfigLogger.DebugP("**isHighContrast[%d1] Ret Path: [%s1]", d1: Convert.ToInt32(isHighContrast), s1: retPath);

            return retPath;
        }

        private static bool HasResourceExtension(string fileName)
        {
            if (fileName == null)
            {
                return false;
            }

            FileInfo filename = new FileInfo(fileName);
            switch (filename.Extension?.ToLowerInvariant())
            {
                case ".webp": /* FALL THROUGH */
                case ".png":  /* FALL THROUGH */
                case ".json":  /* FALL THROUGH */
                case ".svg":  /* FALL THROUGH */
                case ".jpg":  /* FALL THROUGH */
                case ".jpeg": /* FALL THROUGH */
                case ".bmp":  /* FALL THROUGH */
                    return true;
                default:
                    return false;
            }
        }

        private static bool NeedToCheckHighDimension(string fileName)
        {
            if (fileName == null || IsHighDimension == false)
            {
                return false;
            }

            FileInfo filename = new FileInfo(fileName);
            switch (filename.Extension?.ToLowerInvariant())
            {
                case ".webp": /* FALL THROUGH */
                case ".png":  /* FALL THROUGH */
                case ".jpg":  /* FALL THROUGH */
                case ".jpeg": /* FALL THROUGH */
                case ".bmp":  /* FALL THROUGH */
                    return true;
                default:
                    return false;
            }
        }

        private static bool isFlux = false;
        private static bool isHighContrast = false;

        private static readonly string defaultRootPath = "/usr/apps/com.samsung.tv.theme-resource/shared/res/";
        private static string rootPath = "/usr/apps/com.samsung.tv.theme-resource/shared/res/";
        private static readonly string principleTag = "/PRINCIPLE/";
        private static readonly string principle4KTag = "/PRINCIPLE_4K/";

        private static readonly string iconTag = "/VD_ICONS/";
        // TODO: unused variable
        //private static readonly string smallTag = "_s";
        //private static readonly string largeTag = "_l";

        private static readonly string highContrastTag = "COMMON_HIGHCONTRAST/";
        private static readonly string fluxTag = "FLUX/";
        private static readonly string nonFluxTag = "NONFLUX/";
        private static readonly string fluxHighContrastTag = "FLUX_HighContrast/";

        private static readonly Dictionary<string, string> Cache = new Dictionary<string, string>();
    }
}
