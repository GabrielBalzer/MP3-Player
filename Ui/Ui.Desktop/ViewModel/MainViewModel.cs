using GalaSoft.MvvmLight;

namespace MP3Player.Ui.Desktop.ViewModel
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
        }

        public string WindowTitle { get; private set; }
    }
}