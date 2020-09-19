using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    class Constant : IExpression
    {
        public decimal Value { get; }
        public string Name { get; }

        public Constant(string name, decimal val)
        {
            Value = val;
            Name = name;
        }

        public string ToGLSL()
        {
            return Value.ToString();
        }
    }
}
