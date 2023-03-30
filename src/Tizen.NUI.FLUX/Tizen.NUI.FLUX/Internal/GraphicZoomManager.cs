/// @file GraphicZoomManager.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 8.8.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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
using Tizen.NUI.BaseComponents;
//using Tizen.TV.System;

namespace Tizen.NUI.FLUX
{
    internal static class GraphicZoomManager
    {
        //TODO: Need to check this vconf key is available or not across all products
        private const string vconfKeyIntGraphicZoomEnabled = "memory/menu/system/accessibility/graphiczoom";
        private const string vconfKeyStringFocusedObjectGeometry = "memory/menu/system/accessibility/focused-object-geometry";
        private const string geometryContentFormat =
            "{{\n" +
            "\t\"x\":{0},\n" +
            "\t\"y\":{1},\n" +
            "\t\"w\":{2},\n" +
            "\t\"h\":{3}\n" +
            "}}";

        private const int graphicZoomAppResolutionWidth = 1920;
        private const int graphicZoomAppResolutionHeight = 1080;

        private static View observingFocusIndicator = null;
        private static PropertyNotification positionChangedPropertyNotification = null;

        private static bool isGeometryInformationUpdateRequired = false;
        private static bool isInitialized = false;
        private static bool isGraphicZoomEnabled = false;
        private static bool hasWindowFocus = true;

        private static float widthScalingRatio = 1.0f;
        private static float heightScalingRatio = 1.0f;

        private static bool IsGraphicZoomEnabled
        {
            get => isGraphicZoomEnabled;
            set
            {
                CLog.Debug("IsGraphicZoomEnabled: %d1", d1: Convert.ToInt32(value));
                isGraphicZoomEnabled = value;

                if (value)
                {
                    SubscribeFocusChangedEvent();
                }
                else
                {
                    UnsubscribeFocusChangedEvent();
                }
            }
        }

        public static void Initialize()
        {
            if (SystemConfigUtil.Instance.PlatformSmartType == SystemConfigUtil.PlatformSmartTypes.ENTRY)
            {
                return;
            }

            CLog.Debug("Initialize GraphicZoom");
            InitializeFocusIndicator();

            //Vconf.NotifyKeyChanged(vconfKeyIntGraphicZoomEnabled, OnVconfValueChanged, IntPtr.Zero);
            //Vconf.GetInt(vconfKeyIntGraphicZoomEnabled, out int vconfValue);
            //IsGraphicZoomEnabled = (vconfValue == 1);

            FocusManager.Instance.FocusChanged += OnFocusChanged;
            Window.Instance.Resized += OnWindowResized;  // for resizing of partial window. not for rotation.
            Window.Instance.FocusChanged += OnWindowFocusChanged;

            SetScalingRatio(Window.Instance.WindowSize);

            isInitialized = true;
        }

        public static void CleanUp()
        {
            if (isInitialized == false)
            {
                return;
            }

            CLog.Debug("CleanUp GraphicZoom");
            UnsubscribeFocusChangedEvent();
            //Vconf.IgnoreKeyChanged(vconfKeyIntGraphicZoomEnabled, OnVconfValueChanged);
            observingFocusIndicator = null;
            FocusManager.Instance.FocusChanged -= OnFocusChanged;
            Window.Instance.Resized -= OnWindowResized;
            Window.Instance.FocusChanged -= OnWindowFocusChanged;

            isInitialized = false;
            isGeometryInformationUpdateRequired = false;
        }

        private static void InitializeFocusIndicator()
        {
            if (FocusManager.Instance.FocusIndicator == null)
            {
                CLog.Error("FocusIndicator is null. Attach new one");
                FocusManager.Instance.FocusIndicator = new View();
            }

            observingFocusIndicator = FocusManager.Instance.FocusIndicator;
        }

        private static void OnFocusChanged(object sender, FocusManager.FocusChangedEventArgs e)
        {
            isGeometryInformationUpdateRequired = true;

            if (FocusManager.Instance.FocusIndicator != observingFocusIndicator)
            {
                CLog.Error("FocusIndicator was changed!!");
                RecoverFocusIndicator();
                if (IsGraphicZoomEnabled)
                {
                    WriteGeometryInfo(e.Current); // need to update manually in this case
                }
            }
        }

        private static void OnWindowFocusChanged(object sender, Window.FocusChangedEventArgs e)
        {
            CLog.Info("IsWindowFocusGained: %d1", d1: Convert.ToInt32(e.FocusGained));
            if (e.FocusGained && !hasWindowFocus)
            {
                CLog.Info($"Update new geometry info");
                isGeometryInformationUpdateRequired = true;
                WriteGeometryInfo(FocusManager.Instance.GetCurrentFocusView());
            }

            hasWindowFocus = e.FocusGained;
        }

