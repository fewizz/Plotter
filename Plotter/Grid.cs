using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public class Grid
    {
        protected GridRenderer renderer;
        private Argument arg0, arg1;
        public IExpression ValueExpression { get; private set; }
        Dictionary<ColorComponent, IExpression> colorComponentsExpressions;

        public Exception ValueParseException { get; private set; }
        public Dictionary<ColorComponent, Exception> ColorComponentsParseExceptions { get; private set; }

        public enum GridType { Plain, Sphere }
        GridType type;

        public object Type
        {
            get { return type; }
            set {
                type = (GridType)value;
                renderer?.Dispose();
                renderer = type switch
                {
                    GridType.Plain => new PlainGridRenderer(50, 0.5F),
                    GridType.Sphere => new SphereGridRenderer(),
                    _ => throw new NotImplementedException()
                };
                arg0 = new Argument(renderer.Arg0());
                arg1 = new Argument(renderer.Arg1());
                if(colorComponentsExpressions != null)
                    renderer.UpdateColorComponentsExpressions(colorComponentsExpressions);
                if(ValueExpression != null)
                    renderer.UpdateValueExpression(ValueExpression);
            }
        }

        public Grid() {
            Type = GridType.Plain;

            colorComponentsExpressions = new Dictionary<ColorComponent, IExpression>();
            ColorComponentsParseExceptions = new Dictionary<ColorComponent, Exception>();
            foreach (var cc in Enum.GetValues(typeof(ColorComponent)))
            {
                colorComponentsExpressions.Add((ColorComponent)cc, null);
                ColorComponentsParseExceptions.Add((ColorComponent)cc, null);
            }
        }

        public decimal Value(decimal arg0, decimal arg1)
        {
            this.arg0.Value = arg0;
            this.arg0.Value = arg1;
            return ValueExpression.Value;
        }

        public void TryParseValueExpression(string expr)
        {
            try
            {
                ValueExpression = Parser.Parser.Parse(expr, arg0, arg1, Program.TimeArg, renderer.AdditionalValue());
                ValueParseException = null;
                renderer.UpdateValueExpression(ValueExpression);
            } catch(Exception e)
            {
                ValueParseException = e;
            }
        }

        public void TryParseColorComponent(ColorComponent cc, string expr)
        {
            try
            {
                colorComponentsExpressions[cc] = Parser.Parser.Parse(expr, arg0, arg1, Program.TimeArg, renderer.AdditionalColor());
                ColorComponentsParseExceptions[cc] = null;
                if (colorComponentsExpressions.ContainsValue(null)) return;
                renderer.UpdateColorComponentsExpressions(colorComponentsExpressions);
            }
            catch (Exception e)
            {
                ColorComponentsParseExceptions[cc] = e;
            }
        }

        public void Draw()
        {
            renderer.Draw();
        }
    }
}
