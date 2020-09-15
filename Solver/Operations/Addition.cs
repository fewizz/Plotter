namespace Solver
{
    public class Add : Expression2
    {
        public Add(IExpression e1, IExpression e2)
        : base(e1, e2) { }

        public override decimal Value()
        {
            return expr1.Value() + expr2.Value();
        }
    }

    class Addition : IAlgebraicOperation
    {
        public static Addition INSTANCE = new Addition();

        private Addition() { }

        public Expression2 CreateExpression(IExpression e1, IExpression e2)
        {
            return new Add(e1, e2);
        }
        char IAlgebraicOperation.OperatorSymbol()
        {
            return '+';
        }
    }
}
