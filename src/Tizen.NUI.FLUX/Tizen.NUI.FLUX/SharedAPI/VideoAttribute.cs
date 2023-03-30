/// @file VideoAttribute.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version>10.10.0</version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2022 Samsung Electronics Co., Ltd All Rights Reserved
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

using System.ComponentModel;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// VideoAttribute which is parameter type passed through the method of IVideoWindowControlExtension
    /// </summary>
    /// <code>
    /// VideoAttribute videoAttribute = new VideoAttribute(0, 0, 960, 540);
    /// int x = videoAttribute.X;
    /// int y = videoAttribute.Y;
    /// int width = videoAttribute.Width;
    /// int height = videoAttribute.Height;
    /// </code>
    public class VideoAttribute
    {
        private int videoX;
        private int videoY;
        private int videoWidth;
        private int videoHeight;

        /// <summary>
        /// Constructor of VideoAttribute
        /// </summary>        
        public VideoAttribute()
        {
        }

        /// <summary>
        /// Constructor of VideoAttribute
        /// </summary>
        /// <param name="x">X Position of Video area</param>
        /// <param name="y">Y Position of Video area</param>
        /// <param name="width">Width of Video area</param>
        /// <param name="height">Height of Video area</param>
        public VideoAttribute(int x, int y, int width, int height)
        {
            videoX = x;
            videoY = y;
            videoWidth = width;
            videoHeight = height;
        }

        /// <summary>
        /// X Position of Video area
        /// </summary>
        public int X => videoX;

        /// <summary>
        /// Y Position of Video area
        /// </summary>
        public int Y => videoY;

        /// <summary>
        /// Width of Video area
        /// </summary>
        public int Width => videoWidth;

        /// <summary>
        /// Height of Video area
        /// </summary>
        public int Height => videoHeight;

        /// <summary>
        /// Set position and size attribute of video
        /// </summary>
        /// <param name="x">Position of x</param>
        /// <param name="y">Position of y</param>
        /// <param name="width">Size of width</param>
        /// <param name="height">Size of height</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetVideoAttribute(int x, int y, int width, int height)
        {
            videoX = x;
            videoY = y;
            videoWidth = width;
            videoHeight = height;
        }
    }
}