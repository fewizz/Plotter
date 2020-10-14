using System;
using System.Windows.Forms;
using static Plotter.ColorComponent;

namespace Plotter
{
    public partial class ColorControl : UserControl
    {
        ColorConstructor constructor;
        public ColorConstructor Constructor
        {
            get { return constructor; }
            set
            {
                constructor = value;

                if (constructor == null) return;

                void bindColor(ColorComponent cc) {
                    Control c = Controls[Enum.GetName(typeof(ColorComponent), cc).ToLower()];
                    c.DataBindings.Clear();
                    c.DataBindings.Add("BackColor", constructor[cc], "BackColor", false, DataSourceUpdateMode.OnPropertyChanged);
                    c.DataBindings.Add("Text", constructor[cc], "ExpressionString", false, DataSourceUpdateMode.OnPropertyChanged);
                }

                foreach (var cc in ColorComponents.ARRAY) bindColor(cc);
            }
        }

        public ColorControl()
        {
            InitializeComponent();
            chooseColorButton.Click += (s, e) =>
            {
                if(colorDialog.ShowDialog() == DialogResult.OK)
                {
                    constructor[Red].ExpressionString = (colorDialog.Color.R / 256.0).ToString();
                    constructor[Green].ExpressionString = (colorDialog.Color.G / 256.0).ToString();
                    constructor[Blue].ExpressionString = (colorDialog.Color.B / 256.0).ToString();
                    constructor[Alpha].ExpressionString = (colorDialog.Color.A / 256.0).ToString();
                }
            };
        }
    }
}
