using deSPICYtoINVADER.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deSPICYtoINVADER
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.GameLoop();
            Console.ReadLine();
        }
    }
}
