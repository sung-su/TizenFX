using System;

namespace Tizen.NUI.FLUX.Component
{
    internal class NativeTableLayoutItemInfo : IDisposable
    {
        internal NativeTableLayoutItemInfo()
        {
            ptr = WrapNativePolicyManager.NewTableLayoutItemInfo();
        }

        internal IntPtr GetPtr()
        {
            return ptr;
        }

        public void Dispose()
        {
            WrapNativePolicyManager.DestroyTableLayoutItemInfo(ptr);
        }
        internal int ColumnIndex
        {
            get => WrapNativePolicyManager.GetColumnIndex(ptr);
            set => WrapNativePolicyManager.SetColumnIndex(ptr, value);
        }

        internal int RowIndex
        {
            get => WrapNativePolicyManager.GetRowIndex(ptr);
            set => WrapNativePolicyManager.SetRowIndex(ptr, value);
        }

        internal int ColumnSpan
        {
            get => WrapNativePolicyManager.GetColumnSpan(ptr);
            set => WrapNativePolicyManager.SetColumnSpan(ptr, value);
        }

        internal int RowSpan
        {
            get => WrapNativePolicyManager.GetRowSpan(ptr);
            set => WrapNativePolicyManager.SetRowSpan(ptr, value);
        }
        private IntPtr ptr;
    }
}