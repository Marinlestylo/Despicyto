using deSPICYtoINVADER.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace deSPICYtoINVADER
{
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

        /* Attributs */
        private string[] _options = new string[5] { "Jouer", "Options", "Highscore", "A propos", "Quitter" };
        private int _index;
        private bool _closeGame;

        /// <summary>
        /// Constructeur du menu
        /// set l'index à 0
        /// </summary>
        public Menu()
        {
            _index = 0;
            _closeGame = false;//ATTENTIOn a delete?
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
            for (int i = 0; i < Sprites.mainTitle.Length; i++)
            {
                Console.SetCursorPosition(titlePadding.X , titlePadding.Y + i);
                Console.WriteLine(Sprites.mainTitle[i]);
            }

            //boucle pour les options du menu
            for (int i = 0; i < _options.Length; i++)
            {
                Console.SetCursorPosition(menuPadding.X, menuPadding.Y + i * TOP_SPACE_MENU);
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
                Console.SetCursorPosition(menuPadding.X - 3, menuPadding.Y + i * TOP_SPACE_MENU);
                Console.WriteLine("  ");
                Console.SetCursorPosition(menuPadding.X + _options[i].Length + 1, menuPadding.Y + i * TOP_SPACE_MENU);
                Console.WriteLine("  ");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(menuPadding.X - 3, menuPadding.Y + _index * TOP_SPACE_MENU);//Position du chevron de gauche
            Console.Write(">>");
            Console.SetCursorPosition(menuPadding.X + _options[_index].Length + 1, menuPadding.Y + _index * TOP_SPACE_MENU);//Position du chevron de droite
            Console.Write("<<");
            Console.ResetColor();
        }

        private void Select()
        {
            switch (_index)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    HighScore();
                    break;
                case 3:
                    About();
                    break;
                case 4:
                    _closeGame = true;
                    break;
            }
        }

        /// <summary>
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
        /// Affiche des infos sur le jeu
        /// </summary>
        private void About()
        {
            Console.Clear();
            Sprites.DrawTitle(Sprites.aboutTitle, new Point(15, 0));
            Console.SetCursorPosition(33, 6);
            Console.WriteLine("Created by :");
            Sprites.DrawTitle(Sprites.joAscii, new Point(2, 8));
            Sprites.DrawTitle(Sprites.andAscii, new Point(10, 16));
            ReturnToMenu();
        }

        private void HighScore()
        {
            Console.Clear();
            Sprites.DrawTitle(Sprites.highScoreTitle, new Point(5, 0));
            Console.WriteLine("Highscore");//texte à modifier:)
            Console.WriteLine("Appoui sur escape pour reviendre en arrière !");//texte à modifier:)
            ReturnToMenu();
        }

        /// <summary>
        /// Permet de naviguer dans le menu et de sélectionner une options
        /// </summary>
        private void Navigate()
        {
            while (!_closeGame)
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
