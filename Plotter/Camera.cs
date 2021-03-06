﻿using System;
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
        static public Matrix4x4f Projection = Matrix4x4f.Identity;
        static public Matrix4x4f ModelView
        {
            get
            {
                var m = Matrix4x4f.Identity;
                m.RotateX(-Rotation.x);
                m.RotateY(-Rotation.y);
                m.Translate(-Position.x, -Position.y, -Position.z);
                return m;
            }
        }
        static public Matrix4x4f Combined => Projection * ModelView;
        static public Vertex3f Position { get; private set; }
        static public Vertex2f Rotation { get; private set; }

        static public void Rotate(Vertex2f v)
        {
            Rotation += v / 3F;
            Rotation = new Vertex2f(Math.Max(Math.Min(Rotation.x, 90), -90), Rotation.y);
        }

        static public void Translate(Vertex3f trans)
        {
            trans /= 5F;
            trans = RotationMatrix * trans;
            Position += trans;
        }

        static public Matrix3x3f RotationMatrix { get
            {
                var rotM = Matrix3x3f.RotatedY(Rotation.y);
                rotM.RotateX(Rotation.x);
                return rotM;
            }
        }

        static public void Apply()
        {
            ApplyProjection();
            ApplyModelview();
        }

        static public void ApplyProjection()
        {
            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadMatrix((float[])Projection);
        }

        static public void ApplyModelview()
        {
            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.LoadMatrix((float[])ModelView);
        }
    }
}
