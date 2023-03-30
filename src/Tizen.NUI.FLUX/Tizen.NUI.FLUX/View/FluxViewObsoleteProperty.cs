/// @file FluxViewObsoleteProperty.cs
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
using System.ComponentModel;
using Tizen.NUI.BaseComponents;
using Tizen.NUI;

namespace Tizen.NUI.FLUX
{
    /// <summary>
    /// FluxView is the base class for all views in FLUX Application
    /// </summary>
    /// <code>
    /// view = new FluxView();
    /// view.UnitPosition = new UnitPosition(50, 50);
    /// view.UnitSizeWidth = 50;
    /// view.UnitSizeHeight = 50;
    /// view.BackgroundColor = Color.Red;
    /// </code>
    public partial class FluxView : View
    {
        ///*********************** Don't use ******************************///

        /// <summary>
        /// Gets or sets the base Position of the fluxview for layout
        /// </summary>
        [Obsolete("Please do not use! Position is obsolete.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Position Position
        {
            set
            {
                if (value != null)
                {
                    if (value.NotEqualTo(base.Position))
                    {
                        base.Position = value;
                        EmitPositionChangedEvent();
                    }
                }
            }
            get
            {
                return new Position(base.Position.X, base.Position.Y, base.Position.Z);
            }
        }

        /// <summary>
        /// Gets or sets the base Position2D of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Position2D Position2D
        {
            set
            {
                if (value != null)
                {
                    if (value.NotEqualTo(base.Position2D))
                    {
                        base.Position2D = value;
                        EmitPositionChangedEvent();
                    }
                }
            }
            get
            {
                return base.Position2D;
            }
        }

        /// <summary>
        /// Gets or sets the base Size of the fluxview for layout 
        /// </summary>
        [Obsolete("Please do not use! Size is obsolete.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Size Size
        {
            set
            {
                if (value != null)
                {
                    if (value.NotEqualTo(base.Size))
                    {
                        EmitPreSizeChangedEvent((int)value.Width, (int)value.Height);
                        base.Size = value;
                        EmitSizeChangedEvent();
                    }
                }
            }
            get
            {
                return new Size(base.Size.Width, base.Size.Height, base.Size.Depth);
            }
        }

        /// <summary>
        /// Gets or sets the base Size2D of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Size2D Size2D
        {
            set
            {
                if (value != null)
                {
                    if (value.NotEqualTo(base.Size2D))
                    {
                        EmitPreSizeChangedEvent(value.Width, value.Height);
                        base.Size2D = value;
                        EmitSizeChangedEvent();
                    }
                }
            }
            get
            {
                return base.Size2D;
            }
        }

        /// <summary>
        /// Gets the base WorldPosition of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Vector3 WorldPosition
        {
            get
            {
                return base.WorldPosition;
            }
        }

        /// <summary>
        /// Gets the base CurrentPosition of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Position CurrentPosition
        {
            get
            {
                return base.CurrentPosition;
            }
        }

        /// <summary>
        /// Gets the base ScreenPosition of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Vector2 ScreenPosition
        {
            get
            {
                return base.ScreenPosition;
            }
        }

        /// <summary>
        /// Gets the base CurrentSize of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Size2D CurrentSize
        {
            get
            {
                return base.CurrentSize;
            }
        }

        /// <summary>
        /// Gets the base NaturalSize of the fluxview for layout 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Vector3 NaturalSize
        {
            get
            {
                return base.NaturalSize;
            }
        }

        /// <summary>
        /// Gets the base NaturalSize2D of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Size2D NaturalSize2D
        {
            get
            {
                return base.NaturalSize2D;
            }
        }

        /// <summary>
        /// Gets or sets the base PositionX of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new float PositionX
        {
            set
            {
                if (value.Equals(base.PositionX) == false)
                {
                    base.PositionX = value;
                    EmitPositionChangedEvent();
                }
            }
            get
            {
                return base.PositionX;
            }
        }
        /// <summary>
        /// Gets or sets the base PositionY of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new float PositionY
        {
            set
            {
                if (value.Equals(base.PositionY) == false)
                {
                    base.PositionY = value;
                    EmitPositionChangedEvent();
                }
            }
            get
            {
                return base.PositionY;
            }
        }
        /// <summary>
        /// Gets or sets the base PositionZ of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new float PositionZ
        {
            set
            {
                if (value.Equals(base.PositionZ) == false)
                {
                    base.PositionZ = value;
                    EmitPositionChangedEvent();
                }
            }
            get
            {
                return base.PositionZ;
            }
        }

        /// <summary>
        /// Gets or sets the base size width of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new float SizeWidth
        {
            set
            {
                if (value.Equals(base.SizeWidth) == false)
                {
                    EmitPreSizeChangedEvent((int)value, (int)base.SizeHeight);
                    base.SizeWidth = value;
                    EmitSizeChangedEvent();
                }
            }
            get
            {
                return base.SizeWidth;
            }
        }

        /// <summary>
        /// Gets or sets the base size height of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new float SizeHeight
        {
            set
            {
                if (value.Equals(base.SizeHeight) == false)
                {
                    EmitPreSizeChangedEvent((int)base.SizeWidth, (int)value);
                    base.SizeHeight = value;
                    EmitSizeChangedEvent();
                }
            }
            get
            {
                return base.SizeHeight;
            }
        }

        /// <summary>
        /// Gets or sets the base maximum size of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Size2D MaximumSize
        {
            set
            {
                if (value != null)
                {
                    base.MaximumSize = value;
                }
            }
            get
            {
                return base.MaximumSize;
            }
        }

        /// <summary>
        /// Gets or sets the base minimum size of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Size2D MinimumSize
        {
            set
            {
                if (value != null)
                {
                    base.MinimumSize = value;
                }
            }
            get
            {
                return base.MinimumSize;
            }
        }

        /// <summary>
        /// Gets or sets the base margin of the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Extents Margin
        {
            set
            {
                if (value != null)
                {
                    base.Margin = value;
                }
            }
            get
            {
                return base.Margin;
            }
        }

        /// <summary>
        /// Gets or sets the base padding value for the fluxview for layout
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Extents Padding
        {
            set
            {
                if (value != null)
                {
                    base.Padding = value;
                }
            }
            get
            {
                return base.Padding;
            }
        }

        /// <summary>
        /// Gets or sets the base PositionUsesPivotPoint of the fluxview for layout
        /// </summary>
        /// <version> 9.9.0 </version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool PositionUsesPivotPoint
        {
            set
            {
                if (value.Equals(base.PositionUsesPivotPoint) == false)
                {
                    base.PositionUsesPivotPoint = value;
                    EmitGeometryChangedEvent();
                }
            }
            get
            {
                return base.PositionUsesPivotPoint;
            }
        }

        /// <summary>
        /// Gets or sets the base PivotPoint of the fluxview for layout
        /// </summary>
        /// <version> 9.9.0 </version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Position PivotPoint
        {
            set
            {
                if (value.NotEqualTo(base.PivotPoint))
                {
                    base.PivotPoint = value;
                    EmitGeometryChangedEvent();
                }
            }
            get
            {
                return base.PivotPoint;
            }
        }

        /// <summary>
        /// Gets or sets the base Scale of the fluxview for layout
        /// </summary>
        /// <version> 9.9.0 </version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Vector3 Scale
        {
            set
            {
                if (value.Equals(base.Scale) == false)
                {
                    base.Scale = value;
                    EmitGeometryChangedEvent();
                }
            }
            get
            {
                return base.Scale;
            }
        }

        /// <summary>
        /// Gets or sets the base ScaleX of the fluxview for layout
        /// </summary>
        /// <version> 9.9.0 </version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new float ScaleX
        {
            set
            {
                if (value.Equals(base.ScaleX) == false)
                {
                    base.ScaleX = value;
                    EmitGeometryChangedEvent();
                }
            }
            get
            {
                return base.ScaleX;
            }
        }

        /// <summary>
        /// Gets or sets the base ScaleY of the fluxview for layout
        /// </summary>
        /// <version> 9.9.0 </version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new float ScaleY
        {
            set
            {
                if (value.Equals(base.ScaleY) == false)
                {
                    base.ScaleY = value;
                    EmitGeometryChangedEvent();
                }
            }
            get
            {
                return base.ScaleY;
            }
        }

        /// <summary>
        /// Gets or sets the base ScaleZ of the fluxview for layout
        /// </summary>
        /// <version> 9.9.0 </version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new float ScaleZ
        {
            set
            {
                if (value.Equals(base.ScaleZ) == false)
                {
                    base.ScaleZ = value;
                    EmitGeometryChangedEvent();
                }
            }
            get
            {
                return base.ScaleZ;
            }
        }
    }
}