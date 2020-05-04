using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Sphere
    {
        public Sphere(Point3d center, double radius)
        {
            Center = center;
            Radius = radius;
        }
        public Point3d Center { get; set; }
        public double Radius { get; set; }
    }
}
