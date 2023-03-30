/// @file GLSurfaceViewBaseRenderer.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 9.9.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
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

using System.Threading;
using System;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// Renderer extension for GLSurfaceView, This is responsible to make OpenGL
    /// calls to reander a frame.
    /// </summary>
    /// <code>
    /// Implement Renderer Interface 
    /// class Renderer : GLsurfaceViewRendererBase
    /// {
    ///     public override void Initialize(uint width, uint height){}
    ///     public override void Draw(){ 
    ///         // if you want to synchronized update, 
    ///         this.EndSynchronizedUpdate();
    ///     }
    ///     public override void Terminate(){}
    /// }
    /// 
    /// Create GLSurfaceView
    /// glSurfaceView = new GLSurfaceView(800, 800);
    /// Renderer rend = new Renderer();
    /// glSurfaceView.SetRenderer(rend);
    /// glSurfaceView.RenderingMode = RenderMode.Continuous;
    /// glSurfaceView.GLContentResizingMode = GLContentResizeMode.Static;
    /// parent.Add(glSurfaceView);    
    /// 
    /// // if you want to synchronized update, 
    /// glSurfaceView.StartSynchronizedUpdate();
    /// </code>
    public abstract class GLSurfaceViewBaseRenderer : IGLSurfaceViewRenderer
    {
        private RendererUpdated rendererUpdatedDelegate;

        /// <summary>
        /// Initialize - called only once.
        /// </summary>
        /// <param name="width"> renderer width </param>
        /// <param name="height"> renderer height </param>        
        public abstract void Initialize(uint width, uint height);

        /// <summary>
        /// Draw - Resposible to draw current frame
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Terminate - Called before terminating render thread, 
        /// USer can release resource here.
        /// </summary>
        public abstract void Terminate();

        /// <summary>
        ///  End Synchronize update from renderer.
        ///  <remarks> It should be pair with GLSurfaceView.StartSynchronizedUpdate </remarks>
        /// </summary>
        public void EndSynchronizedUpdate()
        {
            rendererUpdatedDelegate();
        }

        internal delegate void RendererUpdated();

        internal void RegisterUpdatedCallback(RendererUpdated callback)
        {
            rendererUpdatedDelegate = new RendererUpdated(callback);
        }
    }
}
