using Core;
using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SceneModel;
using Geometry;
using Utils;

namespace TestField
{
    
    public partial class MainForm : Form
    {
        GraphicsContext _gc;
        OpenGL _gl;
        Shader shader;


        Camera camera;
        Sphere sphere;
        Scene scene;

        
        public MainForm()
        {
            InitializeComponent();
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            camera = new Camera(new Point3d(0, 1, -1), new Vector3d(0, 0, -1));
            //sphere = new Sphere(new Point3d(0, 0, 6), 1);
            _gl = (sender as OpenGLControl).OpenGL;
            Viewport viewport = new Viewport();
            viewport.Height = (sender as OpenGLControl).Size.Height;
            viewport.Width = (sender as OpenGLControl).Size.Width;
            scene = new Scene();
            scene.Create(_gl, viewport);
            scene.Camera = camera;
        }

        private void openGLControl_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            scene.Draw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
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
    }
}
