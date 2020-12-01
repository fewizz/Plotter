using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plotter
{
    public partial class GridsControl : UserControl
    {
        public static BindingList<GridControl> List = new BindingList<GridControl>();

        public GridControl SelectedGridControl => GridsList.ComboBox.SelectedItem as GridControl;
        public GridControl CurrentGridControl => Panel.Controls.Count > 0 ? Panel.Controls[0] as GridControl : null;

        public GridsControl()
        {
            InitializeComponent();
            GridsList.DataSource = List;
            GridsList.ComboBox.SelectedValueChanged += OnGridSelectChanged;
            GridsList.DisplayMember = "GridName";
            GridsList.Add.Click += (s, e) =>
            {
                var gt = new GridTypeDialog();
                if (gt.ShowDialog() == DialogResult.OK)
                {
                    GridControl gc;
                    if (gt.Value == GridType.Plain)
                        gc = new PlainGridControl();
                    else
                        gc = new SphereGridControl();
                    GridsList.AddAndSelect(gc);
                }
            };
        }

        private void OnGridSelectChanged(object sender, EventArgs e)
        {
            if (SelectedGridControl == CurrentGridControl) return;
            Panel.Controls.Clear();
            if (SelectedGridControl == null) return;
            Panel.Controls.Add(SelectedGridControl);
            CurrentGridControl.Dock = DockStyle.Fill;
        }
    }
}
