using System;

namespace Parser
{
    public class Literal : IExpression
    {
        public decimal Value { get; }
        public Literal(decimal v)
        {
            Value = v;
        }

        public string ToGLSL()
        {
            return Value.ToString();
        }
    }
}
