using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    class Operations
    {
        //public static List<IOperation> OPERATIONS = new List<IOperation>();
        public static List<AlgebraicOperation> ALGEBRAIC = new List<AlgebraicOperation>();
        public static List<Function> FUNCTIONS = new List<Function>();

        static void Algebraic(char s, Func<IExpression, IExpression, decimal> f)
        {
            ALGEBRAIC.Add(new AlgebraicOperation(s, f));
        }

        static void Fun(string name, Func<IExpression[], decimal> f)
        {
            FUNCTIONS.Add(new Function(name, f));
        }

        static Operations() {
            Algebraic('+', (e1, e2) => e1.Value + e2.Value );
            Algebraic('-', (e1, e2) => e1.Value - e2.Value);
            Algebraic('*', (e1, e2) => e1.Value * e2.Value);
            Algebraic('/', (e1, e2) => e1.Value / e2.Value);
            Algebraic('^', (e1, e2) => (decimal)( Math.Pow((double)e1.Value, (double)e2.Value) ));

            Fun("sin", es => (decimal) Math.Sin((double)es[0].Value));
            Fun("cos", es => (decimal)Math.Cos((double)es[0].Value));
            Fun("sqrt", es => (decimal)Math.Sqrt((double)es[0].Value));
        }
    }
}
