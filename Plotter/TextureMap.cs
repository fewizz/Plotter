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
    class TextureMap
    {
        Dictionary<char, uint> map = new Dictionary<char, uint>();
    
        public void Add(char ch, Font f)
        {
            Bitmap bm = new Bitmap(256, 256, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bm);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            //System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            g.DrawString("Hello!", f, Brushes.White, 50, 50);
            g.DrawLine(Pens.Red, new Point(0, 256), new Point(256, 0));
            g.DrawLine(Pens.Red, new Point(0, 0), new Point(256, 256));
            //MemoryStream ms = new MemoryStream(bm.Width * bm.Height * 4);
            //bm.Save(ms, ImageFormat.MemoryBmp);
            var bmData = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, bm.PixelFormat);
            //IntPtr unmanagedPointer = Marshal.AllocHGlobal((int)ms.Length);
            //Marshal.Copy(ms.ToArray(), 0, unmanagedPointer, (int)ms.Length);

            uint[] names = new uint[1];
            Gl.GenTextures(names);
            uint name = names[0];
            Gl.BindTexture(TextureTarget.Texture2d, name);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Nearest);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMagFilter.Nearest);
            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, 256, 256, 0, OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmData.Scan0);
            //Marshal.FreeHGlobal(unmanagedPointer);
            map.Add(ch, name);
        }

        public void Bind(char ch)
        {
            Gl.BindTexture(TextureTarget.Texture2d, map[ch]);
        }
    }
}
