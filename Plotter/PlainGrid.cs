using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    class PlainGrid : Grid
    {
        private Argument x, z;

        public PlainGrid()
        : base(new PlainGridRenderer(40, 0.5f) ){
            x = new Argument("x");
            z = new Argument("z");
        }

        protected override object[] ColorComponentExpressionArguments()
        {
            return new object[] { x, "y", z, t};
        }

        protected override object[] ValueExpressionArguments()
        {
            return new object[] { x, z, t };
        }
    }
}
