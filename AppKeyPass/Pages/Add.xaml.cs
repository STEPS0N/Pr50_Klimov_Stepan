using AppKeyPass.Models;
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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        Storage ChangeStorage;
        public Add(Storage storage = null)
        {
            InitializeComponent();

            ChangeStorage = storage;

            if (ChangeStorage != null)
            {
                tbName.Text = ChangeStorage.Name;
                tbUrl.Text = ChangeStorage.Url;
                tbLogin.Text = ChangeStorage.Login;
                tbPassword.Text = ChangeStorage.Password;
                btn.Content = "Изменить";
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (ChangeStorage == null)
            {
                Storage storage = new Storage()
                {
                    Name = tbName.Text,
                    Url = tbUrl.Text,
                    Login = tbLogin.Text,
                    Password = tbPassword.Text,
                };
                StorageContext.Add(storage);
            }
            else
            {
                ChangeStorage.Name = tbName.Text;
                ChangeStorage.Url = tbUrl.Text;
                ChangeStorage.Login = tbLogin.Text;
                ChangeStorage.Password = tbPassword.Text;

                StorageContext.Update(ChangeStorage);
            }
            MessageBox.Show("Данные сохранены");
            MainWindow.init.OpenPage(new Pages.Main());
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Main());
        }
    }
}
