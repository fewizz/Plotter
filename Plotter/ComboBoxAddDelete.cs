using System;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;

namespace Plotter
{
    public partial class ComboBoxAddDelete : UserControl
    {

        public class ComboBoxExtended : ComboBox
        {
            public delegate void ItemRefreshedEventHandler(int index);
            public event ItemRefreshedEventHandler ItemRefreshed;

            public ComboBoxExtended()
            {
            }

            void OnItemRefreshed(int index)
            {
                ItemRefreshedEventHandler handler = ItemRefreshed;
                handler?.Invoke(index);
            }

            public bool Refreshing { get; private set; }
            public new void RefreshItem(int index)
            {
                Refreshing = true;
                base.RefreshItem(index);
                Refreshing = false;
                OnItemRefreshed(index);
            }

            public void RefreshSelectedItem()=>RefreshItem(SelectedIndex);

            override protected void OnSelectedIndexChanged(EventArgs e)
            {
                if (Refreshing) return;
                base.OnSelectedIndexChanged(e);
            }

            public void OnSelectedIndexChanged()=>OnSelectedIndexChanged(null);


            public void ItemNameTextBox(TextBox tb) {

                tb.TextChanged += (s, e) => {
                    SelectedItem.GetType().GetProperty(DisplayMember).SetValue(SelectedItem, tb.Text);
                    RefreshSelectedItem();
                };
            }
        }

        public ComboBox.ObjectCollection Items => comboBox.Items;
        public object SelectedItem => comboBox.SelectedItem;

        public ComboBoxAddDelete()
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

        public object this[string Text] => comboBox.FindStringExact(Text);
    }
}
