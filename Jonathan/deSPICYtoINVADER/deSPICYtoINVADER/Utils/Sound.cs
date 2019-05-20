///ETML
///Auteur : Jonathan Friedli et Filipe Andrade Barros
///Date : 20.05.19
///Description : Classe Sound, permet de jouer un son
using System;
using System.Collections.Generic;
using System.Media;
using System.Windows.Media;

namespace deSPICYtoINVADER.Utils
{
    /// <summary>
    /// Permet de joueur un son de 2 manières différentes.
    /// On utilise SoundPlayer car il permet de jouer une musique en boucle mais ne permet pas de faire plusieur sons simultanés. Utile pour les musiques de fond. 
    /// On utilise MediaPlayer car il permet de jouer plusieurs sons de manières simultanées mais pas possible d'en jouer en boucle. Utile pour les bruitages
    /// L'utilisation des 2 joueur de sons permet d'avoir des musics de fond et des bruitages de manières simultanées
    /// </summary>
    public static class Sound
    {
        /// <summary>
        /// Liste de MediaPlayer contenant 3 Mediaplayer car il n'a jamais plus de 3 bruitages simultanément
        /// </summary>
        private static List<MediaPlayer> _sounds = new List<MediaPlayer>() { new MediaPlayer(), new MediaPlayer(), new MediaPlayer()};//3 soundplayer car max 3 sons simultané
        /// <summary>
        /// Liste de SoundPlayer. Chaque soundplayer contient un "son" qu'on va joueur en boucle. 2 Music de jeu et une musique "bruitage" de clavier pour la fin du jeu.
        /// </summary>
        private static List<SoundPlayer> _backgroudMusic = new List<SoundPlayer>() {new SoundPlayer("Utils\\Sounds\\DarudePiano.wav"), new SoundPlayer("Utils\\Sounds\\DarudeHard.wav"), new SoundPlayer("Utils\\Sounds\\Keyboard.wav")};

        /// <summary>
        /// Permet de jouer une music de fond. Il y a 3 choix et l'option stop. Si on joue une music de fond et qu'on lance une 2eme ça coupe la première. Cependant pour couper
        /// la dernière musique de fond, il faut une option stop.
        /// Si le son est set à OFF dans le menu, cela ne joue pas
        /// </summary>
        /// <param name="name">Nom de la musique (3 choix)</param>
        public static void BackMusic(string name)
        {
            if (Menu.Sound)//Menu : son off
            {
                switch (name)
                {
                    case "Piano":
                        _backgroudMusic[0].PlayLooping();
                        break;
                    case "hard":
                        _backgroudMusic[1].PlayLooping();
                        break;
                    case "Keyboard":
                        _backgroudMusic[2].PlayLooping();
                        break;
                    case "stop":
                        for (int i = 0; i < _backgroudMusic.Count; i++)
                        {
                            _backgroudMusic[i].Stop();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Stop tous les SoundPlayer de la liste car on ne sait pas lequel est entrain de jouer.
        /// </summary>
        public static void StopMusic()
        {
            foreach (SoundPlayer s in _backgroudMusic)
            {
                s.Stop();
            }
        }

        /// <summary>
        /// Permet de jouer un "bruitage". Si le son est set à OFF dans le menu, ne joue pas le son
        /// </summary>
        /// <param name="name">On gère les choix avec un switch</param>
        public static void PlaySound(string name)
        {
            if (Menu.Sound)//Menu : son off
            {
                switch (name)
                {
                    case "shoot":
                        _sounds[0].Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Utils\\Sounds\\Bow.wav"));
                        _sounds[0].Play();
                        break;
                    case "hurt":
                        _sounds[1].Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Utils\\Sounds\\Hurt.wav"));
                        _sounds[1].Play();
                        break;
                    case "enemy":
                        _sounds[2].Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Utils\\Sounds\\EnemyHit.wav"));
                        _sounds[2].Play();
                        break;
                }
            }
        }
    }
}
