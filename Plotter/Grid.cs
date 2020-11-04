using OpenGL;
using Parser;
using System;
using System.Collections.Generic;

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
                if (value != null)
                    Compile(vs, VertexShaderSrc);
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
                Compile(fs, FragmentShaderSrc);
            }
        }
        protected ShaderProgram program;
        protected Shader vs, fs;

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
                var l = new List<object>();
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
                var l = new List<object>();
                l.Add(Program.TimeArg);
                l.Add(arg0);
                l.Add(arg1);
                l.AddRange(AdditionalValueArgs);
                return l;
            }
        }

        public Grid() {
            arg0 = new Argument(Arg0Name);
            arg1 = new Argument(Arg1Name);
            program = new ShaderProgram();
            vs = new Shader(ShaderType.VertexShader);
            fs = new Shader(ShaderType.FragmentShader);
            program.Attach(vs, fs);
            ProgramLinkageStatus = Status.Error;
            ValueExpressionCompilationStatus = Status.Error;
            ColorExpressionCompilationStatus = Status.Error; 

            foreach (var cc in Enum.GetValues(typeof(ColorComponent)))
                ColorComponentsExpressions.Add((ColorComponent)cc, null);
        }

        protected Status Compile(Shader sh, string source)
        {
            if (Status.Error == sh.Compile(source)) return Status.Error;
            ProgramLinkageStatus = program.Link();
            return ProgramLinkageStatus;
        }


        public abstract Vertex3f CartesianCoord(decimal a0, decimal a1);

        abstract protected string VertexShaderSrc { get; }
        abstract protected string FragmentShaderSrc { get; }

        public virtual Status TryParseValueExpression(string expr)
        {
            ValueExpression = Parser.Parser.TryParse(expr, ValueArgs);
            return (ValueExpression != null).ToStatus();
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
            program.Dispose();
            vs.Dispose();
            fs.Dispose();
        }
    }
}
