using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Point3d
    {
        public Point3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public static Point3d operator +(Point3d p, Vector3d v)
        {
            return new Point3d(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }
        public static Point3d operator -(Point3d p, Vector3d v)
        {
            return new Point3d(p.X - v.X, p.Y - v.Y, p.Z - v.Z);
        }
        public static Vector3d operator -(Point3d p, Point3d v)
        {
            return new Vector3d(p.X - v.X, p.Y - v.Y, p.Z - v.Z);
        }
        public static Point3d CenterBetween(Point3d p1, Point3d p2)
        {
            return new Point3d((p1.X + p2.X) * 0.5, (p1.Y + p2.Y) * 0.5, (p1.Z + p2.Z) * 0.5);
        }

        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }
    }
}
