using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using System.Threading;

namespace Spicy_Nvader
{
    class Program
    {
        public const int WIDTH_OF_WIDOWS = 150;
        public const int HEIGHT_OF_WINDOWS = 80;
        public const int MARGIN = 4;//Marge de chaque de côté
        public static bool game = true;
        public static char[][] allChars = new char[HEIGHT_OF_WINDOWS-1][];//tableau de tous les caractères
        private static string everyPixel;//String qui va tout afficher
        public static int tics = 0;
        public static Bullet[] allBullets;
        public static SoundPlayer sound = new SoundPlayer("Sounds\\Darude - Sandstorm.wav");
        public static SoundPlayer keyboard = new SoundPlayer("Sounds\\KeyTouchNoise.Wav");

        public static bool isSoundPlaying = false;
        //public static Enemy[,] enemySwarm;
        public static Random rnd = new Random();
        static void Main (string[] args)
        {
            Console.WindowHeight = HEIGHT_OF_WINDOWS;
            Console.WindowWidth = WIDTH_OF_WIDOWS;
            Console.BufferHeight = HEIGHT_OF_WINDOWS;
            Console.BufferWidth = WIDTH_OF_WIDOWS;
            Console.CursorVisible = false;
            allBullets = new Bullet[10 + 1];//+ 1 car le joueur doit pouvoir tirer sa bullet
            //CreateEnemySwarm(5, 5);
            Player p1 = new Player();
            Swarm s1 = new Swarm();
            s1.Create(5, 5);
            Stopwatch s = new Stopwatch();


            while (game)//boucle de jeu
            {
                s.Restart();//timer


                if (tics == int.MaxValue)//tics (si les tics sont au max, on les remets à 0)
                    tics = 0;

                ResetArray();//Remet le tableau à vide

                GameUpdate(p1, s1);//Update TOUT !

                FromArrayToString(p1);//Crée et écrit le string qui contient tout

                
                tics++;//InCrémente les tics
                int ts = (int)s.ElapsedMilliseconds;//"Stabiliser" la vitesse, indépendemment des ordis
                if (ts > 10) ts = 10;
                Debug.WriteLine(ts);
                Thread.Sleep(10 - ts);
            }
            sound.Stop();
            ShowScore(p1);
            Console.Read();
        }

        public static void ResetArray()
        {
            for (int i = 0; i < allChars.Length; i++)//Boucle pour reset le tableau de char
            {
                allChars[i] = "".PadLeft(WIDTH_OF_WIDOWS - 1).ToCharArray();//La ligne entière du tableau sera rempli d'espace. On met le -1 car le 80eme char est à la pos 79
            }
        }

        public static void ShowScore(Player p1)
        {
            Console.Clear();
            Console.ResetColor();
            string message = "Le tigre ne perd pas ! Retourne jouer à Minecraft NABOT ! \n\nTon score est de : ";
            keyboard.PlayLooping();
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(50);
            }
            Console.WriteLine(p1.PlayerScore);
            keyboard.Stop();
        }

        public static void PlayMusic()
        {
            if (!isSoundPlaying)
            {
                sound.PlayLooping();
                isSoundPlaying = true;
            }
        }

        public static void FromArrayToString(Player p1)
        {
            if (p1.Music)
            {
                Console.ForegroundColor = (ConsoleColor)rnd.Next(9, 16);
                PlayMusic();
            }
            /*Console.SetCursorPosition(0, 0);
            Console.Write(everyPixel);//tableau vide*/
            Console.SetCursorPosition(0, 0);
            everyPixel = "";
            for (int i = 0; i < allChars.Length; i++)
            {
                everyPixel += new string(allChars[i]) + " ";
            }
            Console.Write(everyPixel);
        }


        public static void GameUpdate(Player p1, Swarm s1)
        {
            Collision(p1, s1);
            s1.UpdateSwarm();
            p1.PlayerUpdate();
        }

        public static void Collision(Player p1, Swarm s1)
        {
            for(int k = 0; k < allBullets.Length; k++)//On regarde d'abord toutes les bullets
            {
                if (allBullets[k] != null)//Si elles existent (ne sont pas null)
                {
                    allBullets[k].UpdateBullet();//On les update
                    if (allBullets[k].Direction == 1)//bullet qui montent
                    {
                        foreach (Enemy e in s1.Enemies)
                        {
                            if (e.EnemyGetShot(allBullets[k]))
                            {
                                p1.AddOnScore();
                            }
                        }
                    }
                    if (allBullets[k].Direction == -1)//bullet qui descendent
                    {
                        p1.GetShot(allBullets[k]);//On regarde si une bullet touche le joueur
                    }
                    if (allBullets[k].GonnaDelete)//On regarde si la bullet doit être supprimée (propriété Gonnadelete vaut true)
                    {
                        allBullets[k] = null;
                    }
                }
            }
        }
    }
}
