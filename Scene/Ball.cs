using Core.Physics;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SceneModel
{
    public class Ball
    {
        Sphere _grapicsPart;
        Body _physicsPart;

        public Ball(double mass, Point3d center, double radius)
        {
            _physicsPart = new Body(mass);
            _grapicsPart = new Sphere(center, radius);
        }

        public Point3d Center
        {
            get => _grapicsPart.Center;
            set => _grapicsPart.Center = value;
        }

        public double Radius => _grapicsPart.Radius;

        public Body RigidBody => _physicsPart;
    }
}
