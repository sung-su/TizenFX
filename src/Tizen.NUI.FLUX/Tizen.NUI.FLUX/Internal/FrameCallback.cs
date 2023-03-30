/// @file FrameCallback.cs
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


using System;
using System.Runtime.InteropServices;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{

    internal class FrameCallback : NUIDisposable
    {
        Interop.FrameCallback.NativeFrameCallback nativeCallback;
        private EventHandler<FrameCallbackEventArgs> _frameCallback;
        private HandleRef handleRef;

        public FrameCallback()
        {
            SecurityUtil.CheckPlatformPrivileges();                        
            handleRef = new HandleRef(this, Interop.FrameCallback.New());
            if (handleRef.Handle == IntPtr.Zero)
            {                
                throw new ArgumentException("Unable to create callback object");
            }

            nativeCallback = NativeFrameCallbackFunction;
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (_frameCallback != null)
            {
                Interop.FrameCallback.RemoveCallback(handleRef.Handle);
                nativeCallback = null;
                _frameCallback = null;
            }

            if (handleRef.Handle != null)
            {                
                Interop.FrameCallback.Delete(handleRef.Handle);
                handleRef = new HandleRef(this, IntPtr.Zero);
            }            

            disposed = true;
        }

        public event EventHandler<FrameCallbackEventArgs> Update
        {
            add
            {
                if (_frameCallback == null)
                {
                    Interop.FrameCallback.AddCallback(handleRef.Handle, nativeCallback);
                    _frameCallback += value;
                }
            }
            remove
            {
                if (_frameCallback != null)
                {
                    Interop.FrameCallback.RemoveCallback(handleRef.Handle);
                    _frameCallback -= value;
                }
            }
        }

        public bool GetScale(uint id, Vector3 scale)
        {
            return Interop.FrameCallback.GetScale(handleRef.Handle, id, Vector3.getCPtr(scale));
        }

        public bool BakeScale(uint id, Vector3 newScale)
        {
            return Interop.FrameCallback.BakeScale(handleRef.Handle, id, Vector3.getCPtr(newScale));
        }

        public bool GetPosition(uint id, Vector3 position)
        {
            return Interop.FrameCallback.GetPosition(handleRef.Handle, id, Vector3.getCPtr(position));
        }

        public bool BakePosition(uint id, Vector3 size)
        {
            return Interop.FrameCallback.BakePosition(handleRef.Handle, id, Vector3.getCPtr(size));
        }

        public bool GetColor(uint id, Vector4 color)
        {
            return Interop.FrameCallback.GetColor(handleRef.Handle, id, Vector4.getCPtr(color));
        }

        public bool BakeColor(uint id, Vector4 color)
        {
            return Interop.FrameCallback.BakeColor(handleRef.Handle, id, Vector4.getCPtr(color));
        }

        private void NativeFrameCallbackFunction(float elapsedSeconds)
        {            
            _frameCallback?.Invoke(this, new FrameCallbackEventArgs(elapsedSeconds));
        }
    }
}
