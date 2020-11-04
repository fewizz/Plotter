using System.Collections.Generic;
using System.ComponentModel;

namespace Plotter
{
    public class GridType
    {
        public string CoordinateSystemTypeName { get; private set; }
        public static GridType
            Plain = new GridType() {
                CoordinateSystemTypeName = "Декартова Система Координат"
            },
            Sphere = new GridType() {
                CoordinateSystemTypeName = "Сферическая Система Координат"
            };

        public static GridType[] Types = new GridType[] { Plain, Sphere };
    }
}
