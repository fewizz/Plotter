using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using OpenGL;
using Solver;

namespace Plotter
{
    class Grid
    {
        Param2Expression expr;
        decimal[,] values;
        decimal step;

        decimal XFor(int x)
        {
            return step * (x - values.GetLength(0) / 2M);
        }

        decimal YFor(int y)
        {
            return step * (y - values.GetLength(1) / 2M);
        }

        public Grid(Size s, decimal step, Param2Expression expr)
        {
            this.expr = expr;
            Resize(s, step);
        }

        public void Resize(Size s, decimal step)
        {
            values = new decimal[s.Width, s.Height];
            this.step = step;

            for(int x = 0; x < values.GetLength(0); x++) 
                for (int y = 0; y < values.GetLength(1); y++)
                    values[x, y] = expr.Value(XFor(x), YFor(y));
        }

        public void Expr(Param2Expression expr)
        {
            this.expr = expr;
        }

        public void Draw()
        {
            bool begin = false;
            //Gl.Begin(PrimitiveType.TriangleStrip);

            for (int x = 0; x < values.GetLength(0) - 1; x++)
            {
                Vertex4f color = new Vertex4f();

                Action<int, int> vert = (int x0, int y0) => {
                    decimal v = values[x0, y0];
                    if (v == decimal.MaxValue) color.w = 0;
                    else color.w = 1F;
                    if (v == decimal.MaxValue)
                    {
                        if (begin)
                        {
                            Gl.End();
                            begin = false;
                        }
                    }
                    else
                    {
                        if (!begin) {
                            Gl.Begin(PrimitiveType.TriangleStrip);
                            begin = true;
                        }
                        Gl.Color3((float[])color);
                        Gl.Vertex3(
                            (float)XFor(x0),
                            (float)v,
                            (float)YFor(y0)
                        );
                    }
                };

                for (int y = 0; y < values.GetLength(1); y++)
                {
                    color = new Vertex4f(0, 0, 1f, 1F);
                    vert(x, y);
                    color = new Vertex4f(1, 0, 0F, 1F);
                    vert(x + 1, y);
                }
                vert(x + 1, values.GetLength(1) - 1);
                vert(x + 1, 0);
            }

            if(begin)
                Gl.End();
        }
    }
}
