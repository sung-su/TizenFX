/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file StateMachine.cs
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
    /// This is StateMachine class.
    /// You have to make it in preset.
    /// </summary>
    public class StateMachine
    {
        /// <summary>
        /// State change delegate
        /// </summary>
        public delegate void StateChangeAction();

        #region public property
        /// <summary>
        /// CurrentState
        /// </summary>
        public string CurrentState
        {
            get => stateController.CurrentState;
            set => ChangeState(value);
        }
        /// <summary>
        /// PreviouseState
        /// </summary>
        public string PreviousState => stateController.PreviousState;
        /// <summary>
        ///  Array StateProperty using indexer
        /// </summary>
        /// 
        /// <example>
        /// <code>
        ///  StateMachine stateMachine = new StateMachine();
        ///  stateMachine[StateUtility.Normal] = new StateProperty(StateUtility.Normal);
        ///  
        /// StateProperty normal = stateMachine[StateUtility.Normal];
        /// </code>
        /// </example>
        public StateProperty this[string state]
        {
            get => GetStateProperty(state);
            set => AddStateProperty(value);
        }


        /// <summary>
        /// Get Current StateProperty class
        /// </summary>
        public StateProperty CurrentStateProperty => stateProperties[stateController.CurrentState];

        /// <summary>
        /// Action for post all state. There are 2 action parameter which means  previous and current. 
        /// All other property's action (AddStateAction)will be called before applying property value which assigned by state machine.
        /// But this action will be called after applying property. So, if user do something influenced by property value at action , please use it. 
        /// Please refer below example.
        /// </summary>
        /// <example> 
        ///  internal void FocusChangedAction(string from, string to)
        ///  {
        ///       bool isFocusedFrom = from.Contains(StateUtility.Focused);
        ///       bool isFocusedTo = to.Contains(StateUtility.Focused);
        ///       
        ///      if (isFocusedFrom == false &amp;&amp; isFocusedTo == true)
        ///      {
        ///          GainFocus();
        ///      }
        ///      else if (isFocusedFrom == true &amp;&amp; isFocusedTo == false)
        ///      {
        ///          LostFocus();
        ///      }
        ///  }
        ///  
        ///  Component.State.PostStateAction += FocusChangedAction;
        /// 
        /// </example>
        /// <version>8.8.1 </version>
        public Action<string, string> PostStateAction;

        #endregion

        #region public method
        /// <summary>
        /// This is Constructor in StateMachine
        /// </summary>
        public StateMachine()
        {
            Initialize();
        }
        /// <summary>
        /// AddStateProperty in StateMachine 
        /// You should add 12 State that UX defined.
        /// </summary>
        /// <param name="property"></param>
        public void AddStateProperty(StateProperty property)
        {
            if (property == null)
            {
                return;
            }

            if (property.StateName == null)
            {
                return;
            }

            UpdateStateProperty(property);
        }

        /// <summary>
        /// If you want to change propertyValue , you enter stateName, propertyName, propertyValue
        /// </summary>
        /// <code>
        /// stateMachine.ChangeStatePropertyValue(stateName , propertyname, value);
        /// </code>
        /// <param name="stateName">StateName</param>
        /// <param name="propertyName">PropertyName</param>
        /// <param name="value">PropertyValue</param>
        public void ChangeStatePropertyValue(string stateName, string propertyName, object value)
        {
            if (stateName == null || propertyName == null || value == null)
            {
                return;
            }

            StateProperty stateProperty = GetStateProperty(stateName);
            stateProperty.SetPropertyValue(propertyName, value);
        }

        /// <summary>
        /// If you want to change property action, you can add Action by this API.
        /// </summary>
        /// <param name="stateName">StateName</param>
        /// <param name="action">Add action which will be invoked when component chagne to target state</param>
        public void AddStateAction(string stateName, Action<string, string> action)
        {
            StateProperty stateProperty = GetStateProperty(stateName);

            stateProperty?.AddStateAction(action);

            if (stateName == StateUtility.StateAll)
            {
                stateChangeAction += action;
            }
        }

        #endregion

        #region internal property

        internal string PresetState => stateController.PresetState;

        internal void ChangeStatePropertyValueInternal(string stateName, string propertyName, object value)
        {
            if (UIConfig.SupportPointingMode == UIConfig.PointingMode.SupportTouchOnly && stateName == StateUtility.Focused)
            {

                return;
            }

            ChangeStatePropertyValue(stateName, propertyName, value);
        }

        #endregion


        #region internal method

        internal StateProperty GetCurrentStateProperty()
        {
            if (stateController == null)
            {
                return null;
            }

            string currentState = stateController.CurrentState;
            StateProperty retProp = new StateProperty(currentState);

            if (stateProperties.TryGetValue(StateUtility.Basic, out StateProperty tmpProp) == true)
            {
                retProp += tmpProp;
            }

            if (stateController.IsCheckedState == true && stateProperties.TryGetValue(StateUtility.Checked, out tmpProp) == true)
            {
                retProp += tmpProp;
            }

            if (stateController.IsFocusedState == true && stateProperties.TryGetValue(StateUtility.Focused, out tmpProp) == true)
            {
                retProp += tmpProp;
            }

            if (stateController.IsPressedState == true && stateProperties.TryGetValue(StateUtility.Pressed, out tmpProp) == true)
            {
                retProp += tmpProp;
            }

            if (stateProperties.TryGetValue(currentState, out tmpProp) == true)
            {
                retProp += tmpProp;
            }

            if (stateProperties.TryGetValue(StateUtility.StateAll, out tmpProp) == true)
            {
                retProp += tmpProp;
            }

            return retProp;
        }

        internal void ChangeState(string toState)
        {
            string current = stateController.CurrentState;

            if (current != toState)
            {
                stateController.CurrentState = toState;
                InvokeStateActions(current, toState);
            }
        }


        internal StateMachine Clone()
        {
            StateMachine retStateMachine = new StateMachine();

            foreach (StateProperty property in stateProperties.Values)
            {
                retStateMachine.AddStateProperty(property);
            }

            retStateMachine.stateChangeAction = stateChangeAction;

            return retStateMachine;
        }

        internal void Clear()
        {
            stateChangeAction = null;
            PostStateAction = null;

            if (stateProperties != null)
            {
                foreach (KeyValuePair<string, StateProperty> items in stateProperties)
                {
                    items.Value?.Clear();
                }
                stateProperties.Clear();
                stateProperties = null;
            }

            stateController.Clear();
            stateController = null;
        }

        internal FluxAnimationPlayer UpdateState(string stateName, bool flag)
        {
            stateController.UpdateStateList(stateName, flag);

            InvokeStateActions(stateController.PreviousState, stateController.CurrentState);

            MotionTypes type = stateController.GetPredefinedMotionType();

            if (type == MotionTypes.Undefined)
            {
                return null;
            }

            return stateController.GetStateAnimator(type);
        }

        #endregion

        #region private Method
        private void Initialize()
        {

        }


        private void UpdateStateProperty(StateProperty property)
        {
            if (property.StateName == null)
            {
                return;
            }

            stateProperties[property.StateName] = property;
        }

        internal StateProperty GetStateProperty(string state)
        {

            if (stateProperties.TryGetValue(state, out StateProperty retProp) == false)
            {
                retProp = new StateProperty(state);
                stateProperties[state] = retProp;
            }

            return retProp;
        }

        internal void InvokeStateActions(string from, string to)
        {
            StateProperty stateProp = this[to];
            stateProp?.RunStateAction(from);

            stateChangeAction?.Invoke(from, to);
        }

        #endregion


        #region private field

        internal Dictionary<string, StateProperty> stateProperties = new Dictionary<string, StateProperty>();

        internal StateController stateController = new StateController();

        internal Action<string, string> stateChangeAction;

        #endregion


    }


}
