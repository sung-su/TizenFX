using System;
using System.Runtime.InteropServices;

namespace Tizen.NUI.FLUX
{
  internal static partial class Interop
  {
    internal static class Ecore
    {
      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate bool EcoreTaskCallback(IntPtr data);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      internal delegate void EcoreCallback(IntPtr data);

      [DllImport(Libraries.Ecore, EntryPoint = "ecore_idler_add", CallingConvention = CallingConvention.Cdecl)]
      internal static extern IntPtr EcoreIdlerAdd(EcoreTaskCallback callback, IntPtr data);

      [DllImport(Libraries.Ecore, EntryPoint = "ecore_idler_del", CallingConvention = CallingConvention.Cdecl)]
      internal static extern void EcoreIdlerDelete(IntPtr idler);

      [DllImport(Libraries.Ecore, EntryPoint = "ecore_main_loop_thread_safe_call_async", CallingConvention = CallingConvention.Cdecl)]
      internal static extern void EcoreMainLoopThreadSafeCallAsync(EcoreCallback callback, IntPtr data);

    }
  }
}
