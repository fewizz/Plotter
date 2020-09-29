using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plotter
{
    public partial class ListBoxUnique : ListBox
    {
        public bool Refreshing { get; private set; }

        public ListBoxUnique()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

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

        public bool ContainsItemWithText(string str)
        {
            return GetItemWithText(str) != null;
        }

        public bool ContainsItemWithTextExceptCurrent(string str)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (i == SelectedIndex) continue;
                if (GetItemText(Items[i]).Equals(str)) return true;
            }
            return false;
        }

        public object GetItemWithText(string str)
        {
            foreach (var it in Items)
                if (GetItemText(it).Equals(str)) return it;
            return null;
        }

        public int IndexOfItemWithText(string str)
        {
            object o = GetItemWithText(str);
            if (o == null) return -1;
            return Items.IndexOf(o);
        }

        public object this[string Text]
        {
            get { return GetItemWithText(Text); }
        }

        override protected void OnSelectedIndexChanged(EventArgs e)
        {
            if (Refreshing) return;
            base.OnSelectedIndexChanged(e);
        }
    }
}
