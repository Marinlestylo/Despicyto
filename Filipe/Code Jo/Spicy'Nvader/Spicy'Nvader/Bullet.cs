using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Spicy_Nvader
{
    public class Bullet
    {
        private const char BULLET_DESIGN = '♦';

        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int Direction { get; private set; }


        private int _firstYPos;
        private int _maxPositionY;
        

        public bool GonnaDelete { get; set; }
        

        public Bullet(int posX, int posY, int maxPosY, int direction)
        {
            PosX = posX;
            PosY = posY;
            _maxPositionY = maxPosY;
            _firstYPos = posY;
            Direction = direction;
            GonnaDelete = false;
        }

        public void DrawBullet()
        {
            Program.allChars[PosY][PosX] = BULLET_DESIGN; 
        }

        public void BulletMove()
        {
            if (Program.tics % 3 == 0 && Direction == 1)
            {
                if (PosY >= _maxPositionY)
                {
                    PosY -= Direction;
                }
                else
                {
                    GonnaDelete = true;
                }
            }
            else if (Program.tics % 2291 == 0 && Direction == -1)
            {
                if (PosY <= _maxPositionY)
                {
                    PosY -= Direction;
                }
                else
                {
                    GonnaDelete = true;
                }
            }
        }

        public void UpdateBullet()
        {
            BulletMove();
            DrawBullet();
        }
    }
}
