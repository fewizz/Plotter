using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plotter
{
    public partial class GridControl : UserControl, INotifyPropertyChanged
    {
        public Grid Grid { get; protected set; }

        string gridName = "Grid";
        public string GridName {
            get { return gridName; }
            set { gridName = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GridName")); }
        }

        public GridControl()
        {
            InitializeComponent();
        }

        protected void Init(Grid g)
        {
            Grid = g;
            expression.StatusUpdater = () => g.TryParseValueExpression(expression.Text);
            colorControl1.StatusUpdater = cc => g.TryParseColorComponent(cc, colorControl1[cc].Text);

            name.DataBindings.Add(new Binding("Text", this, "GridName", false, DataSourceUpdateMode.OnPropertyChanged));
            expression.Text = "0";
            colorControl1[ColorComponent.Red].Text = "y*1.5";
            colorControl1[ColorComponent.Green].Text = "1.5 - |y|";
            colorControl1[ColorComponent.Blue].Text = "-y*1.5";
            colorControl1[ColorComponent.Alpha].Text = "1";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
