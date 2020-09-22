using System;

namespace Parser
{
    public class Literal : IExpression
    {
        decimal val;
        public Literal(decimal valFactory)
        {
            val = valFactory;
        }

        public decimal Value => val;

        public string ToGLSL()
        {
            return val.ToString();
        }
    }
}
