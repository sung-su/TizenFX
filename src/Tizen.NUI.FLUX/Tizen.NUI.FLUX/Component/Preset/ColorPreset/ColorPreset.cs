/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file ColorPreset.cs
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
using System.IO;
using System.Text.Json;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// This is ColorPreset class.
    /// </summary>
    /// <code>
    /// ColorPreset preset = ColorPresetManager.GetColorPreset(themeTextColorPreset);
    /// </code>
    public class ColorPreset
    {
        /// <summary>
        ///  Construct of ColorPreset
        /// </summary>
        public ColorPreset()
        {

        }

        /// <summary>
        ///  Construct of ColorPreset
        /// </summary>
        /// <param name="jsonFilePath">colorjson file path</param>
        /// <version>10.10.0</version>
        [Obsolete("@Deprecated ColorPreset is deprecated.")]
        public ColorPreset(string jsonFilePath)
        {
            FluxLogger.InfoP("jsonFilePath :%s1", s1: jsonFilePath);
            if (jsonFilePath != null)
            {
                ParsingJsonFile(jsonFilePath);
            }
        }
        /// <summary>
        /// Clone the ColorPreset object, each derived class need to override this method. 
        /// </summary>
        /// <returns>CplorPreset object</returns>
        public ColorPreset Clone()
        {
            ColorPreset clone = Activator.CreateInstance(GetType(), true) as ColorPreset;
            if (clone != null)
            {
                clone.ColorPresetSet = ColorPresetSet.Clone();
            }
            return clone;
        }
        /// <summary>
        /// When you add colorjson file, you can get ColorPresetName. 
        /// If you don't add jsonfile in construct, you get null.
        /// </summary>
        /// <version>10.10.0</version>
        public string ColorPresetName => colorPresetName;
        /// <summary>
        /// Set Colorpreset value
        /// </summary>
        public ColorPresetTable ColorPresetSet = new ColorPresetTable();

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
        private void ParsingJsonFile(string jsonfile)
        {
            FluxLogger.InfoP("jsonFile: %s1 ", s1: jsonfile);
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
            FluxLogger.InfoP("listcolortable.Count = %d1", d1: listcolortable.Count);
            foreach (ColorPresetStateTable colortable in listcolortable)
            {
                if (colortable != null)
                {
                    FluxLogger.InfoP("colortable.name = %s1", s1: colortable.name);
                    colorPresetName = colortable.name;
                    ColorPresetSet[StateUtility.Normal] = colortable.Normal;
                    ColorPresetSet[StateUtility.Selected] = colortable.Selected;
                    ColorPresetSet[StateUtility.Disabled] = colortable.Disabled;
                    ColorPresetSet[StateUtility.NormalFocused] = colortable.NormalFocused;
                    ColorPresetSet[StateUtility.SelectedFocused] = colortable.SelectedFocused;
                    ColorPresetSet[StateUtility.DisabledFocused] = colortable.DisabledFocused;
                    ColorPresetSet[StateUtility.NormalPressed] = colortable.NormalPressed;
                    ColorPresetSet[StateUtility.NormalFocusedPressed] = colortable.NormalFocusedPressed;
                    ColorPresetSet[StateUtility.SelectedPressed] = colortable.SelectedPressed;
                    ColorPresetSet[StateUtility.SelectedFocusedPressed] = colortable.SelectedFocusedPressed;
                    ColorPresetSet[StateUtility.DisabledPressed] = colortable.DisabledPressed;
                    ColorPresetSet[StateUtility.DisabledFocusedPressed] = colortable.DisabledFocusedPressed;
                }
            }

        }
        private string colorPresetName = null;

    }
}
