using OpenGL;
using Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public abstract class Grid : IDisposable
    {
        protected static readonly string NOISE_GLSL_SRC;
        static Grid()
        {
            NOISE_GLSL_SRC = File.ReadAllText("../../common.glsl");
        }

        protected Argument arg0, arg1;
        private IExpression valueExpression;
        public IExpression ValueExpression {
            get { return valueExpression; }
            set {
                valueExpression = value;
                if(value != null)
                ValueExpressionCompilationStatus = Compile(vs, VertexShaderSrc);
            }
        }
        private Dictionary<ColorComponent, IExpression> colorComponentsExpressions = new Dictionary<ColorComponent, IExpression>();
        public Dictionary<ColorComponent, IExpression> ColorComponentsExpressions {
            get { return colorComponentsExpressions; }
            set {
                //colorComponentsExpressions.Clear();
                foreach(var cc in (ColorComponent[])Enum.GetValues(typeof(ColorComponent)))
                {
                    //colorComponentsExpressions.Add(key, value[key]);
                    this[cc] = value[cc];
                }
            }
        }
        public IExpression this[ColorComponent cc] {
            get { return ColorComponentsExpressions[cc]; }
            set
            {
                ColorComponentsExpressions[cc] = value;
                if (ColorComponentsExpressions.ContainsValue(null)) return;
                ColorExpressionCompilationStatus = Compile(fs, FragmentShaderSrc);
            }
        }
        protected uint program, vs, fs;

        public enum Status { Ok, Error }

        public Status ValueExpressionCompilationStatus { get; private set; }
        public Status ColorExpressionCompilationStatus { get; private set; }
        public Status ProgramLinkageStatus { get; private set; }

        public abstract string Arg0Name { get; }
        public abstract string Arg1Name { get; }
        public virtual string[] AdditionalValueArgs => new string[0];
        public virtual string[] AdditionalColorArgs => new string[0];

        public Grid() {
            arg0 = new Argument(Arg0Name);
            arg1 = new Argument(Arg1Name);
            program = Gl.CreateProgram();
            vs = Gl.CreateShader(ShaderType.VertexShader);
            fs = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.AttachShader(program, vs);
            Gl.AttachShader(program, fs);
            ValueExpressionCompilationStatus = Status.Error;
            ColorExpressionCompilationStatus = Status.Error; 

            foreach (var cc in Enum.GetValues(typeof(ColorComponent)))
                ColorComponentsExpressions.Add((ColorComponent)cc, null);
        }

        protected Status Compile(uint name, string source)
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
            Gl.LinkProgram(program);
            Gl.GetProgram(program, ProgramProperty.LinkStatus, out succ);
            if (succ == 0)
            {
                Gl.GetProgram(program, ProgramProperty.InfoLogLength, out int len);
                StringBuilder sb = new StringBuilder(len);
                Gl.GetProgramInfoLog(program, len, out _, sb);
                Console.WriteLine(sb.ToString());
                ProgramLinkageStatus = Status.Error;
            }
            else ProgramLinkageStatus = Status.Ok;
            return Status.Ok;
        }

        public abstract Vertex3f CartesianCoord(decimal a0, decimal a1);

        abstract protected string VertexShaderSrc { get; }
        abstract protected string FragmentShaderSrc { get; }

        public void TryParseValueExpression(string expr)
        {
            try
            {
                valueExpression = null;
                ValueExpression = Parser.Parser.Parse(expr, arg0, arg1, Program.TimeArg, AdditionalValueArgs);
            } catch { }
        }

        public void TryParseColorComponent(ColorComponent cc, string expr)
        {
            try
            {
                this[cc] = Parser.Parser.Parse(expr, arg0, arg1, Program.TimeArg, AdditionalColorArgs);
            }
            catch { }
        }

        public void Draw(Camera c)
        {
            if (ProgramLinkageStatus != Status.Ok) return;

            Gl.UseProgram(program);
            Gl.Uniform1f(Gl.GetUniformLocation(program, "t"), 1, (float)Program.TimeArg.Value);

            Draw0(c);

            Gl.UseProgram(0);
        }

        abstract protected void Draw0(Camera c);

        public void Dispose()
        {
            Gl.DeleteShader(vs);
            Gl.DeleteShader(fs);
            Gl.DeleteProgram(program);
        }
    }
}
