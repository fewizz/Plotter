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
    public partial class SphereGridControl : GridControl
    {
        public SphereGridControl() {
            Init(new SphereGrid());
            InitializeComponent();
            Frequency.StatusUpdater = () =>
            {
                IExpression e = Parser.Parser.TryParse(Frequency.Text, new object[0]);
                if (e == null) return Status.Error;
                (Grid as SphereGrid).Frequency = (int)e.Value;
                return Status.Ok;
            };
            Frequency.Text = "10";
            expression.Text = "10";
        }
    }
}
