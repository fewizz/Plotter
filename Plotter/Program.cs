using Parser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plotter
{
    static class Program
    {
        public static DateTime START = DateTime.Now;
        public static Argument TimeArg = new Argument("t");
        public static readonly float R = 50;

        public static Color ColorByStatus(bool status)
        {
            return status ? SystemColors.Window : Color.Red;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PlotterForm());
        }
    }
}
