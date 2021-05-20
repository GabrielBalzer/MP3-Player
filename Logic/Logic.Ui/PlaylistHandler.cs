using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace MP3Player.Logic.Ui
{
    public static class PlaylistHandler
    {
        public static TrulyObservableCollection<SingleTrack> playlist = new TrulyObservableCollection<SingleTrack>();
        private static int songIndex = 0;

        public static TrulyObservableCollection<SingleTrack> returnWholePlaylist()
        {
            return playlist;
        }

        public static void addSongToPlaylist(SingleTrack track)
        {
            playlist.Add(track);
        }

        public static SingleTrack returnFirstTrack()
        {
            if (playlist.Count != 0)
            {
                playlist[0].IsPlaying = true;
                Console.WriteLine("isplaying: " + playlist[0].IsPlaying);
                return playlist[0];
            }
            else
            {
                return null;
            }
        }

        public static int getNumberOfTracks()
        {
            return playlist.Count;
        }

        public static SingleTrack GetNextTrack()
        {
            if (songIndex < playlist.Count - 1)
            {
                playlist[songIndex].IsPlaying = false;
                songIndex++;
                playlist[songIndex].IsPlaying = true;
                return playlist[songIndex];
            }
            else
            {
                songIndex = 0;
                playlist[playlist.Count - 1].IsPlaying = false;
                return returnFirstTrack();
            }
        }

        public static SingleTrack GetLastTrack()
        {
            if (songIndex == 0)
            {
                if (songIndex < playlist.Count - 1)
                {
                    playlist[0].IsPlaying = false;
                }

                songIndex = playlist.Count - 1;
                playlist[songIndex].IsPlaying = true;
                return playlist[playlist.Count - 1];

            }
            else
            {
                playlist[songIndex].IsPlaying = false;
                songIndex--;
                playlist[songIndex].IsPlaying = true;
                return playlist[songIndex];
            }
        }
    }
}