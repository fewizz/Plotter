using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenGL;
using Parser;

namespace Plotter
{
    public abstract class GridRenderer
    {
        protected static string commonShaderSrc;
        static GridRenderer()
        {
            commonShaderSrc = File.ReadAllText("../../common.glsl");
        }

        protected uint program, vs, fs;

        public enum CompilationStatus {
            Ok, Error
        }

        public CompilationStatus ValueExpressionCompilationStatus { get; private set; }
        public CompilationStatus ColorExpressionCompilationStatus { get; private set; }

        public CompilationStatus ProgramLinkageStatus { get; private set; }

        public GridRenderer()
        {
            program = Gl.CreateProgram();
            vs = Gl.CreateShader(ShaderType.VertexShader);
            fs = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.AttachShader(program, vs);
            Gl.AttachShader(program, fs);
        }

        protected bool Compile(uint name, string source)
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
                return false;
            }

            Gl.LinkProgram(program);

            Gl.GetProgram(program, ProgramProperty.LinkStatus, out succ);
            if (succ == 0)
            {
                Gl.GetProgram(program, ProgramProperty.InfoLogLength, out int len);

                StringBuilder sb = new StringBuilder(len);
                Gl.GetProgramInfoLog(program, len, out _, sb);
                Console.WriteLine(sb.ToString());
                ProgramLinkageStatus = CompilationStatus.Error;
                return false;
            }
            ProgramLinkageStatus = CompilationStatus.Ok;

            return true;
        }

        abstract protected string VertexShaderSrc(IExpression ex);
        abstract protected string FragmentShaderSrc(Dictionary<ColorComponent, IExpression> exprs);

        public void UpdateValueExpression(IExpression expr)
        {
            ValueExpressionCompilationStatus =
                Compile(vs, VertexShaderSrc(expr)) ? CompilationStatus.Ok : CompilationStatus.Error;
        }

        public void UpdateColorComponentsExpressions(Dictionary<ColorComponent, IExpression> exprs)
        {
            ColorExpressionCompilationStatus =
                Compile(fs, FragmentShaderSrc(exprs)) ? CompilationStatus.Ok : CompilationStatus.Error;
        }

        public void Draw()
        {
            if (ProgramLinkageStatus != CompilationStatus.Ok) return;

            Gl.UseProgram(program);
            Gl.Uniform1f(Gl.GetUniformLocation(program, "t"), 1, (float)Program.TimeArg.Value);

            Draw0();

            Gl.UseProgram(0);
        }

        abstract protected void Draw0();
    }
}
