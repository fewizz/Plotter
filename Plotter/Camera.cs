using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

namespace Plotter
{
    class Camera
    {
        public Matrix4x4f Projection = Matrix4x4f.Identity;
        public Vertex3f Position;
        public Vertex2f Rotation;

        public void ApplyTransformations()
        {
            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadMatrix((float[])Projection);
            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.LoadIdentity();
            Gl.Rotate(-Rotation.x, 1, 0, 0);
            Gl.Rotate(-Rotation.y, 0, 1, 0);
            Gl.Translate(-Position.x, -Position.y, -Position.z);
        }
    }
}
