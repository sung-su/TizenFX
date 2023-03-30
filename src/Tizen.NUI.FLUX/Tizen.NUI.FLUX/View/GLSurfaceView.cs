/// @file GLSurfaceView.cs
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
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// GLSurfaceView allows user OpenGL rendering in separate Thread.
    /// </summary>
    /// <code>
    /// Implement Renderer Interface 
    /// class Renderer : IGLSurfaceViewRenderer
    /// {
    ///     public void Initialize(uint width, uint height){}
    ///     public void Draw(){ }
    ///     public void Terminate(){}
    /// }
    /// 
    /// Create GLSurfaceView
    /// glSurfaceView = new GLSurfaceView(800, 800);
    /// Renderer rend = new Renderer();
    /// glSurfaceView.SetRenderer(rend);
    /// glSurfaceView.RenderingMode = RenderMode.Continuous;
    /// glSurfaceView.GLContentResizingMode = GLContentResizeMode.Static;
    /// parent.Add(glSurfaceView);
    /// 
    /// </code>
    public partial class GLSurfaceView : FluxView
    {
        private RenderMode renderMode;
        private GLContentResizeMode glContentResizeMode = GLContentResizeMode.Static;
        private readonly Interop.GLSurfaceView.GLInitCallback initCallback;
        private readonly Interop.GLSurfaceView.GLDrawCallback drawCallback;
        private readonly Interop.GLSurfaceView.GLTerminateCallback terminateCallback;
        private AutoResetEvent updatedEvent = null;
        private bool rendererUpdated = false;
        private int rendererUpdateCount = 0;
        private bool rendererInitialized = false;
        private const int bufferSize = 4;
        private const int updateTimeoutMilliseconds = 10000;
        private bool synchonizeStarted = false;

        private readonly object syncObject = new object();

        private Action<uint, uint> GLInit;
        private Action GLDraw;
        private Action GLTerminate;

        /// <summary>
        /// Supported Rendering Mode.
        /// </summary>
        public enum RenderMode : uint
        {
            /// <summary>
            /// Continuous Mode. if set, GLDraw callback is called repeatedly.
            /// </summary>
            Continuous = 0,
            /// <summary>
            /// OnDemand Mode, if set, GLDraw callback is called when requestRender() is called
            /// </summary>
            OnDemand = 1
        }

        /// <summary>
        /// Enumeration for GL Content resize mode
        /// </summary>
        /// <version>9.9.0</version>
        public enum GLContentResizeMode : uint
        {
            /// <summary>
            /// GLContent size is fixed to the first created size and filled with the actor size.
            /// </summary>
            Static = 0,

            /// <summary>
            /// GLContent size is dynmaically changed according to the actor size. 
            /// </summary>
            Dynamic = 1
        }

        /// <summary>
        /// Creates GLSurfaceView.
        /// <param name = "surfaceWidth">surfaceWidth</param>
        /// <param name = "surfaceHeight">surfaceHeight</param>
        /// </summary>
        public GLSurfaceView(uint surfaceWidth, uint surfaceHeight) : this(Interop.GLSurfaceView.New(surfaceWidth, surfaceHeight), true)
        {
            SecurityUtil.CheckPlatformPrivileges();
            renderMode = RenderMode.OnDemand;
            glContentResizeMode = GLContentResizeMode.Static;
            initCallback = new Interop.GLSurfaceView.GLInitCallback(OnInitCallback);
            drawCallback = new Interop.GLSurfaceView.GLDrawCallback(OnDrawCallback);
            terminateCallback = new Interop.GLSurfaceView.GLTerminateCallback(OnTerminateCallback);

            updatedEvent = new AutoResetEvent(initialState: false);
        }

        private RenderMode privateRenderingMode
        {
            set
            {
                renderMode = value;
                Interop.GLSurfaceView.SetRenderingMode(SwigCPtr, value);
            }

            get => renderMode;
        }

        /// <summary>
        /// RenderingMode.
        /// </summary>
        public RenderMode RenderingMode
        {
            set => SetValue(RenderingModeProperty, value);

            get => (RenderMode)GetValue(RenderingModeProperty);
        }

        private GLContentResizeMode privateGLContentResizingMode
        {
            set
            {
                glContentResizeMode = value;
                Interop.GLSurfaceView.SetGLContentResizingMode(SwigCPtr, value);

                PreSizeChanged -= OnPreSizeChanged;
                if (value == GLContentResizeMode.Dynamic)
                {
                    PreSizeChanged += OnPreSizeChanged;
                }
            }

            get => glContentResizeMode;
        }

        /// <summary>
        /// GLContentResizingMode property
        /// </summary>
        /// <version>9.9.0</version>
        public GLContentResizeMode GLContentResizingMode
        {
            set => SetValue(GLContentResizingModeProperty, value);
            get => (GLContentResizeMode)GetValue(GLContentResizingModeProperty);
        }

        /// <summary>
        /// Set the renderer to the view. It provide interfaces for OpenGL rendering.
        /// Note. Renderer need to be set, before RequestRender(), RenderingMode()
        /// </summary>
        /// <param name = "renderer"></param>
        public void SetRenderer(IGLSurfaceViewRenderer renderer)
        {
            GLInit = renderer.Initialize;
            GLDraw = renderer.Draw;
            GLTerminate = renderer.Terminate;

            if (renderer is GLSurfaceViewBaseRenderer rendererBase)
            {
                rendererBase.RegisterUpdatedCallback(OnRendererUpdated);
            }
            Interop.GLSurfaceView.RegisterGLCallback(SwigCPtr, initCallback, drawCallback, terminateCallback);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        /// <summary>
        ///  Request render frame on demand.
        ///  This method is typically used when the render mode has been set to OnDemand.
        /// </summary>
        public void RequestRender()
        {
            Interop.GLSurfaceView.RequestRender(SwigCPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        /// <summary>
        /// Start synchronized update.
        /// <remarks> It should be pair with GLsurfaceViewBaseRenderer.EndSynchronizedUpdate </remarks>
        /// </summary>
        /// <version> 9.9.0 </version>
        /// <param name="timeoutMilliseconds"> timeout value for synchronized update </param>
        public void StartSynchronizedUpdate(int timeoutMilliseconds = updateTimeoutMilliseconds)
        {
            try
            {
                if (rendererInitialized == false)
                {
                    CLog.Error("GLSurfaceRenderer OnInitialize callback not called");
                    return;
                }

                synchonizeStarted = true;

                if (updatedEvent.WaitOne(timeoutMilliseconds) == false)
                {
                    CLog.Error("Timeout. Failed to get renderer updated signal.");
                }
                synchonizeStarted = false;
            }
            catch (Exception e)
            {
                CLog.Error("Failed to WaitOne AutoResetEvent: %s1", s1: e.Message);
            }
        }

        internal GLSurfaceView(IntPtr cPtr, bool cMemoryOwn) : base(Interop.GLSurfaceView.Upcast(cPtr), cMemoryOwn)
        {

        }

        private bool OnInitCallback(uint w, uint h)
        {
            Action<uint, uint> local = GLInit;
            if (local != null)
            {
                local(w, h);
                rendererInitialized = true;
                return true;
            }

            return false;
        }

        private void OnDrawCallback()
        {
            Action local = GLDraw;
            if (local != null)
            {
                local.Invoke();
            }

            if (rendererUpdated == true)
            {
                lock (syncObject)
                {
                    if (synchonizeStarted == true)
                    {
                        if (rendererUpdateCount > bufferSize)
                        {
                            rendererUpdateCount = 0;
                            rendererUpdated = false;

                            try
                            {
                                updatedEvent.Set();
                            }
                            catch (ObjectDisposedException e)
                            {
                                CLog.Error("Failed to set AutoResetEvent: %s1", s1: e.Message);
                            }
                            return;
                        }
                        rendererUpdateCount++;
                    }
                    else
                    {
                        rendererUpdated = false;
                    }
                }
            }
        }

        private void OnTerminateCallback()
        {
            Action local = GLTerminate;
            if (local != null)
            {
                local.Invoke();
            }
        }

        private void OnRendererUpdated()
        {
            rendererUpdated = true;
        }

        private void ResetSurfaceSize(int width, int height)
        {
            if (GLContentResizingMode == GLContentResizeMode.Dynamic)
            {
                Interop.GLSurfaceView.ResetSurfaceSize(SwigCPtr, width, height);
            }
        }

        private void OnPreSizeChanged(object sender, PreSizeChangedEventArgs e)
        {
            ResetSurfaceSize(e.Width, e.Height);
        }

        /// <summary>
        /// Release Native Handle
        /// </summary>
        /// <param name="swigCPtr">HandleRef object holding corresponding native pointer.</param>
        /// <version>9.9.0</version>
        protected override void ReleaseSwigCPtr(HandleRef swigCPtr)
        {
            Interop.GLSurfaceView.Delete(swigCPtr);
        }

        /// <summary>
        /// Cleaning up managed and unmanaged resources 
        /// </summary>
        /// <param name = "type">Disposing type</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                PreSizeChanged -= OnPreSizeChanged;

                lock (syncObject)
                {
                    try
                    {
                        updatedEvent?.Dispose();
                        updatedEvent = null;
                    }
                    catch (Exception e)
                    {
                        CLog.Error("Failed to Dispose AutoResetEvent: %s1", s1: e.Message);
                    }
                }
            }

            base.Dispose(type);
        }
    }
}
