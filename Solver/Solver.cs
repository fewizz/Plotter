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
        public static IExpression ParseExpr(ref string str, params Argument[] args)
        {
            IExpression expr;
            str = str.Trim();

            if (str.First() == '(')
            {
                int depth = 1;
                int index = -1;
                for (int i = 1; i < str.Length; i++)
                {
                    if (str[i] == '(') depth++;
                    if (str[i] == ')') depth--;
                    if (depth == 0)
                    {
                        index = i;
                        break;
                    }
                }


                //int index = str.IndexOf(')');
                if (index == -1) throw new Exception("Expected ')'");
                int len = index - 1;
                expr = Parse(str.Substring(1, len), args);
                str = str.Substring(index + 1).Trim();
            }
            else if (!char.IsLetter(str.First()))
            {
                bool isNumSymbol(char c) => c >= '0' && c <= '9' || c == '.';
                int index = 0;
                for (; index < str.Length && isNumSymbol(str[index]); index++) ;
                string numStr = index < str.Length ? str.Remove(index) : str;
                expr = new Literal(decimal.Parse(numStr));
                str = str.Substring(index).Trim();
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
                    if(a != null)
                        expr = a.Arg;
                    else
                    {
                        Constant c = null;
                        foreach (Constant c0 in Operations.CONSTANTS)
                        {
                            if (c0.Name.Equals(name))
                            {
                                c = c0;
                                break;
                            }
                        }
                        if (c == null) throw new Exception("Undefined name: " + name);
                        expr = c;
                    }
                }
                else
                {

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

                    str = str.Substring(1);
                    foreach (string e0 in str.Remove(str.IndexOf(')')).Split(','))
                        expressions.Add(Parse(e0, args));

                    expr = f.CreateExpression(expressions);
                    str = str.Substring(str.IndexOf(')') + 1).Trim();
                }
            }

            str = str.Trim();

            return expr;
        }

        public static IExpression Parse(string str, params Argument[] args)
        {
            IExpression e = ParseExpr(ref str, args);
            while (!str.Equals(""))
                e = ParseAlgebraic(e, ref str, args);

            return e;
        }

        public static IExpression ParseAlgebraic(IExpression expr, ref string str, params Argument[] args)
        {
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
                str = str.Substring(1);
                return ao.CreateExpression(expr, ParseExpr(ref str, args));
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
