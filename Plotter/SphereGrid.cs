using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    class SphereGrid : Grid
    {
        public int Frequency = 40;
        public SphereGrid()
        { }

        public override IEnumerable<object> AdditionalValueArgs => new string[] { "x", "y", "z" };
        public override IEnumerable<object> AdditionalColorArgs => new string[] { "x", "y", "z" };

        protected override string FragmentShaderSrc =>
            "#version 130\n" +

            "uniform float Time;\n" +
            "in vec3 Position, Normal;\n" +
            "in vec2 SpherePosition;\n"+

            GLSLNoise.SOURCE +

            "void main(void) {\n" +
            "   if(SpherePosition.x < -3.14 || SpherePosition.x > 3.14) discard;\n" +
            "   float x = Position.x, y = Position.y, z = Position.z, a = SpherePosition.x, b = SpherePosition.y, t = Time;\n" +
            "   gl_FragColor = vec4(\n" +
                    ColorComponentsExpressions[ColorComponent.Red].ToGLSLSource() + ",\n" +
                    ColorComponentsExpressions[ColorComponent.Green].ToGLSLSource() + ",\n" +
                    ColorComponentsExpressions[ColorComponent.Blue].ToGLSLSource() + ",\n" +
                    "1\n" +
            "   ) * normalize(Normal).y;\n" +
            "   gl_FragColor.a = "+ColorComponentsExpressions[ColorComponent.Alpha].ToGLSLSource()+";\n" +
            "}\n";

        protected override string VertexShaderSrc =>
            "#version 130\n" +
            
            GLSLNoise.SOURCE +

            "uniform float Time;\n" +
            "uniform int Frequency;"+
            "out vec3 Position, Normal;\n" + //5
            "out vec2 SpherePosition;\n" +

            "vec3 to_cartesian(vec2 c) {\n" +
            "   return vec3(cos(c.x)*cos(c.y), sin(c.y), sin(c.x)*cos(c.y));\n" +
            "}\n" +

            "vec2 to_sphere(vec3 v) {\n" +
            "   return vec2(atan(v.z, v.x), asin(v.y));\n" +
            "}\n" +

            "float y_offset(int i) {\n" +
            "   float yoff = 0.447213599;\n" + //5
            "   if(i == 0) return 1;\n" +
            "   if((i >= 1 && i <= 4)  || i == 6) return 1.0-yoff;\n" +
            "   if((i >= 7 && i <= 10) || i == 5) return yoff-1.0;\n" +
            "   return -1;\n" +
            "}\n" + // 10

            "float r(float x, float y, float z, float a, float b) {\n" +
            "   return "+ValueExpression.ToGLSLSource()+";\n" +
            "}\n"+

            "float r(vec2 vs, vec3 vc) { return r(vc.x, vc.y, vc.z, vs.x, vs.y); }\n" +
            "float r(vec2 vs) { return r(vs, to_cartesian(vs)); }\n" +

            "float offset0(int i) {\n" +
            "   float xoff = -0.62831853071;\n" +
            "   if(i == 5 || (i >= 7 && i <= 10)) return 0;\n" +
            "   return xoff;\n" +
            "}\n" + // 15

            "float shift(int i) {\n" +
            "   if(i%2==1) return 1.25663706144; return 0;\n" +
            "}\n" + // 15

            "float x_offset(int i, int xi) {\n" +
            "   if(i == 0 || i == 11) return 0;\n" +
            "   return cos(shift(i) + offset0(i) + 1.25663706144*float(xi));\n" +
            "}\n" +

            "float z_offset(int i, int xi) {\n" +
            "   if(i == 0 || i == 11) return 0;\n" +
            "   return sin(shift(i) + offset0(i) + 1.25663706144*float(xi));\n" +
            "}\n" +

            "int sqrt(int v) {\n"+
            "   int i = 0;\n" +
            "   for(; v >= 0; v-=i*2+1, i++); \n" +
            "   return i - 1;\n" +
            "}\n" +

            "float sign_(float v) {\n" +
            "   return v >= 0 ? 1 : -1;\n" +
            "}\n"+

            "void main(void) {\n" +
            "   int subtriangles = Frequency*Frequency;\n" +
            "   int triangle_index = gl_VertexID / 3; \n" + // 20
            "   int main_triangle_index = triangle_index / subtriangles;"+
            "   int triangle_vertex = gl_VertexID % 3;\n" +

            "   int y_segment_index = main_triangle_index / 6;\n" +
            "   int x_segment_index = main_triangle_index % 6;\n" +
            "   int i = y_segment_index*3;\n" +

            "   vec3 v0 = vec3(\n" +
            "           x_offset(i, x_segment_index),\n" +
            "           y_offset(i),\n" +
            "           z_offset(i, x_segment_index)\n" +
            "   );i++;\n" +

            "   vec3 v1 = vec3(\n" +
            "           x_offset(i, x_segment_index),\n" +
            "           y_offset(i),\n" +
            "           z_offset(i, x_segment_index)\n" +
            "   );i++;\n" +

            "   vec3 v2 = vec3(\n" +
            "           x_offset(i, x_segment_index),\n" +
            "           y_offset(i),\n" +
            "           z_offset(i, x_segment_index)\n" +
            "   );\n" +

            "   int sub_triangle_index = triangle_index % subtriangles;\n" +
            "   int tr_y = Frequency - 1 - sqrt(Frequency*Frequency - sub_triangle_index - 1);\n" +
            "   int tr_x = sub_triangle_index - (Frequency*Frequency - (Frequency-tr_y)*(Frequency-tr_y));" +
            "   vec3 v01 = (v1 - v0) / Frequency;\n" +
            "   vec3 v02 = (v2 - v0) / Frequency;\n" +
            "   vec3 v12 = (v2 - v1) / Frequency;\n" +

            "   vec3 vec = v0;" +
            "   if(triangle_vertex == 1) if(tr_x % 2 == 1) vec += v12; else vec += v01;\n" +
            "   if(triangle_vertex == 2) vec+=v02;\n" +
            "   tr_x = (tr_x % 2) + tr_x / 2;\n" +
            "   vec += v01*tr_x + v02*tr_y;\n" +
            "   vec = normalize(vec);\n"+

            "   SpherePosition = to_sphere(vec);\n" +
            "   vec2 offseted[2] = vec2[2](vec2(SpherePosition + vec2(-sign_(SpherePosition.y)*0.01, -sign_(SpherePosition.y)*0.01)), vec2(SpherePosition + vec2(sign_(SpherePosition.y)*0.01, -sign_(SpherePosition.y)*0.01)));\n" +
            "   Position = vec*r(SpherePosition);\n" +
            "   Normal = cross(Position - to_cartesian(offseted[1])*r(offseted[1]), Position - to_cartesian(offseted[0])*r(offseted[0]));\n" +
            "   gl_Position = gl_ModelViewProjectionMatrix * vec4(Position, 1);\n" +
            "}";

        override protected void Draw0()
        {
            program.Use();
            program.Uniform("Time", (float)Program.TimeArg.Value);
            program.Uniform("Frequency", Frequency);
            Gl.DrawArrays(PrimitiveType.Triangles, 0, 25*Frequency*Frequency*3);
            Gl.UseProgram(0);
        }

        public override Vertex3f CartesianCoord(decimal a0, decimal a1)
        {
            float x = (float)(Math.Cos((double)a0) * Math.Cos((double)a1));
            float z = (float)(Math.Cos((double)a0) * Math.Sin((double)a1));
            float y = (float)Math.Sin((double)a1);
            arg0.Value = a0;
            arg1.Value = a1;
            return new Vertex3f(x, y, z)*(float)ValueExpression.Value;
        }

        public override string Arg0Name => "a";
        public override string Arg1Name => "b";
    }
}
