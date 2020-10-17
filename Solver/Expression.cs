using System;

namespace Parser
{
    public interface IExpression {
        decimal Value { get; }
        string ToGLSLSource();
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

        public string ToGLSLSource()
        {
            return glslFactory();
        }
    }
}
