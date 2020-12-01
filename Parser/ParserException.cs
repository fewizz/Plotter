using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokenizer;

namespace Parser
{
    public class ParserException : Exception
    {
        public Token Token { get; set; }

        public ParserException(Token t, string s)
        : base(s)
        {
            Token = t;
        }
    }
}
