using Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plotter
{
    public partial class StatusTextBox : TextBox
    {
        ToolTip toolTip;
        public Func<string> StatusUpdater;

        public StatusTextBox()
        {
            BackColor = Program.ColorByStatus(false);
            InitializeComponent();
            toolTip = new ToolTip();
            TextChanged += (s, e) =>
            {
                string message = StatusUpdater?.Invoke();
                BackColor = Program.ColorByStatus((message == null).ToStatus());
                if (message != null)
                    toolTip.SetToolTip(this, message);
                else toolTip.RemoveAll();
            };
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
