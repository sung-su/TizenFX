/// @file PhysicsCore.cs
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
using Tizen.NUI.BaseComponents;
using Tizen.TV.SPhysics;

namespace Tizen.NUI.FLUX
{
    internal abstract class PhysicsCore
    {
        public enum States
        {
            Stopped,
            Playing,
            Paused
        }

        protected enum JumpOverMode
        {
            From, 
            To
        }

        internal States State { get; private set; } = States.Stopped;
        protected abstract void SetOrigin(View v);
        protected abstract void UpdateViewFromSPhysics(FrameCallback fCallback, uint viewID);
        protected abstract void SetDestination(object destinationValue);
        protected abstract void Save(View v);        
        protected abstract void JumpOver(JumpOverMode mode, View v);
        protected WeakReference WeakRefForView{ get; set; }

        protected SPhysicsAttribute SPhysicsAttribute;

        private uint viewID;

        private bool canSave;

        public void Play()
        {  
            if(State == States.Playing)
            {
                return;
            }

            View targetView = WeakRefForView.Target as View;

            if(targetView == null)
            {
                throw new InvalidOperationException("Animation Target View is null");
            }

            SetOrigin(targetView);            

            if (motionID < 0)
            {
                motionID = SPhysicsSingle.Create(SPhysicsAttribute);
            }
            else
            {
                SPhysicsSingle.Modify(motionID, SPhysicsAttribute);
            }

            if (nativeFinishedInstance == null)
            {
                nativeFinishedInstance = NativeFinished;
            }

            SPhysicsSingle.RegisterStopEvent(motionID, nativeFinishedInstance);

            SPhysicsSingle.Play(motionID);

            viewID = targetView.ID;

            canSave = true;

            State = States.Playing;
        }

        public void Stop(PhysicsAnimation.EndActions endAction = PhysicsAnimation.EndActions.Cancel)
        {
            if(State == States.Stopped)
            {
                return;
            }

            canSave = (endAction != PhysicsAnimation.EndActions.Cancel) ? false : true;
            
            SPhysicsSingle.Stop(motionID);
             
            JumpOver(endAction);            
        }

        public void Delete()
        {
            if (Finished != null)
            {
                foreach (Delegate d in Finished.GetInvocationList())
                {
                    Finished -= (EventHandler)d;
                }
            }

            nativeFinishedInstance = null;
            SPhysicsSingle.Delete(motionID);
        }

        public void Update(FrameCallback fCallback, float delta)
        {
            if(State != States.Playing)
            {
                return;
            }

            SPhysicsSingle.Update(motionID, delta);

            UpdateViewFromSPhysics(fCallback, viewID);
        }

        public void SaveCurrentValue()
        {
            if(canSave)
            {
                View v = WeakRefForView.Target as View;
                if (v != null)
                {
                    Save(v);
                }
            }            
        }

        private void JumpOver(PhysicsAnimation.EndActions endAction)
        {
            View v = WeakRefForView.Target as View;
            if (v == null)
            {
                return;
            }

            if (SPhysicsAttribute.AnimationType == SPhysicsAnimationType.BounceBackL || SPhysicsAttribute.AnimationType == SPhysicsAnimationType.BounceBackS)
            {
                JumpOver(JumpOverMode.From, v);
                return;
            }
            if (endAction == PhysicsAnimation.EndActions.Discard)
            {
                JumpOver(JumpOverMode.From, v);
            }
            else if (endAction == PhysicsAnimation.EndActions.StopFinal)
            {
                JumpOver(JumpOverMode.To, v);
            }
        }

        private void NativeFinished()
        {
            State = States.Stopped;
            Finished?.Invoke(this, EventArgs.Empty);
        }

        private SPhysicsSingle.StopEvent nativeFinishedInstance;        
        protected int motionID = -1;        
        public event EventHandler Finished;

        public class Builder
        {            
            private string type;
            private WeakReference weakRefForView;
            private object destinationValue;
                      
