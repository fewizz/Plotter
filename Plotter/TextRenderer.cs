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
        Dictionary<char, uint> CharMap = new Dictionary<char, uint>();
        public Font Font { get; private set; }
        Bitmap Bitmap;
        Graphics Graphics;

        public TextRenderer(Font f)
        {
            Font = f;
            Bitmap = new Bitmap(256, 256, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics = Graphics.FromImage(Bitmap);
        }

        private void Add(char ch)
        {
            uint[] names = new uint[1];
            Gl.GenTextures(names);
            uint name = names[0];
            Gl.BindTexture(TextureTarget.Texture2d, name);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Nearest);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMagFilter.Nearest);
            Graphics.Clear(Color.Transparent);
            Graphics.DrawString(ch.ToString(), Font, Brushes.White, 0, Bitmap.Height - Font.Height);
            var bmData = Bitmap.LockBits(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height), ImageLockMode.ReadOnly, Bitmap.PixelFormat);

            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, Bitmap.Width, Bitmap.Height, 0, OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmData.Scan0);

            Bitmap.UnlockBits(bmData);
            CharMap.Add(ch, name);
        }


        public void Render(char ch)
        {
            if (!CharMap.ContainsKey(ch))
                Add(ch);

            Gl.Enable(EnableCap.Texture2d);
            Gl.BindTexture(TextureTarget.Texture2d, CharMap[ch]);
            Gl.Color3(1F, 1F, 1F);
            Gl.Begin(PrimitiveType.Quads);
            Gl.TexCoord2(0F, 1F);
            Gl.Vertex3(0, 0, 0);
            Gl.TexCoord2(1F, 1F);
            Gl.Vertex3(Bitmap.Width, 0, 0);
            Gl.TexCoord2(1F, 0F);
            Gl.Vertex3(Bitmap.Width, Bitmap.Height, 0);
            Gl.TexCoord2(0F, 0F);
            Gl.Vertex3(0, Bitmap.Height, 0);
            Gl.End();
            Gl.Disable(EnableCap.Texture2d);
        }

        public void Render(string text)
        {
            Gl.PushMatrix();
            foreach (char ch in text)
            {
                Gl.Translate(Font.Size*(64/72.0), 0, 0);
                Render(ch);
            }
            Gl.PopMatrix();
        }
    }
}
