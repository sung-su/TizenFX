/**
*Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
*For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
*/

using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// 
    /// </summary>
    internal class GestureDetector : NUIDisposable
    {
        public static GestureDetector Instance { get; } = new GestureDetector();
        private GestureDetector()
        {
            TapGestureDetector = new TapGestureDetector();
            TapGestureDetector.SetMaximumTapsRequired(1);
            TapGestureDetector.Detected += TapGestureDetected;

            LongPressGestureDetector = new LongPressGestureDetector();
            LongPressGestureDetector.Detected += LongPressGestureDetected;
        }

        private void TapGestureDetected(object source, TapGestureDetector.DetectedEventArgs e)
        {
            if (e.View is Component component)
            {
                component.OnTapped(source, e);
            }
        }

        private void LongPressGestureDetected(object source, LongPressGestureDetector.DetectedEventArgs e)
        {
            if (e.View is Component component)
            {
                component.OnTouchLongPressed(source, e);
            }
        }

        internal LongPressGestureDetector LongPressGestureDetector
        {
            get;
            set;
        }

        internal TapGestureDetector TapGestureDetector
        {
            get;
            set;
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }
            if (type == DisposeTypes.Explicit)
            {
                if (TapGestureDetector != null)
                {
                    TapGestureDetector.Detected -= TapGestureDetected;
                    TapGestureDetector.DetachAll();
                    TapGestureDetector.Dispose();
                    TapGestureDetector = null;
                }

                if (LongPressGestureDetector != null)
                {
                    LongPressGestureDetector.Detected -= LongPressGestureDetected;
                    LongPressGestureDetector.DetachAll();
                    LongPressGestureDetector.Dispose();
                    LongPressGestureDetector = null;
                }
            }

        }

        internal string ConvertWheelEventToVertical(View.WheelEventArgs e)
        {
            FluxLogger.InfoP("{%s1}, direction : {%s2}, value:{%d1}", s1: e.Wheel.Type.ToString(), s2: e.Wheel.Direction.ToString(), d1: e.Wheel.Z);
            string result = null;

            if (e.Wheel.Type == Wheel.WheelType.MouseWheel)
            {
                if (e.Wheel.Z == -1)
                {
                    result = "Up";
                }
                else
                {
                    result = "Down";
                }
            }
            return result;
        }

        internal string ConvertWheelEventToHorizontal(View.WheelEventArgs e, bool inverse = false)
        {
            FluxLogger.InfoP("{%s1}, direction : {%s2}, value:{%d1}", s1: e.Wheel.Type.ToString(), s2: e.Wheel.Direction.ToString(), d1: e.Wheel.Z);
            string result = null;

            if (e.Wheel.Type == Wheel.WheelType.MouseWheel)
            {
                int zValue = -1;
                if (inverse == true)
                {
                    zValue = 1;
                }

                if (e.Wheel.Z == zValue)
                {
                    result = "Left";
                }
                else
                {
                    result = "Right";
                }
            }
            return result;
        }

        internal bool IsPrimaryMouseButton(Gesture gesture)
        {
            if (gesture.Source == Gesture.SourceType.Mouse)
            {
                if (gesture.SourceData != Gesture.SourceDataType.MousePrimary)
                {
                    FluxLogger.DebugP("Source : %s1, Type : %s2 skipped", s1: gesture.Source.ToString(), s2: gesture.SourceData.ToString());
                    return false;
                }
            }

            return true;
        }
    }



}
