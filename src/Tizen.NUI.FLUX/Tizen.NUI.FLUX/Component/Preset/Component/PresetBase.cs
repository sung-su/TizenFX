/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
using System.Collections.Generic;
using Tizen.NUI;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// Base class of Preset
    /// </summary>
    public class PresetBase
    {
        #region Public Property
        /// <summary>
        /// Set ColorPreset in backgroundColor of Preset. When you set this value,it is changed automatically according to state. 
        /// The string starts "CP_".
        /// <example>
        /// preset.ThemeBackgroundColorPreset = "CP_Info1100";
        /// </example>
        /// </summary>
        /// <version> 8.8.0 </version>
        public string ThemeBackgroundColorPreset
        {
            get;
            set;
        } = null;

        /// <summary>
        /// Set ColorChip in backgroundColor of Preset. It is only set single Color so that will be same color for every state.
        /// The string starts "CC_"
        /// </summary>
        /// <example>
        /// preset.ThemeBackgroundColorChip = "CC_Basic1100";
        /// </example>
        /// <version> 8.8.0 </version>
        public string ThemeBackgroundColorChip
        {
            get;
            set;
        } = null;
        /// <summary>
        /// Min Unit Size.
        /// <example>
        /// preset.MinimumUnitSize = new UnitSize(90, 10);
        /// </example>
        /// </summary>
        /// <version> 8.8.0 </version>
        public UnitSize MinimumUnitSize { get; set; } = null;

        /// <summary>
        /// Default Unit Size.
        /// <example>
        /// preset.UnitSize = new UnitSize(90, 10);
        /// </example>
        /// </summary>
        /// <version> 8.8.0 </version>
        public UnitSize UnitSize { get; set; } = null;

        /// <summary>
        /// Max Unit Size
        /// <example>
        /// preset.MaximumUnitSize = new UnitSize(90, 10);
        /// </example>
        /// </summary>
        /// <version> 8.8.0 </version>
        public UnitSize MaximumUnitSize { get; set; } = null;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor of PresetBase
        /// </summary>
        public PresetBase()
        {
            ComponentState.ChangeStatePropertyValue(StateUtility.Normal, "Scale", MotionSpec.NomalScaleValue);
        }

        /// <summary>
        ///  Add component or layout to element tree
        /// </summary>
        /// <param name="treeIndex">Index of element tree.</param>
        /// <param name="element">Instance of element</param>
        /// <param name="elementName">If elementName isn't given, treeIndex will be elementName and it will be used for "KEY" of GetElement of Component class</param>
        /// <version> 8.8.0 </version>
        public void AddToElementTree(string treeIndex, ComponentBase element, string elementName = null)
        {
            if (elementName == null)
            {
                elementName = treeIndex;
            }

            ElementName[elementName] = treeIndex;
            elements[treeIndex] = element;
            if (element != null)
            {
                element.Name = elementName;
            }
        }
        #endregion

        #region Public member
        /// <summary>
        /// Collection of element composing Component
        /// </summary>
        public Dictionary<string, ComponentBase> elements = new Dictionary<string, ComponentBase>();

        /// <summary>
        /// The dictionary of the name of the element which user could modify.
        /// For example, elements like text, image or componentArea could be modified by user, user can get modify these elements by the ElementName.
        /// </summary>
        public Dictionary<string, string> ElementName = new Dictionary<string, string>();
        #endregion

        #region Internal Method
        internal virtual void SetComponentProperty(Component component)
        {
            if (ThemeBackgroundColorPreset != null)
            {
                component.ThemeColor[Component.PlaneThemeColor].ColorPreset = ThemeBackgroundColorPreset;
            }

            if (ThemeBackgroundColorChip != null)
            {
                component.ThemeColor[Component.PlaneThemeColor].ColorChip = ThemeBackgroundColorChip;
            }

            component.UnitSize = UnitSize;
            component.MinimumUnitSize = MinimumUnitSize;
            component.MaximumUnitSize = MaximumUnitSize;

            ThemeBackgroundColorPreset = null;
            ThemeBackgroundColorChip = null;
            MinimumUnitSize = null;
            UnitSize = null;
            MaximumUnitSize = null;
        }
        // Add the element to its parent.
        internal void AddToParent(Component obj)
        {
            if (ElementName == null || obj == null)
            {
                return;
            }
            string treeIndex = ElementName[obj.Name];
            int pos = treeIndex.LastIndexOf('-');
            if (pos > 0)
            {
                if (elements.TryGetValue(treeIndex.Substring(0, pos), out ComponentBase parentView))
                {
                    parentView.Add(obj);
                }
            }
        }
        /// <summary>
        /// Create a Layout.
        /// </summary>
        /// <param name="name">The Layout's name.</param>
        /// <param name="type">The type of the Layout</param>
        internal Layout CreateLayout(string name, LayoutTypes type)
        {
            Layout layout = new Layout(type)
            {
                Name = name,
            };
            elements[name] = layout;

            return layout;
        }

        // TODO: Remove circular dependency
        ///// <summary>
        ///// Create a FrameImageBox.
        ///// </summary>
        ///// <param name="name">The FrameImageBox's name.</param>
        //internal FrameImageBox CreateFrameImageBox(string name)
        //{
        //    FrameImageBox image = new FrameImageBox
        //    {
        //        Name = name
        //    };
        //    elements[name] = image;

        //    return image;
        //}

        // TODO: Remove circular dependency
        //internal TextBox CreateTextBox(string textBoxStyle, string colorChipOrPreset, int pointSize, HorizontalAlignment hAlign)
        //{
        //    TextBox textBox = new TextBox(textBoxStyle)
        //    {
        //        VerticalAlign = VerticalAlignment.Center,
        //        HorizontalAlign = hAlign,
        //        PointSize = pointSize
        //    };

        //    if (colorChipOrPreset.StartsWith("CC_") == true)
        //    {
        //        textBox.ThemeTextColorChip = colorChipOrPreset;
        //    }
        //    else
        //    {
        //        textBox.ThemeTextColorPreset = colorChipOrPreset;
        //    }
        //    return textBox;
        //}

        // TODO: Remove circular dependency
        //internal UIPlate CreateUIPlate(string plateType, bool shaderRoundCorner = false)
        //{
        //    if (UIConfig.IsFullSmart == false && plateType == Constant.UIPlateRoundRect)
        //    {
        //        plateType = Constant.UIPlateRect;
        //    }

        //    string UIPlatePresetName = "I_UIPlate";

        //    if (plateType == Constant.UIPlateRoundRect)
        //    {
        //        UIPlatePresetName = "I_UIPlateRoundRect";
        //    }
        //    else if (plateType == Constant.UIPlateRound)
        //    {
        //        UIPlatePresetName = "I_UIPlateRound";
        //    }

        //    return new UIPlate(UIPlatePresetName, shaderRoundCorner);
        //}

        /// <summary>
        /// Create a ComponentArea.
        /// </summary>
        /// <param name="name">The ComponentArea's name.</param>
        internal Component CreateComponentArea(string name)
        {
            Component componentView = new Component
            {
                Name = name,
            };
            elements[name] = componentView;

            return componentView;
        }

        internal static string GetResourcePath()
        {
            return @"/usr/share/resources/image/";
        }

        internal StateMachine ComponentState = new StateMachine();

        internal ComponentBase GetElement(string eName)
        {
            if (ElementName == null || elements == null || eName == null)
            {
                return null;
            }
            ComponentBase element = null;
            if (ElementName.TryGetValue(eName, out string name) && elements.TryGetValue(name, out ComponentBase componentBase))
            {
                element = componentBase;
            }
            else
            {
                FluxLogger.FatalP("Can't find the element named: %s1", s1: eName);
            }
            return element;
        }

        internal virtual void Destroy()
        {
            elements?.Clear();
            elements = null;

            ElementName?.Clear();
            ElementName = null;

            ComponentState = null;
        }
        #endregion
    }


}
