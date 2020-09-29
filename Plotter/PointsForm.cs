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

namespace Plotter
{

    public partial class PointsForm : Form
    {
        public class Point
        {
            GridsForm.GridConstructor grid;
            public string Name { get; set; }
            public string GridName { get { return grid == null ? "" : grid.Name;  } }
            public GridRenderer Grid { get { return grid == null ? null : grid.Grid; } }
            public string X { get; private set; }
            public IExpression XExpr { get; private set; }
            public string Z { get; private set; }
            public IExpression ZExpr { get; private set; }
            public bool Status { get; private set; }

            public Point(string n)
            {
                Name = n;
                Update(n, grid, "0", "0");
            }

            public void Update(string n, GridsForm.GridConstructor grid, string x, string z)
            {
                Name = n;
                this.grid = grid;
                try
                {
                    X = x;
                    Z = z;
                    XExpr = Parser.Parser.Parse(x);
                    ZExpr = Parser.Parser.Parse(z);
                    Status = grid != null;
                }
                catch { Status = false; }
            }

            public override string ToString()
            {
                return Name;
            }
        }

        GridsForm grids;

        TextBox PointName { get { return (TextBox)pointConstructor.Controls["name"]; } }
        TextBox GridName { get { return (TextBox)pointConstructor.Controls["grid_name"]; } }
        TextBox X { get { return (TextBox)pointConstructor.Controls["x"]; } }
        TextBox Z { get { return (TextBox)pointConstructor.Controls["z"]; } }

        Point CurrentPoint { get { return (Point)pointsList.SelectedItem; } }

        public PointsForm(GridsForm gf)
        {
            InitializeComponent();
            grids = gf;
        }

        private void OnAdd(object sender, EventArgs e)
        {
            pointsList.Items.Add(new Point("point_" + pointsList.Items.Count));
        }

        private void OnPointSelectChanged(object sender, EventArgs e)
        {
            bool selected = pointsList.SelectedItem != null;
            buttonDelete.Enabled = selected;
            if (selected)
            {
                PointName.Text = CurrentPoint.Name;
                GridName.Text = CurrentPoint.GridName;
                X.Text = CurrentPoint.X;
                Z.Text = CurrentPoint.Z;
                pointConstructor.BackColor = CurrentPoint.Status ? SystemColors.Window : Color.Red;
            }
            pointConstructor.Visible = selected;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && pointConstructor.Visible)
            {
                CurrentPoint.Update(PointName.Text, grids[GridName.Text], X.Text, Z.Text);
                pointConstructor.BackColor = CurrentPoint.Status ? SystemColors.Window : Color.Red;

                pointsList.Items[pointsList.SelectedIndex] = CurrentPoint;
            }
        }

        public IEnumerable<Point> Points()
        {
            return pointsList.Items.Cast<Point>();
        }

    }
}
