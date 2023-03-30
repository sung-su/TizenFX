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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using Tizen.NUI;


namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// This is ColorChipTable Class. It is SingleTon.
    /// </summary>
    /// <code>
    /// ColorChipTable.Instance.GetColor("CC_Basic1100");
    /// </code>
    public class ColorChipTable
    {
        private ColorChipTable()
        {
            Initialize();
        }
        #region public Property
        /// <summary>
        /// ColorChip instance (read-only) <br></br>
        /// Gets the current ColorChipTable object.
        /// </summary>
        public static ColorChipTable Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ColorChipTable();
                }
                return instance;
            }
        }
        #endregion public Property
        #region public Method
        /// <summary>
        /// You get directly Color value according to ColorChip
        /// </summary>
        /// <param name="colorChipName">ColorChip</param>
        /// <returns>Color value according to ColorChip</returns>
        /// <exception>You enter wrong colorchip</exception>
        public Color GetColor(string colorChipName)
        {
            if (colorChipName == null)
            {
                ConfigLogger.DebugP("GetColor - ColorchipName :[%s1]", s1: colorChipName);
                return null;
            }
            Color color = Color.Transparent;
            if (ResourceUtility.IsHighContrast == true)
            {
                if (HighcontrasColorChipDic.ContainsKey(colorChipName) == true)
                {
                    color = HighcontrasColorChipDic[colorChipName];
                }
                else if (ColorChipDic.ContainsKey(colorChipName) == true)
                {
                    color = ColorChipDic[colorChipName];
                }
                else
                {
                    throw new ArgumentException($"You enter wrong colorchip: {colorChipName}, ColorChip start CC_.");
                }
            }
            else
            {
                if (ColorChipDic.ContainsKey(colorChipName) == true)
                {
                    color = ColorChipDic[colorChipName];
                }
                else
                {
                    throw new ArgumentException($"You enter wrong colorchip: {colorChipName}, ColorChip start CC_.");
                }
            }
            //ConfigLogger.DebugP("colorchip: [%s1], color [%d1], [%d2], [%d3], [%d4]", s1: colorChipName, d1: ConfigLogger.GetColor(color.R), d2: ConfigLogger.GetColor(color.G), d3: ConfigLogger.GetColor(color.B), d4: ConfigLogger.GetColor(color.A));
            return color;
        }
        /// <summary>
        /// You can know if there is ColorChip
        /// </summary>
        /// <param name="colorChipName">ColorChip</param>
        /// <returns>True - Exist ColorChip / False - Not Exist ColorChip</returns>
        /// <version>10.10.0</version>
        public bool ExistColorChip(string colorChipName)
        {
            bool ret = false;
            if (colorChipName == null)
            {
                ConfigLogger.DebugP("ExistColorChip - ColorchipName :[%s1]", s1: colorChipName);
                return ret;
            }
            if (ColorChipDic.ContainsKey(colorChipName) == true)
            {
                ret = true;
            }
            return ret;
        }
        /// <summary>
        ///  User can update color chip table using json format file
        /// </summary>
        /// <param name="jsonFilePath"> It's json file path to update color chip</param>
        /// <version> 6.6.1 </version>
        public void UpdateColorChipTable(string jsonFilePath)
        {
            if (jsonFilePath != null)
            {
                ParsingJsonFile(jsonFilePath);
            }
        }

        /// <summary>
        /// You get directly Color value according to ColorChip in theme.
        /// </summary>
        /// <param name="theme">Theme name</param>
        /// <param name="colorChipName">ColorChip name</param>
        /// <returns>Color value according to ColorChip</returns>
        /// <exception>You enter wrong colorchip</exception>
        [Obsolete("GetColor(string theme, string colorChipName) is deprecated, please use GetColor(string colorChipName) instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Color GetColor(string theme, string colorChipName)
        {
            if (theme == null || colorChipName == null)
            {
                ConfigLogger.DebugP("GetColor - theme:[%s1], ColorchipName :[%s2]", s1: theme, s2: colorChipName);

                return null;
            }
            Color color = Color.Transparent;
            if (ResourceUtility.IsHighContrast == true)
            {
                if (HighcontrasColorChipDic.ContainsKey(colorChipName) == true)
                {
                    color = HighcontrasColorChipDic[colorChipName];
                }
                else if (ColorChipDic.ContainsKey(colorChipName) == true)
                {
                    color = ColorChipDic[colorChipName];
                }
                else
                {
                    throw new ArgumentException($"You enter wrong colorchip: {colorChipName}, ColorChip start CC_.");
                }
            }
            else
            {
                if (ColorChipDic.ContainsKey(colorChipName) == true)
                {
                    color = ColorChipDic[colorChipName];
                }
                else
                {
                    throw new ArgumentException($"You enter wrong colorchip: {colorChipName}, ColorChip start CC_.");
                }
            }
            ConfigLogger.DebugP("colorchip: [%s1], color [%d1], [%d2], [%d3], [%d4]", s1: colorChipName, d1: ConfigLogger.GetColor(color.R), d2: ConfigLogger.GetColor(color.G), d3: ConfigLogger.GetColor(color.B), d4: ConfigLogger.GetColor(color.A));
            return color;
        }
        #endregion public Method
        #region private Method
        private class ColorTable
        {
            public string Chipname { get; set; }
            public string Color { get; set; }
            public string Opacity { get; set; }
            public string HCColor { get; set; }
            public string HCOpacity { get; set; }
        }
        private void Initialize()
        {
            if (UIConfig.MainThreadID != Thread.CurrentThread.ManagedThreadId)
            {
                ConfigLogger.DebugP("UIConfig.MainThreadID :[%d1], Thread.CurrentThread.ManagedThreadId:[%d2]", d1: UIConfig.MainThreadID, d2: Thread.CurrentThread.ManagedThreadId);
                throw new Exception($"ColorTable is called in user thread (MainThreadID {UIConfig.MainThreadID}, ManagedThreadId {Thread.CurrentThread.ManagedThreadId})");
            }
            try
            {
                ParsingJsonFile(ResourceUtility.CommonPath + "COLOR_TABLE/" + principleColortable + ".json");
                ParsingJsonFile(ResourceUtility.CommonPath + "COLOR_TABLE/" + appCustomColortable + ".json");
            }
            catch (Exception exception)
            {
                ConfigLogger.DebugP("exception:[%s1]", s1: exception.Message);
                string commonPath = "/usr/apps/com.samsung.tv.theme-resource/shared/res//PRINCIPLE/";
                ParsingJsonFile(commonPath + "COLOR_TABLE/" + principleColortable + ".json");
                ParsingJsonFile(commonPath + "COLOR_TABLE/" + appCustomColortable + ".json");
            }
            ColorChipDic.Add(OverlayDefaultColor, Color.White);
            HighcontrasColorChipDic.Add(OverlayDefaultColor, Color.White);
        }
        private uint[] ConvertUIntColor(string colortext)
        {
            uint[] returnColor = new uint[3];
            string[] temp = new string[3];
            int j = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = colortext.Substring(j, 2);
                j += 2;
            }
            for (int i = 0; i < temp.Length; i++)
            {
                returnColor[i] = Convert.ToUInt32(temp[i], 16);
            }
            return returnColor;
        }
        private void ParsingJsonFile(string jsonfile)
        {
            string fileReadText = File.ReadAllText(jsonfile);

            if (fileReadText != null)
            {
                List<ColorTable> listcolortable = new List<ColorTable>();
                ParseString(fileReadText, ref listcolortable);

                if (listcolortable != null)
                {
                    ConfigLogger.DebugP("jsonFile[%s1], listcolortable.Count = :[%d1]", s1: jsonfile, d1: listcolortable.Count);
                    foreach (ColorTable colortable in listcolortable)
                    {
                        if (colortable != null)
                        {
                            uint[] convertColor = ConvertUIntColor(colortable.Color.Replace("#", ""));
                            Color color = new Color(convertColor[0] / 255.0f, convertColor[1] / 255.0f, convertColor[2] / 255.0f, Convert.ToInt32(colortable.Opacity) * 0.01f);

                            ColorChipDic[colortable.Chipname] = color;
                            //ConfigLogger.DebugP("Normal Chipname: [%s1], color [%d1], [%d2], [%d3], [%d4]", s1: colortable.Chipname, d1: ConfigLogger.GetColor(color.R), d2: ConfigLogger.GetColor(color.G), d3: ConfigLogger.GetColor(color.B), d4: ConfigLogger.GetColor(color.A));

                            uint[] convertHcColor = ConvertUIntColor(colortable.HCColor.Replace("#", ""));
                            Color hccolor = new Color(convertHcColor[0] / 255.0f, convertHcColor[1] / 255.0f, convertHcColor[2] / 255.0f, Convert.ToInt32(colortable.HCOpacity) * 0.01f);

                            HighcontrasColorChipDic[colortable.Chipname] = hccolor;
                            //ConfigLogger.DebugP("Highcontrast [%d1], [%d2], [%d3], [%d4]", d1: ConfigLogger.GetColor(hccolor.R), d2: ConfigLogger.GetColor(hccolor.G), d3: ConfigLogger.GetColor(hccolor.B), d4: ConfigLogger.GetColor(hccolor.A));
                        }
                    }
                }
            }
            else
            {
                new IOException($"jsonfile is null :{fileReadText}");
            }
        }

        private void ParseString(string fileStr, ref List<ColorTable> ctList)
        {
            char[] separators = { '[', ']', '\r', '\n', ' ', '"', ':', ',', '{', '}' };
            string[] values = fileStr.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            string chipName, color, opacity, hcColor, hcOpacity;
            chipName = color = opacity = hcColor = hcOpacity = null;

            int i = 0;
            foreach (string str in values)
            {
                if (str == CHIPNAME)
                {
                    chipName = values[i + 1];
                }
                else if (str == COLOR)
                {
                    color = values[i + 1];
                }
                else if (str == OPACITY)
                {
                    opacity = values[i + 1];
                }
                else if (str == HCCOLOR)
                {
                    hcColor = values[i + 1];
                }
                else if (str == HCOPACITY)
                {
                    hcOpacity = values[i + 1];
                }
                i++;

                if (chipName != null && color != null && opacity != null && hcColor != null && hcOpacity != null)
                {
                    ColorTable colorTable = new ColorTable
                    {
                        Chipname = chipName,
                        Color = color,
                        Opacity = opacity,
                        HCColor = hcColor,
                        HCOpacity = hcOpacity
                    };
                    ctList.Add(colorTable);
                    chipName = color = opacity = hcColor = hcOpacity = null;
                }
            }
        }
        #endregion private Method
        internal readonly string OverlayDefaultColor = "OverlayColorWhite";
        private readonly string principleColortable = "principle_colortable";
        private readonly string appCustomColortable = "app_custom_colortable";
        private static readonly Dictionary<string, Color> ColorChipDic = new Dictionary<string, Color>();
        private static readonly Dictionary<string, Color> HighcontrasColorChipDic = new Dictionary<string, Color>();
        private static ColorChipTable instance = null;
        private const string CHIPNAME = "Chipname";
        private const string COLOR = "Color";
        private const string OPACITY = "Opacity";
        private const string HCCOLOR = "HCColor";
        private const string HCOPACITY = "HCOpacity";
    }
}
