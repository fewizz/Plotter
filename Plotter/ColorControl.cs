using Parser;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Plotter.ColorComponent;

namespace Plotter
{
    public partial class ColorControl : UserControl
    {
        public Func<ColorComponent, Status> StatusUpdater;
        //public delegate void ExpressionChangedHandler(ColorComponent cc, IExpression e);
        //public event ExpressionChangedHandler ExpressionChanged;

        /*ColorConstructor constructor;
        public ColorConstructor Constructor
        {
            get { return constructor; }
            set
            {
                constructor = value;

                if (constructor == null) return;

                void bindColor(ColorComponent cc) {
                    var db = Controls[Enum.GetName(typeof(ColorComponent), cc).ToLower()].DataBindings;
                    db.Clear();
                    db.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
                    db.Add("BackColor", constructor[cc], "BackColor");
                    db.Add("Text", constructor[cc], "ExpressionString");
                }

                foreach (var cc in ColorComponents.ARRAY) bindColor(cc);
            }
        }*/

        public StatusEditBox this[ColorComponent cc]
                => Controls[Enum.GetName(typeof(ColorComponent), cc).ToLower()] as StatusEditBox;

        public ColorControl()
        {
            InitializeComponent();
            chooseColorButton.Click += (s, e) =>
            {
                if(colorDialog.ShowDialog() == DialogResult.OK)
                {
                    red.Text = (colorDialog.Color.R / 255.0).ToString();
                    green.Text = (colorDialog.Color.G / 255.0).ToString();
                    blue.Text = (colorDialog.Color.B / 255.0).ToString();
                    alpha.Text = (colorDialog.Color.A / 255.0).ToString();
                }
            };

            void updater(ColorComponent cc) {
                this[cc].StatusUpdater = str => StatusUpdater?.Invoke(cc) ?? Status.Error;
            }

            updater(Red);
            updater(Green);
            updater(Blue);
            updater(Alpha);
        }
    }
}
