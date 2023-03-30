/**
*Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
*For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
*/
/// @file ThemeColor.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 9.9.0</version>
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
/// this

using System.Collections.Generic;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// This is line ColorType of ThemeColor
    /// </summary>
    /// supporting xaml
    /// <version> 10.10.0 </version>
    public enum ThemeColorType
    {
        /// <summary>
        /// ColorChip of ThemeColor
        /// </summary>
        ColorChip,

        /// <summary>
        /// ColorPreset of ThemeColor
        /// </summary>
        ColorPreset
    }

    /// <summary>
    /// This is class of KeyValueOfThemeColor
    /// </summary>
    /// supporting xaml
    /// <code>
    /// private readonly List<KeyValueOfThemeColor> keyValueList = new List<KeyValueOfThemeColor>();
    /// keyValueList.Add(info);
    /// </code>
    /// <version> 10.10.0 </version>
    public class KeyValueOfThemeColor
    {
        /// <summary>
        /// PropertyName of component
        /// </summary>
        public string PropertyName
        {
            get;
            set;
        }

        /// <summary>
        /// ColorValue, start with "CC_" or "CP_"
        /// </summary>
        public string ColorValue
        {
            get;
            set;
        }

        /// <summary>
        /// ColorType, ColorChip or ColorPreset
        /// </summary>
        public ThemeColorType ColorType
        {
            get;
            set;
        }
    }

    /// <summary>
    /// This is class of ThemeColor
    /// </summary>
    public class ThemeColor
    {
        /// <summary>
        /// Constructor for ThemeColor
        /// </summary>
        /// supporting xaml
        /// <version>10.10.0</version>
        public ThemeColor()
        {
            IsRoot = true;
        }

        /// <summary>
        /// Add function for ThemeColor
        /// </summary>
        /// supporting xaml
        public void Add(KeyValueOfThemeColor info)
        {
            if (requirer != null)
            {
                if (info.ColorType == ThemeColorType.ColorChip)
                {
                    this[info.PropertyName].ColorChip = info.ColorValue;
                }
                else
                {
                    this[info.PropertyName].ColorPreset = info.ColorValue;
                }

                return;
            }

            keyValueList.Add(info);
        }

        /// <summary>
        /// Indexer for user usability
        /// </summary>
        public ThemeColor this[string property]
        {
            get
            {
                if (IsRoot == false)
                {
                    return null;
                }

                if (themeColorList.TryGetValue(property, out ThemeColor propertyThemeColor) == false)
                {
                    propertyThemeColor = new ThemeColor(requirer, property);
                    themeColorList[property] = propertyThemeColor;
                }

                return propertyThemeColor;
            }
        }

        /// <summary>
        /// Color Chip value which starts with "CC_"
        /// </summary>
        public string ColorChip
        {
            get => colorChip;
            set
            {
                colorChip = value;
                UpdateColor();
            }
        }

        /// <summary>
        /// Color Preset value which starts with "CP_"
        /// </summary>
        public string ColorPreset
        {
            get => colorPreset;
            set
            {
                colorPreset = value;
                UpdateColor();
            }
        }

        internal Component Requirer
        {
            set
            {
                requirer = value;

                foreach (KeyValueOfThemeColor keyValue in keyValueList)
                {
                    if (keyValue.ColorType == ThemeColorType.ColorChip)
                    {
                        this[keyValue.PropertyName].ColorChip = keyValue.ColorValue;
                    }
                    else
                    {
                        this[keyValue.PropertyName].ColorPreset = keyValue.ColorValue;
                    }
                }
            }
        }

        internal ThemeColor(Component requirer)
        {
            this.requirer = requirer;
            IsRoot = true;
        }

        private ThemeColor(Component requirer, string property)
        {
            this.requirer = requirer;
            this.property = property;
        }

        internal void UpdateState()
        {
            if (themeColorList != null)
            {
                foreach (KeyValuePair<string, ThemeColor> item in themeColorList)
                {
                    item.Value?.UpdateColor();
                }
            }
        }

        internal void Clear()
        {
            foreach (KeyValuePair<string, ThemeColor> item in themeColorList)
            {
                item.Value?.ClearChild();
            }
            themeColorList.Clear();
            themeColorList = null;
        }

        private void ClearChild()
        {
            ThemeHelper.Instance.UnMapColorChip(requirer, property);
            requirer = null;
            property = null;
            colorChip = null;
            colorPreset = null;
        }

        private void UpdateColor()
        {
            if (IsRoot == true || requirer == null || property == null)
            {
                return;
            }
            string currentColorChip = colorChip;

            if (colorChip == null && colorPreset != null)
            {
                string state = requirer.State.PresetState;

                if (UIConfig.SupportPointingMode == UIConfig.PointingMode.SupportTouchOnly)
                {
                    state = requirer.State.PresetState.Replace(StateUtility.Focused, "");
                }

                currentColorChip = ColorPresetManager.GetColorChipByState(colorPreset, state);

            }

            if (currentColorChip != null)
            {
                ThemeHelper.Instance.MapColorChip(requirer, property, currentColorChip);
            }
            else
            {
                ThemeHelper.Instance.UnMapColorChip(requirer, property);
            }
        }


        private Dictionary<string, ThemeColor> themeColorList = new Dictionary<string, ThemeColor>();
        private readonly List<KeyValueOfThemeColor> keyValueList = new List<KeyValueOfThemeColor>();

        private Component requirer = null;
        private string property = null;
        private string colorChip = null;
        private string colorPreset = null;

        private readonly bool IsRoot = false;
    }
}
