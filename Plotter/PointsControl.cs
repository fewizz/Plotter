using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Plotter
{
    public partial class PointsControl : UserControl
    {
        Points.Point CurrentPoint => PointsList.SelectedItem as Points.Point;

        public PointsControl()
        {
            InitializeComponent();
            PointsList.DisplayMember = "Name";
            GridsList.DataSource = GridsControl.List;
            GridsList.DisplayMember = "GridName"; 
            GridsList.SelectedIndexChanged += (s, e) =>
            {
                if (CurrentPoint != null)
                    CurrentPoint.GridControl = GridsList.SelectedItem as GridControl;
            };

            x.StatusUpdater = () =>
            {
                CurrentPoint.X.ExpressionString = x.Text;
                return (CurrentPoint.X.Expression != null).ToStatus();
            };
            z.StatusUpdater = () =>
            {
                CurrentPoint.Z.ExpressionString = z.Text;
                return (CurrentPoint.Z.Expression != null).ToStatus();
            };
            PointsList.DataSource = Points.List;
            PointsList.ComboBox.SelectedValueChanged += OnPointSelectChanged;
            PointsList.Add.Click += (s, e)
                => PointsList.AddAndSelect(new Points.Point("Point"));
        }

        private void OnPointSelectChanged(object sender, EventArgs e)
        {
            bool selected = CurrentPoint != null;
            pointConstructor.Visible = selected;

            if (!selected) return;

            void bind(Control tb, object src, string member)
            {
                tb.DataBindings.Clear();
                tb.DataBindings.Add("Text", src, member, false, DataSourceUpdateMode.OnPropertyChanged);
            }

            bind(name, CurrentPoint, "Name");
            bind(x, CurrentPoint.X, "ExpressionString");
            bind(z, CurrentPoint.Z, "ExpressionString");
            GridsList.SelectedItem = CurrentPoint.GridControl;
        }
    }
}
