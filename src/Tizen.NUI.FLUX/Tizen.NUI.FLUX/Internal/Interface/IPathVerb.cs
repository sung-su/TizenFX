/// @file IPathVerb.cs
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



using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.FLUX
{
    using Point = Vector2;

    internal interface IPathVerbs
    {
        void Draw(VectorView view);
    }

    /// <summary>
    /// Move To
    /// </summary>
    internal class MoveTo : IPathVerbs
    {
        public MoveTo(Point point)
        {
            Point = point;
        }

        private Point Point { set; get; }

        public void Draw(VectorView view)
        {
            CLog.Debug("MoveTo : [%f1, %f2]", f1: Point.X, f2: Point.Y);
            Interop.VectorView.MoveTo(View.getCPtr(view), Point.X, Point.Y);
        }
    }

    /// <summary>
    /// Line To
    /// </summary>
    internal class LineTo : IPathVerbs
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="point"></param>
        public LineTo(Point point)
        {
            End = point;
        }

        private Point End { set; get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        public void Draw(VectorView view)
        {
            CLog.Debug("LineTo : [%f1, %f2]", f1: End.X, f2: End.Y);
            Interop.VectorView.LineTo(View.getCPtr(view), End.X, End.Y);
        }
    }

    /// <summary>
    /// Bezier To
    /// </summary>
    internal class BezierTo : IPathVerbs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="cp1"></param>
        /// <param name="cp2"></param>
        public BezierTo(Point cp1, Point cp2, Point endPoint)
        {
            EndPoint = endPoint;
            ControlPoint1 = cp1;
            ControlPoint2 = cp2;
        }

        private Point EndPoint { set; get; }
        private Point ControlPoint1 { set; get; }
        private Point ControlPoint2 { set; get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        public void Draw(VectorView view)
        {
            CLog.Debug("BezierTo : ControlPoint1[%f1, %f2] ControlPoint2[%f3, %f4]"
                , f1: ControlPoint1.X
                , f2: ControlPoint1.Y
                , f3: ControlPoint2.X
                , f4: ControlPoint2.Y
                );
            CLog.Debug("BezierTo : EndPoint[%f1, %f2]", f1: EndPoint.X, f2: EndPoint.Y);
            Interop.VectorView.BezierTo(View.getCPtr(view), ControlPoint1.X, ControlPoint1.Y, ControlPoint2.X, ControlPoint2.Y, EndPoint.X, EndPoint.Y);
        }
    }

    /// <summary>
    /// Arc To
    /// </summary>
    internal class ArcTo : IPathVerbs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cp">Center Point</param>
        /// <param name="rx">Radius X</param>
        /// <param name="ry">Radius Y</param>
        /// <param name="sAngle">Start angle</param>
        /// <param name="eAngle">End angle</param>
        /// <param name="orient">Orient</param>
        public ArcTo(Point cp, int rx, int ry, float sAngle, float eAngle, int orient)
        {
            CenterPoint = cp;
            RadiusX = rx;
            RadiusY = ry;
            StartAngle = sAngle;
            EndAngle = eAngle;
            Orient = orient;
        }

        private Point CenterPoint { set; get; }
        private int RadiusX { set; get; }
        private int RadiusY { set; get; }
        private float StartAngle { set; get; }
        private float EndAngle { set; get; }
        private int Orient { set; get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        public void Draw(VectorView view)
        {
            CLog.Debug("ArcTo : [%f1, %f2] [%d1, %d2] [%f3] [%f4] [%d3]"
                , f1: CenterPoint.X
                , f2: CenterPoint.Y
                , d1: RadiusX
                , d2: RadiusY
                , f3: StartAngle
                , f4: EndAngle
                , d3: Orient
                );
            Interop.VectorView.ArcTo(View.getCPtr(view), CenterPoint.X, CenterPoint.Y, RadiusX, RadiusY, StartAngle, EndAngle, Orient);
        }
    }

    /// <summary>
    /// Close
    /// </summary>
    internal class Close : IPathVerbs
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="view"></param>
        public void Draw(VectorView view)
        {
            CLog.Debug($"Close");
            Interop.VectorView.Close(View.getCPtr(view));
        }
    }
}
