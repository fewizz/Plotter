using Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plotter
{
    public partial class GridsForm : Form
    {
        public class GridConstructor
        {
            static Color ColorByStatus(bool ok) { return ok ? SystemColors.Window : Color.Red; }

            public class ColorConstructor {
                public ColorComponent Component { get; private set; }
                public bool Status { get { return grid.ColorComponentsParseExceptions[Component] == null; } }
                public Color BackColor { get { return ColorByStatus(Status); } }
                string expression;
                readonly Grid grid;

                public ColorConstructor(ColorComponent cc, Grid g)
                {
                    Component = cc;
                    grid = g;
                }

                public string Expression {
                    get { return expression; }
                    set { expression = value; grid.TryParseColorComponent(Component, value); }
                }
            }

            string expr;
            Dictionary<ColorComponent, ColorConstructor> ColorConstructors =
                new Dictionary<ColorComponent, ColorConstructor>();

            public string Name { get; set; }
            public string ValueExpr { 
                get { return expr; }
                set { expr = value; Grid.TryParseValueExpression(expr); }
            }

            public bool Status { get { return Grid.ValueParseException == null; } }
            public Color BackColor { get { return ColorByStatus(Status); } }

            public ColorConstructor this[ColorComponent cc] { get { return ColorConstructors[cc]; } }

            public Grid Grid { get; set; }

            public GridConstructor(string name)
            {
                Grid = new PlainGrid();

                void addColor(ColorComponent cc, string expr)
                    => ColorConstructors.Add(cc, new ColorConstructor(cc, Grid) { Expression = expr });

                addColor(ColorComponent.Red, "y*1.5");
                addColor(ColorComponent.Green, "1.5 - |y|");
                addColor(ColorComponent.Blue, "-y*1.5");
                addColor(ColorComponent.Alpha, "1");

                Name = name;
                ValueExpr = "0";
            }
        }

        public GridConstructor this[string name] { get { return gridsList[name] as GridConstructor; } }

        public IEnumerable<GridConstructor> GridConstructors() { return gridsList.Items.Cast<GridConstructor>(); }

        GridConstructor CurrentGridConstructor { get { return (GridConstructor)gridsList.SelectedItem; } }

        public GridsForm()
        {
            InitializeComponent();
            buttonDelete.Click += (s, e) => gridsList.Items.Remove(gridsList.SelectedItem);
            buttonAdd.Click += (s, e) => gridsList.Items.Add(new GridConstructor("grid_" + gridsList.Items.Count));
            gridsList.DisplayMember = "Name";

            TextBox name = gridConstructor.Controls["Name"] as TextBox;

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
            bool selected = gridsList.SelectedItem != null;
            buttonDelete.Enabled = gridConstructor.Visible = selected;

            if (!selected) return;
            void bind(string textBoxName, object src, string member, bool bindBackColor = true)
            {
                var tb = gridConstructor.Controls[textBoxName] as TextBox;
                tb.DataBindings.Clear();
                if (bindBackColor)
                    tb.DataBindings.Add("BackColor", src, "BackColor", false, DataSourceUpdateMode.OnPropertyChanged);
                tb.DataBindings.Add("Text", src, member, false, DataSourceUpdateMode.OnPropertyChanged);
            }

            bind("name", CurrentGridConstructor, "Name", false);
            bind("expression", CurrentGridConstructor, "ValueExpr");

            void bindColor(ColorComponent cc) =>
                bind(Enum.GetName(typeof(ColorComponent), cc).ToLower(), CurrentGridConstructor[cc], "Expression");

            foreach (var cc in Enum.GetValues(typeof(ColorComponent))) bindColor((ColorComponent)cc);
        }
    }
}
