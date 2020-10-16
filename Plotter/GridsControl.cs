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
        public static BindingList<GridControl> CONTROLS = new BindingList<GridControl>();

        public GridControl SelectedGridControl => gridsList.comboBox.SelectedItem as GridControl;
        public GridControl CurrentGridControl => panel.Controls.Count > 0 ? panel.Controls[0] as GridControl : null;

        public GridsControl()
        {
            InitializeComponent();
            gridsList.comboBox.DataSource = CONTROLS;
            gridsList.comboBox.SelectedValueChanged += OnGridSelectChanged;
            gridsList.comboBox.DisplayMember = "GridName";
            gridsList.add.Click += (s, e) => gridsList.AddAndSelect(new PlainGridControl());
        }

        private void OnGridSelectChanged(object sender, EventArgs e)
        {
            if (SelectedGridControl == CurrentGridControl) return;
            panel.Controls.Clear();
            if (SelectedGridControl == null) return;
            panel.Controls.Add(SelectedGridControl);
            CurrentGridControl.Dock = DockStyle.Fill;
        }
    }
}
