using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Plane
    {
        double _a;
        double _b;
        double _c;
        double _d;

        public Plane(Vector3d normal, Point3d point)
        {
            Normal = normal;
            _a = normal.X;
            _b = normal.Y;
            _c = normal.Z;
            _d = -(_a * point.X + _b * point.Y + _c * point.Z);
        }

        //public Point3d IntersectWith(Line line)
        //{
        //    var dot = line.Direction.Dot(Normal);
        //    if (dot == 0)
        //        return null;

        //    var t = -(_a * line.PointOnLine.X + _b * line.PointOnLine.Y + _c * line.PointOnLine.Z + _d) / dot;

        //    return new Point3d(line.PointOnLine.X + line.Direction.X * t, line.PointOnLine.Y + line.Direction.Y * t, line.PointOnLine.Z + line.Direction.Z * t);
        //}
        public Vector3d Normal { get; }
    }
}
