using System;
using System.Collections.Generic;
using System.Media;
using System.Windows.Media;

namespace deSPICYtoINVADER.utils
{
    public static class Sound
    {
        private static List<MediaPlayer> _sounds = new List<MediaPlayer>() { new MediaPlayer(), new MediaPlayer(), new MediaPlayer()};//3 soundplayer car max 3 sons simultané
        private static List<SoundPlayer> _backgroudMusic = new List<SoundPlayer>() {new SoundPlayer("Utils\\Sounds\\DarudePiano.wav"), new SoundPlayer("Utils\\Sounds\\DarudeHard.wav"), new SoundPlayer("Utils\\Sounds\\Keyboard.wav")};

        public static void BackMusic(string name)
        {
            if (true == true)//Menu : son off
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
                }
            }
        }

        public static void StopMusic()
        {
            foreach (SoundPlayer s in _backgroudMusic)
            {
                s.Stop();
            }
        }

        public static void PlaySound(string name)
        {
            if (true == true)//Menu : son off
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
