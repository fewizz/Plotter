using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Operations
    {
        public static Dictionary<char, AlgebraicOperation> ALGEBRAIC_BY_SYM = new Dictionary<char, AlgebraicOperation>();
        public static Dictionary<string, Function> FUN_BY_NAME = new Dictionary<string, Function>();
        public static Dictionary<string, Constant> CONSTANTS_BY_NAME = new Dictionary<string, Constant>();

        static void Algebraic(char s, uint priority, Func<IExpression, IExpression, IExpression> f)
        {
            Algebraic(s, priority, (e1, e2) => f(e1, e2).Value, (e1, e2) => f(e1, e2).ToGLSL());
        }

        static void Algebraic(
            char s,
            uint priority,
            Func<IExpression, IExpression, decimal> f,
            Func<IExpression, IExpression, string> glsl = null
        )
        {
            if (glsl == null) glsl = (e1, e2) => "(" + e1.ToGLSL() + s + e2.ToGLSL() + ")";
            var ao = new AlgebraicOperation(s, priority, f, glsl);
            ALGEBRAIC_BY_SYM.Add(s, ao);
        }

        static void Fun(string name, Func<IExpression[], decimal> f = null)
        {
            if (f == null) f = pms => 0;
            FUN_BY_NAME.Add(name, new Function(name, f));
        }

        static void Const(string name, decimal v)
        {
            CONSTANTS_BY_NAME.Add(name, new Constant(name, v));
        }

        static Operations() {
            Fun("sign", es => Math.Sign(es[0].Value));
            Fun("floor", es => Math.Floor(es[0].Value));
            Fun("ceiling", es => Math.Ceiling(es[0].Value));
            Fun("negate", es => -es[0].Value);
            Fun("pow", es => (decimal)Math.Pow((double)es[0].Value, (double)es[1].Value));
            Fun("abs", es => Math.Abs(es[0].Value));
            Fun("sin", es => (decimal) Math.Sin((double)es[0].Value));
            Fun("cos", es => (decimal) Math.Cos((double)es[0].Value));
            Fun("tg", es => (decimal)Math.Tan((double)es[0].Value));
            Fun("sqrt", es => (decimal) Math.Sqrt((double)es[0].Value));
            Fun("log", es => (decimal)Math.Log((double)es[0].Value, (double)es[1].Value));
            Fun("ln", es => (decimal)Math.Log((double)es[0].Value));
            Fun("lg", es => (decimal)Math.Log10((double)es[0].Value));
            Fun("min", es => Math.Min(es[0].Value, es[1].Value));
            Fun("max", es => Math.Max(es[0].Value, es[1].Value));
            Fun("noise");

            Algebraic('+', 2, (e1, e2) => e1.Value + e2.Value);
            Algebraic('-', 2, (e1, e2) => e1.Value - e2.Value);
            Algebraic('*', 1, (e1, e2) => e1.Value * e2.Value);
            Algebraic('/', 1, (e1, e2) => e1.Value / e2.Value);
            Algebraic('%', 1, (e1, e2) => e1.Value % e2.Value);
            Algebraic(
                '^',
                0,
                (e1, e2) => FUN_BY_NAME["pow"].CreateExpression(e1, e2)
            );

            Const("e", (decimal)Math.E);
            Const("pi", (decimal)Math.PI);
        }
    }
}
