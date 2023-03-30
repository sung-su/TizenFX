/// @file FluxWidgetView.cs
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

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// The WidgetView is a class for displaying the widget image and controlling the widget.
    /// </summary>
    /// <code>
    /// FluxWidgetView fluxWidgetView = FluxWidgetViewManager.Instance.AddWidget(WidgetAppID, new UnitSize(40, 40), 1000);
    /// </code>
    public class FluxWidgetView : FluxView
    {
        private EventHandler<WidgetViewEventArgs> _widgetAddedEventHandler;
        private WidgetAddedEventCallbackType _widgetAddedEventCallback;
        private EventHandler<WidgetViewEventArgs> _widgetContentUpdatedEventHandler;
        private WidgetContentUpdatedEventCallbackType _widgetContentUpdatedEventCallback;
        private EventHandler<WidgetViewEventArgs> _widgetDeletedEventHandler;
        private WidgetDeletedEventCallbackType _widgetDeletedEventCallback;
        private EventHandler<WidgetViewEventArgs> _widgetCreationAbortedEventHandler;
        private WidgetCreationAbortedEventCallbackType _widgetCreationAbortedEventCallback;
        private EventHandler<WidgetViewEventArgs> _widgetUpdatePeriodChangedEventHandler;
        private WidgetUpdatePeriodChangedEventCallbackType _widgetUpdatePeriodChangedEventCallback;
        private EventHandler<WidgetViewEventArgs> _widgetFaultedEventHandler;
        private WidgetFaultedEventCallbackType _widgetFaultedEventCallback;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void WidgetAddedEventCallbackType(IntPtr data);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void WidgetContentUpdatedEventCallbackType(IntPtr data);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void WidgetDeletedEventCallbackType(IntPtr data);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void WidgetCreationAbortedEventCallbackType(IntPtr data);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void WidgetUpdatePeriodChangedEventCallbackType(IntPtr data);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void WidgetFaultedEventCallbackType(IntPtr data);

        /// <summary>
        /// FluxWidgetView fluxWidgetView = FluxWidgetViewManager.Instance.AddWidget(WidgetAppID, new UnitSize(40, 40), 1000);
        /// </summary>
        internal FluxWidgetView(IntPtr cPtr, bool cMemoryOwn) : base(Tizen.NUI.Interop.WidgetView.WidgetView_SWIGUpcast(cPtr), cMemoryOwn)
        {
            SecurityUtil.CheckPlatformPrivileges();
        }

        /// <summary>
        /// An event for the WidgetAdded signal which can be used to subscribe or unsubscribe the event handler.
        /// </summary>
        public event EventHandler<WidgetViewEventArgs> WidgetAdded
        {
            add
            {
                if (_widgetAddedEventHandler == null)
                {
                    _widgetAddedEventCallback = OnWidgetAdded;
                    WidgetAddedSignal().Connect(_widgetAddedEventCallback);
                }
                _widgetAddedEventHandler += value;
            }

            remove
            {
                _widgetAddedEventHandler -= value;

                if (_widgetAddedEventHandler == null && WidgetAddedSignal().Empty() == false)
                {
                    WidgetAddedSignal().Disconnect(_widgetAddedEventCallback);
                }
            }
        }

        /// <summary>
        /// An event for the WidgetContentUpdated signal which can be used to subscribe or unsubscribe the event handler.
        /// </summary>
        /// <version>10.10.0</version>
        public event EventHandler<WidgetViewEventArgs> WidgetContentUpdated
        {
            add
            {
                if (_widgetContentUpdatedEventHandler == null)
                {
                    _widgetContentUpdatedEventCallback = OnWidgetContentUpdated;
                    WidgetContentUpdatedSignal().Connect(_widgetContentUpdatedEventCallback);
                }
                _widgetContentUpdatedEventHandler += value;
            }

            remove
            {
                _widgetContentUpdatedEventHandler -= value;

                if (_widgetContentUpdatedEventHandler == null && WidgetContentUpdatedSignal().Empty() == false)
                {
                    WidgetContentUpdatedSignal().Disconnect(_widgetContentUpdatedEventCallback);
                }
            }
        }

        /// <summary>
        /// An event for the WidgetDeleted signal which can be used to subscribe or unsubscribe the event handler.
        /// </summary>
        public event EventHandler<WidgetViewEventArgs> WidgetDeleted
        {
            add
            {
                if (_widgetDeletedEventHandler == null)
                {
                    _widgetDeletedEventCallback = OnWidgetDeleted;
                    WidgetDeletedSignal().Connect(_widgetDeletedEventCallback);
                }
                _widgetDeletedEventHandler += value;
            }

            remove
            {
                _widgetDeletedEventHandler -= value;

                if (_widgetDeletedEventHandler == null && WidgetDeletedSignal().Empty() == false)
                {
                    WidgetDeletedSignal().Disconnect(_widgetDeletedEventCallback);
                }
            }
        }

        /// <summary>
        /// An event for the WidgetCreationAborted signal which can be used to subscribe or unsubscribe the event handler.
        /// </summary>
        /// <version>10.10.0</version>
        public event EventHandler<WidgetViewEventArgs> WidgetCreationAborted
        {
            add
            {
                if (_widgetCreationAbortedEventHandler == null)
                {
                    _widgetCreationAbortedEventCallback = OnWidgetCreationAborted;
                    WidgetCreationAbortedSignal().Connect(_widgetCreationAbortedEventCallback);
                }
                _widgetCreationAbortedEventHandler += value;
            }

            remove
            {
                _widgetCreationAbortedEventHandler -= value;

                if (_widgetCreationAbortedEventHandler == null && WidgetCreationAbortedSignal().Empty() == false)
                {
                    WidgetCreationAbortedSignal().Disconnect(_widgetCreationAbortedEventCallback);
                }
            }
        }

        /// <summary>
        /// An event for the WidgetUpdatePeriodChanged signal which can be used to subscribe or unsubscribe the event handler.
        /// </summary>
        /// <version>10.10.0</version>
        public event EventHandler<WidgetViewEventArgs> WidgetUpdatePeriodChanged
        {
            add
            {
                if (_widgetUpdatePeriodChangedEventHandler == null)
                {
                    _widgetUpdatePeriodChangedEventCallback = OnWidgetUpdatePeriodChanged;
                    WidgetUpdatePeriodChangedSignal().Connect(_widgetUpdatePeriodChangedEventCallback);
                }
                _widgetUpdatePeriodChangedEventHandler += value;
            }

            remove
            {
                _widgetUpdatePeriodChangedEventHandler -= value;

                if (_widgetUpdatePeriodChangedEventHandler == null && WidgetUpdatePeriodChangedSignal().Empty() == false)
                {
                    WidgetUpdatePeriodChangedSignal().Disconnect(_widgetUpdatePeriodChangedEventCallback);
                }
            }
        }

        /// <summary>
        /// An event for the WidgetFaulted signal which can be used to subscribe or unsubscribe the event handler.
        /// </summary>
        /// <version>10.10.0</version>
        public event EventHandler<WidgetViewEventArgs> WidgetFaulted
        {
            add
            {
                if (_widgetFaultedEventHandler == null)
                {
                    _widgetFaultedEventCallback = OnWidgetFaulted;
                    WidgetFaultedSignal().Connect(_widgetFaultedEventCallback);
                }
                _widgetFaultedEventHandler += value;
            }

            remove
            {
                _widgetFaultedEventHandler -= value;

                if (_widgetFaultedEventHandler == null && WidgetFaultedSignal().Empty() == false)
                {
                    WidgetFaultedSignal().Disconnect(_widgetFaultedEventCallback);
                }
            }
        }


        /// <summary>
        /// Pauses a given widget.
        /// </summary>
        public void PauseWidget()
        {
            Tizen.NUI.Interop.WidgetView.WidgetView_PauseWidget(SwigCPtr);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        /// <summary>
        /// Resumes a given widget.
        /// </summary>
        public void ResumeWidget()
        {
            Tizen.NUI.Interop.WidgetView.WidgetView_ResumeWidget(SwigCPtr);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        /// <summary>
        /// Cancels the touch event procedure.
        /// If you call this function after feed the touch down event, the widget will get ON_HOLD events.
        /// If a widget gets ON_HOLD event, it will not do anything even if you feed touch up event.
        /// </summary>
        /// <returns>True on success, false otherwise.</returns>
        /// <version>10.10.0</version>
        public bool CancelTouchEvent()
        {
            bool ret = Tizen.NUI.Interop.WidgetView.CancelTouchEvent(SwigCPtr);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            return ret;
        }

        /// <summary>
        /// Activates a widget in the faulted state.
        /// A widget in faulted state must be activated before adding the widget.
        /// </summary>
        /// <version>10.10.0</version>
        public void ActivateFaultedWidget()
        {
            Tizen.NUI.Interop.WidgetView.ActivateFaultedWidget(SwigCPtr);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }


        /// <summary>
        /// Terminate a widget instance.
        /// </summary>
        /// <deprecated> Deprecated since 9.9.0. Use FluxWidgetViewManager.Instance.RemoveWidget instead.</deprecated>
        [Obsolete("Please do not use! This will be deprecated, Please use FluxWidgetViewManager.Instance.RemoveWidget instead!")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void TerminateWidget()
        {
            RestrictedModeManager.Instance.NotifyRestrictedOperation("use FluxWidgetViewManager.Instance.RemoveWidget instead of new FluxWidgetManager");

            Tizen.NUI.Interop.WidgetView.WidgetView_TerminateWidget(SwigCPtr);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        /// <summary>
        /// Gets the ID of the widget.
        /// </summary>
        public string WidgetID
        {
            get
            {
                GetProperty(WidgetView.Property.WidgetId).Get(out string value);
                return value;
            }
        }

        /// <summary>
        /// Gets the ID of the instance.
        /// </summary>
        /// <version>10.10.0</version>
        public string InstanceID
        {
            get
            {
                GetProperty(WidgetView.Property.InstanceId).Get(out string value);
                return value;
            }
        }

        /// <summary>
        /// Gets the content info.
        /// </summary>
        public string ContentInfo
        {
            get
            {
                GetProperty(WidgetView.Property.ContentInfo).Get(out string value);
                return value;
            }
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title
        {
            get
            {
                GetProperty(WidgetView.Property.TITLE).Get(out string value);
                return value;
            }
        }

        /// <summary>
        /// Gets the update period.
        /// </summary>
        public float UpdatePeriod
        {
            get
            {
                GetProperty(WidgetView.Property.UpdatePeriod).Get(out float value);
                return value;
            }
        }

        /// <summary>
        /// Gets or sets whether to set the preview.
        /// </summary>
        public bool EnabledPreview
        {
            get
            {
                GetProperty(WidgetView.Property.PREVIEW).Get(out bool value);
                return value;
            }
            set => SetProperty(WidgetView.Property.PREVIEW, new Tizen.NUI.PropertyValue(value));
        }

        /// <summary>
        /// Gets or sets whether to set the loading text.
        /// </summary>
        public bool EnabledLoadingText
        {
            get
            {
                GetProperty(WidgetView.Property.LoadingText).Get(out bool value);
                return value;
            }
            set => SetProperty(WidgetView.Property.LoadingText, new Tizen.NUI.PropertyValue(value));
        }

        /// <summary>
        /// Gets or sets whether the widget state is faulted or not.
        /// </summary>
        /// <version>10.10.0</version>
        public bool WidgetStateFaulted
        {
            get
            {
                GetProperty(WidgetView.Property.WidgetStateFaulted).Get(out bool value);
                return value;
            }
            set => SetProperty(WidgetView.Property.WidgetStateFaulted, new Tizen.NUI.PropertyValue(value));
        }

        /// <summary>
        /// Gets or sets whether the widget is to delete permanently or not.
        /// </summary>
        /// <version>10.10.0</version>
        public bool PermanentDelete
        {
            get
            {
                GetProperty(WidgetView.Property.PermanentDelete).Get(out bool value);
                return value;
            }
            set => SetProperty(WidgetView.Property.PermanentDelete, new Tizen.NUI.PropertyValue(value));
        }

        /// <summary>
        /// Gets or sets retry text.
        /// </summary>
        /// <version>10.10.0</version>
        public PropertyMap RetryText
        {
            get
            {
                PropertyMap retValue = new PropertyMap();
                PropertyValue retryText = GetProperty(WidgetView.Property.RetryText);
                retryText?.Get(retValue);
                retryText?.Dispose();
                return retValue;
            }
            set => SetProperty(WidgetView.Property.RetryText, new Tizen.NUI.PropertyValue(value));
        }

        /// <summary>
        /// Gets or sets effect.
        /// </summary>
        /// <version>10.10.0</version>
        public PropertyMap Effect
        {
            get
            {
                PropertyMap retValue = new PropertyMap();
                PropertyValue effect = GetProperty(WidgetView.Property.EFFECT);
                effect?.Get(retValue);
                effect?.Dispose();
                return retValue;
            }
            set => SetProperty(WidgetView.Property.EFFECT, new Tizen.NUI.PropertyValue(value));
        }

        /// <summary>
        /// Event arguments of the widget view.
        /// </summary>
        /// <code>
        /// WidgetViewEventArgs e = new WidgetViewEventArgs();
        /// string str = e.ToString();
        /// </code>
        public class WidgetViewEventArgs : EventArgs
        {
            /// <summary>
            /// The widet view.
            /// </summary>
            public FluxWidgetView FluxWidgetView { get; }

            /// <summary>
            /// Constructor for WidgetViewEventArgs.
            /// </summary>
            /// <version> 10.10.0 </version>
            [EditorBrowsable(EditorBrowsableState.Never)]
            public WidgetViewEventArgs(FluxWidgetView fluxWidgetView)
            {
                FluxWidgetView = fluxWidgetView;
            }
        }

        /// <summary>
        /// Release Native Handle
        /// </summary>        
        /// <param name="swigCPtr">HandleRef object holding corresponding native pointer.</param>
        /// <version>9.9.0</version>
        protected override void ReleaseSwigCPtr(HandleRef swigCPtr)
        {
            Tizen.NUI.Interop.WidgetView.delete_WidgetView(swigCPtr);
        }

        /// <summary>
        /// To make the FluxWidgetView instance be disposed.
        /// </summary>
        /// <param name="type">Disposing type</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Release your own unmanaged resources here.
                //You should not access any managed member here except static instance.
                //because the execution order of Finalizes is non-deterministic.
                if (_widgetAddedEventCallback != null)
                {
                    WidgetAddedSignal().Disconnect(_widgetAddedEventCallback);
                }

                if (_widgetDeletedEventCallback != null)
                {
                    WidgetDeletedSignal().Disconnect(_widgetDeletedEventCallback);
                }
            }

            base.Dispose(type);
        }

        internal static FluxWidgetView GetWidgetViewFromPtr(IntPtr cPtr)
        {
            if (cPtr == global::System.IntPtr.Zero)
            {
                return null;
            }
            FluxWidgetView ret = Registry.GetManagedBaseHandleFromNativePtr(cPtr) as FluxWidgetView;

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            return ret;
        }

        internal WidgetViewSignal WidgetAddedSignal()
        {
            WidgetViewSignal ret = new WidgetViewSignal(Tizen.NUI.Interop.WidgetView.WidgetView_WidgetAddedSignal(SwigCPtr), false);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            return ret;
        }

        internal WidgetViewSignal WidgetDeletedSignal()
        {
            WidgetViewSignal ret = new WidgetViewSignal(Tizen.NUI.Interop.WidgetView.WidgetView_WidgetDeletedSignal(SwigCPtr), false);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            return ret;
        }

        internal WidgetViewSignal WidgetContentUpdatedSignal()
        {
            WidgetViewSignal ret = new WidgetViewSignal(Tizen.NUI.Interop.WidgetView.WidgetContentUpdatedSignal(SwigCPtr), false);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            return ret;
        }

        internal WidgetViewSignal WidgetCreationAbortedSignal()
        {
            WidgetViewSignal ret = new WidgetViewSignal(Tizen.NUI.Interop.WidgetView.WidgetCreationAbortedSignal(SwigCPtr), false);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            return ret;
        }

        internal WidgetViewSignal WidgetUpdatePeriodChangedSignal()
        {
            WidgetViewSignal ret = new WidgetViewSignal(Tizen.NUI.Interop.WidgetView.WidgetUpdatePeriodChangedSignal(SwigCPtr), false);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            return ret;
        }

        internal WidgetViewSignal WidgetFaultedSignal()
        {
            WidgetViewSignal ret = new WidgetViewSignal(Tizen.NUI.Interop.WidgetView.WidgetFaultedSignal(SwigCPtr), false);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
            return ret;
        }

        // Callback for WidgetView WidgetAdded signal
        internal void OnWidgetAdded(IntPtr data)
        {
            if (data != null)
            {
                WidgetViewEventArgs e = new WidgetViewEventArgs(GetWidgetViewFromPtr(data));
                _widgetAddedEventHandler?.Invoke(this, e);
            }
        }

        // Callback for WidgetView WidgetDeleted signal
        internal void OnWidgetDeleted(IntPtr data)
        {
            if (data != null)
            {
                WidgetViewEventArgs e = new WidgetViewEventArgs(GetWidgetViewFromPtr(data));
                _widgetDeletedEventHandler?.Invoke(this, e);
            }
        }

        // Callback for WidgetView WidgetContentUpdated signal
        internal void OnWidgetContentUpdated(IntPtr data)
        {
            if (data != null)
            {
                WidgetViewEventArgs e = new WidgetViewEventArgs(GetWidgetViewFromPtr(data));
                _widgetContentUpdatedEventHandler?.Invoke(this, e);
            }
        }

        // Callback for WidgetView WidgetCreationAborted signal
        internal void OnWidgetCreationAborted(IntPtr data)
        {
            if (data != null)
            {
                WidgetViewEventArgs e = new WidgetViewEventArgs(GetWidgetViewFromPtr(data));
                _widgetCreationAbortedEventHandler?.Invoke(this, e);
            }
        }

        // Callback for WidgetView WidgetUpdatePeriodChanged signal
        internal void OnWidgetUpdatePeriodChanged(IntPtr data)
        {
            if (data != null)
            {
                WidgetViewEventArgs e = new WidgetViewEventArgs(GetWidgetViewFromPtr(data));
                _widgetUpdatePeriodChangedEventHandler?.Invoke(this, e);
            }
        }

        // Callback for WidgetView WidgetFaulted signal
        internal void OnWidgetFaulted(IntPtr data)
        {
            if (data != null)
            {
                WidgetViewEventArgs e = new WidgetViewEventArgs(GetWidgetViewFromPtr(data));
                _widgetFaultedEventHandler?.Invoke(this, e);
            }
        }
    }
}