using System;
using System.Collections.Generic;
using Tizen.NUI;


namespace Tizen.NUI.FLUX.Component
{
    internal class FluxAnimationPlayer : Tizen.NUI.FLUX.NUIDisposable
    {
        /// <summary>
        /// Enumeration for States of AnimationPlayer
        /// </summary>
        public enum States
        {
            /// <summary>
            /// AnimationPlayer is in stopped state.
            /// </summary>
            Stopped,
            /// <summary>
            /// AnimationPlayer is in playing state.
            /// </summary>
            Playing,
            /// <summary>
            /// AnimationPlayer is in paused state.
            /// </summary>
            Paused
        }

        /// <summary>
        /// Enumeration for where to jump after stopping of AnimationPlayer
        /// </summary>
        public enum EndActions
        {
            /// <summary>
            /// Target of AnimationPlayer will be in place when Stop() is called.
            /// </summary>
            Cancel,
            /// <summary>
            /// Target of AnimationPlayer will be located in the place where it started when Stop() is called.
            /// </summary>
            Discard,
            /// <summary>
            /// Target of AnimationPlayer will be located in the place where it would finish when Stop() is called.
            /// </summary>
            StopFinal
        }

        public event EventHandler Finished
        {
            add => animator.Finished += value;
            remove => animator.Finished -= value;
        }

        public bool Looping
        {
            get => animator.Looping;
            set => animator.Looping = value;
        }

        public EndActions EndAction
        {
            get => (EndActions)animator.EndAction;
            set => animator.EndAction = (Animation.EndActions)value;
        }

        public States State => (FluxAnimationPlayer.States)animator.State;

        public FluxAnimationPlayer(MotionTypes motionType)
        {
            Initialize(motionType);
        }

        public void Play()
        {
            animator.Play();
        }

        public void Play(FluxView view)
        {
            animator.Stop(Animation.EndActions.StopFinal);
            animator.Clear();
            AnimateTo(view);
            animator.Play();
        }


        public void Stop(EndActions endAction = EndActions.Cancel)
        {
            animator?.Stop((Animation.EndActions)endAction);
        }

        public void Pause()
        {
            animator.Pause();
        }

        public void Reset(EndActions endAction = EndActions.Cancel)
        {
            Stop(endAction);
            Clear();
        }

        public void Clear()
        {
            animator.Clear();
        }

        internal MotionTypes MotionType
        {
            get => motionType;
            set => Initialize(value);
        }


        internal void AnimateTo(FluxView animatee, StateProperty prop, string property = null, object to = null)
        {
            if (animatee == null)
            {
                return;
            }

            if (prop == null || prop.animatable.Count == 0)
            {
                AnimateTo(animatee, property, to);
                return;
            }

            MotionSpec newSpec = GenerateSpec(property, to, null, null, null);

            foreach (KeyValuePair<string, object> item in prop.animatable)
            {
                if (item.Key == newSpec.Property)
                {
                    FluxLogger.InfoP("** Skip [%d1][%s1][%s2] motionType:%s3,%s4", d1: animatee?.ID ?? 0, s1: animatee?.Name, s2: animatee?.GetTypeName(), s3: FluxLogger.EnumToString(motionType), s4: newSpec.Property);
                    continue;
                }


                if (newSpec.From != null)
                {
                    animatee.SetProperty(newSpec.Property, newSpec.From);
                }

                if (newSpec.AdditionalSpec is MotionSpec addtional)
                {
                    newSpec.To = animatee.Scale * (Vector3)newSpec.To;

                    // 나중에 추가 사양이 있으면 무조건 scale로 해논 것은 재고해야 함. 지금은 일단 scale 밖에 없어서 상관없음
                    Vector3 scaleTo = animatee.Scale * (Vector3)addtional.To;
                    animator?.AnimateTo(animatee, addtional.Property, scaleTo, addtional.StartTime, addtional.EndTime, addtional.Curve);
                }
                AnimateTo(animatee, newSpec);
            }
        }

        public void AnimateTo(FluxView animatee, string property = null, object to = null, int? startTime = null, int? endTime = null, AlphaFunction curve = null)
        {
            if (animatee == null)
            {
                return;
            }

            MotionSpec newSpec = GenerateSpec(property, to, startTime, endTime, curve);

            if (newSpec.From != null)
            {
                animatee.SetProperty(newSpec.Property, newSpec.From);
            }

            if (newSpec.AdditionalSpec is MotionSpec addtional)
            {
                newSpec.To = animatee.Scale * (Vector3)newSpec.To;

                // 나중에 추가 사양이 있으면 무조건 scale로 해논 것은 재고해야 함. 지금은 일단 scale 밖에 없어서 상관없음
                Vector3 scaleTo = animatee.Scale * (Vector3)addtional.To;
                animator?.AnimateTo(animatee, addtional.Property, scaleTo, addtional.StartTime, addtional.EndTime, addtional.Curve);
            }

            AnimateTo(animatee, newSpec);
        }

        public void AnimateTo(FluxView animatee, MotionSpec newSpec)
        {
            animator?.AnimateTo(animatee, newSpec.Property, newSpec.To, newSpec.StartTime, newSpec.EndTime, newSpec.Curve);
        }

        internal void AnimateTo(FluxView animatee, MotionTypes motionTypes)
        {
            if (MotionSpec.specTable.TryGetValue(motionTypes, out MotionSpec spec))
            {
                AnimateTo(animatee, spec);
            }
        }

        internal void AnimateTo(FluxView animatee, MotionTypes motionTypes, int delayTime)
        {
            if (delayTime < 0)
            {
                return;
            }
            if (MotionSpec.specTable.TryGetValue(motionTypes, out MotionSpec spec))
            {
                MotionSpec newSpec = new MotionSpec(spec);
                newSpec.StartTime += delayTime;
                newSpec.EndTime += delayTime;
                AnimateTo(animatee, newSpec);
            }
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (animator != null)
            {
                animator.Stop();
                animator.Clear();
                animator.Dispose();
                animator = null;
            }
        }

        private MotionSpec GenerateSpec(string property, object to, int? startTime, int? endTime, AlphaFunction curve)
        {
            MotionSpec retSpec = spec.Clone();

            if (property != null)
            {
                retSpec.Property = property;
            }

            if (to != null)
            {
                retSpec.To = to;
            }

            if (startTime != null)
            {
                retSpec.StartTime = startTime.Value;
            }

            if (endTime != null)
            {
                retSpec.EndTime = endTime.Value;
            }

            if (curve != null)
            {
                retSpec.Curve = curve;
            }

            return retSpec;
        }


        private void Initialize(MotionTypes type)
        {
            if (MotionSpec.specTable.TryGetValue(type, out MotionSpec motionSpec) == true)
            {
                spec = motionSpec;
                motionType = type;
            }
        }

        private MotionTypes motionType = MotionTypes.Undefined;

        private MotionSpec spec = null;

        private FluxAnimation animator = new FluxAnimation();
    }



}
