/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */

/// @file FluxBackground.cs
/// <published> N </published>
/// <privlevel> Non-privilege </privlevel>
/// <privilege> None </privilege> 
/// <privacy> N </privacy>
/// <product> TV </product>
/// <version> 6.6.0 </version>
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
/// 
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// FluxBackground is the Class that describe the default background of FluxApp. 
    /// And it will be included at RootLayout as default. 
    /// </summary>
    /// <code>
    /// FluxBackground fluxBG = new FluxBackground((int)SizeWidth, (int)SizeHeight);
    /// </code>
    public partial class FluxBackground : View
    {
        #region public Method
        /// <summary>
        ///  Constructor of Flux App Background, it has primitive defined by UX principle. ( TileImage , Color )
        /// </summary>
        /// <param name="width"> if width or height was 0, it will be created with window's size</param>
        /// <param name="height"> if width or height was 0, it will be created with window's size</param>
        public FluxBackground(int width = 0, int height = 0) : base()
        {
            Initialize(width, height);
        }

        /// <summary>
        ///  Constructor of Flux App Background, it has primitive defined by UX principle. ( Image , Color )
        /// </summary>
        /// <param name="resourceURL"> if url was null, it means "use default iamge" defined by UX</param>
        /// <version> 8.8.0 </version>
        public FluxBackground(string resourceURL) : base()
        {
            imageURL = resourceURL;
            Initialize(0, 0);
        }

        /// <summary>
        ///  ThemeBackgroundColorChip value ( default value is "CC_BG5100" )
        /// </summary>
        public string ThemeBackgroundColorChip
        {
            get => (string)GetValue(ThemeBackgroundColorChipProperty);

            set => SetValue(ThemeBackgroundColorChipProperty, value);
        }

        ///  Change top/bottom image of AppBackground
        /// </summary>
        /// <param name="ResourceURL"> URL of Background image if it is null, it will be default image provided by UIFW </param>
        public void ChangeImage(string ResourceURL = null)
        {
            if (ResourceURL != null)
            {
                imageURL = ResourceURL;
            }
            else
            {
                imageURL = ResourceUtility.GetCommonResourcePath(defaultImageFileName);
            }

            ProcessDrawing(SizeWidth, SizeHeight);
        }
        #endregion public Method

        #region private Property
        private string privateThemeBackgroundColorChip
        {
            get => themeBackgroundColorChip;
            set
            {
                if (value == null)
                {
                    themeBackgroundColorChip = "CC_BG5100";
                }
                else
                {
                    themeBackgroundColorChip = value;
                }

                ThemeHelper.Instance.ChangeColorchip(this, "BackgroundColor", themeBackgroundColorChip);
            }
        }
        #endregion private Property
        #region protected Method
        /// <summary>
        /// Cleaning up managed and unmanaged resources
        /// </summary>
        /// <param name="type">
        /// Type of Dispose.
        /// Explicit - Called by user explicitly.
        /// Implicit - Called by gc implicitly.
        /// </param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.

                ThemeHelper.Instance.UnMapColorChip(this, "BackgroundColor");

                //this.SizeChanged -= FluxBackgroundSizeChanged;
                DestroyUtility.DestroyView(ref image);
            }

            base.Dispose(type);
        }
        #endregion protected Method

        #region internal Method
        internal void ProcessDrawing(float width, float height)
        {
            if (width == 0 || height == 0)
            {
                Size2D windowSize = Window.Instance.Size;
                if (windowSize != null)
                {
                    SizeWidth = windowSize.Width;
                    SizeHeight = windowSize.Height;
                }
            }
            else
            {
                SizeWidth = width;
                SizeHeight = height;
            }

            if (imageURL == null && AccessibilityManager.Instance.HighContrast == false)
            {
                imageURL = ResourceUtility.GetCommonResourcePath(defaultImageFileName, ResourceUtility.ResourceSizes.Middle);
            }

            DrawImage();
            ProcessHighcontrast(AccessibilityManager.Instance.HighContrast);
        }

        internal virtual void DrawImage()
        {
            if (UIConfig.IsFullSmart == false)
            {
                return;
            }

            DrawTile();
        }

        internal void DrawTile()
        {
            image.ResourceUrl = imageURL;
            Size2D imageSize = image.NaturalSize2D;

            if (imageSize == null || imageSize.Width == 0 || imageSize.Height == 0)
            {
                imageURL = null;
                return;
            }

            image.SizeWidth = SizeWidth;
            image.SizeHeight = SizeHeight;

            image.ParentOrigin = Tizen.NUI.ParentOrigin.TopCenter;
            image.PivotPoint = Tizen.NUI.PivotPoint.TopCenter;
            image.PositionUsesPivotPoint = true;

            image.PixelArea = new RelativeVector4(0.0f, 0.0f, SizeWidth / imageSize.Width, 1.0f);
            image.WrapModeU = WrapModeType.Repeat;

        }
        #endregion internal Method

        #region private Method
        private void Initialize(int width, int height)
        {
            ThemeHelper.Instance.MapColorChip(this, "BackgroundColor", "CC_BG5100");

            if (image == null)
            {
                image = new ImageView
                {
                    SynchronousLoading = true
                };
                Add(image);
            }

            ProcessDrawing(width, height);
        }

        private void ProcessHighcontrast(bool isHighContrast)
        {
            if (image == null)
            {
                return;
            }

            if (isHighContrast == true)
            {
                image.Hide();
            }
            else
            {
                image.Show();
            }
        }

        #endregion private Method
        internal ImageView image = null;

        internal string imageURL = null;

        private readonly string defaultImageFileName = "i_img_2020bg_tile.webp";

        private string themeBackgroundColorChip = "CC_BG5100";

    }
}
