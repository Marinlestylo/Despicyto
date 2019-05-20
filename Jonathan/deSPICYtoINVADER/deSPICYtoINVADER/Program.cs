///ETML
///Auteur : Jonathan Friedli et Filipe Andrade Barros
///Date : 20.05.19
///Description : Classe Program
namespace deSPICYtoINVADER
{
    /// <summary>
    /// Classe contenant la méthode Main
    /// </summary>
    class Program
    {
        /// <summary>
        /// On crée un menu et on lance la méthode LoadMenu, qui elle va lancer le jeu etc
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.LoadMenu();
        }
    }
}
