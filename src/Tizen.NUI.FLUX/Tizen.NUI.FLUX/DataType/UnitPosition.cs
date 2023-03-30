/// @file UnitPosition.cs
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
    /// View view = new View();
    /// view.SetFluxSize(new Unit(10, 10));
    /// </code>
    [Tizen.NUI.Binding.TypeConverter(typeof(UnitPositionTypeConverter))]
    public class UnitPosition : IObservable<UnitPosition>
    {
        /// <summary>
        /// Constructor to instantiate the UnitSize class.
        /// </summary>
        /// <param name="x">The PositionX of position in flux</param>
        /// <param name="y">The PositionY of position in flux</param>
        public UnitPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        /// <summary>
        /// The PositionX of position in flux
        /// </summary>
        public int X
        {
            get
            {
                return x;
            }

            set
            {
                if (x != value)
                {
                    x = value;
                    propertyChanged?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// The PositionY of position in flux
        /// </summary>
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                if (y != value)
                {
                    y = value;
                    propertyChanged?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// operator for add
        /// </summary>
        /// <param name="arg1">UnitPosition to add.</param>
        /// <param name="arg2">UnitPosition to add.</param>
        /// <returns></returns>
        public static UnitPosition operator +(UnitPosition arg1, UnitPosition arg2)
        {
            return new UnitPosition(arg1.X + arg2.X, arg1.Y + arg2.Y);
        }

        /// <summary>
        /// operator for subtract
        /// </summary>
        /// <param name="arg1">UnitPosition to subtract.</param>
        /// <param name="arg2">UnitPosition to subtract.</param>
        /// <returns></returns>
        public static UnitPosition operator -(UnitPosition arg1, UnitPosition arg2)
        {
            return new UnitPosition(arg1.X - arg2.X, arg1.Y - arg2.Y);
        }

        /// <summary>
        /// The multiplication operator.
        /// </summary>
        /// <param name="arg1">UnitPosition to multiply.</param>
        /// <param name="arg2">UnitPosition to multiply.</param>
        /// <returns></returns>
        public static UnitPosition operator *(UnitPosition arg1, UnitPosition arg2)
        {
            return new UnitPosition(arg1.X * arg2.X, arg1.Y * arg2.Y);
        }

        /// <summary>
        /// The multiplication operator.
        /// </summary>
        /// <param name="arg1">UnitPosition to multiply.</param>
        /// <param name="arg2">The int value to scale the UnitPosition.</param>
        /// <returns></returns>
        public static UnitPosition operator *(UnitPosition arg1, int arg2)
        {
            return new UnitPosition(arg1.X * arg2, arg1.Y * arg2);
        }

        /// <summary>
        /// The division operator.
        /// </summary>
        /// <param name="arg1">UnitPosition to divide.</param>
        /// <param name="arg2">UnitPosition to divide.</param>
        /// <returns></returns>
        public static UnitPosition operator /(UnitPosition arg1, UnitPosition arg2)
        {
            return new UnitPosition(arg1.X / arg2.X, arg1.Y / arg2.Y);
        }

        /// <summary>
        /// The division operator.
        /// </summary>
        /// <param name="arg1">UnitPosition to divide.</param>
        /// <param name="arg2">The int value to scale the UnitPosition by.</param>
        /// <returns></returns>
        public static UnitPosition operator /(UnitPosition arg1, int arg2)
        {
            return new UnitPosition(arg1.X / arg2, arg1.Y / arg2);
        }

        /// <summary>
        /// The Equality operator.
        /// </summary>
        /// <param name="a">The first operand.</param>
        /// <param name="b">The second operand.</param>
        /// <returns>True if the UnitPositions are exactly the same. </returns>
        public static bool operator ==(UnitPosition a, UnitPosition b)
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
            return a.X == b.X && a.Y == b.Y;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="a">The first operand.</param>
        /// <param name="b">The second operand.</param>
        /// <returns>True if the UnitPositions are not identical.</returns>
        public static bool operator !=(UnitPosition a, UnitPosition b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if UnitPositions are exactly same.</returns>
        public override bool Equals(object obj)
        {
            UnitPosition l = this;
            UnitPosition r = obj as UnitPosition;
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

        UnitPosition IObservable<UnitPosition>.NotifiableClone(PropertyChangedDelegate<UnitPosition> handler)
        {
            UnitPosition clone = this.MemberwiseClone() as UnitPosition;
            if (clone != null)
            {
                clone.propertyChanged = handler;
            }
            return clone;
        }

        private int x;
        private int y;
        private PropertyChangedDelegate<UnitPosition> propertyChanged;
    }
    
    internal static partial class IObservableExtensions
    {
        public static UnitPosition NotifiableClone(this IObservable<UnitPosition> unitPosition, PropertyChangedDelegate<UnitPosition> handler)
        {
            return unitPosition.NotifiableClone(handler);
        }
    }

}