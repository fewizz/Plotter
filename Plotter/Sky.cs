using OpenGL;
using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    class Sky
    {
        uint program, vs, fs;

        public Sky()
        {
            program = Gl.CreateProgram();
            //vs = Gl.CreateShader(ShaderType.VertexShader);
            fs = Gl.CreateShader(ShaderType.FragmentShader);
            //Gl.AttachShader(program, vs);
            Gl.AttachShader(program, fs);
        }

        IExpression expr;

        IExpression Expression { get; }
    }
}
