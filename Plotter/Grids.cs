using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

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
            string valueExpressionString;
            public event PropertyChangedEventHandler PropertyChanged;

            string name;
            public string Name {
                get { return name; }
                set {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
            public string ValueExpressionString
            {
                get { return valueExpressionString; }
                set { valueExpressionString = value; Grid.TryParseValueExpression(valueExpressionString); }
            }

            public Grid Grid { get; private set; }

            public object Type
            {
                get { return Grid is PlainGrid ? GridType.Plain : GridType.Sphere; }
                set
                {
                    Grid prev = Grid;
                    Grid = value switch
                    {
                        GridType.Plain => new PlainGrid(),
                        GridType.Sphere => new SphereGrid(),
                        _ => throw new NotImplementedException()
                    };
                    Grid.ValueExpression = prev?.ValueExpression;
                    Grid.ColorComponentsExpressions = prev?.ColorComponentsExpressions;
                    prev.Dispose();
                }
            }

            ColorConstructor ColorConstructor;

            public GridConstructor(string name)
            {
                Grid = new PlainGrid();

                ColorConstructor = new ColorConstructor(null);

                void initColor(ColorComponent cc, string expr) {
                    //this[cc].ExpressionStringChanged += e => Grid.TryParseColorComponent(cc, e);
                    this[cc].PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName.Equals("ExpressionString"))
                            ;// this[cc].ExpressionString = this[cc].ExpressionString;
                    };
                }

                initColor(ColorComponent.Red, "y*1.5");
                initColor(ColorComponent.Green, "1.5 - |y|");
                initColor(ColorComponent.Blue, "-y*1.5");
                initColor(ColorComponent.Alpha, "1");

                Name = name;
                ValueExpressionString = "0";
            }

            public ColorComponentConstructor this[ColorComponent cc] => ColorConstructor[cc];
        }

        readonly public static BindingList<GridConstructor> List = new BindingList<GridConstructor>();
    }
}
