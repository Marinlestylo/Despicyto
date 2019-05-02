using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy_Nvader
{
    public class Swarm
    {
        public List<Enemy> Enemies { get; }
        private int _direction;
        private int _lastDirection;
        public Swarm()
        {
            _direction = 1;
            _lastDirection = _direction;
            Enemies = new List<Enemy>();
        }

        public void Create(int row, int col)
        {
            _direction = 1;
            _lastDirection = 1;
            if(row < 8 && col < 8)//Obligation d'avoir moins de 8 row et col
            {
                for (int i = 0; i < col; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        Enemies.Add(new Enemy(2 + i * 9, 8 + j * 13));
                    }
                }
            }
        }

        public void UpdateSwarm()
        {
            if (Enemies.Count == 0)
            {
                Create(5, 5);
            }
            foreach (Enemy e in Enemies)
            {
                e.EnemyUpdate();
            }
            if (Program.tics % 10 == 0)
            {
                move(DirectionChange());
            }
            DeleteEnemy();
            Invasion();
        }

        public void DeleteEnemy()
        {
            for(int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i].GonnaDelete)
                {
                    Enemies.Remove(Enemies[i]);
                }
            }
        }

        public void move(int dir)
        {
            _direction = dir;
            if (_lastDirection != _direction)
            {
                foreach (Enemy e in Enemies)
                {
                    e.GoDown(5);
                }
            }
            foreach (Enemy e in Enemies)
            {
                e.MoveEnemy(_direction);
            }
            _lastDirection = _direction;
        }

        public void Invasion()
        {
            foreach (Enemy e in Enemies)
            {
                if (e.MaxY >= Program.HEIGHT_OF_WINDOWS - (e.MaxY - e.CurrentTopPos))
                {
                    Program.game = false;
                }
            }
        }

        /// <summary>
        /// Méthode qui check si un ennemy arrive au bord de l'écran afin que l'essaim change de direction
        /// </summary>
        /// <returns>La direction voulue (elle change si un ennemi arrive trop près du bord)</returns>
        public int DirectionChange()
        {
            if (_direction == 1)
            {
                foreach (Enemy e in Enemies)
                {
                    if (e.MaxX == Program.WIDTH_OF_WIDOWS - 1 - Program.MARGIN)
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
                    if (e.MinX == Program.MARGIN)
                    {
                        return _direction = 1;
                    }
                }
                return _direction = -1;
            }
        }
    }
}
