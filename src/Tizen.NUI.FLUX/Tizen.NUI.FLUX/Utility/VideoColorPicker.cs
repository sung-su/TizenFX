/// @file VideoColorPicker.cs
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
using System.ComponentModel;
using System.Runtime.InteropServices;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// VideoColorPicker class which enables user to obtain the most dominant colour in the user defiend area of video.
    /// </summary>
    /// <code>
    /// videoColorPicker = new VideoColorPicker();                   
    /// videoColorPicker.PickingArea = new Rect(100, 100, 200, 200);
    /// videoColorPicker.ColorPicked += VideoColorPicker_ColorPicked;
    /// videoColorPicker.Start();                                    
    /// </code>
    public class VideoColorPicker : NUIDisposable
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void NativeColorpickCallback(IntPtr data, int red, int green, int blue, int status);

        /// <summary>
        /// Delegate for ColorPicked event
        /// </summary>
        /// <param name="red">the red component at the picked color</param>
        /// <param name="green">the green component at the picked color</param>
        /// <param name="blue">the blue component at the picked color</param>
        public delegate void ColorReceivedEventHandler(float red, float green, float blue);

        /// <summary>
        /// Struct for setting the area of a rect
        /// </summary>
        [Description("@So : " + Interop.Libraries.UIFW_Misc + "@Function : get_size_of_Ua_Rect")]
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            /// <summary>
            /// x coordinate of the area.
            /// </summary>
            public int x;

            /// <summary>
            /// y coordinate of the area.
            /// </summary>
            public int y;

            /// <summary>
            /// Width of the area
            /// </summary>
            public int w;

            /// <summary>
            /// Height of the area
            /// </summary>
            public int h;

            /// <summary>
            /// Setting Area of a rect.
            /// </summary>
            /// <param name="x">x coordinate of the area.</param>
            /// <param name="y">y coordinate of the area.</param>
            /// <param name="width">Width of the area</param>
            /// <param name="height">Height of the area</param>
            public Rect(int x, int y, int width, int height)
            {
                this.x = x;
                this.y = y;
                w = width;
                h = height;
            }
        }

        [Description("@So : " + Interop.Libraries.UIFW_Misc + "@Function : get_size_of_Ua_Colopick_info")]
        [StructLayout(LayoutKind.Sequential)]
        internal class ColorpickInfo
        {
            public Rect rect;
            public IntPtr cb_data;
            public NativeColorpickCallback nativeCallback;
        }

        /// <summary>
        /// Constructor to instantiate the VideoColorPicker class.
        /// </summary>
        public VideoColorPicker()
        {
            SecurityUtil.CheckPlatformPrivileges();
            //ColorPick Info Initialize
            info = new ColorpickInfo
            {
                cb_data = IntPtr.Zero
            };
            info.rect.x = 0;
            info.rect.y = 0;
            info.rect.w = 1920;
            info.rect.h = 1080;
            info.nativeCallback = new NativeColorpickCallback(NativeColorPickCallbackFunction);
        }

        /// <summary>
        /// Set/Get the Area of a rectangle
        /// </summary>
        public Rect PickingArea
        {
            get => info.rect;
            set => info.rect = value;
        }

        /// <summary>
        /// Tells whether color pick service is running.
        /// </summary>
        /// <returns>Whether color pick service is started or not</returns>
        public bool IsRunning { private set; get; } = false;

        /// <summary>
        /// ColorPicked Event occurs periodically with the dominant colour value. 
        /// </summary>        
        //public event EventHandler<VideoColorPickEventArgs> ColorPicked;
        public event ColorReceivedEventHandler ColorPicked;

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

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.
            }

            if (IsRunning)
            {
                Stop();
            }

            disposed = true;
        }

        /// <summary>
        /// Start picking the most dominant colour from the video source.
        /// </summary>        
        /// <exception cref='OutOfMemoryException'>
        /// Throw failed if there is insufficient memory to satisfy the request.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throw failed when the input structure is a reference type that is not a formatted class
        /// </exception>
        public void Start()
        {
            //Return if the user hasn't registered their handler
            if (IsRunning || ColorPicked == null)
            {
                return;
            }

            try
            {
                //Allocate Unmanaged Memory 
                //C# will not free memory automatically. 
                //The user must free it with Marsha.FreeGlobal                 
                pnt = Marshal.AllocHGlobal(Marshal.SizeOf(info));

                Marshal.StructureToPtr(info, pnt, false);
            }
            catch (OutOfMemoryException e)
            {
                throw new OutOfMemoryException("Failed to Allocate Memory:" + e.Message);
            }
            catch (ArgumentException e)
            {
                throw new Exception("Failed to Marshal:" + e.Message);
            }

            id = Interop.VideoColorPick.native_videocolorpick_subscribe(pnt);

            IsRunning = true;
        }

        /// <summary>
        /// Stop picking the most dominant colour from the video source.
        /// </summary>        
        public void Stop()
        {
            if (id < 0 || IsRunning == false)
            {
                return;
            }

            Interop.VideoColorPick.native_videocolorpick_unsubscribe(id);

            id = -1;

            if (pnt != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(pnt);
                pnt = IntPtr.Zero;
            }

            IsRunning = false;

            return;
        }

        internal void NativeColorPickCallbackFunction(IntPtr data, int red, int green, int blue, int status)
        {
            CLog.Info("red: %d1, green: %d2, blue: %d3", d1: red, d2: green, d3: blue);

            //Nomalize the gray scale value.
            float r = red / 255.0f;
            float g = green / 255.0f;
            float b = blue / 255.0f;

            ColorPicked?.Invoke(r, g, b);
        }

        private readonly ColorpickInfo info;
        private IntPtr pnt;
        private int id = -1;
    }
}
