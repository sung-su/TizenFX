/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file Constants.cs
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

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// Component touch mode. User can choose their component as Pointing action. (Touch / Mouse)
    /// Default value is "PressInFocus"
    /// </summary>
    /// <code>
    /// component.PointingBehavior = PointingBehaviorMode.ExecuteIsFocus; 
    /// </code>
    /// <version> 9.9.0 </version>
    public enum PointingBehaviorMode
    {
        /// <summary>
        /// Touch / Mouse Click event will set focus to target component and excute.
        /// Focus comes up when pressing and then Execute.
        /// </summary>
        PressIsFocus,

        /// <summary>
        /// Touch / Mouse Click event will set focus to target component.
        /// If components or patterns don't have focus, the focus moved to it. / If they have, Execute.
        /// </summary>
        ExecuteIsFocus
    }


    /// <summary>
    /// Constant class
    /// </summary>
    /// <code>
    /// Constant.CURVE_ELASTIC_V1
    /// </code>
    public class Constant
    {
        /// <summary>
        /// CURVE_ELASTIC_V1 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_ELASTIC_V1 = new Vector2(0.21f, 1.5f);
        /// <summary>
        /// CURVE_ELASTIC_V2 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_ELASTIC_V2 = new Vector2(0.54f, 1);
        /// <summary>
        /// CURVE_OUT_V1 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_OUT_V1 = new Vector2(0.15f, 1);
        /// <summary>
        /// CURVE_OUT_V2 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_OUT_V2 = new Vector2(0.35f, 1);
        /// <summary>
        /// CURVE_BASIC_V1 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_BASIC_V1 = new Vector2(0.3f, 0);
        /// <summary>
        /// CURVE_BASIC_V2 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_BASIC_V2 = new Vector2(0.15f, 1);
        /// <summary>
        /// CURVE_APPEARS_V1 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_APPEARS_V1 = new Vector2(0.32f, 1.35f);
        /// <summary>
        /// CURVE_APPEARS_V2 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_APPEARS_V2 = new Vector2(0.3f, 1.0f);
        /// <summary>
        /// CURVE_BREATH_IN_V1 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_BREATH_IN_V1 = new Vector2(0.5f, 0.0f);
        /// <summary>
        /// CURVE_BREATH_IN_V2 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_BREATH_IN_V2 = new Vector2(0.7f, 1.0f);
        /// <summary>
        /// CURVE_BREATH_OUT_V1 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_BREATH_OUT_V1 = new Vector2(0.3f, 0.0f);
        /// <summary>
        /// CURVE_BREATH_OUT_V2 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_BREATH_OUT_V2 = new Vector2(0.4f, 1.0f);
        /// <summary>
        /// CURVE_SININOUT_V1 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_SININOUT_V1 = new Vector2(0.3f, 0);
        /// <summary>
        /// CURVE_SININOUT_V2 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_SININOUT_V2 = new Vector2(0.3f, 1);
        /// <summary>
        /// CURVE_AIRY_V1 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_AIRY_V1 = new Vector2(0.3f, 0);
        /// <summary>
        /// CURVE_AIRY_V2 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_AIRY_V2 = new Vector2(0.7f, 1);
        /// <summary>
        /// CURVE_EASEINOUT_V1 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_EASEINOUT_V1 = new Vector2(0.50f, 0.10f);
        /// <summary>
        /// CURVE_EASEINOUT_V2 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_EASEINOUT_V2 = new Vector2(0.30f, 1.0f);

        /// <summary>
        /// CURVE_APPEAR_V1 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_APPEAR_V1 = new Vector2(0.32f, 1.35f);
        /// <summary>
        /// CURVE_APPEAR_V2 parameter for animation
        /// </summary>
        public static readonly Vector2 CURVE_APPEAR_V2 = new Vector2(0.3f, 1);


        /// <summary>
        /// Define font size by UX, 144
        /// </summary>
        /// <version> 9.9.0 </version>
        public static readonly int T144 = 144;

        /// <summary>
        /// Define font size by UX, 120
        /// </summary>
        /// <version> 9.9.0 </version>
        public static readonly int T120 = 120;

        /// <summary>
        /// Define font size by UX, 72
        /// </summary>
        public static readonly int T72 = 72;
        /// <summary>
        /// Font size is 68
        /// </summary>
        public static readonly int T68 = 68;
        /// <summary>
        /// Font size is 64
        /// </summary>
        public static readonly int T64 = 64;
        /// <summary>
        /// Font size is 60
        /// </summary>
        public static readonly int T60 = 60;
        /// <summary>
        /// Font size is 56
        /// </summary>
        public static readonly int T56 = 56;
        /// <summary>
        /// Font size is 52
        /// </summary>
        public static readonly int T52 = 52;
        /// <summary>
        /// Font size is 48
        /// </summary>
        public static readonly int T48 = 48;
        /// <summary>
        /// Font size is 44
        /// </summary>
        public static readonly int T44 = 44;
        /// <summary>
        /// Font size is 42
        /// </summary>
        public static readonly int T42 = 42;
        /// <summary>
        /// Font size is 40
        /// </summary>
        public static readonly int T40 = 40;
        /// <summary>
        /// Font size is 36
        /// </summary>
        public static readonly int T36 = 36;
        /// <summary>
        /// Font size is 32
        /// </summary>
        public static readonly int T32 = 32;
        /// <summary>
        /// Font size is 28
        /// </summary>
        public static readonly int T28 = 28;
        /// <summary>
        /// Font size is 24
        /// </summary>
        public static readonly int T24 = 24;
        /// <summary>
        /// Font size is 20
        /// </summary>
        public static readonly int T20 = 20;
        /// <summary>
        /// Font size is 16
        /// </summary>
        public static readonly int T16 = 16;
        /// <summary>
        /// Font size is 12. This is only touch based product.
        /// </summary>
        /// <version>10.10.0</version>
        public static readonly int T12 = 12;
        /// <summary>
        /// OutStrokeWidth is 0
        /// </summary>
        public static readonly int OutStrokeWidth = 0;

        /// <summary> Input Event type Hover </summary>
        /// <version> 9.9.0 </version>
        internal const string Hover = "Hover";
        /// <summary> Input Event type Touch</summary>
        /// <version> 9.9.0 </version>
        public const string Touch = "Touch";
        /// <summary> Input Event type Tap </summary>
        /// <version> 9.9.0 </version>
        public const string Tap = "Tap";
        /// <summary> Input Event type TouchLongPress </summary>
        /// <version> 9.9.0 </version>
        public const string TouchLongPress = "TouchLongPress";
        /// <summary> All Input Event type </summary>
        /// <version> 9.9.0 </version>
        public const string EventAll = "EventAll";

        /// <summary> Tab PropertyEnabler type: ItemShadow </summary>
        /// <version> 9.9.0 </version>
        public const string ItemShadow = "ItemShadow";

        /// <summary> Plate type - Rect </summary>
        /// <version> 9.9.0 </version>
        public const string UIPlateRect = "Rect";

        /// <summary> Plate type - RoundRect </summary>
        /// <version> 9.9.0 </version>
        public const string UIPlateRoundRect = "RoundRect";

        /// <summary> Plate type - Round </summary>
        /// <version> 9.9.0 </version>
        public const string UIPlateRound = "Round";


        #region internal constants

        internal static readonly Vector2 CURVE_REMIND01_V1 = new Vector2(0.5f, 0);
        internal static readonly Vector2 CURVE_REMIND01_V2 = new Vector2(0.7f, 1.0f);

        internal static readonly Vector2 CURVE_REMIND02_V1 = new Vector2(0.3f, 0);
        internal static readonly Vector2 CURVE_REMIND02_V2 = new Vector2(0.4f, 1.0f);

        internal static readonly Vector2 CURVE_SCROLL_V1 = new Vector2(0.5f, 1.0f);
        internal static readonly Vector2 CURVE_SCROLL_V2 = new Vector2(0.3f, 1.0f);
        internal static readonly Vector2 CURVE_SCROLL_V3 = new Vector2(0.15f, 1.0f);
        internal static readonly Vector2 CURVE_SCROLL_V4 = new Vector2(0.34f, 1.0f);

        internal const float EPSILON = 0.000001f;

        internal static readonly int DURATION_20FRAME = 334;
        internal static readonly int DURATION_FOCUS_IN = 500;
        internal static readonly int DURATION_FOCUS_OUT = 500;
        internal static readonly int DURATION_DIM_IN = DURATION_20FRAME;
        internal static readonly int DURATION_DIM_OUT = DURATION_20FRAME;
        internal static readonly int DURATION_FADE_IN_01 = 300;
        internal static readonly int DURATION_FADE_OUT_01 = 300;
        internal static readonly int DURATION_FADE_IN_02 = 200;
        internal static readonly int DURATION_FADE_OUT_02 = 200;
        internal static readonly int DURATION_SELECT_IN = 850;
        internal static readonly int DURATION_SELECT_OUT = 1100;
        internal static readonly int DURATION_PRESS = 150;
        internal static readonly int DURATION_RELEASE = 350;
        internal static readonly int DURATION_HIDE = DURATION_20FRAME;
        internal static readonly int DURATION_APPEAR_01 = 500;
        internal static readonly int DURATION_APPEAR_02 = 500;
        internal static readonly int DURATION_APPEAR_SCALE = 700;
        internal static readonly int DURATION_APPEAR_OPACITY = DURATION_20FRAME;
        internal static readonly int DURATION_DISAPPEAR_01 = 500;
        internal static readonly int DURATION_DISAPPEAR_02 = 500;
        internal static readonly int DURATION_DISAPPEAR = 500;
        internal static readonly int DURATION_SPINCONTROL_PRESS = 117;
        internal static readonly int DURATION_SPINCONTROL_RELEASE = 217;
        internal static readonly int DURATION_PARALLAX_MOTION = 850;
        internal static readonly int DURATION_BASIC_01 = 300;
        internal static readonly int DURATION_DEFAULT_SCROLL = 120;
        internal static readonly int SCROOL_TIMER_RATIO = 20;
        internal static readonly float LONGPRESS_SCROOL_SPPED = 1.6f;
        internal static readonly float REPEAT_KEY_DELAY = 0.350f;
        internal static readonly float REPEAT_KEY_INTERVAL = DURATION_DEFAULT_SCROLL * 0.001f;
        internal static readonly float SCALE_ORIGINAL = 1.0f;
        internal static readonly float SCALE_APPEAR = 0.925f;
        internal static readonly float SCALE_APPEAR_01 = 0.95f;
        internal static readonly float SCALE_APPEAR_03 = 0.85f;
        internal static readonly float SCALE_DISAPPEAR_01 = 0.95f;
        internal static readonly float SCALE_ENLARGE = 1.2f;

        internal static float SCALE_FOCUS_IN => UIConfig.SupportPointingMode == UIConfig.PointingMode.SupportTouchOnly ? 1.0f : 1.08f;
        internal static float SCALE_IMAGE_FOCUS_IN => UIConfig.SupportPointingMode == UIConfig.PointingMode.SupportTouchOnly ? 1.0f : 1.2f;
        internal static float SCALE_SELECT => UIConfig.SupportPointingMode == UIConfig.PointingMode.SupportTouchOnly ? 1.0f : 1.04f;

        internal static readonly float SCALE_PRESS = 0.97f;
        internal static readonly float SCALE_PRESS_NORMAL = 0.93f;
        internal static readonly float SCALE_RELEASE = SCALE_ORIGINAL;
        internal static readonly float SCALE_HIDE = 0.01f;
        internal static readonly float SCALE_REMIND_S = 0.9f;
        internal static readonly float SCALE_REMIND_L = 0.95f;

        internal static readonly float NORMAL_POPUP_SCALE_VALUE_PORTRAIT = 1.0f;
        internal static readonly float ENLARGE_POPUP_SCALE_VALUE_PORTRAIT = 1.08f;

        internal static readonly float NORMAL_POPUP_SCALE_VALUE_LANDSCAPE = 1.0f;
        internal static readonly float ENLARGE_POPUP_SCALE_VALUE_LANDSCAPE = 1.2f;

        internal static readonly float OPACITY_ORIGINAL = 1.0f;
        internal static readonly float OPACITY_HIDE = 0.0f;
        internal static readonly float OPACITY_DIM = 0.4f;
        internal static readonly float OPACITY_PRESSED = 0.5f;

        internal static readonly float SHADOW_MARGIN = 4.0f;

        internal static readonly float ORIGIN_POSITIONX = 0.0f;
        internal static readonly float ORIGIN_POSITIONY = 0.0f;

        internal static readonly float SPINCONTROL_POSITIONY_OFFSET = 45.0f;

        internal static readonly int[] PickerWidth = { 26, 49, 72, 95, 118 };
        internal static readonly int PickerHeight = 15;
        internal static readonly int SPINCONTROL_MAX_COUNT = 5;
        internal static readonly string HORIZONTAL = "Horizontal";
        internal static readonly string VERTICAL = "Vertical";

        internal const uint BORDER_DISAPPEARS_TIME = 5000;

        internal static readonly uint DOUBLETAP_TIMEOUT = 250;
        internal static readonly uint MINIMUM_LONGPRESS_HOLDTIME = 1000;

        //Divider Shader
        internal const string DIVIDER_VERTEX_SHADER = @"
            precision highp float;
            attribute vec2 aPosition;
            varying vec2 vTexCoord;
            uniform mat4 uMvpMatrix;
            uniform vec3 uSize;
            void main()
            {
                highp vec4 vertexPosition = vec4(aPosition, 0.0, 1.0);
                vertexPosition.xyz *= uSize;
                vertexPosition = uMvpMatrix * vertexPosition;
                vTexCoord = aPosition + vec2(0.5);
                gl_Position = vertexPosition;
            }";

        internal const string DIVIDER_FRAGMENT_SHADERV = @"
            precision highp float;
            varying  vec2 vTexCoord;
            uniform  vec4 uColor;
            uniform  vec4 uColor0;
            uniform  vec4 uColor1;

            void main()
            {
                float value = step(0.5, vTexCoord.x);
                gl_FragColor = mix(uColor0, uColor1, value) * uColor;
            }";

        internal const string DIVIDER_FRAGMENT_SHADERH = @"
            precision highp float;
            varying   vec2 vTexCoord;
            uniform  vec4 uColor;
            uniform   vec4 uColor0;
            uniform   vec4 uColor1;

            void main()
            {
                float value = step(0.5, vTexCoord.y);
                gl_FragColor = mix(uColor0, uColor1, value) * uColor;
            }";


        #endregion

    }
}