/// @file BlurView.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 8.8.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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
using System.ComponentModel;
using System.Runtime.InteropServices;
using Tizen.System;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// BlurView allows user to blur Video or Graphic Content under the area
    /// </summary>
    /// <code>
    /// Create BlurView
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
    public partial class BlurView : FluxView
    {
        private bool isActivated = false;
        private BlurViewStyle blurStyle = BlurViewStyle.Medium;
        private BlurViewType blurType = BlurViewType.Static;
        private string resourceUrl = null;
        private string alphaMaskUrl = null;
        private float blurIntensity = 1.0f;
        private uint graphicCaptureInterval = 1000;
        private readonly bool isBlurAvailable = true;
        private Color alternativeDimColor = new Color(0.0f, 0.0f, 0.0f, 0.75f);
        private float dimmingLevel = 0.5f;
        private const string uifwThemeTypeFmsKey = "com.samsung/featureconf/uifw.theme_type";
        private const string androidOnTizenSupportFmsKey = "com.samsung/featureconf/android_on_tizen.support";

        /// <summary>
        /// BlurView Constructor
        /// </summary>
        /// <param name = "type">BlurView Type. You can choose the type by considering the blurred target object.</param>
        /// <param name = "style">BlurView Style. Blurring Algorithm can be affected by the style. </param>
        /// <remarks>
        /// The BlurViewStyle default value changed to Medium. Sinsce 9.9.0.
        /// Downloadable apps must explicitly set the BlurViewStyle.
        /// </remarks>
        public BlurView(BlurViewType type = BlurViewType.Static, BlurViewStyle style = BlurViewStyle.Medium) : this(Interop.BlurView.New((uint)type, (uint)style), true)
        {
            SecurityUtil.CheckPlatformPrivileges();
            BlurType = type;
            BlurStyle = style;
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            GeometryChanged += OnGeometryChanged;
            isBlurAvailable = IsBlurAvailable();
        }

        private void OnGeometryChanged(object sender, EventArgs e)
        {
            Vector4 screenExtents = TransformationUtil.GetScreenExtents(this);
            Interop.BlurView.GeometryUpdated(SwigCPtr, screenExtents.X, screenExtents.Y, screenExtents.Z, screenExtents.W);
        }

        internal BlurView(IntPtr cPtr, bool cMemoryOwn) : base(Interop.BlurView.Upcast(cPtr), cMemoryOwn)
        {

        }

        /// <summary>
        /// Set/Get Blur Intensity
        /// </summary>
        internal float Intensity
        {
            set
            {
                blurIntensity = MathUtil.Clamp(value, 10.0f, 1.0f);
                Interop.BlurView.SetBlurIntensity(SwigCPtr, blurIntensity);
            }

            get => blurIntensity;
        }

        /// <summary>
        /// Enumerator for blur view type. 
        /// </summary>
        public enum BlurViewType
        {
            /// <summary>
            /// Type that blurs Static Content
            /// </summary>
            Static,
            /// <summary>
            /// Type that blurs Non-Static Content
            /// </summary>
            Dynamic
        }

        /// <summary>
        /// Enumerator for blur algorithm style
        /// </summary>
        public enum BlurViewStyle
        {
            /// <summary>
            /// Light Blur Style
            /// </summary>            
            Light = 6,
            /// <summary>
            /// Medium Blur Intensity (Default).
            /// </summary>
            /// <version> 9.9.0 </version>
            Medium = 8,
            /// <summary>
            /// Heavy Blur Intensity
            /// </summary>
            /// <version> 9.9.0 </version>
            Heavy = 10
        }

        /// <summary>
        /// Release Native Handle
        /// </summary>        
        /// <param name="swigCPtr">HandleRef object holding corresponding native pointer.</param>
        /// <version>9.9.0</version>
        protected override void ReleaseSwigCPtr(HandleRef swigCPtr)
        {
            Interop.BlurView.Delete(swigCPtr);
        }

        /// <summary>
        /// Cleaning up managed and unmanaged resources 
        /// </summary>
        /// <param name = "type">Disposing type</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            base.Dispose(type);
        }

        /// <summary>
        ///  Resumes Capturing and Blurring of frames
        /// </summary>
        public void Resume()
        {
            Interop.BlurView.Resume(SwigCPtr);
        }

        /// <summary>
        ///  Pauses Capturing and Blurring of frames
        /// </summary>
        public void Pause()
        {
            Interop.BlurView.Pause(SwigCPtr);
        }

        /// <summary>
        /// Activate Blur
        /// </summary>
        public void Activate()
        {
            if (isBlurAvailable == false)
            {
                ApplyDimColor();
                return;
            }

            if (isActivated == false)
            {
                Interop.BlurView.Activate(SwigCPtr);
                isActivated = true;
            }
        }

        /// <summary>
        /// Activate with capture result store mode
        /// </summary>
        /// <remarks>
        /// It is not common feature. It is eden ux scenario dependent.                
        /// </remarks>
        /// <version> 10.10.1 </version>
        /// <param name="enableCaptureStore"> Enable store capture result </param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Activate(bool enableCaptureStore)
        {
            if (isBlurAvailable == false)
            {
                ApplyDimColor();
                return;
            }

            if (isActivated == false)
            {
                Interop.BlurView.Activate(SwigCPtr, enableCaptureStore);
                isActivated = true;
            }
        }

        /// <summary>
        /// Deactivate Blur
        /// </summary>
        public void Deactivate()
        {
            BackgroundColor = Color.Transparent;

            if (isActivated == true)
            {
                Interop.BlurView.Deactivate(SwigCPtr);
                isActivated = false;
            }
        }

        private BlurViewType privateBlurType
        {
            set
            {
                if (value != blurType)
                {
                    blurType = value;
                    Interop.BlurView.SetBlurType(SwigCPtr, (uint)blurType);
                }
            }

            get => blurType;
        }

        /// <summary>
        /// BlurView Type 
        /// </summary>
        public BlurViewType BlurType
        {
            get => (BlurViewType)GetValue(BlurTypeProperty);

            set => SetValue(BlurTypeProperty, value);
        }

        private BlurViewStyle privateBlurStyle
        {
            set
            {
                if (value != blurStyle)
                {
                    blurStyle = value;
                    Interop.BlurView.SetBlurIntensity(SwigCPtr, (float)blurStyle);
                }
            }

            get => blurStyle;
        }

        /// <summary>
        /// BlurView Style
        /// </summary>
        public BlurViewStyle BlurStyle
        {
            set => SetValue(BlurStyleProperty, value);

            get => (BlurViewStyle)GetValue(BlurStyleProperty);
        }

        private string privateAlternativeResourceUrl
        {
            set
            {
                string url = value ?? "";

                if (!url.Equals(resourceUrl))
                {
                    resourceUrl = url;
                    Interop.BlurView.SetAlternativeResourceUrl(SwigCPtr, resourceUrl);
                }
            }

            get => resourceUrl;
        }

        /// <summary>
        /// Set Alternative Resource URL.
        /// An image URL. The image will be displayed when the blur effect is absent during turn-off. 
        /// </summary>
        public string AlternativeResourceUrl
        {
            set => SetValue(AlternativeResourceUrlProperty, value);

            get => (string)GetValue(AlternativeResourceUrlProperty);
        }

        private string privateAlphaMaskUrl
        {
            set
            {
                string url = value ?? "";

                if (!url.Equals(alphaMaskUrl))
                {
                    alphaMaskUrl = url;
                    Interop.BlurView.SetGradientAlphaUrl(SwigCPtr, alphaMaskUrl);
                }
            }

            get => alphaMaskUrl;
        }

        /// <summary>
        /// Set AlphaMask Resource Url
        /// </summary>
        public string AlphaMaskUrl
        {
            set => SetValue(AlphaMaskUrlProperty, value);

            get => (string)GetValue(AlphaMaskUrlProperty);
        }

        private Color privateAlternativeDimColor
        {
            set
            {
                alternativeDimColor = value;
                Interop.BlurView.SetAlternativeDimColor(SwigCPtr, Color.getCPtr(alternativeDimColor));
            }

            get => alternativeDimColor;
        }

        /// <summary>
        /// Set Alternative Dim Color.
        /// The color will be displayed when the blur effect not supported case. 
        /// If user does not set dim color, then UX defined default color will be displayed not supported case.
        /// Not supported case 1. Lite model
        /// Not supported case 2. Multi view scenario
        /// Not supported case 3. Blur failed
        /// </summary>
        /// <version> 9.9.0 </version>
        public Color AlternativeDimColor
        {
            set => SetValue(AlternativeDimColorProperty, value);

            get => (Color)GetValue(AlternativeDimColorProperty);
        }

        private float privateDimmingLevel
        {
            set
            {
                dimmingLevel = value;
                Interop.BlurView.SetDimmingLevel(SwigCPtr, dimmingLevel);
            }

            get => dimmingLevel;
        }

        /// <summary>
        /// Set Blur Dimming Level.
        /// The original color is 1, and it gets darker as it goes to 0.
        /// The range is from 0.0 to 1.0. 
        /// Default value is 0.5.        
        /// </summary>
        /// <version>10.10.0</version>
        public float DimmingLevel
        {
            set
            {
                if (value >= 0.0f && value <= 1.0f)
                {
                    SetValue(DimmingLevelProperty, value);
                }
            }
            get => (float)GetValue(DimmingLevelProperty);
        }

        /// <summary>
        /// Show stored capture result and pause blur and capture thread.
        /// </summary>
        /// <remarks>
        /// It is not common feature. It is eden ux scenario dependent.
        /// </remarks>
        /// <version> 10.10.1 </version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ShowStoredCapture()
        {
            Interop.BlurView.ShowStoredCapture(SwigCPtr);
        }

        internal uint GraphicCaptureInterval
        {
            set
            {
                graphicCaptureInterval = value;
                Interop.BlurView.SetGraphicCaptureInterval(SwigCPtr, graphicCaptureInterval);
            }

            get => graphicCaptureInterval;
        }

        private bool IsBlurAvailable()
        {
            try
            {
                string modelType;
                bool isAndroidOnTizen;

                if (Information.TryGetValue(uifwThemeTypeFmsKey, out modelType) && Information.TryGetValue(androidOnTizenSupportFmsKey, out isAndroidOnTizen))
                {
                    if (modelType.Equals("lite") || isAndroidOnTizen)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (PlatformNotSupportedException e)
            {
                CLog.Error("Failed to get FMS Key: %s1", s1: e.Message);
                return false;
            }
        }

        private void ApplyDimColor()
        {
            BackgroundColor = alternativeDimColor;
        }
    }
}