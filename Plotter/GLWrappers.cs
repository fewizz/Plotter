using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

namespace Plotter
{
    public class Shader : IDisposable
    {
        public uint Name { get; private set; }

        public Shader(ShaderType type) => Name = Gl.CreateShader(type);
        public void Source(string source) => Gl.ShaderSource(Name, new string[] { source });
        public Status Compile()
        {
            Gl.CompileShader(Name);
            Gl.GetShader(Name, ShaderParameterName.CompileStatus, out int succ);
            if (succ == 0)
            {
                Gl.GetShader(Name, ShaderParameterName.InfoLogLength, out int len);
                StringBuilder sb = new StringBuilder(len);
                Gl.GetShaderInfoLog(Name, len, out int _, sb);
                Console.WriteLine(sb.ToString());
                return Status.Error;
            }
            return Status.Ok;
        }
        public Status Compile(string src)
        {
            Source(src);
            return Compile();
        }

        public void Dispose() => Gl.DeleteShader(Name);
    }

    public class ShaderProgram : IDisposable
    {
        uint name;

        public ShaderProgram() => name = Gl.CreateProgram();
        public void Attach(params Shader[] sh)
        {
            foreach (var s in sh) Gl.AttachShader(name, s.Name);
        }
        public Status Link()
        {
            Gl.LinkProgram(name);
            Gl.GetProgram(name, ProgramProperty.LinkStatus, out int succ);
            if (succ == 0)
            {
                Gl.GetProgram(name, ProgramProperty.InfoLogLength, out int len);
                StringBuilder sb = new StringBuilder(len);
                Gl.GetProgramInfoLog(name, len, out _, sb);
                Console.WriteLine(sb.ToString());
                return Status.Error;
            }
            return Status.Ok;
        }
        public void Use() => Gl.UseProgram(name);
        public void Uniform(string name, int v)
        {
            Use();
            Gl.Uniform1i(Gl.GetUniformLocation(this.name, name), 1, v);
        }
        public void Uniform(string name, float v)
        {
            Use();
            Gl.Uniform1f(Gl.GetUniformLocation(this.name, name), 1, v);
        }
        public void Uniform(string name, Vertex3f v)
        {
            Use();
            Gl.Uniform3f(Gl.GetUniformLocation(this.name, name), 1, v);
        }
        public void Dispose() => Gl.DeleteProgram(name);
    }

    public class Texture : IDisposable
    {
        public uint Name { get; private set; }

        public Texture() => Name = Gl.CreateTexture(TextureTarget.Texture2d);
        public void Bind() => Gl.BindTexture(TextureTarget.Texture2d, Name);
        public void Filter(TextureMinFilter filter)
        {
            Gl.TextureParameteri(Name, TextureParameterName.TextureMinFilter, filter);
            Gl.TextureParameteri(Name, TextureParameterName.TextureMagFilter, filter);
        }
        public void Wrap(TextureWrapMode mode)
        {
            Gl.TextureParameteri(Name, TextureParameterName.TextureWrapS, mode);
            Gl.TextureParameteri(Name, TextureParameterName.TextureWrapT, mode);
        }
        public void Image(InternalFormat i, int width, int height, PixelFormat pf, PixelType pt, IntPtr data)
        {
            Bind();
            Gl.TexImage2D(TextureTarget.Texture2d, 0, i, width, height, 0, pf, pt, data);
        }
        public void Storage(InternalFormat format, int w, int h) => Gl.TextureStorage2D(Name, 1, format, w, h);
        public void Dispose() => Gl.DeleteTextures(new uint[] { Name });
    }

    public class Framebuffer : IDisposable
    {
        uint name;

        public Framebuffer() => name = Gl.CreateFramebuffer();
        public void Texture(FramebufferAttachment fa, Texture t)
            => Gl.FramebufferTexture(FramebufferTarget.Framebuffer, fa, t.Name, 0);
        public void Bind() => Gl.BindFramebuffer(FramebufferTarget.Framebuffer, name);
        public void Dispose() => Gl.DeleteFramebuffers(new uint[] { name });
    }
}
