namespace Solver
{
    public class Constant : IExpression
    {
        decimal val;

        public Constant(decimal v)
        {
            val = v;
        }

        public decimal Value()
        {
            return val;
        }
    }
}
