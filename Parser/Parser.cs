using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using BaseTokenizer;
using BracketsTokenizer;
using Tokenizer;

namespace Parser
{
    public class Parser
    {
        static object ExprByName(string name, IEnumerable<object> args)
        {
            if (Operations.FUN_BY_NAME.ContainsKey(name)) return Operations.FUN_BY_NAME[name];

            foreach (object a in args)
            {
                if (
                    a is string && a.Equals(name) || a is string[] && (a as string[]).Contains(name)
                )
                    return new Argument(name);
                if (a is Argument && (a as Argument).Name.Equals(name))
                    return a as Argument;
            }

            if (Operations.CONSTANTS_BY_NAME.ContainsKey(name)) return Operations.CONSTANTS_BY_NAME[name];

            return null;
        }

        static List<List<Token>> Split(BracketsToken bt)
        {
            if (((List<Token>)bt.Value).Count() == 0) return new List<List<Token>>();

            var result = new List<List<Token>>();
            var current = new List<Token>();
            result.Add(current);
            foreach (var el in (bt.Value) as List<Token>)
            {
                if (el.Char == ',')
                {
                    current = new List<Token>();
                    result.Add(current);
                }
                else current.Add(el);
            }

            return result;
        }

        private static void ForEachExp(Token token, IEnumerable<object> args, Action<object> a)
        {
            string str = (string)token.Value;
            int begin = 0;
            int size = str.Length;

            for (; size > 0; size--)
            {
                var exp = ExprByName(str.Substring(begin, size), args);
                if (exp == null)
                    continue;

                begin = size;
                size = str.Length - begin;

                a.Invoke(exp);
            }
        }

        private static IExpression NormalizeFunctionCall(
            Function f,
            BracketsToken bt,
            IEnumerable<object> args
        )
        {
            List<IExpression> es = new List<IExpression>();

            foreach (var list in Split(bt))
                es.Add(Parse(list, args));

            if (!f.possibleArgsSize.Contains(es.Count()))
                throw new Exception("Неправильное колическтво аргументов для функции '" + f.Name + "' (" + es.Count()+")");

            return f.CreateExpression(es);
        }

        private static IExpression SubNormalize(
            ref int beginning,
            List<Token> tokens,
            IEnumerable<object> args
        )
        {
            if (beginning >= tokens.Count) throw new Exception("Nothing to normalize");
            Token first = tokens[beginning];

            if (first is NameToken)
            {
                var e = ExprByName((string)first.Value, args);
                if (e is Function f)
                {
                    beginning++;
                    if (beginning == tokens.Count || !(tokens[beginning] is RoundBracketsToken))
                        throw new Exception(
                            "Ожидался список аргументов после имени функции '" + (string)first.Value + "'"
                        );
                    RoundBracketsToken bt = (RoundBracketsToken)tokens[beginning];
                    return NormalizeFunctionCall(f, bt, args);
                }
                if (e is Argument || e is Constant)
                    return (IExpression)e;
                else throw new Exception("Неизвестное имя '" + (string)first.Value + "'");
            }
            if (first is NumberToken nt)
                return new Literal((decimal)nt.Value);
            if (first is RoundBracketsToken rbt)
                return Parse((List<Token>)rbt.Value, args);
            else if (first is ModuleBracketsToken mbt)
                return Operations.FUN_BY_NAME["abs"].CreateExpression(Parse((List<Token>)mbt.Value, args));
            else if (first is CharToken && first.Char == '-')
            {
                beginning++;
                IExpression sub = SubNormalize(ref beginning, tokens, args);
                return new Expression(() => -sub.Value, () => "(-" + sub.ToGLSLSource() + ")");
            }
            else throw new Exception("Непонятное строковое выражение на " + first.Index);
        }

        private static List<object> Normalize(List<Token> tokens, IEnumerable<object> args)
        {
            var result = new List<object>();
            int beginning = 0;

            Token token() => tokens[beginning];
            void skip() => beginning += 1;
            bool end() => beginning >= tokens.Count;

            if (end()) return result;
            while (true)
            {
                result.Add(SubNormalize(ref beginning, tokens, args));
                skip();

                if (end())
                    break;

                Token prev = tokens[beginning - 1];
                if (!(token() is CharToken)) throw new Exception("Ожидался символ алгебраической операции после " + (prev.Index + prev.Length - 1));
                CharToken ct = (CharToken)token();
                result.Add(ct);
                skip();

                if(end()) throw new Exception("Ожидалось выражение после знака алгебраической операции '" + ct.Char + "' на " + ct.Index);
            }

            return result;
        }

        public static IExpression Parse(string str, IEnumerable<object> args)
        {
            var tokens = BracketsTokenizer.BracketsTokenizer.Tokenize (
                BaseTokenizer.BaseTokenizer.Tokenize(str)
            );
            if (tokens.Count() == 0) throw new Exception("Строка выражения пуста");
            return Parse(tokens, args);
        }

        public static IExpression TryParse(string str, params object[] args)
        {
            return TryParse(str, out string m, args as IEnumerable<object>);
        }

        public static IExpression TryParse(string str, out string message, params object[] args)
        {
            return TryParse(str, out message, args as IEnumerable<object>);
        }

        public static IExpression TryParse(string str, out string message, IEnumerable<object> args)
        {
            message = null;
            try
            {
                return Parse(str, args);
            }
            catch (Exception e){ message = e.Message; }
            return null;
        }

        private static IExpression Parse(List<Token> tokens, IEnumerable<object> args)
        {
            var normalized = Normalize(tokens, args);
            var aos = from ao in Operations.ALGEBRAIC_BY_SYM.Values orderby ao.Priority select ao;

            var i = (from s in Operations.ALGEBRAICS_BY_PRIO orderby s.Key select s.Value);

            foreach (var aol in i)
            {
                for (int x = 1; x < normalized.Count;)
                {
                    char op = (normalized[x] as CharToken).Char;
                    var ao = Operations.ALGEBRAIC_BY_SYM[op];

                    if (aol.Contains(ao))
                    {
                        normalized[x - 1] = ao.CreateExpression(
                            (IExpression)normalized[x - 1],
                            (IExpression)normalized[x + 1]
                        );

                        normalized.RemoveRange(x, 2);
                    }
                    else
                    {
                        x += 2;
                    }
                }
            }

            return (IExpression)normalized[0];
        }

        public static void Main()
        {
            while (true) try
            {
                string str = Console.ReadLine();

                var tokens = BracketsTokenizer.BracketsTokenizer.Tokenize(
                        BaseTokenizer.BaseTokenizer.Tokenize(str)
                    );
                foreach (var t in tokens)
                    Console.WriteLine(t.ToString());

                Console.WriteLine();
                IExpression e = Parse(tokens, new object[0]);
                Console.WriteLine("value > " + e.Value);
                Console.WriteLine("glsl > " + e.ToGLSLSource());
                Console.WriteLine();
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
