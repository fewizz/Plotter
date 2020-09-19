using System;

namespace Solver
{

    class AlgebraicOperation
    {
        public char OperatorSymbol { get; private set; }

        Func<IExpression, IExpression, decimal> factory;
        Func<IExpression, IExpression, string> glslFactory;

        public AlgebraicOperation(
            char symbol,
            Func<IExpression, IExpression, decimal> exprFactor,
            Func<IExpression, IExpression, string> glslFactory
        )
        {
            OperatorSymbol = symbol;
            factory = exprFactor;
            this.glslFactory = glslFactory;
        }

        public IExpression
        CreateExpression(IExpression e1, IExpression e2)
        {
            return new Expression ( () => factory(e1, e2), () => glslFactory(e1, e2) );
        }
    }


    
}
