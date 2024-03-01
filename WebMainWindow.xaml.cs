using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System;

namespace my_project
{
    public partial class WebMainWindow : Window
    {
        WindowMuzik windowMuzik;
        DateTime DateOfBirth;
        byte[] image_bytes;
        DataBase dataBase = new DataBase();
        private void Label_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        public WebMainWindow()
        {
            InitializeComponent();
            if(Global1.LOGG1 == 1)
            {
                YourCode();
                YourCodeProgress();
            }
        }

        //установка прогреса
        private void YourCodeProgress()
        {
            string connectionString = @"Data Source=DESKTOP-NTMKSG2\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            string query = $"select ProgressB1, ProgressB2, ProgressB3 from register where id_user = '{Global.LOGG}'";
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command1 = new SqlCommand(query, connection);
            using SqlDataReader reader = command1.ExecuteReader();
            while (reader.Read())
            {
                if (!Convert.IsDBNull(reader["ProgressB1"]))
                {
                    Progress1.Value = Convert.ToDouble(reader["ProgressB1"]);
                    if (Progress1.Value >= 100)
                    {
                        ListOfUsers.Items.Add("выполнено первое задание");
                    }
                }
                if (!Convert.IsDBNull(reader["ProgressB2"]))
                {
                    Progress2.Value = Convert.ToDouble(reader["ProgressB2"]);
                    if (Progress2.Value >= 100)
                    {
                        ListOfUsers.Items.Add("выполнено второе задание");
                    }
                }
                if (!Convert.IsDBNull(reader["ProgressB3"]))
                {
                    Progress3.Value = Convert.ToDouble(reader["ProgressB3"]);
                    if (Progress3.Value >= 100)
                    {
                        ListOfUsers.Items.Add("выполнено третье задание");
                    }
                }
            }
        }

        //отрисовка данных автаризованного пользователя
        private void YourCode()
        {
            string connectionString = @"Data Source=DESKTOP-NTMKSG2\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            string query = $"select * from register where id_user = '{Global.LOGG}'";
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            using SqlCommand command1 = new SqlCommand(query, connection);
            using SqlDataReader reader = command1.ExecuteReader();
            while (reader.Read())
            {
                image_bytes = (byte[])reader["Rimage"];
                Img.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray(image_bytes));
                InputFirstName.Content = reader.GetString(5);
                InputLastName.Content = reader.GetString(4);

                DateOfBirth = reader.GetDateTime(6);
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                InputDateOfBirth.Content += " " + age.ToString();
            }
        }

        //обработка нажатия кнопок
        private void Mes_Demo(object sender,                        RoutedEventArgs e)
        {
            MessageBox.Show("сработала какая-то функция)");
        }
        private void New_Info_Messeg(object sender,                 RoutedEventArgs e)
        {
            MessageBox.Show("Это основное окно приложения.\n" +
                " и это кнопка в которой будет\n" +
                " подробная информация о работе приложения,\n" +
                " и о всех её возможностях.\n" +
                " если есть идеи по улочшению функционала приложения\n" +
                " буду рад услышать ваше мнение");
        }
        private void New_Messeg_Save(object sender,                 RoutedEventArgs e)
        {
            MessageBox.Show("пока-что нечего сохранять");
        }
        private void Button_Window_Main(object sender,              RoutedEventArgs e)
        {
            if (Global1.LOGG1 == 1)
            {
                MainWindowApplications mainWindowApplications = new();
                mainWindowApplications.Show();
                Close();
            }
            else
            {
                MessageBox.Show("чтобы выполнять задания вам нужно авторизоваться");
            }

            
            
        }
        private void Button_Window_Muzic(object sender,             RoutedEventArgs e)
        {
            windowMuzik = new();
            windowMuzik.Show();
        }
        private void Button_Search_Click(object sender,             RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ya.ru");
        }
        private void Button_Window_Welcom(object sender,            RoutedEventArgs e)
        {
            if (windowMuzik != null)
            {
                windowMuzik.Close();
            }
            Global1.LOGG1 = 0;
            WelcomeWindow welcomeWindow = new();
            welcomeWindow.Show();
            Close();
        }
        private void Button_Close_Window_Click(object sender,       RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Minimized_Window_Click(object sender,   RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //удаление прогреа
        private void ClearProgress(object sender,                   RoutedEventArgs e)
        {
            if (Global1.LOGG1 == 1)
            {
                string connectionString = @"Data Source=DESKTOP-NTMKSG2\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";

                string query = $"update register set ProgressB1 = 0, ProgressB2 = 0, ProgressB3 = 0 where id_user = '{Global.LOGG}'";

                using SqlConnection connection = new(connectionString);

                using SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();


                WebMainWindow webMainWindow = new();
                webMainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("чтобы взаимодействовать с прогресом нужно авторизоваться");
            }
        }

        //?
        private void settingsButtonClick(object sender,             RoutedEventArgs e)
        {
            MessageBox.Show("тут будет окно в котором можно менять стили приложения");
        }
        private void Button_Window_Pipl(object sender,              RoutedEventArgs e)
        {
            MessageBox.Show("можно будет видеть других пользователей приложения");
        }
        private void Button_Window_Pipl_Messeg(object sender,       RoutedEventArgs e)
        {
            MessageBox.Show("можно будет переписываться с другими пользователями");
        }
    }
}