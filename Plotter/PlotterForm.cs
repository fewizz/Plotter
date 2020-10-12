using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using OpenGL;
using Parser;

namespace Plotter
{
    public partial class PlotterForm : Form
    {
        Camera cam = new Camera();
        GridsForm gridsForm;
        PointsForm pointsForm;
        TextRenderer textRenderer;

        public PlotterForm()
        {
            InitializeComponent();

            gridsForm = new GridsForm();
            pointsForm = new PointsForm();
            FormClosed += (s, e) =>
            {
                pointsForm.Close();
                gridsForm.Close();
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

                cam.Rotate(nrot - rot);

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
            gridsForm.Left = Right;
            gridsForm.Top = Top;
            gridsForm.Show();

            pointsForm.Left = Left - pointsForm.Width;
            pointsForm.Top = Top;
            pointsForm.Show();
        }

        private void glLoad(object sender, EventArgs e)
        {
            Gl.ClearColor(0, 0, 0.2F, 1F);
            Gl.Enable(EnableCap.DepthTest);
            Gl.Enable(EnableCap.Blend);
            Gl.Enable(EnableCap.AlphaTest);
            Gl.AlphaFunc(AlphaFunction.Greater, 0.1f);
            Gl.Enable(EnableCap.LineSmooth);
            Gl.BlendFuncSeparate(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha, BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha);
            textRenderer = new TextRenderer(new Font("Arial", 50));
        }

        List<Keys> keysPressed = new List<Keys>();

        private void glRender(object sender, GlControlEventArgs e)
        {
            Program.TimeArg.Value = (decimal)((DateTime.Now - Program.START).TotalMilliseconds / 1000D);
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
            if (keysPressed.Contains(Keys.C))
                Program.START = DateTime.Now;

            cam.Translate(trans);

            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            cam.ApplyTransformations();

            foreach (var g in Grids.List) {
                Gl.Color3(1F, 1F, 1F);
                g.Grid.Draw(cam);
            }

            Gl.PointSize(10);
            Gl.Begin(PrimitiveType.Points);
            foreach (var p in pointsForm.pointsControl.Points)
            {
                if (p.GridConstructor == null || p.X.Expression == null || p.Z.Expression == null) continue;
                Gl.Color3(1F, 1F, 1F);
                if (p.GridConstructor.Grid.ValueExpression == null) continue;
                Gl.Vertex3(p.GridConstructor.Grid.CartesianCoord(p.X.Expression.Value, p.Z.Expression.Value));
            }
            Gl.End();

            Gl.Color3(1F, 1F, 1F);

            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadMatrix((float[])Matrix4x4f.Ortho2D(0, Width, 0, Height));
            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.LoadIdentity();
            Gl.Translate(0, 0, 0);
            var rot = cam.RotationMatrix;

            void drawAxis(Vertex3f v, Vertex3f c, char ch)
            {
                float rad = 30;
                Gl.Begin(PrimitiveType.Lines);
                Gl.Color3(c);
                Gl.Vertex3(v.x * rad, v.y * rad, 0);
                Gl.Vertex3(0, 0, 0);
                Gl.End();

                Gl.PushMatrix();
                Gl.Translate(v.x * rad, v.y * rad, 0);
                Gl.Scale(0.3, 0.3, 0);
                textRenderer.Render(ch);
                Gl.PopMatrix();
            }

            Gl.Translate(30, 30, 0);
            drawAxis(rot.Row0, new Vertex3f(1F, 0, 0), 'x');
            drawAxis(rot.Row1, new Vertex3f(0, 1F, 0), 'y');
            drawAxis(rot.Row2, new Vertex3f(0, 0, 1F), 'z');
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!keysPressed.Contains(e.KeyCode))
                keysPressed.Add(e.KeyCode);
        }

        private void PlotterForm_KeyUp(object sender, KeyEventArgs e)
        {
            keysPressed.Remove(e.KeyCode);
        }
    }
}
