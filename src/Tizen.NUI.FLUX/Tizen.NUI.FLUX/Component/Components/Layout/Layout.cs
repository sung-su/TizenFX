/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file Layout.cs
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

using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// This class is Layout of Component
    /// </summary>
    /// <code>
    /// Layout layout = new Layout(LayoutType.FlexV);
    /// layout.UnitSize = new UnitSize(25,10);
    /// layout.MaximumUnitSize = new UnitSize(25,10);
    /// layout.MinimumUnitSize = new UnitSize(25,10);
    /// </code>
    public partial class Layout : ComponentBase, IFocusManageable
    {
        #region public Method
        /// <summary>
        ///  Adds a child view to this view.
        /// </summary>
        /// <param name="child"> The Child view that you want to add </param>
        public override void Add(View child)
        {
            base.Add(child);
            if (child is FluxView childFluxView)
            {
                if (items.Contains(childFluxView) == false)
                {
                    items.Add(childFluxView);
                }
            }
        }

        /// <summary>
        /// Removes a child view from this View. If the view was not a child of this view, this is a no-op.
        /// </summary>
        /// <param name="child"> The Child view that you want to remove </param>
        public override void Remove(View child)
        {
            base.Remove(child);
            if (child is FluxView childFluxView)
            {
                items.Remove(childFluxView);
            }
        }

        /// <summary>
        /// Puts a child at a specific index in the layout.
        /// Not Need Call Add method.
        /// </summary>
        /// <param name="index">layout`s child index.</param>
        /// <param name="child"> The Child view that you want to insert </param>
        /// <version> 10.10.0 </version>
        public void Insert(int index, View child)
        {
            Insert(child, index);
        }

        /// <summary>
        /// Constructor to instantiate the Layout class.
        /// </summary>
        public Layout() : base()
        {
            Initialize();
        }

        /// <summary>
        /// Constructor to instantiate the Layout class.
        /// </summary>
        public Layout(LayoutTypes value) : base()
        {
            Initialize();
            LayoutParam.type = value;
        }


        /// <summary>
        /// Constructor to instantiate the Layout class.
        /// </summary>
        public Layout(string name = null) : base(name)
        {
            Initialize();
        }

        /// <summary>
        /// User can call this function to layout its children
        /// </summary>
        public override void UpdateLayout()
        {
            if (SizeWidth < 0 || SizeHeight < 0)
            {
                return;
            }
            Spec spec = GenerateSpec(this, false, false);
            if (spec == null)
            {
                return;
            }

            PolicyManager.Instance.ApplyPolicy(spec);
            ApplyPolicy(this, spec);
            spec.Dispose();
            spec = null;
        }
        /// <summary>
        ///  This class is layout's property. for FlexH, FlexV, Linear
        /// </summary>
        public new FluxLayoutParam LayoutParam
        {
            set => SetValue(LayoutParamProperty, value);

            get => (FluxLayoutParam)GetValue(LayoutParamProperty);
        }

        /// <summary>
        ///  VIew instance of Layout's Background
        /// </summary>
        public View BackgroundView
        {
            set => SetValue(BackgroundViewProperty, value);

            get => (View)GetValue(BackgroundViewProperty);
        }

        /// <summary>
        /// Set ColorChip in backgroundColor of Layout. It is only set Color not changing color according to state.
        /// The string starts "CC_"
        /// </summary>
        /// <example>
        /// layout.ThemeBackgroundColorChip = "CC_Basic1100";
        /// </example>
        /// <version>8.8.0</version>
        public string ThemeBackgroundColorChip
        {
            get => (string)GetValue(ThemeBackgroundColorChipProperty);

            set => SetValue(ThemeBackgroundColorChipProperty, value);
        }

        /// when implement this interface, have to return view.
        /// </summary>
        /// <param name="originView"> currnet focused view</param>
        /// <param name="direction"> Direction to get next focus</param>
        /// <returns> Return View instance to apply auto focus</returns>
        public View GetNextFocusableFluxView(View originView, FocusDirection direction)
        {
            View nextView = null;

            if (items.Count > 0)
            {
                nextView = AutoFocusAlgorithm.Instance.FindFocusableSiblingsFluxView(items[0], direction, originView);
            }
            return nextView;
        }
        #endregion public Method

        #region private Property
        private FluxLayoutParam privateLayoutParam
        {
            set
            {
                if (value is FluxLayoutParam)
                {
                    base.LayoutParam = value;
                }
                else
                {
                    //TODO handle
                }
            }

            get => base.LayoutParam as FluxLayoutParam;
        }

        private View privateBackgroundView
        {
            set;
            get;
        }

        private string privateThemeBackgroundColorChip
        {
            get => themeBackgroundColorChip;
            set
            {
                themeBackgroundColorChip = value;
                FluxLogger.DebugP("themeBackgroundColorChip :[%s1]", s1: themeBackgroundColorChip);
                ThemeHelper.Instance.MapColorChip(this, "BackgroundColor", themeBackgroundColorChip);
            }
        }
        #endregion private Property

        #region ptotected Method
        /// <summary>
        /// Cleaning up managed and unmanaged resources
        /// </summary>
        /// <param name="type">
        /// Type of Dispose.
        /// Explicit - Called by user explicitly.
        /// Implicit - Called by gc implicitly.
        /// </param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.
                items.Clear();
                items = null;

                LayoutParam = null;

                ThemeHelper.Instance.UnMapColorChip(this, "BackgroundColor");
            }
            base.Dispose(type);
        }
        #endregion ptotected Method

        #region internal Method
        internal override void UIDirectionChanged(object sender, DirectionChangedEventArgs e)
        {
            UpdateLayout();
            base.UIDirectionChanged(sender, e);
        }
        internal void AddToFirst(View child)
        {
            base.Add(child);
            if (child is FluxView childFluxView)
            {
                items.Insert(0, childFluxView);
            }
        }
        internal void Insert(View child, int index)
        {
            base.Add(child);
            if (child is FluxView childFluxView)
            {
                if (items.Count >= 0)
                {
                    if (index < items.Count)
                    {
                        items.Insert(index, childFluxView);
                    }
                    else
                    {
                        items.Add(childFluxView);
                    }
                }
            }
        }
        #endregion internal Method

        #region internal Property
        internal LayoutTypes Type
        {
            get => LayoutParam.type;
            set => LayoutParam.type = value;
        }
        #endregion internal Property

        #region private Method

        private void Initialize()
        {
            LayoutParam = new RootLayoutParam();
        }

        private int GetUnitWidth(UnitSize val)
        {
            return (val != null) ? val.Width : 0;
        }

        private int GetUnitHeight(UnitSize val)
        {
            return (val != null) ? val.Height : 0;
        }

        private int GetOriginalWidth(FluxView obj)
        {
            if (obj == null)
            {
                return 0;
            }
            if (obj.LayoutParam is LayoutItemParam itemParam)
            {
                if (itemParam.originalWidth == Spec.INVALID_VALUE && obj.SizeWidth > 0)
                {
                    itemParam.originalWidth = (int)obj.SizeWidth;
                }
                return itemParam.originalWidth;
            }
            else
            {
                return 0;
            }
        }

        private int GetOriginalHeight(FluxView obj)
        {
            if (obj == null)
            {
                return 0;
            }
            if (obj.LayoutParam is LayoutItemParam itemParam)
            {
                if (itemParam.originalHeight == Spec.INVALID_VALUE && obj.SizeHeight > 0)
                {
                    itemParam.originalHeight = (int)(obj.SizeHeight);
                }
                return itemParam.originalHeight;
            }
            return 0;
        }

        private Spec MeasureLayout(ComponentBase component)
        {
            Spec spec = GenerateSpec(component, true, false);
            if (spec == null)
            {
                return null;
            }
            PolicyManager.Instance.ApplyPolicy(spec);
            return spec;
        }

        private Spec ReShapeLayout(ComponentBase component)
        {
            Spec spec = GenerateSpec(component, false, true);
            if (spec == null)
            {
                return null;
            }
            PolicyManager.Instance.ApplyPolicy(spec);
            return spec;
        }

        private Spec GenerateSpec(FluxView obj, bool measure, bool reshape)
        {
            bool typeCheckflag = false;

            Spec spec = new Spec();

            if (obj.LayoutParam is RootLayoutParam rootLayoutParam)
            {
                GenerateMarginSpec(ref spec, rootLayoutParam);
                typeCheckflag = true;
            }

            if (obj.LayoutParam is FluxLayoutParam layoutParam)
            {
                GenerateLayoutSpec(ref spec, obj, layoutParam);
                typeCheckflag = true;
            }

            if (obj.LayoutParam is LayoutItemParam layoutItemParam)
            {
                // when create spec in itemCase , need view`s spec. so just checking itemParam.
                GenerateComponentSpec(ref spec, obj, layoutItemParam, measure, reshape);
                typeCheckflag = true;
            }

            if (!typeCheckflag)
            {
                spec.Dispose();
                spec = null;
                return null;
            }

            if (obj is Layout layout)
            {
                if (!measure && !reshape)
                {
                    foreach (FluxView item in layout.items)
                    {
                        if (item is Component component)
                        {
                            if (component.Reshapable)
                            {
                                layout.HasReShapechild = true;
                                break;
                            }
                        }
                    }
                }

                if (layout.LayoutParam.MarginAreaPolicy != MarginAreaPolicy.Disable)
                {
                    if (layout.LayoutParam is RootLayoutParam rootlayoutParam)
                    {
                        spec.IsRootLayout = true;
                        switch (layout.LayoutParam.MarginAreaPolicy)
                        {
                            case MarginAreaPolicy.EnableAll:
                                {
                                    rootlayoutParam.Margin.LeftMargin = (int)PolicyManager.Instance.Margin[0];
                                    rootlayoutParam.Margin.RightMargin = (int)PolicyManager.Instance.Margin[1];
                                }
                                break;
                            case MarginAreaPolicy.EnableLeft:
                                {
                                    rootlayoutParam.Margin.LeftMargin = (int)PolicyManager.Instance.Margin[0];
                                    rootlayoutParam.Margin.RightMargin = 0;
                                }
                                break;
                            case MarginAreaPolicy.EnableRight:
                                {
                                    rootlayoutParam.Margin.LeftMargin = 0;
                                    rootlayoutParam.Margin.RightMargin = (int)PolicyManager.Instance.Margin[1];
                                }
                                break;

                            default:
                                break;
                        }
                        GenerateMarginSpec(ref spec, rootlayoutParam);
                    }
                }

                RemoveDirtyItems();

                foreach (FluxView item in layout.items)
                {
                    Spec childSpec = GenerateSpec(item, measure, reshape);

                    if (childSpec != null)
                    {
                        spec.AddChildSpec(childSpec);
                        spec.ChildCount++;
                    }
                }
            }

            return spec;
        }

        private void RemoveDirtyItems()
        {
            List<FluxView> dirtyItems = new List<FluxView>();

            foreach (FluxView item in items)
            {
                //Items is not remove from layout before disposed
                if (item.HasBody() == false)
                {
                    dirtyItems.Add(item);
                }
            }

            foreach (FluxView item in dirtyItems)
            {
                items.Remove(item);
            }

            dirtyItems.Clear();
            dirtyItems = null;
        }

        private void GenerateMarginSpec(ref Spec spec, RootLayoutParam rootLayoutParam)
        {
            if (rootLayoutParam.Margin == null)
            {
                spec.Margin.LeftMargin = 0;
                spec.Margin.RightMargin = 0;
                spec.Margin.TopMargin = 0;
                spec.Margin.BottomMargin = 0;
            }
            else
            {
                spec.Margin.LeftMargin = rootLayoutParam.Margin.LeftMargin;
                spec.Margin.RightMargin = rootLayoutParam.Margin.RightMargin;
                //This Value used after 2023 Model.(for Portrait)
                spec.Margin.TopMargin = DisplayMetrics.Instance.UnitToPixel(rootLayoutParam.Margin.TopMargin);
                spec.Margin.BottomMargin = DisplayMetrics.Instance.UnitToPixel(rootLayoutParam.Margin.BottomMargin);
            }
        }

        private void GenerateLayoutSpec(ref Spec spec, FluxView fluxView, FluxLayoutParam layParam)
        {
            spec.LayoutType = layParam.type;
            spec.Align = layParam.Align;
            spec.Rearrange = layParam.Rearrange;
            spec.IsRootLayout = layParam.isRootLayout;

            //We should check UIDirection for each child view
            if (fluxView.UIDirection == UIDirection.RTL && layParam.EnableRTL)
            {
                spec.EnableRTL = layParam.EnableRTL;
            }
            else
            {
                spec.EnableRTL = false;
            }

            spec.RearrangeGap = spec.ItemGap = DisplayMetrics.Instance.UnitToPixel(layParam.ItemGap);

            if (layParam.RearrangeGap.HasValue == true)
            {
                spec.RearrangeGap = DisplayMetrics.Instance.UnitToPixel(layParam.RearrangeGap.Value);
            }

            if (layParam.Padding == null)
            {
                spec.Padding.Left = 0;
                spec.Padding.Right = 0;
                spec.Padding.Top = 0;
                spec.Padding.Bottom = 0;
            }
            else
            {
                spec.Padding.Left = DisplayMetrics.Instance.UnitToPixel(layParam.Padding.Left);
                spec.Padding.Right = DisplayMetrics.Instance.UnitToPixel(layParam.Padding.Right);
                spec.Padding.Top = DisplayMetrics.Instance.UnitToPixel(layParam.Padding.Top);
                spec.Padding.Bottom = DisplayMetrics.Instance.UnitToPixel(layParam.Padding.Bottom);
            }
            
            if (spec.IsRootLayout == true && layParam is RootLayoutParam rootLayoutParam)
            {
                spec.Padding.Top += DisplayMetrics.Instance.UnitToPixel(rootLayoutParam.Margin.TopMargin);
                spec.Padding.Bottom += DisplayMetrics.Instance.UnitToPixel(rootLayoutParam.Margin.BottomMargin);
            }

            spec.TableLayoutInfo.ColumnCount = layParam.TableLayoutInfo.ColumnCount;
            spec.TableLayoutInfo.RowCount = layParam.TableLayoutInfo.RowCount;
        }

        private void GenerateParamSpec(ref Spec spec, LayoutItemParam param)
        {
            spec.Weight = param.Weight;
            spec.Priority = param.Priority;
            spec.RearrangePoliocy = param.Omission;
            spec.WidthResizePolicy = param.WidthResizePolicy;
            spec.HeightResizePolicy = param.HeightResizePolicy;
            spec.ExpandWidthToUIArea = param.ExpandWidthToUIArea;
            spec.ItemAlign = param.ItemAlign;
            spec.TableLayoutItemInfo.ColumnIndex = param.TableLayoutItemInfo.ColumnIndex;
            spec.TableLayoutItemInfo.ColumnSpan = param.TableLayoutItemInfo.ColumnSpan;
            spec.TableLayoutItemInfo.RowIndex = param.TableLayoutItemInfo.RowIndex;
            spec.TableLayoutItemInfo.RowSpan = param.TableLayoutItemInfo.RowSpan;
        }

        private void RegisterMeasureFunction(ref Spec spec, ComponentBase component, LayoutItemParam layoutParam)
        {
            if (spec == null || component == null || component.HasBody() == false || layoutParam == null)
            {
                return;
            }

            if (component.KeepHeightByRatio == true)
            {
                spec.RegisterMeasureHeight(component.MeasureHeight);
            }
            if (component.KeepWidthByRatio == true)
            {
                spec.RegisterMeasureWidth(component.MeasureWidth);
            }
        }

        private void GenerateComponentSpec(ref Spec spec, FluxView obj, LayoutItemParam layoutParam, bool measure, bool doReshape)
        {
            ComponentBase component = obj as ComponentBase;

            if (layoutParam == null)
            {
                return;
            }

            RegisterMeasureFunction(ref spec, component, layoutParam);

            if (obj == this)
            {
                spec.W = (int)(obj.SizeWidth);
                spec.H = (int)(obj.SizeHeight);
            }
            else
            {
                if (layoutParam.WidthResizePolicy == ResizePolicyTypes.Reserved)
                {
                    spec.W = (int)(obj.SizeWidth);
                }
                else
                {
                    spec.W = GetOriginalWidth(obj);
                }

                if (layoutParam.HeightResizePolicy == ResizePolicyTypes.Reserved)
                {
                    if (spec.LayoutType == LayoutTypes.FlexH && spec.Rearrange == RearrangeRules.On)
                    {
                        // This case, layout's height could be changed by children be arranged to new row, we should remember its original value, PolicyManager will handle its current height.
                        spec.H = GetOriginalHeight(obj);
                    }
                    else
                    {
                        if (layoutParam.EnableGetHeightByWidth)
                        {
                            if (component != null)
                            {
                                spec.H = component.GetHeightByWidth();
                            }
                        }
                        else
                        {
                            spec.H = (int)(obj.SizeHeight);
                        }
                    }
                }
                else
                {
                    spec.H = GetOriginalHeight(obj);
                }
            }

            if (measure)
            {
                spec.MinWidth = spec.MinHeight = Spec.MINIMUM_VALUE;
                spec.MaxWidth = spec.MaxHeight = Spec.MAXIMUM_VALUE;
            }
            else
            {
                if (doReshape && component != null)
                {
                    spec.W = spec.MaxWidth = spec.MinWidth = DisplayMetrics.Instance.UnitToPixel(GetUnitWidth(component.reShapeSize));
                    spec.H = spec.MaxHeight = spec.MinHeight = DisplayMetrics.Instance.UnitToPixel(GetUnitHeight(component.reShapeSize));
                }
                else
                {
                    spec.MinWidth = DisplayMetrics.Instance.UnitToPixel(GetUnitWidth(obj.MinimumUnitSize));
                    spec.MinHeight = DisplayMetrics.Instance.UnitToPixel(GetUnitHeight(obj.MinimumUnitSize));
                    spec.MaxWidth = DisplayMetrics.Instance.UnitToPixel(GetUnitWidth(obj.MaximumUnitSize));
                    spec.MaxHeight = DisplayMetrics.Instance.UnitToPixel(GetUnitHeight(obj.MaximumUnitSize));

                    if (layoutParam.WidthResizePolicy == ResizePolicyTypes.Shared && obj.MinimumUnitSize == null)
                    {
                        spec.MinHeight = spec.MinWidth = Spec.MINIMUM_VALUE;
                    }
                    if (layoutParam.WidthResizePolicy == ResizePolicyTypes.Shared && obj.MaximumUnitSize == null)
                    {
                        spec.MaxHeight = spec.MaxWidth = Spec.MAXIMUM_VALUE;
                    }
                }

                spec.ClearChildSpecList();
                spec.ChildCount = 0;
                spec.Visibility = obj.Visibility;

                if (obj.LayoutParam is LayoutItemParam itemParam)
                {
                    GenerateParamSpec(ref spec, itemParam);
                }
            }
        }

        private void ApplyPolicy(FluxView obj, Spec spec)
        {
            Layout layout = obj as Layout;

            if (layout == null || spec == null)
            {
                return;
            }

            if (obj == this)
            {
                if (spec.WidthResizePolicy == ResizePolicyTypes.Wrap && spec.W > 0)
                {
                    obj.SizeWidth = (int)spec.W;
                }
                if (spec.HeightResizePolicy == ResizePolicyTypes.Wrap && spec.H > 0)
                {
                    obj.SizeHeight = (int)spec.H;
                }
            }

            uint childCount = (uint)layout.items.Count;
            uint childCountInSpec = (uint)spec.GetChildSpecListCount();

            if (childCount != childCountInSpec || childCount == 0 || childCountInSpec == 0)
            {
                return;
            }

            for (uint index = 0; index < childCount; ++index)
            {
                ApplyCoordinate(layout.items[(int)index], spec, (int)index);
            }
        }

        private void ApplyCoordinate(FluxView obj, Spec spec, int specIndex)
        {
            if (obj == null || obj.Visibility == false)
            {
                return;
            }

            int x = spec.GetChildSpecX(specIndex);
            int y = spec.GetChildSpecY(specIndex);
            int w = spec.GetChildSpecW(specIndex);
            int h = spec.GetChildSpecH(specIndex);

            if (obj is Component component)
            {
                if (component.Reshapable)
                {
                    if (w < DisplayMetrics.Instance.UnitToPixel(component.MinimumUnitSize.Width))
                    {
                        w = DisplayMetrics.Instance.UnitToPixel(component.reShapeSize.Width);
                        h = DisplayMetrics.Instance.UnitToPixel(component.reShapeSize.Height);

                        NeedRePosition = true;
                    }
                }
            }

            if (w > 0 && h > 0)
            {
                obj.SizeWidth = w;
                obj.SizeHeight = h;
                obj.PositionX = x;
                obj.PositionY = y;
                //FluxLogger.FatalP("ID = %d1 / [%d2,%d3] - [%d4,%d5]", d1: obj.ID, d2: x, d3: y, d4: w, d5: h); // for debuging
                ApplyPolicy(obj, spec.GetChildSpec(specIndex));

                if (!(obj is Layout))
                {
                    if (obj is ComponentBase componentBase)
                    {
                        if (componentBase.NeedUpdateLayout == true)
                        {
                            componentBase.UpdateLayout();
                        }
                        else
                        {
                            FluxLogger.FatalP("Skip updateLayout : [%s1]:[[%d1]]", s1: componentBase.GetType()?.Name, d1: (int)componentBase.ID);
                        }
                    }
                    else
                    {
                        // FluxView
                    }
                }
            }
        }

        #endregion private Method
        #region private Field
        private List<FluxView> items = new List<FluxView>();
        private bool NeedRePosition = false;
        private bool HasReShapechild = false;
        private string themeBackgroundColorChip = null;
        #endregion private Field
    }
}