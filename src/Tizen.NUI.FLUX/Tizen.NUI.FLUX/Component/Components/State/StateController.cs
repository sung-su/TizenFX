/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file StateController.cs
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
using System.Linq;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// This is StateController class
    /// <code>
    /// StateController stateController = new StateController();
    /// </code>
    /// </summary>
    internal class StateController
    {
        #region public function
        /// <summary>
        /// Constructor StateController
        /// </summary>
        public StateController()
        {
            StateList = new List<int>() { 0, 0, 0, 0, 0 };
            FlagState = new List<string>() { StateUtility.Focused, StateUtility.Checked, StateUtility.LongPressed, StateUtility.Pressed };
        }

        /// <summary>
        /// Add userStateName in List
        /// </summary>
        /// <code>
        /// stateController.AddUserStateList("userStateName");
        /// </code>
        /// <param name="userStateName">userStateName</param>
        public void AddUserStateList(string userStateName)
        {
            StateList.Add(0);
            FlagState.Add(userStateName);
        }

        /// <summary>
        /// If you want to change all userState to false, you call this function.
        /// <code>
        /// stateController.UnsetAllUserStateList();
        /// </code>
        /// </summary>
        public void UnsetAllUserStateList()
        {
            for (int i = 3; i < StateList.Count; i++)
            {
                StateList[i] = 0;
            }
        }
        /// <summary>
        /// Unset userStateName in List
        /// </summary>
        /// <param name="userStateName">userStateName</param>
        public void UnsetUserStateList(string userStateName)
        {
            StateList[FindStateindex(userStateName)] = 0;
        }
        /// <summary>
        /// UpdateStateList about each state.
        /// StateList[NDSIndex] = 0(Normal),1(Selected) ,2(Disabled) / StateList[1] = 0, 1(Focused) /  StateList[2] = 0, 1(Checked)
        /// StateList[3~] = 0, 1(UserState)
        /// </summary>
        /// <code>
        /// stateController.UpdateStateList(userStateName, true);
        /// </code>
        /// <param name="stateName">stateName to update</param>
        /// <param name="flag">True/False</param>
        public void UpdateStateList(string stateName, bool flag)
        {
            if (IsBasicState(stateName) == true)
            {
                if (flag == true)
                {
                    StateList[BasicStateIndex] = GetBasicStateValue(stateName);
                }
                else
                {
                    StateList[BasicStateIndex] = 0; // if flag is false, then NDS state should be Normal state
                }
            }
            else
            {
                StateList[FindStateindex(stateName)] = Convert.ToInt32(flag);
            }

            UpdateCurrentState();
        }

        /// <summary>
        /// If you want to know currentState, you should call it.
        /// </summary>
        /// <code>
        /// stateController.CurrentState();
        /// </code>
        /// <returns>CurrentState</returns>

        public void Clear()
        {
            if (StateList != null)
            {
                StateList.Clear();
                StateList = null;
            }
            if (FlagState != null)
            {
                FlagState.Clear();
                FlagState = null;
            }

            foreach (KeyValuePair<MotionTypes, FluxAnimationPlayer> item in ComponentAnimators)
            {
                if (item.Value is FluxAnimationPlayer animator)
                {
                    animator.Stop();
                    animator.Clear();
                    animator.Dispose();
                }
            }
            ComponentAnimators.Clear();
            ComponentAnimators = null;
        }

        #region internal property

        internal string CurrentState
        {
            get => currentState;
            set
            {
                if (currentState != value)
                {
                    previousState = currentState;
                    currentState = value;
                    UpdateStateFlag(currentState);
                    UpdatePresetState();
                }
            }
        }

        internal string PreviousState => previousState;


        internal string BasicState
        {
            get
            {
                string basicState = StateUtility.Normal;

                if (StateList[BasicStateIndex] == 1)
                {
                    basicState = StateUtility.Selected;
                }
                else if (StateList[BasicStateIndex] == 2)
                {
                    basicState = StateUtility.Disabled;
                }

                return basicState;
            }
        }

        internal bool IsFocusedState => (StateList[FindStateindex(StateUtility.Focused)] == 1);

        internal bool IsCheckedState => (StateList[FindStateindex(StateUtility.Checked)] == 1);

        internal bool IsLongPressState => (StateList[FindStateindex(StateUtility.LongPressed)] == 1);

        internal bool IsPressedState => (StateList[FindStateindex(StateUtility.Pressed)] == 1);
        internal string PresetState => presetState;


        internal bool IsBasicState(string stateName)
        {
            return (stateName == StateUtility.Normal) || (stateName == StateUtility.Selected) || (stateName == StateUtility.Disabled);
        }

        internal FluxAnimationPlayer GetStateAnimator(MotionTypes type)
        {
            if (ComponentAnimators == null || StaticAnimators == null)
            {
                return null;
            }


            if (StaticAnimators.TryGetValue(type, out FluxAnimationPlayer animator) == false)
            {
                if (ComponentAnimators.TryGetValue(type, out animator) == false)
                {
                    animator = ComponentAnimators[type] = new FluxAnimationPlayer(type);
                }
            }

            return animator;
        }

        internal void StopPlayingMotion()
        {
            foreach (KeyValuePair<MotionTypes, FluxAnimationPlayer> item in ComponentAnimators)
            {
                if (item.Value is FluxAnimationPlayer animator)
                {
                    if (animator.State == FluxAnimationPlayer.States.Playing)
                    {
                        animator.Stop(FluxAnimationPlayer.EndActions.StopFinal);
                    }
                }
            }

            foreach (KeyValuePair<MotionTypes, FluxAnimationPlayer> item in StaticAnimators)
            {
                if (item.Value is FluxAnimationPlayer animator)
                {
                    if (animator.State == FluxAnimationPlayer.States.Playing)
                    {
                        animator.Stop(FluxAnimationPlayer.EndActions.StopFinal);
                    }
                }
            }
        }


        #endregion

        #endregion
        #region private Method
        private int FindStateindex(string userState)
        {
            return FlagState.IndexOf(userState) + 1;
        }


        private void UpdateCurrentState()
        {
            previousState = currentState;

            if (StateList[BasicStateIndex] == 0)
            {
                currentState = StateUtility.Normal;
            }
            else if (StateList[BasicStateIndex] == 1)
            {
                currentState = StateUtility.Selected;
            }
            else if (StateList[BasicStateIndex] == 2)
            {
                currentState = StateUtility.Disabled;
            }

            presetState = currentState;
            if (IsFocusedState)
            {
                presetState += StateUtility.Focused;
            }
            if (IsPressedState)
            {
                presetState += StateUtility.Pressed;
            }

            List<int> tmplist = StateList.GetRange(1, StateList.Count - 1).ToList();
            int i = 0;

            foreach (int stateType in tmplist)
            {
                if (stateType == 1)
                {
                    currentState += FlagState[i];
                }
                i++;
            }
        }

        private void UpdatePresetState()
        {
            if (StateList[BasicStateIndex] == 0)
            {
                presetState = StateUtility.Normal;
            }
            else if (StateList[BasicStateIndex] == 1)
            {
                presetState = StateUtility.Selected;
            }
            else if (StateList[BasicStateIndex] == 2)
            {
                presetState = StateUtility.Disabled;
            }

            presetState += ((IsFocusedState) ? StateUtility.Focused : "");
            presetState += ((IsPressedState) ? StateUtility.Pressed : "");
        }


        private int GetBasicStateValue(string stateName)
        {
            int retVal = 0;

            if (stateName.Contains(StateUtility.Normal) == true)
            {
                retVal = 0;
            }
            else if (stateName.Contains(StateUtility.Selected) == true)
            {
                retVal = 1;
            }
            else if (stateName.Contains(StateUtility.Disabled) == true)
            {
                retVal = 2;
            }
            else if (stateName.Contains(StateUtility.Pressed) == true)
            {
                retVal = 3;
            }
            return retVal;
        }

        private void UpdateStateFlag(string checkState)
        {
            if (currentState != null)
            {
                StateList[FindStateindex(StateUtility.Checked)] = Convert.ToInt32(checkState.Contains(StateUtility.Checked));
                StateList[FindStateindex(StateUtility.Focused)] = Convert.ToInt32(checkState.Contains(StateUtility.Focused));
                StateList[FindStateindex(StateUtility.Pressed)] = Convert.ToInt32(checkState.Contains(StateUtility.Pressed));
                StateList[BasicStateIndex] = GetBasicStateValue(checkState);
            }
        }

        internal MotionTypes GetPredefinedMotionType()
        {
            MotionTypes ret = MotionTypes.Undefined;

            //if (TryGetLongPressMotionType(previousState.Contains(StateUtility.LongPressed), currentState.Contains(StateUtility.LongPressed), out ret) == false)
            {
                if (StateMotionMap.TryGetValue(previousState + "to" + currentState, out ret) == false)
                {
                    return MotionTypes.Undefined;
                }
            }
            return ret;
        }
        private bool TryGetLongPressMotionType(bool preLongPressed, bool curLongPressed, out MotionTypes type)
        {
            bool ret = false;
            type = MotionTypes.Undefined;

            if (curLongPressed == true)
            {
                if (IsFocusedState == true)
                {
                    type = MotionTypes.LongPress_Focused;
                    ret = true;
                }
                else
                {
                    type = MotionTypes.LongPress_Normal;
                    ret = true;
                }
            }
            else if (preLongPressed == true)
            {
                if (IsFocusedState == true)
                {
                    type = MotionTypes.FocusIn_01;
                    ret = true;
                }
                else
                {
                    type = MotionTypes.LongPress_End;
                    ret = true;
                }
            }

            return ret;
        }

        #endregion
        #region private field
        private List<int> StateList = null;
        private List<string> FlagState = null;

        private string currentState = StateUtility.Normal;
        private string previousState = "";
        private string presetState = StateUtility.Normal;

        internal static Dictionary<string, MotionTypes> StateMotionMap = new Dictionary<string, MotionTypes>()
        {
            { StateUtility.Normal + "to" + StateUtility.NormalFocused , MotionTypes.FocusIn_01 },
            { StateUtility.Disabled + "to" + StateUtility.DisabledFocused , MotionTypes.FocusIn_01 },
            { StateUtility.Selected + "to" + StateUtility.SelectedFocused , MotionTypes.FocusIn_01 },

            { StateUtility.NormalFocused + "to" + StateUtility.Normal , MotionTypes.FocusOut_01 },
            { StateUtility.DisabledFocused + "to" + StateUtility.Disabled , MotionTypes.FocusOut_01 },
            { StateUtility.SelectedFocused + "to" + StateUtility.Selected , MotionTypes.Select_In },

            { StateUtility.NormalChecked + "to" + StateUtility.NormalFocusedChecked , MotionTypes.FocusIn_01 },
            { StateUtility.DisabledChecked + "to" + StateUtility.DisabledFocusedChecked , MotionTypes.FocusIn_01 },
            { StateUtility.SelectedChecked + "to" + StateUtility.SelectedFocusedChecked , MotionTypes.FocusIn_01 },

            { StateUtility.NormalFocusedChecked + "to" + StateUtility.NormalChecked , MotionTypes.FocusOut_01 },
            { StateUtility.DisabledFocusedChecked + "to" + StateUtility.DisabledChecked , MotionTypes.FocusOut_01 },
            { StateUtility.SelectedFocusedChecked + "to" + StateUtility.SelectedChecked , MotionTypes.Select_In },

            { StateUtility.NormalFocusedChecked + "to" + StateUtility.Normal , MotionTypes.FocusOut_01 },
            { StateUtility.DisabledFocusedChecked + "to" + StateUtility.Disabled , MotionTypes.FocusOut_01 },
            { StateUtility.SelectedFocusedChecked + "to" + StateUtility.Selected , MotionTypes.Select_In },


            { StateUtility.Normal + "to" + StateUtility.Selected , MotionTypes.Select_In },
            { StateUtility.Disabled + "to" + StateUtility.Selected , MotionTypes.Select_In },

            { StateUtility.Selected + "to" + StateUtility.Normal , MotionTypes.Select_Out },
            { StateUtility.Selected + "to" + StateUtility.Disabled , MotionTypes.Select_Out },

            { StateUtility.NormalChecked + "to" + StateUtility.SelectedChecked , MotionTypes.Select_In },
            { StateUtility.DisabledChecked + "to" + StateUtility.SelectedChecked , MotionTypes.Select_In },

            { StateUtility.SelectedChecked + "to" + StateUtility.NormalChecked , MotionTypes.Select_Out },
            { StateUtility.SelectedChecked + "to" + StateUtility.DisabledChecked , MotionTypes.Select_Out },

        };

        private Dictionary<MotionTypes, FluxAnimationPlayer> ComponentAnimators = new Dictionary<MotionTypes, FluxAnimationPlayer>();

        private static readonly Dictionary<MotionTypes, FluxAnimationPlayer> StaticAnimators = new Dictionary<MotionTypes, FluxAnimationPlayer>()
        {
            // {MotionTypes.FocusIn_01 , new FluxAnimationPlayer(MotionTypes.FocusIn_01) },
        };


        private readonly int BasicStateIndex = 0;



        #endregion
    }
}
