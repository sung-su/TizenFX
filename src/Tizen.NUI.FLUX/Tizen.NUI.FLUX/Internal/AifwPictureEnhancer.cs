/// @file AifwPictureEnhancer.cs
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
using System.Runtime.InteropServices;
using Tizen.NUI;

/// <summary>
/// namespace for Tizen.TV.NUI package
/// </summary>
namespace Tizen.NUI.FLUX
{
    internal class AifwPictureEnhancer : BaseHandle
    {

        /// <summary>
        /// PictureEnhancer Constructor
        /// </summary>
        public AifwPictureEnhancer(AIEnhancedImageView.PictureEnhancerType type) : this(Interop.AifwPictureEnhancer.New(type), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            textureUrlCallback = new Interop.AifwPictureEnhancer.TextureUrlCallback(this.OnTextureUrlCallback);
        }

        /// <summary>
        ///  Enhance the provided image resource asynchronously
        /// </summary>
        /// <param name="resourceUrl"> image resource url </param>
        /// <exception cref="ArgumentNullException">Thrwon when failed when input resource URL is null.</exception>
        public void RunFileAsync(string resourceUrl)
        {
            if (resourceUrl == null)
            {
                throw new ArgumentNullException("Resource Url is null.");
            }
            Interop.AifwPictureEnhancer.RunFileAsync(SwigCPtr, resourceUrl, textureUrlCallback);
        }

        /// <summary>
        /// TextureUrl Callback
        /// </summary>
        public Action<string> TextureUrl;

        internal AifwPictureEnhancer(IntPtr cPtr, bool cMemoryOwn) : base(Interop.AifwPictureEnhancer.UpCast(cPtr), cMemoryOwn)
        {
         
        }

        private void OnTextureUrlCallback(IntPtr textureUrl)
        {
            if(TextureUrl != null)
            {
                string imageUrl = Marshal.PtrToStringAnsi(textureUrl);
                TextureUrl.Invoke(imageUrl);
            }            
            return;
        }

        /// <summary>
        /// Release Native Handle
        /// </summary>        
        /// <param name="swigCPtr">HandleRef object holding corresponding native pointer.</param>
        /// <version>9.9.0</version>
        protected override void ReleaseSwigCPtr(HandleRef swigCPtr)
        {
            Interop.AifwPictureEnhancer.Delete(swigCPtr);
        }


        /// <summary>
        /// Cleaning up managed and unmanaged resources 
        /// </summary>
        /// <param name="type">Disposing type</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            base.Dispose(type);
        }

        private Interop.AifwPictureEnhancer.TextureUrlCallback textureUrlCallback;
    }
}
