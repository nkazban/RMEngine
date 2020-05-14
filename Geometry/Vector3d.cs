using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Vector3d
    {
        public Vector3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        public static Vector3d UpDirection() => new Vector3d(0, 1, 0);

        public static Vector3d LeftDirection() => new Vector3d(1, 0, 0);

        public static Vector3d ForwardDirection() => new Vector3d(0, 0, -1);
        public void Normalize()
        {
            X = X / Length;
            Y = Y / Length;
            Z = Z / Length;
        }
        public Vector3d Cross(Vector3d v)
        {
            double x = Y * v.Z - Z * v.Y;
            double y = Z * v.X - X * v.Z;
            double z = X * v.Y - v.X * Y;
            return new Vector3d(x, y, z);
        }

        public double Dot(Vector3d v)
        {
            return v.X * X + v.Y * Y + v.Z * Z;
        }

        public Vector3d ProjectTo(Vector3d vec)
        {
            return vec * (vec.Dot(this) / (vec.Length * vec.Length));
        }
        public static Vector3d operator +(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3d operator -(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static Vector3d operator -(Vector3d v1)
        {
            return new Vector3d(-v1.X, -v1.Y, -v1.Z);
        }
        public static Vector3d operator *(Vector3d v, double num)
        {
            return new Vector3d(v.X * num, v.Y * num, v.Z * num);
        }

        public static Vector3d operator *(double num, Vector3d v)
        {
            return new Vector3d(v.X * num, v.Y * num, v.Z * num);
        }

        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }
    }
}
