/// @file GCNotification.cs
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
using System.Threading;

namespace Tizen.NUI.FLUX
{    
    internal class GCNotification
    {
        private static Action<int> gcDone = null;   

        public static event Action<int> GCDone
        {
            add
            {
                if(gcDone == null)
                {                    
                    new GenObject(0);
                    new GenObject(GC.MaxGeneration);
                }
                gcDone += value;
            }
            remove
            {
                gcDone -= value;
            }
        }                

        private class GenObject
        {
            private int m_generation;
            public GenObject(int generation)
            {
                m_generation = generation;
            }
            ~GenObject()
            {
                if(GC.GetGeneration(this) >= m_generation)
                {                    
                    Volatile.Read(ref gcDone)?.Invoke(m_generation);
                }
                                
                if((gcDone != null) && !Environment.HasShutdownStarted)
                {                
                    if(m_generation == 0)
                    {
                        new GenObject(0);
                    }
                    else
                    {                 
                        GC.ReRegisterForFinalize(this);
                    }
                }
                else
                {
                    /*Let the objects go away */
                }
            }            
        }
    }
}