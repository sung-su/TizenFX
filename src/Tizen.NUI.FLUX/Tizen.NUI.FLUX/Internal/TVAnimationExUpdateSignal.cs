/// @file TVAnimationExUpdateSignal.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.6.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
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



using System.Runtime.InteropServices;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{

    internal class AnimationExUpdateSignal : NUIDisposable
    {
        private HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal AnimationExUpdateSignal(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new HandleRef(this, cPtr);
        }

        internal static HandleRef GetCPtr(AnimationExUpdateSignal obj)
        {
            return (obj == null) ? new HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {

            }

            lock (this)
            {
                if (swigCPtr.Handle != global::System.IntPtr.Zero)
                {
                    if (swigCMemOwn)
                    {
                        swigCMemOwn = false;
                        Interop.AnimationEX.UpdateSignal_Delete(swigCPtr);
                    }
                    swigCPtr = new HandleRef(null, global::System.IntPtr.Zero);
                }
                global::System.GC.SuppressFinalize(this);
            }

            disposed = true;
        }

        public bool Empty()
        {
            bool ret = Interop.AnimationEX.UpdateSignal_Empty(swigCPtr);
            return ret;
        }

        public uint GetConnectionCount()
        {
            uint ret = Interop.AnimationEX.UpdateSignal_GetConnectionCount(swigCPtr);
            return ret;
        }

        public void Connect(global::System.Delegate func)
        {
            global::System.IntPtr ip = Marshal.GetFunctionPointerForDelegate(func);
            {
                Interop.AnimationEX.UpdateSignal_Connect(swigCPtr, new global::System.Runtime.InteropServices.HandleRef(this, ip));
            }
        }

        public void Disconnect(global::System.Delegate func)
        {
            global::System.IntPtr ip = global::System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(func);
            {
                Interop.AnimationEX.UpdateSignal_Disconnect(swigCPtr, new global::System.Runtime.InteropServices.HandleRef(this, ip));
            }
        }
        
        public AnimationExUpdateSignal() : this(Interop.AnimationEX.UpdateSignal_New(), true)
        {

        }
    }

}
