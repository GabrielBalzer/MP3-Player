using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Win32;


namespace MP3Player.Logic.Ui
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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
            Paused = true;
            PlayButtonText = "Play";
            CurrentSongTime = "00:00";
            AbsoluteSongTime = "00:00";
        }



        public string WindowTitle { get; private set; }

        public ICommand ButtonCommand { get; set; }

        public ICommand PlayButton { get; set; }


        private double _sliderValue { get; set; }

        public double SliderValue
        {
            get
            {
                return _sliderValue;
            }
            set
            {
                _sliderValue = value;
                OnPropertyChanged();
                Printer();

            }
        }

        private void Printer()
        {
            Console.WriteLine($"New Slider Value is: {SliderValue}");
            SongPlayer.SetVolume(SliderValue);
        }

        public string PlayButtonText { get; set; }

        public string FirstName { get; set; }

        public string CurrentSongTime { get; set; }
        public string AbsoluteSongTime { get; set; }

        public int Progress { get; set; }

        public bool Paused { get; set; }




        private void MainButtonClick(object sender)
        {
            FileManager.OpenFile();
        }

        private void PlayButtonClick(object sender)
        {
            if (Paused)
            {
                SongPlayer.PlaySong();
                PlayButtonText = "Pause";
            }
            else
            {
                PlayButtonText = "Play";
                SongPlayer.PauseSong();
            }
            Paused = !Paused;
        }

        public string this[string propertyName]
        {
            get
            {
                Trace.TraceInformation(propertyName);
                //Console.WriteLine($"New Slider Value is: {Slider}");
                return string.Empty;
            }
        }

        public string Error => string.Empty;
    }
}