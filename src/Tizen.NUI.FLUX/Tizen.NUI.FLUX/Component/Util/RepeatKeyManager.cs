/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file RepeatKeyManager.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 8.8.0 </version>
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
/// 
using System;
using Tizen.NUI;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// This is RepeatKeyManager class.
    /// You can get the event of repeat key start / end 
    /// </summary>
    /// <code>
    /// RepeatKeyManager.Instance.RepeatKeyEvent += RepeatKeyEvent;
    /// </code>
    public class RepeatKeyManager
    {
        /// <summary>
        /// the event type of repeat key state
        /// </summary>
        public enum EventType
        {
            /// <summary>
            /// LongPress start.
            /// </summary>
            LongPressStart = 0,
            /// <summary>
            /// LongPress end.
            /// </summary>
            LongPressEnd
        }

        /// <summary>
        /// Repeat key event arguments
        /// </summary>
        public class RepeatKeyEventArgs : EventArgs
        {
            /// <summary>
            /// the event type of repeat key state.
            /// </summary>
            public EventType EventType
            {
                get;
                set;
            }

            internal bool IsWheelEvent
            {
                get;
                set;
            } = false;
        }

        private RepeatKeyManager()
        {
            timer = new Timer(wheelRepeatKeyFinishTime);
            timer.Tick += OnRepeatWheelReleased;
        }

        internal void Initialize()
        {
            SetKeyRepeatInterval();
            Window.Instance.FocusChanged += OnWindowFocusChanged;
        }

        internal void CleanUp()
        {
            UnSetKeyRepeatInterval();
            Window.Instance.FocusChanged -= OnWindowFocusChanged;
            timer.Tick -= OnRepeatWheelReleased;
        }

        /// <summary>
        /// RepeatKeyManager instance (read-only)
        /// </summary>
        public static RepeatKeyManager Instance { get; } = new RepeatKeyManager();

        /// <summary>
        /// LongPress event args at event call back, user can get information
        /// </summary>
        /// <code>
        /// private void RepeatKeyEvent(object sender, RepeatKeyManager.RepeatKeyEventArgs e)
        /// {
        ///     if (e.EventType == RepeatKeyManager.RepeatKeyEventArgs.LongPressStart)
        ///     {
        ///     }
        /// }
        /// </code>
        public event EventHandler<RepeatKeyEventArgs> RepeatKeyEvent
        {
            add
            {
                repeatKeyEventHandler += value;
            }
            remove
            {
                repeatKeyEventHandler -= value;
            }
        }

        /// <summary>
        /// Retrieve if current is longpress state
        /// </summary>
        public bool LongPressed
        {
            get;
            private set;
        } = false;


        /// <summary>
        /// KeyPressedName when long pressed start.
        /// </summary>
        public string KeyPressedName
        {
            get;
            private set;
        }

        /// <summary>
        /// this is the delay time to change repeat key's start time (ms)
        /// Default is 350ms. This is defined by UX Principle D-Pad Interaction.
        /// When accessibility's slow focus is on, then this value will be ignored.
        /// </summary>
        /// <exception cref="ArgumentException"> Delay value should be larger than 0 </exception> 
        /// <version> 9.9.1 </version>
        public int Delay
        {
            get => (int)(delay * secToMs);
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Delay value should be larger than 0, you set {value}");
                }
                if (Delay == value)
                {
                    return;
                }

                delay = value * msToSec;
                UpdateKeyRepeatInterval(IntPtr.Zero, IntPtr.Zero);
            }
        }

        /// <summary>
        /// this is the interval time to generate repeat key (ms)
        /// Default is 70ms. This is defined by UX Principle D-Pad Interaction.
        /// When accessibility's slow focus is on, then this value will be ignored.
        /// </summary>
        /// <exception cref="ArgumentException"> Interval value should be larger than 0 </exception> 
        /// <version> 9.9.1 </version>
        public int Interval
        {
            get => (int)(interval * secToMs);
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Interval value should be larger than 0, you set {value}");
                }
                if (Interval == value)
                {
                    return;
                }

                interval = value * msToSec;
                UpdateKeyRepeatInterval(IntPtr.Zero, IntPtr.Zero);
            }
        }

        internal bool LongPressedForScrollBase
        {
            get;
            private set;
        } = false;

        internal void KeyPressed(Key key)
        {
            if (key.IsWheelEvent() == false)
            {
                KeyPressed(key.KeyPressedName);
            }
            else
            {
                WheelPressed(key);
            }
        }

        internal void KeyReleased(Key key)
        {
            if (key.IsWheelEvent() == false)
            {
                KeyReleased(key.KeyPressedName);
            }
        }

        private void WheelPressed(Key key)
        {
            //1st interval
            keyPressCount = 0;
            wheelKey1stHistory[0] = wheelKey1stHistory[1];
            wheelKey1stHistory[1] = wheelKey1stHistory[2];
            wheelKey1stHistory[2] = wheelKey1stHistory[3];
            wheelKey1stHistory[3] = key.Time;

            //2nd interval
            wheelKey2ndHistory[0] = wheelKey2ndHistory[1];
            wheelKey2ndHistory[1] = wheelKey2ndHistory[2];
            wheelKey2ndHistory[2] = wheelKey2ndHistory[3];
            wheelKey2ndHistory[3] = wheelKey2ndHistory[4];
            wheelKey2ndHistory[4] = key.Time;
            uint interval1 = wheelKey1stHistory[3] - wheelKey1stHistory[0];
            uint interval2 = wheelKey2ndHistory[4] - wheelKey2ndHistory[0];

            bool isFastRotation1 = false;
            bool isFastRotation2 = false;

            if (KeyPressedName?.Equals(KeyPressedName) == false)
            {
                WheelRelease();
                return;
            }

            if (interval1 < wheelRepeatKey1stInterval)
            {
                isFastRotation1 = true;
            }
            if (interval2 < wheelRepeatKey2ndInterval)
            {
                isFastRotation2 = true;
            }

            FluxLogger.ErrorP("%s1 --> interval:%d1, %d2, isfast :%d1, %d2", s1: key.KeyPressedName, d1: interval1, d2: interval2, d3: Convert.ToInt32(isFastRotation1), d4: Convert.ToInt32(isFastRotation2));
            if (isFastRotation1 || isFastRotation2)
            {
                KeyPressedName = key.KeyPressedName;

                if (LongPressed == false)
                {
                    LongPressed = true;
                    RepeatKeyEventArgs args = new RepeatKeyEventArgs()
                    {
                        EventType = EventType.LongPressStart,
                        IsWheelEvent = key.IsWheelEvent()
                    };
                    LongPressedForScrollBase = true;
                    repeatKeyEventHandler?.Invoke(this, args);
                    FluxLogger.ErrorP("Long Press motion start");
                    timer.Start();
                    return;
                }

                timer.Stop();
                timer.Start();

                return;
            }

            WheelRelease();
        }

        private void WheelRelease()
        {
            LongPressedForScrollBase = false;

            if (LongPressed == true)
            {
                LongPressed = false;
                RepeatKeyEventArgs args = new RepeatKeyEventArgs()
                {
                    EventType = EventType.LongPressEnd
                };
                repeatKeyEventHandler?.Invoke(this, args);
                FluxLogger.ErrorP("Long Press motion finish");
            }
            KeyPressedName = null;
            keyPressCount = 0;
        }

        private bool OnRepeatWheelReleased(object source, Timer.TickEventArgs e)
        {
            WheelRelease();
            FluxLogger.ErrorP(" Finish repeated wheel event by timer");
            return false;
        }

        private void KeyPressed(string keyPressedName)
        {
            keyPressCount++;
            KeyPressedName = keyPressedName;

            if (keyPressCount == 2)
            {
                LongPressed = true;
                RepeatKeyEventArgs args = new RepeatKeyEventArgs()
                {
                    EventType = EventType.LongPressStart
                };
                repeatKeyEventHandler?.Invoke(this, args);
            }

            if (keyPressCount == 3)
            {
                LongPressedForScrollBase = true;
            }
        }

        private void KeyReleased(string keyPressedName)
        {
            if (LongPressed == true)
            {
                LongPressed = false;
                LongPressedForScrollBase = false;
                RepeatKeyEventArgs args = new RepeatKeyEventArgs()
                {
                    EventType = EventType.LongPressEnd
                };
                repeatKeyEventHandler?.Invoke(this, args);
            }

            KeyPressedName = null;
            keyPressCount = 0;
        }

        private void OnWindowFocusChanged(object sender, Window.FocusChangedEventArgs e)
        {
            if (e.FocusGained == false)
            {
                KeyReleased("");
            }
        }

        // TODO: Temporary remove Vconf dependency
        //private Vconf.VconfCallBack slowKeyRepeatCallback;

        private void SetKeyRepeatInterval()
        {
            // TODO: Temporary remove Vconf dependency
            //slowKeyRepeatCallback = SlowKeyRepeatChanged;

            //Vconf.NotifyKeyChanged("db/menu/system/accessibility/slow_key_repeat", slowKeyRepeatCallback, IntPtr.Zero);
            //Vconf.NotifyKeyChanged("db/menu/system/accessibility/slow_key_repeat/delay_interval", slowKeyRepeatCallback, IntPtr.Zero);

            UpdateKeyRepeatInterval(IntPtr.Zero, IntPtr.Zero);
        }

        private void UnSetKeyRepeatInterval()
        {
            // TODO: Temporary remove Vconf dependency
            //Vconf.IgnoreKeyChanged("db/menu/system/accessibility/slow_key_repeat", slowKeyRepeatCallback);
            //Vconf.IgnoreKeyChanged("db/menu/system/accessibility/slow_key_repeat/delay_interval", slowKeyRepeatCallback);

            //slowKeyRepeatCallback = null;
        }

        private void SlowKeyRepeatChanged(IntPtr node, IntPtr userData)
        {
#if Support_FLUXCore_Separated_UIThread
            if (FluxApplication.UIThreadSeparated == false)
            {
                UpdateKeyRepeatInterval(node, userData);
            }
            else
            {
                FluxSynchronizationContext.ToUIThread.Post(UIThreadSlowKeyRepeatChanged, this);
            }
#else
            UpdateKeyRepeatInterval(node, userData);
#endif
        }

        private void UpdateKeyRepeatInterval(IntPtr node, IntPtr userData)
        {
            // TODO: Temporary remove Vconf dependency
            //Vconf.GetBool("db/menu/system/accessibility/slow_key_repeat", out bool slowKeyRepeatEnabled);

            //if (slowKeyRepeatEnabled == false)
            //{
            //    Window.Instance.SetKeyboardRepeatInfo(interval, delay);
            //    FluxLogger.InfoP("update KeyboardRepeatInfo : rate : %f1, delay :%f2", f1: interval, f2: delay);
            //}
            //else
            //{
            //    Vconf.GetDouble("db/menu/system/accessibility/slow_key_repeat/delay_interval", out double delay_interval);
            //    Window.Instance.SetKeyboardRepeatInfo((float)delay_interval, (float)delay_interval);

            //    FluxLogger.InfoP("update KeyboardRepeatInfo : rate : %f1, delay :%f1, slowKeyRepeatEnabled : %d1", f1: delay_interval, d1: Convert.ToInt32(slowKeyRepeatEnabled));
            //}
        }

        private void UIThreadSlowKeyRepeatChanged(object obj)
        {
            if (obj is RepeatKeyManager repeatKeyManager)
            {
                UpdateKeyRepeatInterval(IntPtr.Zero, IntPtr.Zero);
            }
        }

        private readonly float secToMs = 1000.0f;
        private readonly float msToSec = 0.001f;
        private float delay = Constant.REPEAT_KEY_DELAY;
        private float interval = Constant.REPEAT_KEY_INTERVAL;
        private int keyPressCount = 0;
        private EventHandler<RepeatKeyEventArgs> repeatKeyEventHandler;

        private readonly uint wheelRepeatKey1stInterval = 170;
        private readonly uint wheelRepeatKey2ndInterval = 265;
        private readonly uint wheelRepeatKeyFinishTime = 250;
        private readonly uint[] wheelKey1stHistory = { 0, 0, 0, 0, };
        private readonly uint[] wheelKey2ndHistory = { 0, 0, 0, 0, 0 };
        private readonly Timer timer;
    }
}

