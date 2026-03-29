using AppKeyPass.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppKeyPass.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        public async Task Auth(string login, string password)
        {
            string? Token = await UserContext.Login(login, password);

            if (Token == null)
            {
                MessageBox.Show("Логин и пароль указаны не верно");
            }
            else
            {
                MainWindow.Token = Token;
                MainWindow.init.OpenPage(new Pages.Main());
            }
        }

        private void BtnAuth(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLogin.Text))
            {
                MessageBox.Show("Необходимо указать логин пользователя");
                return;
            }
            if (string.IsNullOrEmpty(tbPassword.Password))
            {
                MessageBox.Show("Необходимо указать пароль пользователя");
                return;
            }

            Auth(tbLogin.Text, tbPassword.Password);
        }
    }
}
