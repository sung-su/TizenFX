/// @file FluxApplication.cs
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
using Tizen.Applications;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// The base class of flux tv nui application. Your ui application code should inherit this class.
    /// </summary>
    /// <code>
    /// class MyUIApp : FluxApplication
    /// {
    ///     static void Main(string[] args)
    ///     {
    ///         MyUIApp uiApp = new MyUIApp();
    ///         uiApp.Run(args);
    ///     }
    /// }
    /// </code>
    public class FluxApplication : NUIApplication
    {
        private static FluxCoreTask coreTask = null;
        private static bool useSeparatedUIThread = false;

        /// <summary>
        /// Constructor to instantiate the FluxApplication class.
        /// <param name="windowMode">window mode for deciding whether application window is opaque or transparent.</param>
        /// </summary>
        public FluxApplication(WindowMode windowMode = WindowMode.Opaque) : base("", windowMode)
        {
            SecurityUtil.CheckPlatformPrivileges();
        }

        /// <summary>
        /// Constructor to instantiate the FluxApplication class.
        /// <param name="styleSheet">The stylesheet url</param>
        /// <param name="windowMode">window mode for deciding whether application window is opaque or transparent.</param>
        /// </summary>
        public FluxApplication(string styleSheet, WindowMode windowMode = WindowMode.Opaque) : base(styleSheet, windowMode)
        {
            SecurityUtil.CheckPlatformPrivileges();
        }

        /// <summary>
        /// Constructor to instantiate the FluxApplication class.
        /// The constructor with a stylesheet, size, position, boderInterface and window mode.
        /// </summary>
        /// <param name="styleSheet">The styleSheet URL.</param>
        /// <param name="windowSize">The window size. It is full size if set null</param>
        /// <param name="windowPosition">The window position. It is 0, 0 if set null</param>
        /// <param name="borderInterface"><see cref="Tizen.NUI.IBorderInterface"/>The border interface</param>
        /// <exception cref="ArgumentNullException">Thrown when borderInterface instance is null</exception>
        /// <param name="windowMode">window mode for deciding whether application window is opaque or transparent.</param>
        /// <version> 10.10.0 </version>
        public FluxApplication(string styleSheet, Size2D windowSize, Position2D windowPosition, IBorderInterface borderInterface, WindowMode windowMode = WindowMode.Opaque) : base(styleSheet, windowSize, windowPosition, borderInterface, windowMode)
        {
            if (borderInterface == null)
            {
                throw new ArgumentNullException("BorderInterface is null.");
            }

            SecurityUtil.CheckPlatformPrivileges();
        }

        /// <summary>
        /// Constructor to instantiate the FluxApplication class.
        /// The constructor with window mode and ui thread enable mode.
        /// </summary>
        /// <param name="windowMode">window mode for deciding whether application window is opaque or transparent.</param>
        /// <param name="separateUIThread">If the separateUIThread is enabled, the main thread and UI thread are separated. </param>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public FluxApplication(WindowMode windowMode, bool separateUIThread) : base("", windowMode, separateUIThread ? coreTask = new FluxCoreTask() : null)
        {
            useSeparatedUIThread = separateUIThread;
            SecurityUtil.CheckPlatformPrivileges();
            if (useSeparatedUIThread == true)
            {
                coreTask.CreatedEventHandler += ServiceCreated;
                coreTask.TerminatedEventHandler += ServiceTerminated;
            }
        }

        /// <summary>
        /// Dispatches an asynchronous message to the ecore main loop of the UIThread.
        /// </summary>
        /// <param name="runner"> The runner callback </param>
        /// <exception cref="ArgumentNullException">Thrown when the runner is null.</exception>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ToUIThreadPost(Action runner)
        {
            if (runner != null)
            {
                Post(runner);
            }
        }

        /// <summary>
        /// Dispatches an asynchronous message to the gmain loop of the CoreTask.
        /// </summary>
        /// <param name="runner"> The runner callback </param>
        /// <exception cref="ArgumentNullException">Thrown when the runner is null.</exception>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ToServiceThreadPost(Action runner)
        {
            if (runner != null)
            {
                CoreTask.Post(runner);
            }
        }

        /// <summary>
        /// Dispatches an asynchronous message to the gmain loop of the CoreTask.
        /// </summary>
        /// <param name="runner"> The runner callback </param>
        /// <exception cref="ArgumentNullException">Thrown when the runner is null.</exception>
        /// <version>10.10.0</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void Post(Action runner)
        {
            CoreApplication.Post(runner);
        }

        /// <summary>
        /// Runs the FluxApplication.
        /// </summary>
        /// <param name="args">Arguments from commandline.</param>
        public override void Run(string[] args)
        {
            if (UIThreadSeparated == false)
            {
                FluxSynchronizationContext.ToServiceThread.Initialize();
                OnServiceCreate();
            }
            base.Run(args);
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
        /// Overrides this method if want to handle behavior when the application is created.        
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the application is resumed.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the application is terminated.
        /// If base.OnTerminate() is not called, the event 'Terminated' will not be emitted.
        /// </summary>
        protected override void OnTerminate()
        {
            if (UIThreadSeparated == false)
            {
                OnServiceTerminate();
            }
            CleanUp();
            base.OnTerminate();
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the service thread(main thread) is created.
        /// </summary>
        /// <version>10.10.0</version>
        protected virtual void OnServiceCreate()
        {
        }

        /// <summary>
        /// Overrides this method if want to handle behavior when the service thread(main thread) is terminated.
        /// </summary>
        /// <version>10.10.0</version>
        protected virtual void OnServiceTerminate()
        {
        }

        internal static bool UIThreadSeparated => useSeparatedUIThread;

        private void Initialize()
        {
            FluxSynchronizationContext.ToUIThread.Initialize();
            FluxApplicationInitializer.Initialize();
            FluxSimulatorInitializer.Initialize();
            GraphicZoomManager.Initialize();
            ObjectDump.Initialize();
            TVWindow.CheckKeyInitialize();
        }

        private void CleanUp()
        {
            if (UIThreadSeparated == true)
            {
                coreTask.CreatedEventHandler -= ServiceCreated;
                coreTask.TerminatedEventHandler -= ServiceTerminated;
            }
            ObjectDump.CleanUp();
            FluxApplicationInitializer.CleanUp();
            GraphicZoomManager.CleanUp();
            TVWindow.CheckKeyDestroy();
        }

        private void ServiceCreated(object sender, EventArgs e)
        {
            OnServiceCreate();
        }

        private void ServiceTerminated(object sender, EventArgs e)
        {
            OnServiceTerminate();
        }
    }
}