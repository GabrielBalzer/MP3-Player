using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace MP3Player.Logic.Ui
{
    public static class FileManager
    {
        private static readonly OpenFileDialog fileDialog = new OpenFileDialog();
        public static void OpenFile()
        {
            fileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (fileDialog.ShowDialog() == true)
            {
                SongPlayer.PickFile(fileDialog.FileName);
            }
        }
        
    }
}