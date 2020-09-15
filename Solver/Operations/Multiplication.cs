namespace Solver
{
    class Multiply : Expression2
    {
        public Multiply(IExpression e1, IExpression e2)
        : base(e1, e2) { }

        public override decimal Value()
        {
            return expr1.Value() * expr2.Value();
        }
    }

    class Multiplication : IAlgebraicOperation
    {
        public static Multiplication INSTANCE = new Multiplication();

        private Multiplication() { }

        public Expression2 CreateExpression(IExpression e1, IExpression e2)
        {
            return new Multiply(e1, e2);
        }
        char IAlgebraicOperation.OperatorSymbol()
        {
            return '*';
        }
    }
}
