using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class Sin : Func
    {
        public Sin(IExpression e) : base(new List<IExpression>()) {
            exprs.Add(e);
        }

        public override decimal Value()
        {
            return (decimal) Math.Sin((double)exprs[0].Value());
        }
    }

    class Sinus : Function
    {
        public static Sinus INSTANCE = new Sinus();
        public override Func CreateExpression0(List<IExpression> exprs)
        {
            return new Sin(exprs[0]);
        }

        public override string Name()
        {
            return "sin";
        }

        public override uint ParamsCount()
        {
            return 1;
        }
    }
}
