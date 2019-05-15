﻿using deSPICYtoINVADER.Characters;
using deSPICYtoINVADER.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace deSPICYtoINVADER
{
    public class Game
    {
        /* Constantes */
        public const int WIDTH_OF_WIDOWS = 150;
        public const int HEIGHT_OF_WINDOWS = 80;
        public const int MARGIN = 4;//Marge de chaque de côté
        private const string END_MESSAGE = ", suite à votre malencontreuse défaite contre ces aliens, ils ont envahi la terre et asservi les humains." +
                                           "\nVous en êtes l'unique responsable. Cependant la terre étant peuplée d'humains débiles pour la plupart," +
                                           " ce n'est peut-être pas une si mauvaise chose.\nVotre score est de : ";

        /* Static */
        public static int tics = 0;
        public static bool gameRunning;
        private static string everyPixel;//String qui va tout afficher
        public static char[][] allChars = new char[HEIGHT_OF_WINDOWS - 1][];
        public static List<Bullet> allBullets = new List<Bullet>();

        /* Attributs */
        //private Enemy _enemy = new Enemy(new Point(15, 15), Sprites.SmallEnemy);
        private Swarm _swarm;
        private Player _user;
        private Stopwatch _stopTime;
        private string _username;
        private JsonHighScore _score;

        /// <summary>
        /// Constructeur de la classe Game
        /// Set les propriétés de la fenêtre (taille, largeur, curseur invi)
        /// set gameRunning a true(pour que la boucle de la game tourne)
        /// </summary>
        public Game()
        {
            gameRunning = true;
            _swarm = new Swarm(5, 7);
            _user = new Player();
            _stopTime = new Stopwatch();
            _score = new JsonHighScore("Resources\\HighScore.json");
        }

        /// <summary>
        /// Fais les réglages pour la taille de la fenêtre ainsi que pour le curseur
        /// </summary>
        private void SetWindow()
        {
            Console.CursorVisible = false;
            Console.SetBufferSize(WIDTH_OF_WIDOWS, HEIGHT_OF_WINDOWS);
            Console.SetWindowSize(WIDTH_OF_WIDOWS,HEIGHT_OF_WINDOWS);
        }

        private void EnterName()
        {
            Console.Clear();
            bool temp = true;
            do
            {
                Console.SetCursorPosition(25, 13);
                Console.Write("Entrez votre pseudo : ");
                _username = Console.ReadLine();
                if (_username.Length < 16 && _username.Length > 3)
                {
                    temp = false;
                }
                Console.Clear();
            } while (temp);
        }

        public void GameLoop()
        {
            EnterName();
            SetWindow();
            Sound.BackMusic("Piano");
            while (!_user.GonnaDelete && gameRunning)
            {
                /* Début de boucle */
                _stopTime.Restart();
                if (tics == int.MaxValue)//tics (si les tics sont au max, on les remets à 0)
                    tics = 0;
                /* Début de boucle */

                ResetArray();//Reset le tableau de char, le remet vide

                GameUpdate();//Update tout (Bullet, Enemy, le swarm et player). Durant l'update, plein de chose vont se noter dans le tableau allChars

                FromArrayToString();//Transforme le tableau en un string, va en 0,0  et l'écrit.



                /* Fin de boucle */
                tics++;
                int ts = (int)_stopTime.ElapsedMilliseconds;//"Stabiliser" la vitesse, indépendemment des ordis
                if (ts > 10)
                    ts = 10;
                Thread.Sleep(10 - ts);
                /* Fin de boucle */
            }
            GameOver();
        }

        private void ShowGameOver()
        {
            _score.AddScoreInJson(new Score(_username, Player.Score));
            Sound.StopMusic();
            Console.ForegroundColor = ConsoleColor.Red;
            Sprites.DrawTitle(Sprites.gameOver, new Point(30, 30));
            Console.ResetColor();
        }

        /// <summary>
        /// Quand on perd, le jeu freeze sur un écran avec Game Over écrit en rouge.
        /// Ensuite un message s'affiche contenant le score du joueur
        /// </summary>
        private void GameOver()
        {
            ShowGameOver();
            Thread.Sleep(1500);

            Console.Clear();
            Sound.BackMusic("Keyboard");
            Console.Write(_username);
            for (int i = 0; i < END_MESSAGE.Length; i++)
            {
                Console.Write(END_MESSAGE[i]);
                System.Threading.Thread.Sleep(50);
            }
            Console.WriteLine(Player.Score);
            Sound.BackMusic("stop");
            Thread.Sleep(2000);
            Console.Clear();
            ResetGame();
            while(Console.KeyAvailable)//Eviter de spam espace/enter pendant les animations et relancer le jeu par erreur
            {
                Console.ReadKey(true);
            }
        }

        public void ResetGame()
        {
            Player.Score = 0;
            allBullets = new List<Bullet>();
            _user.Reset();
            gameRunning = true;
        }

        /// <summary>
        /// Update les Enemis, les Bullets, le player, le swarm
        /// </summary>
        private void GameUpdate()
        {
            _user.Update();
            _swarm.Update();
            Collision();
            BulletUpdate();
        }

        private void Collision()
        {
            foreach (Bullet b in allBullets)
            {
                if (b.Direction == 1 )//Bullet qui descendent (collision avec le player)
                {
                    _user.GetShot(b);
                }
                else//Bullet qui montent
                {
                    foreach (Enemy e in _swarm.Enemies)
                    {
                        e.GetShot(b);
                    }
                }
            }
        }

        #region BulletUpdate
        private void BulletUpdate()
        {
            RemoveBullet();//On remove les bullets avant de les update pour pas update des bullets "mortes"
            foreach (Bullet bull in allBullets)
            {
                bull.Update();
            }
        }

        private void RemoveBullet()
        {
            for (int i = 0; i < allBullets.Count; i++)
            {
                if (allBullets[i].GonnaDelete)
                {
                    allBullets.RemoveAt(i);
                    i--;
                }
            }
        }
        #endregion

        #region Array
        /// <summary>
        /// Met tous les chars du tableau allChars en "espace"
        /// </summary>
        private void ResetArray()
        {
            for (int i = 0; i < allChars.Length; i++)//Boucle pour reset le tableau de char
            {
                allChars[i] = "".PadLeft(WIDTH_OF_WIDOWS - 1).ToCharArray();//La ligne entière du tableau sera rempli d'espace. On met le -1 car le 150eme char est à la pos 149
            }
        }

        /// <summary>
        /// Va a la position 0;0
        /// Transforme le tableau en une seule string
        /// Ecris la string
        /// </summary>
        private void FromArrayToString()
        {
            if (_user.Life < (Player.MAX_LIVES / 3) + 1)
            {
                Console.ForegroundColor = (ConsoleColor)Utils.Random.RandomValue(9, 16);
            }
            Console.SetCursorPosition(0, 0);
            everyPixel = "";
            for (int i = 0; i < allChars.Length; i++)
            {
                everyPixel += new string(allChars[i]) + " ";
            }
            Console.Write(everyPixel);
        }
        #endregion
    }
}
