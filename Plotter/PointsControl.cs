﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Plotter
{
    public partial class PointsControl : UserControl
    {
        Points.Point CurrentPoint => (Points.Point)pointsList.SelectedItem;

        public PointsControl()
        {
            InitializeComponent();
            pointsList.comboBox.DisplayMember = "Name";
            gridsList.DataSource = Grids.List;
            gridsList.DisplayMember = "Name";
            gridsList.SelectedIndexChanged += (s, e) =>
            {
                if (CurrentPoint != null)
                    CurrentPoint.GridConstructor = gridsList.SelectedItem as Grids.GridConstructor;
            };

            pointsList.comboBox.DataSource = Plotter.Points.List;
            pointsList.comboBox.SelectedIndexChanged += OnPointSelectChanged;
            pointsList.add.Click += (s, e)
                => pointsList.AddAndSelect(new Points.Point("point_" + pointsList.Items.Count));
        }

        private void OnPointSelectChanged(object sender, EventArgs e)
        {
            bool selected = CurrentPoint != null;
            pointConstructor.Visible = selected;

            if (!selected) return;

            void bind(Control tb, object src, string member)
            {
                tb.DataBindings.Clear();
                tb.DataBindings.Add("BackColor", src, "BackColor", false, DataSourceUpdateMode.OnPropertyChanged);
                tb.DataBindings.Add("Text", src, member, false, DataSourceUpdateMode.OnPropertyChanged);
            }

            bind(name, CurrentPoint, "Name");
            bind(x, CurrentPoint.X, "ExpressionText");
            bind(z, CurrentPoint.Z, "ExpressionText");
            gridsList.SelectedItem = CurrentPoint.GridConstructor;
        }

        public IEnumerable<Points.Point> Points => pointsList.Items.Cast<Points.Point>();
    }
}
