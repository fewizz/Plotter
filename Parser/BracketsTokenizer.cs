using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tokenizer;

namespace BracketsTokenizer
{
    public abstract class BracketsToken : Token
    {
        public Token Opening { get; set; }
        public Token Ending { get; set; }

        public override int Index { get => Opening.Index; }
        public override int Length { get => Ending.Index + 1 - Opening.Index; }

        public override string ToString()
        {
            string res = GetType().Name + "{ ";
            foreach (var v in (List<Token>)Value)
                res += v.ToString() + " ";
            res += "}";
            return res;
        }
    }

    public class ModuleBracketsToken : BracketsToken { }
    public class RoundBracketsToken : BracketsToken { }

    class BracketsTokenizer
    {
        static char ToChar(Token t) =>
            t is BaseTokenizer.CharToken ? (char)t.Value : (char)0;

        static int ClosingIndex(List<Token> tokens, int index)
        {
            if (ToChar(tokens[index]) != '(') throw new ArgumentException("First character should be '('");
            int depth = 1;
            index++;
            for (; index < tokens.Count; index++)
            {
                if (ToChar(tokens[index]) == ')') depth--;
                else if (ToChar(tokens[index]) == '(') depth++;
                if (depth == 0) return index;
            }
            return -1;
        }

        public static List<Token> Tokenize(List<Token> tokens)
        {
            var result = new List<Token>();
            int beginnning = 0;

            Token token() => tokens[beginnning];
            bool end() => beginnning >= tokens.Count;

            char toChar() => ToChar(token());
            int closingIndex() => ClosingIndex(tokens, beginnning);

            while(!end())
            {
                if(toChar() == '(')
                {
                    int closing = closingIndex();
                    if (closing == -1)
                        throw new Exception("Нет закрывающей скобки для открывающей на " + token().Index);
                    int subBegin = beginnning + 1;
                    int subSize = closing - subBegin;
                    result.Add(new RoundBracketsToken()
                    {
                        Value = Tokenize(tokens.GetRange(subBegin, subSize).ToList()),
                        Opening = token(),
                        Ending = tokens[closing]
                    });
                    beginnning = closing + 1;
                    continue;
                }
                if(toChar() == '|')
                {
                    var closing = tokens.Skip(beginnning + 1).TakeWhile(t0 => ToChar(t0) != '|');

                    if (closing.Count() == 0)
                        throw new Exception("Нет закрывающего знака модуля для открывающего на " + token().Index);

                    result.Add(new ModuleBracketsToken()
                    {
                        Value = Tokenize(closing.ToList()),
                        Opening = token(),
                        Ending = closing.First()
                    });
                    beginnning += 2 + closing.Count();
                    continue;
                }
                result.Add(token());
                beginnning++;
            }

            return result;
        }
    }
}
