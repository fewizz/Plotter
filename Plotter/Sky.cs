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
        uint program, vs, fs;

        Dictionary<ColorComponent, IExpression> ColorExpressions;

        Sky()
        {
            ColorExpressions = new Dictionary<ColorComponent, IExpression>();
            foreach (var cc in ColorComponents.ARRAY)
            {
                ColorExpressions.Add(cc, null);
            }
        }

        void init()
        {
            if (!inited)
            {
                program = Gl.CreateProgram();
                vs = Gl.CreateShader(ShaderType.VertexShader);
                fs = Gl.CreateShader(ShaderType.FragmentShader);
                //Gl.AttachShader(program, vs);
                Gl.AttachShader(program, vs);
                Gl.AttachShader(program, fs);
                inited = true;
            }
            if (!compiled && !ColorExpressions.ContainsValue(null))
            {
                ShaderUtil.Compile(vs,
                    "#version 130\n"+
                    "out vec4 tr;\n"+

                    "void main() {\n" +
                    "   float z = 0.9999;\n"+
                    "   vec4[] verts = vec4[](vec4(-1, -1, z, 1), vec4(-1, 1, z, 1), vec4(1, -1, z, 1), vec4(1, 1, z, 1));\n" +
                    "   tr = gl_ModelViewProjectionMatrixInverse * verts[gl_VertexID];\n" +
                    "   gl_Position = verts[gl_VertexID];\n"+
                    "}"
                );

                ShaderUtil.Compile(fs,
                        "#version 120\n" +
                        "in vec4 tr;" +

                        "vec2 to_sphere(vec3 v) {\n" +
                        "   return vec2(atan(v.z, v.x), asin(v.y));\n" +
                        "}\n" +

                        "void main() {\n" +
                        "   vec2 ab = to_sphere(normalize(tr.xyz));\n" +
                        "   float a = ab.x, b = ab.y;\n" +
                        //"   vec4 coord = gl_ModelViewMatrixInverse * gl_FragCoord;\n" +
                        "   gl_FragColor = vec4("
                        + ColorExpressions[ColorComponent.Red].ToGLSL() + ", "
                        + ColorExpressions[ColorComponent.Green].ToGLSL() + ","
                        + ColorExpressions[ColorComponent.Blue].ToGLSL() + ","
                        + ColorExpressions[ColorComponent.Alpha].ToGLSL() +
                        ");\n" +
                        "   //gl_FragDepth = -10;\n" +
                        "}"
                    );
                ShaderUtil.Link(program);
                compiled = true;
            }
        }

        public void Draw()
        {
            init();
            Gl.UseProgram(program);

            var e = Gl.GetError();
            //Gl.Rect(-1, -1, 1, 1);
            Gl.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
            Gl.UseProgram(0);
        }

        //IExpression expr;
        //IExpression Expression { get; }

        public Status TryParseColorComponent(ColorComponent cc, string expr)
        {
            ColorExpressions[cc] = Parser.Parser.TryParse(expr, new string[] { "a", "b"});
            compiled = false;
            return (ColorExpressions[cc] != null).ToStatus();
        }
    }
}
