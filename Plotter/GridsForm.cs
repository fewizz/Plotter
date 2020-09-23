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
            public string Expr { get; set; }
            public string Red { get; set; }
            public string Green { get; set; }
            public string Blue { get; set; }
            public string Alpha { get; set; }
            public Grid Grid { get; set; }
            public bool Status { get; set; }

            public void Update()
            {
                Status = Grid.Update(Expr, Red, Green, Blue, Alpha);
            }

            override public string ToString()
            {
                return Expr;
            }
        }

        TextBox Expr { get { return (TextBox)gridConstructor.Controls["expr"]; } }
        TextBox Red { get { return (TextBox)gridConstructor.Controls["r"]; } }
        TextBox Green { get { return (TextBox)gridConstructor.Controls["g"]; } }
        TextBox Blue { get { return (TextBox)gridConstructor.Controls["b"]; } }
        TextBox Alpha { get { return (TextBox)gridConstructor.Controls["a"]; } }
        GridConstructor CurrentGridConstructor { get { return (GridConstructor)gridsList.SelectedItem; } }

        public GridsForm()
        {
            InitializeComponent();
            buttonDelete.Click += (s, e) => gridsList.Items.Remove(gridsList.SelectedItem);
        }

        private void onAdd(object sender, EventArgs e)
        {
            gridsList.Items.Add(
                new GridConstructor()
                {
                    Expr = "0",
                    Red = "y*1.5",
                    Green = "1.5 - |y|",
                    Blue = "-y*1.5",
                    Alpha = "1",
                    Grid = new Grid(100, 0.5f)
                }
            ); ;
        }

        private void OnGridSelectChanged(object sender, EventArgs e)
        {
            bool selected = gridsList.SelectedItem != null;
            buttonDelete.Enabled = selected;
            if (selected)
            {
                Expr.Text = CurrentGridConstructor.Expr;
                Red.Text = CurrentGridConstructor.Red;
                Green.Text = CurrentGridConstructor.Green;
                Blue.Text = CurrentGridConstructor.Blue;
                Alpha.Text = CurrentGridConstructor.Alpha;
                gridConstructor.BackColor = CurrentGridConstructor.Status ? SystemColors.Window : Color.Red;
            }
            gridConstructor.Visible = selected;
        }

        public IEnumerable<GridConstructor> GridConstructors()
        {
            return gridsList.Items.Cast<GridConstructor>();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && gridConstructor.Visible)
            {
                CurrentGridConstructor.Expr = Expr.Text;
                CurrentGridConstructor.Red = Red.Text;
                CurrentGridConstructor.Green = Green.Text;
                CurrentGridConstructor.Blue = Blue.Text;
                CurrentGridConstructor.Alpha = Alpha.Text;
                CurrentGridConstructor.Update();
                gridConstructor.BackColor = CurrentGridConstructor.Status ? SystemColors.Window : Color.Red;

                gridsList.Items[gridsList.SelectedIndex] = CurrentGridConstructor;
            }
        }
    }
}
