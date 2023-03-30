/// @file ImageBuffer.cs
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
using System.Threading;
using System.Threading.Tasks;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// ImageBufferSafeHandle class
    /// </summary>
    /// <code>
    /// ImageBufferSafeHandle handle = new ImageBufferSafeHandle(nativeHandle);
    /// </code>
    internal class ImageBufferSafeHandle : SafeHandle
    {
        internal ImageBufferSafeHandle(IntPtr existingHandle) : base(IntPtr.Zero, true)
        {
            SetHandle(existingHandle);
        }

        /// <summary>
        /// To check if the handle is valid or not
        /// </summary>
        /// <returns>if it is false, the safehandle is invalid</returns>
        public override bool IsInvalid => handle == IntPtr.Zero;

        /// <summary>
        /// Function overriding to define how to release the internal native handle
        /// </summary>
        /// <returns> if it succeed, the function will retrive true</returns>
        protected override bool ReleaseHandle()
        {
            //cleanup native resource
            Interop.ImageUtility.Delete(handle);
            SetHandle(IntPtr.Zero);
            return true;
        }
    }

    /// <summary>
    /// Application can use raw buffer data of image. \n 
    /// ImageBuffer load Image internally, and provide Image manipulation operation.
    /// like flip, Rotate and Encode to File.
    /// </summary>
    /// <code>
    ///  imageBuffer = new ImageBuffer("a.jpg");
    ///  imageBuffer.Rotate(RotationType.Rotate90);
    ///  imageBuffer.Flip(FlipType.Horizontal);
    ///  imageView.SetImageBuffer(imageBuffer);
    /// </code>
    public class ImageBuffer : NUIDisposable
    {
        private readonly ImageBufferSafeHandle imageBufferSafeHandle;

        private string textureUrl;

        /// <summary>
        /// It is called automatically before the first instance is created or any static members are referenced.
        /// </summary>
        static ImageBuffer()
        {
            SecurityUtil.CheckPlatformPrivileges();
        }

        /// <summary>
        /// Constructor to instantiate the ImageBuffer class. Can add image url
        /// You can't change url in runtime.
        /// </summary>
        /// <param name="url"> The url is that you want to apply Imagebuffer function </param>
        /// <exception cref='ArgumentException'>
        /// Image Path is wrong. Check your image Path
        /// </exception>
        public ImageBuffer(string url)
        {
            IntPtr intPtrUrl = Interop.ImageUtility.NewWithUrl(url);
            if (intPtrUrl == IntPtr.Zero)
            {
                CLog.Error("Image path is wrong. Check your image path: %s1", s1: url);
                throw new ArgumentException("Image Path is wrong. Check your image Path : " + url);
            }
            imageBufferSafeHandle = new ImageBufferSafeHandle(intPtrUrl);
            CLog.Debug("Constructor ImageBuffer image path : %s1", s1: url);
        }

        /// <summary>
        /// Constructor to instantiate the ImageBuffer class. Can set image buffer
        /// </summary>
        /// <version> 6.6.0 </version>
        /// <param name="buffer"> The buffer is that you want to set in Imagebuffer function </param>
        /// <param name="bufferSize"> Size of the buffer </param>
        /// <param name="width"> Width of  image </param>
        /// <param name="height"> Height of  image </param>
        /// <param name="pixelFormat"> Pixel Format of image </param>
        /// <exception cref="ArgumentNullException"> Thrown when the user input parameters are null</exception> 
        /// <exception cref="ArgumentException"> Thrown when the user input parameters are wrong</exception> 
        public ImageBuffer(byte[] buffer, uint bufferSize, uint width, uint height, PixelFormat pixelFormat)
        {
            CheckBufferValidity(buffer, bufferSize, width, height, pixelFormat);

            IntPtr nativeBuffer = Marshal.AllocHGlobal((int)bufferSize);
            Marshal.Copy(buffer, 0, nativeBuffer, (int)bufferSize);

            IntPtr imageUtilityPtr = Interop.ImageUtility.NewWithImageBuffer(nativeBuffer, bufferSize, width, height, pixelFormat, ReleaseFunction.Free, false);

            if (imageUtilityPtr.Equals(IntPtr.Zero))
            {
                throw new ArgumentException("Need to check the input parameters.");
            }

            imageBufferSafeHandle = new ImageBufferSafeHandle(imageUtilityPtr);
            CLog.Debug("Constructor ImageBuffer set buffer : %d1 x %d2, buffer size: %d3"
                , d1: width
                , d2: height
                , d3: bufferSize
                );
        }

        /// <summary>
        /// Constructor to instantiate the ImageBuffer class. Can set image buffer
        /// </summary>
        /// <version> 6.6.0 </version>
        /// <param name="buffer"> The buffer is that you want to set in Imagebuffer function </param>
        /// <param name="bufferSize"> Size of the buffer </param>
        /// <param name="width"> Width of  image </param>
        /// <param name="height"> Height of  image </param>
        /// <param name="pixelFormat"> Pixel Format of image </param>
        /// <param name="releaseFunction"> ReleaseFunction to free the buffer. If application has allocated memory using malloc then free and if
        /// memory is allocated using new then delete. </param>
        /// <param name="clone"> If true ownership of buffer is with application and it should release the buffer to prevent memory leak.
        /// If false ownership of buffer is transferred to  Imagebuffer(i.e NUI), therefore buffer should not be modified and released by application.</param>
        /// <exception cref="ArgumentNullException"> Thrown when the user input parameters are null</exception> 
        /// <exception cref="ArgumentException"> Thrown when the user input parameters are wrong</exception> 
        public ImageBuffer(IntPtr buffer, uint bufferSize, uint width, uint height, PixelFormat pixelFormat, ReleaseFunction releaseFunction, bool clone)
        {
            CheckBufferValidity(buffer, bufferSize, width, height, pixelFormat);

            IntPtr imageUtilityPtr = Interop.ImageUtility.NewWithImageBuffer(buffer, bufferSize, width, height, pixelFormat, releaseFunction, clone);

            if (imageUtilityPtr.Equals(IntPtr.Zero))
            {
                throw new ArgumentException("Need to check the input parameters.");
            }

            imageBufferSafeHandle = new ImageBufferSafeHandle(imageUtilityPtr);
            CLog.Debug("Constructor ImageBuffer set buffer : %d1 x %d2, buffer size: %d3"
                , d1: width
                , d2: height
                , d3: bufferSize
                );
        }

        /// <summary>
        /// Get size width and height of Image.
        /// </summary>
        /// <returns> Size2D width and height of Image</returns>
        public Size2D GetSize()
        {
            return new Vector2(Interop.ImageUtility.GetSize(imageBufferSafeHandle), true);
        }

        /// <summary>
        /// Get PixelFormat of Image.
        /// </summary>
        /// <returns> PixelFormat of Image.</returns>
        public PixelFormat GetPixelFormat()
        {
            return Interop.ImageUtility.GetPixelFormat(imageBufferSafeHandle);
        }

        /// <summary>
        /// Get BytePerPixel of Image.
        /// </summary>
        /// <returns> BytePerPixel of Image.</returns>
        public uint GetBytePerPixel()
        {
            return Interop.ImageUtility.GetBytePerPixel(imageBufferSafeHandle);
        }

        /// <summary>
        /// EncodeToFile Save the manipulated pixel buffer to . The two valid encoding are (".jpeg"|".jpg") and ".png".
        /// </summary>
        /// <param name="filePath"> filename Identify the filesytem location at which to write the encoded image. </param>
        /// <exception cref='InvalidOperationException'>
        /// Can't save file, check filePath
        /// </exception>
        /// <returns>Ture is success that save image file. False isn't </returns>
        public void SaveToFile(string filePath)
        {
            if (Interop.ImageUtility.EncodeToFile(imageBufferSafeHandle, filePath) != true)
            {
                throw new InvalidOperationException("Can't save file, check filePath : " + filePath);
            }
            CLog.Debug("filePath : %s1", s1: filePath);
        }

        /// <summary>
        /// Get raw pixel buffer
        /// </summary>
        /// <returns>The pixel buffer, or NULL.</returns>
        public IntPtr GetBuffer()
        {
            return Interop.ImageUtility.GetBuffer(imageBufferSafeHandle);
        }

        /// <summary>
        /// texture URL of image texture.
        /// </summary>
        /// <returns>texture URL</returns>
        internal string GetTextureUrl()
        {
            PropertyValue textureUrlPropertyValue = new PropertyValue(Interop.ImageUtility.GetTextureUrl(imageBufferSafeHandle), true);
            textureUrlPropertyValue.Get(out textureUrl);
            CLog.Debug("textureUrl : %s1", s1: textureUrl);
            return textureUrl;
        }

        /// <summary>
        /// Flip the image according to FlipType.
        /// </summary>
        /// <param name="flipType"> flip type are VERTICAL, HORIZONTAL </param>
        public void Flip(FlipType flipType)
        {
            CLog.Debug("FlipType : %s1", s1: CLog.EnumToString(flipType));
            Interop.ImageUtility.Flip(imageBufferSafeHandle, flipType);
        }

        /// <summary>
        /// Rotate the image according to RotationType.
        /// </summary>
        /// <param name="rotateType"> rotation are 0, 90, 180 and 270 in Counterclockwise direction </param>
        public void Rotate(RotationType rotateType)
        {
            CLog.Debug("RotationType : %s1", s1: CLog.EnumToString(rotateType));
            Interop.ImageUtility.Rotate(imageBufferSafeHandle, rotateType);
        }

        /// <summary>
        /// Scale the image according to Interpolation Type.
        /// </summary>
        /// <version> 6.6.0 </version>
        /// <param name="scaledWidth"> width for scaled Image </param>
        /// <param name="scaledHeight"> height for scaled Image </param>
        /// <param name="interpolationType">Interpolation type for scaling operation</param>
        public void Scale(uint scaledWidth, uint scaledHeight, Interpolation interpolationType = Interpolation.BiLinear)
        {
            CLog.Debug("Scale Width: %d1, Scale Height: %d2, Interpolation Type: %s1"
                , d1: scaledWidth
                , d2: scaledHeight
                , s1: CLog.EnumToString(interpolationType)
                );
            Interop.ImageUtility.Scale(imageBufferSafeHandle, scaledWidth, scaledHeight, interpolationType);
        }

        /// <summary>
        /// Perform Fast Box Blur on the image.
        /// </summary>
        /// <param name="kernelSize">The size of the blur kernel (number of samples).</param>
        /// <param name="downSampleScale">The scale factor applied during the blur process</param>
        internal void BoxFilterBlur(uint kernelSize, float downSampleScale = 1.0f)
        {
            Size2D size = GetSize();
            float width = size.Width * downSampleScale;
            float height = size.Height * downSampleScale;
            if (width <= 1.0f || height <= 1.0f)
            {
                CLog.Error("Changing downSampleScale value from %f1 to 1.0f since destination buffer width[%f2] height[%f3] is becoming less than 1.0f"
                    , f1: downSampleScale
                    , f2: width
                    , f3: height
                    );
                downSampleScale = 1.0f;
            }
            CLog.Debug("kernelSize: %d1, downSampleScale: %f1", d1: kernelSize, f1: downSampleScale);
            Interop.ImageUtility.BoxBlur(imageBufferSafeHandle, kernelSize, downSampleScale);
        }

        /// <summary>
        /// Cleaning up managed and unmanaged resources 
        /// </summary>
        /// <param name="type">Disposing type</param>       
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }
            if (textureUrl != null)
            {
                Interop.ImageUtility.RemoveTexture(textureUrl);
            }
            disposed = true;
        }

        private void CheckBufferValidity(dynamic buffer, uint bufferSize, uint width, uint height, PixelFormat pixelFormat)
        {
            bool isNull = (buffer == null || buffer.Equals(IntPtr.Zero));

            if (isNull)
            {
                throw new ArgumentNullException("image buffer is null");
            }

            if (width <= 0 || height <= 0 || bufferSize <= 0)
            {
                throw new ArgumentException("Invalid width, height or buffer size ");
            }

            uint bytePerPixel = Interop.ImageUtility.GetBytesPerPixelFormat(pixelFormat);

            if (bufferSize != width * height * bytePerPixel)
            {
                throw new ArgumentException("image buffer size is wrong ");
            }
        }

        // TBD : The public api is provided by wrapping the internal api. 
        internal Task ApplyFilterEffectAsync(int filterType)
        {
            if (!Window.IsInstalled())
            {
                throw new InvalidOperationException("This API called from separate thread. This API must be called from MainThread.");
            }

            CLog.Debug("filterType: %d1", d1: filterType);

            SynchronizationContext mainThreadContext = SynchronizationContext.Current ?? throw new InvalidOperationException("Failed to get the SynchronizationContext for the current thread");
            ManualResetEvent waitHandle = new ManualResetEvent(initialState: false);

            Action onApplyFilterFinished = new Action(() => // filter thread
            {
                waitHandle.Set(); // notifies the work thread that the filter process is done.
            });

            FilterFinishedSignal signal = FilterFinishedSignal();
            signal.Connect(onApplyFilterFinished);

            Interop.ImageUtility.ApplyFilterEffect(imageBufferSafeHandle, filterType);

            // a worker thread that waits for a finished signal from PhotoFilterThread
            return Task.Factory.StartNew(() =>
            {
                bool isTimeOut = !waitHandle.WaitOne(millisecondsTimeout: 10000);

                // release all resources before leave, regardless of the result.
                signal.Disconnect(onApplyFilterFinished);
                waitHandle.Dispose();
                waitHandle = null;

                // since Dispose of signal must be called from main thread, dispatch to the main thread.
                mainThreadContext.Post(new SendOrPostCallback((p) =>
                {
                    signal.Dispose();
                    signal = null;
                }), this);

                if (isTimeOut)
                {
                    throw new TimeoutException($"ApplyFilterEffect Timeout"); // the exception is processed in the public api
                }
            });
        }

        private FilterFinishedSignal FilterFinishedSignal()
        {
            FilterFinishedSignal signal = new FilterFinishedSignal(Interop.ImageUtility.FilterFinishedSignal(imageBufferSafeHandle), false);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            return signal;
        }
    }
}