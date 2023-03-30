/// @file FluxWidgetApplication.cs
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
using System.Collections.Generic;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Represents an application that have UI screen. The FluxWidgetApplication class has a default stage.
    /// </summary>
    /// <code>
    /// class Program : FluxWidgetApplication
    /// {
    /// 
    /// public Program(Type widgetType) : base(widgetType)
    /// {
    /// }
    /// 
    /// protected override void OnCreate()
    /// {
    ///    base.OnCreate();
    /// }
    /// 
    /// static void Main(string[] args)
    /// {
    ///    var app = new Program(typeof(MyWidget));
    ///    app.Run(args);
    /// }
    /// </code>
    public class FluxWidgetApplication : NUIWidgetApplication
    {
        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <remarks>Widget ID will be replaced as the application ID.</remarks>
        /// <param name="widgetType">Derived widget class type.</param>
        public FluxWidgetApplication(Type widgetType) : base(widgetType)
        {
            SecurityUtil.CheckPlatformPrivileges();
        }

        /// <summary>
        /// The constructor for multi widget class and instance.
        /// </summary>
        /// <param name="widgetTypes">List of derived widget class type.</param>
        public FluxWidgetApplication(Dictionary<Type, string> widgetTypes) : base(widgetTypes)
        {
            SecurityUtil.CheckPlatformPrivileges();
        }

        /// <summary>
        /// Overrides this method if want to handle behavior before calling OnCreate().
        /// </summary>
        protected override void OnPreCreate()
        {
            base.OnPreCreate();
            Initialize();
        }

        /// <summary>
        /// Overrides this method if want to handle OnTerminate behavior.
        /// </summary>
        protected override void OnTerminate()
        {
            CleanUp();
            base.OnTerminate();
        }

        private void Initialize()
        {
            FluxApplicationInitializer.Initialize();
            ObjectDump.Initialize();
        }

        private void CleanUp()
        {
            FluxApplicationInitializer.CleanUp();
            ObjectDump.CleanUp();
        }
    }
}