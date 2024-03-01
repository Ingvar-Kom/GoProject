using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace my_project
{
    public partial class AuthorizationWindow : Window
    {
        DataBase dataBase       = new DataBase();
        public AuthorizationWindow()
        {
            InitializeComponent();
        }
        private void Label_MouseDown(object sender,                 MouseButtonEventArgs e) => DragMove();
        private void Button_Window_Reg(object sender,               RoutedEventArgs e)
        {
            RegistrationWindow winR = new RegistrationWindow();
            winR.Show();
            Close();
        }
        private void Button_Window_Vhod(object sender,              RoutedEventArgs e)
        {
            WelcomeWindow welcomeWindow = new WelcomeWindow();
            welcomeWindow.Show();
            Close();
        }
        private void Button_Window_Avtoriz(object sender,           RoutedEventArgs e)
        {
            var LoginUser           = TextBoxLogin.Text;
            var passUser            = TextBoxPassword.Password;
            SqlDataAdapter adapter  = new SqlDataAdapter();
            DataTable table         = new DataTable();
            string querystring      = $"select id_user, login_user, password_user from register where Login_user = '{LoginUser}' and password_user = '{passUser}'";
            SqlCommand command      = new SqlCommand(querystring, dataBase.GetConnection());
            adapter.SelectCommand   = command;
            adapter.Fill(table);
            
            if (table.Rows.Count == 1)
            {
                string connectionString = @"Data Source=DESKTOP-NTMKSG2\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
                string query = $"select id_user from register where Login_user = '{LoginUser}' and password_user = '{passUser}'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command1 = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Global1.LOGG1 = 1;
                                Global.LOGG = reader.GetInt32(0);
                                MessageBox.Show($"Авторицация прошла успешно {Global.LOGG}");
                            }
                        }
                    }
                }
                WebMainWindow windowApplications = new WebMainWindow();
                windowApplications.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Не верно введен логин или пароль");
            }
        }
        private void Button_Close_Window_Click(object sender,       RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Minimized_Window_Click(object sender,   RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
