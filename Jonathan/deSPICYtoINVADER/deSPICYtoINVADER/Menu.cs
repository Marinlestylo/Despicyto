///ETML
///Auteur : Jonathan Friedli et Filipe Andrade Barros
///Date : 20.05.19
///Description : Classe Menu qui gère le menu
using deSPICYtoINVADER.Utils;
using System;
using System.Collections.Generic;
using System.Threading;

namespace deSPICYtoINVADER
{
    /// <summary>
    /// Classe qui gère le menu, sa navigation et qui lance le jeu en temps voulu. A la fin du jeu, on revient dans la navigation
    /// du menu.
    /// </summary>
    public class Menu
    {
        /* Constantes */
        private const int WIDTH_OF_MENU = 80;//Taille du buffer et fenêtre
        private const int HEIGHT_OF_MENU = 25;//Taille du buffer et fenêtre
        private const int TOP_SPACE_MENU = 3;//Espace entre les options (hauteur)

        /* Readonly */
        private readonly Point titlePadding = new Point(10, 0);
        private readonly Point menuPadding = new Point(24, 10);

        /// <summary>
        /// Difficulté du jeu. Plus le chiffre est élevé plus les enemies ont des chances de tirer
        /// </summary>
        public static int Difficulty { get; private set; }
        /// <summary>
        /// Booléen qui gère le son
        /// </summary>
        public static bool Sound { get; private set; }
        /// <summary>
        /// Bool qui gère si on doit fermer le jeu ou non
        /// </summary>
        //public bool CloseGame { get; private set; }//Si = true, le jeu se ferme

        /* Attributs */
        private string[] _options;//tableau avec le nom des options du menu
        private string[] _optionsName;//Tableau avec le nom des paramètres dans le options
        private string[] _optionValues; //Tableau avec les valeurs des options
        private int _index;//Index du menu
        private bool _loadGame;//permet de savoir quand lancer le jeu
        private Game _game;//Permet de lancer le jeu
        private JsonHighScore _highScore;//affichage des highscore
        private bool _closeProgram;//fermeture du program

        /// <summary>
        /// Constructeur du menu
        /// set l'index à 0
        /// </summary>
        public Menu()
        {
            _options = new string[5] { "Jouer", "Options", "Scores hauts", "A propos", "Quitter" };//tableau avec le nom des options du menu
            _optionsName = new string[2] { "Difficulté", "Son" };//Tableau avec le nom des paramètres dans le options
            _optionValues = new string[4] { "Iron IV (Facile)     ", "Silver II (Difficile)", "ON ", "OFF" }; //Tableau avec les valeurs des options
            _highScore = new JsonHighScore("Resources\\HighScore.json");//Pour les highscore
            _index = 0;
            _closeProgram = false;
            Difficulty = 2;
            Sound = true;
        }

        /// <summary>
        /// Permet de resize la fenêtre de la console, rendre le curseur invisible, de lancer le menu et d'y naviguer
        /// </summary>
        public void LoadMenu()
        {
            _loadGame = false;
            Console.SetWindowSize(WIDTH_OF_MENU, HEIGHT_OF_MENU);
            Console.SetBufferSize(WIDTH_OF_MENU, HEIGHT_OF_MENU);
            Console.CursorVisible = false;
            ShowMenu();
            Navigate();
        }

        /// <summary>
        /// Affiche tout le menu
        /// </summary>
        private void ShowMenu()
        {
            //Méthode qui affiche le titre
            Sprites.DrawTitle(Sprites.mainTitle, new Point(titlePadding.X, titlePadding.Y));

            //boucle pour les options du menu
            for (int i = 0; i < _options.Length; i++)
            {
                Console.SetCursorPosition(menuPadding.X, menuPadding.Y + i * TOP_SPACE_MENU);
                Console.WriteLine(_options[i]);
            }
            //Curseur du menu
            DrawCursor();
        }

