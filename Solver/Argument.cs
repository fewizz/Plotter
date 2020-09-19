using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class Arg : IExpression
    {
        public string Name { get; set; }
        public decimal Value { get; set; }

        public string ToGLSL()
        {
            return Name;
        }
    }

    public class Argument
    {
        public string Name;
        public Arg Arg;
    }
}
