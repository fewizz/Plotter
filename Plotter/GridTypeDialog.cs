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
    public partial class GridTypeDialog : Form
    {
        public Grids.GridType Value => (Grids.GridType)comboBox1.SelectedItem;

        public GridTypeDialog()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(Grids.GridType));
            button1.DialogResult = DialogResult.OK;
            AcceptButton = button1;
        }
    }
}
