///ETML
///Auteur : Jonathan Friedli et Filipe Andrade Barros
///Date : 20.05.19
///Description : Cette classe utilise le package Nuget Newtonsoft afin de pouvoir lire et stocker des choses dans un fichier json.
using deSPICYtoINVADER.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace deSPICYtoINVADER
{
    public class JsonHighScore
    {
        /// <summary>
        /// Path du fichier json
        /// </summary>
        private string _path;
        /// <summary>
        /// Pour désérialiser
        /// </summary>
        private dynamic _jsonObj;
        private List<Score> _topTen;

        /// <summary>
        /// Constructeur de la classe.
        /// On initialise _topTen dans Deserialize()
        /// </summary>
        /// <param name="path">Path du json à lire</param>
        public JsonHighScore(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Permet d'ajoute un score dans le fichier Json, si il rentre dans le top 10
        /// </summary>
        /// <param name="newOne"></param>
        public void AddScoreInJson(Score newOne)
        {
            Deserialize();
            SortTopTen(newOne);
            Serialize();
        }

        /// <summary>
        /// Méthode pour déserialiser. Toutes les valeurs contenues dans le json sont stockée dans des listes. (Tab1 et Tab2)
        /// </summary>
        private void Deserialize()
        {
            //Charge le fichier json
            string json = File.ReadAllText(_path);

            //Deserialize
            _jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            _topTen = new List<Score>();

            //Stocker les valeurs du json dans les variables c# pour pouvoir les modifier à notre guise.
            for (int i = 0; i < 10; i++)
            {
                _topTen.Add(new Score((string)_jsonObj["Tab1"][i], (int)_jsonObj["Tab2"][i]));
            }
        }

        /// <summary>
        /// Trie le top 10, ajoute (ou pas) le nouveau score, et retrie le top 10
        /// </summary>
        /// <param name="newOne"></param>
        private void SortTopTen(Score newOne)
        {
            _topTen = _topTen.OrderByDescending(x => x.Value).ToList();//trie la liste
            //Si la nouvelle valeure est plus grande que la plus petite valeur de la liste (c'est toujours la dernière car elle est triée),
            //on la remplace et on retrie la liste
            if (newOne.Value > _topTen[9].Value)
            {
                _topTen.RemoveAt(9);
                _topTen.Add(newOne);
                _topTen = _topTen.OrderByDescending(x => x.Value).ToList();//trie la liste
            }
        }

        /// <summary>
        /// Ajoute les highscore dans la liste, si le nom est vide, n'ajoute pas ce score dans la liste
        /// </summary>
        /// <returns>La liste à afficher</returns>
        public List<Score> ShowList()
        {
            Deserialize();
            List<Score> showList = new List<Score>();
            foreach (Score s in _topTen)
            {
                if (!s.Name.Equals(""))
                {
                    showList.Add(s);
                }
            }
            Serialize();
            return showList;
        }

        /// <summary>
        /// A utiliser en cas de bug du Json
        /// Permet de reset le fichier json en supprimant tous les scores
        /// Serialize ensuite le json
        /// </summary>
        public void ResetJson()
        {
            for (int i = 0; i < 10; i++)
            {
                _jsonObj["Tab1"][i] = "";
                _jsonObj["Tab2"][i] = "0";
            }

            //Serialize
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(_jsonObj, Newtonsoft.Json.Formatting.Indented);

            //On l'écrit dans le fichier json
            File.WriteAllText(_path, output);
        }

        /// <summary>
        /// Méthode pour Sérialiser. On restocke toutes les valeurs dans nos listes, dans le json
        /// </summary>
        private void Serialize()
        {
            //On remet les toutes valeurs dans le json
            for (int i = 0; i < _topTen.Count; i++)
            {
                _jsonObj["Tab1"][i] = _topTen[i].Name;
                _jsonObj["Tab2"][i] = _topTen[i].Value.ToString();
            }

            //Serialize
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(_jsonObj, Newtonsoft.Json.Formatting.Indented);

            //On l'écrit dans le fichier json
            File.WriteAllText(_path, output);
        }
    }
}
