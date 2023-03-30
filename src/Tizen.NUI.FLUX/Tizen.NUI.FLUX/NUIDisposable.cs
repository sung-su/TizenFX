/// @file NUIDisposable.cs
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
    /// NUIDisposable abstact class which enables user to define how to clean-up their unmanaged and managed resource in Application Thread.
    /// </summary>
    /// <code>
    /// public class DisposbleObject:NUIDisposable
    /// {
    ///     protected override void Dispose(DisposeTypes type)
    ///     {
    ///         if(disposed)
    ///         {
    ///             return;
    ///         }
    ///  
    ///         if (type == DisposeTypes.Explicit)
    ///         {
    ///             Called by User
    ///             Release your own managed resources here.
    ///             You should release all of your own disposable objects here.
    ///         }
    ///         
    ///         Release your own unmanaged resources here.   
    ///         You should not access any managed member here except static instance.
    ///         because the execution order of Finalizes is non-deterministic.
    ///         Unreference this from if a static instance refer to this.    
    ///         
    ///         disposed = true;
    ///     }
    /// }
    /// </code>

    public abstract class NUIDisposable : IDisposable
    {
        private bool isDisposeQueued = false;

        /// <summary>
        /// A Flat to check if it is already disposed.
        /// </summary>        
        protected bool disposed = false;

        /// <summary>
        /// Finalizer 
        /// </summary>
        ~NUIDisposable()
        {
            if (!isDisposeQueued)
            {
                isDisposeQueued = true;
                DisposeQueue.Instance.Add(this);
            }
        }

        /// <summary>
        /// Clean up managed and unmanaged resources explicitly.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Throw failed if the function is not called in Application thread.
        /// </exception>
        public void Dispose()
        {
            if (!Window.IsInstalled())
            {
                throw new InvalidOperationException("This API called from separate thread. This API must be called from MainThread.");
            }

            if (isDisposeQueued)
            {
                Dispose(DisposeTypes.Implicit);
            }
            else
            {        
                Dispose(DisposeTypes.Explicit);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>        
        /// User can override this function to define how to clean their own managed and unmanaged resources.
        /// </summary>
        /// <param name="type">Disposing type</param>     
        abstract protected void Dispose(DisposeTypes type);
    }
}
