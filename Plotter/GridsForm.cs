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
    public partial class GridsForm : Form
    {
        public class GridConstructor
        {
            string expr, r, g, b, a;
            public string Name { get; set; }
            public string Expr { get { return expr; } set { expr = value; Grid.UpdateExpr(expr); } }
            public string Red { get { return r; } set { r = value; UpdateColor(); } }
            public string Green { get { return g; } set { g = value; UpdateColor(); } }
            public string Blue { get { return b; } set { b = value; UpdateColor(); } }
            public string Alpha { get { return a; } set { a = value; UpdateColor(); } }
            public GridRenderer Grid { get; set; }
            public bool Status { get; set; }

            public void UpdateColor()
            {
                Status = Grid.UpdateColorExpr(r, g, b, a);
            }

            public GridConstructor(string name)
            {
                Grid = new PlainGridRenderer(100, 0.5f);
                Name = name;
                Expr = "0";
                r = "y*1.5";
                g = "1.5 - |y|";
                b = "-y*1.5";
                a = "1";
                UpdateColor();
            }
        }

        public GridConstructor this[string name]
        {
            get {
                return gridsList[name] as GridConstructor;
            }
        }

        public IEnumerable<GridConstructor> GridConstructors()
        {
            return gridsList.Items.Cast<GridConstructor>();
        }

        GridConstructor CurrentGridConstructor { get { return (GridConstructor)gridsList.SelectedItem; } }

        public GridsForm()
        {
            InitializeComponent();
            buttonDelete.Click += (s, e) => gridsList.Items.Remove(gridsList.SelectedItem);
            buttonAdd.Click += (s, e) => gridsList.Items.Add(new GridConstructor("grid_" + gridsList.Items.Count));
            gridsList.DisplayMember = "Name";

            TextBox name = gridConstructor.Controls["name"] as TextBox;

            name.TextChanged += (s, e) =>
            {
                if (gridsList.ContainsItemWithTextExceptCurrent(name.Text) )
                    name.BackColor = Color.Red;
                else
                {
                    CurrentGridConstructor.Name = name.Text;
                    name.BackColor = SystemColors.Window;
                    gridsList.RefreshSelectedItem();
                }
            };
        }

        private void OnGridSelectChanged(object sender, EventArgs e)
        {
            void bind(string tbn, string prop)
            {
                if (CurrentGridConstructor == null) return;
                var tb = gridConstructor.Controls[tbn] as TextBox;
                tb.DataBindings.Clear();
                tb.DataBindings.Add("Text", CurrentGridConstructor, prop, false, DataSourceUpdateMode.OnPropertyChanged);
            }

            bool selected = gridsList.SelectedItem != null;
            buttonDelete.Enabled = gridConstructor.Visible = selected;
            if (selected)
                gridConstructor.BackColor = CurrentGridConstructor.Status ? SystemColors.Window : Color.Red;

            bind("name", "Name");
            bind("expr", "Expr");
            bind("r", "Red");
            bind("g", "Green");
            bind("b", "Blue");
            bind("a", "Alpha");
        }
    }
}
