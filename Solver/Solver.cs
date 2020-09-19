using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Solver
{
    public class Parser
    {
        private static int ClosingParenthesisIndex(string str)
        {
            if (str.First() != '(') throw new ArgumentException("First character should be '('");
            int depth = 1;
            for (int index = 1; index < str.Length; index++)
            {
                if (str[index] == '(') depth++;
                if (str[index] == ')') depth--;
                if (depth == 0)
                    return index;
            }
            return -1;
        }
        private static IExpression ParseSimpleExpr(ref string str, params Argument[] args)
        {
            IExpression expr;
            str = str.Trim();

            if (str.First() == '(')
            {
                int closingIndex = ClosingParenthesisIndex(str);
                if (closingIndex == -1) throw new Exception("Expected ')'");
                int length = closingIndex - 1;
                expr = Parse(str.Substring(1, length), args);
                str = str.Substring(closingIndex + 1).Trim();
            }
            else if (char.IsDigit(str.First()))
            {
                string numStr = Regex.Match(str, @"^\d+\.?\d*").Value;
                expr = new Literal(decimal.Parse(numStr));
                str = str.Substring(numStr.Length).Trim();
            }
            else if (char.IsLetter(str.First()))
            {
                string name = Regex.Match(str, @"^\w*").Value;
                str = str.Substring(name.Length).Trim();

                if (str.Length > 0 && str[0] == '(')
                {
                    Function f = Operations.FUNCTIONS.Find(f0 => f0.Name.Equals(name));
                    if (f == null) throw new Exception("Undefined function: " + name);
                    var expressions = new List<IExpression>();

                    int closingIndex = ClosingParenthesisIndex(str);
                    string arguments = str.Remove(closingIndex).Substring(1);
                    str = str.Substring(closingIndex + 1).Trim();

                    foreach (string arg in arguments.Split(','))
                        expressions.Add(Parse(arg, args));

                    expr = f.CreateExpression(expressions);
                }
                else
                {
                    Argument a = args.ToList().Find(a0 => a0.Name.Equals(name));
                    if (a != null)
                        expr = a.Arg;
                    else
                        expr = Operations.CONSTANTS.Find(c0 => c0.Name.Equals(name));

                    if (expr == null) throw new Exception("Undefined name: " + name);
                }
            }
            else throw new Exception("Can't parse simple expression");

            str = str.Trim();
            return expr;
        }

        public static IExpression Parse(string str, params Argument[] args)
        {
            IExpression e = ParseSimpleExpr(ref str, args);
            while (str.Length > 0)
                e = ParseAlgebraic(e, ref str, args);

            return e;
        }

        private static IExpression ParseAlgebraic(IExpression expr, ref string str, params Argument[] args)
        {
            char symbol = str.First();
            var ao = Operations.ALGEBRAIC.Find(el => el.OperatorSymbol == symbol);
            if(ao == null) throw new Exception("Undefined algebraic operation with symbol: " + symbol);
            str = str.Substring(1);
            return ao.CreateExpression(expr, ParseSimpleExpr(ref str, args));
        }

        public static void Main()
        {
            while (true) try
            {
                IExpression e = Parse(Console.ReadLine());
                Console.WriteLine("value > " + Math.Round(e.Value));
                Console.WriteLine("glsl > " + e.ToGLSL());
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
