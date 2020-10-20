using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using Microsoft.Win32;

namespace MP3Player.Logic.Ui
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                WindowTitle = "Design";
            }
            else
            {
                WindowTitle = "MP3Player";
            }

            ButtonCommand = new RelayCommand(o => MainButtonClick("MainButton"));
        }

        public string WindowTitle { get; private set; }

        public ICommand ButtonCommand { get; set; }

        public MediaPlayer mediaPlayer = new MediaPlayer();


        private void MainButtonClick(object sender)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mediaPlayer.Open(new Uri(openFileDialog.FileName));
                mediaPlayer.Play();
            }
        }

        

    }
}