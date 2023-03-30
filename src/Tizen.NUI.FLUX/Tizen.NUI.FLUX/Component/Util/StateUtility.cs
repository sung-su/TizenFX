/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file StateUtility.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.0.0 </version>
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


namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// 
    /// </summary>
    public static class StateUtility
    {
        /// <summary> Provide for user convenient </summary>
        public static readonly string StateAll = "StateAll";

        /// <summary> Provide for user convenient </summary>
        public static readonly string Normal = "Normal";
        /// <summary> Provide for user convenient </summary>
        public static readonly string Disabled = "Disabled";
        /// <summary> Provide for user convenient </summary>
        public static readonly string Selected = "Selected";
        /// <summary> Provide for user convenient . Basic include Normal / Disabled / Selected state</summary>
        /// <version>8.8.0</version>
        public static readonly string Basic = "Basic";
        /// <summary> Provide for user convenient </summary>
        /// <version>8.8.0</version>
        public static readonly string Focused = "Focused";
        /// <summary> Provide for user convenient </summary>
        /// <version>8.8.0</version>
        public static readonly string Checked = "Checked";
        /// <summary> Provide for user convenient </summary>
        internal static readonly string LongPressed = "LongPressed";
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string Pressed = "Pressed";



        /// <summary> Provide for user convenient </summary>
        public static readonly string NormalFocused = Normal + Focused;
        /// <summary> Provide for user convenient </summary>
        public static readonly string DisabledFocused = Disabled + Focused;
        /// <summary> Provide for user convenient </summary>
        public static readonly string SelectedFocused = Selected + Focused;
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string NormalPressed = Normal + Pressed;
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string DisabledPressed = Disabled + Pressed;
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string SelectedPressed = Selected + Pressed;
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string NormalFocusedPressed = Normal + Focused + Pressed;
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string DisabledFocusedPressed = Disabled + Focused + Pressed;
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string SelectedFocusedPressed = Selected + Focused + Pressed;

        /// <summary> Provide for user convenient </summary>
        public static readonly string NormalChecked = Normal + Checked;
        /// <summary> Provide for user convenient </summary>
        public static readonly string DisabledChecked = Disabled + Checked;
        /// <summary> Provide for user convenient </summary>
        public static readonly string SelectedChecked = Selected + Checked;
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string NormalCheckedPressed = Normal + Checked + Pressed;
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string DisabledCheckedPressed = Disabled + Checked + Pressed;
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string SelectedCheckedPressed = Selected + Checked + Pressed;

        /// <summary> Provide for user convenient </summary>
        public static readonly string NormalFocusedChecked = Normal + Focused + Checked;
        /// <summary> Provide for user convenient </summary>
        public static readonly string DisabledFocusedChecked = Disabled + Focused + Checked;
        /// <summary> Provide for user convenient </summary>
        public static readonly string SelectedFocusedChecked = Selected + Focused + Checked;
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string NormalFocusedCheckedPressed = Normal + Focused + Checked + Pressed;
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string DisabledFocusedCheckedPressed = Disabled + Focused + Checked + Pressed;
        /// <summary> Provide for user convenient </summary>
        /// <version>9.9.0</version>
        public static readonly string SelectedFocusedCheckedPressed = Selected + Focused + Checked + Pressed;

        internal static bool IsPredefinedState(string state)
        {
            if (state == Normal || state == Disabled || state == Selected
               || state == NormalFocused || state == DisabledFocused || state == SelectedFocused
               || state == NormalFocusedChecked || state == DisabledFocusedChecked || state == SelectedFocusedChecked)
            {
                return true;
            }

            return false;
        }

        internal static void UpdateDisabledOpacityValue(Component component, float opacityValue)
        {
            if (component != null)
            {
                component.UpdateStateProperty(StateUtility.Basic, "Opacity", Constant.OPACITY_ORIGINAL);
                component.UpdateStateProperty(StateUtility.Disabled, "Opacity", opacityValue);
                component.UpdateStateProperty(StateUtility.DisabledFocused, "Opacity", opacityValue);
                component.UpdateStateProperty(StateUtility.DisabledChecked, "Opacity", opacityValue);
                component.UpdateStateProperty(StateUtility.DisabledFocusedChecked, "Opacity", opacityValue);
            }
        }
        internal static void UpdatePressedOpacityValue(Component component, float opacityValue)
        {
            if (component != null)
            {
                component.UpdateStateProperty(StateUtility.Basic, "Opacity", Constant.OPACITY_ORIGINAL);
                component.UpdateStateProperty(StateUtility.Pressed, "Opacity", opacityValue);
            }
        }
    }
}
