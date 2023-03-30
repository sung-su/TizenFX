/// @file VideoImageView.cs
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

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// This is VideoImageView which allows user to do video texturing. 
    /// </summary>
    /// <code>
    /// videoImageView = new VideoImageView();
    /// videoImageView.Size2D = new Size2D(400, 300);
    /// videoImageView.Position = new Position(100, 100, 0);  
    /// mPlayer = new Player();
    /// mPlayer.SetSource(new MediaUriSource("The_Power_of_Teamwork.mp4"));
    /// mPlayer.PrepareAsync().Wait();
    /// videoView.SetPlayerHandle(mPlayer.GetType(), mPlayer.Handle);
    /// </code>
    /// <code>
    /// videoImageViewTVPlayer = new VideoImageView()
    /// videoImageViewTVPlayer.SetBuiltinPlayer(PlayerType.TVPLAYER);
    /// videoImageViewTVPlayer.HorizontalFlip = true;
    /// videoImageViewTVPlayer.VerticalFlip = true;
    /// </code> 
    public partial class VideoImageView : FluxView
    {
        private bool isHorizontalFlip = false;
        private bool isVerticalFlip = false;
        internal VideoImageView(IntPtr cPtr, bool cMemoryOwn): base(Interop.VideoImageView.Upcast(cPtr), cMemoryOwn)
        {
            
        }

        /// <summary>
        /// Creates an initialized VideoView.
        /// </summary>
        public VideoImageView(): this(Interop.VideoImageView.New(), true)
        {
            SecurityUtil.CheckPlatformPrivileges();
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        /// <summary>
        /// Release Native Handle
        /// </summary>
        /// <param name="swigCPtr">HandleRef object holding corresponding native pointer.</param>
        /// <version>9.9.0</version>
        protected override void ReleaseSwigCPtr(HandleRef swigCPtr)
        {
            Interop.VideoImageView.Delete(swigCPtr);
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
        /// Set Player native handle.
        /// </summary>
        /// <param name = "type">Player type</param>
        /// <param name = "player">Player handle</param>
        /// <exception cref = 'ArgumentNullException'> Player handle is null. You should set valid Player handle </exception>
        /// <exception cref = 'ArgumentException'> Agruments is not valid. </exception>
        public void SetPlayerHandle(Type type, IntPtr player)
        {
            if (player == IntPtr.Zero)
            {
                throw new ArgumentNullException("Player handle is null. You should set valid Player handle");
            }

            bool result = Interop.VideoImageView.SetPlayerHandle(SwigCPtr, type.FullName, player);
            if (!result)
            {
                throw new ArgumentException("SetPlayerHandle API failed, Check log message for more Info");
            }
        }

        /// <summary>
        /// Unset the player handle if already set
        /// </summary>
        public void UnSetPlayerHandle()
        {
            Interop.VideoImageView.UnSetPlayerHandle(SwigCPtr);
        }

        private bool privateHorizontalFlip
        {
            set
            {
                if (value != isHorizontalFlip)
                {
                    Interop.VideoImageView.Filp(SwigCPtr, (uint)FlipType.Horizontal);
                    isHorizontalFlip = value;
                }
            }

            get
            {
                return isHorizontalFlip;
            }
        }

        /// <summary>
        /// Property to Enable/Disable Horizontal Flip
        /// </summary>
        /// <version>10.10.0</version>
        public bool HorizontalFlip
        {
            set
            {
                SetValue(HorizontalFlipProperty, value);
            }

            get
            {
                return (bool)GetValue(HorizontalFlipProperty);
            }
        }

        private bool privateVerticalFlip
        {
            set
            {
                if (value != isVerticalFlip)
                {
                    Interop.VideoImageView.Filp(SwigCPtr, (uint)FlipType.Vertical);
                    isVerticalFlip = value;
                }
            }

            get
            {
                return isVerticalFlip;
            }
        }

        /// <summary>
        /// Property to Enable/Disable Vertical Flip
        /// </summary>
        /// <version>10.10.0</version>
        public bool VerticalFlip
        {
            set
            {
                SetValue(VerticalFlipProperty, value);
            }

            get
            {
                return (bool)GetValue(VerticalFlipProperty);
            }
        }
    }
}