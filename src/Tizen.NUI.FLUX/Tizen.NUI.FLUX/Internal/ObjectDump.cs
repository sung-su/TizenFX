/// @file ObjectDump.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 10.10.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2022 Samsung Electronics Co., Ltd All Rights Reserved
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
using System.Reflection;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    internal static class ObjectDump
    {
        private delegate void ObjectDumpTriggered();

        private static Interop.Misc.ObjectDumpCallback objectDumpCallback = null;
        private static ObjectDumpTriggered objectDumpTriggered;
        private static StringBuilder fileStringBuilder = null;
        private static StringBuilder propertyStringBuilder = null;

        private static bool isTouchArgumentEnabled = false;

        private const string writeFilePath = "/run/user/5001/nui-objectdump.log";
        private const string inputArgumentTouch = "touch";

        private static int viewTypeInsertIndex = 0;
        private static ViewType currentViewType = ViewType.View;
        private enum ViewType
        {
            View,
            Image,
            Text
        };

        public static void Initialize()
        {
            RegisterNativeCallback();
            objectDumpTriggered += OnObjectDumpTriggered;
            fileStringBuilder = new StringBuilder();
            propertyStringBuilder = new StringBuilder();
        }

        public static void CleanUp()
        {
            objectDumpTriggered -= OnObjectDumpTriggered;
            objectDumpCallback = null;
            fileStringBuilder = null;
            propertyStringBuilder = null;
        }

        private static void RegisterNativeCallback()
        {
            objectDumpCallback = new Interop.Misc.ObjectDumpCallback(OnTriggered);
            Interop.Misc.RegisterNuiObjectdump(objectDumpCallback);
        }

        private static bool OnTriggered(string data)
        {
            CheckInputArgument(data);

            CLog.Info("ObjectDump Start");
            objectDumpTriggered?.Invoke();
            CLog.Info("ObjectDump End");
            return true;
        }

        private static void OnObjectDumpTriggered()
        {
            string dumpResult = ">>>>>>>>>>>>>>>>>>>>>>>>>>>>> START OBJECT DUMP >>>>>>>>>>>>>>>>>>>>>>>>>>>>>";
            CLog.Info(dumpResult);
            fileStringBuilder.Clear()
                .Append(dumpResult)
                .Append(Environment.NewLine);

            TraverseWindow();
            dumpResult = ">>>>>>>>>>>>>>>>>>>>>>>>>>>>> END OBJECT DUMP >>>>>>>>>>>>>>>>>>>>>>>>>>>>>";
            CLog.Info(dumpResult);
            fileStringBuilder.Append(dumpResult);

            try
            {
                global::System.IO.File.WriteAllText(writeFilePath, fileStringBuilder.ToString());
            }
            catch (Exception e)
            {
                CLog.Fatal(e.Message);
            }
        }

        private static void CheckInputArgument(string data)
        {
            char[] delimiterCharacters = { ' ', '\t' };
            string[] arguments = data.Split(delimiterCharacters);

            if (arguments[0].Contains(inputArgumentTouch))
            {
                isTouchArgumentEnabled = true;
            }
        }

        private static void TraverseWindow()
        {
            List<Window> windowList = Application.GetWindowList();

            foreach (Window win in windowList)
            {
                AppendWindowInformation(win);
                for (uint index = 0; index < win.GetLayerCount(); index++)
                {
                    Layer layer = win.GetLayer(index);
                    if (layer != null)
                    {
                        TraverseObject(layer, 1, layer.Visibility);
                    }
                }
            }
        }

        private static void TraverseObject(Animatable obj, int depth, bool parentVisiblity)
        {
            if (obj == null)
            {
                return;
            }

            propertyStringBuilder.Clear();
            AppendDividingLine(depth);

            if (obj is View myView)
            {
                currentViewType = ViewType.View;
                AppendViewCommonInformation(myView, parentVisiblity);

                switch (myView)
                {
                    case TextEditor myTextEditor:
                        AppendTextEditorInformation(myTextEditor);
                        currentViewType = ViewType.Text;
                        break;
                    case TextField myTextField:
                        AppendTextFieldInformation(myTextField);
                        currentViewType = ViewType.Text;
                        break;
                    case TextLabel myTextLabel:
                        AppendTextLabelInformation(myTextLabel);
                        currentViewType = ViewType.Text;
                        break;
                    case ImageView myImageView:
                        AppendImageViewInformation(myImageView);
                        currentViewType = ViewType.Image;
                        break;
                    default:
                        break;
                }

                CheckViewIsFluxComponent(myView);
                AppendViewType();
                AppendNewLine();

                for (uint index = 0; index < myView.ChildCount; index++)
                {
                    TraverseObject(myView.GetChildAt(index), depth + 1, myView.Visibility && parentVisiblity);
                }
            }
            else if (obj is Layer myLayer)
            {
                propertyStringBuilder.AppendFormat("[{0}],", myLayer.GetType().Name);
                AppendNewLine();
                for (uint index = 0; index < myLayer.ChildCount; index++)
                {
                    TraverseObject(myLayer.GetChildAt(index), depth + 1, myLayer.Visibility && parentVisiblity);
                }
            }
            else
            {
                return;
            }
        }

        private static void AppendNewLine()
        {
            CLog.Info(propertyStringBuilder.ToString());
            fileStringBuilder.Append(propertyStringBuilder).Append(Environment.NewLine);
        }

        private static void AppendViewType()
        {
            propertyStringBuilder.Insert(viewTypeInsertIndex, "[Type:" + currentViewType.ToString() + "]");
        }

        private static void AppendDividingLine(int depth)
        {
            for (int index = 0; index < depth; index++)
            {
                propertyStringBuilder.Append("-");
            }
        }

        private static void AppendWindowInformation(Window win)
        {
            fileStringBuilder
                .AppendFormat("[{0}][Window:{1}][X:{2},Y:{3},W:{4},H:{5}]", win.GetResourceID(), win.Title, win.WindowPosition.X, win.WindowPosition.Y, win.WindowSize.Width, win.WindowSize.Height)
                .AppendFormat("[Orientation:{0}]", win.GetCurrentOrientation())
                .AppendFormat("[Parent:{0}]", win.GetParent())
                .AppendFormat("[WindowType:{0}]", win.Type)
                .AppendFormat("[Dpi:{0},{1}]", win.Dpi.X, win.Dpi.Y)
                .AppendFormat("[BorderEnabled:{0}]", win.IsBorderEnabled)
                .AppendFormat("[BGColor:{0},{1},{2},{3}", win.BackgroundColor.R, win.BackgroundColor.G, win.BackgroundColor.B, win.BackgroundColor.A)
                .Append(Environment.NewLine);

            CLog.Info(fileStringBuilder.ToString());
        }

        private static void AppendViewCommonInformation(View view, bool parentVisiblity)
        {
            Vector3 axis = new Vector3();
            Radian angle = new Radian();
            view.Orientation.GetAxisAngle(axis, angle);
            propertyStringBuilder
                .AppendFormat("[{0}]", view.ID)
                .AppendFormat("[{0}]", view.GetType().Name);
            viewTypeInsertIndex = propertyStringBuilder.Length;

            propertyStringBuilder
                .AppendFormat("[X:{0},Y:{1},W:{2},H:{3}]", view.ScreenPosition.X, view.ScreenPosition.Y, view.SizeWidth, view.SizeHeight)
                .AppendFormat("[Focused:{0}]", view.HasFocus().ToString())
                .AppendFormat("[BGColor:{0},{1},{2},{3}]", view.BackgroundColor.R, view.BackgroundColor.G, view.BackgroundColor.B, view.BackgroundColor.A)
                .AppendFormat("[Visible:{0}]", view.Visibility && parentVisiblity)
                .AppendFormat("[Focusable:{0}]", view.Focusable)
                .AppendFormat("[Opacity:{0}]", view.Opacity)
                .AppendFormat("[Scale:{0},{1},{2}]", view.ScaleX, view.ScaleY, view.ScaleZ)
                .AppendFormat("[Orientation:{0},{1},{2}, Radian:{3}]", axis.X, axis.Y, axis.Z, angle.Value)
                .AppendFormat("[ResizePolicy:W:{0},H:{1}", view.WidthResizePolicy, view.HeightResizePolicy)
                .AppendFormat("[OwnPosition:{0},{1}, CurrentSize:{2},{3}, NaturalSize:{4},{5}]", view.PositionX, view.PositionY, view.CurrentSize.Width, view.CurrentSize.Height, view.NaturalSize2D.Width, view.NaturalSize2D.Height)
                .AppendFormat("[InheritPosition:{0}, ", view.InheritPosition)
                .AppendFormat("InheritOrientation:{0}, ", view.InheritOrientation)
                .AppendFormat("InheritScale:{0}]", view.InheritScale)
                .AppendFormat("[ParentOrigin:{0},{1}, ", view.ParentOriginX, view.ParentOriginY)
                .AppendFormat("PivotPoint:{0},{1}, ", view.PivotPointX, view.PivotPointY)
                .AppendFormat("PositionUsesPivotPoint:{0}]", view.PositionUsesPivotPoint);

            if (isTouchArgumentEnabled == true)
            {
                AppendViewTouchInformation(view);
            }
        }

        private static void AppendViewTouchInformation(View view)
        {
            propertyStringBuilder
                .AppendFormat("[Sensitive:{0}]", view.Sensitive)
                .AppendFormat("[IsEnabled:{0}]", view.IsEnabled)
                .AppendFormat("[LeaveRequired:{0}]", view.LeaveRequired)
                .AppendFormat("[GrabTouchAfterLeave:{0}]", view.GrabTouchAfterLeave)
                .AppendFormat("[DisallowInterceptTouchEvent:{0}]", view.DisallowInterceptTouchEvent)
                .AppendFormat("[AllowOnlyOwnTouch:{0}]", view.AllowOnlyOwnTouch)
                .AppendFormat("[EnableControlState:{0}]", view.EnableControlState)
                .AppendFormat("[PropagatableControlStates:{0}]", view.PropagatableControlStates)
                .AppendFormat("[TouchAreaOffset:L:{0},R:{1},B:{2},T:{3}]", view.TouchAreaOffset.Left, view.TouchAreaOffset.Right, view.TouchAreaOffset.Bottom, view.TouchAreaOffset.Top);
        }

        private static void AppendTextLabelInformation(TextLabel textLabel)
        {
            propertyStringBuilder
                .AppendFormat("[FontColor:{0},{1},{2},{3}]", textLabel.TextColor.R, textLabel.TextColor.G, textLabel.TextColor.B, textLabel.TextColor.A)
                .AppendFormat("[{0}, {1}, \"{2}\" ", textLabel.FontFamily, textLabel.PointSize, textLabel.Text)
                .AppendFormat("H:{0}, V:{1}, ", textLabel.HorizontalAlignment.ToString(), textLabel.VerticalAlignment.ToString())
                .AppendFormat("Multiline:{0}, ", textLabel.MultiLine.ToString())
                .AppendFormat("LineSize:{0}, ", textLabel.LineSpacing)
                .AppendFormat("LineStyle:{0}]", textLabel.LineWrapMode.ToString())
                .AppendFormat("[Ellipsis:{0}]", textLabel.Ellipsis)
                .AppendFormat("[EnableMarkup:{0}]", textLabel.EnableMarkup);
            if (textLabel.EnableAutoScroll)
            {
                propertyStringBuilder
                    .AppendFormat("[AutoScrollSpeed:{0}, AutoScrollGap:{1}, AutoScrollLoopCount:{2}]", textLabel.AutoScrollSpeed, textLabel.AutoScrollGap, textLabel.AutoScrollLoopCount);
            }
            propertyStringBuilder
                .AppendFormat("[Unicode:{0}]", PrintUnicode(textLabel.Text));
        }

        private static void AppendTextEditorInformation(TextEditor textEditor)
        {
            propertyStringBuilder
                .AppendFormat("[FontColor:{0},{1},{2},{3}]", textEditor.TextColor.R, textEditor.TextColor.G, textEditor.TextColor.B, textEditor.TextColor.A)
                .AppendFormat("[{0}, {1}, \"Text : {2}\" \" / PlaceholderText : \"{3}\" ", textEditor.FontFamily, textEditor.PointSize, textEditor.Text, textEditor.PlaceholderText)
                .AppendFormat("H:{0}, ", textEditor.HorizontalAlignment.ToString())
                .AppendFormat("Multiline:{0}, ", "true") //True by default, immutable.
                .AppendFormat("LineSize:{0}, ", textEditor.LineSpacing)
                .AppendFormat("LineStyle:{0}]", textEditor.LineWrapMode.ToString())
                .AppendFormat("[Unicode:{0}]", PrintUnicode(textEditor.Text));
        }

        private static void AppendTextFieldInformation(TextField textField)
        {
            propertyStringBuilder
                .AppendFormat("[FontColor:{0},{1},{2},{3}]", textField.TextColor.R, textField.TextColor.G, textField.TextColor.B, textField.TextColor.A)
                .AppendFormat("[{0}, {1}, \"Text : {2}\" \" / PlaceholderText : \"{3}\" ", textField.FontFamily, textField.PointSize, textField.Text, textField.PlaceholderText)
                .AppendFormat("H:{0}, V:{1}, ", textField.HorizontalAlignment.ToString(), textField.VerticalAlignment.ToString())
                .AppendFormat("Multiline:{0}]", "False")  //False by default, immutable.                        
                .AppendFormat("[Unicode:{0}]", PrintUnicode(textField.Text));
        }

        private static void AppendImageViewInformation(ImageView imageView)
        {
            propertyStringBuilder
                .AppendFormat("[ResourceUrl:\"{0}\"]", imageView.ResourceUrl)
                .AppendFormat("[BGImg:\"{0}\"]", imageView.BackgroundImage)
                .AppendFormat("[AlphaMaskUrl:\"{0}\"]", imageView.AlphaMaskURL)
                .AppendFormat("[SynchronousLoading:{0}]", imageView.SynchronousLoading.ToString())
                .AppendFormat("[ReleasePolicy:{0}]", imageView.ReleasePolicy);
        }

        private static void AppendImageBoxInformation(View imageBox)
        {
            AppendGetProperty(imageBox, "String", "ResourceUrl", "ResourceUrl", "[", ":\"", "\"]");
            AppendGetProperty(imageBox, "String", "BackgroundImage", "BGImg", "[", ":\"", "\"]");
            AppendGetProperty(imageBox, "String", "AlphaMaskUrl", "AlphaMaskUrl", "[", ":\"", "\"]");
            AppendGetProperty(imageBox, "Boolean", "SynchronousLoading", "SynchronousLoading");
            AppendGetProperty(imageBox, "ScaleTypes", "ScaleType", "ScaleType");
            AppendGetProperty(imageBox, "Boolean", "PreMultipliedAlpha", "PreMultipliedAlpha");
            AppendGetProperty(imageBox, "Boolean", "UseOverlayColor", "UseOverlayColor");
            AppendGetProperty(imageBox, "ReleasePolicyTypes", "ReleasePolicyType", "ReleasePolicyType");
            AppendGetProperty(imageBox, "Boolean", "CropToMask", "CropToMask");

            AppendGetProperty(imageBox, "StateMachine", "State", "CurrentState");
        }

        private static void AppendTextBoxInformation(View textBox)
        {
            PropertyInfo propertyInfo = textBox.GetType().GetProperty("TextColor");
            if (propertyInfo != null)
            {
                Color textColor = propertyInfo.GetValue(textBox) as Color;
                if (textColor != null)
                {
                    propertyStringBuilder.AppendFormat("[FontColor:{0},{1},{2},{3}]", textColor.R, textColor.G, textColor.B, textColor.A);
                }
            }

            AppendGetProperty(textBox, "String", "TextWeight", "", "[", "", ",");
            AppendGetProperty(textBox, "Int32", "PointSize", "", " ", "", ",");
            AppendGetProperty(textBox, "String", "Text", "", " \"", "", "\"");
            AppendGetProperty(textBox, "HorizontalAlignment", "HorizontalAlign", "H", " ", ":", ",");
            AppendGetProperty(textBox, "VerticalAlignment", "VerticalAlign", "V", " ", ":", ",");
            AppendGetProperty(textBox, "Boolean", "MultiLine", "Multiline", " ", ":", ",");
            AppendGetProperty(textBox, "Int32", "LineGap", "LineSize", " ", ":", ",");
            AppendGetProperty(textBox, "LineWrapModes", "LineWrapMode", "LineStyle", " ", ":", "]");

            AppendGetProperty(textBox, "Int32", "LineCount", "LineCount");
            AppendGetProperty(textBox, "Int32", "OverflowOption", "OverflowOption");
            AppendGetProperty(textBox, "Boolean", "EnableMarkup", "EnableMarkup");
            AppendGetProperty(textBox, "Boolean", "AutoHorizontalAlignmentEnabled", "AutoHorizontalAlignmentEnabled");

            AppendGetProperty(textBox, "StateMachine", "State", "CurrentState");
        }

        private static void AppendGetProperty(dynamic view, string propertyType, string property, string propertyPrintFormat, string preSeparator = "[", string midSeparator = ":", string postSeparator = "]")
        {
            Type componentType = view.GetType();
            PropertyInfo propertyInfo = componentType.GetProperties().Where(propertyInfo => propertyInfo.Name == property && propertyInfo.PropertyType.Name == propertyType).FirstOrDefault();

            if (propertyInfo != null)
            {
                if (propertyInfo.Name == "State")
                {
                    dynamic stateMachine = propertyInfo.GetValue(view);
                    propertyStringBuilder.Append(preSeparator + propertyPrintFormat + midSeparator + stateMachine.CurrentState + postSeparator);
                }
                else
                {
                    propertyStringBuilder.Append(preSeparator + propertyPrintFormat + midSeparator + propertyInfo.GetValue(view) + postSeparator);
                }
            }
        }

        private static void CheckViewIsFluxComponent(View view)
        {
            string type = GetFluxComponentBaseType(view.GetType());
            if (type == "ImageBox")
            {
                AppendImageBoxInformation(view);
                currentViewType = ViewType.Image;
            }
            else if (type == "TextBox")
            {
                AppendTextBoxInformation(view);
                currentViewType = ViewType.Text;
            }
            else
            {
                return;
            }
        }

        private static string GetFluxComponentBaseType(this Type type)
        {
            while (type != null)
            {
                if (type.Name.Equals("ImageBox"))
                {
                    return "ImageBox";
                }
                else if (type.Name.Equals("TextBox"))
                {
                    return "TextBox";
                }
                type = type.BaseType;
            }
            return null;
        }

        private static string PrintUnicode(string text)
        {
            int i = 0;
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(text);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in bytes)
            {
                stringBuilder.AppendFormat(i++ % 2 == 0 ? "0x{0:X2}" : "{0:X2} ", b);
            }
            return stringBuilder.ToString();
        }
    }
}
