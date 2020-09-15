using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    public class Parser
    {
        public static IExpression Parse(string str, params Argument[] args)
        {
            str = str.Trim();

            IExpression expr;

            if (str.First() == '(')
            {
                str = str.Substring(1).Trim();
                int index = str.IndexOf(')');
                expr = Parse(str.Remove(index), args);
                str = str.Substring(index + 1);
            }
            else if(!char.IsLetter(str.First()))
            {
                bool isNumSymbol(char c) => c >= '0' && c <= '9' || c == '.';
                int index = 0;
                for (; index < str.Length && isNumSymbol(str[index]); index++) ;
                string numStr = index < str.Length ? str.Remove(index) : str;
                expr = new Constant(decimal.Parse(numStr));
                str = str.Substring(index);
            }
            else
            {
                int wordSize = 0;
                for (; wordSize < str.Length; wordSize++)
                    if (!char.IsLetter(str[wordSize])) break;
                string name = str.Substring(0, wordSize);
                str = str.Substring(wordSize).Trim();
                if (str.Equals("") || str[0] != '(')
                {
                    Argument a = null;
                    foreach (Argument a0 in args)
                    {
                        if (a0.Name.Equals(name))
                        {
                            a = a0;
                            break;
                        }
                    }
                    expr = a.Arg;
                }
                else
                {
                    str = str.Substring(1).Trim();

                    Function f = null;
                    foreach (Function f0 in Operations.FUNCTIONS)
                    {
                        if (f0.Name.Equals(name))
                        {
                            f = f0;
                            break;
                        }
                    }

                    List<IExpression> expressions = new List<IExpression>();

                    foreach (string e0 in str.Remove(str.IndexOf(')')).Split(','))
                        expressions.Add(Parse(e0, args));

                    expr = f.CreateExpression(expressions);
                    str = str.Substring(str.IndexOf(')') + 1);
                }
            }

            str = str.Trim();
            if (str.Equals("")) return expr;
            char ch = str.First();

            AlgebraicOperation ao = null;
            foreach(AlgebraicOperation op in Operations.ALGEBRAIC)
            {
                if (op.OperatorSymbol == ch)
                {
                    ao = op;
                    break;
                }
            }

            if (ao != null)
            {
                str = str.Substring(1).Trim();
                return ao.CreateExpression(expr, Parse(str, args));
            }
            throw new NullReferenceException();
        }

        public static void Main(string[] args)
        {
            while (true)
                Console.WriteLine("> " + Math.Round(Parse(Console.ReadLine()).Value, 3));
        }
    }
}
