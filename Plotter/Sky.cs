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
        public static Sky Instance = new Sky();
        bool inited = false;
        bool compiled = false;
        ShaderProgram program;
        Shader vs, fs;

        Dictionary<ColorComponent, IExpression> ColorExpressions;

        public void Init()
        {
            program = new ShaderProgram();
            vs = new Shader(ShaderType.VertexShader);
            fs = new Shader(ShaderType.FragmentShader);
            program.Attach(vs, fs);

            vs.Compile(
                    "#version 130\n" +
                    "out vec3 tr;\n" +

                    "void main() {\n" +
                    "   float z = 0.9999;\n" +
                    "   vec4[] verts = vec4[](vec4(-1, -1, z, 1), vec4(-1, 1, z, 1), vec4(1, -1, z, 1), vec4(1, 1, z, 1));\n" +
                    "   vec4 tr0 = gl_ProjectionMatrixInverse * verts[gl_VertexID];\n" +
                    "   mat4 mvWithoutRotation = gl_ModelViewMatrixInverse;\n" +
                    "   mvWithoutRotation[3]=vec4(0,0,0,1);\n" +
                    "   vec4 tr1 = mvWithoutRotation * tr0;\n" +
                    "   tr = tr1.xyz / tr1.w;\n" +
                    "   gl_Position = verts[gl_VertexID];\n" +
                    "}"
                );

            inited = true;
        }

        Sky()
        {
            ColorExpressions = new Dictionary<ColorComponent, IExpression>();
            foreach (var cc in ColorComponents.ARRAY)
            {
                ColorExpressions.Add(cc, null);
            }
        }

        void CompileAndLinkIfNeeded()
        {
            if (inited && !compiled && !ColorExpressions.ContainsValue(null))
            {
                fs.Compile(
                    "#version 130\n" +

                    GLSLNoise.SOURCE +

                    "in vec3 tr;" +
                    "uniform float t;"+

                    "vec2 to_sphere(vec3 v) {\n" +
                    "   return vec2(atan(v.z, v.x), asin(v.y));\n" +
                    "}\n" +

                    "void main() {\n" +
                    "   vec2 ab = to_sphere(normalize(tr));\n" +
                    "   float a = ab.x, b = ab.y;\n" +
                    "   gl_FragColor = vec4("
                    +   ColorExpressions[ColorComponent.Red].ToGLSLSource() + ", "
                    +   ColorExpressions[ColorComponent.Green].ToGLSLSource() + ", "
                    +   ColorExpressions[ColorComponent.Blue].ToGLSLSource() + ", "
                    +   ColorExpressions[ColorComponent.Alpha].ToGLSLSource() +
                    "   );\n" +
                    "}"
                );

                program.Link();

                compiled = true;
            }
        }

        public void Draw()
        {
            CompileAndLinkIfNeeded();
            program.Use();
            program.Uniform("t", (float)Program.TimeArg.Value);
            Gl.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            Gl.UseProgram(0);
        }

        public Status TryParseColorComponent(ColorComponent cc, string expr)
        {
            ColorExpressions[cc] = Parser.Parser.TryParse(expr, new object[] { "a", "b", Program.TimeArg });
            compiled = false;
            return (ColorExpressions[cc] != null).ToStatus();
        }
    }
}
