/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file ColorPresetManager.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.6.0 </version>
/// <SDK_Support> Y </SDK_Support>
///
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
using System.Text.Json;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// This is ColorPresetManager class.
    /// </summary>
    /// <code>
    /// ColorPresetManager.RegisterColorThemePreSet("CP_Info1100", new CPInfo1100());
    /// </code>
    public class ColorPresetManager
    {
        /// <summary>
        /// Get Colorpreset according to colorpreset Name
        /// </summary>
        /// <param name="colorPreset">ColorPreset Name (ex.CP_Info1100)</param>
        /// <returns>ColorPreset object</returns>
        /// <exception cref="ArgumentException">You enter wrong ColorPreSetName, ColorPreset start CP_</exception>
        public static ColorPreset GetColorPreset(string colorPreset)
        {
            if (colorpresetDic.TryGetValue(colorPreset, out ColorPreset preset) == false)
            {
                throw new ArgumentNullException($"You enter wrong ColorPreSetName, We don't have {colorPreset}. ColorPreset start CP_");
            }
            else
            {
                return preset.Clone();
            }
        }

        /// <summary>
        /// Register ColorPreset.
        /// </summary>
        /// <param name="userPresetName">Your ColorPreset name</param>
        /// <param name="colorPreset">Add ColorPreset object</param>
        /// <vesion> 9.9.0 </vesion>
        public static void RegisterColorPreset(string userPresetName, ColorPreset colorPreset)
        {
            if (colorpresetDic.ContainsKey(userPresetName) == false)
            {
                colorpresetDic.Add(userPresetName, colorPreset);
            }
            else
            {
                FluxLogger.ErrorP(" We already have %s1", s1: userPresetName);
            }
        }

        /// <summary>
        /// Change ColorPreset.
        /// </summary>
        /// <param name="userPresetName">Your ColorPreset name</param>
        /// <param name="colorPreset">Add ColorPreset object</param>
        /// <vesion> 9.9.0 </vesion>
        public static void ChangeColorPreset(string userPresetName, ColorPreset colorPreset)
        {
            if (colorpresetDic != null)
            {
                colorpresetDic[userPresetName] = colorPreset;
            }
        }

        /// <summary>
        /// Register ColorPreset.
        /// </summary>
        /// <param name="userPresetName">Your ColorPreset name</param>
        /// <param name="colorPreset">Add ColorPreset object</param>
        /// <deprecated> Deprecated since 9.9.0.  Please use  RegisterColorPreset</deprecated>
        [Obsolete("@Deprecated RegisterColorPreSet is deprecated. Please use RegisterColorPreset")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void RegisterColorPreSet(string userPresetName, ColorPreset colorPreset)
        {
            ColorPresetManager.RegisterColorPreset(userPresetName, colorPreset);
        }

        /// <summary>
        ///  User can update color preset table using json format file
        /// </summary>
        /// <param name="jsonFilePath"> It's json file path to update color preset</param>
        /// <version> 10.10.0 </version>
        public static void UpdateColorPresetTable(string jsonFilePath)
        {
            FluxLogger.InfoP("jsonFilePath :[%s1]", s1: jsonFilePath);
            if (jsonFilePath != null)
            {
                ParsingJsonFile(jsonFilePath);
            }
        }

        internal static string GetColorChipByState(string colorPreset, string state)
        {
            if (colorpresetDic.TryGetValue(colorPreset, out ColorPreset preset) == false)
            {
                throw new ArgumentNullException($"You enter wrong ColorPreSetName, We don't have {colorPreset}. ColorPreset start CP_");
            }
            else
            {
                return preset.ColorPresetSet[state];
            }
        }

        private class ColorPresetStateTable
        {
            public string name { get; set; }
            public string Normal { get; set; }
            public string NormalFocused { get; set; }
            public string Selected { get; set; }
            public string SelectedFocused { get; set; }
            public string Disabled { get; set; }
            public string DisabledFocused { get; set; }
            public string NormalPressed { get; set; }
            public string NormalFocusedPressed { get; set; }
            public string SelectedPressed { get; set; }
            public string SelectedFocusedPressed { get; set; }
            public string DisabledPressed { get; set; }
            public string DisabledFocusedPressed { get; set; }
        }
        private static void ParsingJsonFile(string jsonfile)
        {
            FluxLogger.InfoP("jsonFilePath :[%s1]", s1: jsonfile);
            string fileReadText = File.ReadAllText(jsonfile);

            if (fileReadText == null)
            {
                throw new InvalidOperationException($"jsonfile is null :{fileReadText}");
            }

            List<ColorPresetStateTable> listcolortable = JsonSerializer.Deserialize<List<ColorPresetStateTable>>(fileReadText);
            if (listcolortable == null)
            {
                throw new ArgumentNullException("ColorPreset data is null. check you json file");
            }
            FluxLogger.InfoP("ColorPreset Count :[%d1] ", d1: listcolortable.Count);
            foreach (ColorPresetStateTable colortable in listcolortable)
            {
                if (colortable != null)
                {
                    FluxLogger.InfoP("ColorPreset name = [%s1] ", s1: colortable.name);
                    ColorPreset colorPreset = new ColorPreset();
                    colorPreset.ColorPresetSet[StateUtility.Normal] = colortable.Normal;
                    colorPreset.ColorPresetSet[StateUtility.Selected] = colortable.Selected;
                    colorPreset.ColorPresetSet[StateUtility.Disabled] = colortable.Disabled;
                    colorPreset.ColorPresetSet[StateUtility.NormalFocused] = colortable.NormalFocused;
                    colorPreset.ColorPresetSet[StateUtility.SelectedFocused] = colortable.SelectedFocused;
                    colorPreset.ColorPresetSet[StateUtility.DisabledFocused] = colortable.DisabledFocused;
                    colorPreset.ColorPresetSet[StateUtility.NormalPressed] = colortable.NormalPressed;
                    colorPreset.ColorPresetSet[StateUtility.NormalFocusedPressed] = colortable.NormalFocusedPressed;
                    colorPreset.ColorPresetSet[StateUtility.SelectedPressed] = colortable.SelectedPressed;
                    colorPreset.ColorPresetSet[StateUtility.SelectedFocusedPressed] = colortable.SelectedFocusedPressed;
                    colorPreset.ColorPresetSet[StateUtility.DisabledPressed] = colortable.DisabledPressed;
                    colorPreset.ColorPresetSet[StateUtility.DisabledFocusedPressed] = colortable.DisabledFocusedPressed;
                    ColorPresetManager.RegisterColorPreset(colortable.name, colorPreset);
                }
            }

        }
        internal static Dictionary<string, ColorPreset> colorpresetDic = new Dictionary<string, ColorPreset>();
    }
}
