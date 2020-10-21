using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MP3Player.Logic.Ui
{
    public static class SongPlayer
    {
        private static readonly MediaPlayer Player = new MediaPlayer();

        public static void PickFile(string fileuri)
        {
            Player.Open(new Uri(fileuri));
        }

        public static void SetVolume(double volume)
        {
            Player.Volume = volume / 100;
            Console.WriteLine($"Player value is {Player.Volume}");
        }

        public static void PlaySong()
        {
            Player.Play();
        }

        public static void PauseSong()
        {
            Player.Pause();
        }
    }
}
