using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deSPICYtoINVADER
{
    public class Menu
    {
        /* Constantes */
        private const int WIDTH_OF_MENU = 80;//Taille du buffer et fenêtre
        private const int HEIGHT_OF_MENU = 25;//Taille du buffer et fenêtre
        private const int LEFT_MARGE_TITLE = 10;//Marge à gauche du titre
        private const int TOP_MARGE_TITLE = 0;//Marge en haut du titre
        private const int LEFT_MARGE_MENU = 24;//Marge à gauche des options
        private const int TOP_MARGE_MENU = 10;//Marge en haut des options
        private const int TOP_SPACE_MENU = 3;//Espace entre les options (hauteur)
        private readonly string[] TITLE = new string[8]// tableau de string qui contient le titre 
        {
            "   _____       _            _ _   _                _           ",
            "  / ____|     (_)          ( ) \\ | |              | |          ",
            " | (___  _ __  _  ___ _   _|/|  \\| |_   ____ _  __| | ___ _ __ ",
            "  \\___ \\| '_ \\| |/ __| | | | | . ` \\ \\ / / _` |/ _` |/ _ \\ '__|",
            "  ____) | |_) | | (__| |_| | | |\\  |\\ V / (_| | (_| |  __/ |   ",
            " |_____/| .__/|_|\\___|\\__, | |_| \\_| \\_/ \\__,_|\\__,_|\\___|_|   ",
            "        | |            __/ |                                   ",
            "        |_|           |___/                                    "
        };

        private string[] _options = new string[5] { "Jouer", "Options", "Score haut", "A propos", "Quitter" };

        /// <summary>
        /// Difficulté du jeu. Plus le chiffre est élevé plus les enemies ont des chances de tirer
        /// </summary>
        public static int Difficulty { get; private set; }
        /// <summary>
        /// Booléen qui gère le son
        /// </summary>
        public static bool Sound { get; private set; }

        /* Attributs */
        private int _index;
        private bool _loadGame;


        public Menu()
        {
            _index = 0;
            _loadGame = false;
        }

        public void LoadMenu()
        {
            Console.SetWindowSize(WIDTH_OF_MENU, HEIGHT_OF_MENU);
            Console.SetBufferSize(WIDTH_OF_MENU, HEIGHT_OF_MENU);
            Console.CursorVisible = false;
            ShowMenu();
            Navigate();
        }

        private void ShowMenu()
        {
            //Boucle pour le titre
            for (int i = 0; i < TITLE.Length; i++)
            {
                Console.SetCursorPosition(LEFT_MARGE_TITLE, TOP_MARGE_TITLE + i);
                Console.WriteLine(TITLE[i]);
            }

            //boucle pour les options du menu
            for (int i = 0; i < _options.Length; i++)
            {
                Console.SetCursorPosition(LEFT_MARGE_MENU, TOP_MARGE_MENU + i * TOP_SPACE_MENU);
                Console.WriteLine(_options[i]);
            }
            //Curseur du menu
            Console.WriteLine("");
            DrawCursor();
        }

        /// <summary>
        /// Dessine le cursor et efface le précédent
        /// </summary>
        private void DrawCursor()
        {
            //Effacer le cursor puis l'écrir afin de donner l'impression d'un mouvement
            for (int i = 0; i < _options.Length; i++)
            {
                Console.SetCursorPosition(LEFT_MARGE_MENU - 2, TOP_MARGE_MENU + i * TOP_SPACE_MENU);
                Console.WriteLine(" ");
                Console.SetCursorPosition(LEFT_MARGE_MENU + _options[i].Length + 1, TOP_MARGE_MENU + i * TOP_SPACE_MENU);
                Console.WriteLine(" ");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(LEFT_MARGE_MENU - 2, TOP_MARGE_MENU + _index * TOP_SPACE_MENU);//Position du chevron de gauche
            Console.Write('>');
            Console.SetCursorPosition(LEFT_MARGE_MENU + _options[_index].Length + 1, TOP_MARGE_MENU + _index * TOP_SPACE_MENU);//Position du chevron de droite
            Console.Write('<');
            Console.ResetColor();
        }

        private void Select()
        {
            switch (_index)
            {
                case 0:
                    _loadGame = true;
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    About();
                    break;
                case 4:
                    Environment.Exit(42);
                    break;
            }
        }

        /// <summary>
        /// Permet de revenir au menu principal
        /// </summary>
        private void ReturnToMenu()
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                Console.Clear();
                ShowMenu();
                return;
            }
        }

        /// <summary>
        /// Affiche des infos sur le jeu
        /// </summary>
        private void About()
        {
            Console.Clear();
            Console.WriteLine("instert random bullshit here");//texte à modifier:)
            while (true)
            {
                ReturnToMenu();
            }
        }

        private void HighScore()
        {
            Console.Clear();
            Console.WriteLine("Highscore");
        }

        /// <summary>
        /// Permet de naviguer dans le menu et de sélectionner une options
        /// </summary>
        private void Navigate()
        {
            while (!_loadGame)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)//Lis la touche du clavier sur laquelle on appuie
                    {
                        case ConsoleKey.UpArrow:
                            _index--;
                            if (_index == -1)
                            {
                                _index = 4;
                            }
                            DrawCursor();
                            break;
                        case ConsoleKey.DownArrow:
                            _index++;
                            if (_index == 5)
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
