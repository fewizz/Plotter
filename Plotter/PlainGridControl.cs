using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parser;

namespace Plotter
{
    public partial class PlainGridControl : GridControl
    {
        public PlainGridControl()
        {
            InitializeComponent();
            Grid = new PlainGrid();
            step.StatusUpdater = (b) =>
            {
                IExpression e = Parser.Parser.TryParse(step.Text, new object[0]);
                if (e == null) return Status.Error;
                (Grid as PlainGrid).step = (float)e.Value;
                return Status.Ok;
            };
            step.Text = "0.5";
        }
    }
}
