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
        double _yaw = 0;
        double _pitch = -90;

        double speed = 1;
        double sensivity = 0.5;
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
                    Position += speed * Front;
                    break;
                case Direction.DOWN:
                    Position -= speed * Front;
                    break;
                case Direction.LEFT:
                    Position -= Front.Cross(UpDirection) * speed;
                    break;
                case Direction.RIGHT:
                    Position += Front.Cross(UpDirection) * speed;
                    break;
            }
        }

        public void OnMouseMove(object sender, Vector2d delta)
        {
            double offsetX = delta.X;
            double offsetY = delta.Y;
            offsetX *= sensivity;
            offsetY *= sensivity;
            _yaw += offsetX;
            _pitch += offsetY;
            if (_pitch > 89.0f)
                _pitch = 89.0f;
            if (_pitch < -89.0f)
                _pitch = -89.0f;
            var front = new Vector3d(0, 0, 0);
            front.X = Math.Cos(Helpers.ToRadians(_pitch)) * Math.Cos(Helpers.ToRadians(_yaw));
            front.Y = Math.Sin(Helpers.ToRadians(_pitch));
            front.Z = Math.Cos(Helpers.ToRadians(_pitch)) * Math.Sin(Helpers.ToRadians(_yaw));
            front.Normalize();
            Front = front;
        }
    }
}
