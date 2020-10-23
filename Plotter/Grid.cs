using OpenGL;
using Parser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public abstract class Grid : IDisposable
    {
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
        public string ValueExpressionString
        {
            set
            {
                TryParseValueExpression(value);
            }
        }

        private Dictionary<ColorComponent, IExpression> colorComponentsExpressions = new Dictionary<ColorComponent, IExpression>();
        public Dictionary<ColorComponent, IExpression> ColorComponentsExpressions {
            get { return colorComponentsExpressions; }
            set {
                foreach(var cc in (ColorComponent[])Enum.GetValues(typeof(ColorComponent)))
                {
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

        public Status ValueExpressionCompilationStatus { get; private set; }
        public Status ColorExpressionCompilationStatus { get; private set; }
        public Status ProgramLinkageStatus { get; private set; }

        public abstract string Arg0Name { get; }
        public abstract string Arg1Name { get; }
        public virtual IEnumerable<object> AdditionalValueArgs => new string[0];
        public virtual IEnumerable<object> AdditionalColorArgs => new string[0];
        public virtual IEnumerable<object> ColorArgs
        {
            get
            {
                List<object> l = new List<object>();
                l.Add(Program.TimeArg);
                l.Add(Arg0Name);
                l.Add(Arg1Name);
                l.AddRange(AdditionalColorArgs);
                return l;
            }
        }

        public virtual IEnumerable<object> ValueArgs
        {
            get
            {
                List<object> l = new List<object>();
                l.Add(Program.TimeArg);
                l.Add(Arg0Name);
                l.Add(Arg1Name);
                l.AddRange(AdditionalValueArgs);
                return l;
            }
        }

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
            if (Status.Error == ShaderUtil.Compile(name, source)) return Status.Error;
            ProgramLinkageStatus = ShaderUtil.Link(program);
            return ProgramLinkageStatus;
        }

        public abstract Vertex3f CartesianCoord(decimal a0, decimal a1);

        abstract protected string VertexShaderSrc { get; }
        abstract protected string FragmentShaderSrc { get; }

        public virtual Status TryParseValueExpression(string expr)
        {
            return ((ValueExpression = Parser.Parser.TryParse(expr, ValueArgs)) != null).ToStatus();
        }

        public Status TryParseColorComponent(ColorComponent cc, string expr)
        {
            return ((this[cc] = Parser.Parser.TryParse(expr, ColorArgs)) != null).ToStatus();
        }

        public void Draw()
        {
            if (ProgramLinkageStatus != Status.Ok) return;

            Draw0();
        }

        abstract protected void Draw0();

        public virtual void Dispose()
        {
            Gl.DeleteShader(vs);
            Gl.DeleteShader(fs);
            Gl.DeleteProgram(program);
        }
    }
}
