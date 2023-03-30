/// @file EventService.cs
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
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// This is a helper class to support async event handler
    /// </summary>
    /// <code>
    /// EventService.Instance.ASyncEvent += Instance_ASync0;
    /// private void Instance_ASync0(object sender, EventArgs e)
    /// {
    ///    Log.Error("TV.FLUX.Example", "#0 async event handler is invoked");
    /// }
    /// </code>
    public sealed class EventService
    {
        /// <summary>
        /// get the instance of EventService class
        /// </summary>
        public static EventService Instance
        {
            get
            {
                return instance;
            }
        }
        /// <summary>
        /// add/remove async event handler
        /// </summary>
        public event EventHandler<EventArgs> ASyncEvent
        {
            add
            {
                eventHandlers[index] += value;

                if (eventHandlers[index].GetInvocationList().Length > 0)
                {
                    timer.Start();
                }
            }
            remove
            {
                eventHandlers[index] -= value;

                if (eventHandlers[index].GetInvocationList().Length <= 0)
                {
                    timer.Stop();
                }
            }
        }

        //Cleaning up unmanaged resources has to be done carefully and internally just before application exits. 
        //since it will make the instance unusable permanently.
        internal void CleanUp()
        {
            if (cleanedup != true)
            {             
                timer.Tick -= OnEventDispatcher;
                timer.Dispose();
                timer = null;

                cleanedup = true;
            }
        }
        
        private EventService()
        {
            SecurityUtil.CheckPlatformPrivileges();
            timer = new Tizen.NUI.Timer(1);
            timer.Tick += OnEventDispatcher;

            eventHandlers = new EventHandler<EventArgs>[2];
            index = 0;
        }
        private bool OnEventDispatcher(object source, Timer.TickEventArgs e)
        {
            if (eventHandlers[index] != null)
            {
                int currentIndex = index;
                // increase the index to get the new event in event handler
                index = (index + 1) % 2;

                // invokes handlers
                eventHandlers[currentIndex](instance, new EventArgs());

                // clear all events handlers
                eventHandlers[currentIndex] = null;

                // check if the new added event exsits or not.
                if (eventHandlers[index] != null && eventHandlers[index].GetInvocationList().Length > 0)
                {
                    // this handler should be invokded to handle new events.
                    return true;
                }
            }

            // this handler should be stopped.
            return false;
        }

        private bool cleanedup = false;
        private int index;
        private EventHandler<EventArgs> [] eventHandlers;
        private static Timer timer;
        private static readonly EventService instance = new EventService();
    }
}
