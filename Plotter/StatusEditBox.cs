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
    public partial class StatusEditBox : TextBox
    {
        public Func<StatusEditBox, Status> StatusUpdater;
        //public Statuc

        public StatusEditBox()
        {
            BackColor = Program.ColorByStatus(false);
            InitializeComponent();
            TextChanged += (s, e) =>
            {
                Status status = StatusUpdater?.Invoke(this) ?? Status.Error;
                BackColor = Program.ColorByStatus(status);
            };
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
