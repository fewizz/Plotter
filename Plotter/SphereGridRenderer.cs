using OpenGL;
using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    class SphereGridRenderer : GridRenderer
    {

        public SphereGridRenderer()
        {
        }

        public override string[] AdditionalValue()
        {
            return new string[] { "x", "y", "z" };
        }

        public override string[] AdditionalColor()
        {
            return new string[] { "x", "y", "z" };
        }

        protected override string FragmentShaderSrc(Dictionary<ColorComponent, IExpression> exprs)
        {
            return
            "#version 130\r" +

            "uniform float t;\n" +
            "in vec3 res, normal;\n" +
            "in vec2 sc;\n"+

            commonShaderSrc +

            "float r(float x, float y, float z, float a, float b) {\n" +
            "   return " + exprs[ColorComponent.Red].ToGLSL() + ";\n" +
            "}\n" +
            "float g(float x, float y, float z, float a, float b) {\n" +
            "   return " + exprs[ColorComponent.Green].ToGLSL() + ";\n" +
            "}\n" +
            "float b(float x, float y, float z, float a, float b) {\n" +
            "   return " + exprs[ColorComponent.Blue].ToGLSL() + ";\n" +
            "}\n" +
            "float a(float x, float y, float z, float a, float b) {\n" +
            "   return " + exprs[ColorComponent.Alpha].ToGLSL() + ";\n" +
            "}\n" +

            "void main(void) {\n" +
            "    gl_FragColor = vec4(r(res.x, res.y, res.z, sc.x, sc.y), g(res.x, res.y, res.z, sc.x, sc.y), b(res.x, res.y, res.z, sc.x, sc.y), 1) * normalize(normal).y;\n" +
            "    gl_FragColor.a = a(res.x, res.y, res.z, sc.x, sc.y);\n" +
            "}\n";
        }

        protected override string VertexShaderSrc(IExpression ex)
        {
            return
            "#version 130\r" +
            
            commonShaderSrc+

            "uniform float t;\n" +
            "out vec3 res, normal;\n" + //5
            "out vec2 sc;\n" +

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
            "   return "+ex.ToGLSL()+";\n" +
            "}\n"+

            "float r(vec2 vs, vec3 vc) { return r(vc.x, vc.y, vc.z, vs.x, vs.y); }\n" +
            "float r(vec2 vs) { return r(vs, to_cartesian(vs)); }\n" +

            "float offset0(int i) {\n" +
            "   float xoff = 0.62831853071;\n" +
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
            "   int freq = 40;\n" +
            "   int subtriangles = freq*freq;\n"+
            "   int triangle_index = gl_VertexID / 3; \n" + // 20
            "   int main_triangle_index = triangle_index / subtriangles;"+
            "   int triangle_vertex = gl_VertexID % 3;\n" +

            "   int y_segment_index = main_triangle_index / 5;\n" +
            "   int x_segment_index = main_triangle_index % 5;\n" +
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
            "   int tr_y = freq - 1 - sqrt(freq*freq - sub_triangle_index - 1);//int(sqrt(float(sub_triangle_index)));\n" +
            "   int tr_x = sub_triangle_index - (freq*freq - (freq-tr_y)*(freq-tr_y));" +
            "   vec3 v01 = (v1 - v0) / freq;\n" +
            "   vec3 v02 = (v2 - v0) / freq;\n" +
            "   vec3 v12 = (v2 - v1) / freq;\n" +

            "   vec3 vec = v0;" +
            "   if(triangle_vertex == 1) if(tr_x % 2 == 1) vec += v12; else vec += v01;\n" +
            "   if(triangle_vertex == 2) vec+=v02;\n" +
            "   tr_x = (tr_x % 2) + tr_x / 2;\n" +
            "   vec += v01*tr_x + v02*tr_y;\n" +
            "   vec = normalize(vec);\n"+

            "   sc = to_sphere(vec);\n" +
            "   vec2 offseted[2] = vec2[2](vec2(sc + vec2(-sign_(sc.y)*0.01, -sign_(sc.y)*0.01)), vec2(sc + vec2(sign_(sc.y)*0.01, -sign_(sc.y)*0.01)));\n" +
            "   res = vec*r(sc);\n" +
            "   normal = normalize(cross(res - to_cartesian(offseted[1])*r(offseted[1]), res - to_cartesian(offseted[0])*r(offseted[0])));\n" +
            "   gl_Position = gl_ModelViewProjectionMatrix * vec4(res, 1);\n" +
            "   "+
            "}";
        }

        override protected void Draw0(Camera c)
        {
            int freq = 40;
            Gl.DrawArrays(PrimitiveType.Triangles, 0, 20*freq*freq*3);
        }

        public override string Arg0() => "a";

        public override string Arg1() => "b";
    }
}
