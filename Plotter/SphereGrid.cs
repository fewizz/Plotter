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
        public override IEnumerable<object> AdditionalColorArgs => new string[] { "x", "y", "z", "normal_x", "normal_y", "normal_z" };

        protected override string FragmentShaderSrc =>
            "#version 130\n" +

            "const float PI = 3.14159265359;\n" +
            "float AMin = -PI; float AMax = PI;\n" +
            "uniform float Time;\n" +
            "in vec3 Position, NormalDirection;\n" +
            "in vec2 SpherePosition;\n"+

            GLSLNoise.SOURCE +

            "void main(void) {\n" +
            "   if(SpherePosition.x < AMin || SpherePosition.x > AMax) discard;\n" +
            "   float x = Position.x, y = Position.y, z = Position.z, a = SpherePosition.x, b = SpherePosition.y, t = Time;\n" +
            "   vec3 normal = normalize(NormalDirection);"+
            "   float normal_x = normal.x, normal_y = normal.y, normal_z = normal.z;\n" +
            "   gl_FragColor = vec4(\n" +
                    ColorComponentsExpressions[ColorComponent.Red].ToGLSLSource() + ",\n" +
                    ColorComponentsExpressions[ColorComponent.Green].ToGLSLSource() + ",\n" +
                    ColorComponentsExpressions[ColorComponent.Blue].ToGLSLSource() + ",\n" +
                    "1\n" +
            "   );\n" +
            "   gl_FragColor.a = "+ColorComponentsExpressions[ColorComponent.Alpha].ToGLSLSource()+";\n" +
            "}\n";

        protected override string VertexShaderSrc =>
            "#version 130\n" +

            GLSLNoise.SOURCE +
            "uniform float Time;\n" +// 113
            "uniform int Frequency;\n" +
            "out vec3 Position, NormalDirection;\n" + // 115
            "out vec2 SpherePosition;\n" +
            "const float PI = 3.14159265359;\n" +
            "const float _2PI5 = 2*PI/5, PI5 = PI/5;\n" +
            "float t = Time;\n" +
            "float AMin = -PI; float AMax = PI;\n" + // 120

            "vec3 to_cartesian(vec2 c) {\n" +
            "   return vec3(cos(c.x)*cos(c.y), sin(c.y), sin(c.x)*cos(c.y));\n" +
            "}\n" +

            "float y_offset(int si) {\n" +// 124
            "   float yoff = 0.447213599;\n" +
            "   if(si == 0) return 1;\n" +
            "   if((si >= 1 && si <= 4)  || si == 6) return 1.0-yoff;\n" +
            "   if((si >= 7 && si <= 10) || si == 5) return yoff-1.0;\n" +
            "   return -1;\n" +
            "}\n" + // 130

            "float r(float x, float y, float z, float a, float b) {\n" +
            "   return " + ValueExpression.ToGLSLSource() + ";\n" +
            "}\n" +

            "float r(vec2 vs, vec3 vc) { return r(vc.x, vc.y, vc.z, vs.x, vs.y); }\n" +
            "float r(vec2 vs) { return r(vs, to_cartesian(vs)); }\n" +

            "float a_angle_offset(int si) {\n" +
            "   if(si == 1 || si == 3) return -PI5;\n" +
            "   if(si == 0 || si == 5 || si == 7 || si == 9) return 0;\n" +
            "   if(si == 2 || si == 4 || si == 6 || si == 11) return PI5;\n" +
            "   if(si == 8 || si == 10) return _2PI5;\n" +
            "}\n" + // 141

            "float a_angle(int slice_index, int xs) {\n" +
            "   return AMin + a_angle_offset(slice_index) + _2PI5*float(xs);\n" +
            "}\n" +

            "vec2 to_sphere(vec3 v, int slice_index, int xs) {\n" + // 145
            "   float b = asin(v.y);\n" +
            "   float angle = AMin + xs * _2PI5 - PI5;\n" +
            "   float offset = fract((angle + PI) / (2.0*PI)) * 2.0*PI;\n"+
            "   int lap = int(floor((angle + PI) / (2.0*PI)));\n" +

            "   if(v.x == 0 && v.z == 0) {\n" +
            "       vec2 res = vec2(angle + PI5, b);\n" +
            "       //if(slice_index == 11) res.x = res.x + PI5;\n" +
            "       return vec2(angle + PI5, b);\n" +
            "   }\n" +

            "   if(offset > PI && v.z < 0 && v.x < 0) lap++;\n"+
            "   if(offset < PI && v.z > 0 && v.x < 0) lap--;\n" +

            "   return vec2(atan(v.z, v.x) + PI*2.0*float(lap), b);\n" +
            "}\n" +

            "float x_offset(int slice_index, int xs) {\n" +
            "   if(slice_index == 0 || slice_index == 11) return 0;\n" +
            "   return cos(a_angle(slice_index, xs));\n" +
            "}\n" +

            "float z_offset(int slice_index, int xs) {\n" +
            "   if(slice_index == 0 || slice_index == 11) return 0;\n" +
            "   return sin(a_angle(slice_index, xs));\n" + // 150
            "}\n" +

            "int sqrt(int v) {\n"+
            "   int i = 0;\n" +
            "   for(; v >= 0; v-=i*2+1, i++); \n" +
            "   return i - 1;\n" +
            "}\n" +

            "float sign_(float v) {\n" +
            "   return v >= 0 ? 1 : -1;\n" +
            "}\n"+

            "void main(void) {\n" + // 160
            "   int subtriangles = Frequency*Frequency;\n" +
            "   int triangle_index = gl_VertexID / 3; \n" +
            "   int main_triangle_index = triangle_index / subtriangles;\n"+
            "   int triangle_vertex = gl_VertexID % 3;\n" +

            "   int segments = 6;//int((AMax - AMin) / (2*PI) * 5.0) + 1;\n"+
            "   int y_segment_index = main_triangle_index / segments;\n" +
            "   int x_segment_index = main_triangle_index % segments;\n" +
            "   int slice_index = y_segment_index*3;\n" +
            
            "   vec3 v0 = vec3(\n" +
            "       x_offset(slice_index, x_segment_index),\n" +
            "       y_offset(slice_index),\n" + // 170
            "       z_offset(slice_index, x_segment_index)\n" +
            "   );slice_index++;\n" +

            "   vec3 v1 = vec3(\n" +
            "       x_offset(slice_index, x_segment_index),\n" +
            "       y_offset(slice_index),\n" +
            "       z_offset(slice_index, x_segment_index)\n" +
            "   );slice_index++;\n" +

            "   vec3 v2 = vec3(\n" +
            "       x_offset(slice_index, x_segment_index),\n" +
            "       y_offset(slice_index),\n" + // 180
            "       z_offset(slice_index, x_segment_index)\n" +
            "   );\n" +

            "   int sub_triangle_index = triangle_index % subtriangles;\n" +
            "   int tr_y = Frequency - 1 - sqrt(Frequency*Frequency - sub_triangle_index - 1);\n" +
            "   int tr_x = sub_triangle_index - (Frequency*Frequency - (Frequency-tr_y)*(Frequency-tr_y));" +
            "   vec3 v01 = (v1 - v0) / Frequency;\n" +
            "   vec3 v02 = (v2 - v0) / Frequency;\n" +
            "   vec3 v12 = (v2 - v1) / Frequency;\n" +

            "   vec3 vec = v0;\n" +
            "   if(triangle_vertex == 1) if(tr_x % 2 == 1) vec += v12; else vec += v01;\n" + // 190
            "   if(triangle_vertex == 2) vec+=v02;\n" +
            "   tr_x = (tr_x % 2) + tr_x / 2;\n" +
            "   vec += v01*tr_x + v02*tr_y;\n" +
            "   vec = normalize(vec);\n"+

            "   SpherePosition = to_sphere(vec, slice_index, x_segment_index);\n" + // 194 ?
            "   vec2 offseted[2] = vec2[2](" +
            "       vec2(" +
            "           SpherePosition +" +
            "           vec2(" +
            "               -sign_(SpherePosition.y)*0.01," +
            "               -sign_(SpherePosition.y)*0.01" +
            "           )" +
            "       )," +
            "       vec2(" +
            "           SpherePosition +" +
            "           vec2(" +
            "               sign_(SpherePosition.y)*0.01," +
            "               -sign_(SpherePosition.y)*0.01" +
            "           )" +
            "       )" +
            "   );\n" +
            "   Position = vec*r(SpherePosition);\n" +
            "   NormalDirection = cross(" +
            "       Position - to_cartesian(offseted[1])*r(offseted[1])," +
            "       Position - to_cartesian(offseted[0])*r(offseted[0])" +
            "   );\n" +
            "   gl_Position = gl_ModelViewProjectionMatrix * vec4(Position, 1);\n" +
            "}"; // 200

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
            float x = (float)(Math.Cos((double)a1) * Math.Cos((double)a0));
            float z = (float)(Math.Cos((double)a1) * Math.Sin((double)a0));
            float y = (float)Math.Sin((double)a1);
            arg0.Value = a0;
            arg1.Value = a1;
            return new Vertex3f(x, y, z)*(float)ValueExpression.Value;
        }

        public override string Arg0Name => "a";
        public override string Arg1Name => "b";
    }
}
