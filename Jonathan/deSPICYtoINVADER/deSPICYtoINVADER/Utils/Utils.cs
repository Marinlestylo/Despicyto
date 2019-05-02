using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deSPICYtoINVADER
{
    public static class Utils
    {
        private static Random rand = new Random();

        public static int RandomValue(int maxPLusOne)
        {
            return rand.Next(maxPLusOne);
        }

        public static int RandomValue(int minValue, int maxPLusOne)
        {
            return rand.Next(minValue, maxPLusOne);
        }
    }
}
