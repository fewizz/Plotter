using System;

namespace Solver
{


    public interface IExpression {
        decimal Value { get; }
        string ToGLSL();
    }

    public class Expression : IExpression
    {
        Func<decimal> val;
        Func<string> glslFactory;
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
