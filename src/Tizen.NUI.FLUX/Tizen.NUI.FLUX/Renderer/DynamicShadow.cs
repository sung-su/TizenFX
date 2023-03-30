/// @file DynamicShadow.cs
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
    /// class for DynamicShadow
    /// </summary>
    public class DynamicShadow
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DynamicShadow()
        {
            SecurityUtil.CheckPlatformPrivileges();
        }

        /// <summary>
        /// Shadow Offset
        /// </summary>
        private Vector2 offset = Vector2.Zero;

        /// <summary>
        /// Set shadow offset
        /// </summary>
        public Vector2 Offset
        {
            set
            {
                offset = value;
                OnPropertyChanged("Offset");
            }
            get
            {
                return offset;
            }
        }

        /// <summary>
        /// Shadow Color
        /// </summary>
        private Color color = Color.Transparent;

        /// <summary>
        /// Set/Get shadow Color
        /// </summary>
        public Color Color
        {
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
            get
            {
                return color;
            }
        }

        /// <summary>
        /// Shadow BlurSize
        /// </summary>
        private float blurSize = 0.0f;

        /// <summary>
        /// Set/Get BlurSize.
        /// Value must be between 1u and 10u
        /// </summary>
        public float BlurSize
        {
            set
            {
                blurSize = Math.Min(Math.Max(value, 1), 10);
                OnPropertyChanged("BlurSize");
            }
            get
            {
                return blurSize;
            }
        }

        /// <summary>
        /// Shadow BlurSigma
        /// </summary>
        private float blurSigma = 0.0f;

        /// <summary>
        /// Set/Get BlurSigma
        /// The constant controlling the Gaussian function, must be > 0.0. Controls the width of the bell curve, i.e. the look of the blur and also indirectly
        /// the amount of blurriness Smaller numbers for a tighter curve.Useful values in the range[0.5..3.0]
        /// </summary>
        public float BlurSigma
        {
            set
            {
                blurSigma = value;
                OnPropertyChanged("BlurSigma");
            }
            get
            {
                return blurSigma;
            }
        }

        /// <summary>
        /// ShadowArea
        /// It should be equal to size of actor(actual actor whose shadow is shown) + offset of that actor from MultiShadowView actor
        /// </summary>
        private Vector2 size = Vector2.Zero;

        /// <summary>
        /// Set/Get ShadowArea
        /// It should be equal to size of actor(actual actor whose shadow is shown) + offset of that actor from MultiShadowView actor
        /// </summary>
        public Vector2 Size
        {
            set
            {
                size = value;
                OnPropertyChanged("Size");
            }
            get
            {
                return size;
            }
        }

        internal Action<DynamicShadow, string> PropertyChanged;

        /// <summary>
        /// OnPropertyChanged method to raise event
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, propertyName);
        }
    }
}
