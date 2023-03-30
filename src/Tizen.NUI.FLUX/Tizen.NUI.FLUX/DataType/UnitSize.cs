/// @file UnitSize.cs
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


namespace Tizen.NUI.FLUX
{    
    /// <summary>
    /// Unit of UI class
    /// </summary>
    /// <code>
    /// FluxView view = new FluxView();
    /// view.UnitSize = new UnitSize(100, 100);
    /// view.UnitPosition = new UnitPosition(10, 10);
    /// </code>
    [Tizen.NUI.Binding.TypeConverter(typeof(UnitSizeTypeConverter))]
    public class UnitSize : IObservable<UnitSize>
    {
        /// <summary>
        /// Constructor to instantiate the UnitSize class.
        /// </summary>
        /// <param name="width">The width of size in flux</param>
        /// <param name="height">The height of size in flux</param>
        public UnitSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
                
        /// <summary>
        /// The width of size in flux
        /// </summary>
        public int Width {
            get
            {
                return width;
            }
            set
            {
                if (width != value)
                {
                    width = value;
                    propertyChanged?.Invoke(this);
                }
            }

        }

        /// <summary>
        /// The height of size in flux
        /// </summary>
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height != value)
                {
                    height = value;
                    propertyChanged?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// operator for add
        /// </summary>
        /// <param name="arg1">UnitSize to add.</param>
        /// <param name="arg2">UnitSize to add.</param>
        /// <returns></returns>
        public static UnitSize operator +(UnitSize arg1, UnitSize arg2)
        {
            return new UnitSize(arg1.Width + arg2.Width, arg1.Height + arg2.Height);
        }

        /// <summary>
        /// operator for subtract
        /// </summary>
        /// <param name="arg1">UnitSize to subtract.</param>
        /// <param name="arg2">UnitSize to subtract.</param>
        /// <returns></returns>
        public static UnitSize operator -(UnitSize arg1, UnitSize arg2)
        {
            return new UnitSize(arg1.Width - arg2.Width, arg1.Height - arg2.Height);
        }

        /// <summary>
        /// The multiplication operator.
        /// </summary>
        /// <param name="arg1">UnitSize to multiply.</param>
        /// <param name="arg2">UnitSize to multiply.</param>
        /// <returns></returns>
        public static UnitSize operator *(UnitSize arg1, UnitSize arg2)
        {
            return new UnitSize(arg1.Width * arg2.Width, arg1.Height * arg2.Height);
        }

        /// <summary>
        /// The multiplication operator.
        /// </summary>
        /// <param name="arg1">UnitSize to multiply.</param>
        /// <param name="arg2">The int value to scale the UnitSize.</param>
        /// <returns></returns>
        public static UnitSize operator *(UnitSize arg1, int arg2)
        {
            return new UnitSize(arg1.Width * arg2, arg1.Height * arg2);
        }

        /// <summary>
        /// The division operator.
        /// </summary>
        /// <param name="arg1">UnitSize to divide.</param>
        /// <param name="arg2">UnitSize to divide.</param>
        /// <returns></returns>
        public static UnitSize operator /(UnitSize arg1, UnitSize arg2)
        {
            return new UnitSize(arg1.Width / arg2.Width, arg1.Height / arg2.Height);
        }

        /// <summary>
        /// The division operator.
        /// </summary>
        /// <param name="arg1">UnitSize to divide.</param>
        /// <param name="arg2">The int value to scale the UnitSize by.</param>
        /// <returns></returns>
        public static UnitSize operator /(UnitSize arg1, int arg2)
        {
            return new UnitSize(arg1.Width / arg2, arg1.Height / arg2);
        }

        /// <summary>
        /// The Equality operator.
        /// </summary>
        /// <param name="a">The first operand.</param>
        /// <param name="b">The second operand.</param>
        /// <returns>True if the UniSizes are exactly the same. </returns>
        public static bool operator ==(UnitSize a, UnitSize b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Width == b.Width && a.Height == b.Height;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="a">The first operand.</param>
        /// <param name="b">The second operand.</param>
        /// <returns>True if the UnitSizes are not identical.</returns>
        public static bool operator !=(UnitSize a, UnitSize b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if UnitSizes are exactly same.</returns>
        public override bool Equals(object obj)
        {
            UnitSize l = this;
            UnitSize r = obj as UnitSize;
            if (r == null)
            {
                return false;
            }
            return l == r;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        UnitSize IObservable<UnitSize>.NotifiableClone(PropertyChangedDelegate<UnitSize> handler)
        {
            UnitSize clone = this.MemberwiseClone() as UnitSize;
            if (clone != null)
            {
                clone.propertyChanged = handler;
            }
            return clone;
        }

        private int width;
        private int height;
        private PropertyChangedDelegate<UnitSize> propertyChanged;
    }    

    internal static partial class IObservableExtensions
    {
        public static UnitSize NotifiableClone(this IObservable<UnitSize> unitSize, PropertyChangedDelegate<UnitSize> handler)
        {
            return unitSize.NotifiableClone(handler);
        }
    }
}