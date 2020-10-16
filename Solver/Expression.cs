using System;

namespace Parser
{
    public interface IExpression {
        decimal Value { get; }
        string ToGLSL();
    }

    public class Expression : IExpression
    {
        protected Func<decimal> val;
        protected Func<string> glslFactory;
        public Expression(Func<decimal> valFactory, Func<string> glslFactory)
        {
            val = valFactory;
            this.glslFactory = glslFactory;
        }

        public decimal Value => val();

        public string ToGLSL()
        {
            return glslFactory();
        }
    }
}
