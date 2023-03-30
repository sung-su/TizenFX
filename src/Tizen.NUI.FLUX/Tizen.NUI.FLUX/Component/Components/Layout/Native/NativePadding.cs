/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
using System;

namespace Tizen.NUI.FLUX.Component
{

    /// <summary>
    /// The padding of layout.
    /// </summary>
    internal class NativePadding : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        internal NativePadding()
        {
            ptr = WrapNativePolicyManager.NewPadding();
        }

        internal IntPtr GetPaddingPtr()
        {
            return ptr;
        }

        public void Dispose()
        {
            WrapNativePolicyManager.DestroyPadding(ptr);
        }

        internal int? Left
        {
            get
            {
                return WrapNativePolicyManager.GetLeftPadding(ptr);
            }
            set
            {
                WrapNativePolicyManager.SetLeftPadding(ptr, value.Value);
            }
        }

        internal int? Right
        {
            get
            {
                return WrapNativePolicyManager.GetRightPadding(ptr);
            }
            set
            {
                WrapNativePolicyManager.SetRightPadding(ptr, value.Value);
            }
        }

        internal int? Top
        {
            get
            {
                return WrapNativePolicyManager.GetTopPadding(ptr);
            }
            set
            {
                WrapNativePolicyManager.SetTopPadding(ptr, value.Value);
            }
        }

        internal int? Bottom
        {
            get
            {
                return WrapNativePolicyManager.GetBottomPadding(ptr);
            }
            set
            {
                WrapNativePolicyManager.SetBottomPadding(ptr, value.Value);
            }
        }

        internal int? All
        {
            get
            {
                return WrapNativePolicyManager.GetAllPadding(ptr);
            }
            set
            {
                WrapNativePolicyManager.SetAllPadding(ptr, value.Value);
            }
        }
        private IntPtr ptr;
    }
}
