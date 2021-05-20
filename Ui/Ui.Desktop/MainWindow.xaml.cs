using System;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MP3Player.Logic.Ui;

namespace MP3Player.Ui.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public bool sliderValueChanged { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            sliderValueChanged = false;
            this.PreviewMouseUp += new MouseButtonEventHandler(slider_MouseUp);
        }

        void slider_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sliderValueChanged == true)
            {
                //MessageBox.Show("Value changed to " + ProgressSlider.Value.ToString());
                sliderValueChanged = false;
                
                Communicator.ProgressSliderIsDragging = false;
                Communicator.ProgressSliderValueChanged = true;
                e.Handled = true;
            }
        }


        private void ProgressSlider_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            sliderValueChanged = true;
            Communicator.ProgressSliderIsDragging = true;
        }
    }


}
