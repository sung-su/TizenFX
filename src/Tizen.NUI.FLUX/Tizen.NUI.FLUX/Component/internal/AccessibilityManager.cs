/// Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved 
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
using Tizen.NUI;

namespace Tizen.NUI.FLUX.Component
{
    internal class AccessibilityManager : BaseHandle
    {
        // TODO: Need to replace vconf
        //private Vconf.VconfCallBack highContrastChangedCallback;
        //private Vconf.VconfCallBack enlargeChangedCallback;

        public AccessibilityManager()
        {
            highContrast = GetVconfBool("db/menu/system/accessibility/highcontrast");
            enlarge = GetVconfBool("db/menu/system/accessibility/focuszoom");

            // TODO: Need to relace Vconf
            //highContrastChangedCallback = HighContrastChangedCB;
            //enlargeChangedCallback = EnlargeChangedCB;

            //Vconf.NotifyKeyChanged("db/menu/system/accessibility/highcontrast", highContrastChangedCallback, IntPtr.Zero);
            //Vconf.NotifyKeyChanged("db/menu/system/accessibility/focuszoom", enlargeChangedCallback, IntPtr.Zero);
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            // TODO: Need to replace Vconf
            //Vconf.IgnoreKeyChanged("db/menu/system/accessibility/focuszoom", enlargeChangedCallback);
            //Vconf.IgnoreKeyChanged("db/menu/system/accessibility/highcontrast", highContrastChangedCallback);

            //enlargeChangedCallback = null;
            //highContrastChangedCallback = null;

            base.Dispose(type);
        }

        private static readonly AccessibilityManager instance = new AccessibilityManager();

        public static AccessibilityManager Instance => instance;

        public bool HighContrast => highContrast;

        //public class HighContrastChangedEventArgs : EventArgs
        //{
        //    private bool highContrast;
        //    public bool HighContrast
        //    {
        //        get
        //        {
        //            return highContrast;
        //        }
        //        set
        //        {
        //            highContrast = value;
        //        }
        //    }
        //}

        private EventHandler highContrastChangedEventHandler;

        public event EventHandler HighContrastChanged
        {
            add
            {
                highContrastChangedEventHandler += value;
            }
            remove
            {
                highContrastChangedEventHandler -= value;
            }
        }

        private void HighContrastChangedCB(IntPtr node, IntPtr userData)
        {
            highContrast = GetVconfBool("db/menu/system/accessibility/highcontrast");

            ResourceUtility.IsHighContrast = highContrast;

#if Support_FLUXCore_Separated_UIThread
            if (FluxApplication.UIThreadSeparated == false)
            {
                highContrastChangedEventHandler?.Invoke(this, null);
            }
            else
            {
                FluxSynchronizationContext.ToUIThread.Post(UIThreadHighContrastChanged, this);
            }
#else
            highContrastChangedEventHandler?.Invoke(this, null);
#endif
        }

        private void UIThreadHighContrastChanged(object obj)
        {
            if (obj is AccessibilityManager accessibilityManager)
            {
                highContrastChangedEventHandler?.Invoke(accessibilityManager, null);
            }
        }

        public bool Enlarge => enlarge;

        //public class EnlargeChangedEventArgs : EventArgs
        //{
        //    private bool enlarge;
        //    public bool Enlarge
        //    {
        //        get
        //        {
        //            return enlarge;
        //        }
        //        set
        //        {
        //            enlarge = value;
        //        }
        //    }
        //}

        private EventHandler enlargeChangedEventHandler;

        public event EventHandler EnlargeChanged
        {
            add
            {
                enlargeChangedEventHandler += value;
            }
            remove
            {
                enlargeChangedEventHandler -= value;
            }
        }

        private void EnlargeChangedCB(IntPtr node, IntPtr userData)
        {
            enlarge = GetVconfBool("db/menu/system/accessibility/focuszoom");

#if Support_FLUXCore_Separated_UIThread
            if (FluxApplication.UIThreadSeparated == false)
            {
                enlargeChangedEventHandler?.Invoke(this, null);
            }
            else
            {
                FluxSynchronizationContext.ToUIThread.Post(UIThreadEnlargeChanged, this);
            }
#else
            enlargeChangedEventHandler?.Invoke(this, null);
#endif
        }

        private void UIThreadEnlargeChanged(object obj)
        {
            if (obj is AccessibilityManager accessibilityManager)
            {
                enlargeChangedEventHandler?.Invoke(this, null);
            }
        }

        private bool GetVconfBool(string key)
        {
            return false;
            // TODO: Need to replace Vconf
            //int ret = Vconf.GetInt(key, out int value);
            //if (ret != (int)VconfErrorType.OK)
            //{
            //    FluxLogger.ErrorP("Vconf %s1 get error!! error type is %d1", s1: key, d1: ret);
            //}

            //return value != 0;
        }

        private bool highContrast;
        private bool enlarge;
    }
}
