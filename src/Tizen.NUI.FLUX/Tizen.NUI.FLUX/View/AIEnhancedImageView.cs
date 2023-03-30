/// @file AIEnhancedImageView.cs
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
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// AIEnhancedImageView enable user to apply different PictureEnhancer algorithm, 
    /// on provided image resource, and display it.
    /// </summary>
    /// <code>
    /// AIEnhancedImageView view = new AIEnhancedImageView(AIEnhancedImageView.PictureEnhancerType.OneToOneEnhancement, 1000);;
    /// view.ResourceUrl = CommonResource.GetLocalResourceURL() + "gallery-4.jpg";
    /// </code>
    /// <deprecated>
    /// Deprecated since 10.10.0. Use ImageBox instead.
    /// </deprecated>
    [Obsolete("AIEnhancedImageView is deprecated, please use ImageBox instead.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial class AIEnhancedImageView : FluxImageView
    {
        /// <summary>
        /// Enumerator for PictureEnhancement Algorithm type
        /// </summary>
        public enum PictureEnhancerType : uint
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0,
            /// <summary>
            /// OneToOneEnhancement
            /// </summary>
            OneToOneEnhancement = 1,
        }

        /// <summary>
        /// Enumeration for EnhancedImage Resource Loading Status
        /// </summary>
        public enum EnhancedImageResourceLoading : uint
        {
            /// <summary>
            /// Success
            /// </summary>
            Success,
            /// <summary>
            /// Fail
            /// </summary>
            Fail
        }

        /// <summary>
        /// Enumerator for EnhancedImageLoading status
        /// </summary>
        /// <code>
        /// AIEnhancedImageView enhancedImageview = new AIEnhancedImageView(AIEnhancedImageView.PictureEnhancerType.OneToOneEnhancement, 5000);
        /// enhancedImageview.EnhancedImageResourceLoadingEvent += EnhancedImageViewSample_EnhancedImageResourceLoadingEvent;
        /// ...
        /// ...
        /// private void EnhancedImageViewSample_EnhancedImageResourceLoadingEvent(object sender, EnhancedImageView.EnhancedImageResourceLoadingEventArgs e)
        ///{
        ///    AIEnhancedImageView imageView = sender as AIEnhancedImageView;
        ///    Log.Error("TV.FLUX", imageView.ResourceUrl + " " + e.State);
        ///}
        /// </code>
        public class EnhancedImageResourceLoadingEventArgs : EventArgs
        {
            /// <summary>
            /// Event for Image Resource Loading.
            /// </summary>            
            /// <param name = "state"> The result of resource loading</param>
            /// <version>10.10.0</version>
            public EnhancedImageResourceLoadingEventArgs(EnhancedImageResourceLoading state)
            {
                State = state;
            }

            /// <summary>
            /// Success or Failed
            /// </summary>
            public EnhancedImageResourceLoading State
            {
                private set;
                get;
            }
        }

        /// <summary>
        /// EnhancedImageView 
        /// User can provide set different PictureEnhancerType.
        /// Currently, AIFW framework support only type OneToOneEnhancement, It is to support, if other enhancement types are supported in future.
        /// <param name = "type"></param>
        /// <param name = "delayInMilliSec"></param>
        /// </summary>
        /// <version>10.10.0</version>
        public AIEnhancedImageView(PictureEnhancerType type = PictureEnhancerType.OneToOneEnhancement, uint delayInMilliSec = 0) : base()
        {
            SecurityUtil.CheckPlatformPrivileges();
            delay = delayInMilliSec;
            CLog.Debug("New Version");
            pictureEnhancer = new AifwPictureEnhancer(type);
            pictureEnhancer.TextureUrl += OnTextureUrl;
            if (delay != 0)
            {
                timer = new Timer(delay);
                timer.Tick += TimerTick;
            }
        }

        /// <summary>
        /// EnhancedResourceLoadingStatus
        /// Emitted when resource image is loaded and texture is uploaded.
        /// </summary>
        public event EventHandler<EnhancedImageResourceLoadingEventArgs> EnhancedImageResourceLoadingEvent;
        private bool TimerTick(object source, Timer.TickEventArgs e)
        {
            pictureEnhancer.RunFileAsync(filePath);
            return false;
        }

        private void OnTextureUrl(string textureUrl)
        {
            EnhancedImageResourceLoadingEventArgs status = string.IsNullOrEmpty(textureUrl) ? new EnhancedImageResourceLoadingEventArgs(EnhancedImageResourceLoading.Fail) : new EnhancedImageResourceLoadingEventArgs(EnhancedImageResourceLoading.Success);
            if (string.IsNullOrEmpty(textureUrl) == false)
            {
                base.ResourceUrl = textureUrl;
            }

            EnhancedImageResourceLoadingEvent?.Invoke(this, status);
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

            if (type == DisposeTypes.Explicit)
            {
                if (timer != null)
                {
                    timer.Stop();
                    timer.Tick -= TimerTick;
                    timer.Dispose();
                    timer = null;
                }

                if (pictureEnhancer != null)
                {
                    pictureEnhancer.TextureUrl -= OnTextureUrl;
                    pictureEnhancer.Dispose();
                    pictureEnhancer = null;
                }
            }

            base.Dispose(type);
        }

        private string filePath = string.Empty;
        private AifwPictureEnhancer pictureEnhancer = null;
        private Timer timer = null;
        private readonly uint delay = 0;

        private string privateResourceUrl
        {
            set
            {
                filePath = value;

                if (string.IsNullOrEmpty(filePath))
                {
                    base.ResourceUrl = filePath;
                    return;
                }

                if (delay == 0)
                {
                    pictureEnhancer.RunFileAsync(filePath);
                }
                else
                {
                    base.ResourceUrl = filePath;
                    timer.Start();
                }
            }

            get => filePath;
        }

        /// <summary>
        /// ResourceUrl -Image resource Url
        /// </summary>
        public override string ResourceUrl
        {
            set => SetValue(ResourceUrlProperty, value);

            get => (string)GetValue(ResourceUrlProperty);
        }
    }
}