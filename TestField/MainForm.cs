using Core;
using SharpGL;
using System;
using System.Globalization;
using System.Windows.Forms;
using System.Diagnostics;
using SceneModel;
using Geometry;
using Utils;
using Core.Physics;

namespace TestField
{
    
    public partial class MainForm : Form
    {
        OpenGL _gl;

        Camera camera;
        Ball ball;
        Scene scene;

        Vector3d force;
        double frictionKoef = 0.06;
        double epsilon = 0.05;
        
        public MainForm()
        {
            InitializeComponent();
            this.MouseWheel += MainForm_MouseWheel;
        }

        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            int sign = 1;
            if (e.Delta > 0)
                sign = 1;
            else
                sign = -1;
            camera.OnMouseWheel(this, sign);
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            force = new Vector3d(0, 0, 0);
            camera = new Camera(new Point3d(0, 1, 10), new Vector3d(0, -1, 0));
            ball = new Ball(0.005, new Point3d(0, 1, 0), 1);
            _gl = (sender as OpenGLControl).OpenGL;


            Viewport viewport = new Viewport();
            viewport.Height = (sender as OpenGLControl).Size.Height;
            viewport.Width = (sender as OpenGLControl).Size.Width;
            scene = new Scene();
            scene.Create(_gl, viewport);
            scene.Camera = camera;
            scene.Ball = ball;
        }

        private void openGLControl_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            var gravityForce = -Vector3d.UpDirection() * ball.RigidBody.Mass * Constants.G;
            if (ball.Center.Y - ball.Radius > 0)
            {
                force += gravityForce;
                ball.Center.X += force.X;
                ball.Center.Z += force.Z;
                if (-force.Y > ball.Radius)
                    ball.Center.Y = ball.Radius;
                else
                    ball.Center.Y += force.Y;
            }
            else
            {
                force.Y = -force.Y * 0.4;
                force -= force * frictionKoef;
                if (force.Length < gravityForce.Length)
                    force = new Vector3d(0, 0, 0);
                ball.Center.Y = ball.Radius;
                ball.Center += new Vector3d(0, force.Y, 0);
            }
            scene.Draw();
        }

        private void openGLControl_KeyDown(object sender, KeyEventArgs e)
        {
            Direction dir = Direction.UP;
            switch(e.KeyData)
            {
                case Keys.W:
                    dir = Direction.UP;
                    break;
                case Keys.S:
                    dir = Direction.DOWN;
                    break;
                case Keys.A:
                    dir = Direction.LEFT;
                    break;
                case Keys.D:
                    dir = Direction.RIGHT;
                    break;
            }
            camera.OnKeyDown(this, dir);
        }

        private void jumpButton_Click(object sender, EventArgs e)
        {
            double xcomp = Convert.ToDouble(XtextBox.Text, CultureInfo.InvariantCulture);
            double ycomp = Convert.ToDouble(YtextBox.Text, CultureInfo.InvariantCulture);
            double zcomp = Convert.ToDouble(ZtextBox.Text, CultureInfo.InvariantCulture);

            force = new Vector3d(xcomp, ycomp, zcomp);
            ball.Center += new Vector3d(0, force.Y, 0);
        }
    }
}
