using System;

namespace Solver
{
    public class Constant : IExpression
    {
        decimal val;
        public Constant(decimal valFactory)
        {
            val = valFactory;
        }

        public decimal Value => val;
    }
}
