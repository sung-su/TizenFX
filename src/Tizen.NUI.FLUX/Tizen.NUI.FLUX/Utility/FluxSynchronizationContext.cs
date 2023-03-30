/// @file FluxSynchronizationContext.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version>10.10.0</version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2022 Samsung Electronics Co., Ltd All Rights Reserved
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


using System.ComponentModel;
using System.Threading;
using Tizen.Applications;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// FluxSynchronizationContext class
    /// Provides the basic functionality for propagating a synchronization context in various synchronization models.
    /// </summary>
    /// <code>    
    /// FluxSynchronizationContext.ToUIThread.Post(callback, state);
    /// FluxSynchronizationContext.ToServiceThread.Post(callback, state);
    /// </code>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class FluxSynchronizationContext
    {
        /// <summary>
        /// Provides the basic functionality for propagating message to the UI Thread. 
        /// </summary>
        /// <code>    
        /// FluxSynchronizationContext.ToUIThread.Post(callback, state);
        /// FluxSynchronizationContext.ToUIThread.Send(callback, state);
        /// </code>
        public static class ToUIThread
        {
            private static SynchronizationContext uiThreadContext = new SynchronizationContext();

            internal static void Initialize()
            {
                if (TizenUISynchronizationContext.Current == null)
                {
                    TizenUISynchronizationContext.Initialize();
                }
                uiThreadContext = TizenUISynchronizationContext.Current;
            }

            /// <summary>
            /// Dispatches an asynchronous message to UI Thread.
            /// </summary>
            /// <param name="callback">The System.Threading.SendOrPostCallback delegate to call.</param>
            /// <param name="state">The object passed to the delegate.</param>
            public static void Post(SendOrPostCallback callback, object state)
            {
                uiThreadContext.Post(callback, state);
            }

            /// <summary>
            /// Dispatches a synchronous message to UI Thread.
            /// </summary>
            /// <param name="callback">The System.Threading.SendOrPostCallback delegate to call.</param>
            /// <param name="state">The object passed to the delegate.</param>
            public static void Send(SendOrPostCallback callback, object state)
            {
                uiThreadContext.Send(callback, state);
            }
        }

        /// <summary>
        /// Provides the basic functionality for propagating message to the Service(Main) Thread. 
        /// </summary>
        /// <code>    
        /// FluxSynchronizationContext.ToServiceThread.Post(callback, state);
        /// FluxSynchronizationContext.ToServiceThread.Send(callback, state);
        /// </code>
        public static class ToServiceThread
        {
            private static SynchronizationContext serviceThreadContext = new SynchronizationContext();

            internal static void Initialize()
            {
                if (TizenSynchronizationContext.Current == null)
                {
                    TizenSynchronizationContext.Initialize();
                }
                serviceThreadContext = TizenSynchronizationContext.Current;
            }

            /// <summary>
            /// Dispatches an asynchronous message to Service(Main) Thread.
            /// </summary>
            /// <param name="callback">The System.Threading.SendOrPostCallback delegate to call.</param>
            /// <param name="state">The object passed to the delegate.</param>
            public static void Post(SendOrPostCallback callback, object state)
            {
                serviceThreadContext.Post(callback, state);
            }

            /// <summary>
            /// Dispatches a synchronous message to Service(Main) Thread.
            /// </summary>
            /// <param name="callback">The System.Threading.SendOrPostCallback delegate to call.</param>
            /// <param name="state">The object passed to the delegate.</param>
            public static void Send(SendOrPostCallback callback, object state)
            {
                serviceThreadContext.Send(callback, state);
            }
        }
    }
}
