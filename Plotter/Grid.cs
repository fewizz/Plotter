using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public abstract class Grid<R> where R : GridRenderer
    {
        protected R renderer;
        protected Argument t;
        protected IExpression valueExpression;
        Dictionary<ColorComponent, IExpression> colorComponentsExpressions;

        public Exception ValueParseException { get; private set; }
        public Dictionary<ColorComponent, Exception> ColorComponentsParseExceptions { get; private set; }

        public Grid(R renderer) {
            this.renderer = renderer;

            colorComponentsExpressions = new Dictionary<ColorComponent, IExpression>();
            ColorComponentsParseExceptions = new Dictionary<ColorComponent, Exception>();
            foreach (var cc in Enum.GetValues(typeof(ColorComponent)))
            {
                colorComponentsExpressions.Add((ColorComponent)cc, null);
                ColorComponentsParseExceptions.Add((ColorComponent)cc, null);
            }

            t = new Argument("t");
        }

        protected abstract object[] ValueExpressionArguments();

        public void TryParseValueExpression(string expr)
        {
            try
            {
                valueExpression = Parser.Parser.Parse(expr, ValueExpressionArguments());
                ValueParseException = null;
                renderer.UpdateValueExpression(valueExpression);
            } catch(Exception e)
            {
                ValueParseException = e;
            }
        }

        protected abstract object[] ColorComponentExpressionArguments();

        public void TryParseColorComponent(ColorComponent cc, string expr)
        {
            try
            {
                colorComponentsExpressions[cc] = Parser.Parser.Parse(expr, ColorComponentExpressionArguments());
                ColorComponentsParseExceptions[cc] = null;
                renderer.UpdateColorComponentsExpressions(colorComponentsExpressions);
            }
            catch (Exception e)
            {
                ColorComponentsParseExceptions[cc] = e;
            }
        }

        public void Render(DateTime time)
        {
            renderer.Draw(time);
        }
    }
}
