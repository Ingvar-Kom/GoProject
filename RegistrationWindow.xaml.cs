using System;
using System.IO;
using System.Data;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace my_project
{
    public partial class RegistrationWindow : Window
    {
        private void Label_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();
        DataBase dataBase = new DataBase();
        DateTime DateOfBirth;
        byte[] image_bytes;

        public RegistrationWindow()
        {
            InitializeComponent();
            InputDateOfBirth.SelectedDateChanged += InputAge_SelectedDateChanged;
        }


        //проверка введённых данных с теми что есть в бд
        private Boolean Checkuser()
        {
            var logiUser = InputLog.Text;
            var passUser = InputPass.Password;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring1 = $"select id_user, login_user, password_user from register where login_user ='{logiUser}' and password_user = '{passUser}'";
            SqlCommand command = new SqlCommand(querystring1, dataBase.GetConnection());
            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else if (image_bytes == null)
            {
                MessageBox.Show("Загрузите фото!");
                return true;
            }
            else
            {
                return false;
            }
        }

        //вычисление возроста
        private void InputAge_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateOfBirth = InputDateOfBirth.SelectedDate.Value;
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            InputAge.Text = age.ToString();
        }

        //обработки нажатия кнопок
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            image_bytes = File.ReadAllBytes(openFileDialog.FileName);
            image.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray(image_bytes));
        }
        private void Early_Access_Button_Click(object sender, RoutedEventArgs e)
        {
            Global1.LOGG1 = 0;
            WebMainWindow webMainWindow = new WebMainWindow();
            webMainWindow.Show();
            Close();
        }
        private void Button_Registration_Click(object sender, RoutedEventArgs e)
        {
            string login = InputLog.Text.Trim();
            string pass = InputPass.Password.Trim();
            string Imy = InputFirstName.Text.Trim();
            string Famili = InputLastName.Text.Trim();


            if (Famili.Length == 0)
            {
                InputLastName.Background = Brushes.DarkRed;
            }
            else if (Imy.Length == 0)
            {
                InputLastName.Background = Brushes.Transparent;
                InputFirstName.Background = Brushes.DarkRed;
            }
            else if (login.Length < 5)
            {
                InputFirstName.Background = Brushes.Transparent;
                InputLastName.Background = Brushes.Transparent;
                InputLog.Background = Brushes.DarkRed;
            }
            else if (pass.Length < 5)
            {
                InputLog.Background = Brushes.Transparent;
                InputFirstName.Background = Brushes.Transparent;
                InputLastName.Background = Brushes.Transparent;
                InputPass.Background = Brushes.DarkRed;
            }
            else
            {
                InputLog.Background = Brushes.Transparent;
                InputPass.Background = Brushes.Transparent;
                InputFirstName.Background = Brushes.Transparent;
                InputLastName.Background = Brushes.Transparent;

                var fName = InputFirstName.Text;
                var lName = InputLastName.Text;
                var log = InputLog.Text;
                var pas = InputPass.Password;
                var Dat = InputDateOfBirth.DisplayDate;
                if (Checkuser() == false)
                {
                    string connectionString = @"Data Source=DESKTOP-NTMKSG2\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand
                        {
                            Connection = connection,
                            CommandText = $"INSERT INTO register(Rimage, login_user, password_user, LName, FName, DateOfBirth) VALUES (@ImageData, '{log}', '{pas}','{lName}','{fName}','{Dat}')"
                        };
                        command.Parameters.Add("@ImageData", SqlDbType.Image, 1000000);
                        command.Parameters["@ImageData"].Value = image_bytes;

                        if (command.ExecuteNonQuery() == 1)
                        {
                            
                            string query = "SELECT TOP 1 id_user FROM register ORDER BY id_user DESC";

                            using (SqlConnection connection1 = new SqlConnection(connectionString))
                            {
                                Global1.LOGG1 = 1;
                                connection1.Open();
                                using (SqlCommand command1 = new SqlCommand(query, connection1))
                                {
                                    Global.LOGG = (int)command1.ExecuteScalar();
                                }
                            }
                            MessageBox.Show("Регистрация прошла успешно!");
                            WebMainWindow webMainWindow = new WebMainWindow();
                            webMainWindow.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при регистрации!");
                        }
                        dataBase.CloseConnection();
                    }
                }
            }
        }
        private void Button_Close_Window_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Minimized_Window_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Button_Return_To_Start_Window_Click(object sender, RoutedEventArgs e)
        {
            WelcomeWindow welcomeWindow = new WelcomeWindow();
            welcomeWindow.Show();
            Close();
        }
        private void Go_Button_To_The_Authorization_Window_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            Close();
        }
    }
} 