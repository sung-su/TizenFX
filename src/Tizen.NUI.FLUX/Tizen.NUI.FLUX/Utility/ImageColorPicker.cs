/// @file ImageColorPicker.cs
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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// This is delegate of ImageColorPickAsyncDelegate, It provides path of image, color value(R,G,B,A)
    /// </summary>
    /// <param name="imagePath">Path of Image file for image color pick</param>
    /// <param name="color">Return Average color of image </param>
    public delegate void ImageColorPickAsync(string imagePath, Color color);

    /// <summary>
    /// ImageColorPicker class which enables user to obtain the arerage colour in image.
    /// </summary>
    /// <code> 
    /// //Sync mode
    /// Color color = ImageColorPicker.Instance.GetImageColorPick(imagePath);
    /// //Async mode
    /// private ImageColorPickAsyncDelegate imageColorPickDel;
    /// imageColorPickDel = new ImageColorPickAsyncDelegate(Imagefunc);
    /// ImageColorPicker.Instance.GetImageColorPickAsync(imagepath, imageColorPickDel);
    /// private void Imagefunc(string imagePath, Color color)
    /// {
    ///     string userimagepath = imagePath;
    ///     Color usercolor = color;
    /// }
    /// </code>
    public class ImageColorPicker
    {
        private static readonly ImageColorPicker instance = new ImageColorPicker();
        private Color color;
        private ImageColorPickAsync imageColorPickAsyncDel;
        private readonly Interop.ImageColorPick.NativeColorPickValueAsyncCallback nativeColorPickCallback;
        private readonly Dictionary<string, WeakReference> imageColoPickDictionary;

        /// <summary>
        /// Constructor to instantiate the ImageColorPicker class.
        /// </summary>
        private ImageColorPicker()
        {
            SecurityUtil.CheckPlatformPrivileges();
            color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            imageColoPickDictionary = new Dictionary<string, WeakReference>();
            nativeColorPickCallback = new Interop.ImageColorPick.NativeColorPickValueAsyncCallback(ImageColorPickAsyncCallback);
        }

        /// <summary>
        /// ImageColorPicker instance (read-only) <br></br>
        /// Gets the current ImageColorPicker object.
        /// </summary>
        public static ImageColorPicker Instance => instance;

        /// <summary>
        /// Get average color of image in Sync mode.
        /// </summary>
        /// <param name="imagePath">Path of Image file for image color pick</param>
        /// <exception cref="ArgumentNullException">
        /// ImagePath is Null, You need to add imagePath.
        /// </exception>
        /// <returns>[Color] Average color of image </returns>
        public Color GetImageColorPick(string imagePath)
        {
            if (imagePath == null)
            {
                throw new ArgumentNullException("ImagePath is Null, You need to add imagePath");
            }
            Vector3 rgbColor = new Vector3(Interop.ImageColorPick.GetColorPickValueSync(imagePath), true);
            color = new Vector4(rgbColor.X, rgbColor.Y, rgbColor.Z, 1.0f);
            return color;
        }

        /// <summary>
        /// Get average color of image in Sync mode. If you want to pick specific area, you can set rectangle area. 
        /// </summary>
        /// <param name="imagePath">Path of Image file for image color pick</param>
        /// <param name="rect"> Rectangle object you want to pick color. This area is related with Original size of Image </param>
        /// <exception cref="ArgumentNullException">
        /// ImagePath is Null, You need to add imagePath.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Rectangle is Null.
        /// </exception>
        /// <returns>[Color] Average color of image </returns>
        public Color GetImageColorPick(string imagePath, Rectangle rect)
        {
            if (imagePath == null)
            {
                throw new ArgumentNullException("ImagePath is Null, You need to add imagePath");
            }
            if (rect == null)
            {
                throw new ArgumentNullException("Rectangle is Null");
            }
            Vector3 rgbColor;
            rgbColor = new Vector3(Interop.ImageColorPick.GetColorPickValueSync(imagePath, Rectangle.getCPtr(rect).Handle), true);
            color = new Vector4(rgbColor.X, rgbColor.Y, rgbColor.Z, 1.0f);
            return color;
        }

        /// <summary>
        /// Get average color of image in Async mode. 
        /// </summary>
        /// <param name="imagePath">Path of Imagefile for image color pick</param>
        /// <param name="imageColorPickAsyncDel"> This hanlder to be called when get image color in async mode </param>
        /// <exception cref="ArgumentNullException">
        /// ImagePath is Null, You need to add imagePath.
        /// </exception>
        public void GetImageColorPickAsync(string imagePath, ImageColorPickAsync imageColorPickAsyncDel)
        {
            if (imagePath == null)
            {
                throw new ArgumentNullException("ImagePath is Null, You need to add imagePath");
            }
            if (imageColoPickDictionary.ContainsKey(imagePath) == false)
            {
                imageColoPickDictionary.Add(imagePath, new WeakReference(imageColorPickAsyncDel));
                Interop.ImageColorPick.GetColorPickValueAsync(imagePath, nativeColorPickCallback);
            }
        }

        /// <summary>
        /// Get average color of image in Async mode. If you want to pick specific area, you can set rectangle area. 
        /// </summary>
        /// <param name="imagePath">Path of Imagefile for image color pick</param>
        /// <param name="imageColorPickAsyncDel"> This hanlder to be called when get image color in async mode </param>
        /// <param name="rect"> Rectangle object you want to pick color. This area is related with Original size of Image </param>
        /// <exception cref="ArgumentNullException">
        /// ImagePath is Null, You need to add imagePath.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Rectangle is Null.
        /// </exception>
        public void GetImageColorPickAsync(string imagePath, ImageColorPickAsync imageColorPickAsyncDel, Rectangle rect)
        {
            if (imagePath == null)
            {
                throw new ArgumentNullException("ImagePath is Null, You need to add imagePath");
            }
            if (rect == null)
            {
                throw new ArgumentNullException("Rectangle is Null");
            }
            if (imageColoPickDictionary.ContainsKey(imagePath) == false)
            {
                imageColoPickDictionary.Add(imagePath, new WeakReference(imageColorPickAsyncDel));
                Interop.ImageColorPick.GetColorPickValueAsync(imagePath, nativeColorPickCallback, Rectangle.getCPtr(rect).Handle);
            }
        }

        private void ImageColorPickAsyncCallback(IntPtr imagePath, IntPtr colorValue)
        {
            string userImagePath = Marshal.PtrToStringAnsi(imagePath);
            if (string.IsNullOrEmpty(userImagePath))
            {
                throw new ArgumentNullException("userImagePath is Null or Empty, You need to add imagePath");
            }
            Vector3 tempColor = new Vector3(colorValue, true);
            Color color = new Color(tempColor.R, tempColor.G, tempColor.B, 1.0f);

            CLog.Debug("Image Path: %s1, color R[%f1] G[%f2] B[%f3] A[%f4]"
                , s1: userImagePath
                , f1: color.R
                , f2: color.G
                , f3: color.B
                , f4: color.A
                );

            if (imageColoPickDictionary?.ContainsKey(userImagePath) == true)
            {
                imageColorPickAsyncDel = imageColoPickDictionary[userImagePath].Target as ImageColorPickAsync;
                imageColorPickAsyncDel?.Invoke(userImagePath, color);
                imageColoPickDictionary.Remove(userImagePath);
            }
        }
    }
}
