using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    class Pow : Expression2
    {
        public Pow(IExpression e1, IExpression e2)
        : base(e1, e2) { }

        public override decimal Value()
        {
            return (decimal) Math.Pow((double) expr1.Value(), (double) expr2.Value());
        }
    }

    class Power : IAlgebraicOperation
    {
        public static Power INSTANCE = new Power();

        private Power() { }

        public Expression2 CreateExpression(IExpression e1, IExpression e2)
        {
            return new Pow(e1, e2);
        }
        char IAlgebraicOperation.OperatorSymbol()
        {
            return '^';
        }
    }
}
