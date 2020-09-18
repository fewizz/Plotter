using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenGL;
using Solver;

namespace Plotter
{
    public partial class PlotterForm : Form
    {
        Camera cam = new Camera();
        Grid grid = new Grid();

        public PlotterForm()
        {
            InitializeComponent();

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

        private void glLoad(object sender, EventArgs e)
        {
            Gl.ClearColor(0, 0, 0.2F, 1F);
            Gl.Enable(EnableCap.DepthTest);
        }

        List<Keys> keysPressed = new List<Keys>();

        private void glRender(object sender, GlControlEventArgs e)
        {
            //gl.Size = new Size(Size.Width, Size.Height - gl.Location.Y);
            Gl.Viewport(0, 0, gl.Width, gl.Height);
            cam.Projection = Matrix4x4f.Perspective(
                100,
                (float)gl.Width / gl.Height,
                1,
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

            grid.Draw();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {

            if(textBox1.Focused && e.KeyCode == Keys.Enter)
            {
                Param2Expression expr = null;

                try
                {
                    expr = new Param2Expression(
                        (Arg x, Arg y) =>
                            Parser.Parse(
                                textBox1.Text,
                                new Argument { Arg = x, Name = "x" },
                                new Argument { Arg = y, Name = "y" }
                            )

                    );
                }
                catch { }

                grid.Update(
                    new Size(40, 40),
                    0.5M,
                    expr
                );
            }
            if (!textBox1.Focused && !keysPressed.Contains(e.KeyCode))
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
