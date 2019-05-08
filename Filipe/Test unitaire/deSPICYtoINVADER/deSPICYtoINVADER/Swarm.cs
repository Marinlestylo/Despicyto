using deSPICYtoINVADER.Characters;
using deSPICYtoINVADER.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deSPICYtoINVADER
{
    public class Swarm
    {
        /* Propriétés */
        /// <summary>
        /// Liste de tous les ennemis dans l'essaim
        /// </summary>
        public List<Enemy> Enemies { get; }

        /* Attributs */
        private int _direction;//Direction de l'essaim
        private int _lastDirection;//Comme l'essaim n'a pas de position, on détecte que l'on doit descendre les Enemy 
        //quand _direction et _LastDirection ne sont plus pareil

        /// <summary>
        /// Constructeur de la classe Swarm
        /// </summary>
        public Swarm(int row, int col)
        {
            Enemies = new List<Enemy>();
            _direction = 1;
            _lastDirection = _direction;
            Create(row, col);
        }

        /// <summary>
        /// Update l'essaim
        /// </summary>
        public void Update()
        {
            if (Enemies.Count == 0)
            {
                Player.AddOnScore(217);
                Create(5, 5);
            }
            if (Game.tics % 7 == 0)
            {
                Move(ChangeDirection());
            }
            EnemyUpdate();
            DeleteEnemy();
            Invasion();
        }

        /// <summary>
        /// Permet de créer un nouveal essaim de "row" ligne et de "col" colonne
        /// </summary>
        /// <param name="row">nombre de ligne</param>
        /// <param name="col">nombre de colonne</param>
        public void Create(int row, int col)
        {
            _direction = 1;//Set la direction à 1 (vers la droite)
            _lastDirection = _direction;
            if (row < 7 && col < 10)//obligation d'avoir maximum 9 Enemy par ligne et 6 par colonne
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        Enemies.Add(new Enemy(new Point(9 + j * 13, 2 + i * 9), Sprites.enemyDesign));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// _direction va se faire attribuer la valeur passée en paramètre.
        /// Déplace tous les Enemy de l'essaim, si les Enemis changent de direction (direction et _lastdirection sont pas pareil), ils descendent de 5.
        /// _lastdirection prend la valeur de _direction.
        /// 
        /// </summary>
        /// <param name="dir">Direction dans laquelle vont les Enemis</param>
        public void Move(int dir)
        {
            _direction = dir;
            if (_lastDirection != _direction)
            {
                GoDown();
            }
            foreach (Enemy e in Enemies)
            {
                e.MoveInSwarm(_direction);
            }
            _lastDirection = _direction;
        }

        private void EnemyUpdate()
        {
            foreach (Enemy e in Enemies)
            {
                e.Update();
            }
        }

        private void GoDown()
        {
            foreach (Enemy en in Enemies)
            {
                en.GoDown();
            }
        }

        /// <summary>
        /// Check tous les Enemis de la liste, si un des Enemy est tout à droite ou tout à gauche, l'essaim change de direction
        /// </summary>
        /// <returns>La direction de l'essaim</returns>
        private int ChangeDirection()
        {
            if (_direction == 1)
            {
                foreach (Enemy e in Enemies)
                {
                    if (e.BottomRightCorner.X == Game.WIDTH_OF_WIDOWS - 1 - Game.MARGIN)
                    {
                        return _direction = -1;
                    }
                }
                return _direction = 1;
            }
            else
            {
                foreach (Enemy e in Enemies)
                {
                    if (e.TopLeftCorner.X == Game.MARGIN)
                    {
                        return _direction = 1;
                    }
                }
                return _direction = -1;
            }
        }

        /// <summary>
        /// Supprime tous les Enemis qui ont la valeur GonnaDelete à true
        /// </summary>
        public void DeleteEnemy()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i].GonnaDelete)
                {
                    Enemies.Remove(Enemies[i]);
                }
            }
        }

        /// <summary>
        /// Si un des ennemis arrive à la hauteur du joueur, c'est la fin du jeu. Ils ont envahi la terre :'(.
        /// Set gameRunning à false et stop la boucle du jeu
        /// </summary>
        public void Invasion()
        {
            foreach (Enemy e in Enemies)
            {
                if (e.BottomRightCorner.Y >= Game.HEIGHT_OF_WINDOWS - Sprites.playerDesign.Length - 1)
                {
                    GoDown();
                    Game.gameRunning = false;
                    return;
                }
            }
        }
    }
}
