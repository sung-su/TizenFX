using System.Collections.Generic;
using Tizen.NUI;

namespace Tizen.NUI.FLUX.Component
{

    /// <summary>
    /// Enum of Motion Type defined by UX Principle
    /// </summary>
    internal enum MotionTypes
    {
        Undefined,
        FocusIn_01,
        FocusOut_01,
        Basic_01,
        Basic_02,
        FadeIn_01,
        FadeIn_02,
        FadeIn_03,
        FadeOut_01,
        FadeOut_02,
        FadeOut_03,
        PressFeedback_S,
        PressFeedback_L,
        Remind_S,
        Remind_L,
        Bounceback_S,
        Bounceback_L,
        Scroll_01,
        Scroll_02,
        Scroll_03,
        Scroll_04,
        Scroll_05,
        Scroll_06,
        Appear_01,
        Appear_02,
        Appear_03,
        Disappear_01,
        Disappear_02,
        Select_In,
        Select_Out,
        LongPress_Focused,
        LongPress_Normal,
        LongPress_End,

    }


    internal class CurveValues
    {
        internal static readonly AlphaFunction ElasticCurve = new AlphaFunction(Constant.CURVE_ELASTIC_V1, Constant.CURVE_ELASTIC_V2);
        internal static readonly AlphaFunction OutCurve = new AlphaFunction(Constant.CURVE_OUT_V1, Constant.CURVE_OUT_V2);
        internal static readonly AlphaFunction BasicCurve = new AlphaFunction(Constant.CURVE_BASIC_V1, Constant.CURVE_BASIC_V2);
        internal static readonly AlphaFunction BreathingInCurve = new AlphaFunction(Constant.CURVE_BREATH_IN_V1, Constant.CURVE_BREATH_IN_V2);
        internal static readonly AlphaFunction BreathingOutCurve = new AlphaFunction(Constant.CURVE_BREATH_OUT_V1, Constant.CURVE_BREATH_OUT_V2);
        internal static readonly AlphaFunction AiryCurve = new AlphaFunction(Constant.CURVE_AIRY_V1, Constant.CURVE_AIRY_V1);
        internal static readonly AlphaFunction AppearCurve = new AlphaFunction(Constant.CURVE_APPEAR_V1, Constant.CURVE_APPEAR_V2);
        internal static readonly AlphaFunction EaseInOutCurve = new AlphaFunction(Constant.CURVE_EASEINOUT_V1, Constant.CURVE_EASEINOUT_V2);
        internal static readonly AlphaFunction RemindCurve01 = new AlphaFunction(Constant.CURVE_REMIND01_V1, Constant.CURVE_REMIND01_V2);
        internal static readonly AlphaFunction RemindCurve02 = new AlphaFunction(Constant.CURVE_REMIND02_V1, Constant.CURVE_REMIND02_V2);
        internal static readonly AlphaFunction ScrollCurve01 = new AlphaFunction(Constant.CURVE_SCROLL_V1, Constant.CURVE_SCROLL_V2);
        internal static readonly AlphaFunction ScrollCurve02 = new AlphaFunction(Constant.CURVE_SCROLL_V3, Constant.CURVE_SCROLL_V4);
        internal static readonly AlphaFunction Linear = new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear);
    }


    internal class MotionSpec
    {
        public MotionSpec()
        {

        }
        public MotionSpec(int duration, AlphaFunction curve, string property = null, object from = null, object to = null, MotionSpec addSpec = null)
        {
            Initialize(0, duration, curve, property, from, to, addSpec);
        }

        public MotionSpec(int startTime, int endTime, AlphaFunction curve, string property = null, object from = null, object to = null, MotionSpec addSpec = null)
        {
            Initialize(startTime, endTime, curve, property, from, to, addSpec);
        }

        public MotionSpec(MotionSpec spec)
        {
            if (spec == null)
            {
                return;
            }

            property = spec.Property;
            from = spec.From;
            to = spec.To;
            startTime = spec.StartTime;
            endTime = spec.EndTime;
            curve = spec.Curve;
            additionalSpec = spec.AdditionalSpec?.Clone();

        }

        public MotionSpec Clone()
        {
            return new MotionSpec(this);
        }

        public void Dispose()
        {
            property = null;
            from = null;
            to = null;
            curve = null;

            if (additionalSpec != null)
            {
                additionalSpec.Dispose();
                additionalSpec = null;
            }
        }

        public MotionSpec AdditionalSpec
        {
            get => additionalSpec;
            set
            {
                if (additionalSpec != null)
                {
                    additionalSpec.Dispose();
                    additionalSpec = null;
                }

                additionalSpec = value;
            }

        }

        public int StartTime
        {
            get => startTime;
            set => startTime = value;
        }

        public int EndTime
        {
            get => endTime;
            set => endTime = value;
        }

        public string Property
        {
            get => property;
            set => property = value;
        }

        public object From
        {
            get => from;
            set => from = value;
        }

        public object To
        {
            get => to;
            set => to = value;
        }

        public AlphaFunction Curve
        {
            get => curve;
            set => curve = value;
        }

        private void Initialize(int startTime, int endTime, AlphaFunction curve, string property, object from, object to, MotionSpec addSpec)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.property = property;
            this.from = from;
            this.to = to;
            this.curve = curve;
            additionalSpec = addSpec;
        }

        /// <summary>View object for animation</summary> 
        private string property = null;
        /// <summary>View object for animation</summary> 
        private object from = null;
        /// <summary>View object for animation</summary> 
        private object to = null;
        /// <summary>Start time of animation</summary>
        private int startTime = 0;
        /// <summary>End time of animation</summary>
        private int endTime = 0;
        /// <summary>Curve of animation</summary>
        private AlphaFunction curve = null;

        private MotionSpec additionalSpec = null;

        public static readonly Vector3 FocusInScaleValue = new Vector3(Constant.SCALE_FOCUS_IN, Constant.SCALE_FOCUS_IN, Constant.SCALE_ORIGINAL);
        public static readonly Vector3 SelectInScaleValue = new Vector3(Constant.SCALE_SELECT, Constant.SCALE_SELECT, Constant.SCALE_ORIGINAL);
        public static readonly Vector3 NomalScaleValue = new Vector3(Constant.SCALE_ORIGINAL, Constant.SCALE_ORIGINAL, Constant.SCALE_ORIGINAL);
        public static readonly Vector3 Appear01ScaleValue = new Vector3(Constant.SCALE_APPEAR_01, Constant.SCALE_APPEAR_01, Constant.SCALE_ORIGINAL);
        public static readonly Vector3 Appear03ScaleValue = new Vector3(Constant.SCALE_APPEAR_03, Constant.SCALE_APPEAR_03, Constant.SCALE_ORIGINAL);
        public static readonly Vector3 Dispppear01ScaleValue = new Vector3(Constant.SCALE_DISAPPEAR_01, Constant.SCALE_DISAPPEAR_01, Constant.SCALE_ORIGINAL);
        public static readonly Vector3 HideScaleValue = new Vector3(Constant.SCALE_HIDE, Constant.SCALE_HIDE, Constant.SCALE_ORIGINAL);
        public static readonly Vector3 PressScaleValue = new Vector3(Constant.SCALE_PRESS_NORMAL, Constant.SCALE_PRESS_NORMAL, Constant.SCALE_ORIGINAL);
        public static readonly Vector3 PressLScaleValue = new Vector3(Constant.SCALE_PRESS, Constant.SCALE_PRESS, Constant.SCALE_ORIGINAL);
        public static readonly Vector3 RemindSScaleValue = new Vector3(Constant.SCALE_REMIND_S, Constant.SCALE_REMIND_S, Constant.SCALE_ORIGINAL);
        public static readonly Vector3 RemindLScaleValue = new Vector3(Constant.SCALE_REMIND_L, Constant.SCALE_REMIND_L, Constant.SCALE_ORIGINAL);
        public static readonly Vector3 ImageTileFocusInScaleValue = new Vector3(Constant.SCALE_IMAGE_FOCUS_IN, Constant.SCALE_IMAGE_FOCUS_IN, Constant.SCALE_ORIGINAL);

        public static Dictionary<MotionTypes, MotionSpec> specTable = new Dictionary<MotionTypes, MotionSpec>
        {
            {MotionTypes.Undefined, new MotionSpec(  ) },
            {MotionTypes.FocusIn_01, new MotionSpec( 500 , CurveValues.ElasticCurve , "Scale", to:MotionSpec.FocusInScaleValue ) },
            {MotionTypes.FocusOut_01, new MotionSpec( 500 , CurveValues.OutCurve  , "Scale", to:MotionSpec.NomalScaleValue) },
            {MotionTypes.Select_In, new MotionSpec( 500 , CurveValues.ElasticCurve , "Scale", to:MotionSpec.SelectInScaleValue ) },
            {MotionTypes.Select_Out, new MotionSpec( 500 , CurveValues.OutCurve  , "Scale", to:MotionSpec.NomalScaleValue) },
            {MotionTypes.Basic_01, new MotionSpec( 300 , CurveValues.BasicCurve ) },
            {MotionTypes.Basic_02, new MotionSpec( 200 , CurveValues.BasicCurve ) },
            {MotionTypes.FadeIn_01, new MotionSpec( 300 , CurveValues.BasicCurve  , "Opacity",from:Constant.OPACITY_HIDE, to:Constant.OPACITY_ORIGINAL) },
            {MotionTypes.FadeIn_02, new MotionSpec( 200 , CurveValues.BasicCurve  , "Opacity",from:Constant.OPACITY_HIDE, to:Constant.OPACITY_ORIGINAL) },
            {MotionTypes.FadeIn_03, new MotionSpec( 500 , CurveValues.BasicCurve  , "Opacity",from:Constant.OPACITY_HIDE, to:Constant.OPACITY_ORIGINAL) },
            {MotionTypes.FadeOut_01, new MotionSpec( 300 , CurveValues.BasicCurve  , "Opacity", to:Constant.OPACITY_HIDE) },
            {MotionTypes.FadeOut_02, new MotionSpec( 200 , CurveValues.BasicCurve  , "Opacity", to:Constant.OPACITY_HIDE) },
            {MotionTypes.FadeOut_03, new MotionSpec( 500 , CurveValues.BasicCurve  , "Opacity", to: Constant.OPACITY_HIDE) },
            {MotionTypes.Scroll_01, new MotionSpec( 300 , CurveValues.BasicCurve ) },
            {MotionTypes.Scroll_02, new MotionSpec( 683 , CurveValues.ScrollCurve01 ) },
            {MotionTypes.Scroll_03, new MotionSpec( 566 , CurveValues.ScrollCurve01 ) },
            {MotionTypes.Scroll_04, new MotionSpec( 300 , CurveValues.BasicCurve ) },
            {MotionTypes.Scroll_05, new MotionSpec( 833 , CurveValues.BasicCurve ) },
            {MotionTypes.Scroll_06, new MotionSpec( 1000 , CurveValues.ScrollCurve02) },
            {MotionTypes.Appear_01, new MotionSpec( 500 , CurveValues.ElasticCurve , "Scale", MotionSpec.Appear01ScaleValue , MotionSpec.NomalScaleValue ) },
            {MotionTypes.Appear_02, new MotionSpec( 500 , CurveValues.AppearCurve , "Scale",  MotionSpec.HideScaleValue , MotionSpec.NomalScaleValue ) },
            {MotionTypes.Appear_03, new MotionSpec( 500 , CurveValues.ElasticCurve , "Scale",  MotionSpec.Appear03ScaleValue , MotionSpec.NomalScaleValue ) },
            {MotionTypes.Disappear_01, new MotionSpec( 500 , CurveValues.OutCurve , "Scale", MotionSpec.NomalScaleValue , MotionSpec.Dispppear01ScaleValue ) },
            {MotionTypes.Disappear_02, new MotionSpec( 500 , CurveValues.OutCurve , "Scale",  MotionSpec.NomalScaleValue , MotionSpec.HideScaleValue ) },
            {MotionTypes.PressFeedback_S, (new MotionSpec( 150 , CurveValues.BasicCurve , "Scale",   to:MotionSpec.PressScaleValue , addSpec:(new MotionSpec(150, 500 , CurveValues.BasicCurve , "Scale",   to:MotionSpec.NomalScaleValue) ))) },
            {MotionTypes.PressFeedback_L, (new MotionSpec( 150 , CurveValues.BasicCurve , "Scale",   to:MotionSpec.PressLScaleValue , addSpec:(new MotionSpec(150, 500 , CurveValues.BasicCurve , "Scale",   to:MotionSpec.NomalScaleValue) ))) },
            {MotionTypes.Remind_S, new MotionSpec( 500 , CurveValues.RemindCurve01 , "Scale",   to:MotionSpec.RemindSScaleValue , addSpec:(new MotionSpec(500, 1168 , CurveValues.RemindCurve02 , "Scale",   to:MotionSpec.NomalScaleValue) )) },
            {MotionTypes.Remind_L, new MotionSpec( 500 , CurveValues.RemindCurve01 , "Scale",   to:MotionSpec.RemindLScaleValue , addSpec:(new MotionSpec(500, 1168 , CurveValues.RemindCurve02 , "Scale",   to:MotionSpec.NomalScaleValue) )) },
            {MotionTypes.LongPress_Focused, new MotionSpec( Constant.DURATION_DEFAULT_SCROLL , CurveValues.BasicCurve , "Opacity",  to:Constant.OPACITY_ORIGINAL ) },
            {MotionTypes.LongPress_Normal, new MotionSpec( Constant.DURATION_DEFAULT_SCROLL , CurveValues.BasicCurve , "Opacity",  to:Constant.OPACITY_DIM ) },
            {MotionTypes.LongPress_End, new MotionSpec( Constant.DURATION_DEFAULT_SCROLL , CurveValues.BasicCurve , "Opacity",  to:Constant.OPACITY_ORIGINAL ) },
    };
    }
}


