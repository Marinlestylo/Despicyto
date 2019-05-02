// ETML
// Auteur: friedlijo & andradebfi
// Date: 30.01.2019
// Description: Jeu Spicy'Nvader
//

using System;

namespace Spicy_Nvader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Menu mainMenu = new Menu();
            mainMenu.LaunchMenu();
            Console.Read();
        }
    }
}
