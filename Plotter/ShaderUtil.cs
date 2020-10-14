using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    static class ShaderUtil
    {
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

    }
}
