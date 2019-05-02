using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Spicy_Nvader
{
    class Program
    {
        public const int WIDTH_OF_WIDOWS = 140;
        public const int HEIGHT_OF_WINDOWS = 70;
        public const int MARGIN = 1;//Marge de chaque de côté     
        public static char[][] allChars = new char[HEIGHT_OF_WINDOWS-1][];//tableau de tous les caractères
        private static string everyPixel;//String qui va tout afficher
        public static int tics = 0;
        public static Bullet[] allBullets;
        public static Enemy[,] enemySwarm;
        public static Random rnd = new Random();
        public static List<Enemy> swarm = new List<Enemy>();

        static void Main (string[] args)
        {
            Console.CursorVisible = false;

            Menu mainMenu = new Menu();
            mainMenu.LaunchMenu();

            Console.WindowHeight = HEIGHT_OF_WINDOWS;
            Console.WindowWidth = WIDTH_OF_WIDOWS;
            Console.BufferHeight = HEIGHT_OF_WINDOWS;
            Console.BufferWidth = WIDTH_OF_WIDOWS;
            allBullets = new Bullet[5 + 1];//+ 1 car le joueur doit pouvoir tirer sa bullet
            CreateEnemySwarm(5, 5);
            Player p1 = new Player();
            Stopwatch s = new Stopwatch();


            while (true)//boucle de jeu
            {
                s.Restart();//timer


                if (tics == int.MaxValue)//tics (si les tics sont au max, on les remets à 0)
                    tics = 0;

                ResetArray();//Remet le tableau à vide

                GameUpdate(p1);//Update TOUT !

                FromArrayToString();//Crée et écrit le string qui contient tout

                
                tics++;//InCrémente les tics
                int ts = (int)s.ElapsedMilliseconds;//"Stabiliser" la vitesse, indépendemment des ordis
                Thread.Sleep(10);
            }
        }

        public static void ResetArray()
        {
            for (int i = 0; i < allChars.Length; i++)//Boucle pour reset le tableau de char
            {
                allChars[i] = "".PadLeft(WIDTH_OF_WIDOWS - 1).ToCharArray();//La ligne entière du tableau sera rempli d'espace. On met le -1 car le 80eme char est à la pos 79
            }
        }

        public static void FromArrayToString()
        {
            Console.SetCursorPosition(0, 0);
            everyPixel = "";
            for (int i = 0; i < allChars.Length; i++)
            {
                everyPixel += new string(allChars[i]) + " ";
            }
            Console.Write(everyPixel);
        }

        public static void CreateEnemySwarm(int width, int height)
        {
            enemySwarm = new Enemy[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    enemySwarm[i, j] = new Enemy(i * 8, 5 + j * 12);
                    //swarm.Add();
                }
            }
        }

        public static void UpdateEnnemy()
        {
            for (int i = 0; i < enemySwarm.GetLength(0); i++)
            {
                for (int j = 0; j < enemySwarm.GetLength(1); j++)
                {
                    if (enemySwarm[i, j] != null && enemySwarm[i, j].GonnaDelete)
                    {
                        enemySwarm[i, j] = null;
                    }
                    else if (enemySwarm[i, j] != null)
                    {
                        enemySwarm[i, j].EnemyUpdate();
                    }

                }
            }
        }

        public static void GameUpdate(Player p1)
        {
            Collision();
            UpdateEnnemy();
            p1.PlayerUpdate();
        }

        public static void Collision()
        {
            for(int k = 0; k < allBullets.Length; k++)
            {
                if (allBullets[k] != null)
                {
                    allBullets[k].UpdateBullet();
                    if (allBullets[k].Direction == 1)
                    {
                        for (int i = 0; i < enemySwarm.GetLength(0); i++)
                        {
                            for (int j = 0; j < enemySwarm.GetLength(1); j++)
                            {
                                if (enemySwarm[i, j] != null)
                                {
                                    enemySwarm[i, j].EnemyGetShot(allBullets[k]);
                                }
                            }
                        }
                    }
                    if (allBullets[k].GonnaDelete)
                    {
                        allBullets[k] = null;
                    }
                }
            }
        }
    }
}
