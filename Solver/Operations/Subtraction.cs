using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    class Sub : Expression2
    {
        public Sub(IExpression e1, IExpression e2)
        : base(e1, e2) { }

        public override decimal Value()
        {
            return expr1.Value() - expr2.Value();
        }
    }

    class Subtraction : IAlgebraicOperation
    {
        public static Subtraction INSTANCE = new Subtraction();

        private Subtraction() { }

        public Expression2 CreateExpression(IExpression e1, IExpression e2)
        {
            return new Sub(e1, e2);
        }
        char IAlgebraicOperation.OperatorSymbol()
        {
            return '-';
        }
    }
}
