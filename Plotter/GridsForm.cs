using Solver;
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
            public string Name { get; set; }
            public string Expr { get; set; }
            public Grid Grid { get; set; }
            public bool Status { get; set; }

            public void Update()
            {
                Status = Grid.Update(Expr);
            }

            override public string ToString()
            {
                return Name;
            }
        }

        TextBox ExprTextBox { get { return (TextBox)gridConstructor.Controls["expr"]; } }
        GridConstructor CurrentGridConstructor { get { return (GridConstructor)gridsList.SelectedItem; } }

        public GridsForm()
        {
            InitializeComponent();
            buttonDelete.Click += (s, e) => gridsList.Items.Remove(gridsList.SelectedItem);
        }

        private void onAdd(object sender, EventArgs e)
        {
            string name = TextDialog("Enter name");
            if (name == string.Empty) return;
            gridsList.Items.Add(
                new GridConstructor()
                {
                    Name = name,
                    Expr = "",
                    Grid = new Grid(100, 0.5f)
                }
            );
        }

        private string TextDialog(string text)
        {
            Form f = new Form();
            FormClosed += (s, e) => f.Close();
            f.ClientSize = new Size(200, f.ClientSize.Height);
            f.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            f.StartPosition = FormStartPosition.CenterScreen;

            var label = new Label() { Text = text, Top = 6 };
            f.Controls.Add(label);
            label.Left = (f.ClientSize.Width - TextRenderer.MeasureText(label.Text, label.Font).Width) / 2;

            var tb = new TextBox()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Consolas", 12)
            };
            f.Controls.Add(tb);
            tb.Location = new Point(5, label.Bottom + 5);
            tb.Size = new Size(f.ClientSize.Width - 10, label.Bottom + 5);

            var ok = new Button() { Text = "Ok", DialogResult = DialogResult.OK, Top = tb.Bottom + 5 };
            ok.Click += (s, e) => f.Close();
            f.Controls.Add(ok);
            f.AcceptButton = ok;
            ok.Left = (f.ClientSize.Width - ok.ClientSize.Width) / 2;

            f.ClientSize = new Size(f.ClientSize.Width, ok.Bottom + 5);

            return f.ShowDialog() == DialogResult.OK ? tb.Text : "";
        }

        private void OnGridSelectChanged(object sender, EventArgs e)
        {
            bool selected = gridsList.SelectedItem != null;
            buttonDelete.Enabled = selected;
            if (selected)            
                ExprTextBox.Text = CurrentGridConstructor.Expr;

            gridConstructor.Visible = selected;
        }

        public IEnumerable<GridConstructor> GridConstructors()
        {
            return gridsList.Items.Cast<GridConstructor>();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && ExprTextBox.Focused)
            {
                CurrentGridConstructor.Expr = ExprTextBox.Text;
                CurrentGridConstructor.Update();
                ExprTextBox.BackColor = CurrentGridConstructor.Status ? SystemColors.Window : Color.Red;
            }
        }
    }
}