            private int duration = 2000;
            private SPhysicsAnimationType builtInFunction = SPhysicsAnimationType.BezierBasic;

            public Builder(string type, View view, object destinationValue)
            {
                this.type = type;
                this.weakRefForView = new WeakReference(view);
                this.destinationValue = destinationValue;
            }

            public Builder SetDuration(int duration)
            {
                this.duration = duration;
                return this;
            }
            public Builder SetBuiltInFunction(PhysicsAnimation.BuiltinFunctions builtInFunction)
            {
                this.builtInFunction = (SPhysicsAnimationType)builtInFunction;
                return this;
            }
                        
            public PhysicsCore Build()
            {   
                PhysicsCore core = PhysicsCoreFactory.CreateInstance(type);
                core.SPhysicsAttribute = new SPhysicsAttribute()
                {
                    Duration = duration / 1000.0f,
                    AnimationType = builtInFunction
                };
                core.WeakRefForView = weakRefForView;                
                core.SetDestination(destinationValue);

                return core;
            }                        
        }
    }

    internal class PositionXPhysicsCore:PhysicsCore
    {   
        protected override void SetDestination(object destinationValue)
        {
            SPhysicsAttribute.ToX = Convert.ToSingle(destinationValue);                        
        }
        protected override void SetOrigin(View v)
        {            
            SPhysicsAttribute.FromX = v.PositionX;                 
        }
        protected override void UpdateViewFromSPhysics(FrameCallback cb, uint viewID)
        {
            Vector3 pos = new Vector3();
            cb.GetPosition(viewID, pos);
#pragma warning disable CS0618
            pos.X = SPhysicsSingle.GetX(motionID);
#pragma warning restore CS0618
            cb.BakePosition(viewID, pos);         
        }
        protected override void JumpOver(JumpOverMode mode, View v)
        {
            v.PositionX = (mode == JumpOverMode.From) ? SPhysicsAttribute.FromX : SPhysicsAttribute.ToX;                        
        }

        protected override void Save(View v)
        {
            v.PositionX = v.CurrentPosition.X;
        }
    }

    internal class PositionYPhysicsCore : PhysicsCore
    {
        protected override void SetDestination(object destinationValue)
        {
            SPhysicsAttribute.ToY = Convert.ToSingle(destinationValue);
        }

        protected override void SetOrigin(View v)
        {            
            SPhysicsAttribute.FromY = v.PositionY;
        }

        protected override void UpdateViewFromSPhysics(FrameCallback cb, uint viewID)
        {
            Vector3 pos = new Vector3();
            cb.GetPosition(viewID, pos);
#pragma warning disable CS0618
            pos.Y = SPhysicsSingle.GetY(motionID);
#pragma warning restore CS0618
            cb.BakePosition(viewID, pos);
        }

        protected override void JumpOver(JumpOverMode mode, View v)
        {            
            v.PositionY = (mode == JumpOverMode.From) ? SPhysicsAttribute.FromY : SPhysicsAttribute.ToY;
        }

        protected override void Save(View v)
        {
            v.PositionY = v.CurrentPosition.Y;
        }
    }
    internal class Position2DPhysicsCore:PhysicsCore
    {
        protected override void SetDestination(object destinationValue)
        {
            Position2D position2D = destinationValue as Position2D;
            if(position2D == null)
            {
                throw new InvalidCastException("cannot cast destinationValue to");
            }
            SPhysicsAttribute.ToX = position2D.X;
            SPhysicsAttribute.ToY = position2D.Y;
        }
        protected override void SetOrigin(View v)
        {            
            SPhysicsAttribute.FromX = v.PositionX;
            SPhysicsAttribute.FromY = v.PositionY;
        }
        protected override void UpdateViewFromSPhysics(FrameCallback cb, uint viewID)
        {
            Vector3 pos = new Vector3();
            cb.GetPosition(viewID, pos);
#pragma warning disable CS0618
            pos.X = SPhysicsSingle.GetX(motionID);
            pos.Y = SPhysicsSingle.GetY(motionID);
#pragma warning restore CS0618
            cb.BakePosition(viewID, pos);
        }
        protected override void JumpOver(JumpOverMode mode, View v)
        {            
            float x = (mode == JumpOverMode.From) ? SPhysicsAttribute.FromX : SPhysicsAttribute.ToX;
            float y = (mode == JumpOverMode.From) ? SPhysicsAttribute.FromY : SPhysicsAttribute.ToY;
            v.Position2D = new Position2D((int)x, (int)y);
        }

