using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace Solver
{
    class Function
    {
        public string Name { get; }

        Func<IExpression[], decimal> factory;

        public Function(string name, Func<IExpression[], decimal> factory)
        {
            Name = name;
            this.factory = factory;
        }

        public IExpression CreateExpression(List<IExpression> prms)
        {
            string glsl = Name+"(";
            foreach(var p in prms)
            {
                glsl += p.ToGLSL();
                if(prms.Last() != p) glsl+=", ";
            }
            glsl += ")";
            return new Expression(() => factory(prms.ToArray()), ()=>glsl);
        }
    }
}
