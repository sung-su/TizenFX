/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file Margin.cs
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
namespace Tizen.NUI.FLUX.Component
{
    /// <summary>
    /// The Margin of RootLayout.
    /// </summary>
    /// <code>
    /// RootLayout rootLayout = new RootLayout(Window.Instance.GetDefaultLayer()); 
    /// rootLayout.LayoutParam.Margin.LeftMargin = 84;
    /// rootLayout.LayoutParam.Margin.TopMargin = 8;
    /// </code>
    public class Margin
    {
        /// <summary>
        /// 
        /// </summary>
        public Margin()
        {
        }

        /// <summary>
        /// This LeftMargin is base on "Pixel". 
        /// Usually that LeftMargin value is auto update by GridSystem.
        /// </summary>
        public int LeftMargin
        {
            get
            {
                return leftMargin;
            }
            set
            {
                if (value < 0)
                {
                    leftMargin = 0;
                }
                else
                {
                    leftMargin = value;
                }
            }
        }

        /// <summary>
        /// This RightMargin is base on "Pixel". 
        /// Usually that RightMargin value is auto update by GridSystem.
        /// </summary>
        public int RightMargin
        {
            get
            {
                return rightMargin;
            }
            set
            {
                if (value < 0)
                {
                    rightMargin = 0;
                }
                else
                {
                    rightMargin = value;
                }
            }
        }

        /// <summary>
        /// This TopMargin is base on "Unit". 
        /// TopMargin value is not auto update by GridSystem. 
        /// If want apply to Rootlayout, User Must have to set.
        /// </summary>
        public int TopMargin
        {
            get
            {
                return topMargin;
            }
            set
            {
                if (value < 0)
                {
                    topMargin = 0;
                }
                else
                {
                    topMargin = value;
                }
            }
        }

        /// <summary>
        /// This BottomMargin is base on "Unit". 
        /// BottomMargin value is not auto update by GridSystem. 
        /// If want apply to BottomMargin, User Must have to set.
        /// </summary>
        public int BottomMargin
        {
            get
            {
                return bottomMargin;
            }
            set
            {
                if (value < 0)
                {
                    bottomMargin = 0;
                }
                else
                {
                    bottomMargin = value;
                }
            }
        }

        private int bottomMargin = 0;
        private int topMargin = 0;
        private int rightMargin = 0;
        private int leftMargin = 0;
    }
}