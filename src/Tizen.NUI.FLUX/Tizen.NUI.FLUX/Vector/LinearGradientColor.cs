/// @file LinearGradientColor.cs
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

namespace Tizen.NUI.FLUX
{
    using Point = Vector2;
    /// <summary>
    /// Linear Gradient is a class for Gradient specs.
    /// </summary>
    /// <code>
    /// LinearGradientColor gradientColor = new LinearGradientColor(new Vector2(100, 100), new Vector2(100, 0));
    /// gradientColor.AddGradientStop(new ColorStops(Color.Red, 0));
    /// gradientColor.AddGradientStop(new ColorStops(Color.Blue, 0.5f));
    /// gradientColor.AddGradientStop(new ColorStops(Color.Green, 1));
    /// </code>
    public class LinearGradientColor : GradientColor
    {
        /// <summary>
        /// Constructor for the Linear Gradient Effect
        /// </summary>
        /// <param name="start">Point at which gradient effect Starts</param>
        /// <param name="end"> Point at which gradient effect Ends</param>
        /// <exception cref="ArgumentNullException">Thrown when start or end instance is null.</exception>
        public LinearGradientColor(Point start, Point end)
        {
            if (start == null || end == null)
            {
                throw new ArgumentNullException("start or end cannot be null");
            }
            StartPoint = start;
            EndPoint = end;
        }

        /// <summary>
        /// Point at which gradient effect Starts
        /// </summary>
        public Point StartPoint { set; get; }

        /// <summary>
        /// Point at which gradient effect Ends
        /// </summary>
        public Point EndPoint { set; get; }

        internal override void Draw(VectorView view)
        {
            SetGradient(view, (int)Style.Linear, StartPoint, 0, EndPoint, 0, GradientColorStops, 1);
        }
    }
}
