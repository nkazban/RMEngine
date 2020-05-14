using Core;
using Geometry;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneModel
{
    public class Scene
    {
        public Ball Ball;
        public Camera Camera;
        public GraphicsContext GC;
        public Shader shader;
        Viewport _viewport;

        uint[] VAO = new uint[] { 0 };
        uint[] VBO = new uint[] { 0 };
        public void Create(OpenGL gl, Viewport viewport)
        {
            _viewport = viewport;
            GC = new GraphicsContext();
            GC.GL = gl;
            shader = new Shader(GC, "vertex.vert", "frag.frag");

            float[] vertices = {
            -1f, -1f, 0f,
            -1f, 1f, 0f,
            1f, -1f, 0f,
            1f,  1f, 0.0f,

            };
            gl.GenBuffers(1, VBO);
            gl.GenVertexArrays(1, VAO);

            gl.BindVertexArray(VAO[0]);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBO[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, vertices, OpenGL.GL_STATIC_DRAW);

            gl.VertexAttribPointer(0, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(0);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);

            gl.BindVertexArray(0);
        }
        public void Draw()
        {
            var gl = GC.GL;
            gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //Sphere.Center.Y += 0.01;

            shader.Use();

            var resolutionUniform = gl.GetUniformLocation(shader._id, "Resolution");
            if (resolutionUniform > -1)
            {
                gl.Uniform2(resolutionUniform, (float)_viewport.Width, (float)_viewport.Height);
            }

            var cameraUniform = gl.GetUniformLocation(shader._id, "cameraPosition");
            if (cameraUniform > -1)
            {
                gl.Uniform3(cameraUniform, (float)Camera.Position.X, (float)Camera.Position.Y, (float)Camera.Position.Z);
            }
            SetUniform(gl, shader, "sphereObj", Ball);

            gl.BindVertexArray(VAO[0]);
            gl.DrawArrays(OpenGL.GL_TRIANGLE_STRIP, 0, 4);
            gl.BindVertexArray(0);
        }
        static void SetUniform(OpenGL gl, Shader shader, string name, Vector3d vector)
        {
            var loc = gl.GetUniformLocation(shader._id, name);
            if(loc > -1)
            {
                gl.Uniform3(loc, (float)vector.X, (float)vector.Y, (float)vector.Z);
            }
        }
        static void SetUniform(OpenGL gl, Shader shader, string name, Point3d point)
        {
            var loc = gl.GetUniformLocation(shader._id, name);
            if (loc > -1)
            {
                gl.Uniform3(loc, (float)point.X, (float)point.Y, (float)point.Z);
            }
        }
        static void SetUniform(OpenGL gl, Shader shader, string name, float value) 
        {
            var loc = gl.GetUniformLocation(shader._id, name);
            if (loc > -1)
            {
                gl.Uniform1(loc, value);
            }
        }
        static void SetUniform(OpenGL gl, Shader shader, string name, Ball ball)
        {
            SetUniform(gl, shader, name + ".Position", ball.Center);
            SetUniform(gl, shader, name + ".Radius", (float)ball.Radius);
            SetUniform(gl, shader, name + ".Color", new Vector3d(1, 0, 0));
        }
    }
}
