using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    class GLSLNoise
    {
        public static readonly string SOURCE;
        static GLSLNoise()
        {
            SOURCE = File.ReadAllText("../../common.glsl");
        }
    }
}
