using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class Arg : IExpression
    {
        public decimal Val = 0;
        public decimal Value()
        {
            return Val;
        }
    }

    public class Argument
    {
        public string Name;
        public Arg Arg;

        public Argument(Arg a, string name)
        {
            this.Arg = a;
            this.Name = name;
        }
    }
}
