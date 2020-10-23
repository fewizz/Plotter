using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace Plotter
{
    public partial class SkyControl : UserControl
    {
        public SkyControl()
        {
            InitializeComponent();
            colorControl1.StatusUpdater = (cc) => {
                return Sky.Instance.TryParseColorComponent(cc, colorControl1[cc].Text);
            };
            colorControl1[ColorComponent.Red].Text = "0";
            colorControl1[ColorComponent.Green].Text = "0";
            colorControl1[ColorComponent.Blue].Text = "0";
            colorControl1[ColorComponent.Alpha].Text = "1";
        }
    }
}
