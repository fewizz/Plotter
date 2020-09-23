using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace Parser
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

        public IExpression CreateExpression(params IExpression[] exprs)
        {
            return CreateExpression((IEnumerable<IExpression>)exprs);
        }

        public IExpression CreateExpression(IEnumerable<IExpression> prms)
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
