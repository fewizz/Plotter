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
    public class Camera
    {

        public Matrix4x4f Projection = Matrix4x4f.Identity;
        public Vertex3f Position { get; private set; }
        public Vertex2f Rotation { get; private set; }

        public void Rotate(Vertex2f v)
        {
            Rotation += v / 3F;
            Rotation = new Vertex2f(Math.Max(Math.Min(Rotation.x, 90), -90), Rotation.y);
        }

        public void Translate(Vertex3f trans)
        {
            trans /= 5F;
            trans = RotationMatrix * trans;
            Position += trans;
        }

        public Matrix3x3f RotationMatrix { get
            {
                var rotM = Matrix3x3f.RotatedY(Rotation.y);
                rotM.RotateX(Rotation.x);
                return rotM;
            }
        }

        public void ApplyTransformations()
        {
            ApplyProjection();
            ApplyModelview();
        }

        public void ApplyProjection()
        {
            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadMatrix((float[])Projection);
        }

        public void ApplyModelview()
        {
            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.LoadIdentity();
            ApplyRotation();
            Gl.Translate(-Position.x, -Position.y, -Position.z);
        }

        public void ApplyRotation()
        {
            Gl.Rotate(-Rotation.x, 1, 0, 0);
            Gl.Rotate(-Rotation.y, 0, 1, 0);
        }
    }
}
