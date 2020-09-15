using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    class Div : Expression2
    {
        public Div(IExpression e1, IExpression e2)
        : base(e1, e2) { }

        public override decimal Value()
        {
            if (expr2.Value() == 0) return decimal.MaxValue;
            return expr1.Value() / expr2.Value();
        }
    }

    class Division : IAlgebraicOperation
    {
        public static Division INSTANCE = new Division();

        private Division() { }

        public Expression2 CreateExpression(IExpression e1, IExpression e2)
        {
            return new Div(e1, e2);
        }
        char IAlgebraicOperation.OperatorSymbol()
        {
            return '/';
        }
    }
}
