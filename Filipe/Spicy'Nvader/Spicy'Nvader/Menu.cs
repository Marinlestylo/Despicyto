// ETML
// Auteur: friedlijo & andradebfi
// Date: 30.01.2019
// Description: Classe Menu du Spicy'Nvader
//

using System;
using System.Timers;

namespace Spicy_Nvader
{
    public class Menu
    {
        private const int WIDTH_OF_MENU = 80;       // variable qui sert à set la largeur de la fenêtre de la console et le buffer
        private const int HEIGHT_OF_MENU = 25;      // variable qui sert à set la hauteur de la fenêtre de la console et le buffer

        private static bool _sound;     // booléen du son true -> son: on , false -> son: off
        private static bool _difficulty;    // booléen de la difficulté true -> Difficile , false -> facile
        private bool _startPressed;     // booléen qui nous permet de savoir quand on lance le jeu true -> on peut naviguer dans le menu
        private bool _about;            // booléen qui nous permet de sortir de la séléction About et revenir dans la navigation du Menu
        private int _positionMenu;
        private int _currentLeft;       // variable de la position actuelle en y
        private int _margeTopTitle;     // variable qui nous sert de cursorTop pour afficher le titre qu'on incrémente à chaque fois

        private string[] tab_Title = new string[8]          // tableau de string qui contient le titre
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

        private string[] tab_Menu = new string[5] { "Start", "Options", "HighScores", "About", "Leave Game" };

        private const int SPACE_BETWEEN_OPTION = 24;
        private const int MARGE_LEFT_MENU = 24;
        private const int MARGE_TOP_MENU = 10;
        private const int MARGE_LEFT_TITLE = 10;
        private const int SPACE_MENU = 3;
        private const int MARGE_INFO_MENU = 40;

        private const string ABOUT_INFO = "Created by Jonathan & Filipe";

        /// <summary>
        /// Constructeur par défault
        /// </summary>
        public Menu()
        {
            _sound = true;
            _difficulty = false;
            _startPressed = true;
            _about = false;
            _positionMenu = 1;
            _currentLeft = 0;
            _margeTopTitle = 0;
        }

        /// <summary>
        /// Donne la valeur du booléen son du jeu
        /// </summary>
        /// <returns>son</returns>
        public bool GetSound()
        {
            return _sound;
        }

        /// <summary>
        /// Donne la valeur du booléen difficulté du jeu
        /// </summary>
        /// <returns></returns>
        public bool GetDifficulty()
        {
            return _difficulty;
        }

        /// <summary>
        /// Méthode qui permet de set la taille de la fenètre console du Menu et de lancer le Menu 
        /// </summary>
        public void LaunchMenu()
        {
            Console.WindowWidth = WIDTH_OF_MENU;
            Console.WindowHeight = HEIGHT_OF_MENU;
            Console.BufferWidth = WIDTH_OF_MENU;
            Console.BufferHeight = HEIGHT_OF_MENU;
            CreateMenu();
            MenuMouvement(SPACE_MENU);
        }

        /// <summary>
        /// Méthode qui permet d'afficher les différentes options du Menu
        /// </summary>
        public void CreateMenu()
        {
            WriteTitle();

            for (int i = 0; i < tab_Menu.Length; i++)
            {
                Console.SetCursorPosition(MARGE_LEFT_MENU, MARGE_TOP_MENU + i * SPACE_MENU);
                Console.WriteLine(tab_Menu[i]);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(MARGE_LEFT_MENU - 2, 10);
            WriteTriangle();
        }

        /// <summary>
        /// Méthode qui permet d'afficher le Titre du jeu
        /// </summary>
        public void WriteTitle()
        {
            for (int i = 0; i < tab_Title.Length; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - tab_Title[5].Length) / 2, _margeTopTitle + i);     // Centrer 
                Console.WriteLine(tab_Title[i]);
            }
        }

        /// <summary>
        /// Méthode qui permet de naviguer dans le Menu à l'aide les touches du haut et du bas et qui nous envoi dans la méthode du switch de séléction quand on appuye sur espace
        /// </summary>
        /// <param name="space">espace entre les différentes options du Menu</param>
        public void MenuMouvement(int space)
        {
            do
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (_positionMenu != 1)
                        {
                            _positionMenu--;
                            Erase();
                            Console.CursorTop -= space;
                            WriteTriangle();
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (_positionMenu != 5)
                        {
                            _positionMenu++;
                            Erase();
                            Console.CursorTop += space;
                            WriteTriangle();
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        Selection();
                        break;
                }
            } while (_startPressed);
        }

