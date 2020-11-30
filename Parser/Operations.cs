using Plotter;
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
        public static Dictionary<uint, List<AlgebraicOperation>> ALGEBRAICS_BY_PRIO = new Dictionary<uint, List<AlgebraicOperation>>();
        public static Dictionary<string, Function> FUN_BY_NAME = new Dictionary<string, Function>();
        public static Dictionary<string, Constant> CONSTANTS_BY_NAME = new Dictionary<string, Constant>();

        static void Algebraic(char s, uint priority, Func<IExpression, IExpression, IExpression> f)
        {
            Algebraic(s, priority, (e1, e2) => f(e1, e2).Value, (e1, e2) => f(e1, e2).ToGLSLSource());
        }

        static void Algebraic(char s, uint priority, string funcName)
        {
            Algebraic(
                s,
                priority,
                (e1, e2) => FUN_BY_NAME[funcName].CreateExpression(e1, e2)
            );
        }

        static void Algebraic(
            char s,
            uint priority,
            Func<IExpression, IExpression, decimal> f,
            Func<IExpression, IExpression, string> glsl = null
        )
        {
            if (glsl == null) glsl = (e1, e2) => "(" + e1.ToGLSLSource() + s + e2.ToGLSLSource() + ")";
            var ao = new AlgebraicOperation(s, priority, f, glsl);
            if (!ALGEBRAICS_BY_PRIO.ContainsKey(priority)) ALGEBRAICS_BY_PRIO.Add(priority, new List<AlgebraicOperation>());
            ALGEBRAICS_BY_PRIO[priority].Add(ao);
            ALGEBRAIC_BY_SYM.Add(s, ao);
        }

        static void Fun(string name, int[] pas, Func<IExpression[], decimal> f = null)
        {
            if (f == null) f = pms => 0;
            FUN_BY_NAME.Add(name, new Function(name, f, pas));
        }

        static void Const(string name, decimal v)
        {
            CONSTANTS_BY_NAME.Add(name, new Constant(name, v));
        }

        static Operations() {
            int[] ONE = { 1 };
            int[] TWO = { 2 };
            int[] THREE = { 3 };

            Fun("sign", ONE, es => Math.Sign(es[0].Value));
            Fun("floor", ONE, es => Math.Floor(es[0].Value));
            Fun("round", ONE, es => Math.Round(es[0].Value));
            Fun("ceil", ONE, es => Math.Ceiling(es[0].Value));
            Fun("fract", ONE, es => es[0].Value - Math.Floor(es[0].Value));
            Fun("negate", ONE, es => -es[0].Value);
            Fun("pow", TWO, es => (decimal)Math.Pow((double)es[0].Value, (double)es[1].Value));
            Fun("abs", ONE, es => Math.Abs(es[0].Value));
            Fun("sin", ONE, es => (decimal) Math.Sin((double)es[0].Value));
            Fun("cos", ONE, es => (decimal) Math.Cos((double)es[0].Value));
            Fun("tg", ONE, es => (decimal)Math.Tan((double)es[0].Value));
            Fun("sqrt", ONE, es => (decimal) Math.Sqrt((double)es[0].Value));
            Fun("log", TWO, es => (decimal)Math.Log((double)es[0].Value, (double)es[1].Value));
            Fun("ln", ONE, es => (decimal)Math.Log((double)es[0].Value));
            Fun("lg", ONE, es => (decimal)Math.Log10((double)es[0].Value));
            Fun("min", TWO, es => Math.Min(es[0].Value, es[1].Value));
            Fun("max", TWO, es => Math.Max(es[0].Value, es[1].Value));
            Fun("clamp", THREE, es => Math.Min(Math.Max(es[0].Value, es[1].Value), es[2].Value));
            Fun("noise", new int[] { 2, 3 }
                , es => es.Length == 2 ?
                (decimal)Noise.noise((float)es[0].Value, (float)es[1].Value)
                : (decimal)Noise.noise((float)es[0].Value, (float)es[1].Value, (float)es[2].Value)
            );
            Fun("conditional", THREE, es => es[0].Value > 0 ? es[1].Value : es[2].Value );
            Fun("equals", TWO, es => es[0].Value == es[1].Value ? 1m : 0m);
            Fun("greater", TWO, es => es[0].Value > es[1].Value ? 1m : 0m);
            Fun("less", TWO, es => es[0].Value < es[1].Value ? 1m : 0m);

            Algebraic('+', 2, (e1, e2) => e1.Value + e2.Value);
            Algebraic('-', 2, (e1, e2) => e1.Value - e2.Value);
            Algebraic('*', 1, (e1, e2) => e1.Value * e2.Value);
            Algebraic('/', 1, (e1, e2) => e1.Value / e2.Value);
            Algebraic('%', 1, (e1, e2) => e1.Value % e2.Value);
            Algebraic('^', 0, "pow");
            Algebraic('=', 3, "equals");
            Algebraic('>', 3, "greater");
            Algebraic('<', 3, "less");


            Const("e", (decimal)Math.E);
            Const("pi", (decimal)Math.PI);
        }
    }
}
