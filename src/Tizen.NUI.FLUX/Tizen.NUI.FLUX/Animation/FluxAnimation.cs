/// @file FluxAnimation.cs
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
using System.Collections.Generic;
using System.ComponentModel;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// FluxAnimation is base animation class for support FLUX Application.
    /// </summary>
    /// <code>
    /// fluxAnimation = new FluxAnimation(1000);    
    /// fluxAnimation.AnimateTo(fluxView, "UnitPositionX", 300);
    /// fluxAnimation.AnimateTo(fluxView, "UnitPositionY", 150);
    /// fluxAnimation.Play();
    /// </code>
    public class FluxAnimation : Animation
    {
        /// <summary>
        /// FluxAnimation is a constructor for flux system animation.
        /// </summary>
        public FluxAnimation()
        {
            SecurityUtil.CheckPlatformPrivileges();
        }
        /// <summary>
        /// FluxAnimation constructor class which can set duration.
        /// </summary>
        /// <param name="durationMilliSeconds">The duration in milliseconds.</param>
        public FluxAnimation(int durationMilliSeconds) : base(durationMilliSeconds)
        {            
            SecurityUtil.CheckPlatformPrivileges();
        }

        /// <summary>
        /// Animates a property to a destination value.
        /// </summary>
        /// <param name="target">The target object to animate.</param>
        /// <param name="property">The target property to animate.</param>
        /// <param name="destinationValue">The destination value.</param>
        /// <param name="alphaFunction">The alpha function to apply.</param>
        /// <exception cref="ArgumentNullException">Thrown when target view is null</exception>
        public void AnimateTo(FluxView target, string property, object destinationValue, AlphaFunction alphaFunction = null)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target view is null");
            }

            ConvertUnitPropertyToPixelProperty(property, destinationValue, out string convertedProperty, out object convertedValue);
            base.AnimateTo(target, convertedProperty, convertedValue, alphaFunction);
        }

        /// <summary>
        /// Animates a property to a destination value.
        /// </summary>
        /// <param name="target">The target object to animate.</param>
        /// <param name="property">The target property to animate.</param>
        /// <param name="destinationValue">The destination value.</param>
        /// <param name="startTime">The start time of the animation.</param>
        /// <param name="endTime">The end time of the animation.</param>
        /// <param name="alphaFunction">The alpha function to apply.</param>
        /// <exception cref="ArgumentNullException">Thrown when target view is null</exception>
        public void AnimateTo(FluxView target, string property, object destinationValue, int startTime, int endTime, AlphaFunction alphaFunction = null)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target view is null");
            }

            ConvertUnitPropertyToPixelProperty(property, destinationValue, out string convertedProperty, out object convertedValue);
            base.AnimateTo(target, convertedProperty, convertedValue, startTime, endTime, alphaFunction);
        }

        /// <summary>
        ///  Animates a property value by a relative amount.
        /// </summary>
        /// <param name="target">The target object to animate.</param>
        /// <param name="property">The target property to animate.</param>
        /// <param name="relativeValue">The property value will change by this amount.</param>
        /// <param name="alphaFunction">The alpha function to apply.</param>
        /// <exception cref="ArgumentNullException">Thrown when target view is null</exception>
        public void AnimateBy(FluxView target, string property, object relativeValue, AlphaFunction alphaFunction = null)
        {            
            if (target == null)
            {
                throw new ArgumentNullException("target view is null");
            }

            ConvertUnitPropertyToPixelProperty(property, relativeValue, out string convertedProperty, out object convertedValue);
            base.AnimateBy(target, convertedProperty, convertedValue, alphaFunction);
        }
        /// <summary>
        /// Animates a property value by a relative amount.
        /// </summary>
        /// <param name="target">The target object to animate.</param>
        /// <param name="property">The target property to animate.</param>
        /// <param name="relativeValue">The property value will change by this amount.</param>
        /// <param name="startTime">The start time of the animation.</param>
        /// <param name="endTime">The end time of the animation.</param>
        /// <param name="alphaFunction">The alpha function to apply.</param>
        /// <exception cref="ArgumentNullException">Thrown when target view is null</exception>
        public void AnimateBy(FluxView target, string property, object relativeValue, int startTime, int endTime, AlphaFunction alphaFunction = null)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target view is null");
            }

            ConvertUnitPropertyToPixelProperty(property, relativeValue, out string convertedProperty, out object convertedValue);
            base.AnimateBy(target, convertedProperty, convertedValue, startTime, endTime,  alphaFunction);
        }

        /// <summary>
        /// Animates a property to a destination value.
        /// </summary>
        /// <param name="target">The target object to animate.</param>
        /// <param name="property">The target property to animate.</param>
        /// <param name="destinationValue">The destination value.</param>
        /// <param name="alphaFunction">The alpha function to apply.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void AnimateTo(View target, string property, object destinationValue, AlphaFunction alphaFunction = null) { }

        /// <summary>
        /// Animates a property to a destination value.
        /// </summary>
        /// <param name="target">The target object to animate.</param>
        /// <param name="property">The target property to animate.</param>
        /// <param name="destinationValue">The destination value.</param>
        /// <param name="startTime">The start time of the animation.</param>
        /// <param name="endTime">The end time of the animation.</param>
        /// <param name="alphaFunction">The alpha function to apply.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void AnimateTo(View target, string property, object destinationValue, int startTime, int endTime, AlphaFunction alphaFunction = null) { }

        /// <summary>
        /// Animates a property value by a relative amount.
        /// </summary>
        /// <param name="target">The target object to animate.</param>
        /// <param name="property">The target property to animate.</param>
        /// <param name="relativeValue">The property value will change by this amount.</param>
        /// <param name="alphaFunction">The alpha function to apply.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void AnimateBy(View target, string property, object relativeValue, AlphaFunction alphaFunction = null) { }

        /// <summary>
        /// Animates a property value by a relative amount.
        /// </summary>
        /// <param name="target">The target object to animate.</param>
        /// <param name="property">The target property to animate.</param>
        /// <param name="relativeValue">The property value will change by this amount.</param>
        /// <param name="startTime">The start time of the animation.</param>
        /// <param name="endTime">The end time of the animation.</param>
        /// <param name="alphaFunction">The alpha function to apply.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void AnimateBy(View target, string property, object relativeValue, int startTime, int endTime, AlphaFunction alphaFunction = null) { }

        /// <summary>
        /// Animates a property between keyframes.
        /// </summary>
        /// <param name="target">The target object to animate.</param>
        /// <param name="property">The target property to animate.</param>
        /// <param name="keyFrames">The set of time or value pairs between which to animate.</param>
        /// <param name="interpolation">The method used to interpolate between values.</param>
        /// <param name="alphaFunction">The alpha function to apply.</param>
        //AnimateBetween : keyframe animation
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void AnimateBetween(View target, string property, KeyFrames keyFrames, Interpolation interpolation = Interpolation.Linear, AlphaFunction alphaFunction = null){ }

        /// <summary>
        /// Animates a property between keyframes.
        /// </summary>
        /// <param name="target">The target object to animate.</param>
        /// <param name="property">The target property to animate.</param>
        /// <param name="keyFrames">The set of time/value pairs between which to animate</param>
        /// <param name="startTime">The start time of animation in milliseconds.</param>
        /// <param name="endTime">The end time of animation in milliseconds.</param>
        /// <param name="interpolation">The method used to interpolate between values.</param>
        /// <param name="alphaFunction">The alpha function to apply.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void AnimateBetween(View target, string property, KeyFrames keyFrames, int startTime, int endTime, Interpolation interpolation = Interpolation.Linear, AlphaFunction alphaFunction = null){ }

        /// <summary>
        /// Animates the view's position and orientation through a predefined path.
        /// </summary>
        /// <param name="view">The view to animate.</param>
        /// <param name="path">It defines position and orientation.</param>
        /// <param name="forward">The vector (in local space coordinate system) will be oriented with the path's tangent direction.</param>
        /// <param name="alphaFunction">The alpha function to apply.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void AnimatePath(View view, Path path, Vector3 forward, AlphaFunction alphaFunction = null){ }

        /// <summary>
        /// Animates the view's position and orientation through a predefined path.
        /// </summary>
        /// <param name="view">The view to animate.</param>
        /// <param name="path">It defines position and orientation.</param>
        /// <param name="forward">The vector (in local space coordinate system) will be oriented with the path's tangent direction.</param>
        /// <param name="startTime">The start time of the animation.</param>
        /// <param name="endTime">The end time of the animation.</param>
        /// <param name="alphaFunction">The alpha function to apply.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void AnimatePath(View view, Path path, Vector3 forward, int startTime, int endTime, AlphaFunction alphaFunction = null){ }

        private static void ConvertUnitPropertyToPixelProperty(string property, object destinationValue, out string convertedProperty, out object convertedValue)
        {
            switch (property)
            {
                case "UnitPositionX":
                    convertedProperty = nameof(View.PositionX);
                    convertedValue = DisplayMetrics.Instance.UnitToPixel(Convert.ToInt32(destinationValue));
                    break;
                case "UnitPositionY":
                    convertedProperty = nameof(View.PositionY);
                    convertedValue = DisplayMetrics.Instance.UnitToPixel(Convert.ToInt32(destinationValue));
                    break;
                case "UnitSizeWidth":
                    convertedProperty = nameof(View.SizeWidth);
                    convertedValue = DisplayMetrics.Instance.UnitToPixel(Convert.ToInt32(destinationValue));
                    break;
                case "UnitSizeHeight":
                    convertedProperty = nameof(View.SizeHeight);
                    convertedValue = DisplayMetrics.Instance.UnitToPixel(Convert.ToInt32(destinationValue));
                    break;
                case "UnitPosition":
                    if (!(destinationValue is UnitPosition unitPosition))
                    {
                        throw new ArgumentException($"destinationValue type is different with Property {property}:{destinationValue}");
                    }
                    convertedProperty = nameof(Position);
                    convertedValue = new Position(DisplayMetrics.Instance.UnitToPixel(unitPosition.X), DisplayMetrics.Instance.UnitToPixel(unitPosition.Y), 0);
                    break;
                case "UnitSize":
                    if (!(destinationValue is UnitSize unitSize))
                    {
                        throw new ArgumentException($"destinationValue type is different with Property {property}:{destinationValue}");
                    }
                    convertedProperty = nameof(Size);
                    convertedValue = new Size(DisplayMetrics.Instance.UnitToPixel(unitSize.Width), DisplayMetrics.Instance.UnitToPixel(unitSize.Height), 0);
                    break;
                default:
                    convertedProperty = property;
                    convertedValue = destinationValue;
                    break;
            }
        }
    }
}
