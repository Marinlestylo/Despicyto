///ETML
///Auteur : Jonathan Friedli et Filipe Andrade Barros
///Date : 20.05.19
///Description : Classe Player qui hérite de la classe character
using deSPICYtoINVADER.Utils;
using System;
using System.Collections.Generic;

namespace deSPICYtoINVADER.Characters
{
    /// <summary>
    /// Classe permettant de créer un player, hérite de character
    /// </summary>
    public class Player : Character
    {
        /* Propriétés */
        /// <summary>
        /// Score du joueur, s'incrémente dans la classe Enemy (enemy tué) et dans la classe Swarm(Essaim tué).
        /// Variable static car elle doit être appelée dans Swarm et Enemy, il n'y a aucune raison de créer une instance de player la bas.
        /// </summary>
        public static int Score;
        /// <summary>
        /// Nombre de vie max
        /// </summary>
        public const int MAX_LIVES = 9;

        /* Attributs */
        private const int INVINCIBLE_TIMING = 201;//Faire finir le int par un pour commencer à clignoter immédiatement
        private const string SCORE = "Score : ";//Affichage du score
        private const string LIVES = "Vies restantes : ";//afichage des vies

        private List<Point> _touched;//Liste de points constituant la hitbox
        private bool _autoMove;//bool pour savoir si on est en mode autoMove (Appuyer une fois et ça bouge tout seul)
        private int _nextShootTiming;//Timing pour savoir quand on peut shooter
        private int _invincible;//timing de l'invincibilité

        /// <summary>
        /// Constructeur de la classe, il reprend le constructeur de "Character"
        /// </summary>
        public Player() : base(MAX_LIVES, new Point(Game.WIDTH_OF_WIDOWS / 2, Game.HEIGHT_OF_WINDOWS - Sprites.playerDesign.Length - 1 ))
        {
            _design = Sprites.playerDesign;
            Score = 0;
            _touched = new List<Point>();
            _autoMove = false;
            _nextShootTiming = 0;
            _invincible = 0;
            GetHitBox();
        }

        /// <summary>
        /// Méthode héritée de character. 
        /// Update les informations (vie et score), le dessin du joueur, lis les input du clavier et clignote si le joueur est touché
        /// </summary>
        public override void Update()
        {
            ShowInformations();
            Draw();
            Input();
            Blink();
        }

        #region Informations
        /// <summary>
        /// Affiche la vie et le score
        /// </summary>
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

        /// <summary>
        /// Ajoute les points au score du joueur
        /// </summary>
        /// <param name="points">Nombre de point à ajouter</param>
        public static void AddOnScore(int points)
        {
            if (Menu.Difficulty == 2)
            {
                Score += points * 4;
            }
            else
            {
                Score += points;
            }
        }

        /// <summary>
        /// A la fin du jeur, on appelle cette méthode pour reset le score et GonnaDelete
        /// </summary>
        public void Reset()
        {
            Life = MAX_LIVES;
            GonnaDelete = false;
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

        /// <summary>
        /// Lis les touches entrée par le joueur.
        /// Effectue une action pour les touches suivantes : Gauche (va à gauche), Droite (va à droite), Espace (tir), M (active le autoMove), Bas (Arrête la fusée sur place)
        /// </summary>
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
        /// <summary>
        /// Méthode pour shooter, on peut shoot une fois tous les 75 tics
        /// </summary>
        protected override void Shoot()
        {
            if (_nextShootTiming <= Game.tics)
            {
                Game.allBullets.Add(new Bullet(new Point(_position.X, _position.Y), -1));
                Sound.PlaySound("shoot");
                _nextShootTiming = Game.tics + 75;
            }
        }

        /// <summary>
        /// Pour savoir si on se fait toucher par une Bullet.
        /// On l'utilise sur toutes les bullets descendante et si elles sont à la hauteur du joueur. Inutile de tester si on se fait toucher par une bullet qui est en haut de l'écran
        /// </summary>
        /// <param name="bull">Bullet qui pourrait nous toucher</param>
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
                        if (Life == MAX_LIVES/3)
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
        
        /// <summary>
        /// Clignote pendant 200tics si le joueur se fait toucher
        /// (Alterne entre un sprite vide et le sprite normal tous les 10tics pendant 200 tics)
        /// </summary>
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
