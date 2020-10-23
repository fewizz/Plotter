using System;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Plotter
{
    public partial class ComboBoxAddDelete : UserControl
    {
        public ComboBox.ObjectCollection Items => ComboBox.Items;
        public object SelectedItem => ComboBox.SelectedItem;

        public string DisplayMember
        {
            set
            {
                ComboBox.DisplayMember = value;
            }
        }

        public object DataSource
        {
            set
            {
                ComboBox.DataSource = value;
            }
        }

        public ComboBoxAddDelete()
        {
            InitializeComponent();
            Remove.Enabled = ComboBox.SelectedItem != null;

            Remove.Click += (s, e) => RemoveCurrent();
            ComboBox.SelectedIndexChanged += (s, e) => Remove.Enabled = ComboBox.SelectedItem != null;
        }

        public void RemoveCurrent()
        {
            int index = ComboBox.SelectedIndex;
            if (ComboBox.DataSource == null) ComboBox.Items.RemoveAt(index);
            else (ComboBox.DataSource as IList).RemoveAt(index);
            ComboBox.SelectedIndex = --index;
        }

        public void AddAndSelect(object o)
        {
            if (ComboBox.DataSource == null) ComboBox.Items.Add(o);
            else (ComboBox.DataSource as IList).Add(o);
            ComboBox.SelectedItem = o;
        }
    }
}
