using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using OpenGL;
using Parser;

namespace Plotter
{
    public class Points
    {
        public class Point : INotifyPropertyChanged
        {
            public Grid Grid => GridControl?.Grid;
            public GridControl GridControl { get; set; }

            public class CoordinateComponent
            {
                string expressionString;
                public System.Exception Ex { get; protected set; }

                public string ExpressionString
                {
                    get { return expressionString; }
                    set
                    {
                        expressionString = value;
                        Expression = Parser.Parser.TryParse(value, out System.Exception m, Program.TimeArg);
                        Ex = m;
                    }
                }
                public IExpression Expression { get; private set; }
            }

            public CoordinateComponent X, Z;

            public event PropertyChangedEventHandler PropertyChanged;
            string name;
            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }

            public Point(string n)
            {
                Name = n;
                X = new CoordinateComponent() { ExpressionString = "0" };
                Z = new CoordinateComponent() { ExpressionString = "0" };
            }

            public void Draw(TextRenderer tr)
            {
                if (Grid == null || X.Expression == null || Z.Expression == null
                    || Grid.ValueExpression == null) return;
                var coord = Grid.CartesianCoord(X.Expression.Value, Z.Expression.Value);
                Gl.Disable(EnableCap.DepthTest);
                Gl.Disable(EnableCap.Texture2d);
                Gl.PushMatrix();
                Gl.Translate(coord.x, coord.y, coord.z);
                Gl.PointSize(10);
                Gl.Color3(1F, 1F, 1F);
                Gl.Begin(PrimitiveType.Points);
                Gl.Vertex3(0, 0, 0);
                Gl.End();

                var clip = Camera.Combined * new Vertex4f(coord.x, coord.y, coord.z);

                if (clip.z >= -clip.w && clip.z <= clip.w)
                {
                    var nds = clip.div(clip.w);
                    var window = new Vertex2f(PlotterForm.Instance.gl.Width, PlotterForm.Instance.gl.Height);
                    var wc = new Vertex2f((window.x / 2) * (1 + nds.x), (window.y / 2) * (1 + nds.y));
                    Gl.LoadIdentity();
                    Gl.MatrixMode(MatrixMode.Projection);
                    Gl.PushMatrix();
                    Gl.LoadIdentity();
                    Gl.Ortho(0, window.x, 0, window.y, 0.1, 1);
                    Gl.MatrixMode(MatrixMode.Modelview);
                    Gl.Translate(wc.x, wc.y, -0.1F);
                    Gl.Translate(0, 70, 0);
                    tr.DrawCentered(Name, 0.3F);
                    Gl.Translate(0, -20, 0);
                    tr.DrawCentered("x: " + string.Format("{0:F2}", coord.x).Trim(), 0.3F);
                    Gl.Translate(0, -20, 0);
                    tr.DrawCentered("y: " + string.Format("{0:F2}", coord.y).Trim(), 0.3F);
                    Gl.Translate(0, -20, 0);
                    tr.DrawCentered("z: " + string.Format("{0:F2}", coord.z).Trim(), 0.3F);
                    Gl.MatrixMode(MatrixMode.Projection);
                    Gl.PopMatrix();
                    Gl.MatrixMode(MatrixMode.Modelview);
                }
                Gl.PopMatrix();
                Gl.Enable(EnableCap.DepthTest);
                Gl.Enable(EnableCap.Texture2d);
            }
        }

        readonly public static BindingList<Point> List = new BindingList<Point>();
    }
}
