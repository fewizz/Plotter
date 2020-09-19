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
    class Grid
    {
        Param2Expression expr;
        uint program;
        int size = 0;
        float step = 0;
        //int vertexShader;
        //int fragmentShader;
        //decimal[,] values;
        //decimal step;

        /*decimal XFor(int x)
        {
            return step * (x - values.GetLength(0) / 2M);
        }

        decimal YFor(int y)
        {
            return step * (y - values.GetLength(1) / 2M);
        }*/

        public Grid(int size, float step)
        {
            this.size = size;
            this.step = step;
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

        public void Update(Param2Expression expr)
        {
            string vss = "";
            vss += "#version 130\n";

            vss += "uniform int u_size;\n";
            vss += "uniform float u_step;\n";

            vss += "float y(float x, float z) {\n";
            vss += "   return "+expr.expr.ToGLSL()+";\n";
            vss += "}\n";

            vss += "void main(void) {\n";
            vss += "   int per_column = u_size*2 + 2;\n";
            vss += "   int column = gl_VertexID / per_column;\n";
            vss += "   int vert = gl_VertexID % per_column;\n"; //10

            vss += "   int down = column % 2;\n";

            vss += "   int xoffset = column + vert % 2;\n";
            vss += "   int zoffset = 0;\n";
            vss += "   if(down == 1) zoffset = ((per_column - 1) / 2) - (vert / 2);\n";
            vss += "   else zoffset = (vert / 2);\n"; //15

            vss += "   float x = (-u_size / 2.0 + xoffset)*u_step, z = (-u_size / 2.0 + zoffset)*u_step;\n";
            vss += "   vec3 vec = vec3(x, y(x, z), z);\n"; //17
            vss += "   gl_Position = gl_ModelViewProjectionMatrix * vec4(vec, 1);\n";
            vss += "   float offset = u_step / 10;\n"; //19
            vss += "   vec3 vecX = vec3(x+offset, y(x+offset, z), z);\n";
            vss += "   vec3 vecZ = vec3(x, y(x, z+offset), z+offset);\n";
            vss += "   vec3 normal = normalize(cross(vecZ - vec, vecX - vec));\n";
            vss += "   float rad = 1.5;\n";
            vss += "   gl_FrontColor = vec4(vec.y*rad, rad + vec.y*sign(-vec.y), -vec.y*rad, 1) * normal.y;\n";
            vss += "}";

            uint vs = Shader(ShaderType.VertexShader, vss);
            if (vs == 0) return;

            uint p = Gl.CreateProgram();
            Gl.AttachShader(p, vs);
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
                return;
            }

            Gl.DeleteProgram(program);
            program = p;
            /*this.expr = expr;
            if (expr == null) return;

            values = new decimal[(int)(s.Width/step), (int)(s.Height/step)];
            this.step = step;

            for(int x = 0; x < values.GetLength(0); x++) 
                for (int y = 0; y < values.GetLength(1); y++)
                    values[x, y] = expr.Value(XFor(x), YFor(y));*/
        }

        public void Draw()
        {
            if (program == 0)
                return;
            Gl.UseProgram(program);
            Gl.Uniform1i(Gl.GetUniformLocation(program, "u_size"), 1, size);
            Gl.Uniform1f(Gl.GetUniformLocation(program, "u_step"), 1, step);

            Gl.DrawArrays(PrimitiveType.TriangleStrip, 0, (size * 2 + 2) * size);
            Gl.UseProgram(0);
            /*if (values == null) return;
            bool begin = false;
            //Gl.Begin(PrimitiveType.TriangleStrip);

            for (int x = 0; x < values.GetLength(0) - 1; x++)
            {
                Vertex4f color = new Vertex4f(0, 0, 0, 1);

                Action<int, int> vert = (int x0, int y0) => {
                    decimal v = values[x0, y0];
                    color.y = (float)v;
                    color.z = (float)(1 - v);
                    if (v == decimal.MaxValue) color.w = 0;
                    else color.w = 1F;
                    if (v == decimal.MaxValue)
                    {
                        if (begin)
                        {
                            Gl.End();
                            begin = false;
                        }
                    }
                    else
                    {
                        if (!begin) {
                            Gl.Begin(PrimitiveType.TriangleStrip);
                            begin = true;
                        }
                        Gl.Color3((float[])color);
                        Gl.Vertex3(
                            (float)XFor(x0),
                            (float)v,
                            (float)YFor(y0)
                        );
                    }
                };

                for (int y = 0; y < values.GetLength(1); y++)
                {
                    //color = new Vertex4f(0, 0, 1f, 1F);
                    vert(x, y);
                    //color = new Vertex4f(1, 0, 0F, 1F);
                    vert(x + 1, y);
                }
                vert(x + 1, values.GetLength(1) - 1);
                vert(x + 1, 0);
            }

            if(begin)
                Gl.End();*/
        }
    }
}
