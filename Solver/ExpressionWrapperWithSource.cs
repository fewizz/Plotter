using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class ExpressionWrapper : IExpression
    {
        public IExpression Expression { get; private set; }

        public ExpressionWrapper(IExpression e)
        {
            Expression = e;
        }

        public decimal Value => Expression.Value;

        public string ToGLSLSource() { return Expression.ToGLSLSource(); }
    }

    public class ExpressionWrapperWithSource : ExpressionWrapper
    {
        public string source { get; private set; }

        public ExpressionWrapperWithSource(IExpression e, string source)
        : base(e)
        {
            this.source = source;
        }
    }
}
