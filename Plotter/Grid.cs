using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public abstract class Grid
    {
        protected GridRenderer renderer;
        protected Argument t;
        public IExpression ValueExpression { get; private set; }
        Dictionary<ColorComponent, IExpression> colorComponentsExpressions;

        public Exception ValueParseException { get; private set; }
        public Dictionary<ColorComponent, Exception> ColorComponentsParseExceptions { get; private set; }

        public Grid(GridRenderer renderer) {
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
                ValueExpression = Parser.Parser.Parse(expr, ValueExpressionArguments());
                ValueParseException = null;
                renderer.UpdateValueExpression(ValueExpression);
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
                if (colorComponentsExpressions.ContainsValue(null)) return;
                renderer.UpdateColorComponentsExpressions(colorComponentsExpressions);
            }
            catch (Exception e)
            {
                ColorComponentsParseExceptions[cc] = e;
            }
        }

        public void Render(DateTime time)
        {
            t.Value = (decimal)((DateTime.Now - time).TotalMilliseconds / 1000D);
            renderer.Draw(time);
        }
    }
}
