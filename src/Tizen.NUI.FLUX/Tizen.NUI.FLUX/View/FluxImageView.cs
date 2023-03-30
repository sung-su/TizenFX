/// @file FluxImageView.cs
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
using System.ComponentModel;
using System.Runtime.InteropServices;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// FluxView is the base class for all views in FLUX Application
    /// </summary>
    /// <code>
    /// FluxImageView = new FluxImageView();
    /// FluxImageView.UnitPosition = new UnitPosition(50, 50);
    /// FluxImageView.UnitSizeWidth = 50;
    /// FluxImageView.UnitSizeHeight = 50;
    /// FluxImageView.BackgroundColor = Color.Red;
    /// </code>
    /// <deprecated>
    /// Deprecated since 10.10.0. Use ImageBox instead.
    /// </deprecated>
    [Obsolete("FluxImageView is deprecated, please use ImageBox instead.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial class FluxImageView : FluxView
    {
        private EventHandler<ResourceReadyEventArgs> _resourceReadyEventHandler;
        private ResourceReadyEventCallbackType _resourceReadyEventCallback;
        private EventHandler<ResourceLoadedEventArgs> _resourceLoadedEventHandler;
        private _resourceLoadedCallbackType _resourceLoadedCallback;

        private Rectangle _border;
        private string _resourceUrl = "";
        private bool _synchronosLoading = false;

        /// <summary>
        /// It is called automatically before the first instance is created or any static members are referenced.
        /// </summary>
        static FluxImageView()
        {
            SecurityUtil.CheckPlatformPrivileges();
        }

        /// <summary>
        /// Creates an initialized FluxImageView.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public FluxImageView() : this(Tizen.NUI.Interop.ImageView.ImageView_New__SWIG_0(), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        /// <summary>
        /// Creates an initialized FluxImageView with setting the status of shown or hidden.
        /// </summary>
        /// <param name="shown">false : Not displayed (hidden), true : displayed (shown)</param>
        /// This will be public opened in next release of tizen after ACR done. Before ACR, it is used as HiddenAPI (InhouseAPI).
        [EditorBrowsable(EditorBrowsableState.Never)]
        public FluxImageView(bool shown) : this(Tizen.NUI.Interop.ImageView.ImageView_New__SWIG_0(), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            SetVisible(shown);
        }

        /// <summary>
        /// Creates an initialized FluxImageView from a URL to an image resource.<br />
        /// If the string is empty, FluxImageView will not display anything.<br />
        /// </summary>
        /// <param name="url">The URL of the image resource to display.</param>
        /// <since_tizen> 3 </since_tizen>
        public FluxImageView(string url) : this(Tizen.NUI.Interop.ImageView.ImageView_New__SWIG_2(url), true)
        {
            ResourceUrl = url;
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        /// <summary>
        /// Creates an initialized FluxImageView from a URL to an image resource with setting shown or hidden.
        /// </summary>
        /// <param name="url">The URL of the image resource to display.</param>
        /// <param name="shown">false : Not displayed (hidden), true : displayed (shown)</param>
        /// This will be public opened in next release of tizen after ACR done. Before ACR, it is used as HiddenAPI (InhouseAPI).
        [EditorBrowsable(EditorBrowsableState.Never)]
        public FluxImageView(string url, bool shown) : this(Tizen.NUI.Interop.ImageView.ImageView_New__SWIG_2(url), true)
        {
            ResourceUrl = url;
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            SetVisible(shown);
        }

        internal FluxImageView(string url, Uint16Pair size, bool shown = true) : this(Tizen.NUI.Interop.ImageView.ImageView_New__SWIG_3(url, Uint16Pair.getCPtr(size)), true)
        {
            ResourceUrl = url;
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            if (!shown)
            {
                SetVisible(false);
            }
        }

        internal FluxImageView(global::System.IntPtr cPtr, bool cMemoryOwn, bool shown = true) : base(Tizen.NUI.Interop.ImageView.ImageView_SWIGUpcast(cPtr), cMemoryOwn)
        {
            if (!shown)
            {
                SetVisible(false);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void ResourceReadyEventCallbackType(IntPtr data);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void _resourceLoadedCallbackType(IntPtr view);

        /// <summary>
        /// An event for ResourceReady signal which can be used to subscribe or unsubscribe the event handler.<br />
        /// This signal is emitted after all resources required by a control are loaded and ready.<br />
        /// Most resources are only loaded when the control is placed on the stage.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public event EventHandler<ResourceReadyEventArgs> ResourceReady
        {
            add
            {
                if (_resourceReadyEventHandler == null)
                {
                    _resourceReadyEventCallback = OnResourceReady;
                    ResourceReadySignal(this).Connect(_resourceReadyEventCallback);
                }

                _resourceReadyEventHandler += value;
            }

            remove
            {
                _resourceReadyEventHandler -= value;

                if (_resourceReadyEventHandler == null && ResourceReadySignal(this).Empty() == false)
                {
                    ResourceReadySignal(this).Disconnect(_resourceReadyEventCallback);
                }
            }
        }

        internal event EventHandler<ResourceLoadedEventArgs> ResourceLoaded
        {
            add
            {
                if (_resourceLoadedEventHandler == null)
                {
                    _resourceLoadedCallback = OnResourceLoaded;
                    ResourceReadySignal(this).Connect(_resourceLoadedCallback);
                }

                _resourceLoadedEventHandler += value;
            }
            remove
            {
                _resourceLoadedEventHandler -= value;

                if (_resourceLoadedEventHandler == null && ResourceReadySignal(this).Empty() == false)
                {
                    ResourceReadySignal(this).Disconnect(_resourceLoadedCallback);
                }
            }
        }

        /// <summary>
        /// Enumeration for LoadingStatus of image.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public enum LoadingStatusType
        {
            /// <summary>
            /// Loading preparing status.
            /// </summary>
            /// <since_tizen> 5 </since_tizen>
            Preparing,
            /// <summary>
            /// Loading ready status.
            /// </summary>
            /// <since_tizen> 5 </since_tizen>
            Ready,
            /// <summary>
            /// Loading failed status.
            /// </summary>
            /// <since_tizen> 5 </since_tizen>
            Failed
        }

        /// <summary>
        /// FluxImageView ResourceUrl, type string.
        /// This is one of mandatory property. Even if not set or null set, it sets empty string ("") internally.
        /// When it is set as null, it gives empty string ("") to be read.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public virtual string ResourceUrl
        {
            get => (string)GetValue(ResourceUrlProperty);
            set
            {
                SetValue(ResourceUrlProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// FluxImageView Image, type PropertyMap
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public PropertyMap Image
        {
            get
            {
                if (_border == null)
                {
                    return (PropertyMap)GetValue(ImageProperty);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (_border == null)
                {
                    SetValue(ImageProperty, value);
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// FluxImageView PreMultipliedAlpha, type Boolean.<br />
        /// Image must be initialized.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public bool PreMultipliedAlpha
        {
            get => (bool)GetValue(PreMultipliedAlphaProperty);
            set
            {
                SetValue(PreMultipliedAlphaProperty, value);
                NotifyPropertyChanged();
            }
        }


        /// <summary>
        /// The border of the image in the order: left, right, bottom, top.<br />
        /// If set, ImageMap will be ignored.<br />
        /// For N-Patch images only.<br />
        /// Optional.
        /// </summary>
        /// <remarks>
        /// The property cascade chaining set is possible. For example, this (imageView.Border.X = 1;) is possible.
        /// </remarks>
        /// <since_tizen> 3 </since_tizen>
        public Rectangle Border
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether to draw the borders only (if true).<br />
        /// If not specified, the default is false.<br />
        /// For N-Patch images only.<br />
        /// Optional.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public bool BorderOnly
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether to synchronous loading the resourceurl of image.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public bool SynchronousLoading
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether to automatically correct the orientation of an image.<br />
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public bool OrientationCorrection
        {
            get => (bool)GetValue(OrientationCorrectionProperty);
            set
            {
                SetValue(OrientationCorrectionProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the loading state of the visual resource.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public FluxImageView.LoadingStatusType LoadingStatus => (FluxImageView.LoadingStatusType)Tizen.NUI.Interop.View.View_GetVisualResourceStatus(SwigCPtr, Property.IMAGE);

        /// <summary>
        /// Downcasts a handle to imageView handle.
        /// </summary>
        /// Please do not use! this will be deprecated!
        /// Instead please use as keyword.
        /// <param name="handle">BaseHandle</param>
        /// <returns>FluxImageView</returns>
        /// <since_tizen> 3 </since_tizen>
        [Obsolete("Please do not use! This will be deprecated! Please use as keyword instead! " +
        "Like: " +
        "BaseHandle handle = new ImageView(imagePath); " +
        "ImageView image = handle as ImageView")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static FluxImageView DownCast(BaseHandle handle)
        {
            FluxImageView ret = Registry.GetManagedBaseHandleFromNativePtr(handle) as FluxImageView;
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            return ret;
        }

        /// <summary>
        /// Sets this FluxImageView from the given URL.<br />
        /// If the URL is empty, FluxImageView will not display anything.<br />
        /// </summary>
        /// <param name="url">The URL to the image resource to display.</param>
        /// <since_tizen> 3 </since_tizen>
        public void SetImage(string url)
        {
            if (url.Contains(".json"))
            {
                CLog.Fatal("[ERROR] Please DO NOT set lottie file in FluxImageView! This is temporary checking, will be removed soon!");
                return;
            }

            Tizen.NUI.Interop.ImageView.ImageView_SetImage__SWIG_1(SwigCPtr, url);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            ResourceUrl = url;
        }

        /// <summary>
        /// Queries if all resources required by a control are loaded and ready.<br />
        /// Most resources are only loaded when the control is placed on the stage.<br />
        /// True if the resources are loaded and ready, false otherwise.<br />
        /// </summary>
        /// <returns>true if all resource needed by a control is ready</returns>
        /// <since_tizen> 3 </since_tizen>
        public new bool IsResourceReady()
        {
            bool ret = Tizen.NUI.Interop.View.IsResourceReady(SwigCPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            return ret;
        }

        /// <summary>
        /// Forcefully reloads the image. All the visuals using this image will reload to the latest image.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public void Reload()
        {
            DoAction(FluxImageView.Property.IMAGE, Property.ACTION_RELOAD, new PropertyValue(0));
        }

        /// <summary>
        /// Plays the animated GIF. This is also the default playback mode.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public void Play()
        {
            DoAction(FluxImageView.Property.IMAGE, Property.ACTION_PLAY, new PropertyValue(0));
        }

        /// <summary>
        /// Pauses the animated GIF.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public void Pause()
        {
            DoAction(FluxImageView.Property.IMAGE, Property.ACTION_PAUSE, new PropertyValue(0));
        }

        /// <summary>
        /// Stops the animated GIF.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public void Stop()
        {
            DoAction(FluxImageView.Property.IMAGE, Property.ACTION_STOP, new PropertyValue(0));
        }

        /// <summary>
        /// Gets or sets the URL of the alpha mask.<br />
        /// Optional.
        /// </summary>
        /// <since_tizen> 6</since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string AlphaMaskURL
        {
            get
            {
                string ret = "";
                PropertyMap imageMap = new PropertyMap();
                Tizen.NUI.Object.GetProperty(SwigCPtr, FluxImageView.Property.IMAGE).Get(imageMap);
                imageMap?.Find(ImageVisualProperty.AlphaMaskURL)?.Get(out ret);

                return ret;
            }
            set => UpdateImage(ImageVisualProperty.AlphaMaskURL, new PropertyValue(value ?? ""));
        }


        /// <summary>
        ///  Whether to crop image to mask or scale mask to fit image.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public bool CropToMask
        {
            get
            {
                bool ret = false;
                PropertyMap imageMap = new PropertyMap();
                Tizen.NUI.Object.GetProperty(SwigCPtr, FluxImageView.Property.IMAGE).Get(imageMap);
                imageMap?.Find(ImageVisualProperty.CropToMask)?.Get(out ret);

                return ret;
            }
            set => UpdateImage(ImageVisualProperty.CropToMask, new PropertyValue(value));
        }


        /// <summary>
        /// Gets or sets fitting options used when resizing images to fit the desired dimensions.<br />
        /// If not supplied, the default is FittingModeType.ShrinkToFit.<br />
        /// For normal quad images only.<br />
        /// Optional.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public FittingModeType FittingMode
        {
            get
            {
                int ret = (int)FittingModeType.ShrinkToFit;
                PropertyMap imageMap = new PropertyMap();
                Tizen.NUI.Object.GetProperty(SwigCPtr, FluxImageView.Property.IMAGE).Get(imageMap);
                imageMap?.Find(ImageVisualProperty.FittingMode)?.Get(out ret);

                return (FittingModeType)ret;
            }
            set => UpdateImage(ImageVisualProperty.CropToMask, new PropertyValue((int)value));
        }



        /// <summary>
        /// Gets or sets the desired image width.<br />
        /// If not specified, the actual image width is used.<br />
        /// For normal quad images only.<br />
        /// Optional.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int DesiredWidth
        {
            get
            {
                int ret = -1;
                PropertyMap imageMap = new PropertyMap();
                Tizen.NUI.Object.GetProperty(SwigCPtr, FluxImageView.Property.IMAGE).Get(imageMap);
                imageMap?.Find(ImageVisualProperty.DesiredWidth)?.Get(out ret);

                return ret;
            }
            set => UpdateImage(ImageVisualProperty.DesiredWidth, new PropertyValue(value));
        }

        /// <summary>
        /// Gets or sets the desired image height.<br />
        /// If not specified, the actual image height is used.<br />
        /// For normal quad images only.<br />
        /// Optional.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int DesiredHeight
        {
            get
            {
                int ret = -1;
                PropertyMap imageMap = new PropertyMap();
                Tizen.NUI.Object.GetProperty(SwigCPtr, FluxImageView.Property.IMAGE).Get(imageMap);
                imageMap?.Find(ImageVisualProperty.DesiredHeight)?.Get(out ret);

                return ret;
            }
            set => UpdateImage(ImageVisualProperty.DesiredHeight, new PropertyValue(value));
        }


        /// <summary>
        /// Gets or sets the wrap mode for the u coordinate.<br />
        /// It decides how the texture should be sampled when the u coordinate exceeds the range of 0.0 to 1.0.<br />
        /// If not specified, the default is WrapModeType.Default(CLAMP).<br />
        /// For normal quad images only.<br />
        /// Optional.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public WrapModeType WrapModeU
        {
            get
            {
                int ret = (int)WrapModeType.Default;
                PropertyMap imageMap = new PropertyMap();
                Tizen.NUI.Object.GetProperty(SwigCPtr, FluxImageView.Property.IMAGE).Get(imageMap);
                imageMap?.Find(ImageVisualProperty.WrapModeU)?.Get(out ret);

                return (WrapModeType)ret;
            }
            set => UpdateImage(ImageVisualProperty.WrapModeU, new PropertyValue((int)value));
        }

        /// <summary>
        /// Gets or sets the wrap mode for the v coordinate.<br />
        /// It decides how the texture should be sampled when the v coordinate exceeds the range of 0.0 to 1.0.<br />
        /// The first two elements indicate the top-left position of the area, and the last two elements are the areas of the width and the height respectively.<br />
        /// If not specified, the default is WrapModeType.Default(CLAMP).<br />
        /// For normal quad images only.
        /// Optional.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public WrapModeType WrapModeV
        {
            get
            {
                int ret = (int)WrapModeType.Default;
                PropertyMap imageMap = new PropertyMap();
                Tizen.NUI.Object.GetProperty(SwigCPtr, FluxImageView.Property.IMAGE).Get(imageMap);
                imageMap?.Find(ImageVisualProperty.WrapModeV)?.Get(out ret);

                return (WrapModeType)ret;
            }
            set => UpdateImage(ImageVisualProperty.WrapModeV, new PropertyValue((int)value));
        }

        internal void SetImage(string url, Uint16Pair size)
        {
            if (url.Contains(".json"))
            {
                CLog.Fatal("[ERROR] Please DO NOT set lottie file in FluxImageView! This is temporary checking, will be removed soon!");
                return;
            }

            Tizen.NUI.Interop.ImageView.ImageView_SetImage__SWIG_2(SwigCPtr, url, Uint16Pair.getCPtr(size));
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            ResourceUrl = url;
        }

        internal ViewResourceReadySignal ResourceReadySignal(View view)
        {
            ViewResourceReadySignal ret = new ViewResourceReadySignal(Tizen.NUI.Interop.View.ResourceReadySignal(View.getCPtr(view)), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            return ret;
        }

        internal ResourceLoadingStatusType GetResourceStatus()
        {
            return (ResourceLoadingStatusType)Tizen.NUI.Interop.View.View_GetVisualResourceStatus(SwigCPtr, Property.IMAGE);
        }

        /// <summary>
        /// Release Native Handle
        /// </summary>
        /// <param name="swigCPtr">HandleRef object holding corresponding native pointer.</param>
        /// <version>9.9.0</version>
        protected override void ReleaseSwigCPtr(HandleRef swigCPtr)
        {
            Tizen.NUI.Interop.ImageView.delete_ImageView(swigCPtr);
        }

        /// <summary>
        /// you can override it to clean-up your own resources.
        /// </summary>
        /// <param name="type">DisposeTypes</param>
        /// <since_tizen> 3 </since_tizen>
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
                if (_border != null)
                {
                    _border.Dispose();
                    _border = null;
                }
            }

            base.Dispose(type);
        }

        // Callback for View ResourceReady signal
        private void OnResourceReady(IntPtr data)
        {
            ResourceReadyEventArgs e = new ResourceReadyEventArgs();
            if (data != null)
            {
                e.View = Registry.GetManagedBaseHandleFromNativePtr(data) as View;
            }

            if (_resourceReadyEventHandler != null)
            {
                _resourceReadyEventHandler(this, e);
            }
        }

        private void UpdateImageMap(PropertyMap fromMap)
        {
            PropertyMap imageMap = new PropertyMap();
            Tizen.NUI.Object.GetProperty(SwigCPtr, FluxImageView.Property.IMAGE).Get(imageMap);
            imageMap.Merge(fromMap);

            SetProperty(FluxImageView.Property.IMAGE, new PropertyValue(imageMap));
        }

        private void UpdateImage(int key, PropertyValue value)
        {
            PropertyMap temp = new PropertyMap();

            if (_resourceUrl == "")
            {
                temp.Insert(ImageVisualProperty.URL, new PropertyValue(_resourceUrl));
                SetProperty(FluxImageView.Property.IMAGE, new PropertyValue(temp));
                return;
            }

            if (_border == null)
            {
                temp.Insert(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            }
            else
            {
                temp.Insert(Visual.Property.Type, new PropertyValue((int)Visual.Type.NPatch));
                temp.Insert(NpatchImageVisualProperty.Border, new PropertyValue(_border));
            }

            temp.Insert(NpatchImageVisualProperty.SynchronousLoading, new PropertyValue(_synchronosLoading));

            if (value != null)
            {
                temp.Insert(key, value);
            }

            UpdateImageMap(temp);

            temp.Dispose();
        }


        private void OnResourceLoaded(IntPtr view)
        {
            ResourceLoadedEventArgs e = new ResourceLoadedEventArgs
            {
                Status = (ResourceLoadingStatusType)Tizen.NUI.Interop.View.View_GetVisualResourceStatus(SwigCPtr, Property.IMAGE)
            };

            if (_resourceLoadedEventHandler != null)
            {
                _resourceLoadedEventHandler(this, e);
            }
        }

        /// <summary>
        /// Event arguments of resource ready.
        /// </summary>
        /// <code>
        /// FluxImageView fluxView = new FluxImageView();
        /// fluxView.ResourceReady += View_ResourceReady;
        /// ...
        /// ...
        /// private void View_ResourceReady(object sender, FluxImageView.ResourceReadyEventArgs e)
        ///{
        ///    FluxImageView imageView = sender as FluxImageView;
        ///    View view = e.View;
        ///  }
        /// </code>
        /// <since_tizen> 3 </since_tizen>
        public class ResourceReadyEventArgs : EventArgs
        {
            /// <summary>
            /// The view whose resource is ready.
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public View View { get; set; }
        }

        internal class ResourceLoadedEventArgs : EventArgs
        {
            public ResourceLoadingStatusType Status { get; set; } = ResourceLoadingStatusType.Invalid;
        }

        internal new class Property
        {
            internal static readonly int IMAGE = Tizen.NUI.Interop.ImageView.ImageView_Property_IMAGE_get();
            internal static readonly int PRE_MULTIPLIED_ALPHA = Tizen.NUI.Interop.ImageView.ImageView_Property_PRE_MULTIPLIED_ALPHA_get();
            internal static readonly int PIXEL_AREA = Tizen.NUI.Interop.ImageView.ImageView_Property_PIXEL_AREA_get();
            internal static readonly int ACTION_RELOAD = Tizen.NUI.Interop.ImageView.ImageView_IMAGE_VISUAL_ACTION_RELOAD_get();
            internal static readonly int ACTION_PLAY = Tizen.NUI.Interop.ImageView.ImageView_IMAGE_VISUAL_ACTION_PLAY_get();
            internal static readonly int ACTION_PAUSE = Tizen.NUI.Interop.ImageView.ImageView_IMAGE_VISUAL_ACTION_PAUSE_get();
            internal static readonly int ACTION_STOP = Tizen.NUI.Interop.ImageView.ImageView_IMAGE_VISUAL_ACTION_STOP_get();
        }

        private enum ImageType
        {
            /// <summary>
            /// For Normal Image.
            /// </summary>
            Normal = 0,

            /// <summary>
            /// For normal image, with synchronous loading and orientation correction property
            /// </summary>
            Specific = 1,

            /// <summary>
            /// For nine-patch image
            /// </summary>
            Npatch = 2,
        }

        private void OnBorderChanged(int x, int y, int width, int height)
        {
            Border = new Rectangle(x, y, width, height);
        }

        private void OnPixelAreaChanged(float x, float y, float z, float w)
        {
            //PixelArea = new RelativeVector4(x, y, z, w);
        }
    }
}
