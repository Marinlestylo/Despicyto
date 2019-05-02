using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy_Nvader
{
    public class Player
    {
        private List<Point> touched = new List<Point>();
        private ConsoleColor color = ConsoleColor.Yellow;
        private int invincible;
        public bool Music { get; private set; }
        private static readonly string[] PLAYER = new string[10]//Le tableau contient les string de la fusée du joueur chaque string représente une ligne
        /*{
            "     ▄     ",
            "    / \    ",
            "   | o |   ",
            "   |   |   ",
            "   | o |   ",
            "   |   |   ",
            "  /| o |\   ",
            " / |___| \  ",
            "| / |@| \ |",
            "|/  |@|  \|"
        };*/
        {
            "▄",
            "/ \\",
            "| o |",
            "|   |",//UN GRAND MERCI A KALINVA POUR l'AVIS CRITIQUE APPORTÉ LORS DU DESIGN DU VAISSEAU
            "| o |",
            "|   |",
            "/| o |\\",
            "/ |___| \\",
            "| / |@| \\ |",
            "|/  |@|  \\|"
        };
        /*{
            "     ▄    ",
            "   ▄███▄   ",
            "  █  █  █  ",
            "  █  █  █  ",//UN GRAND MERCI A KALINVA POUR l'AVIS CRITIQUE APPORTÉ LORS DU DESIGN DU VAISSEAU
            "  █▀▄█▄▀█  ",
            "  █     █  ",
            "  █     █ ",
            " █   ▄   █ ",
            "█   █ █   █",
            " ▀██   ██▀ "
        };*/
        private const int VALUE_OF_MOVEMENT = 1;//Nombre de case que parcourt le joueur à chaque fois
        private readonly int topPosition = Program.HEIGHT_OF_WINDOWS - PLAYER.Length - 1;//Position top en fonction de la hauteur de la console - la taille du joueur - 1 sinon c'est trop bas et ça crash(on ne peut pas écrire sur la dernière ligne)
        //Hauteur : 69

        private int _playerPosition;//Nouvelle position du joueur
        private int _playerLives;//Le nombe de vie du joueur
        public int _playerScore { get; set; }//Valeur du score du joueur
        public int _playerDirection;//Sens du joueur mode auto 1 ou -1
        public bool _modeAuto;//Booléen qui permet de switch entre le mode auto et manuel

        /// <summary>
        /// Constructeur de Player. Crée un joueur avec une position, un nombre de vie et une liste de points qui peuvent être touché par les ennemis.
        /// </summary>
        public Player()
        {
            _playerPosition = Program.WIDTH_OF_WIDOWS / 2;
            _playerLives = 9;
            _playerScore = 0;
            invincible = 0;
            Music = false;
            GetHitBox();
        }

        /// <summary>
        /// Via une double boucle, cette méthode stocke chaque caractère du dessins du player à la bonne position dans le tableau "allChars"
        /// </summary>
        public void DrawPlayer()
        {
            for (int i = 0; i < PLAYER.Length; i++)//Boucle pour chaque ligne
            {
                for (int j = 0; j < PLAYER[i].Length; j++)//Boucle pour chaque char
                {
                    Program.allChars[topPosition + i][_playerPosition - PLAYER[i].Length / 2 + j] = PLAYER[i][j];
                }
            }
        }

        /// <summary>
        /// Méthode pour déplacer la positon du joueur. Set l'ancienne position à la position du joueur puis set la position du joueur a sa nouvelle positon et utilise la méthode Draw pour le redessiner instantanément
        /// </summary>
        /// <param name="movement">valeur de déplacement</param>
        public void Move(int movement)
        {
            _playerPosition += movement;
            foreach (Point p in touched)
            {
                p.X += movement;
            }
        }

        /// <summary>
        /// Set le booléen du joueur a false pour éviter de spamm les tirs
        /// </summary>
        public void Shoot()
        {
            if (Program.allBullets[Program.allBullets.Length - 1] == null)//Si il n'y a pas d'autre bullet, on peut tirer sinon non
            {
                Program.allBullets[Program.allBullets.Length - 1] = new Bullet(_playerPosition, topPosition - 2, 1, 1);
            }
        }

        public void AddOnScore()
        {
            _playerScore+=25;
        }

        public void ShowLives()
        {
            Program.allChars[0][Program.WIDTH_OF_WIDOWS - 8] = Convert.ToChar(_playerLives.ToString());
            Program.allChars[0][Program.WIDTH_OF_WIDOWS - 7] = '♥';
        }


        /// <summary>
        /// Si le joueur se fait tirer dessus, on regarde si il lui reste plusieurs vies ou non. Si oui, il perd une vie et le jeu continue. Sinon le jeu s'arrête et affiche un message.
        /// </summary>
        public void GetShot(Bullet bull)
        {

            if (bull.PosY >= topPosition)//On check que les bullets qui sont à la position du joueur ou plus bas
            {
                foreach (Point p in touched)
                {
                    if (bull.PosX == p.X && bull.PosY == p.Y && Program.tics >= invincible)
                    {
                        bull.GonnaDelete = true;
                        //Perdre une vie
                        if (_playerLives > 1)
                        {
                            _playerLives--;
                            Console.ForegroundColor = color;
                            invincible = Program.tics + 100;
                        }
                        else
                        {
                            Program.game = false;
                        }
                    }
                }
            }
        }

        public void ShowScore()
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("Vous avez perdu ! Quel dommage . . . ");
            Console.WriteLine("Voici votre score : " + _playerScore);
        }

        public void EpilepsicMode()
        {
            if (_playerLives < 4)
            {
                Music = true;
            }
        }

        public void GetHitBox()
        {
            //De gauche à droite
            touched.Add(new Point(_playerPosition - 5, topPosition + 8));
            touched.Add(new Point(_playerPosition - 4, topPosition + 7));
            touched.Add(new Point(_playerPosition - 3, topPosition + 6));
            touched.Add(new Point(_playerPosition - 2, topPosition + 2));
            touched.Add(new Point(_playerPosition - 2, topPosition + 3));
            touched.Add(new Point(_playerPosition - 2, topPosition + 4));
            touched.Add(new Point(_playerPosition - 2, topPosition + 5));
            touched.Add(new Point(_playerPosition - 1, topPosition + 1));
            touched.Add(new Point(_playerPosition, topPosition));
            touched.Add(new Point(_playerPosition + 1, topPosition + 1));
            touched.Add(new Point(_playerPosition + 2, topPosition + 2));
            touched.Add(new Point(_playerPosition + 2, topPosition + 3));
            touched.Add(new Point(_playerPosition + 2, topPosition + 4));
            touched.Add(new Point(_playerPosition + 2, topPosition + 5));
            touched.Add(new Point(_playerPosition + 3, topPosition + 6));
            touched.Add(new Point(_playerPosition + 4, topPosition + 7));
            touched.Add(new Point(_playerPosition + 5, topPosition + 8));
        }

        /// <summary>
        /// Permet de gérer les actions du joueur via un switch. Il peut faire 3 choses : Aller à droite, aller à gauche et tirer.
        /// Permet également d'empêcher le joueur d'aller trop à gauche ou trop à droite (Pas de politique ;>)
        /// </summary>
        public void PlayerUpdate()
        {
            if (Program.tics % 2 == 0 && _playerPosition > PLAYER[4].Length / 2 + Program.MARGIN && _playerPosition < (Program.WIDTH_OF_WIDOWS - 1) - (PLAYER[4].Length / 2) - Program.MARGIN)
            {
                Move(_playerDirection);
            }
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)//Lis la touche du clavier sur laquelle on appuie
                {
                    case ConsoleKey.RightArrow:                     // 80 - 1  (car 80eme char = pos 79 )    11 / 2 = 5             1    TOT : 73 position max
                        if (_playerPosition + VALUE_OF_MOVEMENT <= (Program.WIDTH_OF_WIDOWS - 1) - (PLAYER[4].Length / 2) - Program.MARGIN)//Si la position est plus petite que la valeur max, le joueur se déplace normalement
                        {
                            if (_modeAuto)
                            {
                                _playerDirection = 1;
                            }
                            else
                            {
                                Move(VALUE_OF_MOVEMENT);//Se déplace (vers la droite)
                            }
                        }
                        else//Si la position du joueur va plus loin que la valeur max, on lui donne la valeur max
                        {
                            Move(((Program.WIDTH_OF_WIDOWS - 1) - (PLAYER[4].Length / 2) - Program.MARGIN) - _playerPosition);//La forumule calcule la différence entre la positon actuelle et la valeur max et donne le résultat à la méthode Move                   
                        }
                        break;
                    case ConsoleKey.LeftArrow:                   //         11/2 = 5   + 1 TOT : 6 position minimale
                        if (_playerPosition - VALUE_OF_MOVEMENT >= PLAYER[4].Length / 2 + Program.MARGIN)
                        {
                            if (_modeAuto)
                            {
                                _playerDirection = -1;
                            }
                            else
                            {
                                Move(-1 * VALUE_OF_MOVEMENT);//Se déplace (vers la gauche)
                            }
                        }
                        else
                        {
                            Move(-1 * (_playerPosition - (PLAYER[4].Length / 2 + Program.MARGIN)));//Formule qui permet, quelque soit la valeur de mouvement, d'aller le plus à gauche possible au lieu de bloquer avant la marge
                        }
                        break;
                    case ConsoleKey.Spacebar://set le tir sur la touche espace
                        Shoot();
                        break;
                    case ConsoleKey.M: // Permet d'activer ou de désactiver le mode auto
                        if (_modeAuto)
                        {
                            _playerDirection = 0;
                            _modeAuto = false;
                        }
                        else
                        {
                            _playerDirection = 0;
                            _modeAuto = true;
                        }
                        break;
                    case ConsoleKey.DownArrow: // Permet de stoper le joueur en mode auto
                        _playerDirection = 0;
                        break;
                }
            }
            DrawPlayer();
            if (Program.tics == invincible && Console.ForegroundColor == color)//invicibilité du joueurs
            {
                Console.ResetColor();
            }
            ShowLives();
            EpilepsicMode();
        }
    }
}
