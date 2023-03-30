/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Tizen.NUI.FLUX.Component
{
    internal class Spec : IDisposable
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int MeasureHeightCallback(int size);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int MeasureWidthCallback(int size);

        internal Spec()
        {
            ptr = WrapNativePolicyManager.NewSpec();
            childSpecList = new List<Spec>();
            Initialize();
        }

        public void Dispose()
        {
            DestroyChildrenSpec();
            nativePadding.Dispose();
            nativePadding = null;
            nativeMargin.Dispose();
            nativeMargin = null;
            nativeTableLayoutInfo.Dispose();
            nativeTableLayoutInfo = null;
            nativeTableLayoutItemInfo.Dispose();
            nativeTableLayoutItemInfo = null;

            WrapNativePolicyManager.DestroySpec(ptr);

            childSpecList.Clear();
            childSpecList = null;
            MeasureHeightDelegate = null;
            MeasureWidthDelegate = null;
        }

        private void Initialize()
        {
            WrapNativePolicyManager.BindMarginToSpec(ptr, nativeMargin.GetMarginPtr());
            WrapNativePolicyManager.BindPaddingToSpec(ptr, nativePadding.GetPaddingPtr());
            WrapNativePolicyManager.BindTableLayoutInfoToSpec(ptr, nativeTableLayoutInfo.GetPtr());
            WrapNativePolicyManager.BindTableLayoutItemInfoToSpec(ptr, nativeTableLayoutItemInfo.GetPtr());

            //Default Value is 1.0f, support function after 23y.
            if(PolicyManager.Instance.Version > 1.0f)
            {
                WrapNativePolicyManager.SetVersion(ptr, PolicyManager.Instance.Version);
            }
        }

        internal IntPtr SpecPtr => ptr;

        internal int ID
        {
            get => WrapNativePolicyManager.GetID(ptr);
            set => WrapNativePolicyManager.SetID(ptr, value);
        }

        internal int? X
        {
            get => WrapNativePolicyManager.GetX(ptr);
            set => WrapNativePolicyManager.SetX(ptr, value.Value);
        }

        internal int? Y
        {
            get => WrapNativePolicyManager.GetY(ptr);
            set => WrapNativePolicyManager.SetY(ptr, value.Value);
        }

        internal int? W
        {
            get => WrapNativePolicyManager.GetWidth(ptr);
            set => WrapNativePolicyManager.SetWidth(ptr, value.Value);
        }

        internal int? H
        {
            get => WrapNativePolicyManager.GetHeight(ptr);
            set => WrapNativePolicyManager.SetHeight(ptr, value.Value);
        }

        internal int? MinWidth
        {
            get => WrapNativePolicyManager.GetMinWidth(ptr);
            set => WrapNativePolicyManager.SetMinWidth(ptr, value.Value);
        }

        internal int? MinHeight
        {
            get => WrapNativePolicyManager.GetMinHeight(ptr);
            set => WrapNativePolicyManager.SetMinHeight(ptr, value.Value);
        }

        internal int? MaxWidth
        {
            get => WrapNativePolicyManager.GetMaxWidth(ptr);
            set => WrapNativePolicyManager.SetMaxWidth(ptr, value.Value);
        }

        internal int? MaxHeight
        {
            get => WrapNativePolicyManager.GetMaxHeight(ptr);
            set => WrapNativePolicyManager.SetMaxHeight(ptr, value.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        internal uint ChildCount
        {
            get => (uint)WrapNativePolicyManager.GetChildCount(ptr);
            set => WrapNativePolicyManager.SetChildCount(ptr, (int)value);
        }

        internal LayoutTypes LayoutType
        {
            get => WrapNativePolicyManager.GetLayoutType(ptr);
            set => WrapNativePolicyManager.SetLayoutType(ptr, value);
        }

        internal int Priority
        {
            get => WrapNativePolicyManager.GetPriority(ptr);
            set
            {
                if (value < PRIORITY_MIN_VALUE || value > PRIORITY_MAX_VALUE)
                {
                    throw new ArgumentOutOfRangeException("The priority value is out of range, it should be set between 1~100.");
                }
                WrapNativePolicyManager.SetPriority(ptr, value);
            }
        }

        internal NativePadding Padding
        {
            get => nativePadding;
            set => nativePadding = value;
        }

        internal NativeMargin Margin
        {
            get => nativeMargin;
            set => nativeMargin = value;
        }

        internal NativeTableLayoutInfo TableLayoutInfo
        {
            get => nativeTableLayoutInfo;
            set => nativeTableLayoutInfo = value;
        }

        internal NativeTableLayoutItemInfo TableLayoutItemInfo
        {
            get => nativeTableLayoutItemInfo;
            set => nativeTableLayoutItemInfo = value;
        }

        internal Aligns Align
        {
            get => WrapNativePolicyManager.GetAlign(ptr);
            set => WrapNativePolicyManager.SetAlign(ptr, value);
        }

        internal Aligns ItemAlign
        {
            get => WrapNativePolicyManager.GetItemAlign(ptr);
            set => WrapNativePolicyManager.SetItemAlign(ptr, value);
        }

        internal int ItemGap
        {
            get => WrapNativePolicyManager.GetItemGap(ptr);
            set => WrapNativePolicyManager.SetItemGap(ptr, value);
        }

        internal int RearrangeGap
        {
            get => WrapNativePolicyManager.GetRearrangeGap(ptr);
            set => WrapNativePolicyManager.SetRearrangeGap(ptr, value);
        }

        internal OmissionRules RearrangePoliocy
        {
            get => WrapNativePolicyManager.GetOmissionRules(ptr);
            set => WrapNativePolicyManager.SetOmissionRules(ptr, value);
        }

        internal ResizePolicyTypes WidthResizePolicy
        {
            get => WrapNativePolicyManager.GetWidthResizePolicy(ptr);
            set => WrapNativePolicyManager.SetWidthResizePolicy(ptr, value);
        }

        internal ResizePolicyTypes HeightResizePolicy
        {
            get => WrapNativePolicyManager.GetHeightResizePolicy(ptr);
            set => WrapNativePolicyManager.SetHeightResizePolicy(ptr, value);
        }

        internal RearrangeRules Rearrange
        {
            get => WrapNativePolicyManager.GetRearrangeRules(ptr);
            set => WrapNativePolicyManager.SetRearrangeRules(ptr, value);
        }

        internal int Weight
        {
            get => WrapNativePolicyManager.GetWeight(ptr);
            set => WrapNativePolicyManager.SetWeight(ptr, value);
        }

        internal bool Visibility
        {
            get => WrapNativePolicyManager.GetVisibility(ptr);
            set => WrapNativePolicyManager.SetVisibility(ptr, value);
        }

        internal bool IsRootLayout
        {
            get => WrapNativePolicyManager.GetIsRootLayout(ptr);
            set => WrapNativePolicyManager.SetIsRootLayout(ptr, value);
        }

        /// <summary>
        /// The property is only used for the layout which parent is rootlayout.
        /// For example, the width of the title bar in AppStore will fill the screen, but there is the margin area in the left and right side of the rootlayout.
        /// In this case, the margin should be disabled.
        /// </summary>
        internal bool ExpandWidthToUIArea
        {
            get => WrapNativePolicyManager.GetExpandWidthToUIArea(ptr);
            set => WrapNativePolicyManager.SetExpandWidthToUIArea(ptr, value);
        }

        /// <summary>
        /// The property for the layout arrange child for RTL.
        /// </summary>
        internal bool EnableRTL
        {
            get => WrapNativePolicyManager.GetEnableRTL(ptr);
            set => WrapNativePolicyManager.SetEnableRTL(ptr, value);
        }

        internal int GetChildSpecX(int childIndex)
        {
            return WrapNativePolicyManager.GetChildSpecX(ptr, childIndex);
        }

        internal int GetChildSpecY(int childIndex)
        {
            return WrapNativePolicyManager.GetChildSpecY(ptr, childIndex);
        }

        internal int GetChildSpecW(int childIndex)
        {
            return WrapNativePolicyManager.GetChildSpecW(ptr, childIndex);
        }

        internal int GetChildSpecH(int childIndex)
        {
            return WrapNativePolicyManager.GetChildSpecH(ptr, childIndex);
        }

        internal int GetChildSpecListCount()
        {
            return WrapNativePolicyManager.GetChildSpecListCount(ptr);
        }

        internal void ClearChildSpecList()
        {
            WrapNativePolicyManager.ClearChildSpecList(ptr);
            childSpecList.Clear();
        }

        internal void AddChildSpec(Spec spec)
        {
            if (spec == null)
            {
                return;
            }
            WrapNativePolicyManager.AddChildSpec(ptr, spec.SpecPtr);
            childSpecList.Add(spec);
        }

        internal Spec GetChildSpec(int index)
        {
            if (childSpecList == null)
            {
                return null;
            }
            return childSpecList.ElementAt(index);
        }

        internal void RegisterMeasureHeight(MeasureHeightCallback func)
        {
            MeasureHeightDelegate = new MeasureHeightCallback(func);
            try
            {
                WrapNativePolicyManager.RegisterHeightMeasureFunction(ptr, MeasureHeightDelegate);
            }
            catch
            {

                FluxLogger.FatalP("Not support RegisterMeasureHeight");
            }
        }

        public void RegisterMeasureWidth(MeasureWidthCallback func)
        {
            MeasureWidthDelegate = new MeasureWidthCallback(func);
            try
            {
                WrapNativePolicyManager.RegisterWidthMeasureFunction(ptr, MeasureWidthDelegate);
            }
            catch
            {
                FluxLogger.FatalP("Not support RegisterMeasureWidth");
            }
        }

        private void DestroyChildrenSpec()
        {
            for (int i = 0; i < ChildCount; ++i)
            {
                Spec childSpec = GetChildSpec(i);
                childSpec?.Dispose();
                childSpec = null;
            }
        }

        private MeasureHeightCallback MeasureHeightDelegate = null;
        private MeasureWidthCallback MeasureWidthDelegate = null;

        private readonly IntPtr ptr;
        private NativePadding nativePadding = new NativePadding();
        private NativeMargin nativeMargin = new NativeMargin();
        private NativeTableLayoutInfo nativeTableLayoutInfo = new NativeTableLayoutInfo();
        private NativeTableLayoutItemInfo nativeTableLayoutItemInfo = new NativeTableLayoutItemInfo();

        private const int PRIORITY_MIN_VALUE = 1;
        private const int PRIORITY_MAX_VALUE = 10;
        private List<Spec> childSpecList;
        public const int INVALID_VALUE = -999999;
        public const int MAXIMUM_VALUE = 99999;
        public const int MINIMUM_VALUE = 0;
    }
}
