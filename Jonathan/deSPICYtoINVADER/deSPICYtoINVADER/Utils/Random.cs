///ETML
///Auteur : Jonathan Friedli et Filipe Andrade Barros
///Date : 20.05.19
///Description : Classe Static Random
namespace deSPICYtoINVADER.Utils
{
    /// <summary>
    /// Classe static permettant de créer une valeur random sans s'embêter à créer des objets Random dans toutes les classes
    /// </summary>
    public static class Random
    {
        /// <summary>
        /// Variable static contenant un objet random que l'on va utiliser pour générer une valeur aléatoire
        /// </summary>
        private static System.Random rand = new System.Random();

        /// <summary>
        /// Crée une valeur random entre 0 et la valeur entrée en paramètre -1
        /// </summary>
        /// <param name="maxPLusOne">Valeur max + 1 (si on met 11, cela va jusqu'a 10)</param>
        /// <returns></returns>
        public static int RandomValue(int maxPLusOne)
        {
            return rand.Next(maxPLusOne);
        }

        /// <summary>
        /// Surcharge de la méthode précédente, permettant cette fois-ci de créer une valeur aléatoire entre la valeur minimale et la valeur max -1
        /// Exemple si on mets 7 et 18, cela crée une valeur entre 7 et 17 compris
        /// </summary>
        /// <param name="minValue"> Valeur minimale (ex : 7)</param>
        /// <param name="maxPLusOne">Valeur maximale + 1(ex : 18)</param>
        /// <returns></returns>
        public static int RandomValue(int minValue, int maxPLusOne)
        {
            return rand.Next(minValue, maxPLusOne);
        }
    }
}
