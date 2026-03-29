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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
        }

        public async Task GetStorage()
        {
            List<Storage> Storages = await StorageContext.Get();

            StorageList.Children.Clear();

            foreach (Storage Storage in Storages)
            {
                StorageList.Children.Add(new Elements.Item(Storage, this));
            }
        }
        
        private void OpenPageAdd(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Add());
        }
    }
}
