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
        private static IExpression ParseSimpleExpr(ref string str, IEnumerable<object> args)
        {
            str = str.Trim();

            if (str.First() == '-')
            {
                str = str.Substring(1);
                IExpression expr0 = ParseSimpleExpr(ref str, args);
                return new Expression(() => -expr0.Value, () => "(-" + expr0.ToGLSL() + ")");
            }
            if (str.First() == '|')
            {
                int closing = ClosingIndex(str, '|', '|');
                IExpression expr0 = Parse(str.Remove(closing).Substring(1), args);
                str = str.Substring(closing + 1);
                return Operations.FUN_BY_NAME["abs"].CreateExpression(expr0);
            }
            if (str.First() == '(')
            {
                int closingIndex = ClosingIndex(str, '(', ')');
                if (closingIndex == -1) throw new Exception("Expected ')'");
                int length = closingIndex - 1;
                string s = str.Substring(1, length);
                str = str.Substring(closingIndex + 1).Trim();
                return Parse(s, args);
            }
            if (char.IsDigit(str.First()))
            {
                string numStr = Regex.Match(str, @"^\d+\.?\d*").Value;
                str = str.Substring(numStr.Length).Trim();
                return new Literal(decimal.Parse(numStr));
            }
            if (char.IsLetter(str.First()))
            {
                string name = Regex.Match(str, @"^\w*").Value;
                str = str.Substring(name.Length).Trim();

                if (str.Length > 0 && str[0] == '(')
                {
                    Function f = Operations.FUN_BY_NAME[name];
                    if (f == null) throw new Exception("Undefined function: " + name);
                    var expressions = new List<IExpression>();

                    int closingIndex = ClosingIndex(str, '(', ')');
                    string arguments = str.Remove(closingIndex).Substring(1).Trim();
                    str = str.Substring(closingIndex + 1).Trim();

                    while (arguments.Length > 0)
                    {
                        expressions.Add(Parse(ref arguments, args, ','));
                        if (arguments.Length > 0)
                            arguments = arguments.Substring(1);
                        arguments = arguments.Trim();
                    }

                    return f.CreateExpression(expressions);
                }
                else
                {
                    foreach (object a in args)
                    {
                        if (
                            a is string && a.Equals(name) || a is string[] && (a as string[]).Contains(name)
                        )
                            return new Argument(name);
                        if (a is Argument && (a as Argument).Name.Equals(name))
                            return a as Argument;
                    }
                    var c = Operations.CONSTANTS_BY_NAME[name];
                    if (c != null) return c;
                    throw new Exception("Undefined name: " + name);
                }
            }

            throw new Exception("Can't parse simple expression");
        }

        public static IExpression Parse(string str)
        {
            return Parse(str, Enumerable.Empty<object>());
        }

        public static IExpression Parse(string str, IEnumerable<object> args)
        {
            return Parse(ref str, args);
        }

        private static IExpression Parse(ref string str, IEnumerable<object> args, params char[] stop)
        {
            IExpression left = ParseSimpleExpr(ref str, args);
            str = str.Trim();
            if (str == string.Empty || stop.Contains(str[0])) return left;
            AlgebraicOperation aoLeft = Operations.ALGEBRAIC_BY_SYM[str[0]];
            str = str.Substring(1);
            string beforeMiddle = string.Copy(str);
            IExpression middle = ParseSimpleExpr(ref str, args);
            str = str.Trim();
            if (str == string.Empty || stop.Contains(str[0])) return aoLeft.CreateExpression(left, middle);

            AlgebraicOperation aoRight = Operations.ALGEBRAIC_BY_SYM[str[0]];
            string afterMiddle = str.Substring(1);

            if (aoLeft.Priority <= aoRight.Priority)
            {
                str = afterMiddle;
                return aoRight.CreateExpression(aoLeft.CreateExpression(left, middle), Parse(ref str, args, stop));
            }
            else
            {
                str = beforeMiddle;
                return aoLeft.CreateExpression(left, Parse(ref str, args, stop));
            }
        }

        public static void Main()
        {
            while (true) try
            {
                IExpression e = Parse(Console.ReadLine());
                Console.WriteLine("value > " + Math.Round(e.Value));
                Console.WriteLine("glsl > " + e.ToGLSL());
            }
            catch(Exception e) { Console.WriteLine(e.StackTrace); }
        }
    }
}
