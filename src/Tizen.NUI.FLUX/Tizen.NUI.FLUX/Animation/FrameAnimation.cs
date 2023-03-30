/// @file FrameAnimation.cs
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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// This is FrameAnimation which enables user to animate multiple images.
    /// </summary>
    /// <code>
    /// frameAnimation = new FrameAnimation();
    /// frameAnimation.FPS = 60;
    /// frameAnimation.Looping = true;
    /// frameAnimation.ImageArray = new string[]
    /// {
    ///     @"/home/owner/apps_rw/Tizen.TV.FLUX.Example/res/images/Movies/1.jpg",
    ///     @"/home/owner/apps_rw/Tizen.TV.FLUX.Example/res/images/Movies/2.jpg",
    ///     @"/home/owner/apps_rw/Tizen.TV.FLUX.Example/res/images/Movies/3.jpg",
    ///     @"/home/owner/apps_rw/Tizen.TV.FLUX.Example/res/images/Movies/4.jpg",
    ///     @"/home/owner/apps_rw/Tizen.TV.FLUX.Example/res/images/Movies/5.jpg",
    /// };
    /// frameAnimation.Attach(imageView);
    /// frameAnimation.Play();
    /// frameAnimation.Finished += FrameAnimation_Finished;
    /// Log.Error("FluxSample", "Current AnimationState : ", frameAnimation.AnimationState);
    /// 
    /// private void FrameAnimation_Finished(object sender, EventArgs e)
    /// {
    ///     // Do Something 
    /// }
    /// </code>
    public class FrameAnimation
    {
        private int fps = 60;
        private int playingIndex = 0;
        private ImageView parentImageView = null;
        private readonly AnimationEX tick = null;

        /// <summary>
        /// Constructor to instantiate the FrameAnimation class.
        /// </summary>
        public FrameAnimation()
        {
            SecurityUtil.CheckPlatformPrivileges();
            tick = new AnimationEX(1000);
            tick.Update += UpdateEventHandler;

            FPS = 60;
            ImageArray = null;
        }

        /// <summary>
        /// Enable / disable infinite loop of FrameAnimation.
        /// If Looping is true, LoopCount will be ignored.
        /// </summary>
        public bool Looping
        {
            get => tick.IsLooping();
            set => tick.SetLooping(value);
        }

        /// <summary>
        /// Set loop count of FrameAnimation.
        /// </summary>
        public int LoopCount
        {
            get => tick.LoopCount;
            set
            {
                if (value <= 0)
                {
                    CLog.Error("LoopCount value should be 1 or more");
                    tick.LoopCount = 1;
                }
                else
                {
                    tick.LoopCount = value;
                }
            }
        }

        /// <summary>
        /// Set or get FPS of FrameAnimation
        /// When FPS value is 0 or less, fps will be set as 60 by default.
        /// </summary>
        public int FPS
        {
            get => fps;
            set
            {
                if (value <= 0 || value > 60)
                {
                    CLog.Error("FPS value should be between 1 ~ 60");
                    fps = 60;
                }
                else
                {
                    fps = value;
                }
            }
        }

        /// <summary>
        /// Images that will be animated in turn.
        /// </summary>
        public string[] ImageArray
        {
            get;
            set;
        }

        /// <summary>
        /// Get animation state of FrameAnimation.
        /// </summary>
        public Animation.States AnimationState => tick.State;

        /// <summary>
        /// Attach ImageView on FrameAnimation.
        /// </summary>
        /// <param name="imageView">ImageView where images will be animated.</param>
        public void Attach(ImageView imageView)
        {
            parentImageView = imageView;

            if (parentImageView == null)
            {
                return;
            }
            if (ImageArray == null || ImageArray.Length <= 0)
            {
                return;
            }
            if (playingIndex < ImageArray.Length)
            {
                parentImageView.ResourceUrl = ImageArray[playingIndex];
            }
        }

        /// <summary>
        /// Detach ImageView from FrameAnimation.
        /// </summary>
        public void Detach()
        {
            if (parentImageView == null)
            {
                return;
            }
            Stop();
            parentImageView.ResourceUrl = "";
            parentImageView = null;
        }

        /// <summary>
        /// Play FrameAnimation
        /// </summary>
        public void Play()
        {
            if (parentImageView == null)
            {
                CLog.Error("ImageView has not been attached. Play would stop.");
                return;
            }
            if (ImageArray == null || ImageArray.Length <= 0)
            {
                CLog.Error("ImageArray is null or its length is 0 or less. Play would stop.");
                return;
            }
            if (tick.State == Animation.States.Playing)
            {
                return;
            }
            if (tick.State == Animation.States.Paused)
            {
                tick.Play();
                return;
            }

            playingIndex = 0;
            parentImageView.ResourceUrl = ImageArray[playingIndex];

            tick.Duration = 1000 / FPS * ImageArray.Length;
            tick.Play();
        }

        /// <summary>
        /// Stop FrameAnimation
        /// </summary>
        /// <param name="action">Action after stopping. </param>
        public void Stop(Animation.EndActions action = Animation.EndActions.Cancel)
        {
            if (tick.State == Animation.States.Stopped)
            {
                return;
            }

            tick.Stop();

            if (action == Animation.EndActions.Discard)
            {
                parentImageView.ResourceUrl = ImageArray[0];
            }
            if (action == Animation.EndActions.StopFinal)
            {
                parentImageView.ResourceUrl = ImageArray[ImageArray.Length - 1];
            }
        }

        /// <summary>
        /// Pause FrameAnimation
        /// </summary>
        public void Pause()
        {
            tick.Pause();
        }

        /// <summary>
        /// Event for Finished signal which can be used to subscribe/unsubscribe the event handler.
        /// Finished signal is emitted when FrameAnimation's animation have finished.
        /// </summary>
        public event EventHandler Finished
        {
            add
            {
                tick.Finished += value;
            }
            remove
            {
                tick.Finished -= value;
            }
        }

        private void UpdateEventHandler(float progress, float alpha)
        {
            if (ImageArray == null)
            {
                CLog.Debug("ImageArray is null. Can't update image.");
                return;
            }
            if (parentImageView == null)
            {
                CLog.Debug("ImageView is null. You add attach imageview.");
                return;
            }
            int nextIndex = (int)(progress * ImageArray.Length);

            if (nextIndex >= ImageArray.Length)
            {
                nextIndex = ImageArray.Length - 1;
            }
            if (nextIndex != playingIndex)
            {
                playingIndex = nextIndex;
                parentImageView.ResourceUrl = ImageArray[playingIndex];
            }
        }
    }
}
