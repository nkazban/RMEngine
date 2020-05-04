using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Helpers
    {
        public static double ToRadians(double angle)
        {
            return angle * (Math.PI / 180);
        }
    }
}
