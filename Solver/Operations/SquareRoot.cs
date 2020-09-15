using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    class Sqrt : Func
    {
        public Sqrt(IExpression e) : base(new List<IExpression>())
        {
            exprs.Add(e);
        }

        public override decimal Value()
        {
            var val = exprs[0].Value();
            if (val < 0) return decimal.MaxValue;
            return (decimal)Math.Sqrt((double)val);
        }
    }

    class SquareRoot : Function
    {
        public static SquareRoot INSTANCE = new SquareRoot();
        public override Func CreateExpression0(List<IExpression> exprs)
        {
            return new Sqrt(exprs[0]);
        }

        public override string Name()
        {
            return "sqrt";
        }

        public override uint ParamsCount()
        {
            return 1;
        }
    }
}

