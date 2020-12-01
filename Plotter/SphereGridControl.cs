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
                IExpression e = Parser.Parser.TryParse(Frequency.Text, out Exception ex);

                if (e == null) return ex;
                if (e.Value % 1 != 0) return new Exception("Частота есть число целое");
                if (e.Value <= 0) return new Exception("Частота должна быть положительной");
                (Grid as SphereGrid).Frequency = (int)e.Value;
                return null;
            };

            Load += (s, e) =>
            {
                Frequency.Text = "10";
                expression.Text = "10";

                colorControl1[ColorComponent.Red].Text = "|normal_x|";
                colorControl1[ColorComponent.Green].Text = "|normal_y|";
                colorControl1[ColorComponent.Blue].Text = "|normal_z|";
                colorControl1[ColorComponent.Alpha].Text = "1";
            };
        }
    }
}
