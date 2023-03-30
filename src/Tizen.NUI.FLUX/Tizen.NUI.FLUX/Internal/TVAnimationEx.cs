/// @file TVAnimationEx.cs
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
using System.Runtime.InteropServices;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    internal class AnimationEX : NUIDisposable
    {
        private HandleRef handle;

        public AnimationEX(int duration, int delay = 0)
        {
            CLog.Error("Create AnimationEX");
            handle = new HandleRef(this, Interop.AnimationEX.New(MilliSecondsToSeconds(duration), MilliSecondsToSeconds(delay)));
        }

        public int Duration
        {
            get => SecondsToMilliSeconds(Interop.AnimationEX.GetDuration(handle));
            set => Interop.AnimationEX.SetDuration(handle, MilliSecondsToSeconds(value));
        }

        public void SetLooping(bool loop)
        {
            Interop.AnimationEX.SetLooping(handle, loop);
        }

        public int LoopCount
        {
            get => Interop.AnimationEX.GetLoopCount(handle);
            set => Interop.AnimationEX.SetLoopCount(handle, value);
        }

        public int Delay
        {
            get => SecondsToMilliSeconds(Interop.AnimationEX.GetDelay(handle));
            set => Interop.AnimationEX.SetDelay(handle, MilliSecondsToSeconds(value));
        }

        public int GetCurrentLoopCount()
        {
            return Interop.AnimationEX.GetCurrentLoopCount(handle);
        }

        public bool IsLooping()
        {
            return Interop.AnimationEX.IsLooping(handle);
        }

        public AlphaFunction DefaultAlphaFunction
        {
            get
            {
                AlphaFunction alphaFunc = new AlphaFunction(Interop.AnimationEX.GetDefaultAlphaFunction(handle), true);
                return alphaFunc;
            }
            set => Interop.AnimationEX.SetDefaultAlphaFunction(handle, AlphaFunction.getCPtr(value));
        }

        public float CurrentProgress
        {
            get => Interop.AnimationEX.GetCurrentProgress(handle);
            set => Interop.AnimationEX.SetCurrentProgress(handle, value);
        }

        public float SpeedFactor
        {
            get => Interop.AnimationEX.GetSpeedFactor(handle);
            set => Interop.AnimationEX.SetCurrentProgress(handle, value);
        }

        public RelativeVector2 PlayRange
        {
            set => Interop.AnimationEX.SetPlayRange(handle, Vector2.getCPtr(value));
            get
            {
                Vector2 ret = new Vector2(Interop.AnimationEX.GetPlayRange(handle), true);
                return ret;
            }
        }

        public float GetTime()
        {
            return Interop.AnimationEX.GetTime(handle);
        }

        public void Play()
        {
            Interop.AnimationEX.Play(handle);
        }

        public void Pause()
        {
            Interop.AnimationEX.Pause(handle);
        }

        public void Stop()
        {
            Interop.AnimationEX.Stop(handle);
        }

        public Animation.States State => (Animation.States)Interop.AnimationEX.GetState(handle);

        public void Clear()
        {
            Interop.AnimationEX.Clear(handle);
        }

        private AnimationExFinishedEventCallbackType animationExFinishedEventCallback;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void AnimationExFinishedEventCallbackType();

        private event EventHandler AnimationExFinishedEventHandler;

        public event EventHandler Finished
        {
            add
            {
                if (AnimationExFinishedEventHandler == null)
                {
                    animationExFinishedEventCallback = OnFinished;
                    FinishedSignal().Connect(animationExFinishedEventCallback);
                    CLog.Error("FinishedHandler Connection Count (Add): %d1", d1: FinishedSignal().GetConnectionCount());
                }
                AnimationExFinishedEventHandler += value;
            }
            remove
            {
                AnimationExFinishedEventHandler -= value;

                if (AnimationExFinishedEventHandler == null && FinishedSignal().Empty() == false)
                {
                    FinishedSignal().Disconnect(animationExFinishedEventCallback);
                    CLog.Error("FinishedHandler Connection Count (Remove): %d1", d1: FinishedSignal().GetConnectionCount());
                }
            }
        }

        private void OnFinished()
        {
            //Send event to, attached handler
            AnimationExFinishedEventHandler?.Invoke(this, null);
        }


        private AnimationExUpdateEventCallbackType animationExUpdateEventCallback;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void AnimationExUpdateEventCallbackType(float progress, float alpha);

        private DaliEventHandler<float, float> animationExUpdateEventHandler;

        public event DaliEventHandler<float, float> Update
        {
            add
            {
                if (animationExUpdateEventHandler == null)
                {
                    animationExUpdateEventCallback = OnUpdate;
                    UpdateSignal().Connect(animationExUpdateEventCallback);
                }
                animationExUpdateEventHandler += value;
            }
            remove
            {
                animationExUpdateEventHandler -= value;

                if (animationExUpdateEventHandler == null && UpdateSignal().Empty() == false)
                {
                    UpdateSignal().Disconnect(animationExUpdateEventCallback);
                }
            }
        }

        private void OnUpdate(float progress, float alpha)
        {
            //Send event to, attached handler
            animationExUpdateEventHandler?.Invoke(progress, alpha);
        }

        private float MilliSecondsToSeconds(int millisec)
        {
            return millisec / 1000.0f;
        }

        private int SecondsToMilliSeconds(float sec)
        {
            return (int)(sec * 1000);
        }

        internal AnimationExFinishedSignal FinishedSignal()
        {
            AnimationExFinishedSignal ret = new AnimationExFinishedSignal(Interop.AnimationEX.FinishedSignal(handle), false);
            return ret;
        }

        internal AnimationExUpdateSignal UpdateSignal()
        {
            AnimationExUpdateSignal ret = new AnimationExUpdateSignal(Interop.AnimationEX.UpdateSignal(handle), false);
            return ret;
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                CLog.Error("AnimationEx Disposing Explicitly");
                if (animationExUpdateEventHandler != null)
                {
                    foreach (Delegate d in animationExUpdateEventHandler.GetInvocationList())
                    {
                        animationExUpdateEventHandler -= (DaliEventHandler<float, float>)d;
                    }
                }
                if (AnimationExFinishedEventHandler != null)
                {
                    foreach (Delegate d in AnimationExFinishedEventHandler.GetInvocationList())
                    {
                        AnimationExFinishedEventHandler -= (EventHandler)d;
                    }
                }
            }

            //Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.

            CLog.Error("AnimationEx Releasing native memories");

            if (animationExUpdateEventCallback != null)
            {
                UpdateSignal().Disconnect(animationExUpdateEventCallback);
            }
            if (animationExFinishedEventCallback != null)
            {
                FinishedSignal().Disconnect(animationExFinishedEventCallback);
            }

            Interop.AnimationEX.Delete(handle);
            handle = new HandleRef(null, IntPtr.Zero);

            disposed = true;
        }
    }
}

