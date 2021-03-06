﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;
using OpenGL;
using Parser;

namespace Plotter
{
    public partial class PlotterForm : Form
    {
        public static PlotterForm Instance;
        TextRenderer textRenderer;
        public bool timeStop = true;
        DateTime prevTime = DateTime.Now;
        public decimal timeMult = 1;

        Vertex3f speed = new Vertex3f();

        public PlotterForm()
        {
            Instance = this;
            InitializeComponent();
            Load += (s, e) => tabPage3.Show();

            gl.KeyUp += OnKeyUp;
            gl.KeyDown += OnKeyDown;

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

                Camera.Rotate(nrot - rot);

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
            Gl.DebugMessageCallback(
                (source, type, id, severity, length, message, userParam) =>
                    Console.WriteLine(Marshal.PtrToStringAnsi(message)),
                IntPtr.Zero
            );
            Gl.ClearColor(0, 0, 0, 1F);
            Gl.Enable(EnableCap.Blend);
            Gl.Enable(EnableCap.AlphaTest);
            Gl.AlphaFunc(AlphaFunction.Greater, 0);
            Gl.Enable(EnableCap.LineSmooth);
            Gl.BlendFuncSeparate(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha, BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha);
            textRenderer = new TextRenderer(new Font("Consolas", 50));
            Sky.Instance.Init();
        }

        List<Keys> keysPressed = new List<Keys>();

        private void glRender(object sender, GlControlEventArgs e)
        {
            Gl.Enable(EnableCap.DepthTest);
            Gl.Viewport(0, 0, gl.Width, gl.Height);
            Camera.Projection = Matrix4x4f.Perspective(
                100,
                (float)gl.Width / gl.Height,
                0.1F,
                100
            );

            Vertex3f dir = new Vertex3f();
            if (keysPressed.Contains(Keys.W))
                dir.z--;
            if (keysPressed.Contains(Keys.S))
                dir.z++;
            if (keysPressed.Contains(Keys.A))
                dir.x--;
            if (keysPressed.Contains(Keys.D))
                dir.x++;
            if (keysPressed.Contains(Keys.Space))
                dir.y++;
            if (keysPressed.Contains(Keys.ShiftKey))
                dir.y--;
            dir.Normalize();
            float a = 1000;
            var timeChange = DateTime.Now - prevTime;
            var speedModule = speed.Module();

            Vertex3f speedChange;
            if (dir.ModuleSquared() != 0) speedChange = dir * a * timeChange.TotalSeconds;
            else speedChange = speed.Normalized * -Math.Min(speedModule, a * timeChange.TotalSeconds);
            speed += speedChange;
            speedModule = speed.Module();
            if(speedModule != 0)
                speed *= Math.Min(50, speedModule) / speedModule;

            prevTime = DateTime.Now;
            if (!timeStop)
                Program.TimeArg.Value += (decimal)(timeChange.TotalSeconds)*timeMult;

            Camera.Translate(speed*(float)timeChange.TotalSeconds);

            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Camera.Apply();

            Sky.Instance.Draw();

            foreach (var g in GridsControl.List) {
                Gl.Color3(1F, 1F, 1F);
                g.Grid.Draw();
            }

            foreach (var point in Points.List) point.Draw(textRenderer);

            Gl.Color3(1F, 1F, 1F);

            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadMatrix((float[])Matrix4x4f.Ortho2D(0, gl.Width, 0, gl.Height));
            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.LoadIdentity();

            Gl.PushMatrix();
            Gl.Scale(0.3, 0.3, 1);
            textRenderer.Draw("Время " + string.Format("{0:F2}", Program.TimeArg.Value));
            Gl.Translate(0, textRenderer.Font.Height, 0);
            textRenderer.Draw("Множитель " + string.Format("{0:F2}", timeMult));
            Gl.PopMatrix();

            Gl.Translate(0, gl.Height, 0);
            Gl.Scale(0.3, 0.3, 1);

            Gl.Translate(0, -textRenderer.Font.Height, 0);
            textRenderer.Draw("x " + (int)Camera.Position.x);
            Gl.Translate(0, -textRenderer.Font.Height, 0);
            textRenderer.Draw("y " + (int)Camera.Position.y);
            Gl.Translate(0, -textRenderer.Font.Height, 0);
            textRenderer.Draw("z " + (int)Camera.Position.z);

            Gl.LoadIdentity();

            var rot = Camera.RotationMatrix;
            Gl.Disable(EnableCap.DepthTest);
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
                Gl.Scale(0.3, 0.3, 1);
                textRenderer.Draw(ch);
                Gl.PopMatrix();
            }

            Gl.Translate(gl.Width - 60, 30, 0);
            drawAxis(rot.Row0, new Vertex3f(1F, 0, 0), 'x');
            drawAxis(rot.Row1, new Vertex3f(0, 1F, 0), 'y');
            drawAxis(rot.Row2, new Vertex3f(0, 0, 1F), 'z');
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            keysPressed.Remove(e.KeyCode);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) timeStop = !timeStop;
            if (e.KeyCode == Keys.F6) timeMult*=2.0m;
            if (e.KeyCode == Keys.F4) timeMult/=2.0m;
            if (e.KeyCode == Keys.F2) timeMult += e.Alt ? 0.01m : 1;
            if (e.KeyCode == Keys.F1) timeMult -= e.Alt ? 0.01m : 1;
            if (e.KeyCode == Keys.F8)
            {
                Program.TimeArg.Value = 0;
                timeMult = 0;
            }
            if (!keysPressed.Contains(e.KeyCode))
                keysPressed.Add(e.KeyCode);
        }
    }
}
