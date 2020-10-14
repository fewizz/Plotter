using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public enum ColorComponent
    {
        Red, Green, Blue, Alpha
    }

    public static class ColorComponents
    {
        public static ColorComponent[] ARRAY = Enum.GetValues(typeof(ColorComponent)) as ColorComponent[];
    }
}
