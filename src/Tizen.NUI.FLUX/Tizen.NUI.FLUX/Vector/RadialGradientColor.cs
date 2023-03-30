/// @file RadialGradientColor.cs
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
    /// Radial Gradient is a class for Gradient specs.
    /// </summary>
    /// <code>
    /// RadialGradientColor gradientColor = new RadialGradientColor(new Position2D(100, 100), 100, new Position2D(100, 100), 0.0f);
    /// gradientColor.AddGradientStop(new ColorStops(Color.Red, 0.0f));
    /// gradientColor.AddGradientStop(new ColorStops(Color.Blue, 1.0f));
    /// </code>
    public class RadialGradientColor : GradientColor
    {
        private Point mStartCenter;
        private Point mEndCenter;
        /// <summary>
        /// Creates a new radial gradient between the two circles.
        /// </summary>
        /// <param name="startCenter">Center of the start circle</param>
        /// <param name="endCenter">Center of the end circle</param>
        /// <param name="startRadius">Radius of the start circle</param>
        /// <param name="endRadius">Radius of the end circle</param>
        /// <exception cref="ArgumentNullException">Thrown when startCenter instance is null.</exception>
        public RadialGradientColor(Point startCenter, float startRadius, Point endCenter = null, float endRadius = 0)
        {
            if (startCenter == null)
            {
                throw new ArgumentNullException("start cannot be null");
            }

            if (endCenter == null)
            {
                endCenter = startCenter;
            }

            StartCenter = startCenter;
            EndCenter = endCenter;
            StartRadius = startRadius;
            EndRadius = endRadius;
        }

        /// <summary>
        /// Center of the Start circle
        /// </summary>
        public Point StartCenter
        {
            set => mStartCenter = new Point(value.X, value.Y);
            internal get => mStartCenter;
        }

        /// <summary>
        /// Center of the End circle
        /// </summary>
        public Point EndCenter
        {
            set => mEndCenter = new Point(value.X, value.Y);
            internal get => mEndCenter;
        }

        /// <summary>
        /// Radius of the Start circle
        /// </summary>
        public float StartRadius { set; get; }

        /// <summary>
        ///  Radius of the End circle
        /// </summary>
        public float EndRadius { set; get; }

        internal override void Draw(VectorView view)
        {
            SetGradient(view, (int)Style.Radial, StartCenter, StartRadius, EndCenter, EndRadius, GradientColorStops, 1);
        }
    }
}
