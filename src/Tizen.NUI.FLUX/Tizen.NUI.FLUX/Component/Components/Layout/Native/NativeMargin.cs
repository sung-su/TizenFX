/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
using System;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// The margin for layout.
    /// The margin will be active in two cases.
    /// 1. The left and right margin of the RootLayout;
    /// 2. The child of the Frame layout.
    /// </summary>
    internal class NativeMargin : IDisposable
    {

        internal NativeMargin()
        {
            ptr = WrapNativePolicyManager.NewMargin();
        }

        internal IntPtr GetMarginPtr()
        {
            return ptr;
        }

        // Becouse of inherit IDisposable, cant change permission.
        public void Dispose()
        {
            WrapNativePolicyManager.DestroyMargin(ptr);
        }

        internal int? LeftMargin
        {
            get
            {
                return WrapNativePolicyManager.GetLeftMargin(ptr);
            }
            set
            {
                WrapNativePolicyManager.SetLeftMargin(ptr, value.Value);
            }
        }

        internal int? RightMargin
        {
            get
            {
                return WrapNativePolicyManager.GetRightMargin(ptr);
            }
            set
            {
                WrapNativePolicyManager.SetRightMargin(ptr, value.Value);
            }
        }

        internal int? TopMargin
        {
            get
            {
                return WrapNativePolicyManager.GetTopMargin(ptr);
            }
            set
            {
                WrapNativePolicyManager.SetTopMargin(ptr, value.Value);
            }
        }

        internal int? BottomMargin
        {
            get
            {
                return WrapNativePolicyManager.GetBottomMargin(ptr);
            }
            set
            {
                WrapNativePolicyManager.SetBottomMargin(ptr, value.Value);
            }
        }

        private IntPtr ptr; // the address value in C++
    }
}
