using Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Plotter
{
    public partial class GridsForm : Form
    {
        enum GridType
        {
            Plain, Sphere
        }

        public class GridConstructor
        {
            public class ColorConstructor {
                public ColorComponent Component { get; private set; }
                public Color BackColor { get { return Program.ColorByStatus(grid.ColorComponentsParseExceptions[Component] == null); } }
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

            public Color BackColor { get { return Program.ColorByStatus(Grid.ValueParseException == null); } }

            public ColorConstructor this[ColorComponent cc] { get { return ColorConstructors[cc]; } }

            public Grid Grid { get; set; }

            public GridConstructor(string name)
            {
                Grid = new Grid(new SphereGridRenderer(), "x", "z");

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

        readonly public BindingList<GridConstructor> GridConstructors = new BindingList<GridConstructor>();
        public GridConstructor this[string name] { get { return gridsList[name] as GridConstructor; } }

        GridConstructor CurrentGridConstructor { get { return gridsList.SelectedItem as GridConstructor; } }

        public GridsForm()
        {
            InitializeComponent();
            type.DataSource = Enum.GetValues(typeof(GridType));
            type.SelectedItem = GridType.Plain;

            gridsList.add.Click += (s, e) =>
                gridsList.AddAndSelect(new GridConstructor("grid_" + GridConstructors.Count));

            gridsList.comboBox.DataSource = GridConstructors;
            gridsList.comboBox.DisplayMember = "Name";

            gridsList.comboBox.ItemNameTextBox(name);
            gridsList.comboBox.SelectedIndexChanged += OnGridSelectChanged;
        }

        private void OnGridSelectChanged(object sender, EventArgs e)
        {
            bool selected = CurrentGridConstructor != null;
            gridConstructor.Visible = selected && type.SelectedItem != null;

            if (!selected) return;
            void bind(Control tb, object src, string member)
            {
                tb.DataBindings.Clear();
                tb.DataBindings.Add("BackColor", src, "BackColor", false, DataSourceUpdateMode.OnPropertyChanged);
                tb.DataBindings.Add("Text", src, member, false, DataSourceUpdateMode.OnPropertyChanged);
            }

            name.Text = CurrentGridConstructor.Name;
            bind(expression, CurrentGridConstructor, "ValueExpr");

            void bindColor(ColorComponent cc) =>
                bind(
                    gridConstructor.Controls[Enum.GetName(typeof(ColorComponent), cc).ToLower()],
                    CurrentGridConstructor[cc],
                    "Expression"
                );

            foreach (var cc in Enum.GetValues(typeof(ColorComponent))) bindColor((ColorComponent)cc);
        }
    }
}
