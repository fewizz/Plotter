using Parser;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Plotter.ColorComponent;

namespace Plotter
{
    public partial class ColorControl : UserControl
    {
        public Func<ColorComponent, Exception> StatusUpdater;

        public StatusTextBox this[ColorComponent cc]
                => Controls[Enum.GetName(typeof(ColorComponent), cc).ToLower()] as StatusTextBox;

        public ColorControl()
        {
            InitializeComponent();

            chooseColorButton.Click += (s, e) =>
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    red.Text = (colorDialog.Color.R / 255.0).ToString();
                    green.Text = (colorDialog.Color.G / 255.0).ToString();
                    blue.Text = (colorDialog.Color.B / 255.0).ToString();
                    alpha.Text = (colorDialog.Color.A / 255.0).ToString();
                }
            };

            void updater(ColorComponent cc)
            {
                this[cc].StatusUpdater = () => StatusUpdater?.Invoke(cc);
            }

            updater(Red);
            updater(Green);
            updater(Blue);
            updater(Alpha);
        }

        private void ColorControl_Load(object sender, EventArgs e)
        {
            red.Text = "0";
            green.Text = "0";
            blue.Text = "0";
            alpha.Text = "1";
        }
    }
}
