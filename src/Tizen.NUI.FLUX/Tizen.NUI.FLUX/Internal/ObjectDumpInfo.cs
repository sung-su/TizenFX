using System;
using System.Collections.Generic;
using System.ComponentModel;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// This class is dedicated for Voice Interaction Framework Service.
    /// Do not use for other purposes without permission.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class ObjectDumpInfo
    {
        public class FocusableObject
        {
            public FocusableObject() { }

            public int ID { set; get; }

            public uint TextID { set; get; }

            public int LayerID { set; get; }

            public bool Focused { set; get; }

            public (int X, int Y) Position { set; get; }

            public (int W, int H) Size { set; get; }

            public string Title { set; get; }

            public bool Visible { set; get; }

            public bool Editable { set; get; } = false;
        }

        private static readonly List<FocusableObject> focusableObjectInfoList = new List<FocusableObject>();

        public static IReadOnlyList<FocusableObject> GetObjectDumpInfo()
        {
            focusableObjectInfoList.Clear();

            TraverseWindow();

            return focusableObjectInfoList;
        }

        private static void TraverseWindow()
        {
            List<Window> windowList = Application.GetWindowList();

            if (windowList.Count > 0)
            {
                foreach (Window win in windowList)
                {
                    TraverseSceneTree(win);
                }
            }
        }

        private static void TraverseSceneTree(Window win)
        {
            uint layerCount = win.GetLayerCount();
            for (uint index = 0; index < layerCount; index++)
            {
                Layer layer = win.GetLayer(index);

                if (layer != null)
                {
                    TraverseObject(layer, (int)index, layer.Visibility);
                }
            }
        }

        private static bool IsValidTextObject(View obj)
        {
            if (!(obj is TextField) && !(obj is TextLabel) && !(obj is TextEditor))
            {
                return false;
            }

            bool isValidString = true;

            switch (obj)
            {
                case TextLabel txt:
                    isValidString = !string.IsNullOrEmpty(txt.Text);
                    break;
                case TextField txt:
                    isValidString = !string.IsNullOrEmpty(txt.Text);
                    break;
                case TextEditor txt:
                    isValidString = !string.IsNullOrEmpty(txt.Text);
                    break;
            }

            return isValidString;
        }

        private static bool IsValidTextBox(View obj)
        {
            if (!obj.GetType().Name.Equals("TextBox"))
            {
                return false;
            }

            bool isValidString = true;

            dynamic textBox = obj;
            if (textBox.GetType().GetProperty("Text") != null)
            {
                isValidString = !string.IsNullOrEmpty(textBox.Text);
            }

            return isValidString;
        }

        private static void AddToInfoList(FocusableObject info, FocusableObject focusableObject, int layerID)
        {
            info.ID = focusableObject.ID;
            info.Focused = focusableObject.Focused;
            info.LayerID = layerID;
            info.Position = focusableObject.Position;
            info.Size = focusableObject.Size;
            info.Visible = focusableObject.Visible;

            focusableObjectInfoList.Add(info);
        }

        private static void TraverseObject(Animatable obj, int layerID, bool parentVisibility, FocusableObject focusableObject = null)
        {
            if (obj == null)
            {
                return;
            }

            if (obj is View myView)
            {
                //This is to skip NList's group title
                if (myView.Name == "NListTitleArea!!")
                {
                    return;
                }

                if (obj is FluxView myFluxView)
                {
                    bool isFocusableObject = myFluxView.IsComponent && myFluxView.Focusable;

                    if (isFocusableObject && focusableObject == null)
                    {
                        focusableObject = new FocusableObject()
                        {
                            ID = (int)myFluxView.ID,
                            Focused = myFluxView.HasFocus(),
                            Position = ((int)myFluxView.ScreenPosition.X, (int)myFluxView.ScreenPosition.Y),
                            Size = (myFluxView.CurrentSize.Width, myFluxView.CurrentSize.Height),
                            Visible = (parentVisibility && myFluxView.Visibility)
                        };
                    }
                }

                if (focusableObject != null)
                {
                    FocusableObject info = new FocusableObject();

                    if (IsValidTextBox(myView))
                    {
                        dynamic textBox = myView;
                        info.Title = textBox.Text;
                        info.TextID = textBox.ID;
                        AddToInfoList(info, focusableObject, layerID);
                    }

                    if (IsValidTextObject(myView))
                    {
                        switch (myView)
                        {
                            case TextEditor myTextEditor:
                                info.Title = myTextEditor.Text;
                                info.TextID = myTextEditor.ID;
                                info.Editable = true;
                                break;
                            case TextField myTextField:
                                info.Title = myTextField.Text;
                                info.TextID = myTextField.ID;
                                info.Editable = true;
                                break;
                            case TextLabel myTextLabel:
                                info.TextID = myTextLabel.ID;
                                info.Title = myTextLabel.Text;
                                break;
                            default:
                                throw new InvalidOperationException("myView is not textType");
                        }
                        AddToInfoList(info, focusableObject, layerID);
                    }
                }

                uint childCount = myView.ChildCount;
                for (uint index = 0; index < childCount; index++)
                {
                    TraverseObject(myView.GetChildAt(index), layerID, myView.Visibility && parentVisibility, focusableObject);
                }

            }
            else if (obj is Layer myLayer)
            {
                uint childCount = myLayer.ChildCount;
                for (uint index = 0; index < childCount; index++)
                {
                    TraverseObject(myLayer.GetChildAt(index), layerID, myLayer.Visibility && parentVisibility);
                }
            }
            else
            {
                //A NUI Object that cannot have child objects.
                return;
            }
        }
    }
}