        protected override void Save(View v)
        {            
            v.PositionX = v.CurrentPosition.X;
            v.PositionY = v.CurrentPosition.Y;
        }
    }

    internal class ScalePhysicsCore : PhysicsCore
    {
        protected override void SetDestination(object destinationValue)
        {
            Vector3 scale = destinationValue as Vector3;
            if (scale == null)
            {
                throw new InvalidCastException("cannot cast value");
            }
            SPhysicsAttribute.ToX = scale.Width;
            SPhysicsAttribute.ToY = scale.Height;
        }
        protected override void SetOrigin(View v)
        {            
            SPhysicsAttribute.FromX = v.ScaleX;
            SPhysicsAttribute.FromY = v.ScaleY;
        }
        protected override void UpdateViewFromSPhysics(FrameCallback cb, uint viewID)
        {
            Vector3 scale = new Vector3();
            cb.GetScale(viewID, scale);
#pragma warning disable CS0618
            scale.X = SPhysicsSingle.GetX(motionID);
            scale.Y = SPhysicsSingle.GetY(motionID);
#pragma warning restore CS0618
            cb.BakeScale(viewID, scale);            
        }
        protected override void JumpOver(JumpOverMode mode, View v)
        {            
            float x = (mode == JumpOverMode.From) ? SPhysicsAttribute.FromX : SPhysicsAttribute.ToX;
            float y = (mode == JumpOverMode.From) ? SPhysicsAttribute.FromY : SPhysicsAttribute.ToY;
            v.Scale = new Vector3(x, y, 1.0f);
        }
        protected override void Save(View v)
        {
            v.Scale = v.GetCurrentScale();
        }
    }
    internal class OpacityPhysicsCore : PhysicsCore
    {        
        protected override void SetDestination(object destinationValue)
        {
            SPhysicsAttribute.ToX = Convert.ToSingle(destinationValue);
        }
        protected override void SetOrigin(View v)
        {            
            SPhysicsAttribute.FromX = v.Opacity;            
        }
        protected override void UpdateViewFromSPhysics(FrameCallback cb, uint viewID)
        {
            Vector4 color = new Vector4();
            cb.GetColor(viewID, color);
#pragma warning disable CS0618
            color.A = SPhysicsSingle.GetX(motionID);
#pragma warning restore CS0618
            cb.BakeColor(viewID, color);
        }
        protected override void JumpOver(JumpOverMode mode, View v)
        {            
            v.Opacity = (mode == JumpOverMode.From) ? SPhysicsAttribute.FromX : SPhysicsAttribute.ToX;
        }

        protected override void Save(View v)
        {
            v.Opacity = v.GetCurrentOpacity();
        }
    }

    internal class PhysicsCoreFactory
    {
        public static PhysicsCore CreateInstance(string type) => type switch
        {            
            "PositionX" => new PositionXPhysicsCore(),
            "PositionY" => new PositionYPhysicsCore(),
            var t when (t == "Position") || (t == "Position2D") => new Position2DPhysicsCore(),            
            "Scale" => new ScalePhysicsCore(),
            "Opacity" => new OpacityPhysicsCore(),
            _ => throw new NotSupportedException("Wrong type name is used:" + type)
        };
    }
}