        private static void OnWindowResized(object sender, Window.ResizedEventArgs e)
        {
            CLog.Debug("WindowResized, Size: [%d1 x %d2], Position: [%d3 x %d4]"
                , d1: e.WindowSize.Width
                , d2: e.WindowSize.Height
                , d3: Window.Instance.WindowPosition.X
                , d4: Window.Instance.WindowPosition.Y
                );
            isGeometryInformationUpdateRequired = true;
            SetScalingRatio(e.WindowSize);
        }

        private static void SetScalingRatio(Size2D windowSize)
        {
            widthScalingRatio = (windowSize.Width > graphicZoomAppResolutionWidth) ? ((float)graphicZoomAppResolutionWidth / windowSize.Width) : 1f;
            heightScalingRatio = (windowSize.Height > graphicZoomAppResolutionHeight) ? ((float)graphicZoomAppResolutionHeight / windowSize.Height) : 1f;
            CLog.Info("ScalingRatio: [%f1, %f2]", f1: widthScalingRatio, f2: heightScalingRatio);
        }

        private static void RecoverFocusIndicator()
        {
            bool isPropertyNotificationEnabled = positionChangedPropertyNotification != null;

            if (isPropertyNotificationEnabled)
            {
                observingFocusIndicator.RemovePropertyNotification(positionChangedPropertyNotification);
                positionChangedPropertyNotification.Notified -= OnFocusIndicatorPositionChanged;
                positionChangedPropertyNotification.Dispose();
                positionChangedPropertyNotification = null;
            }

            if (FocusManager.Instance.FocusIndicator == null)
            {
                InitializeFocusIndicator();
            }
            else
            {
                observingFocusIndicator = FocusManager.Instance.FocusIndicator; // user's indicator
            }

            if (isPropertyNotificationEnabled)
            {
                positionChangedPropertyNotification = observingFocusIndicator.AddPropertyNotification("WorldPosition", PropertyCondition.Step(1));
                positionChangedPropertyNotification.Notified += OnFocusIndicatorPositionChanged;
            }
        }

        private static void OnVconfValueChanged(IntPtr node, IntPtr userData)
        {
            //VconfKeyNode keyNode = Marshal.PtrToStructure<VconfKeyNode>(node);
            //string key = Marshal.PtrToStringAnsi(keyNode.keyname);

            //if (key == vconfKeyIntGraphicZoomEnabled)
            //{
            //    IsGraphicZoomEnabled = (keyNode.intValue == 1);
            //}
        }

        private static void SubscribeFocusChangedEvent()
        {
            if (positionChangedPropertyNotification == null)
            {
                positionChangedPropertyNotification = observingFocusIndicator.AddPropertyNotification("WorldPosition", PropertyCondition.Step(1));
                positionChangedPropertyNotification.Notified += OnFocusIndicatorPositionChanged;
                CLog.Debug("Subscribed");
            }
        }

        private static void UnsubscribeFocusChangedEvent()
        {
            if (positionChangedPropertyNotification == null)
            {
                return;
            }

            observingFocusIndicator.RemovePropertyNotification(positionChangedPropertyNotification);
            positionChangedPropertyNotification.Notified -= OnFocusIndicatorPositionChanged;
            positionChangedPropertyNotification.Dispose();
            positionChangedPropertyNotification = null;
            CLog.Debug("Unsubscribed");
        }

        private static void OnFocusIndicatorPositionChanged(object source, PropertyNotification.NotifyEventArgs e)
        {
            CLog.Debug("OnChanged");
            WriteGeometryInfo(FocusManager.Instance.GetCurrentFocusView());
        }

        private static void WriteGeometryInfo(View view)
        {
            if (isGeometryInformationUpdateRequired)
            {
                if (view == null)
                {
                    CLog.Error("Could not write the geometry info. View is null");
                    return;
                }

                Vector4 screenExtents = TransformationUtil.GetScreenExtents(view);

                int x = (int)(screenExtents.X * widthScalingRatio);
                int y = (int)(screenExtents.Y * heightScalingRatio);
                int w = (int)(view.Size.Width * view.WorldScale.X * widthScalingRatio);
                int h = (int)(view.Size.Height * view.WorldScale.Y * heightScalingRatio);

                string formattedResult = string.Format(geometryContentFormat, x, y, w, h);
                CLog.Debug(formattedResult);
                //Vconf.SetString(vconfKeyStringFocusedObjectGeometry, formattedResult);
                isGeometryInformationUpdateRequired = false;
            }
        }
    }
}