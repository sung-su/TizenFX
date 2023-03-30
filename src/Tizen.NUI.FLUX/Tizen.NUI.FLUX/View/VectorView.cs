/// @file VectorView.cs
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
using System.Runtime.InteropServices;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{

    /// <summary>
    /// VectorView is a abstract class for displaying a vector primitives.
    /// </summary>
    /// <code>
    /// vectorPrimitiveView = new VectorPrimitiveView();
    /// vectorPrimitiveView.Size2D = new Size2D(100, 100);
    /// Stroke strokeStyle = new Stroke(new SolidColor(Color.Red), 1);
    /// Fill fillStyle = new Fill(new SolidColor(Color.Blue));
    /// RectShape rectShape = new RectShape(strokeStyle, fillStyle);
    /// vectorPrimitiveView.SetShape(rectShape);
    /// </code>
    public abstract class VectorView : FluxView
    {
        /// <summary>
        /// Construct an VectorCanvasView.
        /// </summary>
        public VectorView() : this(Interop.VectorView.New(), true)
        {
            SecurityUtil.CheckPlatformPrivileges();
        }

        internal VectorView(IntPtr cPtr, bool cMemoryOwn) : base(Interop.VectorView.UpCast(cPtr), cMemoryOwn)
        {
            
        }

        /// <summary>
        /// Release Native Handle
        /// </summary>
        /// <param name="swigCPtr">HandleRef object holding corresponding native pointer.</param>
        /// <version>9.9.0</version>
        protected override void ReleaseSwigCPtr(HandleRef swigCPtr)
        {
            Interop.VectorView.Delete(swigCPtr);
        }

        /// <summary>
        /// Cleaning up managed and unmanaged resources
        /// </summary>
        /// <param name="type">Disposing type</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            base.Dispose(type);
        }

        /// <summary>
        /// update VG Data native side to draw
        /// </summary>
        /// <param name="shape">Shape set to the Vector view</param>
        public abstract void SetShape(Shape shape);

    }
}
