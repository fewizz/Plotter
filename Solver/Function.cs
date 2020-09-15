using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public abstract class Func : IExpression {
        protected List<IExpression> exprs;

        protected Func(List<IExpression> es)
        {
            exprs = es;
        }

        public abstract decimal Value();
    }

    abstract class Function : IOperation
    {
        public Func CreateExpression(List<IExpression> exprs)
        {
            if (exprs.Count != ParamsCount())
                throw new InvalidOperationException();
            return CreateExpression0(exprs);
        }

        public abstract Func CreateExpression0(List<IExpression> exprs);

        public abstract uint ParamsCount();

        public abstract string Name();
    }
}
