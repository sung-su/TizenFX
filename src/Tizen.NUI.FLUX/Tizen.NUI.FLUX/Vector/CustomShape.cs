/// @file CustomShape.cs
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
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    using Point = Vector2;

    /// <summary>
    /// Custom Shape is a class for drawing shape using vector paths.
    /// </summary>
    /// <code>
    /// vectorPrimitiveView = new VectorPrimitiveView();
    /// vectorPrimitiveView.Size2D = new Size2D(100, 100);
    /// Stroke strokeStyle = new Stroke(new SolidColor(Color.Blue), 1);
    /// Fill fillStyle = new Fill(new SolidColor(Color.Red));
    /// CustomShape customShape = new CustomShape(strokeStyle, fillStyle);
    /// customShape.MoveTo(new Position2D(0, 0));
    /// customShape.LineTo(new Position2D(100, 0));
    /// customShape.LineTo(new Position2D(100, 100));
    /// customShape.LineTo(new Position2D(0, 100));
    /// customShape.Close();
    /// vectorPrimitiveView.SetShape(customShape);
    /// </code>
    public class CustomShape : Shape
    {
        private List<IPathVerbs> mPath = new List<IPathVerbs>();

        /// <summary>
        /// CustomShape Constructor
        /// </summary>
        /// <param name="strokeStyle">Style for drawing stroke of shape</param>
        /// <param name="fillStyle">Style for drawing fill of shape</param>
        public CustomShape(Stroke strokeStyle = null, Fill fillStyle = null)
            : base(strokeStyle, fillStyle)
        {

        }

        /// <summary>
        /// Add a MoveTo command
        /// </summary>
        /// <param name="p">Position to be moved to</param>
        /// <exception cref="ArgumentNullException">Thrown when p instance is null</exception>
        public void MoveTo(Point p)
        {
            if (p == null)
            {
                throw new ArgumentNullException("Point is null.");
            }
            Point mt = new Point(p.X, p.Y);
            mPath.Add(new MoveTo(mt));
        }

        /// <summary>
        /// Add a LineTo command
        /// </summary>
        /// <param name="p">Position of the end of the line.</param>
        /// <exception cref="ArgumentNullException">Thrown when p instance is null.</exception>
        public void LineTo(Point p)
        {
            if (p == null)
            {
                throw new ArgumentNullException("Point is null");
            }
            Point lt = new Point(p.X, p.Y);
            mPath.Add(new LineTo(lt));
        }

        /// <summary>
        /// Add a BezierTo command
        /// </summary>
        /// <param name="cp1">Control Point 1</param>
        /// <param name="cp2">Control Point 2</param>
        /// <param name="end">End Point of the Bezeier Curve</param>
        /// <exception cref="ArgumentNullException">Thrown when cp1, cp2 or end instances are null.</exception>
        public void BezierTo(Point cp1, Point cp2, Point end)
        {
            if (cp1 == null || cp2 == null || end == null)
            {
                throw new ArgumentNullException("One or more points is null");
            }
            Point btCp1 = new Point(cp1.X, cp1.Y);
            Point btCp2 = new Point(cp2.X, cp2.Y);
            Point btEnd = new Point(end.X, end.Y);

            mPath.Add(new BezierTo(btCp1, btCp2, btEnd));
        }

        /// <summary>
        /// Add a ArcTo command
        /// </summary>
        /// <param name="centerPoint">Center Point of Arc</param>
        /// <param name="radiusX">Radius X of Arc</param>
        /// <param name="radiusY">Radius Y of Arc</param>
        /// <param name="startAngle">Start angle of Arc</param>
        /// <param name="endAngle">End angle of Arc</param>
        /// <param name="clockwiseDirection">Clockwise direction of Arc</param>
        /// <exception cref="ArgumentNullException">Thrown when centerPoint instance is null.</exception>
        public void ArcTo(Point centerPoint, int radiusX, int radiusY, float startAngle, float endAngle, int clockwiseDirection)
        {
            if (centerPoint == null)
            {
                throw new ArgumentNullException("Point is null");
            }
            Point atCp = centerPoint;
            //new Point(centerPoint.X, centerPoint.Y);

            mPath.Add(new ArcTo(atCp, radiusY, radiusY, startAngle, endAngle, clockwiseDirection));
        }

        /// <summary>
        /// Add a Close command
        /// </summary>
        public void Close()
        {
            mPath.Add(new Close());
        }

        internal override void Draw(VectorView view)
        {
            base.Draw(view);
            DrawPaths(view);
        }

        internal override RenderBackend GetRenderBackend()
        {
            return RenderBackend.CairoRenderer;
        }

        private void DrawPaths(VectorView view)
        {
            Interop.VectorView.StartPath(View.getCPtr(view));

            mPath.ForEach(path => path.Draw(view));

            Interop.VectorView.FinishPath(View.getCPtr(view));
        }
    }
}
