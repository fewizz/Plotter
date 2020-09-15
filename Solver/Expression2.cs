using System;

namespace Solver
{
    public abstract class Expression2 : IExpression
    {
        protected IExpression expr1;
        protected IExpression expr2;

        protected Expression2(IExpression e1, IExpression e2)
        {
            if (e1 == null || e2 == null) throw new NullReferenceException();
            expr1 = e1; expr2 = e2;
        }

        public abstract decimal Value();
    }
}
