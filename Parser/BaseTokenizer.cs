using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tokenizer;

namespace BaseTokenizer
{
    public class NameToken : Token
    {
        new public int Length { get => (Value as string).Length; }
    }

    public class CharToken : Token
    {
        override public int Length { get => 1; }
    }

    public class NumberToken : Token {}

    public class BaseTokenizer
    {
        public static List<Token> Tokenize(string expressionStr)
        {
            int beginning = 0;
            bool end() => beginning >= expressionStr.Length;
            int spaces()
            {
                int spaces = 0;
                for (int n = beginning; n < expressionStr.Length; n++)
                    if (!char.IsWhiteSpace(expressionStr[n])) return spaces;
                    else spaces++;
                return 0;
            }
            char ch() => expressionStr[beginning];
            string str() => expressionStr.Substring(beginning);
            void skip(int size = 1) => beginning += size;
            void skipSpaces() => skip(spaces());

            string match(string pattern)
            {
                Match m = Regex.Match(str(), pattern);
                if (m.Success && m.Index == 0)
                {
                    skip(m.Value.Length);
                    return m.Value;
                }
                return null;
            }
            var tokens = new List<Token>();

            while (true)
            {
                skipSpaces();
                if (end()) break;

                {
                    string numPattern = @"(\d*\.\d+)|(\d+)";
                    string namePattern = @"[a-zA-Z_]+";

                    int index = beginning;
                    string num = match(numPattern);
                    if (num != null)
                    {
                        tokens.Add(new NumberToken()
                        {
                            Value = decimal.Parse(num),
                            Index = index,
                            Length = num.Length
                        });
                        continue;
                    }

                    string name = match(namePattern);
                    if (name != null)
                    {
                        tokens.Add(new NameToken()
                        {
                            Value = name,
                            Index = index
                        });
                        continue;
                    }
                }

                tokens.Add(new CharToken()
                {
                    Value = ch(),
                    Index = beginning
                });
                beginning++;
            }

            return tokens;
        }
    }
}
