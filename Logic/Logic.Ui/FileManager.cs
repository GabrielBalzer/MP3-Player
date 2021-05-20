using System;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace MP3Player.Logic.Ui
{
    public static class FileManager
    {
        private static readonly OpenFileDialog fileDialog = new OpenFileDialog();
        public static void AddFileToPlaylist()
        {
            fileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (fileDialog.ShowDialog() == true)
            {
                //SongPlayer.PickFile(fileDialog.FileName);
                var friendlyName = fileDialog.SafeFileName.Remove(fileDialog.SafeFileName.Length - 4);
                var numberOfTracks = PlaylistHandler.getNumberOfTracks();
                PlaylistHandler.addSongToPlaylist(new SingleTrack(fileDialog.FileName, friendlyName, numberOfTracks.ToString(), false));
                Console.WriteLine(numberOfTracks);
            }
        }
        
    }
}