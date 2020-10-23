using OpenGL;
using Plotter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        uint fb, tex, program, program0, fs;

        private void glControl1_Load(object sender, EventArgs e)
        {
            Gl.DebugMessageCallback((DebugSource source, DebugType type, uint id, DebugSeverity severity, int length, IntPtr message, IntPtr userParam) =>
            {
                Console.WriteLine(Marshal.PtrToStringAnsi(message));
            }, IntPtr.Zero);

            Gl.Enable(EnableCap.Texture2d);
            Gl.Enable(EnableCap.DepthTest);
            Depth();
            Normal();
        }

        void Depth()
        {
            tex = Gl.GenTexture();

            Gl.BindTexture(TextureTarget.Texture2d, tex);
            Gl.TexStorage2D(
                TextureTarget.Texture2d,
                1,
                InternalFormat.DepthComponent32,
                128,
                128
            );
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Nearest);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMinFilter.Nearest);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.ClampToEdge);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.ClampToEdge);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureCompareMode, Gl.NONE);

            fb = Gl.GenFramebuffer();

            program0 = Gl.CreateProgram();
            uint fs = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.AttachShader(program0, fs);
            ShaderUtil.Compile(fs,
                "#version 130\n" +
                "void main() {\n" +
                "   gl_FragDepth = 0.4;\n" +
                "}"
            );
            ShaderUtil.Link(program0);
        }

        void Normal()
        {
            program = Gl.CreateProgram();
            fs = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.AttachShader(program, fs);
            ShaderUtil.Compile(fs,
                "#version 130\n" +
                "uniform sampler2D tex;\n"+
                "void main() {\n" +
                "   float r = texture(tex, vec2(0,0)).r;\n" +
                "   gl_FragColor = vec4(vec3(r), 1);\n" +
                "}"
            );
            ShaderUtil.Link(program);
            Gl.UseProgram(0);
        }

        private void glControl1_Render(object sender, GlControlEventArgs e)
        {
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Gl.BindFramebuffer(FramebufferTarget.Framebuffer, fb);
            Gl.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, tex, 0);
            Gl.Clear(ClearBufferMask.DepthBufferBit);
            Gl.UseProgram(program0);
            Gl.Rect(-1, -1, 1, 1);
            Gl.UseProgram(0);
            Gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            Gl.UseProgram(program);
            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.BindTexture(TextureTarget.Texture2d, tex);
            Gl.Uniform1i(Gl.GetUniformLocation(program, "tex"), 1, 0);
            Gl.Rect(-1, -1, 1, 1);
            Gl.UseProgram(0);
        }
    }
}
