using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plotter
{
    public static class ShaderUtil
    {

        public static uint Attach(uint program, ShaderType type, string src)
        {
            uint shader = Gl.CreateShader(type);
            Gl.AttachShader(program, shader);
            Compile(shader, src);
            return shader;
        }

        public static Status Compile(uint name, string source)
        {
            Gl.ShaderSource(name, new string[] { source });
            Gl.CompileShader(name);
            Gl.GetShader(name, ShaderParameterName.CompileStatus, out int succ);
            if (succ == 0)
            {
                Gl.GetShader(name, ShaderParameterName.InfoLogLength, out int len);
                StringBuilder sb = new StringBuilder(len);
                Gl.GetShaderInfoLog(name, len, out int _, sb);
                Console.WriteLine(sb.ToString());
                Gl.DeleteShader(name);
                return Status.Error;
            }
            return Status.Ok;
        }

        public static Status Link(uint name)
        {
            Gl.LinkProgram(name);
            Gl.GetProgram(name, ProgramProperty.LinkStatus, out int succ);
            if (succ == 0)
            {
                Gl.GetProgram(name, ProgramProperty.InfoLogLength, out int len);
                StringBuilder sb = new StringBuilder(len);
                Gl.GetProgramInfoLog(name, len, out _, sb);
                Console.WriteLine(sb.ToString());
                return Status.Error;
            }
            return Status.Ok;
        }

        public static void Uniform(uint program, string name, int value)
        {
            Gl.Uniform1i(Gl.GetUniformLocation(program, name), 1, value);
        }

        public static void Uniform(uint program, string name, float value)
        {
            Gl.Uniform1f(Gl.GetUniformLocation(program, name), 1, value);
        }

        public static void Uniform(uint program, string name, Vertex3f value)
        {
            Gl.Uniform3f(Gl.GetUniformLocation(program, name), 1, value);
        }
    }
}
