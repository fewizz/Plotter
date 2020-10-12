using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public class Grids
    {
        public enum GridType
        {
            Plain, Sphere
        }

        public class GridConstructor : INotifyPropertyChanged
        {
            public class ColorConstructor
            {
                public ColorComponent Component { get; private set; }
                public Color BackColor => Program.ColorByStatus(grid.ColorComponentsExpressions[Component] != null);
                string expression;
                readonly Grid grid;

                public ColorConstructor(ColorComponent cc, Grid g)
                {
                    Component = cc;
                    grid = g;
                }

                public string Expression
                {
                    get { return expression; }
                    set { expression = value; grid.TryParseColorComponent(Component, value); }
                }
            }

            string expr;
            Dictionary<ColorComponent, ColorConstructor> ColorConstructors =
                new Dictionary<ColorComponent, ColorConstructor>();

            public event PropertyChangedEventHandler PropertyChanged;

            string name;
            public string Name {
                get { return name; }
                set {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                } 
            }
            public string ValueExpr
            {
                get { return expr; }
                set { expr = value; Grid.TryParseValueExpression(expr); }
            }

            public Color BackColor => Program.ColorByStatus(Grid.ValueExpression != null);

            public ColorConstructor this[ColorComponent cc] => ColorConstructors[cc];

            public Grid Grid { get; set; }

            public object Type
            {
                get { return Grid is PlainGrid ? GridType.Plain : GridType.Sphere; }
                set
                {
                    Grid prev = Grid;
                    Grid.Dispose();
                    Grid = value switch
                    {
                        GridType.Plain => new PlainGrid(),
                        GridType.Sphere => new SphereGrid(),
                        _ => throw new NotImplementedException()
                    };
                    Grid.ValueExpression = prev?.ValueExpression;
                    Grid.ColorComponentsExpressions = prev?.ColorComponentsExpressions;
                }
            }

            public GridConstructor(string name)
            {
                Grid = new PlainGrid();

                void addColor(ColorComponent cc, string expr)
                    => ColorConstructors.Add(cc, new ColorConstructor(cc, Grid) { Expression = expr });

                addColor(ColorComponent.Red, "y*1.5");
                addColor(ColorComponent.Green, "1.5 - |y|");
                addColor(ColorComponent.Blue, "-y*1.5");
                addColor(ColorComponent.Alpha, "1");

                Name = name;
                ValueExpr = "0";
            }
        }

        readonly public static BindingList<GridConstructor> List = new BindingList<GridConstructor>();
    }
}
