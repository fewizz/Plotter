using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public enum Status { Ok, Error }

    public static class StatusUtil
    {
        public static Status ToStatus(this bool b)
        {
            return b ? Status.Ok : Status.Error;
        }
    }
}
