using System;

namespace Tizen.NUI.FLUX.Component
{
    internal class NativeTableLayoutInfo : IDisposable
    {
        internal NativeTableLayoutInfo()
        {
            ptr = WrapNativePolicyManager.NewTableLayoutInfo();
        }
        internal IntPtr GetPtr()
        {
            return ptr;
        }

        public void Dispose()
        {
            WrapNativePolicyManager.DestroyTableLayoutInfo(ptr);
        }
        internal int ColumnCount
        {
            get => WrapNativePolicyManager.GetColumnCount(ptr);
            set => WrapNativePolicyManager.SetColumnCount(ptr, value);
        }

        internal int RowCount
        {
            get => WrapNativePolicyManager.GetRowCount(ptr);
            set => WrapNativePolicyManager.SetRowCount(ptr, value);
        }
        private IntPtr ptr;
    }
}