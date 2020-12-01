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
    public partial class StatusTextBox : RichTextBox
    {
        ToolTip toolTip;
        public Func<Exception> StatusUpdater;

        public StatusTextBox()
        {
            BackColor = Program.ColorByStatus(false);
            InitializeComponent();
            toolTip = new ToolTip();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void StatusTextBox_TextChanged(object sender, EventArgs e)
        {
            System.Exception message = StatusUpdater?.Invoke();
            BackColor = Program.ColorByStatus(true);
            string text = Text;
            int start = SelectionStart;

            void def() {
                Clear();
                SelectionBullet = true;
                SelectionColor = Color.Black;
                SelectedText = text;
                SelectionBullet = false;
            }

            if (message != null)
            {
                toolTip.SetToolTip(this, message.Message);
                if (message is ParserException pe)
                {
                    Clear();
                    BackColor = Color.LightPink;
                    SelectionColor = Color.Black;
                    SelectedText = text.Remove(pe.Token.Index);
                    SelectionBullet = true;
                    SelectionColor = Program.ColorByStatus(false);
                    SelectedText = text.Substring(pe.Token.Index, pe.Token.Length);
                    SelectionBullet = false;
                    SelectionColor = Color.Black;
                    SelectedText = text.Substring(pe.Token.Index + pe.Token.Length);
                }
                else
                {
                    BackColor = Program.ColorByStatus(false);
                    def();
                }
            }
            else
            {
                def();
                toolTip.RemoveAll();
            }

            SelectionStart = start;
        }
    }
}
