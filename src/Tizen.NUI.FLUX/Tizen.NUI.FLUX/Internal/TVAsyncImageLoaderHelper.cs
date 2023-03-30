/// @file TVAsyncImageLoaderHelper.cs
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
    /// <summary>
    /// Wrapper Class for Native AsyncImageLoader.
    /// Connect AsyncLoadComplete event to get callback when image load complete,
    /// AsyncLoadEventArgs provide the pixeldata and LoadId for loaded image.
    /// </summary>
    /// <code>
    /// // Your callback for AsyncLoadComplete
    /// private void OnPixelDataLoadFinish(object sender, TVAsyncImageLoadHelper.AsyncLoadEventArgs e)
    /// {
    ///   uint loadId = e.LoadId
    ///   PixelData pixelData =  e.PixelData
    /// }
    /// 
    /// TVAsyncImageLoadHelper tvasyncImageLoadHelper = new TVAsyncImageLoadHelper();
    /// tvasyncImageLoadHelper.AsyncLoadComplete += OnPixelDataLoadFinish;
    /// uint loadId = tvasyncImageLoadHelper.Load(url);
    /// </code>
    internal class TVAsyncImageLoadHelper : NUIDisposable
    {
        /// <summary>
        /// EventArgument class for AsyncLoadComplete Event
        /// </summary>
        public class AsyncLoadEventArgs : EventArgs
        {
            /// <summary> Load Id for Image  </summary>
            public uint LoadId { get; }

            /// <summary> 
            /// Pixeldata of loaded image
            /// if load failed than pixeldata will be null.
            /// </summary>
            public PixelData PixelData { get; }

            public AsyncLoadEventArgs(uint loadId, PixelData pixelData)
            {
                this.LoadId = loadId;
                this.PixelData = pixelData;
            }
        }

        private readonly HandleRef swigCptr;
        private readonly HandleRef swigCptrSignal;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void PixelDataLoadFinish(UInt32 id, IntPtr pixelDataHandle);
        private PixelDataLoadFinish pixelDataLoadFinishCallback;

        private event EventHandler<AsyncLoadEventArgs> AsyncLoadCompleteHandler;

        public TVAsyncImageLoadHelper()
        {
            IntPtr handle = Interop.TVAsyncImageLoadHelper.New();
            swigCptr = new HandleRef(this, handle);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            swigCptrSignal = GetLoadCompleteSignal();
        }


        private HandleRef GetLoadCompleteSignal()
        {
            IntPtr signalHandle = Interop.TVAsyncImageLoadHelper.LoadCompleteSignal(swigCptr);
            HandleRef signal = new HandleRef(this, signalHandle);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            return signal;
        }

        private bool SignalEmpty()
        {
            bool result = Interop.TVAsyncImageLoadHelper.SignalEmpty(swigCptrSignal);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            return result;
        }

        private void Connect(IntPtr funcPtr)
        {
            Interop.TVAsyncImageLoadHelper.Connect(swigCptrSignal, funcPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        private void Disconnect(IntPtr funcPtr)
        {
            Interop.TVAsyncImageLoadHelper.Disconnect(swigCptrSignal, funcPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        private uint GetLoadTaskID(string url, Uint16Pair uSize)
        {
            uint loadTaskId = Interop.TVAsyncImageLoadHelper.LoadImage(swigCptr, url, Uint16Pair.getCPtr(uSize), (int)(FittingModeType.ShrinkToFit), (int)(SamplingModeType.BoxThenLinear), true);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            return loadTaskId;
        }

        /// <summary>
        /// ASyncLoadComplete Event
        /// Must Connect to get pixel data of loaded image.
        /// </summary>
        public event EventHandler<AsyncLoadEventArgs> AsyncLoadComplete
        {
            add
            {
                if (pixelDataLoadFinishCallback == null)
                {
                    pixelDataLoadFinishCallback = OnPixelDataLoadFinish;
                    IntPtr funcPtr = Marshal.GetFunctionPointerForDelegate(pixelDataLoadFinishCallback);
                    Connect(funcPtr);
                }
                AsyncLoadCompleteHandler += value;
            }

            remove
            {
                AsyncLoadCompleteHandler -= value;
                if (AsyncLoadCompleteHandler == null &&  SignalEmpty() == false)
                {
                    IntPtr funcPtr = Marshal.GetFunctionPointerForDelegate(pixelDataLoadFinishCallback);
                    Disconnect(funcPtr);
                }
            }
        }

        private void OnPixelDataLoadFinish(uint id, IntPtr pixelDataHandle)
        {
            PixelData pixelData = new PixelData(pixelDataHandle, true);
            AsyncLoadEventArgs eventArgs = new AsyncLoadEventArgs(id, pixelData);
            AsyncLoadCompleteHandler?.Invoke(this, eventArgs);
        }

        /// <summary>
        ///  Load Image using async image loader
        ///  Must be connected to AsyncLoadComplete Event to get pixeldata before Load
        /// </summary>
        /// <param name="url"> The URL of the image file to load </param>
        /// <returns>  the loading task id </returns>
        public uint Load(string url)
        {
            return GetLoadTaskID(url, new Uint16Pair(0, 0));
        }

        /// <summary>
        ///  Load Image using async image loader
        ///  Must be connected to AsyncLoadComplete Event to get pixeldata before Load
        /// </summary>
        /// <param name="url"> The URL of the image file to load </param>
        /// <param name="size"> The width and height to fit the loaded image to, 0.0 means whole image </param>
        /// <returns>  the loading task id </returns>
        public uint Load(string url, Size2D size)
        {
            return GetLoadTaskID(url, new Uint16Pair((uint)size.Width, (uint)size.Height));
        }
     
        /// <summary>
        ///  Load Image using async image loader
        ///  Must be connected to AsyncLoadComplete Event to get pixeldata before Load
        /// </summary>
        /// <param name="url">The URL of the image file to load.</param>
        /// <param name="size">The width and height to fit the loaded image to, 0.0 means whole image</param>
        /// <param name="fittingMode">The method used to fit the shape of the image before loading to the shape defined by the size parameter.</param>
        /// <param name="samplingMode">The filtering method used when sampling pixels from the input image while fitting it to desired size.</param>
        /// <param name="orientationCorrection">Reorient the image to respect any orientation metadata in its header.</param>
        /// <returns> the loading task id </returns>
        public uint Load(string url, Size2D size, FittingModeType fittingMode, SamplingModeType samplingMode, bool orientationCorrection)
        {
            var uSize = new Uint16Pair((uint)size.Width, (uint)size.Height);
            uint loadTaskId = Interop.TVAsyncImageLoadHelper.LoadImage(swigCptr, url, Uint16Pair.getCPtr(uSize), (int)fittingMode, (int)samplingMode, orientationCorrection);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            return loadTaskId;
        }

        /// <summary>
        /// Cancels an image loading task if it is still queueing in the work thread.
        /// </summary>
        /// <param name="loadingTaskId">loadingTaskId The task id returned when invoking the load call.</param>
        /// <returns> true If the loading task is removed from the queue, false otherwise the loading is already implemented and unable to cancel anymore</returns>
        public bool Cancel(uint loadingTaskId)
        {
            bool result = Interop.TVAsyncImageLoadHelper.Cancel(swigCptr, loadingTaskId);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            return result;
        }

        /// <summary>
        /// Cancels all the loading task in the queue.
        /// </summary>
        public void CancelAll()
        {
            Interop.TVAsyncImageLoadHelper.CancelAll(swigCptr);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.
            }

            //Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.
            if (SignalEmpty() == false)
            {
                IntPtr funcPtr = Marshal.GetFunctionPointerForDelegate(pixelDataLoadFinishCallback);
                Disconnect(funcPtr);
            }
            if (this != null)
            {
                Interop.TVAsyncImageLoadHelper.Delete(swigCptr);
            }
        }
    }
}
