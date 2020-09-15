using System;

namespace Solver
{
    
    public interface IExpression
    {
        decimal Value { get; }
    }

    public class Expression : IExpression
    {
        Func<decimal> val;
        public Expression(Func<decimal> valFactory)
        {
            val = valFactory;
        }

        public decimal Value => val();
    }
}
