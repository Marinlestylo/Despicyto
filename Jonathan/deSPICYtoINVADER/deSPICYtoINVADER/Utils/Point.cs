///ETML
///Auteur : Jonathan Friedli et Filipe Andrade Barros
///Date : 20.05.19
///Description : Classe Point pour l'affichage
namespace deSPICYtoINVADER.Utils
{
    /// <summary>
    ///  Au lieu d'avoir 2 variables une pour la haut et l'autre pour la gauche, on crée une objet avec les 2 coordonnées
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Correspond à la valeur depuis la gauche de la console
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Correspond à la valeur depuis le haut de la console
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Constrcuteur de Point
        /// </summary>
        /// <param name="x">Gauche</param>
        /// <param name="y">Haut</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
