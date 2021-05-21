using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace MP3Player.Logic.Ui
{
    public class PlaylistHandler : IPlaylistHandler
    {
        public TrulyObservableCollection<SingleTrack> playlist = new TrulyObservableCollection<SingleTrack>();
        private int songIndex = 0;

        public TrulyObservableCollection<SingleTrack> returnWholePlaylist()
        {
            return playlist;
        }

        public void addSongToPlaylist(SingleTrack track)
        {
            playlist.Add(track);
        }

        public SingleTrack returnFirstTrack()
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

        public int getNumberOfTracks()
        {
            return playlist.Count;
        }

        public SingleTrack GetNextTrack()
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

        public SingleTrack GetLastTrack()
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