using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace MP3Player.Logic.Ui
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static PlaylistHandler playlistHandler = new PlaylistHandler();
        public SongPlayer songPlayer = new SongPlayer(playlistHandler);
        public FileManager fileManager = new FileManager();
        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Application.Current.MainWindow.Closing += MainWindow_Closing;
            Communicator.ProgressSliderIsDragging = false;
            Communicator.ProgressSliderValueChanged = false;
            Playlist = playlistHandler.playlist;
            
            

            if (IsInDesignMode)
            {
                WindowTitle = "Design";
                Progress = 30;
            }
            else
            {
                Progress = 0;
                WindowTitle = "MP3Player";
            }


            DispatcherHelper.Initialize();

            Task.Run(
                () =>
                {
                    Task.Delay(100).ContinueWith(
                        t => {
                            while (Progress < 100)
                            {

                                if (Communicator.ProgressSliderValueChanged)
                                {
                                    songPlayer.SetPosition((int)ProgressSliderValue);
                                    Communicator.ProgressSliderValueChanged = false;
                                    //Console.WriteLine("Loopcomm");
                                }

                                if (!Communicator.ProgressSliderIsDragging)
                                {
                                    DispatcherHelper.RunAsync((() => ProgressSliderValue = songPlayer.GetCurrentTrackProgress()));
                                }

                                DispatcherHelper.RunAsync((() => CurrentSongTime = TimeConverter.GetCurrentTrackTimeAsString(songPlayer)));
                                DispatcherHelper.RunAsync((() => AbsoluteSongTime = TimeConverter.GetAbsoluteTrackTimeAsString(songPlayer)));
                                Task.Delay(100).Wait();
                            }
                        });
                });

            ButtonCommand = new RelayCommand(o => OpenFileButtonClick("MainButton"));
            PlayButton = new RelayCommand(o => PlayButtonClick("PlayButton"));
            ForwardButton = new RelayCommand(o => ForwardButtonClick("ForwardButton"));
            BackwardButton = new RelayCommand(o => BackwardButtonClick("BackwardButton"));
            Paused = true;
            PlayButtonText = "Play";
            CurrentSongTime = "00 : 00";
            AbsoluteSongTime = "00 : 00";




        }



        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            songPlayer.ClearSongReader();
            Application.Current.Shutdown();
        }

        private  void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.RefreshUI();
            CommandManager.InvalidateRequerySuggested();
        }

        private void RefreshUI()
        {
            this.CurrentSongTime = songPlayer.GetCurrentTrackTimeInSeconds()
                .ToString(CultureInfo.DefaultThreadCurrentCulture);
        }

        public string WindowTitle { get; private set; }

        public ICommand ButtonCommand { get; set; }

        public ICommand PlayButton { get; set; }

        public ICommand BackwardButton { get; set; }

        public ICommand ForwardButton { get; set; }

        private double _progressSliderValue { get; set; }

        public double ProgressSliderValue
        {
            get
            {
                return _progressSliderValue;
            }
            set
            {
                _progressSliderValue = value;
                OnPropertyChanged();

            }
        }
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
                Console.WriteLine($"New Slider Value is: {SliderValue}");
                songPlayer.SetVolume(SliderValue);

            }
        }

        private string _currentSongTime { get; set; }

        public string CurrentSongTime
        {
            get
            {
                return _currentSongTime;
            }
            set
            {
                _currentSongTime = value;
                OnPropertyChanged();
            }
        }

        private TrulyObservableCollection<SingleTrack> _playlist { get; set; }

        public TrulyObservableCollection<SingleTrack> Playlist
        {
            get
            {
                return _playlist;
            }
            set
            {
                _playlist = value;
                OnPropertyChanged();
            }
        }


        public string PlayButtonText { get; set; }

        public string FirstName { get; set; }

        
        public string AbsoluteSongTime { get; set; }

        public int Progress { get; set; }

        public bool Paused { get; set; }




        private void OpenFileButtonClick(object sender)
        {
            fileManager.AddFileToPlaylist(playlistHandler);
        }

        private void PlayButtonClick(object sender)
        {
            if (songPlayer.getPauseStatus())
            {
                songPlayer.PlaySong();
                PlayButtonText = "Pause";
            }
            else
            {
                PlayButtonText = "Play";
                songPlayer.PauseSong();
            }
            AbsoluteSongTime = TimeConverter.GetAbsoluteTrackTimeAsString(songPlayer);
            Paused = !Paused;
        }

        private void ForwardButtonClick(object sender)
        {
           songPlayer.PlayNextSong();
        }

        private void BackwardButtonClick(object sender)
        {
            songPlayer.PlayLastSong();
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