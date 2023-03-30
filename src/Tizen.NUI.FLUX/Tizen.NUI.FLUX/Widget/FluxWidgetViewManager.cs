/// @file FluxWidgetViewManager.cs
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

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// FluxWidgetViewManager manages addition of FluxWidgetView controls.
    /// This class provides the functionality of adding the widget views and controlling the widgets.
    /// </summary>
    /// <code>
    /// FluxWidgetView fluxWidgetView = FluxWidgetViewManager.Instance.AddWidget(WidgetAppID, new UnitSize(40, 40), 1000);
    /// </code>
    public class FluxWidgetViewManager : WidgetViewManager
    {
        private static FluxWidgetViewManager instance;

        /// <summary>
        /// Creates a new FluxWidgetView manager object.
        /// </summary>
        internal FluxWidgetViewManager() 
            : this(Tizen.NUI.Interop.WidgetViewManager.WidgetViewManager_New(Application.Instance.SwigCPtr, Applications.Application.Current.ApplicationInfo.ApplicationId), true)
        {
            SecurityUtil.CheckPlatformPrivileges();

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        internal FluxWidgetViewManager(IntPtr cPtr, bool cMemoryOwn) : base(Tizen.NUI.Interop.WidgetViewManager.Upcast(cPtr), cMemoryOwn)
        {
        }

        /// <summary>
        /// Gets the singleton of the FluxWidgetViewManager object.
        /// </summary>
		/// <version> 9.9.0 </version>
        public new static FluxWidgetViewManager Instance
        {
            get
            {
                if (!instance)
                {
                    instance = new FluxWidgetViewManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Creates a new FluxWidgetView object.
        /// </summary>
        /// <param name="widgetId">The widget ID.</param>
        /// <param name="unitSize">UnitSize of the widget.</param>
        /// <param name="updatePeriod">The period in milliseconds of updating contents of the widget. If 0 is set, the contents are not updated. Default value is 0.</param>
        /// <param name="contentInfo">Contents that will be given to the widget instance.</param>
        /// <returns>A handle to FluxWidgetView.</returns>
        public FluxWidgetView AddWidget(string widgetId, UnitSize unitSize, uint updatePeriod = 0, string contentInfo = "")
        {
            int widgetWidthSize = DisplayMetrics.Instance.UnitToPixel(unitSize.Width);
            int widgetHeightSize = DisplayMetrics.Instance.UnitToPixel(unitSize.Height);

            IntPtr ptr = Tizen.NUI.Interop.WidgetViewManager.AddWidget(SwigCPtr, widgetId, contentInfo, widgetWidthSize, widgetHeightSize, (float)updatePeriod / 1000);
            FluxWidgetView ret = new FluxWidgetView(ptr, true);

            if (NDalicPINVOKE.SWIGPendingException.Pending)
            {
                throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            return ret;
        }

        /// <summary>
        /// Remove a FluxWidgetView object.
        /// </summary>
        /// <param name="fluxWidgetView">widgetView to remove</param>
        /// <returns> True on success, false otherwise.</returns>
        /// <version> 9.9.0 </version>
        public bool RemoveWidget(FluxWidgetView fluxWidgetView)
        {
            if(fluxWidgetView != null)
            { 
                bool ret = NUI.Interop.WidgetViewManager.RemoveWidget(SwigCPtr, getCPtr(fluxWidgetView));

                if (NDalicPINVOKE.SWIGPendingException.Pending)
                {
                    throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                }

                return ret;
            }

            return false;
        }

        /// <summary>
        /// Release Native Handle
        /// </summary>        
        /// <param name="swigCPtr">HandleRef object holding corresponding native pointer.</param>
        /// <version>9.9.0</version>
        protected override void ReleaseSwigCPtr(HandleRef swigCPtr)
        {
            NUI.Interop.WidgetViewManager.DeleteWidgetViewManager(swigCPtr);
        }
    }
}