/// @file MultiShadowView.cs
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

using System.Collections.Generic;
using Tizen.NUI.BaseComponents;
using Tizen.NUI;
using System.Runtime.InteropServices;
using static Tizen.NUI.Renderer;
using System;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// MultiShadowView shows one or more shadows of its child
    /// </summary>
    public class MultiShadowView : FluxView
    {
        private class ShadowInfo
        {
            internal ShadowInfo(uint id = 0)
            {
                shadowId = id;
            }
            internal uint shadowId;
            internal bool activated = false;
        }

        private HandleRef nativeDynamicShadowHandle;
        private int mDepthIndex = Ranges.BackgroundEffect;

        /// <summary>
        /// Constructor
        /// </summary>
        public MultiShadowView()
        {
            SecurityUtil.CheckPlatformPrivileges();
            nativeDynamicShadowHandle = new HandleRef(this, IntPtr.Zero);
        }

        /// <summary>
        /// Dispose Function to clean up unmanaged resources.
        /// </summary>
        /// <param name="type">Disposing type</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }


            if (nativeDynamicShadowHandle.Handle != IntPtr.Zero)
            {
                Interop.DynamicDropShadow.RemoveShadow(nativeDynamicShadowHandle, 0);
                Interop.DynamicDropShadow.Delete(nativeDynamicShadowHandle);
            }
            dynamicShadowDictionary.Clear();

            base.Dispose(type);            
        }

        Dictionary<DynamicShadow, ShadowInfo> dynamicShadowDictionary = new Dictionary<DynamicShadow, ShadowInfo>();

        /// <summary>
        /// Function called when any Property of DynamicShadow is changed
        /// </summary>
        /// <param name="shadow"></param>
        /// <param name="propertyName"></param>
        private void DynamicShadowPropertyChanged(DynamicShadow shadow, string propertyName)
        {
            DynamicShadow dynamicShadow = shadow as DynamicShadow;
            ShadowInfo shadowInfo = null;
            var isShadowFound = dynamicShadowDictionary.TryGetValue(shadow, out shadowInfo);

            if (isShadowFound)
            {
                bool updateShadow = shadowInfo.activated;
                if (string.Equals(propertyName, "Offset"))
                {
                    Interop.DynamicDropShadow.SetShadowOffset(nativeDynamicShadowHandle, shadowInfo.shadowId, Vector2.getCPtr(dynamicShadow.Offset));
                }
                else if (string.Equals(propertyName, "Color"))
                {
                    Interop.DynamicDropShadow.SetShadowColor(nativeDynamicShadowHandle, shadowInfo.shadowId, Color.getCPtr(dynamicShadow.Color));
                    updateShadow = false;
                }
                else if (string.Equals(propertyName, "BlurSize"))
                {
                    Interop.DynamicDropShadow.SetShadowBlurSize(nativeDynamicShadowHandle, shadowInfo.shadowId, Convert.ToUInt32(dynamicShadow.BlurSize));
                }
                else if (string.Equals(propertyName, "BlurSigma"))
                {

                    Interop.DynamicDropShadow.SetShadowBlurSigma(nativeDynamicShadowHandle, shadowInfo.shadowId, dynamicShadow.BlurSigma);
                }
                else if (string.Equals(propertyName, "Size"))
                {
                    Interop.DynamicDropShadow.SetShadowArea(nativeDynamicShadowHandle, Vector2.getCPtr(dynamicShadow.Size));
                }

                if (updateShadow)
                {
                    DeactivateShadows();
                    ActivateShadows();
                }
            }
        }
        /// <summary>
        /// Add shadow
        /// Must call ActivateShadows after AddShadow to show shadow
        /// </summary>
        /// <param name="shadow">Shadow to be added</param>
        public void AddShadow(DynamicShadow shadow)
        {
            bool alreadyExist = dynamicShadowDictionary.ContainsKey(shadow);
            if (alreadyExist)
            {
                return;
            }

            if (nativeDynamicShadowHandle.Handle == IntPtr.Zero)
            {
                nativeDynamicShadowHandle = new HandleRef(this, Interop.DynamicDropShadow.New(View.getCPtr(this)));
            }
            uint shadowId = Interop.DynamicDropShadow.AddShadow(nativeDynamicShadowHandle);
            shadow.PropertyChanged += DynamicShadowPropertyChanged;
            DynamicShadow dynamicShadow = shadow as DynamicShadow;
            Interop.DynamicDropShadow.SetShadowArea(nativeDynamicShadowHandle, Vector2.getCPtr(dynamicShadow.Size));
            Interop.DynamicDropShadow.SetShadowColor(nativeDynamicShadowHandle, shadowId, Color.getCPtr(dynamicShadow.Color));
            Interop.DynamicDropShadow.SetShadowBlurSigma(nativeDynamicShadowHandle, shadowId, dynamicShadow.BlurSigma);
            Interop.DynamicDropShadow.SetShadowBlurSize(nativeDynamicShadowHandle, shadowId, Convert.ToUInt32(dynamicShadow.BlurSize));
            Interop.DynamicDropShadow.SetShadowOffset(nativeDynamicShadowHandle, shadowId, Vector2.getCPtr(dynamicShadow.Offset));
            dynamicShadowDictionary.Add(shadow, new ShadowInfo(shadowId));
            dirty = true;
        }

        /// <summary>
        /// Remove the passed shadow.
        /// </summary>
        /// <param name="shadow">Shadow to be removed. If null remove alll shadows</param>
        public void RemoveShadow(DynamicShadow shadow = null)
        {
            if (shadow == null)
            {
                foreach (KeyValuePair<DynamicShadow, ShadowInfo> item in dynamicShadowDictionary)
                {
                    DynamicShadow dynamicShadow = item.Key;
                    dynamicShadow.PropertyChanged -= DynamicShadowPropertyChanged;
                }
                mDepthIndex = Ranges.BackgroundEffect;
                Interop.DynamicDropShadow.RemoveShadow(nativeDynamicShadowHandle, 0);
                dynamicShadowDictionary.Clear();
                isShadowActivated = false;
            }
            else
            {
                ShadowInfo shadowInfo;
                bool isShadowFound = dynamicShadowDictionary.TryGetValue(shadow, out shadowInfo);
                if (isShadowFound)
                {
                    shadow.PropertyChanged -= DynamicShadowPropertyChanged;
                    Interop.DynamicDropShadow.RemoveShadow(nativeDynamicShadowHandle, shadowInfo.shadowId);
                    dynamicShadowDictionary.Remove(shadow);
                    if (dynamicShadowDictionary.Count == 0)
                    {
                        mDepthIndex = Ranges.BackgroundEffect;
                        isShadowActivated = false;
                    }
                }
            }
        }

        private bool isShadowActivated = false;
        private bool dirty = false;

        /// <summary>
        /// Show all the shadows added before this API is called.
        /// All the resources required for shadow rendering are allocated.
        /// </summary>
        public void ActivateShadows()
        {
            if (isShadowActivated && !dirty)
            {
                // do nothing if shadow is already activated & is not dirty
                return;
            }

            DeactivateShadows();
            if (dynamicShadowDictionary.Count > 0)
            {
                Interop.DynamicDropShadow.Activate(nativeDynamicShadowHandle);
                foreach (KeyValuePair<DynamicShadow, ShadowInfo> item in dynamicShadowDictionary)
                {
                    if (item.Value != null)
                    {
                        item.Value.activated = true;
                    }
                }
            }
            dirty = false;
            isShadowActivated = true;
        }

        /// <summary>
        /// Hide Shadows
        /// This API hides the shadow and deallocates all its resources.
        /// </summary>
        public void DeactivateShadows()
        {
            if (!isShadowActivated)
            {
                return;
            }

            if (dynamicShadowDictionary.Count > 0)
            {
                Interop.DynamicDropShadow.Deactivate(nativeDynamicShadowHandle);
                foreach (KeyValuePair<DynamicShadow, ShadowInfo> item in dynamicShadowDictionary)
                {
                    if (item.Value != null)
                    {
                        item.Value.activated = false;
                    }
                }
            }
            isShadowActivated = false;
        }
    }
}
