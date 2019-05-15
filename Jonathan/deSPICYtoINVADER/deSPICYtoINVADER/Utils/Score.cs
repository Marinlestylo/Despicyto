using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deSPICYtoINVADER.Utils
{
    public class Score
    {
        public string Name { get; private set; }
        public int Value { get; private set; }

        public Score(string name, int val)
        {
            Value = val;
            Name = name;
        }
    }
}
