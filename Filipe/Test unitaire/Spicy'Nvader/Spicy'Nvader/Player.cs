using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Spicy_Nvader
{
    public class Player
    {
        private List<Point> touched = new List<Point>();
        private int invincible;
        private const string SCORE = "Score : ";
        private const string LIVES = "Vies restantes : ";
        public bool Music { get; private set; }
        private int _timingBlink;
        private SoundPlayer _bulletSound = new SoundPlayer("Sounds\\Bullet.wav");
        private SoundPlayer _hurtSound = new SoundPlayer("Sounds\\Hurt.wav");
        private static readonly string[] PLAYER1 = new string[10]//Le tableau contient les string de la fusée du joueur chaque string représente une ligne
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
        private string[] PLAYER = new string[10];
        private static readonly string[] PLAYER2 = new string[10]
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
        {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
        };
        private const int VALUE_OF_MOVEMENT = 1;//Nombre de case que parcourt le joueur à chaque fois
        private readonly int topPosition = Program.HEIGHT_OF_WINDOWS - PLAYER1.Length - 1;//Position top en fonction de la hauteur de la console - la taille du joueur - 1 sinon c'est trop bas et ça crash(on ne peut pas écrire sur la dernière ligne)
        //Hauteur : 69
        private int _playerPosition;//position du joueur
        private int _playerLives;//Le nombe de vie du joueur
        public int PlayerScore { get; private set; }//Valeur du score du joueur
        private bool isFlash = false;

        /// <summary>
        /// Constructeur de Player. Crée un joueur avec une position, un nombre de vie et une liste de points qui peuvent être touché par les ennemis.
        /// </summary>
        public Player()
        {
            _playerPosition = Program.WIDTH_OF_WIDOWS / 2;
            _playerLives = 9;
            PlayerScore = 0;
            invincible = 0;
            Music = false;
            GetHitBox();
            PLAYER = PLAYER1;
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

        public void Flash()
        {
            if (isFlash)
            {
                _timingBlink++;
                if (_timingBlink % 20 == 0)
                {
                    PLAYER = PLAYER1;
                }
                else if(_timingBlink % 10 == 0)
                {
                    PLAYER = PLAYER2;
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
                if (_playerLives > 3)
                {
                    _bulletSound.Play();
                }
            }
        }

        public void AddOnScore()
        {
            PlayerScore+=25;
        }

        public void ShowLives()
        {
            for (int i = 0; i < LIVES.Length; i++)
            {
                Program.allChars[0][Program.WIDTH_OF_WIDOWS - LIVES.Length - 8 + i] = LIVES[i];
            }
            Program.allChars[0][Program.WIDTH_OF_WIDOWS - 8] = Convert.ToChar(_playerLives.ToString());
            Program.allChars[0][Program.WIDTH_OF_WIDOWS - 7] = '♥';
        }

        public void ShowScore()
        {
            for (int i = 0; i < SCORE.Length; i++)
            {
                Program.allChars[1][Program.WIDTH_OF_WIDOWS - SCORE.Length - 8 + i] = SCORE[i];
            }
            for (int i = 0; i < PlayerScore.ToString().Length; i++)
            {
                Program.allChars[1][Program.WIDTH_OF_WIDOWS - 8 + i] = PlayerScore.ToString()[i];
            }
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
                        if (_playerLives > 3)
                        {
                            _hurtSound.Play();
                        }
                        //Perdre une vie
                        if (_playerLives > 1)
                        {
                            _playerLives--;
                            EpilepsicMode();
                            isFlash = true;
                            _timingBlink = 9;
                            invincible = Program.tics + 200;
                        }
                        else
                        {
                            Program.game = false;
                        }
                    }
                }
            }
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
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)//Lis la touche du clavier sur laquelle on appuie
                {
                    case ConsoleKey.RightArrow:                     // 80 - 1  (car 80eme char = pos 79 )    11 / 2 = 5             1    TOT : 73 position max
                        if (_playerPosition + VALUE_OF_MOVEMENT <= (Program.WIDTH_OF_WIDOWS - 1) - (PLAYER[4].Length / 2) - Program.MARGIN)//Si la position est plus petite que la valeur max, le joueur se déplace normalement
                        {
                            Move(VALUE_OF_MOVEMENT);//Se déplace (vers la droite)
                        }
                        else//Si la position du joueur va plus loin que la valeur max, on lui donne la valeur max
                        {
                            Move(((Program.WIDTH_OF_WIDOWS - 1) - (PLAYER[4].Length / 2) - Program.MARGIN) - _playerPosition);//La forumule calcule la différence entre la positon actuelle et la valeur max et donne le résultat à la méthode Move
                        }
                        break;
                    case ConsoleKey.LeftArrow:                   //         11/2 = 5   + 1 TOT : 6 position minimale
                        if (_playerPosition - VALUE_OF_MOVEMENT >= PLAYER[4].Length / 2 + Program.MARGIN)
                        {
                            Move(-1 * VALUE_OF_MOVEMENT);//Se déplace (vers la gauche)
                        }
                        else
                        {
                            Move(-1 * (_playerPosition - (PLAYER[4].Length / 2 + Program.MARGIN)));//Formule qui permet, quelque soit la valeur de mouvement, d'aller le plus à gauche possible au lieu de bloquer avant la marge
                        }
                        break;
                    case ConsoleKey.Spacebar://set le tir sur la touche espace
                        Shoot();
                        break;
                }
            }
            DrawPlayer();
            if (Program.tics == invincible)//invicibilité du joueurs
            {
                isFlash = false;
                PLAYER = PLAYER1;
            }
            ShowLives();
            ShowScore();
            Flash();
        }
    }
}
