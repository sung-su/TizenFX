/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file ComponentBase.cs
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

using System;
using System.ComponentModel;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// ComponentBase.
    /// </summary>
    /// <code>
    /// Refer to Component class.
    /// </code>
    public partial class ComponentBase : FluxView
    {
        #region Public Method
        /// <summary>
        /// Construct an empty ComponentBase.
        /// </summary>
        public ComponentBase()
        {
            Initialize();
        }

        /// <summary>
        /// Construct an empty ComponentBase.
        /// </summary>
        public ComponentBase(string name = null)
        {
            Initialize();
        }

        internal ComponentBase(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn)
        {
            Initialize();
        }

        /// <summary>
        /// Update with Attributes.
        /// </summary>

        public void Update()
        {
            OnUpdate();
        }

        /// <summary>
        ///  This class is item's property class in layout. 
        /// </summary>
        public new LayoutItemParam LayoutParam
        {
            set
            {
                SetValue(LayoutParamProperty, value);
            }

            get
            {
                return (LayoutItemParam)GetValue(LayoutParamProperty);
            }
        }
        /// <summary>
        ///  Attach view as child with legacy way. And it maybe not influence "Responsive Rule" . So if user want to add to Layout or Component as Flux Rule, please use Add/Remove. 
        ///  If you want to get the state propagation, then please use   Attach(ComponentBase view, bool enablePropagateState)
        /// </summary>
        /// <param name="view"> if null, it makes exception.</param>
        /// <version>8.8.0</version>
        public void Attach(View view)
        {
            if (view == null)
            {
                throw new InvalidOperationException("ComponentBase.Attach() parameter is null , please input proper value as parameter");
            }

            base.Add(view);
        }

        /// <summary>
        ///  Attach view as child with legacy way. And it maybe not influence "Responsive Rule" . So if user want to add to Layout or Component as Flux Rule, please use Add/Remove. 
        /// </summary>
        /// <param name="view"> if null, it makes exception.</param>
        /// <param name="enablePropagateState"> if attached layout or component get the state propagation, then set true</param>
        /// <version>9.9.0</version>
        public void Attach(ComponentBase view, bool enablePropagateState)
        {
            if (view == null)
            {
                throw new InvalidOperationException("ComponentBase.Attach() parameter is null , please input proper value as parameter");
            }

            base.Add(view);

            //Additional Info is componentbase. So, in this case we should set this for Additional Info & both
            EnablePropagateState = enablePropagateState;
            view.EnablePropagateState = enablePropagateState;
        }

        /// <summary>
        ///  Detach view as child with legacy way. And it maybe not influence "Responsive Rule" . So if user want to add to Layout or Component as Flux Rule, please use Add/Remove. 
        /// </summary>
        /// <param name="view"> if null, it makes exception.</param>
        /// <version>8.8.0</version>
        public void Detach(View view)
        {
            if (view == null)
            {
                throw new InvalidOperationException("ComponentBase.Detach() parameter is null , please input proper value as parameter");
            }

            base.Remove(view);
        }


        /// <summary>
        /// Called when the control attributes changed. Should be overridden by derived classes if they need to customize what happens when attributes changed.
        /// </summary>
        protected virtual void OnUpdate()
        {
            // Update attributes if not null and different to before.
        }

        /// <summary>
        /// Call the function to request component to update layout.
        /// </summary>
        public virtual void UpdateLayout()
        {

        }
        /// <summary>
        /// enablePropagateState. 
        /// If enablePropagateState is true , the children state will all update when the parent state changed.
        /// If enablePropagateState is false, the children state will not update  when the parent state changed,such as datepicker,timepicker. 
        /// </summary>
        /// supporting xaml
        /// <version>10.10.0</version>
        public bool EnablePropagateState
        {
            get
            {
                return (bool)GetValue(EnablePropagateStateProperty);
            }

            set
            {
                SetValue(EnablePropagateStateProperty, value);
            }
        }

        /// <summary>
        /// if this value is true, automatical define Height size by SizeRatio. ( SizeRatio is "Initial Width divide Initial Height")
        /// MeasureHeight = Current Width * SizeRatio
        /// </summary>
        /// <version> 9.9.0 </version>
        public bool KeepHeightByRatio
        {
            get
            {
                return (bool)GetValue(KeepHeightByRatioProperty);
            }

            set
            {
                SetValue(KeepHeightByRatioProperty, value);
            }
        }

        /// <summary>
        /// if this value is true, automatical define Width by SizeRatio. ( SizeRatio is "Initial Width divide Initial Height")
        /// MeasureWidth = Current Height * SizeRatio
        /// </summary>
        /// <version> 9.9.0 </version>
        public bool KeepWidthByRatio
        {
            get
            {
                return (bool)GetValue(KeepWidthByRatioProperty);
            }

            set
            {
                SetValue(KeepWidthByRatioProperty, value);
            }
        }
        /// <summary>
        /// if this value is false, skip the component's internal layout update while parent update Layout.
        /// Default is true, This works on Component only. Layout doesn't work.
        /// </summary>
        /// <version> 9.9.1 </version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool NeedUpdateLayout
        {
            get
            {
                return (bool)GetValue(NeedUpdateLayoutProperty);
            }
            set
            {
                SetValue(NeedUpdateLayoutProperty, value);
            }
        }
        #endregion Public Method

        #region private Property
        private LayoutItemParam privateLayoutParam
        {
            get => base.LayoutParam as LayoutItemParam;
            set
            {
                if (value is LayoutItemParam)
                {
                    base.LayoutParam = value;
                }
                else
                {
                    //TODO handle
                }
            }
        }

        private bool privateEnablePropagateState
        {
            get;
            set;
        } = false;

        private bool privateKeepHeightByRatio
        {
            get => keepHeightByRatio;
            set
            {
                if (keepWidthByRatio == false)
                {
                    keepHeightByRatio = value;
                }
            }
        }

        private bool privateKeepWidthByRatio
        {
            get => keepWidthByRatio;
            set
            {
                if (keepHeightByRatio == false)
                {
                    keepWidthByRatio = value;
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool privateNeedUpdateLayout
        {
            get;
            set;
        } = true;
        #endregion private Property

        /// <summary>
        /// This method is called HighContrast state changed.
        /// Derived classes should override this if they wish to customize to handle the highContrast event.
        /// </summary>
        /// <param name="sender">The object who send this event</param>
        /// <param name="e">Event argument</param>
        protected virtual void OnHighContrastChanged(object sender, EventArgs e)
        {
            HandleHighcontrast(AccessibilityManager.Instance.HighContrast);
        }

        /// <summary>
        /// This method is called Enlarge state changed.
        /// Derived classes should override this if they wish to customize to handle the enlarge event.
        /// </summary>
        /// <param name="sender">The object who send this event</param>
        /// <param name="e">Event argument</param>
        protected virtual void OnEnlargeChanged(object sender, EventArgs e)
        {
            ApplyScaleValue();
        }

        /// <summary>
        /// Dispose Control.
        /// </summary>
        /// <param name="type">Dispose caused type</param>
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

                AccessibilityManager.Instance.HighContrastChanged -= OnHighContrastChanged;
                AccessibilityManager.Instance.EnlargeChanged -= OnEnlargeChanged;

                LayoutParam = null;
                UIDirectionChangedEvent -= OnUIDirectionChanged;
                DisposeComponent();
            }

            //Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.
            //Unreference this from if a static instance refer to this. 

            //You must call base.Dispose(type) just before exit.

            base.Dispose(type);
        }

        #region internal Method
        private void OnUIDirectionChanged(object sender, DirectionChangedEventArgs e)
        {
            if (currentDirection == UIDirection)
            {
                return;
            }

            UIDirectionChanged(sender, e);
            currentDirection = UIDirection;
        }
        internal virtual void UIDirectionChanged(object sender, DirectionChangedEventArgs e)
        {
        }

        internal bool Reshapable
        {
            set;
            get;
        } = false;
        internal virtual bool CanBeChanged(string key)
        {
            string[] propertyArray =
            {
                "LayoutParam",
                "EnablePropagateState",
                "KeepHeightByRatio",
                "KeepWidthByRatio",
                "NeedUpdateLayout",
                // below is from FluxView
                "SizeHeight",
                "SizeWidth",
                "LayoutParam",
                "UnitSize",
                "UnitSizeWidth",
                "UnitSizeHeight",
                "Size",
                "MaximumUnitSize",
                "MinimumUnitSize"
            };

            for (int index = 0; index < propertyArray.Length; index++)
            {
                if (key == propertyArray[index])
                {
                    return true;
                }
            }
            return false;
        }
        internal virtual int GetHeightByWidth()
        {
            return 0;
        }
        /// <summary>
        /// PostUpdateLayout() .
        /// </summary>
        internal virtual void PostUpdateLayout()
        {

        }
        /// <summary>
        /// PreUpdateLayout.
        /// </summary>
        internal virtual void PreUpdateLayout()
        {

        }
        internal virtual void ApplyScaleValue()
        {

        }
        internal virtual void DisposeComponent()
        {

        }
        /// <summary>
        /// If Component want to reshape work, implate this function and called in updatelayout.
        /// </summary>
        internal virtual void DoReshape()
        {

        }
        /// <summary>
        /// If Component want to revert reshape work, implate this function and called in updatelayout.
        /// </summary>
        internal virtual void UnDoReshape()
        {

        }

        internal virtual void HandleHighcontrast(bool isHighcontrast)
        {

        }

        internal int MeasureHeight(int Size2DWidth)
        {
            return MeasureHeightPixel(Size2DWidth);
        }

        internal virtual int MeasureHeightPixel(int Size2DWidth)
        {
            int height = 0;
            if (LayoutParam.WidthResizePolicy == ResizePolicyTypes.MatchParent || LayoutParam.WidthResizePolicy == ResizePolicyTypes.Shared)
            {
                if (LayoutParam.SizeRatio == 0.0f)
                {
                    LayoutParam.SizeRatio = (float)(DisplayMetrics.Instance.UnitToPixel(UnitSizeHeight) / (float)Size2DWidth);
                    height = DisplayMetrics.Instance.UnitToPixel(UnitSizeHeight);
                }
                else
                {
                    height = (int)(Size2DWidth * LayoutParam.SizeRatio);
                }
            }
            else
            {
                height = DisplayMetrics.Instance.UnitToPixel(UnitSizeHeight) * Size2DWidth / DisplayMetrics.Instance.UnitToPixel(UnitSizeWidth);
            }
            return height;
        }


        internal int MeasureWidth(int Size2DHeight)
        {
            return MeasureWidthPixel(Size2DHeight);
        }

        internal virtual int MeasureWidthPixel(int Size2DHeight)
        {
            int width = 0;
            if (LayoutParam.WidthResizePolicy == ResizePolicyTypes.MatchParent || LayoutParam.WidthResizePolicy == ResizePolicyTypes.Shared)
            {
                if (LayoutParam.SizeRatio == 0.0f)
                {
                    LayoutParam.SizeRatio = (float)(Size2DHeight / (float)DisplayMetrics.Instance.UnitToPixel(UnitSizeWidth));
                    width = DisplayMetrics.Instance.UnitToPixel(UnitSizeWidth);
                }
                else
                {
                    width = (int)(Size2DHeight * LayoutParam.SizeRatio);
                }
            }
            else
            {
                width = DisplayMetrics.Instance.UnitToPixel(UnitSizeWidth) * Size2DHeight / DisplayMetrics.Instance.UnitToPixel(UnitSizeHeight);
            }
            return width;
        }

        private void ForcePropagateState(string toState, FluxAnimationPlayer animator = null, bool bForceUpdate = false)
        {
            // propagation for all children
            foreach (View child in Children)
            {
                if (child is ComponentBase componentBase)
                {
                    FluxLogger.DebugP("-->[%s1]:[%s2], IsComponent:[%d1], toState:[%s3]"
                        ,s1: GetTypeName()
                        ,s2: componentBase.Name
                        ,d1: componentBase.IsComponent ? 1 : 0
                        ,s3: toState);
                    if (componentBase.IsComponent == true)
                    {
                        componentBase.PropagateState(toState, animator, bForceUpdate);
                    }
                    else
                    {
                        componentBase.ForcePropagateState(toState, animator, bForceUpdate);
                    }
                }
            }
        }

        internal virtual void PropagateState(string toState, FluxAnimationPlayer animator = null, bool bForceUpdate = false)
        {
            FluxLogger.DebugP("[%s1]:[%s2] EnablePropagateState:[%d1], toState:[%s3]"
                ,s1: GetTypeName()
                ,s2: Name
                ,d1: EnablePropagateState ? 1 : 0
                ,s3: toState);
            if (EnablePropagateState)
            {
                ForcePropagateState(toState, animator, bForceUpdate);
            }
        }

        #endregion internal Method
        #region internal Field
        internal UnitSize reShapeSize = new UnitSize(0, 0);
        internal int originalWidth;
        internal int originalHeight;
        internal bool enableAspectRatio = false;
        internal UIDirection currentDirection;

#if Support_FLUXCore_IsComponent
#else
        internal bool IsComponent = false;
#endif
        #endregion internal Field

        #region private Method
        private void Initialize()
        {
            Name = GetType().Name;

            LayoutParam = new LayoutItemParam();

            AccessibilityManager.Instance.HighContrastChanged += OnHighContrastChanged;
            AccessibilityManager.Instance.EnlargeChanged += OnEnlargeChanged;

            // for layout
            originalWidth = Spec.INVALID_VALUE;
            originalHeight = Spec.INVALID_VALUE;

            currentDirection = UIDirection;
            UIDirectionChangedEvent += OnUIDirectionChanged;
        }



        #endregion private Method
        #region private Field
        private bool keepHeightByRatio = false;
        private bool keepWidthByRatio = false;
        #endregion private Field
    }
}



/*
/// <summary>
/// check the privilege of APIs for all TV NUI's controls
/// </summary>
private void CheckPrivilege()
{
    string smackLabel;
    string uid;
    string clientSession = "";

    // Cynara  structure init
    if (Tizen.TV.Security.Privilege.Cynara.CynaraInitialize(false) != 0)
    {
        throw new global::System.ExecutionEngineException("Fail to initialize Cynara");
    }

    // Get credential ? Smack label
    smackLabel = global::System.IO.File.ReadAllText("/proc/self/attr/current");
    // Get credential ? UID
    uid = Tizen.TV.Security.Privilege.Cynara.GetUid();
    // Cynara check
    try
    {
        if (Tizen.TV.Security.Privilege.Cynara.CynaraCheck(smackLabel, clientSession, uid, "http://tizen.org/privilege/internal/default/platform") != 2)
        {
            throw new MethodAccessException("http://tizen.org/privilege/internal/default/platform");
        }
        Tizen.TV.Security.Privilege.Cynara.CynaraFinish();
    }
    catch (ObjectDisposedException e)
    {
        throw new global::System.ExecutionEngineException($"Fail to initialize Cynara : {e}");
    }
}

 */
