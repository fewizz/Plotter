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
        public GridType Value => (GridType)comboBox1.SelectedItem;

        public GridTypeDialog()
        {
            InitializeComponent();
            comboBox1.DataSource = GridType.Types;
            comboBox1.DisplayMember = "CoordinateSystemTypeName";
            button1.DialogResult = DialogResult.OK;
            AcceptButton = button1;
        }
    }
}
