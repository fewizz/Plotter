using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

namespace Plotter
{
    class TextRenderer
    {
        Dictionary<char, uint> map = new Dictionary<char, uint>();
        Font font;
        Bitmap bm;
        Graphics g;

        public TextRenderer(Font f)
        {
            font = f;
            bm = new Bitmap(256, 256, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            g = Graphics.FromImage(bm);
        }

        private void Add(char ch)
        {
            uint[] names = new uint[1];
            Gl.GenTextures(names);
            uint name = names[0];
            Gl.BindTexture(TextureTarget.Texture2d, name);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Nearest);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMagFilter.Nearest);
            g.Clear(Color.Transparent);
            g.DrawString(ch.ToString(), font, Brushes.White, 0, bm.Height - font.Height);
            var bmData = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, bm.PixelFormat);

            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, bm.Width, bm.Height, 0, OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmData.Scan0);

            bm.UnlockBits(bmData);
            map.Add(ch, name);
        }


        public void Render(char ch)
        {
            if (!map.ContainsKey(ch))
                Add(ch);

            Gl.Enable(EnableCap.Texture2d);
            Gl.BindTexture(TextureTarget.Texture2d, map[ch]);
            Gl.Color3(1F, 1F, 1F);
            Gl.Begin(PrimitiveType.Quads);
            Gl.TexCoord2(0F, 1F);
            Gl.Vertex3(0, 0, 0);
            Gl.TexCoord2(1F, 1F);
            Gl.Vertex3(bm.Width, 0, 0);
            Gl.TexCoord2(1F, 0F);
            Gl.Vertex3(bm.Width, bm.Height, 0);
            Gl.TexCoord2(0F, 0F);
            Gl.Vertex3(0, bm.Height, 0);
            Gl.End();
            Gl.Disable(EnableCap.Texture2d);
        }
    }
}
