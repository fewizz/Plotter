using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solver;

namespace Plotter
{
    class Param2Expression
    {
        Arg argX = new Arg();
        Arg argY = new Arg();
        IExpression expr = null;

        public Param2Expression(Func<Arg, Arg, IExpression> f)
        {
            CreateExpression(f);
        }

        public void CreateExpression(Func<Arg, Arg, IExpression> f)
        {
            expr = f(argX, argY);
        }

        public decimal Value(decimal x, decimal y)
        {
            argX.Val = x;
            argY.Val = y;
            return expr.Value();
        }
    }
}
