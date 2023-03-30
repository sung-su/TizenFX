/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file IStatable.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.6.0 </version>
/// <SDK_Support> Y </SDK_Support>
///
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


namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    ///  This class is interface class to support State
    /// </summary>
    /// <version> 6.6.0 </version>
    /// <code>
    /// public class CustomComponent : IStatable
    /// {
    ///  
    /// }
    /// </code>

    public interface IStatable
    {
        /// <summary>
        /// To support Disable State, inherited class should provide same property as public
        /// If you want to disabled state , you set this value to true.
        /// </summary>
        bool Disabled { get; set; }

        /// <summary>
        /// To support Selected State, inherited class should provide same property as public
        /// If you want to checked state , you set this value to true.
        /// </summary>
        bool Selected { get; set; }

        /// <summary>
        /// To support Checked State, inherited class should provide same property as public
        /// If you want to checked state , you set this value to true.
        /// </summary>
        bool Checked { get; set; }

        /// <summary>
        /// StateMachine to support state, inherited class should create new StateMachine. 
        /// </summary>
        StateMachine State { get; set; }

        /// <summary>
        /// Update  state property value at StateMachine , inherited class should provide same logic using StateMachine
        /// </summary>
        /// <param name="stateName"> state name which need to update</param>
        /// <param name="propertyName"> property name which need to update</param>
        /// <param name="value"> new value of specified property </param>
        void UpdateStateProperty(string stateName, string propertyName, object value);
    }


    /// <summary>
    /// If you make userState in your application, you can fill it.
    /// </summary>
    public struct UserState
    {
        /// <summary>
        /// This is pre-defined state from UX.
        /// </summary>
        public string PreDefinedStateName;
        /// <summary>
        /// Enter PropertyName(ex. Opacity etc)
        /// </summary>
        public string PropertyName;
        /// <summary>
        /// Enter PropertyVaule according to PropertyName
        /// </summary>
        public object PropertyValue;
        /// <summary>
        /// Constructor about UserState.
        /// </summary>
        /// <param name="preDefinedStateName">This is pre-defined state from UX.</param>
        /// <param name="propertyName">Enter PropertyName(ex. Opacity etc)</param>
        /// <param name="value">Enter PropertyVaule according to PropertyName</param>
        public UserState(string preDefinedStateName, string propertyName, object value)
        {
            PreDefinedStateName = preDefinedStateName;
            PropertyName = propertyName;
            PropertyValue = value;
        }
    }
}
