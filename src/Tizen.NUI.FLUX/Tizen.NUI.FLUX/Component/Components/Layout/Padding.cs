/**
 *Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *For conditions of distribution and use, see the accompanying LICENSE.Samsung file.
 */
/// @file Padding.cs
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
    /// The padding of layout.
    /// </summary>
    [Tizen.NUI.Binding.TypeConverter(typeof(PaddingTypeConverter))]
    public class Padding
    {
        /// <summary>
        /// 
        /// </summary>
        public Padding()
        {
        }

        internal Padding(int leftPadding, int rightPadding, int topPadding, int bottomPadding)
        {
            this.leftPadding = leftPadding;
            this.rightPadding = rightPadding;
            this.topPadding = topPadding;
            this.bottomPadding = bottomPadding;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Left
        {
            get => leftPadding;
            set
            {
                if (value < 0)
                {
                    leftPadding = 0;
                }
                else
                {
                    leftPadding = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Right
        {
            get => rightPadding;
            set
            {
                if (value < 0)
                {
                    rightPadding = 0;
                }
                else
                {
                    rightPadding = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Top
        {
            get => topPadding;
            set
            {
                if (value < 0)
                {
                    topPadding = 0;
                }
                else
                {
                    topPadding = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Bottom
        {
            get => bottomPadding;
            set
            {
                if (value < 0)
                {
                    bottomPadding = 0;
                }
                else
                {
                    bottomPadding = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int All
        {
            get => allPadding;
            set
            {
                if (value < 0)
                {
                    allPadding = 0;
                }
                else
                {
                    allPadding = value;
                }
            }
        }

        private int allPadding = 0;
        private int bottomPadding = 0;
        private int topPadding = 0;
        private int rightPadding = 0;
        private int leftPadding = 0;
    }
}