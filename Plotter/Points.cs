using System.ComponentModel;
using System.Drawing;
using Parser;

namespace Plotter
{
    public class Points
    {
        public class Point : INotifyPropertyChanged
        {
            public Grids.GridConstructor GridConstructor { get; set; }

            public class CoordinateConstructor
            {
                string expressionText;
                public string ExpressionText
                {
                    get { return expressionText; }
                    set
                    {
                        try
                        {
                            Expression = null;
                            expressionText = value;
                            Expression = Parser.Parser.Parse(value, new object[] { Program.TimeArg });
                        }
                        catch { }
                    }
                }
                public IExpression Expression { get; private set; }
                public Color BackColor => Program.ColorByStatus(Expression != null);
            }

            public CoordinateConstructor X, Z;

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

            public Color BackColor => Program.ColorByStatus(GridConstructor != null);

            public Point(string n)
            {
                Name = n;
                X = new CoordinateConstructor() { ExpressionText = "0" };
                Z = new CoordinateConstructor() { ExpressionText = "0" };
            }
        }

        readonly public static BindingList<Point> List = new BindingList<Point>();
    }
}
