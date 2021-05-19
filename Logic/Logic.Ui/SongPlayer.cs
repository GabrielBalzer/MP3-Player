using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using NAudio.Wave;

namespace MP3Player.Logic.Ui
{
    public static class SongPlayer
    {
        private static WaveOutEvent waveOut = new WaveOutEvent();
        private static AudioFileReader audioFileReader;
       

        public static void PickFile(string fileuri)
        {
            audioFileReader = new AudioFileReader(fileuri);
            waveOut.Init(audioFileReader);
            Console.WriteLine(audioFileReader.TotalTime);
        }

        public static void SetVolume(double volume)
        {
            waveOut.Volume = (float) (volume / 100);
            Console.WriteLine($"Player value is {waveOut.Volume}");
        }

        public static void PlaySong()
        {
            waveOut.Play();
        }

        public static void PauseSong()
        {
            waveOut.Pause();
        }

        public static void StopSong()
        {
            waveOut.Stop();
        }

        public static double GetTrackLengthInSeconds()
        {
            if (audioFileReader != null)
            {
                return audioFileReader.TotalTime.TotalSeconds;

            }
            else
            {
                return 0;
            }
        }

        public static double GetCurrentTrackTimeInSeconds()
        {
            return audioFileReader?.CurrentTime.TotalSeconds ?? 0;
        }
    }
}
