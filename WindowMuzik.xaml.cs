using System.Windows.Input;
using Microsoft.Win32;
using System.Windows;
using System;

namespace my_project
{
    public partial class WindowMuzik : Window
    {
        public WindowMuzik()
        {
            InitializeComponent();
        }

        private void Label_MouseDown(object sender,                 MouseButtonEventArgs e) => DragMove();


        //обработка нажатия кнопок
        private void ButtonStop_OnClick(object sender,              RoutedEventArgs e)
        {
            Player.Stop();
        }
        private void ButtonOpen_OnClick(object sender,              RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                Player.Source = new Uri(dialog.FileName, UriKind.Absolute);
            }
            Position.Maximum = Player.NaturalDuration.TimeSpan.TotalSeconds;
        }
        private void ButtonPlay_OnClick(object sender,              RoutedEventArgs e)
        {
            Player.Play();
        }
        private void ButtonPause_OnClick(object sender,             RoutedEventArgs e)
        {
            Player.Pause();
        }
        private void Button_Close_Window_Click(object sender,       RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Minimized_Window_Click(object sender,   RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //элементы плеера
        private void Volume_OnValueChanged(object sender,           RoutedPropertyChangedEventArgs<double> e)
        {
            Player.Volume = (sender as System.Windows.Controls.Slider)!.Value;
        }
        private void Position_OnValueChanged(object sender,         RoutedPropertyChangedEventArgs<double> e)
        {
            Player.Pause();
            Player.Position = new TimeSpan(0, 0, 0, (int)(sender as System.Windows.Controls.Slider)!.Value);
            Player.Play();
        }
    }
}