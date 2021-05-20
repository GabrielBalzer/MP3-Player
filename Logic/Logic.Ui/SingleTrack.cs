using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using MP3Player.Logic.Ui.Annotations;

namespace MP3Player.Logic.Ui
{
    public class SingleTrack : INotifyPropertyChanged
    {
        private string filePath;
        private string trackName;
        private string trackNumber;
        private bool isPlaying;

        public SingleTrack(string filePath, string trackName, string trackNumber, bool isPlaying)
        {
            this.filePath = filePath;
            this.trackName = trackName;
            this.trackNumber = trackNumber;
            this.isPlaying = isPlaying;
        }

        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                OnPropertyChanged();
            }
        }

        public string TrackName
        {
            get { return trackName; }
            set
            {
                trackName = value;
                OnPropertyChanged();
            }
        }

        public string TrackNumber
        {
            get { return trackNumber; }
            set
            {
                trackNumber = value;
                OnPropertyChanged();
            }
        }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}