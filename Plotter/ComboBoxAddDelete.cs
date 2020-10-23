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
            get { return ComboBox.DataSource; }
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
            ComboBox.SelectedValueChanged += (s, e) => Remove.Enabled = ComboBox.SelectedItem != null;
        }

        public void RemoveCurrent()
        {
            int index = ComboBox.SelectedIndex;
            IList c = null;
            if (DataSource == null) c = Items;
            else c = (DataSource as IList);

            c.RemoveAt(index);

            ComboBox.SelectedIndex = --index;
        }

        public void AddAndSelect(object o)
        {
            int index;
            if (ComboBox.DataSource == null)
                index = ComboBox.Items.Add(o);
            else
                index = (ComboBox.DataSource as IList).Add(o);
            ComboBox.SelectedIndex = -1;
            ComboBox.SelectedIndex = index;
        }
    }
}
