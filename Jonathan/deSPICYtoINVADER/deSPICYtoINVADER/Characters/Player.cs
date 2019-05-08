using deSPICYtoINVADER.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deSPICYtoINVADER.Characters
{
    public class Player : Character
    {
        /* Propriétés */
        public static int Score;

        /* Attributs */
        private const int INVINCIBLE_TIMING = 201;//Faire finir le int par un pour commencer à clignoter immédiatement

        private List<Point> _touched;
        private bool _autoMove;
        private int _nextShootTiming;
        private int _invincible;
        private const string SCORE = "Score : ";
        private const string LIVES = "Vies restantes : ";

        /// <summary>
        /// Constructeur de la classe, il reprend le constructeur de "Character"
        /// </summary>
        public Player() : base(9, new Point(Game.WIDTH_OF_WIDOWS / 2, Game.HEIGHT_OF_WINDOWS - Sprites.playerDesign.Length - 1 ))
        {
            _design = Sprites.playerDesign;
            Score = 0;
            _touched = new List<Point>();
            _autoMove = false;
            _nextShootTiming = 0;
            _invincible = 0;
            GetHitBox();
        }

        public override void Update()
        {
            ShowInformations();
            Draw();
            Input();
            Blink();
        }

        #region Informations
        //affiche la vie et le score
        private void ShowInformations()
        {
            //Vies
            for (int i = 0; i < LIVES.Length; i++)//affiche "Vies restantes :"
            {
                Game.allChars[0][Game.WIDTH_OF_WIDOWS - LIVES.Length - 8 + i] = LIVES[i];
            }
            Game.allChars[0][Game.WIDTH_OF_WIDOWS - 8] = Convert.ToChar(Life.ToString());
            Game.allChars[0][Game.WIDTH_OF_WIDOWS - 7] = ' ';
            Game.allChars[0][Game.WIDTH_OF_WIDOWS - 6] = '♥';
            //Score
            for (int i = 0; i < SCORE.Length; i++)//affiche "Score :"
            {
                Game.allChars[1][Game.WIDTH_OF_WIDOWS - SCORE.Length - 8 + i] = SCORE[i];
            }
            for (int i = 0; i < Score.ToString().Length; i++)//affiche le score char par char
            {
                Game.allChars[1][Game.WIDTH_OF_WIDOWS - 8 + i] = Score.ToString()[i];
            }
        }

        public static void AddOnScore(int points)
        {
            Score += points;
        }
        #endregion

        #region Move
        /// <summary>
        /// Permet de bouger la position du joueur (que latéralement)
        /// </summary>
        /// <param name="direction">1 ou -1 en fonction de si on va à gauche ou a droite</param>
        protected override void Move(int direction)
        {
            _position.X += direction;
            //Actualise la hitbox
            foreach (Point p in _touched)
            {
                p.X += direction;
            }
        }

        /// <summary>
        /// Si le joueur arrive dans la marge, il ne peut plus avancer
        /// </summary>
        /// <returns>return true si le joueur peut encore avancer, false sinon</returns>
        private bool CanIMove()
        {
            if (_direction == 1 && _position.X == Game.WIDTH_OF_WIDOWS - 1 - Game.MARGIN - (Sprites.playerDesign[9].Length / 2))
            {
                return false;
            }
            else if (_direction == -1 && _position.X == Game.MARGIN + (Sprites.playerDesign[9].Length / 2))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Si le mode autoMove est a true, le vaisseau bouge tout seul tous les 5 tics (Dans la direction définie)
        /// </summary>
        private void AutoMove()
        {
            if (_autoMove && Game.tics % 2 == 0 && CanIMove())
            {
                Move(_direction);
            }
        }

        private void Input()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)//Lis la touche du clavier sur laquelle on appuie
                {
                    case ConsoleKey.RightArrow://Flèche de droite
                        _direction = 1;
                        if (!_autoMove && CanIMove())
                        {
                           Move(_direction); 
                        }
                        break;
                    case ConsoleKey.LeftArrow://Flèche de gauche
                        _direction = -1;
                        if (!_autoMove && CanIMove())
                        {
                            Move(_direction);
                        }
                        break;
                    case ConsoleKey.DownArrow://Stoper le vaisseau
                        _direction = 0;
                        break;
                    case ConsoleKey.Spacebar://set le tir sur la touche espace
                        Shoot();
                        break;
                    case ConsoleKey.M://Active ou désactive le mode auto
                        if (_autoMove)
                        {
                            _autoMove = false;
                        }
                        else
                        {
                            _autoMove = true;
                            _direction = 0;
                        }
                        break;
                }
            }
            AutoMove();//Si le mode auto est on
            CanIMove();//Vérifie si on peut bouger
        }
        #endregion

        #region Shoot/GetShot
        protected override void Shoot()
        {
            if (_nextShootTiming <= Game.tics)
            {
                Game.allBullets.Add(new Bullet(new Point(_position.X, _position.Y), -1));
                Sound.PlaySound("shoot");
                _nextShootTiming = Game.tics + 75;
            }
        }

        public override void GetShot(Bullet bull)
        {
            if (bull.Position.Y >= _position.Y && bull.Direction == 1)//Si la bullet est à la hauteur du joueur ou moins, et si elle va vers le bas
            {
                foreach (Point p in _touched)
                {
                    if (bull.Position.X == p.X && bull.Position.Y == p.Y && Game.tics > _invincible)
                    {
                        Life--;
                        _invincible = Game.tics + INVINCIBLE_TIMING;
                        Sound.PlaySound("hurt");
                        bull.GonnaDelete = true;//Supprimer la bullet qui nous a blessé
                        if (Life == 3)
                        {
                            Sound.BackMusic("hard");
                        }
                        else if (Life == 0)
                        {
                            GonnaDelete = true;
                        }
                    }
                }
            }
        }


        private void Blink()
        {
            if (Game.tics < _invincible)
            {
                if ((_invincible - Game.tics) % 20 == 0)
                {
                    _design = new string[] { "" };
                }
                else if((_invincible - Game.tics) % 10 == 0)
                {
                    _design = Sprites.playerDesign;
                }
            }
            else
            {
                _design = Sprites.playerDesign;
            }
        }

        /// <summary>
        /// Méthode qui crée tous les points auquel on peut se faire toucher (Le premier et le dernier char de chaque ligne).
        /// A noter que le point au centre tout en haut y est 2 fois.
        /// </summary>
        private void GetHitBox()
        {
            for (int i = 0; i < Sprites.playerDesign.Length; i++)
            {
                _touched.Add(new Point(_position.X - Sprites.playerDesign[i].Length / 2, _position.Y + i));//premier point de la ligne
                _touched.Add(new Point(_position.X + Sprites.playerDesign[i].Length / 2, _position.Y + i));//dernier point de la ligne
            }
        }
        #endregion
    }
}
