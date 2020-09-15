using System;

namespace Solver
{

    class AlgebraicOperation
    {
        public char OperatorSymbol { get; private set; }

        Func<IExpression, IExpression, decimal> factory;

        public AlgebraicOperation(char symbol, Func<IExpression, IExpression, decimal> exprFactor)
        {
            OperatorSymbol = symbol;
            factory = exprFactor;
        }

        public IExpression CreateExpression(IExpression e1, IExpression e2)
        {
            return new Expression ( () => factory(e1, e2) );
        }
    }


    
}
