using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using OpenGL;
using Solver;

namespace Plotter
{
    public class Grid
    {
        public DateTime Time { get; set; }
        public Arg TimeArg { get; set; }

        //Param2Expression expr;
        uint program;
        int size = 0;
        float step = 0;


        public Grid(int size, float step)
        {
            this.size = size;
            this.step = step;
            TimeArg = new Arg("t", 0);
        }

        private uint Shader(ShaderType st, string source)
        {
            uint name = Gl.CreateShader(st);

            Gl.ShaderSource(name, new string[] { source });
            Gl.CompileShader(name);

            int succ;
            Gl.GetShader(name, ShaderParameterName.CompileStatus, out succ);
            if (succ == 0)
            {
                int len;
                Gl.GetShader(name, ShaderParameterName.InfoLogLength, out len);

                StringBuilder sb = new StringBuilder(len);
                int newLen;
                Gl.GetShaderInfoLog(name, len, out newLen, sb);
                Console.WriteLine(sb.ToString());
                Gl.DeleteShader(name);
                return 0;
            }

            return name;
        }

        public bool Update(string exprText)
        {
            Param2Expression expr = null;

            try
            {
                expr = new Param2Expression(
                    (Arg x, Arg y) =>
                        Parser.Parse(
                            exprText,
                            new Argument { Arg = x },
                            new Argument { Arg = y },
                            new Argument { Arg = TimeArg }
                        )
                );
            }
            catch { return false; }

            string vss = "";
            vss += "#version 130\n";

            vss += "uniform int u_size;\n";
            vss += "uniform float u_step;\n";
            vss += "uniform float t;\n";
            vss += "out vec3 vec, normal;\n";

            vss += "float y(float x, float z) {\n";
            vss += "   return "+expr.expr.ToGLSL()+";\n";
            vss += "}\n";

            vss += "void main(void) {\n";
            vss += "   int per_column = u_size*2 + 2;\n";
            vss += "   int column = gl_VertexID / per_column;\n";
            vss += "   int vert = gl_VertexID % per_column;\n"; //11

            vss += "   int down = column % 2;\n";

            vss += "   int xoffset = column + vert % 2;\n";
            vss += "   int zoffset = 0;\n";
            vss += "   if(down == 1) zoffset = ((per_column - 1) / 2) - (vert / 2);\n";
            vss += "   else zoffset = (vert / 2);\n"; //16

            vss += "   float x = (-u_size / 2.0 + xoffset)*u_step, z = (-u_size / 2.0 + zoffset)*u_step;\n";
            vss += "   vec = vec3(x, y(x, z), z);\n"; //18
            vss += "   gl_Position = gl_ModelViewProjectionMatrix * vec4(vec, 1);\n";
            vss += "   float offset = u_step / 10;\n"; //20
            vss += "   vec3 vecX = vec3(x+offset, y(x+offset, z), z);\n";
            vss += "   vec3 vecZ = vec3(x, y(x, z+offset), z+offset);\n";
            vss += "   normal = cross(vecZ - vec, vecX - vec);\n";
            vss += "}";

            uint vs = Shader(ShaderType.VertexShader, vss);
            if (vs == 0) return false;

            string fss = "";
            fss += "#version 130\n";

            fss += "uniform float t;\n";
            fss += "in vec3 vec, normal;\n";

            fss += "void main(void) {\n";
            fss += "    float rad = 1.5;\n";
            fss += "    gl_FragColor = vec4(vec.y*rad, rad + vec.y*sign(-vec.y), -vec.y*rad, 1) * normalize(normal).y;\n";
            fss += "}\n";

            uint fs = Shader(ShaderType.FragmentShader, fss);
            if (fs == 0) return false;

            uint p = Gl.CreateProgram();
            Gl.AttachShader(p, vs);
            Gl.AttachShader(p, fs);
            Gl.LinkProgram(p);

            int succ;
            Gl.GetProgram(p, ProgramProperty.LinkStatus, out succ);
            if (succ == 0)
            {
                int len;
                Gl.GetProgram(p, ProgramProperty.InfoLogLength, out len);

                StringBuilder sb = new StringBuilder(len);
                int newLen;
                Gl.GetShaderInfoLog(p, len, out newLen, sb);
                Console.WriteLine(sb.ToString());
                Gl.DeleteProgram(p);
                return false;
            }

            Gl.DeleteProgram(program);
            program = p;
            Time = DateTime.Now;

            return true;
        }

        public void Draw()
        {
            if (program == 0)
                return;
            Gl.UseProgram(program);
            Gl.Uniform1i(Gl.GetUniformLocation(program, "u_size"), 1, size);
            Gl.Uniform1f(Gl.GetUniformLocation(program, "u_step"), 1, step);
            double t = (DateTime.Now - Time).TotalMilliseconds / 1000D;
            Gl.Uniform1f(Gl.GetUniformLocation(program, "t"), 1, (float)t);

            Gl.DrawArrays(PrimitiveType.TriangleStrip, 0, (size * 2 + 2) * size);
            Gl.UseProgram(0);
        }
    }
}
