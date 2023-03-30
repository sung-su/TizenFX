/// @file FluxImageViewBindableProperty.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 8.8.0 </version>
/// <SDK_Support> Y </SDK_Support>
/// 
/// Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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
using Tizen.NUI.Binding;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// FluxView is the base class for all views in FLUX Application
    /// </summary>
    /// <code>
    /// FluxImageView = new FluxImageView();
    /// FluxImageView.UnitPosition = new UnitPosition(50, 50);
    /// FluxImageView.UnitSizeWidth = 50;
    /// FluxImageView.UnitSizeHeight = 50;
    /// FluxImageView.BackgroundColor = Color.Red;
    /// </code>
    public partial class FluxImageView : FluxView
    {
        /// <summary>
        /// ResourceUrl Property, Even if not set or null set, it sets empty string ("") internally.
        /// When it is set as null, it gives empty string ("") to be read.
        /// </summary>
        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ResourceUrlProperty = BindableProperty.Create(nameof(FluxImageView.ResourceUrl), typeof(string), typeof(FluxImageView), string.Empty, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var imageView = (FluxImageView)bindable;
            string url = (string)newValue;
            url ??= "";
            if (imageView.IsCreateByXaml && url.Contains("*Resource*"))
            {
                string resource = Tizen.Applications.Application.Current.DirectoryInfo.Resource;
                url = url.Replace("*Resource*", resource);
            }
            imageView._resourceUrl = url;
            imageView.UpdateImage(ImageVisualProperty.URL, new PropertyValue(url));
        },
        defaultValueCreator: (bindable) =>
        {
            var imageView = (FluxImageView)bindable;
            string ret = "";

            PropertyMap imageMap = new PropertyMap();
            Tizen.NUI.Object.GetProperty(imageView.SwigCPtr, FluxImageView.Property.IMAGE).Get(imageMap);
            imageMap.Find(ImageVisualProperty.URL)?.Get(out ret);
            return ret;
        });

        /// <summary>
        /// IMAGE Property.
        /// </summary>
        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(FluxImageView.Image), typeof(PropertyMap), typeof(FluxImageView), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var imageView = (FluxImageView)bindable;
            if (newValue != null)
            {
                PropertyMap map = (PropertyMap)newValue;
                if (imageView.IsCreateByXaml)
                {
                    string url = "", alphaMaskURL = "", auxiliaryImageURL = "";
                    string resource = Tizen.Applications.Application.Current.DirectoryInfo.Resource;
                    PropertyValue urlValue = map.Find(NDalic.ImageVisualUrl);
                    bool ret = false;
                    if (urlValue != null) ret = urlValue.Get(out url);
                    PropertyMap mmap = new PropertyMap();
                    if (ret && url.Contains("*Resource*"))
                    {
                        url = url.Replace("*Resource*", resource);
                        mmap.Insert(NDalic.ImageVisualUrl, new PropertyValue(url));
                    }

                    ret = false;
                    PropertyValue alphaMaskUrlValue = map.Find(NDalic.ImageVisualAlphaMaskUrl);
                    if (alphaMaskUrlValue != null) ret = alphaMaskUrlValue.Get(out alphaMaskURL);
                    if (ret && alphaMaskURL.Contains("*Resource*"))
                    {
                        alphaMaskURL = alphaMaskURL.Replace("*Resource*", resource);
                        mmap.Insert(NDalic.ImageVisualUrl, new PropertyValue(alphaMaskURL));
                    }

                    ret = false;
                    PropertyValue auxiliaryImageURLValue = map.Find(NDalic.ImageVisualAuxiliaryImageUrl);
                    if (auxiliaryImageURLValue != null) ret = auxiliaryImageURLValue.Get(out auxiliaryImageURL);
                    if (ret && auxiliaryImageURL.Contains("*Resource*"))
                    {
                        auxiliaryImageURL = auxiliaryImageURL.Replace("*Resource*", resource);
                        mmap.Insert(NDalic.ImageVisualAuxiliaryImageUrl, new PropertyValue(auxiliaryImageURL));
                    }

                    map.Merge(mmap);
                }
                if (imageView._border == null)
                {
                    Tizen.NUI.Object.SetProperty(imageView.SwigCPtr, FluxImageView.Property.IMAGE, new Tizen.NUI.PropertyValue(map));
                }
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var imageView = (FluxImageView)bindable;
            if (imageView._border == null)
            {
                PropertyMap temp = new PropertyMap();
                Tizen.NUI.Object.GetProperty(imageView.SwigCPtr, FluxImageView.Property.IMAGE).Get(temp);
                return temp;
            }
            else
            {
                return null;
            }
        });

        /// <summary>
        /// PreMultipliedAlpha Property.
        /// </summary>
        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty PreMultipliedAlphaProperty = BindableProperty.Create("PreMultipliedAlpha", typeof(bool), typeof(FluxImageView), false, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var imageView = (FluxImageView)bindable;
            if (newValue != null)
            {
                Tizen.NUI.Object.SetProperty(imageView.SwigCPtr, FluxImageView.Property.PRE_MULTIPLIED_ALPHA, new Tizen.NUI.PropertyValue((bool)newValue));
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var imageView = (FluxImageView)bindable;
            bool temp = false;
            Tizen.NUI.Object.GetProperty(imageView.SwigCPtr, FluxImageView.Property.PRE_MULTIPLIED_ALPHA).Get(out temp);
            return temp;
        });

        /// <summary>
        ///  PixelArea Property.
        /// </summary>
        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty PixelAreaProperty = BindableProperty.Create("PixelArea", typeof(RelativeVector4), typeof(FluxImageView), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var imageView = (FluxImageView)bindable;
            if (newValue != null)
            {
                Tizen.NUI.Object.SetProperty(imageView.SwigCPtr, FluxImageView.Property.PIXEL_AREA, new Tizen.NUI.PropertyValue((RelativeVector4)newValue));
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var imageView = (FluxImageView)bindable;
            Vector4 temp = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            Tizen.NUI.Object.GetProperty(imageView.SwigCPtr, FluxImageView.Property.PIXEL_AREA).Get(temp);
            RelativeVector4 relativeTemp = new RelativeVector4(temp.X, temp.Y, temp.Z, temp.W);
            return relativeTemp;
        });


        /// <summary>
        ///  Border Property.
        /// </summary>
        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty BorderProperty = BindableProperty.Create("Border", typeof(Rectangle), typeof(FluxImageView), null, propertyChanged: (bindable, oldValue, newValue) =>
        {
            if (newValue != null)
            {
                var imageView = (FluxImageView)bindable;
                imageView._border = (Rectangle)newValue;
                imageView.UpdateImage(NpatchImageVisualProperty.Border, new PropertyValue(imageView._border));
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var imageView = (FluxImageView)bindable;
            return imageView._border;
        });

        /// <summary>
        /// BorderOnly Property.
        /// </summary>
        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty BorderOnlyProperty = BindableProperty.Create("BorderOnly", typeof(bool), typeof(FluxImageView), false, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var imageView = (FluxImageView)bindable;
            if (newValue != null)
            {
                imageView.UpdateImage(NpatchImageVisualProperty.BorderOnly, new PropertyValue((bool)newValue));
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var imageView = (FluxImageView)bindable;
            bool ret = false;
            PropertyMap imageMap = new PropertyMap();
            Tizen.NUI.Object.GetProperty(imageView.SwigCPtr, FluxImageView.Property.IMAGE).Get(imageMap);
            imageMap.Find(ImageVisualProperty.BorderOnly)?.Get(out ret);
            return ret;
        });

        /// <summary>
        /// SynchronosLoading Property.
        /// </summary>
        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty SynchronosLoadingProperty = BindableProperty.Create("SynchronosLoading", typeof(bool), typeof(FluxImageView), false, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var imageView = (FluxImageView)bindable;
            if (newValue != null)
            {
                imageView._synchronosLoading = (bool)newValue;
                imageView.UpdateImage(NpatchImageVisualProperty.SynchronousLoading, new PropertyValue((bool)newValue));
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var imageView = (FluxImageView)bindable;
            return imageView._synchronosLoading;
        });

        /// <summary>
        /// OrientationCorrection Property.
        /// </summary>
        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty OrientationCorrectionProperty = BindableProperty.Create("OrientationCorrection", typeof(bool), typeof(FluxImageView), false, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var imageView = (FluxImageView)bindable;
            if (newValue != null)
            {
                imageView.UpdateImage(ImageVisualProperty.OrientationCorrection, new PropertyValue((bool)newValue));
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var imageView = (FluxImageView)bindable;

            bool ret = false;
            PropertyMap imageMap = new PropertyMap();
            Tizen.NUI.Object.GetProperty(imageView.SwigCPtr, FluxImageView.Property.IMAGE).Get(imageMap);
            imageMap?.Find(ImageVisualProperty.OrientationCorrection)?.Get(out ret);

            return ret;
        });
    }
}
