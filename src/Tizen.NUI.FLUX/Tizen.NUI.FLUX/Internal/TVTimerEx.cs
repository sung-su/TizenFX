/// @file TVTimerEx.cs
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
    internal class TimerEx : NUIDisposable
    {
        public enum States
        {
            Playing, 
            Stopped, 
            Pause
        }

        public enum StopMode
        {
            Sync,
            Async
        }
        
        public TimerEx()
        {            
            frameCallback = new FrameCallback();
            State = States.Stopped;
            initStart = true;
            paused = false;
        }

        internal States State
        {
            private set;
            get;
        }

        public void Stop()
        {
            if (State == States.Stopped)
            {
                return;
            }

            frameCallback.Update -= FrameCallbackUpdate;

            lock (syncObject)
            {
                initStart = true;

                paused = false;

                Finished?.Invoke(this, EventArgs.Empty);
            }

            State = States.Stopped;
        }

        public void Pause()
        {
            if (State == States.Pause)
            {
                return;
            }

            frameCallback.Update -= FrameCallbackUpdate;

            lock (syncObject)
            {                                            
                paused = true;
            }

            State = States.Pause;
        }

        public void Play()
        {
            if (State == States.Playing)
            {
                return;
            }

            frameCallback.Update += FrameCallbackUpdate;

            State = States.Playing;
        }

        private void FrameCallbackUpdate(object sender, FrameCallbackEventArgs e)
        {
            lock (syncObject)
            {   
                if (initStart)
                {
                    startTime = DateTime.Now.Ticks;
                    prevTime = startTime;
                    initStart = false;
                    return;
                }

                if (paused)
                {
                    long current = DateTime.Now.Ticks;
                    startTime = current - elapsedTicks;
                    prevTime = current;
                    paused = false;
                    return;
                }
            }

            long curTime = DateTime.Now.Ticks;
            long gapTicks = curTime - prevTime;
            elapsedTicks = curTime - startTime;
            TimeSpan gapTime = new TimeSpan(gapTicks);
            TimeSpan elapsedTime = new TimeSpan(elapsedTicks);
            Elapsed?.Invoke(frameCallback, new TimerExEventArgs((int)elapsedTime.TotalMilliseconds, ((float)gapTime.TotalMilliseconds / 1000.0f)));
            prevTime = curTime;            
        }

        protected override void Dispose(DisposeTypes type)
        {
            if(disposed)
            {
                return;
            }

            if(type == DisposeTypes.Explicit)
            {   
                frameCallback.Update -= FrameCallbackUpdate;                
                frameCallback.Dispose();
                frameCallback = null;                
            }

            disposed = true;
        }
        
        public event EventHandler<TimerExEventArgs> Elapsed;
        public event EventHandler<EventArgs> Finished;
        
        private FrameCallback frameCallback;
        private long startTime;
        private long elapsedTicks;
        private long prevTime;
        private bool initStart = true;
        private bool paused = false;

        private readonly object syncObject = new object();

    }
}