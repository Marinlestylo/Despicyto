using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deSPICYtoINVADER.Utils
{
    public static class Random
    {
        private static System.Random rand = new System.Random();

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
