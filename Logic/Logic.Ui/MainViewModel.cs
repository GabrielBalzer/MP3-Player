using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;

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


        private void MainButtonClick(object sender)
        {
            throw new NotImplementedException();
        }

    }
}