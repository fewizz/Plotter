using Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public class ColorComponentConstructor : INotifyPropertyChanged
    {

        public ColorComponentConstructor()
        {
        }

        public delegate Status ExpressionStringChangedHandler(string expression);
        public event ExpressionStringChangedHandler ExpressionStringChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        string expressionString;
        Status status = Status.Error;

        public string ExpressionString
        {
            get { return expressionString; }
            set
            {
                expressionString = value;
                if(ExpressionStringChanged != null)
                    status = ExpressionStringChanged.Invoke(expressionString);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ExpressionString"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BackColor"));
            }
        }

        public Color BackColor => Program.ColorByStatus(status);
    }

    public class ColorConstructor
    {

        Dictionary<ColorComponent, ColorComponentConstructor> Components
            = new Dictionary<ColorComponent, ColorComponentConstructor>();

        public ColorConstructor()
        {
            foreach(var cc in ColorComponents.ARRAY)
                Components.Add(cc, new ColorComponentConstructor());
        }

        public ColorComponentConstructor this[ColorComponent cc] => Components[cc];
    }
}
