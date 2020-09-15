using System;
using System.CodeDom;
using System.Collections.Generic;

namespace Solver
{
    class Function
    {
        public string Name { get; }

        Func<IExpression[], decimal> factory;

        public Function(string name, Func<IExpression[], decimal> factory)
        {
            Name = name;
            this.factory = factory;
        }

        public IExpression CreateExpression(List<IExpression> prms)
        {
            return new Expression(() => factory(prms.ToArray()));
        }
    }
}
