using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Plotter
{
    public partial class ComboBoxAD : UserControl
    {
        public class CComboBox : ComboBox
        {
            public bool Refreshing { get; private set; }
            public new void RefreshItem(int index)
            {
                Refreshing = true;
                base.RefreshItem(index);
                Refreshing = false;
            }

            public void RefreshSelectedItem()
            {
                RefreshItem(SelectedIndex);
            }

            override protected void OnSelectedIndexChanged(EventArgs e)
            {
                if (Refreshing) return;
                base.OnSelectedIndexChanged(e);
            }

            public void OnSelectedIndexChanged()
            {
                OnSelectedIndexChanged(null);
            }
        }

        public ComboBoxAD()
        {
            InitializeComponent();
            delete.Enabled = comboBox.SelectedItem != null;

            delete.Click += (s, e) => RemoveCurrent();
            comboBox.SelectedIndexChanged += (s, e) => delete.Enabled = comboBox.SelectedItem != null;
        }

        public void RemoveCurrent()
        {
            int index = comboBox.SelectedIndex;
            if (comboBox.DataSource == null) comboBox.Items.RemoveAt(index);
            else (comboBox.DataSource as IList).RemoveAt(index);
            comboBox.SelectedIndex = --index;
            if(index == -1) comboBox.OnSelectedIndexChanged();
        }

        public void AddAndSelect(object o)
        {
            if (comboBox.DataSource == null) comboBox.Items.Add(o);
            else (comboBox.DataSource as IList).Add(o);
            comboBox.SelectedItem = o;
            comboBox.OnSelectedIndexChanged();
        }

        public object this[string Text]
        {
            get { return comboBox.FindStringExact(Text); }
        }
    }
}
