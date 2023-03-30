/// @file WindowAttribute.cs
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
    /// WindowAttribute which is parameter type passed through the method of IVideoWindowControlExtension
    /// </summary>
    /// <code>
    /// WindowAttribute windowAttribute = new WindowAttribute((0, 0), (960, 540), 90, (1920, 1080));
    /// (int X, int Y) position = windowAttribute.Position;
    /// (int Width, int Height) size = windowAttribute.Size;
    /// int degree = windowAttribute.Degree;
    /// (int Width, int Height) baseScreenResolution = windowAttribute.BaseScreenResolution;
    /// </code>
    public class WindowAttribute
    {
        private (int X, int Y) windowPosition;
        private (int Width, int Height) windowSize;
        private (int Width, int Height) baseResolution;
        private int windowDegree;

        /// <summary>
        /// Constructor of WindowAttribute
        /// </summary>
        public WindowAttribute()
        {
        }

        /// <summary>
        /// Constructor of WindowAttribute
        /// </summary>
        /// <param name="position">Window position</param>
        /// <param name="size">Window size</param>
        /// <param name="degree">Window degree</param>
        /// <param name="baseScreenResolution">Base screen resolution</param>
        public WindowAttribute((int X, int Y) position, (int Width, int Height) size, int degree, (int Width, int Height) baseScreenResolution)
        {
            windowPosition = position;
            windowSize = size;
            baseResolution = baseScreenResolution;
            windowDegree = degree;
        }

        /// <summary>
        /// Position of Window
        /// </summary>
        public (int X, int Y) Position => windowPosition;

        /// <summary>
        /// Size of Window
        /// </summary>
        public (int Width, int Height) Size => windowSize;

        /// <summary>
        /// Degree of Window
        /// </summary>
        public int Degree => windowDegree;

        /// <summary>
        /// BaseScreenResolution. It is returned according to the value in the manifest file of current application.
        /// </summary>
        public (int Width, int Height) BaseScreenResolution => baseResolution;

        /// <summary>
        /// Set position, size, degree, base screen resolution of window
        /// </summary>
        /// <param name="position">Position of window (x, y)</param>
        /// <param name="size">Size of window (width, height)</param>
        /// <param name="degree">Degree of window</param>
        /// <param name="baseScreenResolution">Base screen resolution (width, height)</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetWindowAttribute((int X, int Y) position, (int Width, int Height) size, int degree, (int Width, int Height) baseScreenResolution)
        {
            windowPosition = position;
            windowSize = size;
            windowDegree = degree;
            baseResolution = baseScreenResolution;
        }
    }
}