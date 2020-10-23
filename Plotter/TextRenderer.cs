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
    public class TextRenderer
    {
        Dictionary<char, Texture> CharMap = new Dictionary<char, Texture>();
        public Font Font { get; private set; }
        Bitmap Bitmap;
        Graphics Graphics;

        public TextRenderer(Font f)
        {
            Font = f;
            Bitmap = new Bitmap(256, 256, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics = Graphics.FromImage(Bitmap);
            Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }

        private void Add(char ch)
        {
            Texture t = new Texture();
            t.Filter(TextureMinFilter.Linear);
            Graphics.Clear(Color.Transparent);
            Graphics.DrawString(ch.ToString(), Font, Brushes.White, -9, Bitmap.Height - Font.Height);
            var bmData = Bitmap.LockBits(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height), ImageLockMode.ReadOnly, Bitmap.PixelFormat);

            t.Image(
                InternalFormat.Rgba,
                Bitmap.Width,
                Bitmap.Height,
                OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte,
                bmData.Scan0
            );

            Bitmap.UnlockBits(bmData);
            CharMap.Add(ch, t);
        }


        public void Draw(char ch, float size = 1)
        {
            if (!CharMap.ContainsKey(ch))
                Add(ch);

            Gl.Enable(EnableCap.Texture2d);
            CharMap[ch].Bind();
            Gl.Color3(1F, 1F, 1F);
            Gl.Begin(PrimitiveType.Quads);
            Gl.TexCoord2(0F, 1F);
            Gl.Vertex3(0, 0, 0);
            Gl.TexCoord2(1F, 1F);
            Gl.Vertex3(Bitmap.Width*size, 0, 0);
            Gl.TexCoord2(1F, 0F);
            Gl.Vertex3(Bitmap.Width*size, Bitmap.Height*size, 0);
            Gl.TexCoord2(0F, 0F);
            Gl.Vertex3(0, Bitmap.Height*size, 0);
            Gl.End();
            Gl.Disable(EnableCap.Texture2d);
        }

        public double CharSize => Font.Size * (64 / 72.0);

        public void Draw(string text, float size = 1)
        {
            Gl.PushMatrix();
            foreach (char ch in text)
            {
                Draw(ch, size);
                Gl.Translate(CharSize * size, 0, 0);
            }
            Gl.PopMatrix();
        }

        public void DrawCentered(string text, float size = 1)
        {
            Gl.PushMatrix();
            Gl.Translate(-text.Length*CharSize/2.0*size, 0, 0);
            Draw(text, size);
            Gl.PopMatrix();
        }
    }
}
