namespace Solver
{
    interface IAlgebraicOperation : IOperation
    {
        char OperatorSymbol();
        Expression2 CreateExpression(IExpression e1, IExpression e2);
    }
}
