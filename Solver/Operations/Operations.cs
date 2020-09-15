using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver
{
    class Operations
    {
        public static List<IOperation> OPERATIONS = new List<IOperation>();

        static Operations() {
            OPERATIONS.Add(Addition.INSTANCE);
            OPERATIONS.Add(Subtraction.INSTANCE);
            OPERATIONS.Add(Multiplication.INSTANCE);
            OPERATIONS.Add(Division.INSTANCE);
            OPERATIONS.Add(Power.INSTANCE);

            OPERATIONS.Add(Sinus.INSTANCE);
            OPERATIONS.Add(Cosinus.INSTANCE);
            OPERATIONS.Add(SquareRoot.INSTANCE);
        }
    }
}
