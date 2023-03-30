/// @file ImagePaint.cs
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
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// ImagePaint is a class for painting texture in VG shapes
    /// </summary>
    /// <code>
    /// ImagePaint ip = new ImagePaint(url);
    /// ip.FittingMode = FittingModeType:ScaleToFill;
    /// Fill fill = new Fill(ip);   
    /// </code>
    public class ImagePaint : SolidColor
    {
        /// <summary>
        /// This specifies fitting mode types. Fitting options, used when resizing images
        /// to fit desired dimensions. A fitting mode controls the region of a loaded image
        /// to be mapped to the desired image rectangle.
        /// </summary>
        public enum FittingModeType
        {
            /// <summary>
            /// Limit loaded image resolution to device resolution
            /// </summary>
            ShrinkToFit = Tizen.NUI.FittingModeType.ShrinkToFit,
            /// <summary>
            /// Limit loaded image resolution to screen tile
            /// </summary>
            ScaleToFill = Tizen.NUI.FittingModeType.ScaleToFill,
            /// <summary>
            /// Limit loaded image resolution to column width
            /// </summary>
            FitWidth = Tizen.NUI.FittingModeType.FitWidth,
            /// <summary>
            /// Limit loaded image resolution to row height
            /// </summary>
            FitHeight = Tizen.NUI.FittingModeType.FitHeight,
            /// <summary>
            /// Load complete image in the view, Aspect ratio will not be maintained
            /// </summary>
            Fill = FitHeight + 1
        }

        /// <summary>
        /// Create a new texture paint object for applying on VG shape
        /// </summary>
        /// <param name="url"> Texture url to be shown in shape </param>
        /// <exception cref="ArgumentException">Thrown when Resource url does not exist. 
        /// but exception will not be thrown and default image will be shown since Tizen 9.9.0 </exception>
        public ImagePaint(string url) : base(Color.White)
        {
            URL = url;
            FittingMode = FittingModeType.ScaleToFill;
            TextureRect = new Vector4(0.0f, 0.0f, 1.0f, 1.0f);
            BackgroundColor = Color.White;
        }

        /// <summary>
        /// Texture fitting mode
        /// default mode is FittingModeType.ScaleToFill
        /// </summary>
        public FittingModeType FittingMode
        {
            set; get;
        }

        /// <summary>
        /// Texture URL
        /// </summary>
        public string URL
        {
            set; get;
        }

        /// <summary>
        /// Background Color of the Paint
        /// </summary>
        internal Color BackgroundColor
        {
            set; get;
        }

        /// <summary>
        /// Set custom texture rectangle in UV space
        /// value between 0.0f - 1.0f
        /// default value is (0.0f, 0.0f, 1.0f, 1.0f)
        /// </summary>
        public Vector4 TextureRect
        {
            set; get;
        }

        /// <summary>
        ///
        /// </summary>
        internal override void Draw(VectorView view)
        {
            CLog.Debug("Texture URL: [%s1]", s1: URL);
            Interop.VectorView.SetPaintColor(View.getCPtr(view), Color.getCPtr(Color));
            Interop.VectorView.SetBackgroundColor(View.getCPtr(view), Color.getCPtr(BackgroundColor));
            Interop.VectorView.SetResourceUrl(View.getCPtr(view), URL, (int)(FittingMode), TextureRect.X, TextureRect.Y, TextureRect.Z, TextureRect.W);

            base.Draw(view);
        }

        internal override RenderBackend GetRenderBackend()
        {
            return RenderBackend.DirectRenderer;
        }
    }
}
