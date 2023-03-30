/// @file SystemProperty.cs
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
using System.Runtime.InteropServices;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Provide the information of system's information and trigger the system events
    /// </summary>
    /// <code>
    /// //gets the value of the current UI direction
    /// SystemProperty.Instance.UIDirection;
    /// </code>
    public sealed class SystemProperty
    {
        //TODO: Need to check these vconf keys are available or not across all products.
        private const string highContrastKey = "db/menu/system/accessibility/highcontrast";
        private const string enlargeKey = "db/menu/system/accessibility/focuszoom";
        private const string languageKey = "db/menu_widget/language";
        //private Vconf.VconfCallBack languageHandler;
        //private Vconf.VconfCallBack highcontrastHandler;
        //private Vconf.VconfCallBack enlargeHandler;

        private bool cleanedup = false;

        private bool? highContrast = null;
        private bool? enlarge = null;
        private UIDirection? uiDirection = null;
        private string currentLanguage = null;

        /// <summary>
        /// get the instance of SystemProperty class
        /// </summary>
        public static SystemProperty Instance { get; } = new SystemProperty();

        /// <summary>
        /// get the value of highcontrast UI
        /// </summary>
        public bool HighContrast
        {
            get
            {
                if (highContrast == null)
                {
                    highContrast = GetVconfBool(highContrastKey);
                }

                return highContrast.Value;
            }
            private set => highContrast = value;
        }

        /// <summary>
        /// get the value of enlarge UI
        /// </summary>
        public bool Enlarge
        {
            get
            {
                if (enlarge == null)
                {
                    enlarge = GetVconfBool(enlargeKey);
                }

                return enlarge.Value;
            }
            private set => enlarge = value;
        }

        /// <summary>
        /// get the current UI direction
        /// </summary>
        public UIDirection UIDirection
        {
            get
            {
                if (uiDirection == null)
                {
                    uiDirection = GetUIDirection(CurrentLanguage);
                }

                return uiDirection.Value;
            }
            private set => uiDirection = value;
        }

        private string CurrentLanguage
        {
            get
            {
                //if (currentLanguage == null)
                //{
                //    currentLanguage = Vconf.GetString(languageKey);
                //}

                return currentLanguage;
            }
        }

        //Cleaning up unmanaged resources has to be done carefully and internally just before application exits. 
        //since it will make the instance unusable permanently.
        internal void CleanUp()
        {
            if (cleanedup != true)
            {
                //Vconf.IgnoreKeyChanged(enlargeKey, enlargeHandler);
                //Vconf.IgnoreKeyChanged(highContrastKey, highcontrastHandler);
                //Vconf.IgnoreKeyChanged(languageKey, languageHandler);
                //enlargeHandler = null;
                //highcontrastHandler = null;
                //languageHandler = null;

                highContrast = null;
                enlarge = null;
                uiDirection = null;
                currentLanguage = null;

                cleanedup = true;
            }
        }

        private SystemProperty()
        {
            SecurityUtil.CheckPlatformPrivileges();

            //highcontrastHandler = OnHighContrastChanged;
            //enlargeHandler = OnEnlargeChanged;
            //languageHandler = OnLanguageChanged;

            //Vconf.NotifyKeyChanged(highContrastKey, highcontrastHandler, IntPtr.Zero);
            //Vconf.NotifyKeyChanged(enlargeKey, enlargeHandler, IntPtr.Zero);
            //Vconf.NotifyKeyChanged(languageKey, languageHandler, IntPtr.Zero);
        }

        private void OnHighContrastChanged(IntPtr node, IntPtr userData)
        {
            //bool highcontrast = VconfNodeToBool(node);

            //if (highContrast != null && highContrast == highcontrast)
            //{
            //    return;
            //}

            //HighContrast = highcontrast;
        }

        private void OnEnlargeChanged(IntPtr node, IntPtr userData)
        {
            //bool enlarge = VconfNodeToBool(node);

            //if (this.enlarge != null && this.enlarge == enlarge)
            //{
            //    return;
            //}

            //Enlarge = enlarge;
        }

        private void OnLanguageChanged(IntPtr node, IntPtr userData)
        {
            //VconfKeyNode keyNode = Marshal.PtrToStructure<VconfKeyNode>(node);
            //string language = Marshal.PtrToStringAnsi(keyNode.strValue);

            //if (currentLanguage != null && currentLanguage == language)
            //{
            //    return;
            //}

            //currentLanguage = language;
            try
            {
                // Propagate RTL/LTR property
                UIDirection direction = GetUIDirection(currentLanguage);
                if (UIDirection != direction)
                {
                    UIDirection = direction;
                    if (FluxApplication.UIThreadSeparated == false)
                    {
                        PropagateLanguageChange(direction);
                    }
                    else
                    {
                        FluxSynchronizationContext.ToUIThread.Post(UIThreadLanguageChanged, UIDirection);
                    }
                }
            }
            catch (Exception e)
            {
                CLog.Error("Exception: %s1", s1: e.Message);
            }
        }

        private void UIThreadLanguageChanged(object obj)
        {
            if (obj is UIDirection direction)
            {
                PropagateLanguageChange(direction);
            }
        }

        private void PropagateLanguageChange(UIDirection direction)
        {
            List<Window> windowList = Application.GetWindowList();

            foreach (Window window in windowList)
            {
                uint layerCount = window.GetLayerCount();
                for (uint index = 0; index < layerCount; index++)
                {
                    Traverse(window.GetLayer(index), direction);
                }
            }
        }
        private void Traverse(Animatable obj, UIDirection parentDirection)
        {
            if (obj == null)
            {
                return;
            }

            if (obj is FluxView myFluxView)
            {

                uint childCount = myFluxView.ChildCount;
                for (uint index = 0; index < childCount; index++)
                {
                    if (myFluxView.InheritUIDirection == true)
                    {
                        myFluxView.UIDirection = parentDirection;
                        Traverse(myFluxView.GetChildAt(index), parentDirection);
                    }
                }
            }
            else if (obj is Layer myLayer)
            {
                uint childCount = myLayer.ChildCount;
                for (uint index = 0; index < childCount; index++)
                {
                    Traverse(myLayer.GetChildAt(index), parentDirection);
                }
            }
            else if (obj is View myView)
            {
                uint childCount = myView.ChildCount;
                for (uint index = 0; index < childCount; index++)
                {
                    Traverse(myView.GetChildAt(index), parentDirection);
                }
            }
            else
            {
                //A NUI Object that cannot have child objects.
                return;
            }
        }

        private bool GetVconfBool(string key)
        {
            return false;
            //int ret = Vconf.GetInt(key, out int value);
            //if (ret != (int)VconfErrorType.OK)
            //{
            //    CLog.Error("Vconf [%s1] Get error! Error type is %d1", s1: key, d1: ret);
            //}

            //return value != 0;
        }

        private UIDirection GetUIDirection(string language)
        {
            if (language != null && (
                language.StartsWith("ur_") ||
                language.StartsWith("fa_") ||
                language.StartsWith("ar_") ||
                language.StartsWith("he_") ||
                language.StartsWith("ckb_")))
            {
                return UIDirection.RTL;
            }

            return UIDirection.LTR;
        }

        //private static bool VconfNodeToBool(IntPtr node)
        //{
        //    VconfKeyNode keyNode = Marshal.PtrToStructure<VconfKeyNode>(node);
        //    bool ret = keyNode.boolValue == 1 ? true : false;
        //    return ret;
        //}
    }
}
