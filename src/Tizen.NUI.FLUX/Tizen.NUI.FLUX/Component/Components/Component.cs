/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file Component.cs
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
using System.ComponentModel;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// Component is base class of flux components.
    /// </summary>
    /// <version> 6.6.0 </version>
    /// <code>
    /// Component component = new Component();
    /// component.UnitSize = new UnitSize(10,10);
    /// </code>

    public partial class Component : ComponentBase, IStatable
    {

        #region public Property
        /// <summary>
        /// Updates state property by adding StatePropertyDefinition.
        /// Note that objects added to this collection are removed after update value.
        /// This property is only intended for use by the XAML Application.
        /// </summary>
        public StatePropertyDefinitionCollection StatePropertyDefinitions
        {
            get
            {
                if (statePropertyDefinitions == null)
                {
                    statePropertyDefinitions = new StatePropertyDefinitionCollection(this);
                }
                return statePropertyDefinitions;
            }
        }

        /// <summary> Background Color for Theme </summary>
        /// <version> 9.9.0 </version>
        public const string PlaneThemeColor = "PlaneColorInternal";


        /// <summary>
        /// Event arguments of Execute.
        /// </summary>
        /// <version> 9.9.0 </version>
        public class ExecuteEventArgs : EventArgs
        {
            //TODO
            //nothing in now, have to define spec.

            /// <summary>
            /// Provides a value to use with events that do not have event data.
            /// </summary>
            internal static new readonly ExecuteEventArgs Empty = new ExecuteEventArgs();
        }
        /// <summary>
        /// Event arguments of LongPressExecute.
        /// </summary>
        /// <version> 9.9.0 </version>
        public class LongPressExecuteEventArgs : EventArgs
        {
            //TODO
            //nothing in now, have to define spec.

            /// <summary>
            /// Provides a value to use with events that do not have event data.
            /// </summary>
            internal static new readonly LongPressExecuteEventArgs Empty = new LongPressExecuteEventArgs();
        }

        /// <summary>
        /// Connect StateMachine in Preset.
        /// </summary>
        public new StateMachine State
        {
            get => (StateMachine)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        /// <summary>
        /// Default is false(Normal state)
        /// If you want to disabled state to component, you set this value to true.
        /// </summary>
        public bool Disabled
        {
            get => (bool)GetValue(DisabledProperty);
            set => SetValue(DisabledProperty, value);
        }

        /// <summary>
        /// Default is false(Normal state)
        /// If you want to selected state to component, you set this value to true.
        /// </summary>
        public bool Selected
        {
            get => (bool)GetValue(SelectedProperty);

            set => SetValue(SelectedProperty, value);
        }

        /// <summary>
        /// Default is false
        /// If you want to checked state to component, you set this value to true.
        /// </summary>
        public bool Checked
        {
            get => (bool)GetValue(CheckedProperty);
            set => SetValue(CheckedProperty, value);
        }

        /// <summary>
        /// Set item for fast scroll inside scrollview or list control
        /// </summary>
        /// <version> 9.9.1 </version>
        public bool IsItemForFastScroll
        {
            get => (bool)GetValue(IsItemForFastScrollProperty);

            set => SetValue(IsItemForFastScrollProperty, value);
        }

        /// <summary>
        /// If user want to use default focus in/out motion defined by Principle., make it true. Default value is false.
        /// </summary>
        /// <version> 8.8.0 </version>
        public bool DefaultFocusMotionEnabled
        {
            get => (bool)GetValue(DefaultFocusMotionEnabledProperty);

            set => SetValue(DefaultFocusMotionEnabledProperty, value);
        }

        /// <summary>
        /// If user want to use default select in/out motion defined by Principle., make it true. Default value is false.
        /// </summary>
        /// <version> 8.8.0 </version>
        public bool DefaultSelectMotionEnabled
        {
            get => (bool)GetValue(DefaultSelectMotionEnabledProperty);

            set => SetValue(DefaultSelectMotionEnabledProperty, value);
        }

        /// <summary>
        /// Send Key event Instead of Execute event.
        /// Default value is false.
        /// If set false, when touched component, not received execute and return key event.
        /// </summary>
        /// <version> 10.10.0 </version>
        public bool SendKeyInsteadExecute
        {
            set => SetValue(SendKeyInsteadExecuteProperty, value);
            get => (bool)GetValue(SendKeyInsteadExecuteProperty);
        }

        /// <summary>
        /// In view.FocusGained event, user can call this API to check whether the focus is gained by Touch
        /// </summary>
        /// <version>10.10.0</version>
        public bool FocusedByPointing
        {
            get;
            internal set;
        } = false;
        /// <summary>
        /// An event for the Execution signal which can be used to subscribe or unsubscribe the event handler provided by the user in Component touch mode.
        /// The Execution signal is emitted when the control gets the PointingBehaviorMode.
        /// </summary>
        /// <version> 9.9.0 </version>
        /// <code>
        /// component.Execute += ExecuteEventHandler;
        /// </code>
        public event EventHandler<ExecuteEventArgs> Execute
        {
            add
            {
                executeEventHandler += value;
            }
            remove
            {
                executeEventHandler -= value;
            }
        }

        /// <summary>
        /// An event for the touch long pressed signal which can be used to subscribe or unsubscribe the event handler provided by the user in Component touch mode.
        /// The LongPressExecute signal is emitted when a long press gesture occurs.
        /// </summary>
        /// <version> 9.9.0 </version>
        /// <code>
        /// component.LongPressExecute += LongPressExecuteEventHandler;
        /// </code>
        public event EventHandler<LongPressExecuteEventArgs> LongPressExecute
        {
            add
            {
                longPressExecuteEventHandler += value;
            }
            remove
            {
                longPressExecuteEventHandler -= value;
            }
        }

        /// <summary>
        /// Enabler for Input Event. 
        /// </summary>
        /// <code>
        ///  component.InputEnabler[Constant.Touch] = true; // Enable Touch event
        ///  if( component.InputEnabler[Constant.Touch]  == true ) // Check Touch Event is enabled 
        ///  {
        ///  }
        /// </code>
        /// <version> 9.9.0 </version>
        public Enabler<string> InputEnabler
        {
            get
            {
                if (inputEnabler == null)
                {
                    inputEnabler = new Enabler<string>();
                    BindEnableAction();
                }

                return inputEnabler;
            }
        }

        /// <summary>
        /// Change Component ThemeColorChip or ThemeColorPreset . 
        /// </summary>
        /// <code>
        ///  textBox.ThemeColor["TextColor"].ColorChip = "CC_Point3100"; 
        /// </code>
        /// <version> 9.9.0 </version>
        public ThemeColor ThemeColor
        {
            get => (ThemeColor)GetValue(ThemeColorProperty);
            set => SetValue(ThemeColorProperty, value);
        }

        /// <summary>
        /// Component touch mode. User can choose their component as Pointing action. Default is PressIsFocus
        /// </summary>
        /// <version> 9.9.0 </version>
        public PointingBehaviorMode PointingBehavior
        {
            get => (PointingBehaviorMode)GetValue(PointingBehaviorProperty);
            set => SetValue(PointingBehaviorProperty, value);
        }

        /// <summary>
        /// Enable Component Apply Pressed State
        /// </summary>
        /// <code>
        ///  textBox.PressedStateEnabled = true; 
        /// </code>
        /// <version> 10.10.1 </version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool PressedStateEnabled
        {
            set
            {
                if (value == true)
                {
                    StateUtility.UpdatePressedOpacityValue(this, Constant.OPACITY_PRESSED);
                }
                else
                {
                    StateUtility.UpdatePressedOpacityValue(this, Constant.OPACITY_ORIGINAL);
                }
            }
        }

        #endregion

        #region private Property
        private StateMachine privateState
        {
            get => stateMachine;
            set => stateMachine = value;
        }

        private bool privateDisabled
        {
            get => bDisabled;
            set
            {
                bDisabled = value;
                if (bDisabled == true && bSelected == true)
                {
                    bSelected = false;
                }

                DoDisableState();
            }
        }

        private bool privateSelected
        {
            get => bSelected;
            set
            {
                if (bSelected == value)
                {
                    return;
                }

                bSelected = value;
                if (bDisabled == true && bSelected == true)
                {
                    bDisabled = false;
                }

                DoSelectState();
            }
        }

        private bool privateChecked
        {
            get => bChecked;
            set
            {
                if (bChecked == value)
                {
                    return;
                }
                bChecked = value;
                if (stateMachine != null)
                {
                    stateMachine.UpdateState(StateUtility.Checked, bChecked);
                    PropagateState(stateMachine.CurrentState, null, true);
                }
            }
        }

        private bool privateIsItemForFastScroll
        {
            get => isItemForFastScroll;
            set
            {
                isItemForFastScroll = value;
                ApplyAsFastScrollItem(isItemForFastScroll);
            }
        }

        private bool privateDefaultFocusMotionEnabled
        {
            get;
            set;
        } = false;

        private bool privateDefaultSelectMotionEnabled
        {
            get;
            set;
        } = false;

        private bool privateSendKeyInsteadExecute
        {
            get;
            set;
        } = false;
        private ThemeColor privateThemeColor
        {
            get
            {
                if (themeColor == null)
                {
                    themeColor = new ThemeColor(this);
                }
                return themeColor;
            }
            set
            {
                themeColor = value;
                themeColor.Requirer = this;
            }
        }

        private PointingBehaviorMode privatePointingBehavior
        {
            get
            {
                if (UIConfig.SupportPointingMode != UIConfig.PointingMode.SupportTouchOnly)
                {
                    return pointingBehavior;
                }
                else
                {
                    return PointingBehaviorMode.PressIsFocus;
                }
            }
            set => pointingBehavior = value;
        }
        #endregion private Property

        #region internal property

        internal bool Focused
        {
            get => bFocused;
            set
            {
                if (bFocused == value)
                {
                    return;
                }

                bFocused = value;

                DoFocusState();
            }
        }

        internal enum LongPressMotionTypes
        {
            /// <summary>
            /// Scale & Opacity Motion
            /// </summary>
            Type01,
            /// <summary>
            /// Scale Motion
            /// </summary>
            Type02
        }

        internal virtual LongPressMotionTypes LongPressMotionType
        {
            get;
            set;
        } = LongPressMotionTypes.Type01;

        internal virtual bool LongPressed
        {
            get => bLongPressed;
            set
            {
                if (bLongPressed == value)
                {
                    return;
                }

                bLongPressed = value;

                DoLongPressState();
            }
        }

        internal bool Touched
        {
            get => bTouched;
            set
            {
                if (UIConfig.SupportPointingMode == UIConfig.PointingMode.SupportNone && value == true)
                {
                    return;
                }
                bTouched = value;

                if (stateMachine != null)
                {
                    stateMachine.UpdateState(StateUtility.Pressed, bTouched);
                    PropagateState(stateMachine.CurrentState, null, true);
                }
            }
        }
        internal bool IsFocused => Focused || HasFocus();



        internal bool AutoFocusRoundingOffEnabled
        {
            get;
            set;
        } = false;

        internal bool AudioFeedbackEnabled
        {
            get;
            set;
        } = true;



        #endregion

        #region public methods
        /// <summary>
        /// Construct an empty ComponentBase.
        /// </summary>
        public Component()
        {
            Initialize();
        }

        /// <summary>
        /// Construct an empty ComponentBase.
        /// </summary>
        public Component(string preset, string name = null) : base(name)
        {
            // Todo : We will delete presetName later
            presetName = preset;
            Initialize(preset);
        }

        internal Component(string preset, global::System.IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn)
        {
            presetName = preset;
            Initialize(preset);
        }

        internal Component(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn)
        {
            Initialize();
        }

        /// <summary>
        /// Called when the control gain key input focus. Should be overridden by derived classes if they need to customize what happens when focus is gained.
        /// </summary>
        public virtual void OnFocusGained()
        {

        }

        /// <summary>
        /// Called when the control loses key input focus. Should be overridden by derived classes if they need to customize what happens when focus is lost.
        /// </summary>
        public virtual void OnFocusLost()
        {
        }
        /// <summary>
        /// OnUpdate when user call Update directly
        /// </summary>
        /// <version> 6.6.0 </version>
        protected override void OnUpdate()
        {
        }


        /// <summary>
        /// Called after a key-event is received by the view that has had its focus set.
        /// </summary>
        /// <param name="key">The key event</param>
        /// <returns>True if the key event should be consumed</returns>
        public virtual bool OnKey(Key key)
        {
            if (HasFocus())
            {
                if (key.KeyPressedName == "Down" || key.KeyPressedName == "Up" || key.KeyPressedName == "Left" || key.KeyPressedName == "Right")
                {
                    if (key.State == Key.StateType.Down)
                    {
                        RepeatKeyManager.Instance.KeyPressed(key);
                    }
                    else if (key.State == Key.StateType.Up)
                    {
                        RepeatKeyManager.Instance.KeyReleased(key);
                    }
                }
            }

            if (FocusedControl != null)
            {
                return FocusedControl.OnKey(key);
            }
            return false;
        }

        internal void UpdateStatePropertyInternal(string stateName, string propertyName, object value)
        {
            if (UIConfig.SupportPointingMode == UIConfig.PointingMode.SupportTouchOnly && stateName == StateUtility.Focused)
            {
                return;
            }

            UpdateStateProperty(stateName, propertyName, value);
        }

        /// <summary>
        /// You want to change propertyName in your app, You should set stateName, propertyName, propertyValue.
        /// </summary>
        /// <param name="stateName"> state name which need to update</param>
        /// <param name="propertyName"> property name which need to update</param>
        /// <param name="value"> new value of specified property </param>
        public void UpdateStateProperty(string stateName, string propertyName, object value)
        {
            if (stateName == null)
            {
                return;
            }

            if (propertyName == null)
            {
                return;
            }

            stateMachine.ChangeStatePropertyValue(stateName, propertyName, value);

            if (PropagationNeeded(stateMachine.CurrentState, stateName) == true)
            {
                PropagateState(stateMachine.CurrentState, bForceUpdate: true);
            }
        }

        /// <summary>
        /// Get the element in component.
        /// </summary>
        /// <param name="eName">The element's name.</param>
        /// <returns>Element object of specific name</returns>
        public ComponentBase GetElement(string eName)
        {
            ComponentBase element = GetElementInternal(eName, true);
            if (element == null && eName == "BaseLayout")
            {
                element = GetElementInternal("RootLayout", true);
            }

            return element;
        }

        /// <summary>
        /// Show the component with appear animation
        /// </summary>
        /// <version> 6.6.0 </version>
        public void ShowWithAppearAnimation()
        {
            if (appearAnimationPlayer == null)
            {
                appearAnimationPlayer = GetAppearAnimator();
            }
            appearAnimationPlayer?.Reset(FluxAnimationPlayer.EndActions.StopFinal);
            disappearAnimationPlayer?.Reset(FluxAnimationPlayer.EndActions.StopFinal);

            PlayAppearAnimation();
        }


        /// <summary>
        /// Hide the component with disappear animation
        /// </summary>
        /// <version> 6.6.0 </version>
        public void HideWithDisappearAnimation()
        {
            if (disappearAnimationPlayer == null)
            {
                disappearAnimationPlayer = GetDisappearAnimator();
            }

            appearAnimationPlayer?.Reset(FluxAnimationPlayer.EndActions.StopFinal);
            disappearAnimationPlayer?.Reset(FluxAnimationPlayer.EndActions.StopFinal);

            PlayDisappearAnimation();
        }


        /// <summary>
        /// Set fake focus, this was not real focus, only state and visual spec is changed, so be care about it
        /// </summary>
        public void SetFocus()
        {
            focusGainedEventHandler?.Invoke(this, null);
        }

        /// <summary>
        ///  Kill fake focus, this was not real focus, only state and visual spec is changed, so be care about it
        /// </summary>
        public void KillFocus()
        {
            if (Focused == false)
            {
                return;
            }

            focusLostEventHandler?.Invoke(this, null);
        }

        /// <summary>
        ///  Update RootLayout from Component Side
        /// </summary>
        public override void UpdateLayout()
        {
            PreUpdateLayout();
            if (SizeWidth <= 0 || SizeHeight <= 0)
            {
                return;
            }

            if (GetElementInternal("RootLayout") is Layout rootLayout)
            {
                rootLayout.SizeWidth = SizeWidth;
                rootLayout.SizeHeight = SizeHeight;
                rootLayout.UpdateLayout();
            }
            PostUpdateLayout();
            //FluxLogger.InfoP("Name = %s1, Id = %d1, w = %d2,h = %d3", s1: Name, d1: ID, d2: (int)SizeWidth, d3: (int)SizeHeight);   // For Debug
        }

        internal override void UIDirectionChanged(object sender, DirectionChangedEventArgs e)
        {
            UpdateLayout();
            base.UIDirectionChanged(sender, e);
        }

        /// <summary>
        /// An event for the KeyInputFocusGained signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// The KeyInputFocusGained signal is emitted when the control gets the key input focus.<br />
        /// </summary>
        public new event EventHandler FocusGained
        {
            add
            {
                base.FocusGained += value;
                focusGainedEventHandler += value;
            }
            remove
            {
                base.FocusGained -= value;
                focusGainedEventHandler -= value;
            }
        }

        /// <summary>
        /// An event for the KeyInputFocusLost signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// The KeyInputFocusLost signal is emitted when the control loses the key input focus.<br />
        /// </summary>
        /// <version> 6.6.0 </version>
        public new event EventHandler FocusLost
        {
            add
            {
                base.FocusLost += value;
                focusLostEventHandler += value;
            }
            remove
            {
                base.FocusLost -= value;
                focusLostEventHandler -= value;
            }
        }

        /// <summary>
        /// Join element for state propagation
        /// </summary>
        /// <param name="element">Join Component</param>
        [Obsolete(@"Deprecated Deprecated since Tizen6.0  User don't need to call this API to want to propagate state of child. FLUX component will propagate to its children. And this method works before only 6.0(API 8.8.0)\n" +
        @"ingroup Tizen.TV.FLUX.Component\n" +
        @"brief Join state propagation\n" +
        @"since_tizen 6.0\n" +
        @"version 1.1.16\n" +
        @"privlevel platform\n" +
        @"privilege")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void JoinStatePropagation(ComponentBase element)
        {
        }

        #endregion

        #region protected methods
        /// <summary>
        /// Dispose Control.
        /// </summary>
        /// <param name="type">Dispose caused type</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.

                DoDispose();
            }

            //Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.
            //Unreference this from if a static instance refer to this. 

            //You must call base.Dispose(type) just before exit.
            base.Dispose(type);
        }

        #endregion

        #region ELEMENT internal private methods

        internal override bool CanBeChanged(string key)
        {
            string[] propertyArray =
            {
                "StatePropertyDefinitions",
                "State",
                "Disabled",
                "Selected",
                "Checked",
                "IsItemForFastScroll",
                "DefaultFocusMotionEnabled",
                "DefaultSelectMotionEnabled",
                "Execute",
                "LongPressExecute",
                "InputEnabler",
                "ThemeColor",
                "PointingBehavior"
            };

            for (int index = 0; index < propertyArray.Length; index++)
            {
                if (key == propertyArray[index])
                {
                    return true;
                }
            }
            return base.CanBeChanged(key);
        }
        // Add child to the element.
        internal void AddChild(ComponentBase parentComponent, string parentName)
        {
            for (int i = 1; i < elements?.Count; ++i)
            {
                string childName = parentName + "-" + i;

                if (elements.TryGetValue(childName, out ComponentBase childComponent) && childComponent != null)
                {
                    FluxLogger.InfoP("<<< AddChild parent name [%s1] child name [%s2]"
                        , s1: parentName
                        , s2: childName);
                    parentComponent.Add(childComponent);
                    AddChild(childComponent, childName);
                }
            }
        }

        // Add the element to its parent.
        internal void AddToParent(View view)
        {
            if (elements == null)
            {
                return;
            }

            string parentName = FindParent(view);

            if (parentName == null)
            {
                FluxLogger.FatalP("Cannot find parent of [%s1]", s1: view.Name);
                return;
            }

            if (elements.TryGetValue(parentName, out ComponentBase parentComponent))
            {
                FluxLogger.InfoP("Add child to parent [%s1] successfully", s1: parentName);
                parentComponent.Add(view);
            }
        }

        // Remove children from the element.
        internal void RemoveChildren()
        {
            if (elements == null || elements.Count == 0)
            {
                return;
            }

            foreach (KeyValuePair<string, ComponentBase> items in elements)
            {
                ComponentBase componentBase = items.Value;
                if (componentBase != null)
                {
                    FluxLogger.InfoP($">>> RemoveChild [%s1] Name [%s2] Component [%s3]"
                        , s1: Name
                        , s2: items.Key
                        , s3: componentBase.Name);
                    DestroyUtility.DestroyView(ref componentBase);
                }
            }
            elements.Clear();
        }

        internal void RemoveFromParent(View view)
        {
            string parentName = FindParent(view);

            if (elements == null)
            {
                return;
            }

            if (parentName == null)
            {
                FluxLogger.FatalP("Cannot find parent of [%s1]", s1: view.Name);
                return;
            }

            if (elements.TryGetValue(parentName, out ComponentBase parentView))
            {
                FluxLogger.InfoP("Remove child from parent successfully");
                parentView.Remove(view);
            }
        }

        internal string FindParent(View obj)
        {
            // parse child's name to get parent's name
            string childName = obj.Name;
            int pos = childName.LastIndexOf('-');
            if (pos <= 0)
            {
                FluxLogger.FatalP("ChildName error, childName [%s1]", s1: childName);
                return null;
            }
            string parentName = childName.Substring(0, pos);
            FluxLogger.InfoP("ParentName [%s1]", s1: parentName);

            return parentName;
        }


        /// <summary>
        /// Destroy the hierarchy constructor tree.
        /// </summary>
        internal void DestroyTree()
        {
            RemoveChildren();
        }

        /// <summary>
        /// Create the hierarchy constructor tree.
        /// </summary>
        internal void CreateTree()
        {
            if (GetElement("RootLayout", out string name) is ComponentBase root)
            {
                base.Add(root);  // ScrollView overrided the Add function, so must use base.Add() at here.
                AddChild(root, name);
            }
        }


        internal ComponentBase GetElement(string eName, out string componentName)
        {
            componentName = null;

            if (elementName == null || elements == null || eName == null)
            {
                return null;
            }

            ComponentBase element = null;
            if (elementName.TryGetValue(eName, out string value) && elements.TryGetValue(value, out ComponentBase componentBase))
            {
                componentName = value;
                element = componentBase;
            }
            return element;
        }


        internal void UpdateLayoutWithoutSize()
        {
            if (elementName == null || elements == null)
            {
                return;
            }

            FluxLogger.InfoP("name = [%s1], w = [%d1], h = [%d2]", s1: Name, d1: (int)SizeWidth, d2: (int)SizeHeight);
            if (GetElement("RootLayout") is Layout rootLayout)
            {
                rootLayout.UpdateLayout();
            }
        }

        // Get the element whose name is already defined in ElementName dictionary, this kind of element will be created OnDemand.
        internal virtual ComponentBase GetOnDemandCreatedElement(string eName)
        {
            return null;
        }


        private ComponentBase GetElementInternal(string eName, bool needPrintLog = false)
        {
            if (elementName == null || elements == null || eName == null)
            {
                return null;
            }

            ComponentBase element = null;
            if (elementName.TryGetValue(eName, out string name) && elements.TryGetValue(name, out ComponentBase componentBase))
            {
                element = (componentBase != null) ? componentBase : GetOnDemandCreatedElement(eName);
            }
            else
            {
                if (needPrintLog == true)
                {
                    FluxLogger.FatalP("Can't find the element named: [%s1]", s1: eName);
                }
            }
            return element;
        }

        #endregion

        #region THEME internal private methods
        internal Color PlaneColorInternal
        {
            get => GetPlaneColor();
            set => SetPlaneColor(value);
        }

        /// <summary>
        /// Set Color using string property name. 
        /// This is internal method. Don't use this 
        /// </summary>
        /// <param name="property">The target property name.</param>
        /// <param name="color">The color value.</param>
        /// <returns> if success then return true </returns>
        /// <version> 10.10.0 </version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool SetColorByPropertyInternal(string property, Color color)
        {
            switch (property)
            {
                case "PlaneColorInternal":
                    {
                        PlaneColorInternal = color;
                        return true;
                    }
                default:
                    break;
            }

            if (base.SetColorByPropertyInternal(property, color) == true)
            {
                return true;
            }
            return false;
        }

        internal virtual Color GetPlaneColor()
        {
            return BackgroundColor;
        }

        internal virtual void SetPlaneColor(Color color)
        {
            BackgroundColor = color;
        }

        // TODO: Remove circular dependency
        //internal void CreateCommonUIPlate()
        //{
        //    if (commonUIplate == null)
        //    {
        //        commonUIplate = new UIPlate
        //        {
        //            Name = "CommonUIPlate"
        //        };
        //        commonUIplate.ThemeColor[UIPlate.PlaneThemeColor].ColorPreset = null;
        //    }
        //}

        #endregion

        #region INTERACTION internal private methods

        internal virtual bool OnTouched(object source, TouchEventArgs e)
        {
            if (HasBody() == false)
            {
                FluxLogger.DebugP("[OnTouched] No body return false");
                return false;
            }

            FluxLogger.DebugP("[%s1].[%s2] touch [%s3], %s4", s1: GetTypeName(), s2: Name, s3: e.Touch.GetState(0).ToString(), s4: e.Touch.GetMouseButton(0).ToString());

            if (e.Touch.GetMouseButton(0) != MouseButton.Primary)
            {
                return true;
            }


            if (e.Touch.GetState(0) == PointStateType.Down)
            {
                if (PointingBehavior == PointingBehaviorMode.PressIsFocus && CanBeFocused == true)
                {
                    FocusedByPointing = true;
                    FocusManager.Instance.SetCurrentFocusView(this);
                    FocusedByPointing = false;
                    FluxLogger.DebugP("Touch Pressed Set Focus : [%s1] -- [%d1] ==> [%s2]"
                        , s1: GetTypeName()
                        , d1: Focusable ? 1 : 0
                        , s2: FocusManager.Instance.GetCurrentFocusView()?.GetTypeName());
                }

                Touched = true;
            }
            else if (e.Touch.GetState(0) == PointStateType.Up)
            {
                Touched = false;
            }

            return true;
        }

        internal virtual void OnTapped(object source, TapGestureDetector.DetectedEventArgs e)
        {
            if (HasBody() == false)
            {
                FluxLogger.ErrorP("[OnTapped] No body return false");
                return;
            }

            FluxLogger.DebugP("[%s1].[%s2] - TapGesture NumberOfTap : [%d1]", s1: GetTypeName(), s2: Name, d1: (int)e.TapGesture.NumberOfTaps);

            if (GestureDetector.Instance.IsPrimaryMouseButton(e.TapGesture) == false)
            {
                return;
            }

            if (e.TapGesture.NumberOfTaps == 1)
            {
                if (PointingBehavior == PointingBehaviorMode.ExecuteIsFocus && CanBeFocused == true)
                {
                    FocusedByPointing = true;
                    FocusManager.Instance.SetCurrentFocusView(this);
                    FocusedByPointing = false;
                    FluxLogger.DebugP("Touch Tap Set Focus : [%s1] -- [%d1] ==> [%s2]"
                        , s1: GetTypeName()
                        , d1: Focusable ? 1 : 0
                        , s2: FocusManager.Instance.GetCurrentFocusView()?.GetTypeName());
                    return;
                }

                if (IsFocused == true || PointingBehavior == PointingBehaviorMode.PressIsFocus)
                {
                    // Need discussion how to provide handler for Execution
                    SendExecuteEvent();
                }
            }
        }

        private bool CanBeFocused => IsFocused == false && Focusable == true;

        internal virtual void OnTouchLongPressed(object source, LongPressGestureDetector.DetectedEventArgs e)
        {
            FluxLogger.DebugP("[%s1].[%s2] - TapGesture NumberOfTap : [%d1]", s1: GetTypeName(), s2: Name, d1: (int)e.LongPressGesture.NumberOfTouches);

            if (GestureDetector.Instance.IsPrimaryMouseButton(e.LongPressGesture) == false)
            {
                return;
            }

            if (e.LongPressGesture.NumberOfTouches == 1 && e.LongPressGesture.State == Gesture.StateType.Started)  // LongPress Event comes twice, 1, time out; 2, mouse release.
            {
                if (IsFocused == true)   // Same as Execute event
                {
                    SendLongPressExecuteEvent();
                }
            }
        }

        internal virtual bool OnHovered(object source, HoverEventArgs e)
        {
            if (e.Hover.GetState(0) == PointStateType.Motion)
            {
                if (HasBody() == true && HasFocus() == false)
                {
                    //FluxLogger.DebugP("Hover Set Focus : %s1", s1: this?.GetTypeName());
                    //FocusManager.Instance.SetCurrentFocusView(this);
                }
            }
            return true;
        }

        private void BindEnableAction()
        {
            if (inputEnabler == null)
            {
                throw new InvalidOperationException("inputEnabler is null.");
            }
            if (UIConfig.SupportPointingMode == UIConfig.PointingMode.SupportNone)
            {
                FluxLogger.DebugP("BindEnableAction none");

                inputEnabler.RegisterKey(Constant.Touch);
                inputEnabler.RegisterKey(Constant.Tap);
                inputEnabler.RegisterKey(Constant.TouchLongPress);
                inputEnabler.RegisterKey(Constant.Hover);
            }
            else
            {
                inputEnabler.RegisterKey(Constant.Touch, EnableTouch, DisableTouch);
                inputEnabler.RegisterKey(Constant.Tap, EnableTap, DisableTap);
                inputEnabler.RegisterKey(Constant.TouchLongPress, EnableTouchLongPress, DisableTouchLongPress);
                inputEnabler.RegisterKey(Constant.Hover, EnableHover, DisableHover);
            }
        }

        private void EnableTap()
        {
            GestureDetector.Instance.TapGestureDetector?.Attach(this);
        }
        private void DisableTap()
        {
            GestureDetector.Instance.TapGestureDetector?.Detach(this);
        }

        private void EnableTouchLongPress()
        {
            GestureDetector.Instance.LongPressGestureDetector?.Attach(this);
        }

        private void DisableTouchLongPress()
        {
            GestureDetector.Instance.LongPressGestureDetector?.Detach(this);
        }


        private void EnableTouch()
        {
            FluxLogger.InfoP("[%s1].[%s2] is called ", s1: GetTypeName(), s2: Name);

            //If GrabTouchAfterLeave is true, then LeaveRequired is not working.
            //LeaveRequired = true;
#if Support_FLUXCore_GrabTouchAfterLeave
            GrabTouchAfterLeave = true;
#endif
            TouchEvent += OnTouched;
        }
        private void DisableTouch()
        {
            FluxLogger.InfoP("[%s1].[%s2] is called ", s1: GetTypeName(), s2: Name);

            //LeaveRequired = false;
#if Support_FLUXCore_GrabTouchAfterLeave
            GrabTouchAfterLeave = false;
#endif
            TouchEvent -= OnTouched;
        }

        private void EnableHover()
        {
            FluxLogger.InfoP("[%s1].[%s2] is called ", s1: GetTypeName(), s2: Name);

            HoverEvent += OnHovered;

        }
        private void DisableHover()
        {
            FluxLogger.InfoP("[%s1].[%s2] is called ", s1: GetTypeName(), s2: Name);

            HoverEvent -= OnHovered;
        }
        internal void SendExecuteEvent()
        {
            if (executeEventHandler != null)
            {
                executeEventHandler.Invoke(this, ExecuteEventArgs.Empty);
            }
            else
            {
                if (SendKeyInsteadExecute == false)
                {
                    return;
                }
                Key key = new Key
                {
                    KeyPressedName = "Return"
                };
                key.State = Key.StateType.Down;
                Window.Instance.FeedKey(key);

                key.State = Key.StateType.Up;
                Window.Instance.FeedKey(key);
            }
        }

        private void SendLongPressExecuteEvent()
        {
            longPressExecuteEventHandler?.Invoke(this, LongPressExecuteEventArgs.Empty);
        }


        private bool ControlKeyEvent(object source, KeyEventArgs e)
        {
            return OnKey(e.Key);
        }

        private void ControlFocusGained(object sender, EventArgs e)
        {
            Focused = true;
            OnFocusGained();
        }

        private void ControlFocusLost(object sender, EventArgs e)
        {
            if (HasBody() == false)
            {
                FluxLogger.ErrorP("this view is already Dispose");
                return;
            }

            if (Touched == true)
            {
                Touched = false;
            }

            Focused = false;
            OnFocusLost();
        }

        #endregion

        #region MOTION internal private methods

        internal virtual FluxAnimationPlayer GetAppearAnimator()
        {
            return new FluxAnimationPlayer(MotionTypes.Undefined);
        }
        internal virtual FluxAnimationPlayer GetDisappearAnimator()
        {
            return new FluxAnimationPlayer(MotionTypes.Undefined);
        }

        internal virtual void PlayAppearAnimation()
        {
            if (appearAnimationPlayer != null)
            {
                Opacity = appearOpacity;
                Show();
                appearAnimationPlayer.AnimateTo(this, MotionTypes.Appear_01);
                appearAnimationPlayer.Play();
            }
        }

        internal virtual void PlayDisappearAnimation()
        {
            if (disappearAnimationPlayer != null)
            {
                disappearAnimationPlayer.Finished -= DisappearAnimationFinished;
                disappearAnimationPlayer.Finished += DisappearAnimationFinished;
                disappearAnimationPlayer.AnimateTo(this, MotionTypes.Disappear_01);
                disappearAnimationPlayer.AnimateTo(this, MotionTypes.FadeOut_02);
                disappearAnimationPlayer.Play();
            }
        }

        internal void PlayPressFeedbackAnimation()
        {
            if (IsFocused != true)
            {
                return;
            }

            if (pressFeedbackAnimationPlayer == null)
            {
                pressFeedbackAnimationPlayer = new FluxAnimationPlayer(MotionTypes.PressFeedback_S);
            }

            pressFeedbackAnimationPlayer.Play(this);
        }

        private void DisappearAnimationFinished(object sender, EventArgs e)
        {
            disappearAnimationPlayer.Finished -= DisappearAnimationFinished;
            Hide();
        }

        internal virtual void ApplyAsFastScrollItem(bool enable)
        {

        }

        internal bool IsTouchLeaved(TouchEventArgs e)
        {
            if (e.Touch.GetMouseButton(0) != MouseButton.Primary)
            {
                return false;
            }

            Vector2 localPosition = e.Touch.GetLocalPosition(0);
            if ((localPosition != null && Size != null) &&
                (0 <= localPosition.X && localPosition.X <= Size.Width &&
                 0 <= localPosition.Y && localPosition.Y <= Size.Height) == false)
            {
                FluxLogger.InfoP("Leaved : localPosition:[%s1],[%s2]", s1: localPosition.X.ToString(), s2: localPosition.Y.ToString());
                return true;
            }

            return false;
        }


        #endregion

        #region STATE internal private methods



        internal override void PropagateState(string toState, FluxAnimationPlayer animator = null, bool bForceUpdate = false)
        {
            if (stateMachine == null)
            {
                FluxLogger.FatalP("StateMachine is null");
                return;
            }

            FluxLogger.DebugP("PropagateState is [%s1], Enable [%d1], [%s2] -> [%s3]",
                s1: Name,
                d1: (EnablePropagateState) ? 1 : 0,
                s2: stateMachine.CurrentState,
                s3: toState);

            if (bForceUpdate == true || stateMachine.CurrentState != toState)
            {
                stateMachine.CurrentState = toState;
                ApplyStateProperty(toState, animator);
                ApplyComponentStateSpec(stateMachine.PresetState);

                if (string.IsNullOrEmpty(stateMachine.PreviousState) == false)
                {
                    stateMachine.PostStateAction?.Invoke(stateMachine.PreviousState, stateMachine.CurrentState);
                }

                if (EnablePropagateState)
                {
                    if (elements != null)
                    {
                        foreach (ComponentBase element in elements.Values)
                        {
                            if (element == null)
                            {
                                continue;
                            }

                            //In case that element is componentbase, componentbase's default value is false. so return without propagation.
                            //But if user attach something in componentbase, then state will be propagated.
                            element.PropagateState(toState, animator, bForceUpdate);
                        }
                    }

                    // propagation for attached view
                    foreach (View element in Children)
                    {
                        if (element is ComponentBase statable)
                        {
                            statable.PropagateState(toState, animator, bForceUpdate);
                        }
                    }
                }
            }
        }

        internal virtual void ApplyComponentStateSpec(string currentState)
        {
            themeColor?.UpdateState();
        }


        internal Component FocusedControl
        {
            get => focusedControl;
            set
            {
                if (focusedControl == value || value == this)
                {
                    return;
                }
                focusedControl?.KillFocus();
                focusedControl = value;
                focusedControl?.SetFocus();
            }
        }

        private void DoDisableState()
        {
            if (stateMachine != null)
            {
                stateMachine.UpdateState(StateUtility.Disabled, bDisabled);
                PropagateState(stateMachine.CurrentState, null, true);
            }
        }

        private void DoSelectState()
        {
            if (stateMachine != null)
            {
                FluxAnimationPlayer animator = stateMachine.UpdateState(StateUtility.Selected, bSelected);

                FluxLogger.DebugP("Component Name [%s1] , motionType [%s2]",
                    s1: Name, s2: FluxLogger.EnumToString(animator?.MotionType));

                if (DefaultSelectMotionEnabled == false)
                {
                    animator = null;
                }

                PrepareStateMotion(animator);
                PropagateState(stateMachine.CurrentState, animator, true);
                PlayStateMotion(animator);
            }
        }

        private void DoFocusState()
        {
            if (stateMachine != null)
            {
                FluxAnimationPlayer animator = stateMachine.UpdateState(StateUtility.Focused, bFocused);

                FluxLogger.DebugP("Component Name [%s1] Focused!! [%d1], previous State [%s2] state [%s3] motionType [%s4]"
                    , s1: Name
                    , d1: bFocused ? 1 : 0
                    , s2: stateMachine?.PreviousState
                    , s3: stateMachine?.CurrentState
                    , s4: FluxLogger.EnumToString(animator?.MotionType));

                PrepareStateMotion(animator);

                //      FluxLogger.FatalP("%s1[%d1] bFocused:%d2 LongPressed:  %d3 LongPressedForScrollBase: %d4 CurrentState:%s2", s1: this?.GetTypeName(), d1: ID, d2: Convert.ToInt32(bFocused), d3: Convert.ToInt32(bLongPressed), d4: Convert.ToInt32(RepeatKeyManager.Instance.LongPressedForScrollBase), s2: stateMachine.CurrentState);
                if (bLongPressed == false)
                {
                    if (DefaultFocusMotionEnabled == false)
                    {
                        animator = null;
                        stateMachine?.stateController.StopPlayingMotion();
                    }
                    else if (IsItemForFastScroll == true && RepeatKeyManager.Instance.LongPressedForScrollBase == true)
                    {
                        animator = null;
                        stateMachine?.stateController.StopPlayingMotion();
                        MakeSureScaleRestored();
                    }
                    PropagateState(stateMachine.CurrentState, animator, true);
                }
                else
                {
                    animator?.Clear();
                    PropagateState(stateMachine.CurrentState, null, true);
                    if (LongPressMotionType == LongPressMotionTypes.Type01)
                    {
                        if (bFocused)
                        {
                            animator?.AnimateTo(this, MotionTypes.LongPress_Focused);
                        }
                        else
                        {
                            animator?.AnimateTo(this, MotionTypes.LongPress_Normal);
                        }
                    }

                    MakeSureScaleRestored();
                }
                PlayStateMotion(animator);
            }
        }
        private void MakeSureScaleRestored()
        {
            //Todo : We need to check why eden's launcher tile is changed scale property instead of animation.
            {
                StateProperty prop = stateMachine.GetStateProperty(StateUtility.Normal);

                if (prop?.animatable != null && prop.animatable.TryGetValue("Scale", out object scale))
                {
                    if (scale is Vector3 scaleValue)
                    {
                        Scale = scaleValue;
                    }
                }
                else
                {
                    Scale = MotionSpec.NomalScaleValue;
                }
            }
        }

        private void DoLongPressState()
        {
            if (stateMachine == null)
            {
                return;
            }

            //FluxLogger.FatalP("{%s1}:%s2 LongPressed!! : %d1 : Focused:%d2", s1: this?.GetTypeName(), s2: Name, d1: Convert.ToInt32(bLongPressed), d2: Convert.ToInt32(Focused));

            FluxAnimationPlayer animator = null;

            if (bLongPressed == true)
            {
                if (Focused == true)
                {
                    animator = stateMachine.stateController.GetStateAnimator(MotionTypes.FocusOut_01);
                    PrepareStateMotion(animator);

                    StateProperty prop = stateMachine.GetStateProperty(StateUtility.Normal);
                    if (prop?.animatable != null && prop.animatable.TryGetValue("Scale", out object scale) && scale is Vector3 scaleValue)
                    {
                        //FluxLogger.FatalP("  -> %s1 Scale %f1, %f2", s1: this?.GetTypeName(), f1: scaleValue.X, f2: scaleValue.Y);
                        animator?.AnimateTo(this, "Scale", scaleValue);
                    }
                }
                else
                {
                    if (LongPressMotionType == LongPressMotionTypes.Type01)
                    {
                        if (FeatureManager.IsSupportFluxAnimation == false)
                        {
                            if (MotionSpec.specTable.TryGetValue(MotionTypes.LongPress_Normal, out MotionSpec spec))
                            {
                                SetProperty(spec.Property, spec.To);
                                return;
                            }
                        }
                        animator = stateMachine.stateController.GetStateAnimator(MotionTypes.LongPress_Normal);
                        PrepareStateMotion(animator);
                    }
                }
            }
            else
            {
                if (Focused == true)
                {
                    animator = stateMachine.stateController.GetStateAnimator(MotionTypes.FocusIn_01);
                    if (DefaultFocusMotionEnabled == false)
                    {
                        animator?.Reset(FluxAnimationPlayer.EndActions.StopFinal);
                    }
                    else
                    {
                        PrepareStateMotion(animator);
                    }

                    StateProperty prop = stateMachine.GetCurrentStateProperty();
                    if (prop?.animatable != null && prop.animatable.TryGetValue("Scale", out object scale) && scale is Vector3 scaleValue)
                    {
                        //FluxLogger.FatalP("  -> %s1 Scale %f1, %f2", s1: this?.GetTypeName(), f1: scaleValue.X, f2: scaleValue.Y);
                        animator?.AnimateTo(this, "Scale", scaleValue);
                    }
                }
                else
                {
                    if (FeatureManager.IsSupportFluxAnimation == false)
                    {
                        if (MotionSpec.specTable.TryGetValue(MotionTypes.LongPress_End, out MotionSpec spec))
                        {
                            SetProperty(spec.Property, spec.To);
                            return;
                        }
                    }
                    animator = stateMachine.stateController.GetStateAnimator(MotionTypes.LongPress_End);
                    PrepareStateMotion(animator);
                }
            }

            PlayStateMotion(animator);

            if (LongPressed == true)
            {
                PropagateLongPressStateInternaly(LongPressed);
            }
        }

        private void PropagateLongPressStateInternaly(bool longPressed)
        {
            StateProperty stateProperty = State.GetStateProperty(StateUtility.LongPressed);
            stateProperty?.stateAction?.Invoke(StateUtility.LongPressed, longPressed.ToString());
            foreach (View child in Children)
            {
                if (child is Component component)
                {
                    component.PropagateLongPressStateInternaly(longPressed);
                }
            }
        }

        private bool PropagationNeeded(string currentState, string stateName)
        {
            if (stateName == currentState)
            {
                return true;
            }

            if (stateName == StateUtility.StateAll)
            {
                return true;
            }

            if (stateName == StateUtility.Basic && stateMachine.stateController.IsBasicState(currentState) == true)
            {
                return true;
            }

            if (stateName == StateUtility.Focused || stateName == StateUtility.Checked || stateName == StateUtility.Pressed)
            {
                if (currentState.Contains(stateName) == true)
                {
                    return true;
                }
            }

            return false;
        }

        internal virtual void ApplyStateProperty(string changeState, FluxAnimationPlayer animator = null)
        {
            if (stateMachine == null)
            {
                FluxLogger.FatalP("State is null");
                return;
            }

            StateProperty prop = stateMachine.GetCurrentStateProperty();

            if (prop == null)
            {
                FluxLogger.FatalP("Property of state [%s1] is null", s1: changeState);
                return;
            }
            else
            {
                #region For Debug
                if (localDebug)
                {
                    foreach (KeyValuePair<string, object> items in prop.animatable)
                    {
                        if (items.Key == "Scale")
                        {
                            if (items.Value is Vector3 value)
                            {
                                FluxLogger.DebugP("StateName [%s1] , Animatable Property [%s2] ScaleValue [%f1]"
                               , s1: prop.StateName
                               , s2: items.Key
                               , f1: value.X
                               );
                            }

                        }
                        else
                        {
                            FluxLogger.DebugP("StateName [%s1] , Animatable Property [%s2] ScaleValue [%s3]"
                            , s1: prop.StateName
                            , s2: items.Key
                            , s3: items.Value.ToString()
                            );
                        }
                    }
                    foreach (KeyValuePair<string, object> items in prop.unanimatable)
                    {

                        FluxLogger.DebugP("Current State[%s1] , StateName [%s2] , Unanimatable Property [%s3] Value [%s4]"
                            , s1: stateMachine.stateController.CurrentState
                            , s2: prop.StateName
                            , s3: items.Key
                            , s4: items.Value.ToString()
                            );
                    }
                }
                #endregion
            }

            if (prop.animatable.Count != 0)
            {
                if (animator != null)
                {
                    foreach (KeyValuePair<string, object> item in prop.animatable)
                    {
                        if (LongPressed && item.Key.Equals("Scale"))
                        {
                            continue;
                        }
                        if (item.Value != null)
                        {
                            animator.AnimateTo(this, item.Key, item.Value);
                        }
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, object> item in prop.animatable)
                    {
                        if ((LongPressed == true || (IsItemForFastScroll == true && RepeatKeyManager.Instance.LongPressedForScrollBase == true)) && item.Key.Equals("Scale"))
                        {
                            //FluxLogger.FatalP("%s1[%d1] bFocused:%d2 LongPressed:  %d3 LongPressedForScrollBase: %d4 CurrentState:%s2", s1: this?.GetTypeName(), d1: ID, d2: Convert.ToInt32(bFocused), d3: Convert.ToInt32(bLongPressed), d4: Convert.ToInt32(RepeatKeyManager.Instance.LongPressedForScrollBase), s2: stateMachine.CurrentState);
                            prop.animatable.Remove("Scale");
                            break;
                        }
                    }
                    SetPropertyBoost(prop.animatable);
                }
            }

            if (prop.unanimatable.Count != 0)
            {
                SetProperty(prop.unanimatable);
            }

            FluxLogger.DebugP("ApplyStateProperty done animtable [%d1] unanimatable [%d2] ", d1: prop.animatable.Count, d2: prop.unanimatable.Count);
        }

        internal virtual void PrepareStateMotion(FluxAnimationPlayer animator)
        {
            if (animator != null)
            {
                animator.Reset(FluxAnimationPlayer.EndActions.StopFinal);

                StateProperty prop = stateMachine.GetCurrentStateProperty();
                animator.AnimateTo(this, prop);
            }
        }
        private void PlayStateMotion(FluxAnimationPlayer animator)
        {
            if (animator != null)
            {
                stateMachine?.stateController.StopPlayingMotion();

                if (animator.MotionType == MotionTypes.FocusIn_01)
                {
                    if (AudioFeedbackEnabled == true)
                    {
                        AudioFeedback.Instance.Play(AudioFeedback.Pattern.MoveNavigation);
                    }
                }
                animator.Finished -= onStateChangeAnimationFinished;
                animator.Finished += onStateChangeAnimationFinished;
                animator?.Play();
            }
            else
            {
                onStateChangeAnimationFinished(this, null);
            }
        }

        internal void StopPlayingMotion()
        {
            stateMachine?.stateController.StopPlayingMotion();
        }


        internal virtual void onStateChangeAnimationFinished(object sender, EventArgs e)
        {
            if (sender is FluxAnimation animator)
            {
                animator.Finished -= onStateChangeAnimationFinished;
            }
        }


        #endregion

        #region INIT/DISPOSE internal private methods

        internal virtual void InitializeComponent(PresetBase preset)
        {

        }

        private void Initialize(string presetName = null)
        {
            EnablePropagateState = true;
            //register event handler for key event
            KeyEvent += ControlKeyEvent;

            //register event handler for focus
            FocusGained += ControlFocusGained;
            FocusLost += ControlFocusLost;

            BindPresetProperty(presetName);

            IsComponent = true;
        }



        private void BindPresetProperty(string presetName)
        {
            if (presetName == null)
            {
                return;
            }

            PresetBase preset = PresetManager.GetPreset(presetName);
            if (preset == null)
            {
                return;
            }

            this.preset = preset;

            elementName = preset.ElementName;
            elements = preset.elements;
            State = preset.ComponentState;

            InitializeComponent(preset);

            preset.SetComponentProperty(this);

            CreateTree();
            PropagateState(StateUtility.Normal, bForceUpdate: true);
        }


        private void DoDispose()
        {
            KeyEvent -= ControlKeyEvent;
            FocusGained -= ControlFocusGained;
            FocusLost -= ControlFocusLost;

            inputEnabler?.Clear();
            inputEnabler = null;

            themeColor?.Clear();
            themeColor = null;

            DestroyUtility.DestroyFluxAnimationPlayer(ref pressFeedbackAnimationPlayer);
            DestroyUtility.DestroyFluxAnimationPlayer(ref appearAnimationPlayer);
            if (disappearAnimationPlayer != null)
            {
                disappearAnimationPlayer.Finished -= DisappearAnimationFinished;
                DestroyUtility.DestroyFluxAnimationPlayer(ref disappearAnimationPlayer);
            }

            focusedControl = null;

            DestroyTree();
            preset?.Destroy();


            stateMachine?.Clear();
            stateMachine = null;

            elements?.Clear();
            elements = null;


            elementName?.Clear();
            elementName = null;

            ThemeHelper.Instance.ClearColorChip(this);
        }

        #endregion

        #region internal field
        internal Dictionary<string, string> elementName = new Dictionary<string, string>();
        internal Dictionary<string, ComponentBase> elements = new Dictionary<string, ComponentBase>();

        internal string presetName = null;
        internal FluxAnimationPlayer pressFeedbackAnimationPlayer;
        internal FluxAnimationPlayer appearAnimationPlayer;
        internal FluxAnimationPlayer disappearAnimationPlayer;

        internal PresetBase preset;
        internal bool bLongPressed = false;

        // TODO: Remove circular dependency
        //internal static UIPlate commonUIplate;
        //internal bool commonUIplateAdded = false;

        #endregion

        #region private field

        private Component focusedControl = null;
        private EventHandler focusGainedEventHandler;
        private EventHandler focusLostEventHandler;
        private EventHandler<ExecuteEventArgs> executeEventHandler;
        private EventHandler<LongPressExecuteEventArgs> longPressExecuteEventHandler;


        private StateMachine stateMachine = new StateMachine();
        private bool bDisabled = false;
        private bool bFocused = false;
        private bool bSelected = false;
        private bool bChecked = false;
        private bool bTouched = false;
        private readonly bool localDebug = false;

        private bool isItemForFastScroll = false;

        private readonly float appearOpacity = Constant.OPACITY_ORIGINAL;
        private Enabler<string> inputEnabler;
        private ThemeColor themeColor;

        private PointingBehaviorMode pointingBehavior = PointingBehaviorMode.PressIsFocus;

        private StatePropertyDefinitionCollection statePropertyDefinitions;



        #endregion

        #region need to deprecated

        /// <summary>
        /// Set ColorPreset in backgroundColor of Component. When you set this value,it is changed automatically according to state. 
        /// The string starts "CP_".
        /// <example>
        /// component.ThemeBackgroundColorPreset = "CP_Info1100";
        /// </example>
        /// </summary>       
        /// <deprecated> Deprecated since 9.9.0.  Please use  ThemeColor[**].ColorChip or ColorPreset</deprecated>
        [Obsolete("@Deprecated ThemeBackgroundColorPreset is deprecated. Please use ThemeColor[**].ColorChip or ColorPreset")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ThemeBackgroundColorPreset
        {
            get => ThemeColor[Component.PlaneThemeColor].ColorPreset;
            set => ThemeColor[Component.PlaneThemeColor].ColorPreset = value;
        }

        /// <summary>
        /// Set ColorChip in backgroundColor of Component. It is only set Color not changing color according to state.
        /// The string starts "CC_"
        /// </summary>
        /// <example>
        /// component.ThemeBackgroundColorChip = "CC_Basic1100";
        /// </example>
        /// <deprecated> Deprecated since 9.9.0.  Please use  ThemeColor[**].ColorChip or ColorPreset</deprecated>
        [Obsolete("@Deprecated ThemeBackgroundColorChip is deprecated. Please use ThemeColor[**].ColorChip or ColorPreset")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ThemeBackgroundColorChip
        {
            get => ThemeColor[Component.PlaneThemeColor].ColorChip;
            set => ThemeColor[Component.PlaneThemeColor].ColorChip = value;
        }

        #endregion

    }
}
