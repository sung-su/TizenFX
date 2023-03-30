/// @file FluxPhysicsAnimation.cs
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
using Tizen.NUI.BaseComponents;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// This is PhysicsAnimation which enables user to animate by physics engine.
    /// </summary>
    /// <code>
    /// FluxView view = new FluxView();
    /// view.UnitSize = new UnitSize(50,50);
    /// view.UnitPosition = new UnitPosition(50,0);
    /// view.BackgroundColor = Color.Red;
    /// Window.Instance.GetDefaultLayer().Add(view);
    /// FluxPhysicsAnimation animation = new FluxPhysicsAnimation();
    /// animation.AnimateTo(view, "UnitPositionX", 100, PhysicsAnimation.BuiltinFunctions.BezierBasic);
    /// animation.AnimateTo(view, "UnitPositionY", 100, PhysicsAnimation.BuiltinFunctions.BezierBasic);
    /// animation.Play();
    /// </code>
    public class FluxPhysicsAnimation : PhysicsAnimation
    {

        /// <summary>
        /// Animates a property to a destination value.
        /// </summary>
        /// <param name="target">The target object to animate</param>
        /// <param name="property">The target property to animate</param>
        /// <param name="destinationValue">The destination value based on UnintPosition Coordinate System</param>
        /// <param name="startTime">Start time of animation</param>
        /// <param name="endTime">End time of animation</param>
        /// <param name="alpha">The alpha function to apply</param>
        /// <exception cref="InvalidCastException">Thrown when destinationValue cannot be casted into UnitPoistion Type</exception>
        /// <exception cref="NotSupportedException">Thrown when insertd property name does not match for target.</exception>

        public void AnimateTo(FluxView target, string property, object destinationValue, int startTime, int endTime, BuiltinFunctions? alpha = null)
        {            
            if(property == "UnitPositionX")
            {
                destinationValue = DisplayMetrics.Instance.UnitToPixel((int)destinationValue);
                property = "PositionX";
            }
            else if(property == "UnitPositionY")
            {
                destinationValue = DisplayMetrics.Instance.UnitToPixel((int)destinationValue);
                property = "PositionY";
            }
            else if(property == "UnitPosition")
            {
                UnitPosition unitPosition= destinationValue as UnitPosition;
                if(unitPosition == null)
                {
                    throw new InvalidCastException("Please insert UnitPosition Type destinationValue");
                }
                destinationValue = new Position2D(DisplayMetrics.Instance.UnitToPixel(unitPosition.X), DisplayMetrics.Instance.UnitToPixel(unitPosition.Y));
                property = "Position2D";
            }
            else
            {
                throw new NotSupportedException("Please insert correct property name for your target");
            }

            base.AnimateTo(target, property, destinationValue, startTime, endTime, alpha);
        }

        /// <summary>
        /// Animates a property to a destination value.
        /// </summary>
        /// <param name="target">The target object to animate</param>
        /// <param name="property">The target property to animate</param>
        /// <param name="destinationValue">The destination value based on UnintPosition Coordinate System</param>
        /// <param name="alpha">The alpha function to apply</param>        
        public void AnimateTo(FluxView target, string property, object destinationValue, BuiltinFunctions? alpha = null)
        {
            AnimateTo(target, property, destinationValue, 0, Duration, alpha);
        }


        ///*------ Do not use ------*///

        /// <summary>
        /// Animates a property to a destination value.
        /// </summary>
        /// <param name="target">The target object to animate</param>
        /// <param name="property">The target property to animate</param>
        /// <param name="destinationValue">The destination value based on UnintPosition Coordinate System</param>
        /// <param name="startTime">The target animation start time</param>
        /// <param name="endTime">The target animation end time</param>
        /// <param name="alpha">The alpha function to apply</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void AnimateTo(View target, string property, object destinationValue, int startTime, int endTime, BuiltinFunctions? alpha = null)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Animates a property to a destination value.
        /// </summary>
        /// <param name="target">The target object to animate</param>
        /// <param name="property">The target property to animate</param>
        /// <param name="destinationValue">The destination value based on UnintPosition Coordinate System</param>
        /// <param name="alpha">The alpha function to apply</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void AnimateTo(View target, string property, object destinationValue, BuiltinFunctions? alpha = null)
        {
            throw new NotSupportedException();
        }
    }
}
