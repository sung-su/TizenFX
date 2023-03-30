/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file ImageBox.cs
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

using System;
using System.Collections.Generic;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    ///  State Property class to use state machine
    /// </summary>
    /// 
    /// <example>
    /// <code>
    ///  StateMachine stateMachine = new StateMachine();
    ///  StateProperty normal = new StateProperty(StateUtility.Normal);
    ///  normal["BackgroundColor"] = Color.Red;
    ///  stateMachine[StateUtility.Normal] = normal;
    /// </code>
    /// </example>
    public class StateProperty
    {
        #region public Property
        /// <summary>
        ///  [Readonly] Get StateName
        /// </summary>
        public string StateName => stateName;

        #endregion


        #region public Method
        /// <summary>
        ///  Constructor of StateProperty
        /// </summary>
        public StateProperty()
        {
            stateName = StateUtility.Normal;
        }

        /// <summary>
        ///  Constructor of StateProperty
        /// </summary>
        /// 
        /// <example>
        /// <code>
        ///  StateProperty normal = new StateProperty(StateUtility.Normal);
        /// </code>
        /// </example>
        public StateProperty(string stateName)
        {
            this.stateName = stateName;
        }

        /// <summary>
        ///  Constructor of StateProperty
        /// </summary>
        /// 
        /// <example>
        /// <code>
        ///  StateProperty normal = new StateProperty(StateUtility.Normal, property); // Copy property value
        /// </code>
        /// </example>
        /// 
        public StateProperty(string stateName, StateProperty property)
        {
            if (stateName == null)
            {
                return;
            }

            if (property == null)
            {
                return;
            }

            this.stateName = stateName;


            CopyProperty(property);
        }

        /// <summary>
        ///  Property array using indexer. You can use  SetPropertyValue(string propertyName , object value) as same manner.
        /// </summary>
        /// 
        /// <example>
        /// <code>
        ///  StateProperty normal = new StateProperty(StateUtility.Normal);
        ///  normal["BackgroundColor"] = Color.Red;
        /// </code>
        /// </example>
        public object this[string propertyName]
        {
            get => GetPropertyValue(propertyName);
            set => SetPropertyValue(propertyName, value);
        }

        /// <summary>
        ///  Set property value at StateProperty
        /// </summary>
        /// <param name="propertyName">Property name </param>
        /// <param name="value">Real value for each Property</param>
        /// <returns>True if the key event should be consumed</returns>
        /// <example>
        /// <code>
        ///  StateProperty normal = new StateProperty(StateUtility.Normal);
        ///  normal.SetPropertyValue("BackgroundColor", Color.Red );
        /// </code>
        /// </example>
        public void SetPropertyValue(string propertyName, object value)
        {
            Dictionary<string, object> dictionary = GetPropertyDictionary(propertyName);

            dictionary[propertyName] = value;
        }

        /// <summary>
        ///  Get property value at StateProperty
        /// </summary>
        /// <param name="propertyName">Property name </param>
        /// <returns>Value of property as object</returns>
        /// <example>
        /// <code>
        ///  StateProperty normal = new StateProperty(StateUtility.Normal);
        ///  normal["BackgroundColor"] = Color.Red;
        ///  Color color = normal.GetPropertyValue("BackgroundColor") as Color;
        /// </code>
        /// </example>
        public object GetPropertyValue(string propertyName)
        {
            Dictionary<string, object> dictionary = GetPropertyDictionary(propertyName);

            if (dictionary.TryGetValue(propertyName, out object value))
            {
                return value;
            }

            return null;
        }


        /// <summary>
        ///  StateProperty + operator overloading , Base property's value will be overwritten by addOperand if value is same. baseProp shouldn't be null
        /// </summary>
        /// <param name="baseProp">base operand property</param>
        /// <param name="overwriteProp"> overwrite operand property</param>
        /// <returns>merged value</returns>
        /// <version> 8.8.0 </version>
        public static StateProperty operator +(StateProperty baseProp, StateProperty overwriteProp)
        {
            if (baseProp == null)
            {
                throw new ArgumentNullException("baseProp", "NULL exception");
            }

            if (overwriteProp != null)
            {
                foreach (KeyValuePair<string, object> items in overwriteProp.animatable)
                {
                    baseProp.animatable[items.Key] = items.Value;
                }

                foreach (KeyValuePair<string, object> items in overwriteProp.unanimatable)
                {
                    baseProp.unanimatable[items.Key] = items.Value;
                }
            }

            return baseProp;
        }


        #endregion

        internal void AddStateAction(Action<string, string> action)
        {
            if (action == null)
            {
                return; // Add exception dy.chu
            }

            stateAction += action;
        }

        internal void RunStateAction(string from)
        {
            stateAction?.Invoke(from, StateName);
        }

        internal void Clear()
        {
            animatable.Clear();
            animatable = null;
            unanimatable.Clear();
            unanimatable = null;

            stateAction = null;
        }
        #region private method

        private void CopyProperty(StateProperty property)
        {
            animatable = new Dictionary<string, object>(property.animatable);
            unanimatable = new Dictionary<string, object>(property.unanimatable);

            stateAction = property.stateAction;
        }

        private Dictionary<string, object> GetPropertyDictionary(string propertyName)
        {
            if (PropertyUtility.IsAnimatableProperty(propertyName))
            {
                return animatable;
            }
            return unanimatable;
        }

        #endregion


        #region internal Field

        internal Dictionary<string, object> animatable = new Dictionary<string, object>();

        internal Dictionary<string, object> unanimatable = new Dictionary<string, object>();

        internal Action<string, string> stateAction;

        #endregion


        #region private field

        private readonly string stateName = null;

        #endregion

    }


}
