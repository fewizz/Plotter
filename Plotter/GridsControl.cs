using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Plotter.Grids;

namespace Plotter
{
    public partial class GridsControl : UserControl
    {
        public GridConstructor this[string name] { get { return gridsList[name] as GridConstructor; } }

        GridConstructor CurrentGridConstructor { get { return gridsList.SelectedItem as GridConstructor; } }

        public GridsControl()
        {
            InitializeComponent();
            type.DataSource = Enum.GetValues(typeof(GridType));
            type.SelectedItem = GridType.Plain;

            gridsList.add.Click += (s, e) =>
                gridsList.AddAndSelect(new GridConstructor("grid_" + Grids.List.Count));
            type.SelectionChangeCommitted += (s, e) =>
                type.DataBindings["SelectedItem"]?.WriteValue();

            gridsList.comboBox.DataSource = Grids.List;
            gridsList.comboBox.DisplayMember = "Name";
            gridsList.comboBox.SelectedIndexChanged += OnGridSelectChanged;
        }

        private void OnGridSelectChanged(object sender, EventArgs e)
        {
            bool selected = CurrentGridConstructor != null;
            gridConstructor.Visible = selected && type.SelectedItem != null;

            if (!selected) return;
            void bind(string propName, Control tb, object src, string member)
            {
                tb.DataBindings.Clear();
                if(src.GetType().GetProperty("BackColor") != null)
                    tb.DataBindings.Add("BackColor", src, "BackColor", false, DataSourceUpdateMode.OnPropertyChanged);
                tb.DataBindings.Add(propName, src, member, false, DataSourceUpdateMode.OnPropertyChanged);
            }

            bind("SelectedItem", type, CurrentGridConstructor, "Type");
            bind("Text", name, CurrentGridConstructor, "Name");
            bind("Text", expression, CurrentGridConstructor, "ValueExpr");

            void bindColor(ColorComponent cc) =>
                bind(
                    "Text",
                    gridConstructor.Controls[Enum.GetName(typeof(ColorComponent), cc).ToLower()],
                    CurrentGridConstructor[cc],
                    "Expression"
                );

            foreach (var cc in Enum.GetValues(typeof(ColorComponent))) bindColor((ColorComponent)cc);
        }
    }
}
