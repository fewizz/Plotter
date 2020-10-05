using OpenGL;
using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    class SphereGridRenderer : GridRenderer
    {

        public SphereGridRenderer()
        {
        }

        protected override string FragmentShaderSrc(Dictionary<ColorComponent, IExpression> exprs)
        {
            return
            "#version 130\r" +

            "uniform float t; in vec3 color;\n" +
            "in vec3 vec, normal;\n" +


            commonShaderSrc +

            "float r(float x, float y, float z) {\n" +
            "   return " + exprs[ColorComponent.Red].ToGLSL() + ";\n" +
            "}\n" +
            "float g(float x, float y, float z) {\n" +
            "   return " + exprs[ColorComponent.Green].ToGLSL() + ";\n" +
            "}\n" +
            "float b(float x, float y, float z) {\n" +
            "   return " + exprs[ColorComponent.Blue].ToGLSL() + ";\n" +
            "}\n" +
            "float a(float x, float y, float z) {\n" +
            "   return " + exprs[ColorComponent.Alpha].ToGLSL() + ";\n" +
            "}\n" +

            "void main(void) {\n" +
            //"   gl_FragColor = vec4(color, 1);\n" +
            "    gl_FragColor = vec4(r(vec.x, vec.y, vec.z), g(vec.x, vec.y, vec.z), b(vec.x, vec.y, vec.z), 1) * normalize(normal).y;\n" +
            "    gl_FragColor.a = 1;//a(vec.x, vec.y, vec.z);\n" +
            "}\n";
        }

        protected override string VertexShaderSrc(IExpression ex)
        {
            return
            "#version 130\r" +

            //"uniform int u_freq;\n" +
            //"uniform int u_sub_t;\n" +
            "uniform float t;\n" +
            "out vec3 vec, normal;\n" + //5
            "out vec3 color;\n" +
            "float y_offset(int i) {\n" +
            "   float yoff = 0.447213599;\n" + //5
            "   if(i == 0) return 1;\n" +
            "   if((i >= 1 && i <= 4)  || i == 6) return 1.0-yoff;\n" +
            "   if((i >= 7 && i <= 10) || i == 5) return yoff-1.0;\n" +
            "   return -1;\n" +
            "}\n" + // 10

            "float r(float a, float b) {\n" +
            "   return sin(abs(a)*4)+cos(abs(b))*10;\n" +
            "}\n"+

            "float r(vec2 v) { return r(v.x, v.y); }\n"+

            "float offset0(int i) {\n" +
            "   float xoff = 0.62831853071;\n" +
            "   if(i == 5 || (i >= 7 && i <= 10)) return 0;\n" +
            //"   if(i == 6 || i == 11 || i >= 1 && i <= 4 ) 0.31415926535;\n" +
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

            "vec3 from_sphere(vec2 c) {\n" +
            "   return vec3(cos(c.x)*cos(c.y), sin(c.y), sin(c.x)*cos(c.y));\n" +
            "}\n"+

            "float sign_(float v) {\n" +
            "   return v >= 0 ? 1 : -1;\n" +
            "}\n"+

            "void main(void) {\n" +
            "   int freq = 20;\n" +
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

            "   vec = v0;" +
            "   if(triangle_vertex == 1) if(tr_x % 2 == 1) vec += v12; else vec += v01;\n" +
            "   if(triangle_vertex == 2) vec+=v02;\n" +
            "   tr_x = (tr_x % 2) + tr_x / 2;\n" +
            "   vec += v01*tr_x + v02*tr_y;\n" +
            "   vec = normalize(vec);\n"+

            "   vec3 on_plane = vec;\n"+
            "   on_plane.y = 0;\n" +
            "   on_plane = normalize(on_plane);\n"+
            "   vec2 sc = vec2(atan(vec.z, vec.x), asin(vec.y));\n" +
            "   vec2 offseted[2] = vec2[2](vec2(sc + vec2(-sign_(sc.y)*0.01, -sign_(sc.y)*0.01)), vec2(sc + vec2(sign_(sc.y)*0.01, -sign_(sc.y)*0.01)));\n" +
            "   vec3 res = vec*r(sc);\n" +
            "   normal = normalize(cross(res - from_sphere(offseted[1])*r(offseted[1]), res - from_sphere(offseted[0])*r(offseted[0])));\n" +
            "   gl_Position = gl_ModelViewProjectionMatrix * vec4(res, 1);\n" +
            "   color = vec3(0); color.r = 1;//[triangle_vertex] = 1;\n" +
            "   "+
            "}";
        }

        override protected void Draw0()
        {
            int freq = 20;
            Gl.DrawArrays(PrimitiveType.Triangles, 0, 20*freq*freq*3);
        }
    }
}
