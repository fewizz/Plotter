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
                public Color BackColor { get { return ColorByStatus(Expression != null); } }
            }


            public string Name { get; set; }

            public CoordinateConstructor X, Z;

            public Color BackColor { get { return ColorByStatus(GridConstructor != null); } }

            public Point(string n)
            {
                Name = n;
                X = new CoordinateConstructor() { ExpressionText = "0" };
                Z = new CoordinateConstructor() { ExpressionText = "0" };
            }
        }

        Point CurrentPoint { get { return (Point)pointsList.comboBox.SelectedItem; } }

        public PointsForm(GridsForm grids)
        {
            InitializeComponent();
            pointsList.comboBox.DisplayMember = "Name";
            gridsList.DataSource = grids.GridConstructors;
            gridsList.DisplayMember = "Name";
            gridsList.SelectedIndexChanged += (s, e) =>
            {
                if(CurrentPoint != null)
                    CurrentPoint.GridConstructor = gridsList.SelectedItem as GridConstructor;
            };
            pointsList.comboBox.SelectedIndexChanged += OnPointSelectChanged;
            pointsList.add.Click += (s, e)
                => pointsList.AddAndSelect(new Point("point_" + pointsList.comboBox.Items.Count));
            name.TextChanged += (s, e) =>
            {
                CurrentPoint.Name = name.Text;
                pointsList.comboBox.RefreshSelectedItem();
            };
        }

        private void OnPointSelectChanged(object sender, EventArgs e)
        {
            bool selected = pointsList.comboBox.SelectedItem != null;
            pointConstructor.Visible = selected;

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
            return pointsList.comboBox.Items.Cast<Point>();
        }

    }
}
