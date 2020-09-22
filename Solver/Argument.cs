using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class Arg : IExpression
    {
        public string Name { get; private set; }
        public decimal Value { get; set; }

        public Arg(string name, decimal value) {
            Name = name;
            Value = value;
        }

        public string ToGLSL()
        {
            return Name;
        }
    }

    public class Argument
    {
        public string Name {
            get { return Arg.Name; }
        }
        public Arg Arg;
    }
}
