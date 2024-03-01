using System.Windows;
using System.Windows.Input;

namespace my_project
{
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void Label_MouseDown(object sender,                 MouseButtonEventArgs e) => DragMove();

        //обработки нажатия кнопок
        private void Button_Minimized_Window_Click(object sender,   RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Button_Close_Window_Click(object sender,       RoutedEventArgs e)
        {
            Close();
        }
        private void Button_Window_Vhod(object sender,              RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            Close();
        }
        private void Button_Window_Reg(object sender,               RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            Close();
        }
        private void Button_Window_D(object sender,                 RoutedEventArgs e)
        {
            MainWindowApplications mainWindowApplications = new MainWindowApplications();
            mainWindowApplications.Show();
            Close();
        }
    }
}