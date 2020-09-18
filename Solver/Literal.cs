using System;

namespace Solver
{
    public class Literal : IExpression
    {
        decimal val;
        public Literal(decimal valFactory)
        {
            val = valFactory;
        }

        public decimal Value => val;
    }
}
