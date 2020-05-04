using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Core
{
    public class Shader
    {
        public uint _id;
        uint _vertex;
        uint _frag;

        GraphicsContext _gc;
        OpenGL _gl;
        public Shader(GraphicsContext gc, string vertexShader, string fragShader)
        {
            _gc = gc;
            var gl = _gc.GL;
            _gl = gl;
            _id = gl.CreateProgram();

            var vertexShaderSource = File.ReadAllText(vertexShader);
            _vertex = gl.CreateShader(OpenGL.GL_VERTEX_SHADER);
            gl.ShaderSource(_vertex, vertexShaderSource);
            gl.CompileShader(_vertex);
            GetCompileInfo(_vertex);
            

            var fragShaderSource = File.ReadAllText(fragShader);
            _frag = gl.CreateShader(OpenGL.GL_FRAGMENT_SHADER);
            gl.ShaderSource(_frag, fragShaderSource);
            gl.CompileShader(_frag);
            GetCompileInfo(_frag);

            gl.AttachShader(_id, _vertex);
            gl.AttachShader(_id, _frag);
            gl.LinkProgram(_id);
            GetProgramInfo();

            gl.DeleteShader(_vertex);
            gl.DeleteShader(_frag);
        }

        public void Use()
        {
            _gc.GL.UseProgram(_id);
            Debug.WriteLine(_gl.GetErrorDescription(_gl.GetError()));
        }

        void GetCompileInfo(uint shaderId)
        {
            int[] infoLength = new int[] { 0 };
            _gl.GetShader(shaderId, OpenGL.GL_INFO_LOG_LENGTH, infoLength);
            int bufSize = infoLength[0];
            if (bufSize > 0)
            {
                var infoLog = new StringBuilder(bufSize);
                _gl.GetShaderInfoLog(shaderId, 512, IntPtr.Zero, infoLog);
                Debug.WriteLine(infoLog.ToString());
            }
        }

        void GetProgramInfo()
        {
            int[] infoLength = new int[] { 0 };
            _gl.GetProgram(_id, OpenGL.GL_INFO_LOG_LENGTH, infoLength);
            int bufSize = infoLength[0];
            if (bufSize > 0)
            {
                var infoLog = new StringBuilder(bufSize);
                _gl.GetProgramInfoLog(_id, 512, IntPtr.Zero, infoLog);
                Debug.WriteLine(infoLog.ToString());
            }
        }
    }
}
