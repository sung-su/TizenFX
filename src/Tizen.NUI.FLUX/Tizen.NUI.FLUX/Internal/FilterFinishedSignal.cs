/// @file FilterFinishedSignal.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 8.8.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
/// PROPRIETARY/CONFIDENTIAL 
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
using System.Runtime.InteropServices;
using Tizen.NUI;
namespace Tizen.NUI.FLUX
{
    internal class FilterFinishedSignal : NUIDisposable
    {
        private HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal FilterFinishedSignal(IntPtr nativeHandle, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new HandleRef(this, nativeHandle);
        }

        internal static HandleRef GetCPtr(FilterFinishedSignal obj)
        {
            return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (swigCPtr.Handle != IntPtr.Zero)
            {
                if (swigCMemOwn)
                {
                    swigCMemOwn = false;
                    Interop.ImageUtility.FilterFinishSignalDelete(swigCPtr);
                }
                swigCPtr = new HandleRef(null, IntPtr.Zero);
            }

            disposed = true;
        }

        public void Connect(Delegate func)
        {
            IntPtr functionPointer = Marshal.GetFunctionPointerForDelegate(func);

            Interop.ImageUtility.FilterFinishSignalSignalConnect(swigCPtr, new HandleRef(this, functionPointer));
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        public void Disconnect(Delegate func)
        {
            IntPtr functionPointer = Marshal.GetFunctionPointerForDelegate(func);

            Interop.ImageUtility.FilterFinishSignalDisconnect(swigCPtr, new HandleRef(this, functionPointer));
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }
    }
}