        /// <summary>
        /// Méthode qui affiche le triangle en rouge
        /// </summary>
        public void WriteTriangle()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("►");
            Console.CursorLeft--;
            Console.ResetColor();
        }

        /// <summary>
        /// Méthode qui efface le triangle
        /// </summary>
        public void Erase()
        {
            Console.Write(" ");
            Console.CursorLeft--;
        }

        /// <summary>
        /// Méthode qui permet de séléctionner les différentes options du Menu en appuyant sur espace
        /// </summary>
        public void Selection()
        {
            switch (_positionMenu)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("LE JEU EST EN COURS DE DEVELOPEMENT");
                    _startPressed = false;
                    // Lancer le jeu
                    break;
                case 2:
                    Erase();
                    Options();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Les Highscores sont en cours de dev");

                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        _positionMenu = 1;
                        Console.Clear();
                        LaunchMenu();
                    }
                    // HighScore
                    break;
                case 4:
                    About(19);
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
            }
        }

        /// <summary>
        /// Méthode qui affiche la partie A propos du jeu quand on séléctionne About à l'aide de la touche espace
        /// </summary>
        /// <param name="top">curseurTop de la console</param>
        public void About(int top)
        {
            _about = true;
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;
            Console.SetCursorPosition(MARGE_INFO_MENU, top);
            Console.WriteLine(ABOUT_INFO);
            Console.SetCursorPosition(currentLeft, currentTop);
            Erase();
            Console.CursorLeft = MARGE_INFO_MENU - 2;
            WriteTriangle();
            do
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    _about = false;
                }
            } while (_about);
            Erase(ABOUT_INFO.Length + 2);       // +2 Pour éffacer les 2 caractères en plus du triangle et de l'espace avant le string ABOUT_INFO
            Console.CursorLeft = SPACE_BETWEEN_OPTION - 2;
            WriteTriangle();
        }

        /// <summary>
        /// Méthode qui affiche et permet de naviguer dans les options de son et de difficulté et changer ces deux options à l'aide de la touche espace
        /// </summary>
        public void Options()
        {
            Console.CursorLeft = MARGE_INFO_MENU;
            if (_sound)
            {
                Console.Write("Sound : ON");
                Console.CursorLeft += 7;
            }
            else
            {
                Console.Write("Sound : OFF");
                Console.CursorLeft += 6;
            }

            if (_difficulty)
            {
                Console.Write("Difficulty : Francis");
            }
            else
            {
                Console.Write("Difficulty : Lazar");
            }
            Console.CursorLeft = MARGE_INFO_MENU - 2;
            WriteTriangle();

            bool navigation = true;
            bool end = true;
            while (end)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (!navigation)
                        {
                            Erase();
                            Console.CursorLeft -= 17;
                            WriteTriangle();
                            navigation = true;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (navigation)
                        {
                            Erase();
                            Console.CursorLeft += 17;
                            WriteTriangle();
                            navigation = false;
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        if (navigation)
                        {
                            MenuSound();
                        }
                        else
                        {
                            MenuDifficulty();
                        }
                        break;
                    case ConsoleKey.Escape:
                        end = false;
                        Console.CursorLeft = MARGE_INFO_MENU - 2;
                        Erase(39);
                        Console.CursorLeft = SPACE_BETWEEN_OPTION - 2;
                        WriteTriangle();
                        break;
                }
            }
        }

        /// <summary>
        /// Méthode qui change la difficulté en fonction de la valeur du booléen
        /// </summary>
        public void MenuDifficulty()
        {
            _currentLeft = Console.CursorLeft;
            Console.CursorLeft += 15;
            Erase(8);
            if (_difficulty)
            {
                _difficulty = false;
                Console.Write("Lazar");
                Console.CursorLeft = _currentLeft;
            }
            else
            {
                _difficulty = true;
                Console.Write("Francis");
                Console.CursorLeft = _currentLeft;
            }
        }

        /// <summary>
        /// Méthode qui change le son en fonction de la valeur du booléen
        /// </summary>
        public void MenuSound()
        {
            _currentLeft = Console.CursorLeft;
            Console.CursorLeft += 10;
            Erase(3);
            if (_sound)
            {
                _sound = false;
                Console.Write("OFF");
                Console.CursorLeft = _currentLeft;
            }
            else
            {
                _sound = true;
                Console.Write("ON");
                Console.CursorLeft = _currentLeft;
            }
        }

        /// <summary>
        /// Méthode qui efface le nombre de caractère voulu 
        /// </summary>
        /// <param name="number">nombre de caractère qu'on souhaite effacer</param>
        public void Erase(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Console.Write(" ");
            }
            Console.CursorLeft -= number;
        }
    }
}
