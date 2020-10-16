using OpenGL;
using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    class PlainGrid : Grid
    {
        public float step = 0.25F;
        public int Size => (int)(Program.R * 2 / step);

        public PlainGrid()
        { }

        public override IEnumerable<object> AdditionalColorArgs => new string[] { "y" };

        protected override string FragmentShaderSrc =>
            "#version 130\r"+

            "uniform float t;\n"+
            "uniform vec3 u_cam;\n"+
            "in vec3 vec, normal;\n"+

            NOISE_GLSL_SRC+

            "float r(float x, float y, float z) {\n"+
            "   return " + ColorComponentsExpressions[ColorComponent.Red].ToGLSL() + ";\n"+
            "}\n"+
            "float g(float x, float y, float z) {\n"+
            "   return " + ColorComponentsExpressions[ColorComponent.Green].ToGLSL() + ";\n"+
            "}\n"+
            "float b(float x, float y, float z) {\n"+
            "   return " + ColorComponentsExpressions[ColorComponent.Blue].ToGLSL() + ";\n"+
            "}\n"+
            "float a(float x, float y, float z) {\n"+
            "   return " + ColorComponentsExpressions[ColorComponent.Alpha].ToGLSL() + ";\n"+
            "}\n"+

            "void main(void) {\n"+
            "    gl_FragColor = vec4(r(vec.x, vec.y, vec.z), g(vec.x, vec.y, vec.z), b(vec.x, vec.y, vec.z), 1) * normalize(normal).y;\n"+
            "    gl_FragColor.a = -distance(u_cam, vec) + "+Program.R.ToString()+";\n"+
            "}\n";

        protected override string VertexShaderSrc =>
            "#version 130\r"+

            "uniform int u_size;\n"+
            "uniform float u_step;\n"+
            "uniform float t;\n"+
            "uniform vec3 u_cam;\n" +
            "out vec3 vec, normal;\n" + //5

            NOISE_GLSL_SRC+

            "float y(float x, float z) {\n"+
            "   return " + ValueExpression.ToGLSL() + ";\n"+
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

        override protected void Draw0(Camera c)
        {
            Gl.Uniform1i(Gl.GetUniformLocation(program, "u_size"), 1, Size);
            Gl.Uniform1f(Gl.GetUniformLocation(program, "u_step"), 1, step);
            Gl.Uniform3f(Gl.GetUniformLocation(program, "u_cam"), 1, c.Position);
            Gl.DrawArrays(PrimitiveType.TriangleStrip, 0, (Size * 2 + 2) * Size);
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
