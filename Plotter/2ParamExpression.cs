using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser;

namespace Plotter
{
    public class Param2Expression
    {
        Argument argX = new Argument("x");
        Argument argY = new Argument("z");
        public IExpression expr = null;

        public Param2Expression(Func<Argument, Argument, IExpression> f)
        {
            expr = f(argX, argY);
        }

        public decimal Value(decimal x, decimal y)
        {
            argX.Value = x;
            argY.Value = y;
            return expr.Value;
        }
    }
}
