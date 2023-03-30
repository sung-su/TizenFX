/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file Template.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 8.8.0 </version>
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
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.Binding;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// This is Template Class 
    /// <code>
    ///  template = new Template(Window.Instance.GetDefaultLayer(), "BigImage1");
    ///  bodycomponent = new TextBox
    ///  {
    ///     Name = "bodyComp",
    ///     Text = "body",
    ///     BackgroundColor = Color.Yellow,
    ///   };
    ///   template.AddItim("BODY", bodycomponent);
    /// </code>
    /// </summary>
    public class Template : Element
    {
        #region public Method
        /// <summary>
        /// Constructor to instantiate the Template class.
        /// When you use this API, your UI is added in default Window.
        /// </summary>
        /// <param name="layer">Enter Window Layer(ex.Window.Instance.GetDefaultLayer() )</param>
        /// <param name="templateType">Template Name</param>
        public Template(Layer layer, string templateType)
        {
            presetName = templateType;
            Initialize(Window.Instance, layer);
        }
        /// <summary>
        /// Constructor to instantiate the Template class.
        /// If you want to your UI in subWindow,  you should add window Instance.
        /// </summary>
        /// <version>9.9.0</version>
        /// <param name="window">If you want to it in subWindow, you should add window Instance </param>
        /// <param name="layer">Enter Window Layer(ex.subwindow.GetDefaultLayer() )</param>
        /// <param name="templateType">Template Name</param>
        public Template(Window window, Layer layer, string templateType)
        {
            presetName = templateType;
            Initialize(window, layer);
        }

        /// <summary>
        /// You can update all areas at once.
        /// If you don't call it, your UI is broken.
        /// Recommand : You add items in all areas and call this function.
        /// </summary>
        public void Update()
        {
            if (GetAreaLayout(RootLayout) is Layout root)
            {
                UpdateButtonsPadding();
                ////temporary code for update to start portrait mode
                root.UpdateLayout();
                root.UnitPositionX = 0;
                root.UnitPositionY = 0;
                if (currentWindow.Size != null)
                {
                    root.UnitSizeWidth = DisplayMetrics.Instance.PixelToUnit(currentWindow.Size.Width);
                    root.UnitSizeHeight = DisplayMetrics.Instance.PixelToUnit(currentWindow.Size.Height);
                }
                ////
                root.UpdateLayout();
            }
        }
        /// <summary>
        /// You can update all areas at once.
        /// </summary>
        /// supporting xaml
        public void UpdateLayout()
        {
            Update();
        }

        /// <summary>
        /// After you make it in all area, you can call this function each area.
        /// It is updated only for the area you called.
        /// Recommand : You add and remove items in one area and call this function
        /// </summary>
        /// <param name="area">template Area name</param>
        /// <code> 
        /// template.Update("BODY")
        /// </code>
        public void Update(string area)
        {
            if (GetAreaLayout(area) is Layout layout)
            {
                UpdateButtonsPadding();
                layout.UpdateLayout();
            }
        }
        /// <summary>
        /// Cleaning up managed and unmanaged resources
        /// </summary>
        public void Dispose()
        {
            DestroyTree();
            currentWindow.Resized -= WindowResized;

            elements.Clear();
            elements = null;

            elementName.Clear();
            elementName = null;
        }

        /// <summary>
        /// Cleaning up managed and unmanaged resources
        /// </summary>
        /// supporting xaml
        public virtual void DestroyElements()
        {
            Dispose();
        }

        /// <summary>
        /// Change layouttype about AreaLayout.
        /// You should call this function at initial time, before the template.Update function is called.
        /// </summary>
        /// <code>
        /// template.ChangeLayoutTypes("BODY", LayoutTypes.FlexH);
        /// </code>
        /// <param name="name">Template Area name </param>
        /// <param name="layoutTypes">Change layout type</param>
        public void ChangeLayoutTypes(string name, LayoutTypes layoutTypes)
        {
            if (GetAreaLayout(name) is Layout parent)
            {
                parent.Type = layoutTypes;
                parent.UpdateLayout();
            }
        }

        /// <summary>
        /// Add Component in Template Area.
        /// If you add item wrong area, your UI is abnormal.
        /// </summary>
        /// <code>
        /// template.AddItem("BODY", bodyComponent);
        /// </code>
        /// <param name="name">Template Area name </param>
        /// <param name="component">Add Component</param>
        public void AddItem(string name, ComponentBase component)
        {
            if (GetAreaLayout(name) is Layout parent)
            {
                parent.Show();
                parent.Add(component);
            }
        }

        /// <summary>
        /// Remove Component in Template Area.
        /// This api remove items from template. We don't care lifecycle component that you set.
        /// </summary>
        /// <code>
        /// template.RemoveItem("BODY", bodyComponent);
        /// </code>
        /// <param name="name">Template Area name </param>
        /// <param name="component">remove Component</param>
        public void RemoveItem(string name, ComponentBase component)
        {
            if (GetAreaLayout(name) is Layout parent)
            {
                parent.Remove(component);
            }
        }

        /// <summary>
        /// Get the element(Layout) in area of template.
        /// </summary>
        /// <param name="eName">The area's name.</param>
        /// <returns>Element object of specific name</returns>
        public Layout GetAreaLayout(string eName)
        {
            if (elementName == null || elements == null || eName == null)
            {
                return null;
            }

            Layout layout = null;
            if (elementName.TryGetValue(eName, out string name) && elements.TryGetValue(name, out ComponentBase componentBase))
            {
                if (componentBase is Layout componentLayout)
                {
                    layout = componentLayout;
                }
            }
            else
            {
                FluxLogger.FatalP("Can't find the Area named: %s1", s1: eName);
            }
            return layout;
        }

        /// <summary>
        /// If you want to show this template, you call it. Default is visible.
        /// </summary>
        public void Show()
        {
            if (GetAreaLayout(RootLayout) is Layout root)
            {
                root.Show();
            }
        }

        /// <summary>
        /// If you want not to show this template, you call it. Default is visible.
        /// </summary>
        public void Hide()
        {
            if (GetAreaLayout(RootLayout) is Layout root)
            {
                root.Hide();
            }
        }

        #endregion public Method
        #region public Property
        /// <summary>
        /// Enable / Disable default background image of principle
        /// This default vaule is true.
        /// If you want to not use it, set the value is false.
        /// </summary>
        public bool BackgroundEnabled
        {
            get => backgroundEnabled;
            set
            {
                backgroundEnabled = value;
                if (GetAreaLayout(RootLayout) is RootLayout rootlayout)
                {
                    rootlayout.BackgroundEnabled = backgroundEnabled;
                }
            }
        }

        /// <summary>
        /// You can change Background Color to use ColorChip.
        /// </summary>
        /// <code>
        /// template.ThemeBackgroundColorChip = "CC_BG220";
        /// </code>
        public string ThemeBackgroundColorChip
        {
            get => themeBackgroundColorChip;
            set
            {
                themeBackgroundColorChip = value;
                if (GetAreaLayout(RootLayout) is RootLayout rootlayout)
                {
                    rootlayout.ThemeBackgroundColorChip = themeBackgroundColorChip;
                }
            }
        }

        /// <summary>
        /// Add Component in Hat Area.
        /// </summary>
        /// supporting xaml
        /// <version>10.10.0</version>
        public List<ComponentBase> HatAreaContent
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                foreach (ComponentBase component in value)
                {
                    AddItem("HAT", component);
                }
            }
        }

        /// <summary>
        /// Add Component in Top Area.
        /// </summary>
        /// supporting xaml
        /// <version>10.10.0</version>
        public List<ComponentBase> TopAreaContent
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                foreach (ComponentBase component in value)
                {
                    AddItem("TOP", component);
                }
            }
        }

        /// <summary>
        /// Add Component in Neck Area.
        /// </summary>
        /// supporting xaml
        /// <version>10.10.0</version>
        public List<ComponentBase> NeckAreaContent
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                foreach (ComponentBase component in value)
                {
                    AddItem("NECK", component);
                }
            }
        }

        /// <summary>
        /// Add Component in SKIN Area.
        /// </summary>
        /// supporting xaml
        /// <version>10.10.0</version>
        public List<ComponentBase> SkinAreaContent
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                foreach (ComponentBase component in value)
                {
                    AddItem("SKIN", component);
                }
            }
        }

        /// <summary>
        /// Add Component in Body Area.
        /// </summary>
        /// supporting xaml
        /// <version>10.10.0</version>
        public List<ComponentBase> BodyAreaContent
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                foreach (ComponentBase component in value)
                {
                    AddItem("BODY", component);
                }
            }
        }

        /// <summary>
        /// Add Component in Foot Area.
        /// </summary>
        /// supporting xaml
        /// <version>10.10.0</version>
        public List<ComponentBase> FootAreaContent
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                foreach (ComponentBase component in value)
                {
                    AddItem("FOOT", component);
                }
            }
        }

        /// <summary>
        /// Add Component in Buttons Area.
        /// </summary>
        /// supporting xaml
        /// <version>10.10.0</version>
        public List<ComponentBase> ButtonsAreaContent
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                foreach (ComponentBase component in value)
                {
                    AddItem("BUTTONS", component);
                }
            }
        }

        /// <summary>
        /// Set PropertyMap for Hat
        /// </summary>
        /// supporting xaml
        /// <version> 10.10.0 </version>
        public Dictionary<string, object> HatPropertyMap
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                SetLayoutProperties("HAT", value);
            }
        }

        /// <summary>
        /// Set PropertyMap for Top
        /// </summary>
        /// supporting xaml
        /// <version> 10.10.0 </version>
        public Dictionary<string, object> TopPropertyMap
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                SetLayoutProperties("TOP", value);
            }
        }

        /// <summary>
        /// Set PropertyMap for Body
        /// </summary>
        /// supporting xaml
        /// <version> 10.10.0 </version>
        public Dictionary<string, object> BodyPropertyMap
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                SetLayoutProperties("BODY", value);
            }
        }

        /// <summary>
        /// Set PropertyMap for Neck
        /// </summary>
        /// supporting xaml
        /// <version> 10.10.0 </version>
        public Dictionary<string, object> NeckPropertyMap
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                SetLayoutProperties("NECK", value);
            }
        }

        /// <summary>
        /// Set PropertyMap for Skin
        /// </summary>
        /// supporting xaml
        /// <version> 10.10.0 </version>
        public Dictionary<string, object> SkinPropertyMap
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                SetLayoutProperties("SKIN", value);
            }
        }

        /// <summary>
        /// Set PropertyMap for Buttons
        /// </summary>
        /// supporting xaml
        /// <version> 10.10.0 </version>
        public Dictionary<string, object> ButtonsPropertyMap
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                SetLayoutProperties("BUTTONS", value);
            }
        }

        /// <summary>
        /// Set PropertyMap for Foot
        /// </summary>
        /// supporting xaml
        /// <version> 10.10.0 </version>
        public Dictionary<string, object> FootPropertyMap
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                SetLayoutProperties("FOOT", value);
            }
        }

        /// <summary>
        /// Set RootLayout ItemGap.
        /// </summary>
        /// supporting xaml
        /// <version>10.10.0</version>
        public int RootLayoutItemGap
        {
            set
            {
                if (GetAreaLayout("RootLayout") is Layout layout)
                {
                    layout.LayoutParam.ItemGap = value;
                }
            }
        }

        #endregion public Property
        #region private Method
        private void SetLayoutProperties(string elementName, Dictionary<string, object> propertyMap)
        {
            if (GetAreaLayout(elementName) is Layout layout)
            {
                foreach (KeyValuePair<string, object> pair in propertyMap)
                {
                    if (pair.Key.StartsWith("LayoutParam."))
                    {
                        string key = pair.Key.Substring(12);
                        layout.LayoutParam.SetProperty(key, pair.Value);
                        propertyMap.Remove(pair.Key);
                    }
                }
                layout.SetProperty(propertyMap);
            }

        }
        private void Initialize(Window window, Layer layer)
        {
            if (window == null)
            {
                FluxLogger.ErrorP("Window is null");
                return;
            }
            if (layer == null)
            {
                FluxLogger.ErrorP("Layer is null");
                return;
            }
            currentWindow = window;
            FluxLogger.InfoP("Window Title :%s1, Layer Name :%s2", s1: currentWindow.Title, s2: layer.Name);
            if (presetName != null)
            {
                if (TemplateManager.GetPreset(presetName) is TemplatePresetBase preset)
                {
                    preset.CreateRootLayout(layer);
                    // deep copy
                    elementName = new Dictionary<string, string>(preset.ElementName);
                    elements = new Dictionary<string, ComponentBase>(preset.elements);
                    CreateTree();

                    preset.Destroy();
                }
            }

            currentWindow.Resized += WindowResized;
            if (HasButtonsLayout() == true)
            {
                if (GetAreaLayout("BUTTONS") is Layout buttons)
                {
                    seroPaddingBottom = buttons.UnitSizeHeight;
                }
                defaultPaddingBottom = 0;
            }
        }

        private void WindowResized(object sender, Window.ResizedEventArgs e)
        {
            Update();
        }
        private void UpdateButtonsPadding()
        {
            FluxLogger.DebugP("WindowResized W: %d1, H:%d2", d1: currentWindow.WindowSize.Width, d2: currentWindow.WindowSize.Height);
            if (HasButtonsLayout() == true)
            {
                if (GetAreaLayout("BUTTONS") is Layout buttons)
                {
                    if (buttons.Visibility == true)
                    {
                        if (currentWindow.WindowSize.Width <= currentWindow.WindowSize.Height)
                        {
                            buttons.LayoutParam.Align = Aligns.TopCenter;
                            if (GetAreaLayout(RootLayout) is Layout root)
                            {
                                root.LayoutParam.Padding.Bottom = seroPaddingBottom;
                            }
                        }
                        else
                        {
                            buttons.LayoutParam.Align = Aligns.Center;
                            if (GetAreaLayout(RootLayout) is Layout root)
                            {
                                root.LayoutParam.Padding.Bottom = defaultPaddingBottom;
                            }
                        }
                    }
                }
            }
        }

        private bool HasButtonsLayout()
        {
            if (presetName.Contains("BigImage1") || presetName.Contains("StackUp1") || presetName.Contains("TType1") ||
                     presetName.Contains("TType2") || presetName.Contains("Studio") || presetName.Contains("Terrace")
                     || presetName.Contains("Window"))
            {
                return true;
            }
            return false;
        }
        private void AddChild(ComponentBase parentComponent, string parentName)
        {
            if (elements == null)
            {
                return;
            }
            for (int i = 0; i < elements.Count; ++i)
            {
                string childName = parentName + "-" + i;

                if (elements.TryGetValue(childName, out ComponentBase addView) && addView != null)
                {
                    FluxLogger.FatalP("<<< add, parent name = %s1, child name = %s2", s1: parentName, s2: childName);
                    parentComponent.Add(addView);
                    AddChild(addView, childName);
                }
            }
        }
        private void RemoveChild()
        {
            if (elements == null || elements.Count == 0)
            {
                return;
            }

            foreach (KeyValuePair<string, ComponentBase> items in elements)
            {
                if (items.Value is ComponentBase componentBase)
                {
                    FluxLogger.InfoP(">>> remove : Name %s1, Component %s2", s1: items.Key, s2: items.Value.GetType()?.Name);
                    DestroyUtility.DestroyView(ref componentBase);
                }
            }
            elements.Clear();
        }
        private void CreateTree()
        {
            if (elementName.TryGetValue(RootLayout, out string name) && elements.TryGetValue(name, out ComponentBase root))
            {
                AddChild(root, name);
            }
        }
        private void DestroyTree()
        {
            RemoveChild();
        }
        #endregion private Method
        private readonly string presetName;
        internal Dictionary<string, ComponentBase> elements = new Dictionary<string, ComponentBase>();
        internal Dictionary<string, string> elementName = new Dictionary<string, string>();
        private bool backgroundEnabled = true;
        private string themeBackgroundColorChip;
        private readonly string RootLayout = "RootLayout";
        private int defaultPaddingBottom;
        private int seroPaddingBottom;
        private Window currentWindow;
    }
}
