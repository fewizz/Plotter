using Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Plotter.GridsForm;

namespace Plotter
{

    public partial class PointsForm : Form
    {
        public static Color ColorByStatus(bool status)
        {
            return status ? SystemColors.Window : Color.Red;
        }

        public class Point
        {
            public GridConstructor GridConstructor { get; set; }

            public class CoordinateConstructor {
                string expression;
                public string ExpressionText {
                    get { return expression; }
                    set
                    {
                        try
                        {
                            expression = value;
                            IExpression e = Parser.Parser.Parse(value);
                            Expression = e;
                        }
                        catch { }
                    }
                }
                public IExpression Expression { get; private set; }
                public bool Status { get { return Expression != null; } }
                public Color BackColor { get { return ColorByStatus(Status); } }
            }


            public string Name { get; set; }

            public CoordinateConstructor X, Z;

            public bool Status { get { return GridConstructor != null; } }

            public Color BackColor { get { return ColorByStatus(Status); } }

            public Point(string n)
            {
                Name = n;
                X = new CoordinateConstructor() { ExpressionText = "0" };
                Z = new CoordinateConstructor() { ExpressionText = "0" };
            }

            public override string ToString()
            {
                return Name;
            }
        }

        GridsForm grids;

        Point CurrentPoint { get { return (Point)pointsList.SelectedItem; } }

        public PointsForm(GridsForm gf)
        {
            InitializeComponent();
            grids = gf;
            gridsList.DataSource = gf.GridConstructors;
            gridsList.DisplayMember = "Name";
            gridsList.SelectedIndexChanged += (s, e) =>
            {
                if(CurrentPoint != null)
                    CurrentPoint.GridConstructor = gridsList.SelectedItem as GridConstructor;
            };
        }

        private void OnAdd(object sender, EventArgs e)
        {
            pointsList.Items.Add(new Point("point_" + pointsList.Items.Count));
        }

        private void OnPointSelectChanged(object sender, EventArgs e)
        {
            bool selected = pointsList.SelectedItem != null;
            buttonDelete.Enabled = pointConstructor.Visible = selected;

            if (!selected) return;

            void bind(Control tb, object src, string member, bool bindBackColor = true)
            {
                tb.DataBindings.Clear();
                if (bindBackColor)
                    tb.DataBindings.Add("BackColor", src, "BackColor", false, DataSourceUpdateMode.OnPropertyChanged);
                tb.DataBindings.Add("Text", src, member, false, DataSourceUpdateMode.OnPropertyChanged);
            }

            bind(name, CurrentPoint, "Name", false);
            bind(x, CurrentPoint.X, "ExpressionText");
            bind(z, CurrentPoint.Z, "ExpressionText");
            gridsList.SelectedItem = CurrentPoint.GridConstructor;
        }

        public IEnumerable<Point> Points()
        {
            return pointsList.Items.Cast<Point>();
        }

    }
}
