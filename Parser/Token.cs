using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokenizer
{

    public class Token
    {
        public object Value { get; set; }
        virtual public int Index { get; set; }
        virtual public int Length { get; set; }

        public char Char => this is BaseTokenizer.CharToken ? (char)Value : (char)0;

        public override string ToString() => GetType().Name + "(" + Value.ToString() + ")";
    }
}
