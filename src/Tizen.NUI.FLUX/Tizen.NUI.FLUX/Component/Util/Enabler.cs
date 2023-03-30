/**
*Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
*For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
*/
/// @file Enabler.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 9.9.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
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

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// Util class to define Enable/Disable/IsEnabled property with specific key
    /// </summary>
    /// <code>
    /// public Enabler<string> enabler = new Enabler<string>();
    /// enabler.RegisterKey( "Touch" , EnableTouch , DisableTouch ); 
    /// 
    /// enabler["Touch"] = true; // In this case, EnableTouch will be called. 
    /// 
    /// if( enabler["Touch"] == true )
    /// {
    ///         // do something
    ///  }
    ///  
    /// enabler.Clear();
    /// 
    /// </code>
    public class Enabler<T>
    {
        /// <summary>
        /// Indexer for user usability
        /// </summary>
        /// <code>
        /// 
        /// enabler["Touch"] = true; // In this case, EnableTouch will be called. 
        /// 
        /// if( enabler["Touch"] == true )
        /// {
        ///         // do something
        ///  } 
        /// 
        /// </code>

        public bool this[T key]
        {
            get => GetValue(key);
            set => SetValue(key, value);
        }

        /// <summary>
        /// Register key and action to want to make enabler
        /// </summary>
        /// <param name="key"> key value of invoker </param>
        /// <param name="trueAction"> Action when value change to TRUE </param>
        /// <param name="falseAction">Action when value change to FALSE </param>
        /// <code>
        /// public Enabler<string> enabler = new Enabler<string>();
        /// enabler.RegisterKey( "Touch" , EnableTouch , DisableTouch ); 
        /// 
        /// private void EnableTouch()
        /// {
        ///     // do something
        /// }
        ///   
        /// private void DisableTouch()
        /// {
        ///     // do something
        /// }
        /// </code>

        public bool RegisterKey(T key, Action trueAction = null, Action falseAction = null, bool isEnabled = false)
        {
            bool ret = false;
            if (key == null)
            {
                return ret;
            }


            if (invokerList.TryGetValue(key, out Invoker invoker) == false)
            {
                invoker = new Invoker(key)
                {
                    isEnabled = isEnabled
                };
                invokerList[key] = invoker;
                ret = true;
            }

            if (trueAction != null)
            {
                invoker.TrueAction += trueAction;
                ret = true;
            }

            if (falseAction != null)
            {
                invoker.FalseAction += falseAction;
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// Clear all action and invoker in enabler
        /// </summary>
        /// <code>
        /// 
        /// enabler["Touch"] = true; // In this case, EnableTouch will be called. 
        /// 
        /// if( enabler["Touch"] == true )
        /// {
        ///         // do something
        ///  } 
        ///  
        /// enabler.Clear(); 
        /// 
        /// </code>
        public void Clear()
        {
            foreach (KeyValuePair<T, Invoker> item in invokerList)
            {
                if (item.Value is Invoker del)
                {
                    if (del.isEnabled == true)
                    {
                        del.FalseAction?.Invoke();
                    }
                    del.Clear();
                }
            }

            invokerList.Clear();
        }

        private Invoker GetInvoker(T key)
        {

            if (invokerList.TryGetValue(key, out Invoker retInvoker) == false)
            {
                throw new ArgumentException(key + " is invalid key. Please check this");
            }

            return retInvoker;
        }

        private bool GetValue(T key)
        {
            Invoker invoker = GetInvoker(key);

            return (invoker != null) ? (invoker.isEnabled) : false;
        }

        private void SetValue(T key, bool value)
        {
            Invoker invoker = GetInvoker(key);

            if (invoker == null)
            {
                return;
            }

            if (invoker.isEnabled == value)
            {
                return;
            }

            invoker.isEnabled = value;

            if (value == true)
            {
                if (invoker.TrueAction != null)
                {
                    invoker.TrueAction.Invoke();
                }
            }
            else
            {
                if (invoker.FalseAction != null)
                {
                    invoker.FalseAction.Invoke();
                }

            }
        }


        private class Invoker
        {
            public Invoker(T key)
            {
                this.key = key;
            }

            public void Clear()
            {
                TrueAction = null;
                FalseAction = null;
            }

            internal Action TrueAction;
            internal Action FalseAction;

            internal bool isEnabled = false;
            internal T key;
        }

        private readonly Dictionary<T, Invoker> invokerList = new Dictionary<T, Invoker>();

    }
}
