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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        public async Task Registr(string name, string surname, string login, string password)
        {
            bool succes = await UserContext.Registration(name, surname, login, password);

            if (succes)
            {
                MessageBox.Show("Вы зарегестрированы!");
                MainWindow.init.OpenPage(new Pages.Login());
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
            }
        }

        private void BtnRegistration(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Необходимо указать ваше имя");
                return;
            }
            if (string.IsNullOrEmpty(tbSurname.Text))
            {
                MessageBox.Show("Необходимо указать вашу фамилию");
                return;
            }
            if (string.IsNullOrEmpty(tbLogin.Text))
            {
                MessageBox.Show("Необходимо указать ваш логин");
                return;
            }
            if (string.IsNullOrEmpty(tbPassword.Password))
            {
                MessageBox.Show("Придумайте пароль");
                return;
            }

            Registr(tbName.Text, tbSurname.Text ,tbLogin.Text, tbPassword.Password);
        }

        private void ToLogin(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Login());
        }
    }
}
