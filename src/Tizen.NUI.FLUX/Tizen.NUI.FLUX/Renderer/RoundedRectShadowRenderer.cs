/// @file RoundedRectShadowRenderer.cs
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
    /// Draw RoundedRect shadow
    /// </summary>
    internal class RoundedRectShadowRenderer : PrimitiveShadowRenderer
    {
      /// <summary>
      /// RoundedRectSides
      /// </summary>
      private enum RoundedRectSide
      {
        /// <summary>
        /// All Four Corners
        /// </summary>
        All = 0,
        /// <summary>
        /// Top Two Corners
        /// </summary>
        Top,

        /// <summary>
        /// Bottom Two Corners
        /// </summary>
        Bottom,

        /// <summary>
        /// Left Two Corners
        /// </summary>
        Left,

        /// <summary>
        /// Right Two Corners
        /// </summary>
        Right
      }

      /// <summary>
      /// Constructor
      /// </summary>
      internal RoundedRectShadowRenderer() : base(GeometryFactory.Instance.GetGeometry(GeometryFactory.GeometryType.QUAD), ShaderFactory.Instance.GetShader(ShaderFactory.ShaderType.ROUNDED_RECT_COLOR))
      {
          UpdateRoundedRectUniforms();
      }

      /// <summary>
      /// Update RoundedRects Uniforms
      /// </summary>
      private void UpdateRoundedRectUniforms()
      {
          base.UpdateUniforms();
          // Update uniforms specific to RoundedRect primitive if any

          //For default antialiasing.
          float edgeBlur = (blurSize>1.0f)?(blurSize+3.0f):2.0f;
          RegisterProperty("uPrimitiveOffset_blurSize", new PropertyValue(new Vector4(offset.X, Offset.Y, blurSize, edgeBlur)));

          RegisterProperty("uRadius", new PropertyValue(Convert.ToSingle(radius)));
          RegisterProperty("uRectBound", new PropertyValue(roundedRectSideVal));
      }

      /// <summary>
      /// Update Uniforms
      /// </summary>
      protected override void UpdateUniforms()
      {
          UpdateRoundedRectUniforms();
      }

      private uint radius = 0;
      /// <summary>
      /// Set Radius for rounded corners
      /// </summary>
      public uint Radius
      {
          set
          {
              radius = value;
              UpdateRoundedRectUniforms();
          }
          get
          {
              return radius;
          }
      }

      private Vector4 roundedRectSideVal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
    }
}
