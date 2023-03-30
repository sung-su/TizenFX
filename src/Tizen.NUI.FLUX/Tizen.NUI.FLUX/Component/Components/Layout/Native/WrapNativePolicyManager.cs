/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
using System;
using System.Runtime.InteropServices;

namespace Tizen.NUI.FLUX.Component
{

    internal class WrapNativePolicyManager
    {
        // spec
        [DllImport(policyManagerName, EntryPoint = "NewSpec", CharSet = CharSet.Ansi)]
        public static extern IntPtr NewSpec();

        [DllImport(policyManagerName, EntryPoint = "DestroySpec", CharSet = CharSet.Ansi)]
        public static extern void DestroySpec(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetVersion", CharSet = CharSet.Ansi)]
        public static extern void SetVersion(IntPtr intPtr, float version);

        [DllImport(policyManagerName, EntryPoint = "SetID", CharSet = CharSet.Ansi)]
        public static extern void SetID(IntPtr intPtr, int id);

        [DllImport(policyManagerName, EntryPoint = "GetID", CharSet = CharSet.Ansi)]
        public static extern int GetID(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetX", CharSet = CharSet.Ansi)]
        public static extern void SetX(IntPtr intPtr, int x);

        [DllImport(policyManagerName, EntryPoint = "GetX", CharSet = CharSet.Ansi)]
        public static extern int GetX(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetY", CharSet = CharSet.Ansi)]
        public static extern void SetY(IntPtr intPtr, int y);

        [DllImport(policyManagerName, EntryPoint = "GetY", CharSet = CharSet.Ansi)]
        public static extern int GetY(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetWidth", CharSet = CharSet.Ansi)]
        public static extern void SetWidth(IntPtr intPtr, int width);

        [DllImport(policyManagerName, EntryPoint = "GetWidth", CharSet = CharSet.Ansi)]
        public static extern int GetWidth(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetHeight", CharSet = CharSet.Ansi)]
        public static extern void SetHeight(IntPtr intPtr, int height);

        [DllImport(policyManagerName, EntryPoint = "GetHeight", CharSet = CharSet.Ansi)]
        public static extern int GetHeight(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetMinWidth", CharSet = CharSet.Ansi)]
        public static extern void SetMinWidth(IntPtr intPtr, int height);

        [DllImport(policyManagerName, EntryPoint = "GetMinWidth", CharSet = CharSet.Ansi)]
        public static extern int GetMinWidth(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetMinHeight", CharSet = CharSet.Ansi)]
        public static extern void SetMinHeight(IntPtr intPtr, int height);

        [DllImport(policyManagerName, EntryPoint = "GetMinHeight", CharSet = CharSet.Ansi)]
        public static extern int GetMinHeight(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetMaxWidth", CharSet = CharSet.Ansi)]
        public static extern void SetMaxWidth(IntPtr intPtr, int height);

        [DllImport(policyManagerName, EntryPoint = "GetMaxWidth", CharSet = CharSet.Ansi)]
        public static extern int GetMaxWidth(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetMaxHeight", CharSet = CharSet.Ansi)]
        public static extern void SetMaxHeight(IntPtr intPtr, int height);

        [DllImport(policyManagerName, EntryPoint = "GetMaxHeight", CharSet = CharSet.Ansi)]
        public static extern int GetMaxHeight(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetPriority", CharSet = CharSet.Ansi)]
        public static extern void SetPriority(IntPtr intPtr, int priority);

        [DllImport(policyManagerName, EntryPoint = "GetPriority", CharSet = CharSet.Ansi)]
        public static extern int GetPriority(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetChildCount", CharSet = CharSet.Ansi)]
        public static extern void SetChildCount(IntPtr intPtr, int height);

        [DllImport(policyManagerName, EntryPoint = "GetChildCount", CharSet = CharSet.Ansi)]
        public static extern int GetChildCount(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetLayoutType", CharSet = CharSet.Ansi)]
        public static extern void SetLayoutType(IntPtr intPtr, LayoutTypes layoutType);

        [DllImport(policyManagerName, EntryPoint = "GetLayoutType", CharSet = CharSet.Ansi)]
        public static extern LayoutTypes GetLayoutType(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetAlign", CharSet = CharSet.Ansi)]
        public static extern void SetAlign(IntPtr intPtr, Aligns alignParam);

        [DllImport(policyManagerName, EntryPoint = "GetAlign", CharSet = CharSet.Ansi)]
        public static extern Aligns GetAlign(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetItemAlign", CharSet = CharSet.Ansi)]
        public static extern void SetItemAlign(IntPtr intPtr, Aligns alignParam);

        [DllImport(policyManagerName, EntryPoint = "GetItemAlign", CharSet = CharSet.Ansi)]
        public static extern Aligns GetItemAlign(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetItemGap", CharSet = CharSet.Ansi)]
        public static extern void SetItemGap(IntPtr intPtr, int itemGap);

        [DllImport(policyManagerName, EntryPoint = "GetItemGap", CharSet = CharSet.Ansi)]
        public static extern int GetItemGap(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetRearrangeGap", CharSet = CharSet.Ansi)]
        public static extern void SetRearrangeGap(IntPtr intPtr, int itemGap);

        [DllImport(policyManagerName, EntryPoint = "GetRearrangeGap", CharSet = CharSet.Ansi)]
        public static extern int GetRearrangeGap(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetWeight", CharSet = CharSet.Ansi)]
        public static extern void SetWeight(IntPtr intPtr, int weight);

        [DllImport(policyManagerName, EntryPoint = "GetWeight", CharSet = CharSet.Ansi)]
        public static extern int GetWeight(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetVisibility", CharSet = CharSet.Ansi)]
        public static extern void SetVisibility(IntPtr intPtr, bool v);

        [DllImport(policyManagerName, EntryPoint = "GetVisibility", CharSet = CharSet.Ansi)]
        public static extern bool GetVisibility(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetWidthResizePolicy", CharSet = CharSet.Ansi)]
        public static extern void SetWidthResizePolicy(IntPtr intPtr, ResizePolicyTypes policy);

        [DllImport(policyManagerName, EntryPoint = "GetWidthResizePolicy", CharSet = CharSet.Ansi)]
        public static extern ResizePolicyTypes GetWidthResizePolicy(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetHeightResizePolicy", CharSet = CharSet.Ansi)]
        public static extern void SetHeightResizePolicy(IntPtr intPtr, ResizePolicyTypes policy);

        [DllImport(policyManagerName, EntryPoint = "GetHeightResizePolicy", CharSet = CharSet.Ansi)]
        public static extern ResizePolicyTypes GetHeightResizePolicy(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetOmissionRule", CharSet = CharSet.Ansi)]
        public static extern void SetOmissionRules(IntPtr intPtr, OmissionRules policy);

        [DllImport(policyManagerName, EntryPoint = "GetOmissionRule", CharSet = CharSet.Ansi)]
        public static extern OmissionRules GetOmissionRules(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetRearrangeRule", CharSet = CharSet.Ansi)]
        public static extern void SetRearrangeRules(IntPtr intPtr, RearrangeRules v);

        [DllImport(policyManagerName, EntryPoint = "GetRearrangeRule", CharSet = CharSet.Ansi)]
        public static extern RearrangeRules GetRearrangeRules(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetIsLayout", CharSet = CharSet.Ansi)]
        public static extern void SetIsLayout(IntPtr intPtr, bool isLayout);

        [DllImport(policyManagerName, EntryPoint = "GetIsLayout", CharSet = CharSet.Ansi)]
        public static extern bool GetIsLayout(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "ClearChildSpecList", CharSet = CharSet.Ansi)]
        public static extern void ClearChildSpecList(IntPtr intPtr);


        [DllImport(policyManagerName, EntryPoint = "AddChildSpecPtr", CallingConvention = CallingConvention.Cdecl)]
        public extern static void AddChildSpec(IntPtr intPtr, IntPtr childSpecPtr);

        [DllImport(policyManagerName, EntryPoint = "GetChildSpecX", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetChildSpecX(IntPtr intPtr, int childIndex);

        [DllImport(policyManagerName, EntryPoint = "GetChildSpecY", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetChildSpecY(IntPtr intPtr, int childIndex);

        [DllImport(policyManagerName, EntryPoint = "GetChildSpecW", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetChildSpecW(IntPtr intPtr, int childIndex);

        [DllImport(policyManagerName, EntryPoint = "GetChildSpecH", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetChildSpecH(IntPtr intPtr, int childIndex);

        [DllImport(policyManagerName, EntryPoint = "GetChildSpecListCount", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetChildSpecListCount(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetIsRootLayout", CharSet = CharSet.Ansi)]
        public static extern void SetIsRootLayout(IntPtr intPtr, bool isRootLayout);

        [DllImport(policyManagerName, EntryPoint = "GetIsRootLayout", CharSet = CharSet.Ansi)]
        public static extern bool GetIsRootLayout(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetEnableExpandWidthToUIArea", CharSet = CharSet.Ansi)]
        public static extern void SetExpandWidthToUIArea(IntPtr intPtr, bool enableMargin);

        [DllImport(policyManagerName, EntryPoint = "GetEnableExpandWidthToUIArea", CharSet = CharSet.Ansi)]
        public static extern bool GetExpandWidthToUIArea(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetEnableRTL", CharSet = CharSet.Ansi)]
        public static extern void SetEnableRTL(IntPtr intPtr, bool enableRTL);

        [DllImport(policyManagerName, EntryPoint = "GetEnableRTL", CharSet = CharSet.Ansi)]
        public static extern bool GetEnableRTL(IntPtr intPtr);

        // policy manager
        [DllImport(policyManagerName, EntryPoint = "ApplyPolicy", CharSet = CharSet.Ansi)]
        public static extern void ApplyPolicy(IntPtr intPtr);


        // padding
        [DllImport(policyManagerName, EntryPoint = "BindPaddingToSpec", CharSet = CharSet.Ansi)]
        public static extern void BindPaddingToSpec(IntPtr intPtr, IntPtr paddingPtr);

        [DllImport(policyManagerName, EntryPoint = "NewPadding", CharSet = CharSet.Ansi)]
        public static extern IntPtr NewPadding();

        [DllImport(policyManagerName, EntryPoint = "DestroyPadding", CharSet = CharSet.Ansi)]
        public static extern void DestroyPadding(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetTopPadding", CharSet = CharSet.Ansi)]
        public static extern void SetTopPadding(IntPtr intPtr, int padding);

        [DllImport(policyManagerName, EntryPoint = "GetTopPadding", CharSet = CharSet.Ansi)]
        public static extern int GetTopPadding(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetBottomPadding", CharSet = CharSet.Ansi)]
        public static extern void SetBottomPadding(IntPtr intPtr, int padding);

        [DllImport(policyManagerName, EntryPoint = "GetBottomPadding", CharSet = CharSet.Ansi)]
        public static extern int GetBottomPadding(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetLeftPadding", CharSet = CharSet.Ansi)]
        public static extern void SetLeftPadding(IntPtr intPtr, int padding);

        [DllImport(policyManagerName, EntryPoint = "GetLeftPadding", CharSet = CharSet.Ansi)]
        public static extern int GetLeftPadding(IntPtr intPtr);


        [DllImport(policyManagerName, EntryPoint = "SetRightPadding", CharSet = CharSet.Ansi)]
        public static extern void SetRightPadding(IntPtr intPtr, int padding);

        [DllImport(policyManagerName, EntryPoint = "GetRightPadding", CharSet = CharSet.Ansi)]
        public static extern int GetRightPadding(IntPtr intPtr);


        [DllImport(policyManagerName, EntryPoint = "SetAllPadding", CharSet = CharSet.Ansi)]
        public static extern void SetAllPadding(IntPtr intPtr, int padding);

        [DllImport(policyManagerName, EntryPoint = "GetAllPadding", CharSet = CharSet.Ansi)]
        public static extern int GetAllPadding(IntPtr intPtr);

        // margin
        [DllImport(policyManagerName, EntryPoint = "BindMarginToSpec", CharSet = CharSet.Ansi)]
        public static extern void BindMarginToSpec(IntPtr intPtr, IntPtr marginPtr);

        [DllImport(policyManagerName, EntryPoint = "NewMargin", CharSet = CharSet.Ansi)]
        public static extern IntPtr NewMargin();

        [DllImport(policyManagerName, EntryPoint = "DestroyMargin", CharSet = CharSet.Ansi)]
        public static extern void DestroyMargin(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetTopMargin", CharSet = CharSet.Ansi)]
        public static extern void SetTopMargin(IntPtr intPtr, int margin);

        [DllImport(policyManagerName, EntryPoint = "GetTopMargin", CharSet = CharSet.Ansi)]
        public static extern int GetTopMargin(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetBottomMargin", CharSet = CharSet.Ansi)]
        public static extern void SetBottomMargin(IntPtr intPtr, int margin);

        [DllImport(policyManagerName, EntryPoint = "GetBottomMargin", CharSet = CharSet.Ansi)]
        public static extern int GetBottomMargin(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetLeftMargin", CharSet = CharSet.Ansi)]
        public static extern void SetLeftMargin(IntPtr intPtr, int margin);

        [DllImport(policyManagerName, EntryPoint = "GetLeftMargin", CharSet = CharSet.Ansi)]
        public static extern int GetLeftMargin(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetRightMargin", CharSet = CharSet.Ansi)]
        public static extern void SetRightMargin(IntPtr intPtr, int margin);

        [DllImport(policyManagerName, EntryPoint = "GetRightMargin", CharSet = CharSet.Ansi)]
        public static extern int GetRightMargin(IntPtr intPtr);




        //NativeTableLayoutInfo
        [DllImport(policyManagerName, EntryPoint = "SetRowCount", CharSet = CharSet.Ansi)]
        public static extern void SetRowCount(IntPtr intPtr, int v);

        [DllImport(policyManagerName, EntryPoint = "GetRowCount", CharSet = CharSet.Ansi)]
        public static extern int GetRowCount(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetColumnCount", CharSet = CharSet.Ansi)]
        public static extern void SetColumnCount(IntPtr intPtr, int v);

        [DllImport(policyManagerName, EntryPoint = "GetColumnCount", CharSet = CharSet.Ansi)]
        public static extern int GetColumnCount(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetRowIndex", CharSet = CharSet.Ansi)]
        public static extern void SetRowIndex(IntPtr intPtr, int v);

        [DllImport(policyManagerName, EntryPoint = "GetRowIndex", CharSet = CharSet.Ansi)]
        public static extern int GetRowIndex(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetColumnIndex", CharSet = CharSet.Ansi)]
        public static extern void SetColumnIndex(IntPtr intPtr, int v);

        [DllImport(policyManagerName, EntryPoint = "GetColumnIndex", CharSet = CharSet.Ansi)]
        public static extern int GetColumnIndex(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetRowSpan", CharSet = CharSet.Ansi)]
        public static extern void SetRowSpan(IntPtr intPtr, int v);

        [DllImport(policyManagerName, EntryPoint = "GetRowSpan", CharSet = CharSet.Ansi)]
        public static extern int GetRowSpan(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "SetColumnSpan", CharSet = CharSet.Ansi)]
        public static extern void SetColumnSpan(IntPtr intPtr, int v);

        [DllImport(policyManagerName, EntryPoint = "GetColumnSpan", CharSet = CharSet.Ansi)]
        public static extern int GetColumnSpan(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "NewTableLayoutInfo", CharSet = CharSet.Ansi)]
        public static extern IntPtr NewTableLayoutInfo();

        [DllImport(policyManagerName, EntryPoint = "DestroyTableLayoutInfo", CharSet = CharSet.Ansi)]
        public static extern void DestroyTableLayoutInfo(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "NewTableLayoutItemInfo", CharSet = CharSet.Ansi)]
        public static extern IntPtr NewTableLayoutItemInfo();

        [DllImport(policyManagerName, EntryPoint = "DestroyTableLayoutItemInfo", CharSet = CharSet.Ansi)]
        public static extern void DestroyTableLayoutItemInfo(IntPtr intPtr);

        [DllImport(policyManagerName, EntryPoint = "BindTableLayoutInfoToSpec", CharSet = CharSet.Ansi)]
        public static extern void BindTableLayoutInfoToSpec(IntPtr intPtr, IntPtr tableLayoutInfoPtr);

        [DllImport(policyManagerName, EntryPoint = "BindTableLayoutItemInfoToSpec", CharSet = CharSet.Ansi)]
        public static extern void BindTableLayoutItemInfoToSpec(IntPtr intPtr, IntPtr tableLayoutItemInfoPtr);

        [DllImport(policyManagerName, EntryPoint = "RegisterHeightMeasureFunction", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RegisterHeightMeasureFunction(IntPtr intPtr, Spec.MeasureHeightCallback func);

        [DllImport(policyManagerName, EntryPoint = "RegisterWidthMeasureFunction", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RegisterWidthMeasureFunction(IntPtr intPtr, Spec.MeasureWidthCallback func);

        private const string policyManagerName = Config.TizenNativeLibPath + "libfluxpolicymanager.so.0";

    }
}
