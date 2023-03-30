/// @file BlurCapture.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 10.10.1 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2023 Samsung Electronics Co., Ltd All Rights Reserved
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
/// 
using System;
using System.Collections.Generic;
using System.Text;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Singleton class. Links BlurCapture to Dali Native side.
    /// </summary>
    /// <code>
    /// Initialize Singleton instance of BlurCapture
    /// BlurCapture.Instance.Initialize();
    /// Apply PostInitialize EventHandler
    /// BlurCapture.Instance.InitializeFinished += CreateBlurView;
    /// CreateBlurView
    /// blurView = new BlurView(BlurType.Dynamic);
    /// blurView.BlurStyle = BlurView.BlurViewStyle.Medium;
    /// blurView.UnitPosition = new UnitPosition(50, 50);
    /// blurView.UnitSizeWidth = 50;
    /// blurView.UnitSizeHeight = 50;
    /// blurView.AlternativeImageURL = CommonResource.GetLocalResourceURL() + "alter-img.jpg";
    /// blurView.AlphaMaskUrl = CommonResource.GetLocalResourceURL() + "AlphaMask.png";
    /// blurView.AlternativeDimColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    /// blurView.DimmingLevel = 0.5f;
    /// Window.Instance.GetDefaultLayer().Add(blurView);
    /// blurView.Activate();
    /// blurView.Pause();
    /// blurView.Resume();
    /// blurView.Deactivate();
    /// </code>

    public class BlurCapture
    {
        /// <summary>
        /// Enumeration states for Native DSM Engine 
        /// </summary>
        public enum States
        {
            /// <summary>
            /// DSM Engine is not ready.
            /// </summary>
            NotReady,
            /// <summary>
            /// DSM Engine is Initialized.
            /// </summary>
            Initialized
        }
        /// <summary>
        /// State of Native DSM Engine
        /// </summary>
        public States State
        {
            get;
            set;
        }
        /// <summary>
        /// Add / Remove Post Initialization EventHandler 
        /// </summary>
        public event EventHandler<EventArgs> InitializeFinished
        {
            add
            {
                eventHandler += value;
                if (State == States.Initialized)
                {
                    value.Invoke(this, null);
                }
            }
            remove
            {
                eventHandler -= value;
            }
        }
        /// <summary>
        /// Returns the singleton instance of the BlurCapture
        /// </summary>
        /// <returns> BlurCapture singleton instance</returns>
        public static BlurCapture Instance => instance;

        /// <summary>
        /// Initializes Blur Capture Asynchronously.
        /// Post initialization, user's registered InitializeFinished will be called if there is any.
        /// </summary>
        public void Initialize()
        {
            initCallback = new Interop.BlurCapture.InitCallback(InitializeAsyncCallback);
            Interop.BlurCapture.BlurCaptureInitialize(initCallback);
        }

        private static Interop.BlurCapture.InitCallback initCallback;

        private BlurCapture() 
        {
            State = States.NotReady;
        }
		
        private void InitializeAsyncCallback()
        {
            State = States.Initialized;
            eventHandler?.Invoke(this, null);
        }

        private EventHandler<EventArgs> eventHandler;
        private static readonly BlurCapture instance = new BlurCapture();
    }
}
