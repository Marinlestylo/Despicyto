using deSPICYtoINVADER.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deSPICYtoINVADER.Characters
{
    public class Enemy : Character
    {
        /* Propriétés */
        /// <summary>
        /// Coin en haut à gauche de l'Enemy. Ca va servir pour la hitbox
        /// </summary>
        public Point TopLeftCorner { get; private set; }
        /// <summary>
        /// Coin en bas à droite de l'Enemy. Ca va servir pour la hitbox.
        /// </summary>
        public Point BottomRightCorner { get; private set; }

        /// <summary>
        /// Constructeur de la classe Enemy
        /// </summary>
        /// <param name="position">L'endroit où l'ennemi va spawn</param>
        public Enemy(Point position, string[] design) : base(1, position)
        {
            _design = design;//choix du design de l'ennmi
            Hitbox();//Update la hitBox
        }

        /// <summary>
        /// Méthode qui update l'Enemy
        /// </summary>
        public override void Update()
        {
            Draw();
            Shoot();
        }

        /// <summary>
        /// Méthode qui permet de faire bouger l'Enemy.
        /// A noter que sa hitbox le suit.
        /// </summary>
        /// <param name="direction">Direction vaut soit 1 soit -1</param>
        protected override void Move(int direction)
        {
            _position.X += direction;
            Hitbox();//Update la hitbox de l'enemy
        }

        public void MoveInSwarm(int direction)
        {
            Move(direction);
        }

        public void GoDown()
        {
            _position.Y += 3;
        }

        /// <summary>
        /// Méthode héritée de Character
        /// Check si une Bullet entre en collision avec l'Enemy
        /// </summary>
        /// <param name="bull">Une bullet</param>
        public override void GetShot(Bullet bull)
        {
            if (bull.Direction == -1 && bull.Position.X >= TopLeftCorner.X && bull.Position.X <= BottomRightCorner.X && 
                bull.Position.Y >= TopLeftCorner.Y && bull.Position.Y < BottomRightCorner.Y)
            {
                bull.GonnaDelete = true;
                LoseLife();
                Player.AddOnScore(37);
            }
        }

        /// <summary>
        /// Enlève une vie à l'enemy
        /// Si il n'en a que une, set son GonnaDelete à true
        /// </summary>
        private void LoseLife()
        {
            Sound.PlaySound("enemy");
            if (Life > 1)
            {
                Life--;
            }
            else
            {
                GonnaDelete = true;
            }
        }

        /// <summary>
        /// Méthode héritée de Character
        /// Permet de shooter (crée une bullet) si le random passe
        /// </summary>
        protected override void Shoot()
        {
            if (Utils.RandomValue(1001) == 42)
            {
                Game.allBullets.Add(new Bullet(new Point(_position.X, _position.Y + Sprites.enemyDesign.Length), 1));
            }
        }

        /// <summary>
        /// Méthode pour mettre à jour le Coin Haut Gauche et le Coin Bas Droit
        /// </summary>
        public void Hitbox()
        {
            TopLeftCorner = new Point(_position.X - _design[4].Length / 2, _position.Y);
            BottomRightCorner = new Point(_position.X + _design[4].Length / 2, _position.Y + _design.Length - 1);
        }
    }
}
