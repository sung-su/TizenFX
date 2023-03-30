/// @file PhysicsAnimation.cs
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
using System.Linq;
using System.Threading;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// This is PhysicsAnimation which enables user to animate by physics engine.
    /// </summary>
    /// <code>
    /// PhysicsAnimation animation = new PhysicsAnimation();
    /// animation.AnimateTo(view2, "PositionX", 100.0f, PhysicsAnimation.BuiltinFunctions.SnapAppearP);
    /// animation.AnimateTo(view2, "PositionY", 100.0f, PhysicsAnimation.BuiltinFunctions.SnapAppearP);
    /// animation.Play();
    /// </code>
    public class PhysicsAnimation : NUIDisposable
    {
        internal class TimeStamp
        {
            public TimeStamp(int start, int end)
            {
                StartTime = start;
                EndTime = end;
            }

            public int StartTime
            {
                private set;
                get;
            }

            public int EndTime
            {
                private set;
                get;
            }
        }

        /// <summary>
        /// Enumeration for States of PhysicsAnimation
        /// </summary>
        public enum States
        {
            /// <summary>
            /// PhysicsAnimation is in stopped state.
            /// </summary>
            Stopped,
            /// <summary>
            /// PhysicsAnimation is in playing state.
            /// </summary>
            Playing,
            /// <summary>
            /// PhysicsAnimation is in paused state.
            /// </summary>
            Paused
        }
        /// <summary>
        /// Enumeration for where to jump after stopping of PhysicsAnimation
        /// </summary>
        public enum EndActions
        {
            /// <summary>
            /// Target of PhysicsAnimation will be in place when Stop() is called.
            /// </summary>
            Cancel,
            /// <summary>
            /// Target of PhysicsAnimation will be located in the place where it started when Stop() is called.
            /// </summary>
            Discard,
            /// <summary>
            /// Target of PhysicsAnimation will be located in the place where it would finish when Stop() is called.
            /// </summary>
            StopFinal
        }
        /// <summary>
        /// Enumeration for built-in interpolation functions of PhysicsAnimation
        /// </summary>
        public enum BuiltinFunctions
        {
            /// <summary> Built-in motion Snap </summary>
            Snap = 0,
            /// <summary> Built-in motion Bounce </summary>
            Bounce,
            /// <summary> Built-in motion CurveElastic </summary>
            CurveElastic,
            /// <summary> Built-in motion CurveRepeat </summary>
            CurveRepeat,
            /// <summary> Built-in motion Curve </summary>
            Curve,
            /// <summary> Built-in motion SnapFocusInP </summary>
            SnapFocusInP,
            /// <summary> Built-in motion SnapFocusOutP </summary>
            SnapFocusOutP,
            /// <summary> Built-in motion SnapFocusOutS </summary>
            SnapFocusOutS,
            /// <summary> Built-in motion SnapElasticS </summary>
            SnapElasticS,
            /// <summary> Built-in motion BounceBackL </summary>
            BounceBackL,
            /// <summary> Built-in motion BounceBackS </summary>
            BounceBackS,
            /// <summary> Built-in motion SnapMoveP </summary>
            SnapMoveP,
            /// <summary> Built-in motion SnapRemoveP </summary>
            SnapRemoveP,
            /// <summary> Built-in motion SnapInoutP </summary>
            SnapInoutP,
            /// <summary> Built-in motion SnapAppearP </summary>
            SnapAppearP,
            /// <summary> Built-in motion SnapAppearS </summary>
            SnapAppearS,
            /// <summary> Built-in motion BezierOut </summary>
            BezierOut,
            /// <summary> Built-in motion BezierBasic </summary>
            BezierBasic,
            /// <summary> Built-in motion BezierBreathIn </summary>
            BezierBreathIn,
            /// <summary> Built-in motion BezierBreathOut </summary>
            BezierBreathOut,
            /// <summary> Built-in motion Snap </summary>
            SnapUnfocusMoveP
        }

        private TimerEx tick;
        private readonly Dictionary<PhysicsCore, TimeStamp> coreDic;
        private readonly Dictionary<PhysicsCore, TimeStamp> coreFinishDic;
        private int duration;

        private readonly SynchronizationContext uiContext;

        private readonly object syncObject = new object();

        /// <summary>
        /// State of PhysicsAnimation
        /// </summary>
        public States State
        {
            get;
            set;
        }
        /// <summary>
        /// Duration in milli seconds of the animation.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when duration value is negative.</exception>
        public int Duration
        {
            get => duration;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Duration must be non-negative");
                }
                duration = value;
            }
        }
        /// <summary>
        ///  Gets/Sets the default alpha function for the animation.
        /// </summary>
        public BuiltinFunctions DefaultAlphaFunction
        {
            get;
            set;
        }
        /// <summary>
        /// Constructor to instantiate the PhysicsAnimation class.
        /// </summary>
        public PhysicsAnimation()
        {
            SecurityUtil.CheckPlatformPrivileges();
            State = States.Stopped;
            DefaultAlphaFunction = BuiltinFunctions.BezierBasic;

            coreDic = new Dictionary<PhysicsCore, TimeStamp>();
            coreFinishDic = new Dictionary<PhysicsCore, TimeStamp>();

            Duration = 2000;
            tick = new TimerEx();
            tick.Elapsed += TickUpdate;
            tick.Finished += TickFinished;

            if (TizenSynchronizationContext.Current == null)
            {
                TizenSynchronizationContext.Initialize();
            }
            uiContext = TizenSynchronizationContext.Current;
        }

        private void TickFinished(object sender, EventArgs e)
        {
            foreach (PhysicsCore p in coreDic.Keys.Where(x => x != null))
            {
                p.SaveCurrentValue();
            }

            State = States.Stopped;
            Finished?.Invoke(this, new EventArgs());
        }

        private void StopTick(object state)
        {
            tick?.Stop();
        }

        private void TickUpdate(object sender, TimerExEventArgs e)
        {
            lock (syncObject)
            {
                if (coreFinishDic.Count == 0)
                {
                    uiContext.Post(StopTick, null);
                    return;
                }

                for (int i = coreFinishDic.Count - 1; i >= 0; i--)
                {
                    KeyValuePair<PhysicsCore, TimeStamp> pair = coreFinishDic.ElementAt(i);
                    if (pair.Value != null)
                    {
                        if (sender is FrameCallback frameCallback)
                        {
                            if (pair.Value.StartTime <= e.ElapsedTime && e.ElapsedTime <= pair.Value.EndTime)
                            {
                                pair.Key.Update(frameCallback, e.Delta);
                            }
                            else if (e.ElapsedTime > pair.Value.EndTime)
                            {
                                pair.Key.Update(frameCallback, e.Delta);

                                pair.Key.Stop();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Animates a property to a destination value.
        /// </summary>
        /// <param name="target">The target object to animate</param>
        /// <param name="property">The target property to animate</param>
        /// <param name="destinationValue">The destination value</param>
        /// <param name="startTime">Start time of animation</param>
        /// <param name="endTime">End time of animation</param>
        /// <param name="alpha">The alpha function to apply</param>
        /// <exception cref="ArgumentNullException">Thrown when one of them(target, property, destination) is null.</exception>
        /// <exception cref="ArgumentException">Thrown when startTime or endTime is negative.</exception>
        /// <exception cref="ArgumentException">Thrown when startTime is bigger than endTime.</exception>
        public void AnimateTo(View target, string property, object destinationValue, int startTime, int endTime, BuiltinFunctions? alpha = null)
        {
            if (target == null || property == null || destinationValue == null)
            {
                throw new ArgumentNullException("target, property, destinationValue must not be null.");
            }
            if (startTime < 0 || endTime < 0)
            {
                throw new ArgumentException("startTime and endTime must be non-negative");
            }
            if (endTime < startTime)
            {
                throw new ArgumentException("endTime must be bigger than startTime");
            }

            PhysicsCore p = new PhysicsCore.Builder(property, target, destinationValue)
                .SetBuiltInFunction((alpha == null) ? DefaultAlphaFunction : alpha.Value)
                .SetDuration(endTime - startTime)
                .Build();
            p.Finished += PFinished;

            coreDic.Add(p, new TimeStamp(startTime, endTime));
        }
        /// <summary>
        /// Animates a property to a destination value.
        /// </summary>
        /// <param name="target">The target object to animate</param>
        /// <param name="property">The target property to animate</param>
        /// <param name="destinationValue">The destination value</param>
        /// <param name="alpha">The alpha function to apply</param>
        /// <exception cref="ArgumentNullException">Thrown when one of them(target, property, destination) is null.</exception>
        public void AnimateTo(View target, string property, object destinationValue, BuiltinFunctions? alpha = null)
        {
            AnimateTo(target, property, destinationValue, 0, Duration, alpha);
        }
        /// <summary>
        /// Plays the animation.
        /// </summary>
        public void Play()
        {
            if (State == States.Playing || coreDic.Count == 0)
            {
                return;
            }

            coreFinishDic.Clear();

            foreach (KeyValuePair<PhysicsCore, TimeStamp> item in coreDic)
            {
                coreFinishDic.Add(item.Key, item.Value);
                item.Key.Play();
            }

            tick.Play();

            State = States.Playing;

            if (SystemConfigUtil.Instance.PlatformSmartType == SystemConfigUtil.PlatformSmartTypes.ENTRY)
            {
                Stop(EndActions.StopFinal);
            }
        }
        /// <summary>
        /// Stops the animation.
        /// </summary>
        /// <param name="action">End action after stopping</param>
        public void Stop(EndActions action = EndActions.Cancel)
        {
            if (State == States.Stopped || coreDic.Count == 0)
            {
                return;
            }

            lock (syncObject)
            {
                foreach (PhysicsCore p in coreDic.Keys.Where(x => x != null))
                {
                    p.Stop(action);
                }
            }

            tick.Stop();
        }
        /// <summary>
        /// Pauses the animation.
        /// </summary>
        public void Pause()
        {
            if (State == States.Paused)
            {
                return;
            }

            tick.Pause();

            State = States.Paused;
        }
        /// <summary>
        /// Clears the animation.
        /// This disconnects any objects that were being animated, effectively stopping the animation.
        /// </summary>
        public void Clear()
        {
            lock (syncObject)
            {
                foreach (PhysicsCore p in coreDic.Keys.Where(x => x != null))
                {
                    p.Stop();
                    p.Delete();
                }

                coreDic.Clear();
                coreFinishDic.Clear();
            }
        }
        /// <summary>
        /// Event for Finished signal which can be used to subscribe/unsubscribe the event handler.
        /// Finished signal is emitted when an Animation's animations have finished.
        /// </summary>
        public event EventHandler Finished;

        /// <summary>
        /// Cleaning up managed and unmanaged resources 
        /// </summary>
        /// <param name="type">Disposing type</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.
                if (Finished != null)
                {
                    foreach (Delegate d in Finished.GetInvocationList())
                    {
                        Finished -= (EventHandler)d;
                    }
                }

                Stop();
                Clear();
                tick.Elapsed -= TickUpdate;
                tick.Finished -= TickFinished;
                tick.Stop();
                tick.Dispose();
                tick = null;
            }

            disposed = true;
        }

        private void PFinished(object sender, EventArgs e)
        {
            PhysicsCore p = sender as PhysicsCore;
            try
            {
                if (coreFinishDic.ContainsKey(p))
                {
                    coreFinishDic.Remove(p);
                }
            }
            catch (ArgumentNullException)
            {
                CLog.Debug("Key p is null");
            }
        }
    }
}
