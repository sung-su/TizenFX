/// @file FluxRenderer.cs
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
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// FluxRenderer Class
    /// </summary>
    /// <code>
    /// FluxRendere renderer = new FluxRenderer(geometry, shader);
    /// renderer.UpdateTexture(0u, url);
    /// ....
    /// renderer.ResourceReady += OnResourceReady;
    /// 
    /// // event callback for ResourceReady event.
    /// public void OnResourceReady(object sender, FluxRenderer.ResourceReadyEventArgs resourceReadyEvent)
    /// {
    ///     FluxRenderer renderer = sender as FluxRenderer;
    ///     if (resourceReadyEvent.LoadingStaus == FluxRenderer.LoadingStaus.Failed)
    ///     {
    ///         return;
    ///     }
    ///     view.AddRenderer(renderer);
    /// }
    /// </code>
    public class FluxRenderer : Renderer
    {
        /// <summary>
        /// Struct to store image info for async image loading.
        /// </summary>
        private struct LoadingImageInfo
        {
            public FluxRenderer fluxRenderer;
            public string url;
            public Rectangle border;
            public uint textureId;
            public bool isMipMap;

            public LoadingImageInfo(FluxRenderer fluxRenderer, string url, Rectangle border, uint textureId, bool isMipMap)
            {
                this.fluxRenderer = fluxRenderer;
                this.url = url;
                this.border = border;
                this.textureId = textureId;
                this.isMipMap = isMipMap;
            }
        }

        /// <summary>
        /// FluxRenderer ImageLoading Parameter
        /// </summary>
        public struct ImageProperty
        {
            /// <summary> Width, value type int  </summary>
            public static readonly int DesiredWidth = 0;
            /// <summary>Height, value type int </summary>
            public static readonly int DesiredHeight = 1;
            /// <summary>  FittingModeType, value type int </summary>
            public static readonly int FittingMode = 2;
            /// <summary> SamplingModeType, value type int </summary>
            public static readonly int SamplingMode = 3;
            /// <summary> OrientationCorrection, value type bool </summary>
            public static readonly int OrientationCorrection = 4;
        }


        /// <summary>
        /// Helper class for async image loading.
        /// </summary>
        private class TVAsyncImageLoader
        {
            private readonly TVAsyncImageLoadHelper tvAsyncImageLoadHelper;
            private readonly List<LoadingImageInfo> loadingImageDataContainer;

            public TVAsyncImageLoader()
            {
                loadingImageDataContainer = new List<LoadingImageInfo>();
                tvAsyncImageLoadHelper = new TVAsyncImageLoadHelper();
                tvAsyncImageLoadHelper.AsyncLoadComplete += OnPixelDataLoadFinish;
            }

            private void OnPixelDataLoadFinish(object sender, TVAsyncImageLoadHelper.AsyncLoadEventArgs e)
            {
                // Loading images are added as FIFO 
                // so always use the staring imageinfo from container.
                LoadingImageInfo loadedImageInfo = loadingImageDataContainer[0];
                loadedImageInfo.fluxRenderer.OnPixelDataLoadFinish(e.LoadId, e.PixelData, loadedImageInfo);
                // once texture updated remove the image data from container
                loadingImageDataContainer.RemoveAt(0);
            }

            public uint AsyncLoadImage(LoadingImageInfo loadingImageInfo, Size2D size, FittingModeType fittingMode,
                                        SamplingModeType samplingMode, bool orientationCorrection)
            {
                loadingImageDataContainer.Add(loadingImageInfo);
                uint loadID = tvAsyncImageLoadHelper.Load(loadingImageInfo.url, size, fittingMode, samplingMode, orientationCorrection);
                return loadID;
            }
        }

        private readonly TextureSet textureSet;
        private static readonly TVAsyncImageLoader tvAsyncImageLoader = new TVAsyncImageLoader();

        //dummy texture to fix the flickering  issue.
        private static readonly Texture dummyTexture;
        private static readonly byte[] dummyBuffer;
        private static readonly PixelData dummyPixelData;
        private bool dummyTextureRequired;


        /// <summary>
        /// Static Constructor
        /// </summary>
        static FluxRenderer()
        {
            // Creating 1X1 dummy Texture
            uint bufferSize = 4;
            uint width = 1;
            uint height = 1;
            dummyBuffer = new byte[bufferSize];
            for (int i = 0; i < 4; ++i)
            {
                dummyBuffer[i] = 0;
            }
            dummyPixelData = new PixelData(dummyBuffer, bufferSize, width, height, PixelFormat.RGBA8888, PixelData.ReleaseFunction.DeleteArray);
            dummyTexture = new Texture(TextureType.TEXTURE_2D, dummyPixelData.GetPixelFormat(), dummyPixelData.GetWidth(), dummyPixelData.GetHeight());
        }

        /// <summary>
        /// Event Agrument for AsyncTextureUpdated event.
        /// </summary>
        public class ResourceReadyEventArgs : EventArgs
        {
            /// <summary>
            /// Get url of the updated texture
            /// </summary>
            public string Url { get; }

            /// <summary>
            /// Get finish status of update texture process.
            /// </summary>
            public LoadingStaus LoadingStaus { get; }

            /// <summary>
            /// Event for Async Texture Update Complete.
            /// </summary>
            /// <param name="url">loaded image url</param>
            /// <param name="status"> finish status of texture update process</param>
            public ResourceReadyEventArgs(string url, LoadingStaus status)
            {
                Url = url;
                LoadingStaus = status;
            }

        }

        /// <summary>
        /// Create a Rendender using custom geometry and shaders.
        /// </summary>
        /// <param name="geometry"> Geometry for renderer </param>
        /// <param name="shader"> shader for renderer</param>
        public FluxRenderer(Geometry geometry, Shader shader) : base(geometry, shader)
        {
            BlendMode = 2;
            textureSet = new TextureSet();
            SetTextures(textureSet);
            SynchronosLoading = false;
            dummyTextureRequired = true;
        }

        /// <summary>
        /// Enumeration for LoadingStatus of image.
        /// </summary>
        public enum LoadingStaus
        {
            /// <summary> Loading status ready /// </summary>
            Ready,
            /// <summary> Loading status fail /// </summary>
            Failed
        }

        /// <summary>
        /// Update texture for async loaded images.
        /// </summary>
        /// <param name="id"> identifier for loading image</param>
        /// <param name="pixelData"> handle to pixel data </param>
        /// <param name="loadedImageInfo"> image info (url, texture id, etc)</param>
        private void OnPixelDataLoadFinish(uint id, PixelData pixelData, LoadingImageInfo loadedImageInfo)
        {
            bool result = true;
            if (!pixelData)
            {
                CLog.Error("Pixel data is null for url [%s1]", s1: loadedImageInfo.url);
                result = false;
            }

            Texture texture = null;
            if (result)
            {
                texture = new Texture(TextureType.TEXTURE_2D, pixelData.GetPixelFormat(), pixelData.GetWidth(), pixelData.GetHeight());
                if (!texture.Upload(pixelData))
                {
                    texture.Dispose();
                    texture = null;
                    pixelData.Dispose();
                    pixelData = null;
                    CLog.Error("Failed to upload texture [%s1]", s1: loadedImageInfo.url);
                    result = false;
                }
            }

            if (texture != null)
            {
                UpdateTexture(loadedImageInfo.textureId, texture, loadedImageInfo.border, loadedImageInfo.isMipMap);
            }

            // Emit AsyncUpdateTextureFinish Signal
            ResourceReadyEventArgs e = new ResourceReadyEventArgs(loadedImageInfo.url, (result ? LoadingStaus.Ready : LoadingStaus.Failed));
            ResourceReady?.Invoke(this, e);

            CLog.Debug("OnPixelDataLoadFinish called for [%s1]", s1: loadedImageInfo.url);
        }

        /// <summary>
        /// Signal for async texture update.
        /// Only emitted when UpdateTexture is called with isSyncloading set to false.
        /// </summary>
        public event EventHandler<ResourceReadyEventArgs> ResourceReady;


        /// <summary>
        ///  update uniform for npatch image
        ///  updates the below uniforms only
        ///  uFixed[0], uFixed[1], uFixed[0], uFixed[2], uStretchTotal.
        /// </summary>
        /// <param name="texture"> texture of npatch image </param>
        /// <param name="border"> border for npatch image </param>
        private void UpdateUniformForNPATCH(Texture texture, Rectangle border)
        {
            uint croppedWidth = texture.GetWidth();
            uint croppedHeight = texture.GetHeight();

            List<Uint16Pair> stretchPixelsX = new List<Uint16Pair>();
            List<Uint16Pair> stretchPixelsY = new List<Uint16Pair>();

            stretchPixelsX.Add(new Uint16Pair(Convert.ToUInt16(border.X), Convert.ToUInt16(croppedWidth - border.Y)));
            stretchPixelsY.Add(new Uint16Pair(Convert.ToUInt16(border.Height), Convert.ToUInt16(croppedHeight - border.Width)));

            Uint16Pair stretchX = stretchPixelsX[0];
            Uint16Pair stretchY = stretchPixelsY[0];


            Vector2 uFixed_0 = new Vector2(0.0f, 0.0f);
            Vector2 uFixed_1 = new Vector2(stretchX.GetX(), stretchY.GetX());
            ushort stretchWidth = Convert.ToUInt16(stretchX.GetY() - stretchX.GetX());
            ushort stretchHeight = Convert.ToUInt16(stretchY.GetY() - stretchY.GetX());


            Vector2 uFixed_2 = new Vector2(croppedWidth - stretchWidth, croppedHeight - stretchHeight);
            Vector2 uStretchTotal = new Vector2(stretchWidth, stretchHeight);

            RegisterProperty("uFixed[0]", new PropertyValue(uFixed_0));
            RegisterProperty("uFixed[1]", new PropertyValue(uFixed_1));
            RegisterProperty("uFixed[2]", new PropertyValue(uFixed_2));
            RegisterProperty("uStretchTotal", new PropertyValue(uStretchTotal));
        }

        /// <summary>
        /// Create texture using url.
        /// </summary>
        /// <param name="url"> path of the image </param>
        /// <param name="size"> size The width and height to fit the loaded image to, 0.0 means whole image </param>
        /// <param name="fittingMode"> fittingMode The method used to fit the shape of the image before loading to the shape defined by the size parameter. </param>
        /// <param name="samplingMode">samplingMode The filtering method used when sampling pixels from the input image while fitting it to desired size </param>
        /// <param name="orientationCorrection">handle to the loaded PixelBuffer object or an empty handle in case loading failed</param>
        /// <returns></returns>
        private Texture CreateTexture(string url, Size2D size, FittingModeType fittingMode, SamplingModeType samplingMode, bool orientationCorrection)
        {
            Texture texture;

            PixelBuffer pixelbuffer = ImageLoader.LoadImageFromFile(url, size, fittingMode, samplingMode, orientationCorrection);
            if (!pixelbuffer)
            {
                CLog.Error("Failed to open or decode image [%s1]", s1: url);
                return null;
            }
            PixelData pixelData = PixelBuffer.Convert(pixelbuffer);

            CLog.Info("pixelData : %d1 x %d2, %s1"
                , d1: pixelData.GetWidth()
                , d2: pixelData.GetHeight()
                , s1: CLog.EnumToString(pixelData.GetPixelFormat())
                );

            texture = new Texture(TextureType.TEXTURE_2D, pixelData.GetPixelFormat(), pixelData.GetWidth(), pixelData.GetHeight());
            if (!texture.Upload(pixelData))
            {
                CLog.Error("Failed to upload texture [%s1]", s1: url);
                texture.Dispose();
                texture = null;
                pixelData.Dispose();
                pixelData = null;
                return null;
            }
            return texture;
        }

        /// <summary>
        /// SynchronosLoading loading
        /// Default value is false
        /// </summary>
        public bool SynchronosLoading { get; set; }

        /// <summary>
        /// update the texture for renderer.
        /// </summary>
        /// <param name="textureId"> index of texture in textureset </param>
        /// <param name="url"> url to create texture </param>
        /// <param name="imageMap">ProperyMap for Image Loading Parameters</param>
        /// <param name="border"> border for npatch image </param>
        /// <param name="isMipmap"> flag to generate mipmap or not </param>
        /// <code>
        /// Geometry geometry = GeometryFactory.Instance.GetGeometry(GeometryFactory.GeometryType.QUAD);
        /// Shader shader = ShaderFactory.Instance.GetShader(ShaderFactory.ShaderType.IMAGE);
        /// PropertyMap propertyMap = new PropertyMap();
        /// propertyMap.Add(FluxRenderer.ImageProperty.DesiredWidth, new PropertyValue(960));
        /// propertyMap.Add(FluxRenderer.ImageProperty.DesiredHeight, new PropertyValue(452));
        /// propertyMap.Add(FluxRenderer.ImageProperty.FittingMode, new PropertyValue((int) FittingModeType.FitWidth));
        /// propertyMap.Add(FluxRenderer.ImageProperty.SamplingMode, new PropertyValue((int) SamplingModeType.BoxThenLinear));
        /// propertyMap.Add(FluxRenderer.ImageProperty.OrientationCorrection, new PropertyValue(false));
        /// FluxRenderer renderer2 = new FluxRenderer(geometry, shader);
        /// renderer2.UpdateTexture(0u, url, propertyMap);
        /// </code>

        public void UpdateTexture(uint textureId, string url, PropertyMap imageMap = null, Rectangle border = null, bool isMipmap = false)
        {
            if (string.IsNullOrEmpty(url))
            {
                CLog.Error("URL is not valid: %s1", s1: url);
                return;
            }

            Size2D size = new Size2D(0, 0);
            FittingModeType fittingMode = FittingModeType.ShrinkToFit;
            SamplingModeType samplingMode = SamplingModeType.BoxThenLinear;
            bool orientationCorrection = true;

            // Parsing the imagemap for image loading parameter
            if (imageMap != null && imageMap.Empty() == false)
            {
                PropertyValue valueWidth = imageMap.Find(ImageProperty.DesiredWidth);
                PropertyValue valueHeight = imageMap.Find(ImageProperty.DesiredHeight);
                if (valueWidth != null && valueHeight != null)
                {
                    valueWidth.Get(out int width);
                    valueHeight.Get(out int height);
                    size = new Size2D(width, height);
                }

                PropertyValue valueFittingMode = imageMap.Find(ImageProperty.FittingMode);
                if (valueFittingMode != null)
                {
                    valueFittingMode.Get(out int fittingModeTye);
                    fittingMode = (FittingModeType)fittingModeTye;
                }

                PropertyValue valueSamplingMode = imageMap.Find(ImageProperty.SamplingMode);
                if (valueSamplingMode != null)
                {
                    valueSamplingMode.Get(out int samplingModeType);
                    samplingMode = (SamplingModeType)samplingModeType;
                }

                PropertyValue valueOrientation = imageMap.Find(ImageProperty.OrientationCorrection);
                if (valueOrientation != null)
                {
                    valueOrientation.Get(out orientationCorrection);
                }
            }

            if (SynchronosLoading)
            {
                Texture texture = CreateTexture(url, size, fittingMode, samplingMode, orientationCorrection);

                if (texture != null)
                {
                    UpdateTexture(textureId, texture, border, isMipmap);
                }

                // SyncLoading have no flickering issue 
                // so simply set false
                dummyTextureRequired = false;
            }
            else
            {
                // Need to add dummy texture so that view doesn't show garbage texture till image is loaded.
                // Only need to do this for first time.
                if (dummyTextureRequired)
                {
                    dummyTextureRequired = false;
                    UpdateTexture(textureId, dummyTexture);
                }
                // async image loading
                // OnPixelDataLoadFinish is called after load complete
                LoadingImageInfo loadingImageInfo = new LoadingImageInfo(this, url, border, textureId, isMipmap);
                tvAsyncImageLoader.AsyncLoadImage(loadingImageInfo, size, fittingMode, samplingMode, orientationCorrection);
            }
        }

        /// <summary>
        /// update the texture for renderer.
        /// </summary>
        /// <param name="textureId"> index of texture in textureset </param>
        /// <param name="texture"> texture to be updated </param>
        /// <param name="border"> border for npatch image </param>
        /// <param name="isMipmap"> flag to generate mipmap or not </param>
        public void UpdateTexture(uint textureId, Texture texture, Rectangle border = null, bool isMipmap = false)
        {
            if (!texture)
            {
                CLog.Error("Texture is not valid");
                return;
            }
            if (isMipmap)
            {
                texture.GenerateMipmaps();

                Sampler sampler = new Sampler();
                sampler.SetFilterMode(FilterModeType.LINEAR_MIPMAP_LINEAR, FilterModeType.LINEAR_MIPMAP_LINEAR);
                textureSet.SetSampler(textureId, sampler);
            }

            textureSet.SetTexture(textureId, texture);

            if (border != null && border.IsEmpty() == false)
            {
                UpdateUniformForNPATCH(texture, border);
            }
        }

        /// <summary>
        /// Set Sampler for texture
        /// </summary>
        /// <param name="textureId"> </param>
        /// <param name="sampler"></param>
        public void SetSampler(uint textureId, Sampler sampler)
        {
            textureSet?.SetSampler(textureId, sampler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniformName"> string name of uniform in shader</param>
        /// <param name="value"> value to be updated </param>
        public void UpdateUniform(string uniformName, object value)
        {
            RegisterProperty(uniformName, new PropertyValue((dynamic)value));
        }
    }

}