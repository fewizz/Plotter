using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parser
{
    public class Parser
    {
        private static int ClosingIndex(string str, char o, char c, int index = 0)
        {
            if (str[index] != o) throw new ArgumentException("First character should be '"+o+"'");
            int depth = 1;
            index++;
            for (; index < str.Length; index++)
            {
                if (str[index] == c) depth--;
                else if (str[index] == o) depth++;
                if (depth == 0) return index;
            }
            return -1;
        }
        private static IExpression ParseSimpleExpr(ref string str, params Argument[] args)
        {
            IExpression expr;
            str = str.Trim();

            if (str.First() == '-')
            {
                str = str.Substring(1);
                IExpression expr0 = ParseSimpleExpr(ref str, args);
                expr = new Expression(() => -expr0.Value, () => "(-" + expr0.ToGLSL() + ")");
            }
            else if (str.First() == '|')
            {
                int closing = ClosingIndex(str, '|', '|');
                IExpression expr0 = Parse(str.Remove(closing).Substring(1), args);
                str = str.Substring(closing + 1);
                expr = Operations.FUN_BY_NAME["abs"].CreateExpression(expr0);
            }
            else if (str.First() == '(')
            {
                int closingIndex = ClosingIndex(str, '(', ')');
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
                    Function f = Operations.FUN_BY_NAME[name];
                    if (f == null) throw new Exception("Undefined function: " + name);
                    var expressions = new List<IExpression>();

                    int closingIndex = ClosingIndex(str, '(', ')');
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
            IExpression left = ParseSimpleExpr(ref str, args);
            str = str.Trim();
            if (str == string.Empty) return left;
            AlgebraicOperation aoLeft = Operations.ALGEBRAIC_BY_SYM[str[0]];
            str = str.Substring(1);
            string beforeMiddle = string.Copy(str);
            IExpression middle = ParseSimpleExpr(ref str, args);
            if (str == string.Empty) return aoLeft.CreateExpression(left, middle);

            str = str.Trim();
            AlgebraicOperation aoRight = Operations.ALGEBRAIC_BY_SYM[str[0]];
            string afterMiddle = str.Substring(1);

            if(aoLeft.Priority <= aoRight.Priority)
                return aoRight.CreateExpression(aoLeft.CreateExpression(left, middle), Parse(afterMiddle, args));
            else
                return aoLeft.CreateExpression(left, Parse(beforeMiddle, args));
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
