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
        public GridsControl()
        {
            InitializeComponent();
            type.DataSource = Enum.GetValues(typeof(GridType));
            type.SelectedItem = GridType.Plain;

            gridsList.add.Click += (s, e) =>
                gridsList.AddAndSelect(new GridConstructor("grid_" + Grids.List.Count));
            type.SelectionChangeCommitted += (s, e) => type.DataBindings["SelectedItem"]?.WriteValue();

            gridsList.comboBox.DataSource = Grids.List;
            gridsList.comboBox.DisplayMember = "Name";
            gridsList.comboBox.SelectedIndexChanged += OnGridSelectChanged;
            //button1.Click += (s, e) => colorDialog1.ShowDialog();
        }

        private void OnGridSelectChanged(object sender, EventArgs e)
        {
            GridConstructor CurrentGridConstructor = gridsList.SelectedItem as GridConstructor;
            bool selected = CurrentGridConstructor != null;
            gridsConstructor.Visible = colorControl.Visible = selected && type.SelectedItem != null;

            if (!selected) return;
            void bind(string propName, Control c, object src, string member)
            {
                c.DataBindings.Clear();
                if(src.GetType().GetProperty("BackColor") != null)
                    c.DataBindings.Add("BackColor", src, "BackColor", false, DataSourceUpdateMode.OnPropertyChanged);
                c.DataBindings.Add(propName, src, member, false, DataSourceUpdateMode.OnPropertyChanged);
            }

            bind("SelectedItem", type, CurrentGridConstructor, "Type");
            bind("Text", name, CurrentGridConstructor, "Name");
            bind("Text", expression, CurrentGridConstructor, "ValueExpressionString");

            colorControl.Constructor = CurrentGridConstructor.ColorConstructor;
            /*void bindColor(ColorComponent cc) =>
                bind(
                    "Text",
                    gridConstructor.Controls[Enum.GetName(typeof(ColorComponent), cc).ToLower()],
                    CurrentGridConstructor[cc],
                    "Expression"
                );

            foreach (var cc in Enum.GetValues(typeof(ColorComponent))) bindColor((ColorComponent)cc);*/
        }
    }
}
