using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deSPICYtoINVADER.Characters
{
    public class Bullet
    {
        /* Constante */
        private const char DESIGN = '▓';
        /* Propriétés */
        /// <summary>
        /// La direction de la bullet, soit vers le haut : -1, soit vers le bas : 1
        /// </summary>
        public int Direction { get; private set; }
        /// <summary>
        /// La position de la bullet, c'est un "Point" avec une coord X et Y
        /// </summary>
        public Point Position { get; private set; }
        /// <summary>
        /// Pour savoir si on doit supprimer la bullet ou pas
        /// </summary>
        public bool GonnaDelete { get; set; }

        /// <summary>
        /// Constructeurs de Bullet
        /// </summary>
        /// <param name="position">Position de la bull (X, Y)</param>
        /// <param name="direction">Haut ou bas (-1 ou 1)</param>
        public Bullet(Point position, int direction)
        {
            Position = position;
            Direction = direction;
            GonnaDelete = false;
        }

        /// <summary>
        /// Ajoute le char de la bullet aux tableau de char
        /// </summary>
        public void Draw()
        {
            Game.allChars[Position.Y][Position.X] = DESIGN;
        }

        /// <summary>
        /// Si la bullet monte et qu'elle est pas au max ou si elle descend et qu'elle est pas au max : La bullet bouge
        /// Sinon elle se supprime
        /// </summary>
        public void Move()
        {
            if ((Direction == -1 && Position.Y > 1) || (Direction == 1 && Position.Y < Game.HEIGHT_OF_WINDOWS - 2))//Condition pour voir que la bullet ne va pas trop loin
            {
                Position.Y += Direction;//Monte ou descend
            }
            else
            {
                GonnaDelete = true;
            }
        }

        /// <summary>
        /// Update de la bullet :
        /// Draw la bullet à chaque tic
        /// Tous les 7 tics, Move la bullet
        /// </summary>
        public void Update()
        {
            Draw();
            if (Game.tics % 4 == 0)
                Move();
        }
    }
}
