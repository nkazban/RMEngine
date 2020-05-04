using Core;
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
            //sphere.Center.X += 0.01;
            //sphere.Center.Z += 0.01;

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

            //var sphereUniform = gl.GetUniformLocation(shader._id, "spherePosition");
            //if (sphereUniform > -1)
            //{
            //    gl.Uniform3(sphereUniform, (float)sphere.Center.X, (float)sphere.Center.Y, (float)sphere.Center.Z);
            //}

            gl.BindVertexArray(VAO[0]);
            gl.DrawArrays(OpenGL.GL_TRIANGLE_STRIP, 0, 4);
            gl.BindVertexArray(0);
        }
    }
}
