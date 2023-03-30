/// @file
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

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// This is VideoCanvasView which enables user to drill a transparent hole in window. 
    /// </summary>
    /// <code>
    /// videocanvasView = new VideoCanvasView();
    /// videocanvasView.Size2D = new Size2D(400, 300);
    /// videocanvasView.Position = new Position(100, 100, 0);    
    /// Window.Instance.GetDefaultLayer().Add(canvasView);    
    /// </code>
    public partial class VideoCanvasView : FluxView
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void DisplayAreaUpdated(IntPtr rect);
        private readonly DisplayAreaUpdated viewUpdateDisplayAreaDelegate;
        private readonly Rectangle displayArea = new Rectangle();
        private bool isPaused = false;
        internal VideoCanvasViewAdaptor adaptor = null;
        private Window currentWindow;
        private IVideoWindowControlExtension videoWindowControlExtension = null;
        private WindowAttribute windowAttribute = new WindowAttribute();
        private VideoAttribute videoAttribute = new VideoAttribute();
        private readonly (int width, int height) baseResolution = ResolutionUtil.GetCurrentBaseResolution();
        private bool asyncPlayerUpdate = false;

        /// <summary>
        /// Creates an initialized VideoView.
        /// </summary>
        public VideoCanvasView() : this(Interop.VideoCanvasView.VideoCanvasViewNew(), true)
        {
            SecurityUtil.CheckPlatformPrivileges();

            viewUpdateDisplayAreaDelegate = new DisplayAreaUpdated(OnUpdateDisplayArea);
            UpdateDisplayAreaSignal().Connect(viewUpdateDisplayAreaDelegate);
            AddedToWindow += OnAddedToWindow;
            RemovedFromWindow += OnRemovedFromWindow;
            VisibilityChanged += OnVisibilityChanged;

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        internal VideoCanvasView(IntPtr cPtr, bool cMemoryOwn) : base(Interop.VideoCanvasView.VideoCanvasViewUpcast(cPtr), cMemoryOwn)
        {

        }

        /// <summary>
        /// Gets or sets corner radius to VideoCanvasView.
        /// The order for the 4 corner radius to be provided is Vector4(topLeft, topRight, bottomRight, bottomLeft). 
        /// Current limitation - If any corner radius is given greater than or equal to width/2 or height/2 of VideoCanvas view,
        /// all 4 corner radius will be changed to MINIMUM(WIDTH/2, HEIGHT/2).
        /// </summary>
        /// <version> 9.9.0 </version>
        public new Vector4 CornerRadius
        {
            get => (Vector4)GetValue(CornerRadiusProperty);

            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Gets or sets disableAutoUpdateVideoGeometry to VideoCanvasView.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool DisableAutoUpdateVideoGeometry
        {
            set;
            get;
        } = false;

        /// <summary>
        /// Gets or sets disableAutoUpdateWindowInformation to VideoCanvasView.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool DisableAutoUpdateWindowInformation
        {
            set;
            get;
        } = false;

        /// <summary>
        /// Gets or sets player information(Geometry/Attribute) update asynchronously.
        /// It is supported only when the player supports asynchronous update.
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool AsynchronousPlayerUpdate
        {
            set
            {
                if (asyncPlayerUpdate == value)
                {
                    return;
                }
                asyncPlayerUpdate = value;
                adaptor?.SetAsyncUpdateMode(value);
            }
            get => asyncPlayerUpdate;
        }

        /// <summary>
        /// Update video screen geometry forcefully
        /// </summary>
        /// <param name="x">pixel based screen position of x </param>
        /// <param name="y">pixel based screen position of y</param>
        /// <param name="width">pixel based screen size of width</param>
        /// <param name="height">pixel based screen size of height</param>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ForceUpdateVideoGeometry(int x = -1, int y = -1, int width = -1, int height = -1)
        {
            CLog.Info("Force update geometry");
            if (x == -1 && y == -1 && width == -1 && height == -1)
            {
                UpdateGeometry(displayArea, true);
            }
            else
            {
                displayArea.Set(x, y, width, height);
                UpdateGeometry(displayArea, true);
            }
        }

        /// <summary>
        /// Update window information forcefully
        /// </summary>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ForceUpdateWindowInformation()
        {
            CLog.Info("Force update window information");
            UpdateWindowAttributeToPlayer(true);
        }

        /// <summary>
        /// Set VideoWindowControl instance to VideoCanvasView.
        /// </summary>
        /// <param name = "videoWindowControl">IVideoWindowControl Instance</param>
        public void SetPlayer(IVideoWindowControl videoWindowControl)
        {
            if (!Window.IsInstalled())
            {
                throw new InvalidOperationException("The API has been called from separate thread. This API must be called from MainThread.");
            }

            if (videoWindowControl is IVideoWindowControlExtension extension)
            {
                videoWindowControlExtension = extension;
            }
            IVideoCanvasAdaptorBehavior videoCanvasAdaptorBehavior = GetVideoCanvasAdaptorBehavior(videoWindowControl);
            if (adaptor == null)
            {
                adaptor = new VideoCanvasViewAdaptor(videoCanvasAdaptorBehavior);
            }
            else
            {
                adaptor.CleanUp();
                adaptor.AdaptorBehavior = videoCanvasAdaptorBehavior;
            }

            UpdateGeometry(displayArea);
        }

        /// <summary>
        /// Release Native Handle
        /// </summary>
        /// <param name="swigCPtr">HandleRef object holding corresponding native pointer.</param>
        /// <version>9.9.0</version>
        protected override void ReleaseSwigCPtr(HandleRef swigCPtr)
        {
            Interop.VideoCanvasView.VideoCanvasViewDelete(swigCPtr);
        }

        /// <summary>
        /// Dispose Function to clean up unmanaged resources.
        /// </summary>
        /// <param name = "type">Caller</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
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
            if (viewUpdateDisplayAreaDelegate != null)
            {
                UpdateDisplayAreaSignal().Disconnect(viewUpdateDisplayAreaDelegate);
            }

            UnsetCurrentWindow();
            AddedToWindow -= OnAddedToWindow;
            RemovedFromWindow -= OnRemovedFromWindow;
            VisibilityChanged -= OnVisibilityChanged;

            if (adaptor != null)
            {
                adaptor.CleanUp();
                adaptor = null;
            }
            videoAttribute = null;
            windowAttribute = null;

            base.Dispose(type);
        }

        /// <summary>
        /// Pause updating VideoCanvasView Rect
        /// </summary>
        internal void Pause()
        {
            isPaused = true;
        }

        /// <summary>
        /// Resume updating VideoCanvasView Rect
        /// </summary>
        internal void Resume()
        {
            isPaused = false;
            ForceUpdateRectOnce();
        }

        /// <summary>
        /// Determines whether the VideoCanvasView is paused
        /// </summary>
        /// <returns></returns>
        internal bool IsPaused()
        {
            return isPaused;
        }

        /// <summary>
        /// Forcibly updates geometry of VideoCanvasView
        /// </summary>
        internal void ForceUpdateRectOnce()
        {
            if (adaptor == null)
            {
                return;
            }

            //TODO: Need to check below code for TV devices.
            //if (adaptor.AdaptorBehavior is TVWindowControlAdaptorBehavior && displayArea.Width == 0 && displayArea.Height == 0)
            //{
            //    displayArea.Set(0, 0, 1, 1);
            //}

            UpdateGeometry(displayArea);
        }

        internal VideoCanvasViewSignal UpdateDisplayAreaSignal()
        {
            VideoCanvasViewSignal ret = new VideoCanvasViewSignal(Interop.VideoCanvasView.VideoCanvasViewUpdateDisplayAreaSignal(SwigCPtr), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            return ret;
        }

        internal Rectangle GetDisplayArea()
        {
            return displayArea;
        }

        private IVideoCanvasAdaptorBehavior GetVideoCanvasAdaptorBehavior(IVideoWindowControl videoWindowControl)
        {
            //TODO: Need to check below code for TV devices.
            //if (videoWindowControl is ITVWindowControl tvWindowControl)
            //{
            //    return new TVWindowControlAdaptorBehavior(tvWindowControl, this, asyncPlayerUpdate);
            //}
            //else
            //{
                return new VideoWindowControlAdaptorBehavior(videoWindowControl);
            //}
        }

        private void OnUpdateDisplayArea(IntPtr rect)
        {
            Rectangle temp = new Rectangle(rect, false);
            displayArea.Set(temp.X, temp.Y, temp.Width, temp.Height);
            UpdateGeometry(displayArea);
        }

        private void OnAddedToWindow(object sender, EventArgs e)
        {
            CLog.Info("VideoCanvasView added to window");
            if (currentWindow == Window.Get(this))
            {
                UnsetCurrentWindow();
                SetCurrentWindow();
            }
            else
            {
                UnsetCurrentWindow();
                SetCurrentWindow();
                UpdateWindowAttributeToPlayer();
            }
        }

        private void OnRemovedFromWindow(object sender, EventArgs e)
        {
            CLog.Info("VideoCanvasView removed from window");
            if (currentWindow != null && currentWindow.HasBody() == true)
            {
                currentWindow.VisibilityChanged -= OnWindowVisibilityChanged;
            }
        }

        private void OnWindowResized(object sender, Window.ResizedEventArgs e)
        {
            if (HasBody() == false)
            {
                return;
            }

            CLog.Info("VideoCanvasViewWindowResized: %d1 x %d2 , IsOnWindow: %d3", d1: e.WindowSize.Width, d2: e.WindowSize.Height, d3: Convert.ToInt32(IsOnWindow));
            if (IsOnWindow == true)
            {
                UpdateWindowAttributeToPlayer();
                UpdateGeometry(displayArea);
            }
            else
            {
                UpdateWindowAttribute();
            }
        }

        private void OnWindowVisibilityChanged(object sender, Window.VisibilityChangedEventArgs e)
        {
            if (HasBody() == false)
            {
                return;
            }

            CLog.Info("OnWindowVisibilityChanged Visibility: %d1", d1: Convert.ToInt32(e.Visibility));
            if (e.Visibility)
            {
                UpdateWindowAttributeToPlayer();

                if (IsVisible())
                {
                    CLog.Info("ForceUpdate Geometry due to WindowVisibilityChange");
                    ForceUpdateRectOnce();
                }
            }
        }

        private void OnVisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            CLog.Info("OnVisibilityChanged Visibility: %d1", d1: Convert.ToInt32(e.Visibility));
            if (e.Visibility == true)
            {
                if (IsVisible() == true)
                {
                    UpdateGeometry(displayArea);
                }
                else
                {
                    Window.Instance.AddFrameRenderedCallback(OnVisibilityChangedFrameRenderedCallback, 0);
                }
            }
        }

        private void OnVisibilityChangedFrameRenderedCallback(int frameId)
        {
            if (HasBody() == false)
            {
                return;
            }

            if (Visibility == false && IsVisible() == false)
            {
                CLog.Info("VideoCanvasView is in invisible state");
                return;
            }

            if (Visibility == true && IsVisible() == true)
            {
                CLog.Info("Update geometry on visibility true");
                UpdateGeometry(displayArea);
            }
            else
            {
                CLog.Info("Add new FrameRenderedCallback when IsVisible and Visibility value is different");
                Window.Instance.AddFrameRenderedCallback(OnVisibilityChangedFrameRenderedCallback, 0);
            }
        }

        private void SetCurrentWindow()
        {
            currentWindow = Window.Get(this);
            currentWindow.Resized += OnWindowResized;
            currentWindow.VisibilityChanged += OnWindowVisibilityChanged;
        }

        private void UnsetCurrentWindow()
        {
            if (currentWindow != null && currentWindow.HasBody() == true)
            {
                currentWindow.Resized -= OnWindowResized;
                currentWindow.VisibilityChanged -= OnWindowVisibilityChanged;
                currentWindow = null;
            }
        }

        private void UpdateGeometry(Rectangle displayArea, bool forceUpdate = false)
        {
            if (IsUpdateAvailable(forceUpdate) == false)
            {
                return;
            }

            bool visible = IsVisible();
            if (IsPaused() || visible == false)
            {
                CLog.Error("Couldn't update VideoCanvasView geometry. Paused: %d1, Visible: %d2"
                    , d1: Convert.ToInt32(IsPaused())
                    , d2: Convert.ToInt32(visible)
                    );
                return;
            }

            if (videoWindowControlExtension != null)
            {
                UpdateVideoAttribute(displayArea);
                adaptor?.UpdateGeometry(videoAttribute, windowAttribute);
            }
            else
            {
                adaptor?.UpdateGeometry(displayArea);
            }
        }

        private void UpdateWindowAttributeToPlayer(bool forceUpdate = false)
        {
            UpdateWindowAttribute();

            if (IsUpdateAvailable(forceUpdate) == false)
            {
                return;
            }

            adaptor?.UpdateAttribute();
        }

        private void UpdateWindowAttribute()
        {
            if (currentWindow == null)
            {
                CLog.Error("VideoCanvasView current window is null");
                return;
            }

            Position2D position = currentWindow.GetPosition();
            Size2D size = currentWindow.Size;
            int degree = DisplayMetrics.Instance.GetTVAngle(currentWindow.GetCurrentOrientation());
            if (position != null && size != null)
            {
                windowAttribute.SetWindowAttribute((position.X, position.Y), (size.Width, size.Height), degree, baseResolution);
                CLog.Info("UpdateWindowAttribute: [ %d1, %d2, %d3, %d4 ], degree: %d5", d1: position.X, d2: position.Y, d3: size.Width, d4: size.Height, d5: degree);
            }
        }

        private void UpdateVideoAttribute(Rectangle displayArea)
        {
            videoAttribute.SetVideoAttribute(displayArea.X, displayArea.Y, displayArea.Width, displayArea.Height);
        }

        private bool IsUpdateAvailable(bool forceUpdate)
        {
            if (DisableAutoUpdateWindowInformation == true)
            {
                if (forceUpdate == false)
                {
                    CLog.Info("Manual mode. Not update window attribute.");
                    return false;
                }
            }

            if (currentWindow == null || currentWindow.HasBody() == false)
            {
                CLog.Error("Current window is null");
                return false;
            }

            if (currentWindow.IsVisible() == false)
            {
                CLog.Error("Current window invisible");
                return false;
            }

            return true;
        }

        private Vector4 privateCornerRadius
        {
            get => new Vector4(Interop.VideoCanvasView.GetCornerRadius(SwigCPtr), true);

            set
            {
                Vector4 cr = new Vector4();
                if (value != null)
                {
                    float topLeft = (value.X > 0.0f) ? value.X : 0.0f;
                    float topRight = (value.Y > 0.0f) ? value.Y : 0.0f;
                    float bottomRight = (value.Z > 0.0f) ? value.Z : 0.0f;
                    float bottomLeft = (value.W > 0.0f) ? value.W : 0.0f;
                    cr = new Vector4(topLeft, topRight, bottomRight, bottomLeft);
                }

                Interop.VideoCanvasView.SetCornerRadius(SwigCPtr, Vector4.getCPtr(cr));
            }
        }
    }
}