using System.ComponentModel;
using System.Drawing;
using Parser;

namespace Plotter
{
    public class Points
    {
        public class Point : INotifyPropertyChanged
        {
            public Grid Grid => GridControl?.Grid;
            public GridControl GridControl { get; set; }

            public class CoordinateComponent
            {
                string expressionString;
                public string ExpressionString
                {
                    get { return expressionString; }
                    set
                    {
                        expressionString = value;
                        Expression = Parser.Parser.TryParse(value, Program.TimeArg);
                    }
                }
                public IExpression Expression { get; private set; }
            }

            public CoordinateComponent X, Z;

            public event PropertyChangedEventHandler PropertyChanged;
            string name;
            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }

            public Point(string n)
            {
                Name = n;
                X = new CoordinateComponent() { ExpressionString = "0" };
                Z = new CoordinateComponent() { ExpressionString = "0" };
            }
        }

        readonly public static BindingList<Point> List = new BindingList<Point>();
    }
}
