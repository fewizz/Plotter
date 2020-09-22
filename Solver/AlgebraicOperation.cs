using System;

namespace Parser
{

    class AlgebraicOperation
    {
        public char OperatorSymbol { get; private set; }

        Func<IExpression, IExpression, decimal> factory;
        Func<IExpression, IExpression, string> glslFactory;
        public uint Priority { get; private set; }

        public AlgebraicOperation(
            char symbol,
            uint priority,
            Func<IExpression, IExpression, decimal> exprFactor,
            Func<IExpression, IExpression, string> glslFactory
        )
        {
            OperatorSymbol = symbol;
            Priority = priority;
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
