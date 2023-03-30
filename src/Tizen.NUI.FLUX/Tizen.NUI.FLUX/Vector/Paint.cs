/// @file Paint.cs
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
    /// <summary>
    /// Specifies the dash attribute
    /// </summary>
    public struct Dash
    {
        /// <summary>
        /// Constructor for Dash
        /// </summary>
        /// <param name="length">Length of each segment</param>
        /// <param name="interval">interval between 2 dash segments</param>
        /// <exception cref="ArgumentException">Thrown when length or interval of dash is negative.</exception>
        public Dash(float length, float interval)
        {
            if (length < 0 || interval < 0)
            {
                throw new ArgumentException("Invalid Arguments. Length or Interval of dash cannot be negative");
            }
            Length = length;
            Interval = interval;
        }

        /// <summary>
        /// Length of each segment
        /// </summary>
        internal float Length { get; }

        /// <summary>
        /// interval between 2 dash segments
        /// </summary>
        internal float Interval { get; }
    }

    /// <summary>
    /// Represents a Gradient Point
    /// </summary>
    public struct ColorStops
    {
        /// <summary>
        /// Constructor for Gradient Color
        /// </summary>
        /// <param name="color">Color at the gradient stop</param>
        /// <param name="position">Position of the gradient stop</param>
        /// <exception cref="ArgumentNullException">Thrown when color instance is null.</exception>
        public ColorStops(Color color, float position)
        {
            Color = color ?? throw new ArgumentNullException("color cannot be null");
            Position = position;
        }

        /// <summary>
        /// Color at the gradient stop
        /// </summary>
        internal Color Color { get; }

        /// <summary>
        /// Position of the gradient stop
        /// </summary>
        internal float Position { get; }
    }

    /// <summary>
    /// PaintEffect is a class for Gradient specs.
    /// </summary>
    /// <code>
    /// SolidColor red = new SolidColor(Color.Red);
    /// Fill fillStyle = new Fill(red);
    /// </code>
    public abstract class Paint
    {
        internal abstract void Draw(VectorView view);
        internal abstract RenderBackend GetRenderBackend();
    }
}
