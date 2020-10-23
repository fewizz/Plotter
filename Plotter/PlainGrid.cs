using OpenGL;
using Parser;
using System.Collections.Generic;

namespace Plotter
{
    class PlainGrid : Grid
    {
        public float Step;
        public int Size => (int)(Program.R * 2 / Step);

        uint values_fbo, values_texture, values_program, values_vs, values_fs;

        public override void Dispose()
        {
            Gl.DeleteFramebuffers(new uint[] { values_fbo });
            Gl.DeleteShader(values_vs);
            Gl.DeleteShader(values_fs);
            Gl.DeleteProgram(values_program);
            Gl.DeleteTextures(new uint[] { values_texture });
        }

        public PlainGrid()
        {
            values_fbo = Gl.GenFramebuffer();

            values_program = Gl.CreateProgram();
            values_fs = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.AttachShader(values_program, values_fs);

            values_vs = ShaderUtil.Attach(values_program, ShaderType.VertexShader,
                "#version 130\n"+
                "void main() {\n"+
                "   vec2[] verts = vec2[](vec2(-1,-1), vec2(-1,1), vec2(1,-1), vec2(1,1));\n"+
                "   gl_Position = vec4(verts[gl_VertexID], 0, 1);\n"+
                "}"
            );
        }

        public Status TryParseStep(string expression)
        {
            IExpression e = Parser.Parser.TryParse(expression);
            if (e == null || e.Value <= 0) return Status.Error;
            Step = (float)e.Value;

            if (values_texture != 0) Gl.DeleteTextures(new uint[] { values_texture });
            values_texture = Gl.GenTexture();
            Gl.BindTexture(TextureTarget.Texture2d, values_texture);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMinFilter.Linear);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.ClampToEdge);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.ClampToEdge);
            Gl.TexStorage2D(
                TextureTarget.Texture2d,
                1,
                InternalFormat.R32f,
                Size,
                Size
            );

            return Status.Ok;
        }

        public override Status TryParseValueExpression(string expr)
        {
            Status status = base.TryParseValueExpression(expr);
            if (status == Status.Error) return Status.Error;

            ShaderUtil.Compile(values_fs,
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
            ShaderUtil.Link(values_program);

            return Status.Ok;
        }

        public override IEnumerable<object> AdditionalColorArgs => new string[] { "y" };

        protected override string FragmentShaderSrc =>
            "#version 130\n"+

            "uniform float Time;\n"+
            "uniform vec3 CameraPosition;\n"+
            "in vec3 Position, Normal;\n"+

            GLSLNoise.SOURCE +

            "void main(void) {\n"+
            "   float x = Position.x, y = Position.y, z = Position.z;\n" +
            "   gl_FragColor = vec4(\n"+
                    ColorComponentsExpressions[ColorComponent.Red].ToGLSLSource()+",\n"+
                    ColorComponentsExpressions[ColorComponent.Green].ToGLSLSource()+",\n"+
                    ColorComponentsExpressions[ColorComponent.Blue].ToGLSLSource()+",\n"+
                    ColorComponentsExpressions[ColorComponent.Alpha].ToGLSLSource()+"\n"+
            "   ) * normalize(Normal).y;\n"+
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
            "out vec3 Position, Normal;\n" +

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
            "   Normal = -cross(vecX - Position, vecZ - Position);\n" +
            "}";

        override protected void Draw0()
        {
            Gl.Enable(EnableCap.Texture2d);

            Gl.BindTexture(TextureTarget.Texture2d, values_texture);

            Gl.BindFramebuffer(FramebufferTarget.Framebuffer, values_fbo);
            Gl.FramebufferTexture(
                FramebufferTarget.Framebuffer,
                FramebufferAttachment.ColorAttachment0,
                values_texture,
                0
            );
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.UseProgram(values_program);
            ShaderUtil.Uniform(values_program, "CameraPosition", Camera.Position);
            ShaderUtil.Uniform(values_program, "Time", (float)Program.TimeArg.Value);
            ShaderUtil.Uniform(values_program, "Step", Step);
            ShaderUtil.Uniform(values_program, "Size", Size);
            Gl.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            Gl.UseProgram(0);
            Gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            Gl.UseProgram(program);
            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.BindTexture(TextureTarget.Texture2d, values_texture);
            ShaderUtil.Uniform(program, "Time", (float)Program.TimeArg.Value);
            ShaderUtil.Uniform(program, "Size", Size);
            ShaderUtil.Uniform(program, "Step", Step);
            ShaderUtil.Uniform(program, "CameraPosition", Camera.Position);
            ShaderUtil.Uniform(program, "ValuesTexture", 0);
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
