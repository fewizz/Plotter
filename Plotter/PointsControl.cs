﻿using System;
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
                {
                    CurrentPoint.GridControl = GridsList.SelectedItem as GridControl;
                    bool ex = CurrentPoint.Grid != null;
                    if (ex)
                    {
                        label4.Text = CurrentPoint.Grid.Arg0Name.ToUpper();
                        label5.Text = CurrentPoint.Grid.Arg1Name.ToUpper();
                    }

                    label4.Visible = ex;
                    label5.Visible = ex;
                    x.Visible = ex;
                    z.Visible = ex;
                    label2.Visible = ex;
                }
            };

            x.StatusUpdater = () =>
            {
                CurrentPoint.X.ExpressionString = x.Text;
                return CurrentPoint.X.Ex;
            };
            z.StatusUpdater = () =>
            {
                CurrentPoint.Z.ExpressionString = z.Text;
                return CurrentPoint.Z.Ex;
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
            x.Text = CurrentPoint.X.ExpressionString;
            z.Text = CurrentPoint.Z.ExpressionString;

            GridsList.SelectedItem = CurrentPoint.GridControl;
        }
    }
}
