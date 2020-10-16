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

        string expressionString;
        //Action<string, Status> updater;

        public event PropertyChangedEventHandler PropertyChanged;

        public string ExpressionString
        {
            set
            {
                expressionString = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ExpressionString"));
            }
        }

        /*public Status UpdateExpression(string s)
        {
            expressionString = s;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
            return updater(s);
        }*/
    }

    public class ColorConstructor
    {

        Dictionary<ColorComponent, ColorComponentConstructor> Components
            = new Dictionary<ColorComponent, ColorComponentConstructor>();

        public ColorConstructor(Func<ColorComponent, string, Status> updater)
        {
            foreach(var cc in ColorComponents.ARRAY)
                Components.Add(cc, new ColorComponentConstructor());
        }

        public ColorComponentConstructor this[ColorComponent cc] => Components[cc];
    }
}
