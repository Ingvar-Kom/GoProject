using System;
using System.Windows;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;

namespace my_project
{
    public partial class MainWindowApplications : Window
    {
        public MainWindowApplications()
        {
            InitializeComponent();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();


        //обработка нажатия кнопок
        private void Mes_Demo(object sender,                                    RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("сработала какая-то функция)"); 
        }
        private void New_Info_Messeg(object sender,                             RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Это основное окно приложения.\n" +
                " и это кнопка в которой будет\n" +
                " подробная информация о работе приложения,\n" +
                " и о всех её возможностях.\n" +
                " если есть идеи по улочшению функционала приложения\n" +
                " буду рад услышать ваше мнение");
        }
        private void New_Messeg_Save(object sender,                             RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("пока-что нечего сохранять");
        }
        private void Button_Window_Web(object sender,                           RoutedEventArgs e)
        {
            WebMainWindow webMainWindow = new WebMainWindow();
            webMainWindow.Show();
            Close();
        }
        private void Button_Search_Click(object sender,                         RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ya.ru");
        }
        private void Button_Window_Muzic(object sender,                         RoutedEventArgs e)
        {
            WindowMuzik windowMuzik = new();
            windowMuzik.Show();
        }
        private void Button_Window_Welcom(object sender,                        RoutedEventArgs e)
        {
            WelcomeWindow welcomeWindow = new WelcomeWindow();
            welcomeWindow.Show();
            Close();
        }
        private void Button_Close_Window_Click(object sender,                   RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Minimized_Window_Click(object sender,               RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Button_ImitationOfWebsiteOperation_Click(object sender,    RoutedEventArgs e)
        {

            myImage.Source = new BitmapImage(new Uri("/Assets/picture/Alfa.jpg", UriKind.Relative));
        }


        //обработка чек-боксов
        private void Checked_Lvl(object sender,                                 RoutedEventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-NTMKSG2\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            string query = $"update register set ProgressB1 = COALESCE(ProgressB1, 0) + 20 where id_user = '{Global.LOGG}'";

            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
        }
        private void Checked_Lvl2(object sender,                                RoutedEventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-NTMKSG2\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            string query = $"update register set ProgressB2 = COALESCE(ProgressB2, 0) + 20 where id_user = '{Global.LOGG}'";

            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
        }
        private void Checked_Lvl3(object sender,                                RoutedEventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-NTMKSG2\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            string query = $"update register set ProgressB3 = COALESCE(ProgressB3, 0) + 20 where id_user = '{Global.LOGG}'";

            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(query, connection);

            connection.Open();
            command.ExecuteNonQuery();
        }
        private void Checked_Lvl3L(object sender,                               RoutedEventArgs e)
        {
            Checked_Lvl3(sender, e);

            BitmapImage bi = new();
            bi.BeginInit();
            bi.UriSource = new Uri("/Assets/picture/Beta.jpg", UriKind.RelativeOrAbsolute);
            bi.EndInit();

            myImage3.Source = bi;
        }
        private void Checked_Lvl2B(object sender,                               RoutedEventArgs e)
        {
            Checked_Lvl2(sender, e);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("/Assets/picture/Alians.jpg", UriKind.RelativeOrAbsolute);
            bi.EndInit();

            myImage2.Source = bi;
        }
        private void Checked_LvlA(object sender,                                RoutedEventArgs e)
        {
            Checked_Lvl(sender, e);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("/Assets/picture/Alfa.jpg", UriKind.RelativeOrAbsolute);
            bi.EndInit();

            myImage.Source = bi;
        }
    }
}