        /// <summary>
        /// Dessine le cursor et efface le précédent
        /// </summary>
        private void DrawCursor()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            if (_index < 5)//Naviguer dans le menu de base
            {
                //Effacer le cursor puis l'écrir afin de donner l'impression d'un mouvement
                for (int i = 0; i < _options.Length; i++)
                {
                    Console.SetCursorPosition(menuPadding.X - 3, menuPadding.Y + i * TOP_SPACE_MENU);
                    Console.WriteLine("  ");
                    Console.SetCursorPosition(menuPadding.X + _options[i].Length + 1, menuPadding.Y + i * TOP_SPACE_MENU);
                    Console.WriteLine("  ");
                }

                Console.SetCursorPosition(menuPadding.X - 3, menuPadding.Y + _index * TOP_SPACE_MENU);//Position du chevron de gauche
                Console.Write(">>");
                Console.SetCursorPosition(menuPadding.X + _options[_index].Length + 1, menuPadding.Y + _index * TOP_SPACE_MENU);//Position du chevron de droite
                Console.Write("<<");
            }
            else//Naviguer dans les options
            {
                //effacer le curseur
                for (int i = 0; i < _optionsName.Length; i++)
                {
                    Console.SetCursorPosition(menuPadding.X - 3, (menuPadding.Y + TOP_SPACE_MENU) + i * TOP_SPACE_MENU);
                    Console.WriteLine("  ");
                    Console.SetCursorPosition(menuPadding.X + _optionsName[i].Length + 1, (menuPadding.Y + TOP_SPACE_MENU) + i * TOP_SPACE_MENU);
                    Console.WriteLine("  ");
                }
                Console.SetCursorPosition(menuPadding.X - 3, menuPadding.Y + (_index - 4) * TOP_SPACE_MENU);//Position du chevron de gauche
                Console.Write(">>");
                Console.SetCursorPosition(menuPadding.X + _optionsName[_index - 5].Length + 1, menuPadding.Y + (_index - 4) * TOP_SPACE_MENU);//Position du chevron de droite
                Console.Write("<<");
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Effectue des actions en fonction de la valeur de l'index
        /// </summary>
        private void Select()
        {
            switch (_index)
            {
                case 0://lance le jeu
                    _loadGame = true;
                    _game = new Game();
                    _game.GameLoop();
                    HighScore();
                    LoadMenu();
                    break;
                case 1://Ouvre les options
                    Option();
                    break;
                case 2://Montre les meilleurs scores
                    HighScore();
                    break;
                case 3://Montre les créateurs du jeu
                    About();
                    break;
                case 4://Ferme le jeu
                    _closeProgram = true;
                    break;
                case 5://Dans les options, change la difficulté
                    ChangeDifficulty();
                    break;
                case 6://Dans les options, active/désactive le son
                    ChangeSound();
                    break;
            }
        }

        /// <summary>
        /// Dans les différentes options
        /// Détecte si on presse la touche escape afin de revenir au menu
        /// </summary>
        /// <returns>true si escape est pressed, false sinon</returns>
        private bool EscapePressed()
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                Console.Clear();
                ShowMenu();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Permet de changer la fréquence de tir des enemies et la difficulté
        /// </summary>
        private void ChangeDifficulty()
        {
            Console.SetCursorPosition(45, 13);
            if (Difficulty == 2)
            {
                Difficulty = 1;
                Console.WriteLine(_optionValues[0]);
            }
            else
            {
                Difficulty = 2;
                Console.WriteLine(_optionValues[1]);
            }
        }

        /// <summary>
        /// Set le son à ON ou à OFF
        /// </summary>
        private void ChangeSound()
        {
            Console.SetCursorPosition(45, 16);
            if (Sound)
            {
                Sound = false;
                Console.WriteLine(_optionValues[3]);
            }
            else
            {
                Sound = true;
                Console.WriteLine(_optionValues[2]);
            }
        }

        /// <summary>
        /// Boucle qui ne fait rien tant que la condition EscapePressed n'est pas a true.
        /// Dès qu'elle est a true (touche escape pressed), on revient au menu
        /// </summary>
        private void ReturnToMenu()
        {
            while (!EscapePressed())
            {
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Affiche des infos sur les créateurs
        /// </summary>
        private void About()
        {
            Console.Clear();
            Sprites.DrawTitle(Sprites.aboutTitle, new Point(18, 0));
            Console.SetCursorPosition(33, 6);
            Console.WriteLine("Jeu créé par :");
            Sprites.DrawTitle(Sprites.joAscii, new Point(2, 8));
            Sprites.DrawTitle(Sprites.andAscii, new Point(37, 16));
            Sprites.DrawTitle(Sprites.filipeAscii, new Point(50, 16));
            Console.SetCursorPosition(20, 23);
            Console.WriteLine("Appuyez sur escape pour retourner au menu");
            ReturnToMenu();
        }

        /// <summary>
        /// Méthode qui montre les meilleurs scores (jusqu'a 10 scores)
        /// </summary>
        private void HighScore()
        {
            Console.Clear();
            Sprites.DrawTitle(Sprites.highScoreTitle, new Point(5, 0));
            List<Score> temp = _highScore.ShowList();
            for (int i = 0; i < temp.Count; i++)
            {
                Console.SetCursorPosition(25, 10 + i);
                Console.WriteLine(temp[i].Name);
                Console.SetCursorPosition(41, 10 + i);
                Console.WriteLine(temp[i].Value);
            }
            Console.SetCursorPosition(20, 22);
            Console.WriteLine("Appuyez sur escape pour retourner au menu");
            ReturnToMenu();
        }

        /// <summary>
        /// Affiche le menu des options (Difficulté et son). Appelle la méthode qui permet de naviguer dans les options
        /// </summary>
        private void Option()
        {
            Console.Clear();
            Sprites.DrawTitle(Sprites.optionsTitle, new Point(20, 0));//afiche le titre en ascii
            
            //Affichage de la difficulté
            Console.SetCursorPosition(24, 13);
            Console.WriteLine(_optionsName[0]);
            Console.SetCursorPosition(45, 13);
            if (Difficulty == 2)
            {
                Console.WriteLine(_optionValues[1]);
            }
            else
            {
                Console.WriteLine(_optionValues[0]);
            }

            //Affichage du son
            Console.SetCursorPosition(24, 16);
            Console.WriteLine(_optionsName[1]);
            Console.SetCursorPosition(45, 16);
            if (Sound)
            {
                Console.WriteLine(_optionValues[2]);
            }
            else
            {
                Console.WriteLine(_optionValues[3]);
            }
            Console.SetCursorPosition(20, 22);
            Console.WriteLine("Appuyez sur escape pour retourner au menu");

            NavigateInOption();//Naviguer dans le menu

            Console.Clear();
            ShowMenu();//aficher le menu de base
        }

        /// <summary>
        /// Boucle while qui permet de naviguer dans le menu
        /// </summary>
        private void NavigateInOption()
        {
            _index = 5;//_index 5 c'est la difficulté, donc on ouvre les options et le curseur est directement sur Difficulté
            bool goBack = false;
            DrawCursor();
            while (!goBack)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)//Déplacement dans le menu
                    {
                        case ConsoleKey.UpArrow://Quand on "Monte"
                            _index--;
                            if (_index == 4)
                            {
                                _index = 6;
                            }
                            DrawCursor();
                            break;
                        case ConsoleKey.DownArrow://Quand on "Descend"
                            _index++;
                            if (_index == 7)
                            {
                                _index = 5;
                            }
                            DrawCursor();
                            break;
                        case ConsoleKey.Enter://Pour changer l'option sélectionnée
                        case ConsoleKey.Spacebar://Pour changer l'option sélectionnée
                            Select();
                            break;
                        case ConsoleKey.Escape://Pour resortir des options
                            goBack = true;
                            break;
                    }
                }
            }
            _index = 1;//Permet de sélectionner "Options" dans le menu de base
        }

        /// <summary>
        /// Permet de naviguer dans le menu et de sélectionner une options
        /// </summary>
        private void Navigate()
        {
            while (!_closeProgram && !_loadGame)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)//Lis la touche du clavier sur laquelle on appuie
                    {
                        case ConsoleKey.UpArrow:
                            _index--;
                            if (_index == -1)//Si on est tout en haut et qu'on monte encore, on arrive tout en bas du menu
                            {
                                _index = 4;
                            }
                            DrawCursor();
                            break;
                        case ConsoleKey.DownArrow:
                            _index++;
                            if (_index == 5)//Si on est tout en bas et qu'on descend encore on arrive tout en haut du menu
                            {
                                _index = 0;
                            }
                            DrawCursor();
                            break;
                        case ConsoleKey.Enter:
                        case ConsoleKey.Spacebar:
                            Select();
                            break;
                    }
                }
            }
        }
    }
}
