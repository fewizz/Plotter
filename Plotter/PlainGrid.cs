using OpenGL;
using Parser;
using System.Collections.Generic;

namespace Plotter
{
    class PlainGrid : Grid
    {
        public float Step;
        public int Size => (int)(Program.R * 2 / Step);

        Framebuffer valuesFramebuffer;
        Texture valuesTexture;
        ShaderProgram valuesProgram;
        Shader valuesVs, valuesFs;

        public override void Dispose()
        {
            valuesFramebuffer.Dispose();
            valuesTexture?.Dispose();
            valuesProgram.Dispose();
            valuesVs.Dispose();
            valuesFs.Dispose();
        }

        public PlainGrid()
        {
            valuesFramebuffer = new Framebuffer();

            valuesProgram = new ShaderProgram();
            valuesFs = new Shader(ShaderType.FragmentShader);

            valuesVs = new Shader(ShaderType.VertexShader);
            valuesVs.Compile(
                "#version 130\n"+
                "void main() {\n"+
                "   vec2[] verts = vec2[](vec2(-1,-1), vec2(-1,1), vec2(1,-1), vec2(1,1));\n"+
                "   gl_Position = vec4(verts[gl_VertexID], 0, 1);\n"+
                "}"
            );

            valuesProgram.Attach(valuesVs, valuesFs);
        }

        public System.Exception TryParseStep(string expression)
        {
            IExpression e = Parser.Parser.TryParse(expression, out System.Exception m);
            if (e == null || e.Value <= 0) return m != null ? m : new System.Exception("Шаг не может быть отрицательным или равным нулю");
            Step = (float)e.Value;

            valuesTexture?.Dispose();
            valuesTexture = new Texture();

            valuesTexture.Wrap(TextureWrapMode.ClampToEdge);
            valuesTexture.Filter(TextureMinFilter.Linear);
            valuesTexture.Storage(InternalFormat.R32f, Size, Size);

            return null;
        }

        public override System.Exception TryParseValueExpression(string expr)
        {
            var ex = base.TryParseValueExpression(expr);
            if (ex != null) return ex;

            valuesFs.Compile(
                "#version 130\n" +

                GLSLNoise.SOURCE+

                "uniform vec3 CameraPosition;\n"+
                "uniform float Time;\n"+
                "uniform float Step;\n"+
                "uniform int Size;\n"+

                "void main() {\n" +
                "   vec2 offset = (gl_FragCoord.xy - vec2(Size)/2)*Step + CameraPosition.xz;\n"+
                "   float t = Time;\n"+
                "   float x = offset.x, z = offset.y;\n"+
                "   float y = "+ValueExpression.ToGLSLSource()+";\n"+
                "   gl_FragColor = vec4(y, 0, 0, 1);\n"+
                "}"
            );
            valuesProgram.Link();

            return null;
        }

        public override IEnumerable<object> AdditionalColorArgs
            => new string[] { "y", "normal_x", "normal_y", "normal_z" };

        protected override string FragmentShaderSrc =>
            "#version 130\n"+

            "uniform float Time;\n"+
            "uniform vec3 CameraPosition;\n"+
            "in vec3 Position, NormalDirection;\n"+

            GLSLNoise.SOURCE +

            "void main(void) {\n"+
            "   vec3 normal = normalize(NormalDirection);"+
            "   float x = Position.x, y = Position.y, z = Position.z, t = Time;\n" +
            "   float normal_x = normal.x, normal_y = normal.y, normal_z = normal.z;\n" +
            "   gl_FragColor = vec4(\n"+
                    ColorComponentsExpressions[ColorComponent.Red].ToGLSLSource()+",\n"+
                    ColorComponentsExpressions[ColorComponent.Green].ToGLSLSource()+",\n"+
                    ColorComponentsExpressions[ColorComponent.Blue].ToGLSLSource()+",\n"+
                    ColorComponentsExpressions[ColorComponent.Alpha].ToGLSLSource()+"\n"+
            "   );\n"+
            "   gl_FragColor.a = -distance(CameraPosition, Position) + " + Program.R.ToString()+";\n"+
            "}\n";

        protected override string VertexShaderSrc =>
            "#version 130\n"+

            GLSLNoise.SOURCE +

            "uniform int Size;\n" +
            "uniform float Step;\n"+
            "uniform float Time;\n"+
            "uniform vec3 CameraPosition;\n" +
            "uniform sampler2D ValuesTexture;\n"+
            "out vec3 Position, NormalDirection;\n" +

            "float y(vec2 pos) {\n" +
            "   return texture(ValuesTexture, pos/Size).r;\n" +
            "}\n"+

            "void main(void) {\n" +
            "   int per_column = Size*2 + 2;\n"+
            "   int column = gl_VertexID / per_column;\n"+
            "   int vert = gl_VertexID % per_column;\n"+
            "   int down = column % 2;\n"+
            "   vec2 offset = vec2(column + vert % 2, 0);\n"+
            "   if(down == 1) offset.y = ((per_column - 1) / 2) - (vert / 2);\n"+
            "   else offset.y = (vert / 2);\n"+

            "   float value = y(offset)\n;"+
            "   vec2 position_xz = vec2((offset - (Size / 2.0) )*Step);\n" +
            "   position_xz += CameraPosition.xz;\n" +
            "   Position = vec3(position_xz.x, value, position_xz.y);\n" +
            "   gl_Position = gl_ModelViewProjectionMatrix * vec4(Position, 1);\n"+
            "   float off = Step / 10;\n"+
            "   vec3 vecX = vec3(Position.x+off, y(offset + vec2(off, 0)), Position.z);\n" +
            "   vec3 vecZ = vec3(Position.x, y(offset + vec2(0, off)), Position.z+off);\n" +
            "   NormalDirection = cross(vecZ - Position, vecX - Position);\n" +
            "}";

        override protected void Draw0()
        {
            Gl.Enable(EnableCap.Texture2d);

            valuesFramebuffer.Bind();
            valuesFramebuffer.Texture(FramebufferAttachment.ColorAttachment0, valuesTexture);
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            valuesProgram.Use();
            valuesProgram.Uniform("CameraPosition", Camera.Position);
            valuesProgram.Uniform("Time", (float)Program.TimeArg.Value);
            valuesProgram.Uniform("Step", Step);
            valuesProgram.Uniform("Size", Size);
            Gl.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            Gl.UseProgram(0);
            Gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            program.Use();
            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.BindTexture(TextureTarget.Texture2d, valuesTexture.Name);
            program.Uniform("Time", (float)Program.TimeArg.Value);
            program.Uniform("Size", Size);
            program.Uniform("Step", Step);
            program.Uniform("CameraPosition", Camera.Position);
            program.Uniform("ValuesTexture", 0);
            Gl.DrawArrays(PrimitiveType.TriangleStrip, 0, (Size * 2 + 2) * Size);

            Gl.UseProgram(0);
        }

        public override string Arg0Name => "x";
        public override string Arg1Name => "z";

        public override Vertex3f CartesianCoord(decimal a0, decimal a1)
        {
            arg0.Value = a0;
            arg1.Value = a1;
            return new Vertex3f((float)a0, (float)ValueExpression.Value, (float)a1);
        }
    }
}
