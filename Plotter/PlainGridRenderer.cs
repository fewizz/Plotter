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
        private static readonly float step = 0.25F;
        private static readonly int size = (int)(Program.R * 2 / step);

        public PlainGridRenderer()
        {
            //this.size = size;
            //this.step = step;
        }

        public override string[] AdditionalColor() => new string[] { "y" };

        protected override string FragmentShaderSrc(Dictionary<ColorComponent, IExpression> exprs)
        {
            return
            "#version 130\r"+

            "uniform float t;\n"+
            "uniform vec3 u_cam;\n"+
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
            "    gl_FragColor.a = -distance(u_cam, vec) + "+Program.R.ToString()+";\n"+
            "}\n";
        }

        protected override string VertexShaderSrc(IExpression ex)
        {
            return 
            "#version 130\r"+

            "uniform int u_size = "+size.ToString()+";\n"+
            "uniform float u_step = "+step.ToString()+";\n"+
            "uniform float t;\n"+
            "uniform vec3 u_cam;\n" +
            "out vec3 vec, normal;\n" + //5

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

            "   vec2 pos = vec2((-u_size / 2.0 + xoffset)*u_step, (-u_size / 2.0 + zoffset)*u_step);\n"+
            "   pos += u_cam.xz;"+
            "   vec = vec3(pos.x, y(pos.x, pos.y), pos.y);\n"+ //18
            "   gl_Position = gl_ModelViewProjectionMatrix * vec4(vec, 1);\n"+
            "   float offset = u_step / 10;\n"+ //20
            "   vec3 vecX = vec3(pos.x+offset, y(pos.x+offset, pos.y), pos.y);\n" +
            "   vec3 vecZ = vec3(pos.x, y(pos.x, pos.y+offset), pos.y+offset);\n" +
            "   normal = cross(vecX - vec, vecZ - vec) * (-1);\n"+
            "}";
        }

        override protected void Draw0(Camera c)
        {
            //int size = (int)(Program.R * 2 / step);
            Gl.Uniform3f(Gl.GetUniformLocation(program, "u_cam"), 1, c.Position);
            //Gl.Uniform1i(Gl.GetUniformLocation(program, "u_size"), 1, size);
            //Gl.Uniform1f(Gl.GetUniformLocation(program, "u_step"), 1, step);

            Gl.DrawArrays(PrimitiveType.TriangleStrip, 0, (size * 2 + 2) * size);
        }

        public override string Arg0() => "x";

        public override string Arg1() => "z";
    }
}
