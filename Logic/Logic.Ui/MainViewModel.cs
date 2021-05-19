using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;


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
                Progress = 0;
                WindowTitle = "MP3Player";
            }


            DispatcherHelper.Initialize();

            Task.Run(
                () =>
                {
                    Task.Delay(500).ContinueWith(
                        t => {
                            while (Progress < 100)
                            {
                                //DispatcherHelper.RunAsync((() => Progress += 5));
                                DispatcherHelper.RunAsync((() => CurrentSongTime = TimeConverter.getCurrentTrackTimeAsString()));
                                Task.Delay(500).Wait();
                            }
                        });
                });

            ButtonCommand = new RelayCommand(o => MainButtonClick("MainButton"));
            PlayButton = new RelayCommand(o => PlayButtonClick("PlayButton"));
            Paused = true;
            PlayButtonText = "Play";
            CurrentSongTime = "00:00";
            AbsoluteSongTime = "00:00";

            /*DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            dispatcherTimer.Start();*/
        }

        private  void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.RefreshUI();
            CommandManager.InvalidateRequerySuggested();
        }

        private void RefreshUI()
        {
            this.CurrentSongTime = SongPlayer.GetCurrentTrackTimeInSeconds()
                .ToString(CultureInfo.DefaultThreadCurrentCulture);
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

        private void Printer()
        {
            Console.WriteLine($"New Slider Value is: {SliderValue}");
            SongPlayer.SetVolume(SliderValue);
        }

        public string PlayButtonText { get; set; }

        public string FirstName { get; set; }

        
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