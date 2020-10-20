using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
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
                Progress = 30;
            }
            else
            {
                DispatcherHelper.Initialize();
                Progress = 0;
                WindowTitle = "MP3Player";
                Task.Run(
                    () =>
                    {
                        Task.Delay(2000).ContinueWith(
                            t => {
                        while (Progress < 100)
                        {
                            DispatcherHelper.RunAsync((() => Progress += 5));
                            Task.Delay(1000).Wait();
                        }
                    });
            });
        }

            ButtonCommand = new RelayCommand(o => MainButtonClick("MainButton"));
            PlayButton = new RelayCommand(o => PlayButtonClick("PlayButton"));
            PauseButton = new RelayCommand(o => PauseButtonClick("PauseButton"));
            StopButton = new RelayCommand(o => StopButtonClick("StopButton"));
            Paused = true;
            PlayButtonText = "Play";
        }



        public string WindowTitle { get; private set; }

        public ICommand ButtonCommand { get; set; }

        public ICommand PlayButton { get; set; }

        public ICommand PauseButton { get; set; }

        public ICommand StopButton { get; set; }

        public string PlayButtonText { get; set; }

        public int Progress { get; set; }

        public bool Paused { get; set; }

        public MediaPlayer mediaPlayer = new MediaPlayer();


        private void MainButtonClick(object sender)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mediaPlayer.Open(new Uri(openFileDialog.FileName));
                mediaPlayer.Volume = 0.1;

            }
        }

        private void PlayButtonClick(object sender)
        {
            if (Paused)
            {
                mediaPlayer.Play();
                PlayButtonText = "Paused";
            }
            else
            {
                PlayButtonText = "Play";
                mediaPlayer.Pause();
            }
            Paused = !Paused;
        }

        private void PauseButtonClick(object sender)
        {
            mediaPlayer.Pause();
        }

        private void StopButtonClick(object sender)
        {
            mediaPlayer.Stop();
        }



    }
}