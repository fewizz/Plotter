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
        public Func<Status> StatusUpdater;

        public StatusTextBox()
        {
            BackColor = Program.ColorByStatus(false);
            InitializeComponent();
            TextChanged += (s, e) =>
            {
                Status status = StatusUpdater?.Invoke() ?? Status.Error;
                BackColor = Program.ColorByStatus(status);
            };
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
