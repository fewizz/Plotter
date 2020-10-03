using OpenGL;
using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    class PlainGridRenderer : GridRenderer
    {
        private readonly int size = 0;
        private readonly float step = 0;

        public PlainGridRenderer(int size, float step)
        {
            this.size = size;
            this.step = step;
        }

        protected override string FragmentShaderSrc(Dictionary<ColorComponent, IExpression> exprs)
        {
            return
            "#version 130\r"+

            "uniform float t;\n"+
            "in vec3 vec, normal;\n"+

            commonShaderSrc+

            "float r(float x, float y, float z) {\n"+
            "   return " + exprs[ColorComponent.Red].ToGLSL() + ";\n"+
            "}\n"+
            "float g(float x, float y, float z) {\n"+
            "   return " + exprs[ColorComponent.Green].ToGLSL() + ";\n"+
            "}\n"+
            "float b(float x, float y, float z) {\n"+
            "   return " + exprs[ColorComponent.Blue].ToGLSL() + ";\n"+
            "}\n"+
            "float a(float x, float y, float z) {\n"+
            "   return " + exprs[ColorComponent.Alpha].ToGLSL() + ";\n"+
            "}\n"+

            "void main(void) {\n"+
            "    gl_FragColor = vec4(r(vec.x, vec.y, vec.z), g(vec.x, vec.y, vec.z), b(vec.x, vec.y, vec.z), 1) * normalize(normal).y;\n"+
            "    gl_FragColor.a = a(vec.x, vec.y, vec.z);\n"+
            "}\n";
        }

        protected override string VertexShaderSrc(IExpression ex)
        {
            return 
            "#version 130\r"+

            "uniform int u_size;\n"+
            "uniform float u_step;\n"+
            "uniform float t;\n"+
            "out vec3 vec, normal;\n"+ //5

            commonShaderSrc+

            "float y(float x, float z) {\n"+
            "   return " + ex.ToGLSL() + ";\n"+
            "}\n"+

            "void main(void) {\n"+
            "   int per_column = u_size*2 + 2;\n"+
            "   int column = gl_VertexID / per_column;\n"+
            "   int vert = gl_VertexID % per_column;\n"+ //11

            "   int down = column % 2;\n"+

            "   int xoffset = column + vert % 2;\n"+
            "   int zoffset = 0;\n"+
            "   if(down == 1) zoffset = ((per_column - 1) / 2) - (vert / 2);\n"+
            "   else zoffset = (vert / 2);\n"+ //16

            "   float x = (-u_size / 2.0 + xoffset)*u_step, z = (-u_size / 2.0 + zoffset)*u_step;\n"+
            "   vec = vec3(x, y(x, z), z);\n"+ //18
            "   gl_Position = gl_ModelViewProjectionMatrix * vec4(vec, 1);\n"+
            "   float offset = u_step / 10;\n"+ //20
            "   vec3 vecX = vec3(x+offset, y(x+offset, z), z);\n"+
            "   vec3 vecZ = vec3(x, y(x, z+offset), z+offset);\n"+
            "   normal = cross(vecX - vec, vecZ - vec) * (-1);\n"+
            "}";
        }

        override public void Draw()
        {
            if (ProgramLinkageStatus != CompilationStatus.Ok) return;

            Gl.UseProgram(program);
            Gl.Uniform1i(Gl.GetUniformLocation(program, "u_size"), 1, size);
            Gl.Uniform1f(Gl.GetUniformLocation(program, "u_step"), 1, step);
            Gl.Uniform1f(Gl.GetUniformLocation(program, "t"), 1, (float)Program.TimeArg.Value);

            Gl.DrawArrays(PrimitiveType.TriangleStrip, 0, (size * 2 + 2) * size);
            Gl.UseProgram(0);
        }
    }
}
