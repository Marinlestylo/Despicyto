using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spicy_Nvader
{
    public class Enemy
    {
        //Pour la hitbox de l'ennemy
        public bool GonnaDelete { get; private set; }
        public int MinX { get; private set; }//tout à gauche
        public int MaxX { get; private set; }//tout à droite
        public int MaxY { get; private set; }//tout en bas
        public int CurrentTopPos { get; private set; }//Tout en haut de l'ennemi (MinY)
        private int _currentLeftPos;
        //private int _direction;

        private static readonly string[] ENEMY = new string[8]
        /*{
            "  ▄     ▄  ",
            "   █   █   ",
            "  ███████  ",
            " ██ ███ ██ ",
            "███████████",
            "█ ███████ █",
            "█ █     █ █",
            "   ██ ██   " }; //Design du monstre*/
            {
            "▄     ▄",
            "█   █",
            "███████",
            "██ ███ ██",
            "███████████",
            "█ ███████ █",
            "█ █     █ █",
            "██ ██"
            }; //Design du monstre

        public Enemy(int topPos, int leftPos)
        {
            CurrentTopPos = topPos;
            _currentLeftPos = leftPos;
            GonnaDelete = false;
        }

        public void DrawEnemy()
        {
            for (int i = 0; i < ENEMY.Length; i++)//Boucle pour dessiner l'ennemi
            {
                for (int j = 0; j < ENEMY[i].Length; j++)
                {
                    Program.allChars[CurrentTopPos + i][_currentLeftPos - ENEMY[i].Length / 2 + j] = ENEMY[i][j];
                }
            }
        }

        public void MoveEnemy(int direction)
        {
            _currentLeftPos += direction;
            MinX = _currentLeftPos - ENEMY[4].Length / 2;
            MaxX = _currentLeftPos + ENEMY[4].Length / 2;
            MaxY = CurrentTopPos + ENEMY.Length - 1;//Sinon c'est trop bas
        }

        public void GoDown(int val)
        {
            CurrentTopPos += val;
        }

        /// <summary>
        /// Pour debug
        /// </summary>
        public void GetHitBox()
        {
            Program.allChars[CurrentTopPos][MinX] = '╔';
            Program.allChars[CurrentTopPos][MaxX] = '╗';
            Program.allChars[MaxY][MinX] = '╚';
            Program.allChars[MaxY][MaxX] = '╝';
        }

        public bool EnemyGetShot(Bullet bull)
        {
            if (bull.PosX >= MinX && bull.PosX <= MaxX && bull.PosY >= CurrentTopPos && bull.PosY <= MaxY)
            {

                GonnaDelete = true;
                bull.GonnaDelete = true;
                return true;
            }
            return false;
        }

        public void EnemyShoots()
        {
            for (int i = 0; i < Program.allBullets.Length - 1; i++)//La dernière pos est pour le joueur
            {
                if (Program.allBullets[i] == null)//Si le tableau a une place vide on crée la bullet dans cette case sinon on ne crée pas de bullet
                {
                    Program.allBullets[i] = new Bullet(_currentLeftPos, CurrentTopPos + ENEMY.Length + 1, Program.HEIGHT_OF_WINDOWS - 3, -1);
                    return;
                }
            }
        }

        public void EnemyUpdate()
        {
            if (Program.rnd.Next(1, 1000) > 990 && Program.tics % 5 == 0)
            {
                EnemyShoots();
            }
            DrawEnemy();
            //GetHitBox();//DEBUG
        }
    }
}
