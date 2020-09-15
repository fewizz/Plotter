using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    class Cos : Func
    {
        public Cos(IExpression e) : base(new List<IExpression>())
        {
            exprs.Add(e);
        }

        public override decimal Value()
        {
            return (decimal)Math.Cos((double)exprs[0].Value());
        }
    }

    class Cosinus : Function
    {
        public static Cosinus INSTANCE = new Cosinus();
        public override Func CreateExpression0(List<IExpression> exprs)
        {
            return new Cos(exprs[0]);
        }

        public override string Name()
        {
            return "cos";
        }

        public override uint ParamsCount()
        {
            return 1;
        }
    }
}