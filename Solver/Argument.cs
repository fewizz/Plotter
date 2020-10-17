using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{

    public class Argument : IExpression
    {
        public decimal Value { get; set; }

        public string Name { get; set; }

        public Argument(string name, decimal val = 0)
        {
            Name = name;
        }

        public string ToGLSLSource()
        {
            return Name;
        }
    }
}
