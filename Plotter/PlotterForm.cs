using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenGL;
using Parser;

namespace Plotter
{
    public partial class PlotterForm : Form
    {
        Camera cam = new Camera();
        GridsForm grids;
        PointsForm points;
        DateTime TimeArg;
        TextureMap m = new TextureMap();

        public PlotterForm()
        {
            InitializeComponent();

            grids = new GridsForm();
            points = new PointsForm(grids);
            TimeArg = DateTime.Now;
            FormClosed += (s, e) =>
            {
                points.Close();
                grids.Close();
            };

            Vertex2f READY = new Vertex2f(-1);
            Vertex2f DONE = new Vertex2f(-2);
            Vertex2f rot = DONE;

            gl.MouseMove += (sender, e) =>
            {
                if (rot == DONE) return;
                Vertex2f nrot = new Vertex2f(
                    e.Y - gl.Height / 2F,
                    e.X - gl.Width / 2F
                );
                if (rot == READY) rot = nrot;

                cam.Rotation += (nrot - rot) / 3F;
                cam.Rotation.x = Math.Max(Math.Min(cam.Rotation.x, 90), -90);

                rot = nrot;
            };

            gl.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                    rot = READY;
            };

            gl.MouseUp += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                    rot = DONE;
            };

        }

        private void OnShown(object sender, EventArgs e)
        {
            grids.Left = Right;
            grids.Top = Top;
            grids.Show();

            points.Left = Left - points.Width;
            points.Top = Top;
            points.Show();
        }

        private void glLoad(object sender, EventArgs e)
        {
            Gl.ClearColor(0, 0, 0.2F, 1F);
            Gl.Enable(EnableCap.DepthTest);
            Gl.Enable(EnableCap.Blend);
            Gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            m.Add('a', new Font("Arial", 20));
        }

        List<Keys> keysPressed = new List<Keys>();

        private void glRender(object sender, GlControlEventArgs e)
        {
            Gl.Viewport(0, 0, gl.Width, gl.Height);
            cam.Projection = Matrix4x4f.Perspective(
                100,
                (float)gl.Width / gl.Height,
                0.1F,
                100
            );

            Vertex3f trans = new Vertex3f();
            if (keysPressed.Contains(Keys.W))
                trans.z--;
            if (keysPressed.Contains(Keys.S))
                trans.z++;
            if (keysPressed.Contains(Keys.A))
                trans.x--;
            if (keysPressed.Contains(Keys.D))
                trans.x++;
            if (keysPressed.Contains(Keys.Space))
                trans.y++;
            if (keysPressed.Contains(Keys.ShiftKey))
                trans.y--;
            
            trans /= 5F;
            var rotM = Matrix3x3f.RotatedY(cam.Rotation.y);
            rotM.RotateX(cam.Rotation.x);
            trans = rotM * trans;
            cam.Position += trans;

            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            cam.ApplyTransformations();
            Gl.Color3(1f, 0, 0);
            Gl.Begin(PrimitiveType.Lines);
            Gl.Vertex3(0, 0, 0);
            Gl.Vertex3(10, 0, 0);
            Gl.Vertex3(0, 0, 0);
            Gl.Vertex3(0, 10, 0);
            Gl.Vertex3(0, 0, 0);
            Gl.Vertex3(0, 0, 10);
            Gl.End();

            Gl.Enable(EnableCap.Texture2d);
            m.Bind('a');
            Gl.Color3(1F, 1F, 1F);
            Gl.Begin(PrimitiveType.Quads);
            Gl.TexCoord2(0F, 1F);
            Gl.Vertex3(0, 0, 0);
            Gl.TexCoord2(1F, 1F);
            Gl.Vertex3(10, 0, 0);
            Gl.TexCoord2(1F, 0F);
            Gl.Vertex3(10, 10, 0);
            Gl.TexCoord2(0F, 0F);
            Gl.Vertex3(0, 10, 0);
            Gl.End();
            Gl.Disable(EnableCap.Texture2d);

            foreach (var g in grids.GridConstructors()) {
                g.Grid.Draw(TimeArg);
            }

            Gl.PointSize(10);
            Gl.Begin(PrimitiveType.Points);
            foreach (var p in points.Points())
            {
                if (p.Grid == null) continue;
                Gl.Color3(1F, 1F, 1F);
                decimal px = p.XExpr.Value, pz = p.ZExpr.Value;
                Gl.Vertex3((double)px, (double)p.Grid.Expr.Value(px, pz)+0.05, (double)pz);
            }
            Gl.End();

            Gl.Color3(1F, 1F, 1F);

            Gl.Begin(PrimitiveType.Lines);

            double size = 2 / Math.Sin(2 * Math.PI / 5);
            double R = 10;

            void vert3(Vertex3d v)
            {
                v.Normalize();
                v *= R;
                Gl.Vertex3(v.x, v.y, v.z);
            };

            double alpha = Math.Asin(1 / (2 * Math.Cos(3 * Math.PI / 10D)));
            double Y = Math.Cos(Math.PI - 2 * alpha);

            void tri(Vertex3d v0, Vertex3d v1, Vertex3d v2)
            {
                int f = 10;

                var v01 = (v1 - v0) / f;
                var v02 = (v2 - v0) / f;

                for (int h = 0; h < f; h++)
                {
                    for (int w = 0; w < (f - h); w++)
                    {
                        vert3(v0 + v01 * w + v02 * h);
                        vert3(v0 + v01 * (w + 1) + v02 * h);

                        vert3(v0 + v01 * (w + 1) + v02 * h);
                        vert3(v0 + v01 * w + v02 * (h + 1));

                        vert3(v0 + v01 * w + v02 * (h + 1));
                        vert3(v0 + v01 * w + v02 * h);
                    }
                }
            };

            for (int i = 0; i < 5; i++)
            {
                tri(new Vertex3d(Math.Cos(Math.PI * 2 / 5 * i), 1 - Y, Math.Sin(Math.PI * 2 / 5 * i)),
                    new Vertex3d(Math.Cos(Math.PI * 2 / 5 * (i + 1)), 1 - Y, Math.Sin(Math.PI * 2 / 5 * (i + 1))),
                    new Vertex3d(0, 1, 0)
                );

                tri(new Vertex3d(Math.Cos(Math.PI * 2 / 5 * i), 1 - Y, Math.Sin(Math.PI * 2 / 5 * i)),
                    new Vertex3d(Math.Cos(Math.PI * 2 / 5 * (i + 1)), 1 - Y, Math.Sin(Math.PI * 2 / 5 * (i + 1))),
                    new Vertex3d(Math.Cos(Math.PI * 2 / 10 * (i*2 + 1)), Y - 1, Math.Sin(Math.PI * 2 / 10 * (i*2 + 1)))
                );

                tri(new Vertex3d(Math.Cos(Math.PI * 2 / 5 * (i + 1)), 1 - Y, Math.Sin(Math.PI * 2 / 5 * (i + 1))),
                    new Vertex3d(Math.Cos(Math.PI * 2 / 10 * (i * 2 + 1)), Y - 1, Math.Sin(Math.PI * 2 / 10 * (i * 2 + 1))),
                    new Vertex3d(Math.Cos(Math.PI * 2 / 10 * (i * 2 + 3)), Y - 1, Math.Sin(Math.PI * 2 / 10 * (i * 2 + 3)))
                );

                tri(new Vertex3d(Math.Cos(Math.PI * 2 / 10 * (i*2 + 1)), Y - 1, Math.Sin(Math.PI * 2 / 10 * (i * 2 + 1))),
                    new Vertex3d(Math.Cos(Math.PI * 2 / 10 * (i*2 + 3)), Y - 1, Math.Sin(Math.PI * 2 / 10 * (i * 2 + 3))),
                    new Vertex3d(0, -1, 0)
                );
            }
            
            Gl.End();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!keysPressed.Contains(e.KeyCode))
                keysPressed.Add(e.KeyCode);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
        }

        private void PlotterForm_KeyUp(object sender, KeyEventArgs e)
        {
            keysPressed.Remove(e.KeyCode);
        }
    }
}
