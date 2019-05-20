///ETML
///Auteur : Jonathan Friedli et Filipe Andrade Barros
///Date : 20.05.19
///Description : Classe Score qu'on utilise pour les highscore
namespace deSPICYtoINVADER.Utils
{
    /// <summary>
    /// Classe score qui contient le pseudo du joueur et le score
    /// </summary>
    public class Score
    {
        /// <summary>
        /// Username du joueur
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Valeur du score
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Constructeur de la classe Score
        /// </summary>
        /// <param name="name">Nom</param>
        /// <param name="val">Valeur du score</param>
        public Score(string name, int val)
        {
            Value = val;
            Name = name;
        }
    }
}
