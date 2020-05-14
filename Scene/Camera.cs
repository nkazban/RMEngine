using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace SceneModel
{
    public class Camera
    {

        double sensivity = 0.5;

        public float Zoom { get; private set; } = -1.0f;
        public Camera(Point3d position, Vector3d front)
        {
            Position = position;
            Front = front;
        }
        public Vector3d Front { get; set; }
        public Vector3d UpDirection => new Vector3d(0, 1, 0);
        public Point3d Position { get; set; }
        
        public void OnKeyDown(object sender, Direction dir)
        {
            switch (dir)
            {
                case Direction.UP:
                    Position += sensivity * Vector3d.UpDirection();
                    break;
                case Direction.DOWN:
                    Position -= sensivity * Vector3d.UpDirection();
                    break;
                case Direction.LEFT:
                    Position += Vector3d.LeftDirection() * sensivity;
                    break;
                case Direction.RIGHT:
                    Position -= Vector3d.LeftDirection() * sensivity;
                    break;
            }
        }

        public void OnMouseWheel(object sender, int sign) {
            Position += Vector3d.ForwardDirection() * sign * sensivity;
        }
    }
}
