using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using KosterSibovLibrary;

namespace KosterSibovLibraryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CultureInfo newCulture = new CultureInfo("en-US"); // определение сормата выводимыx Данныx  
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
            try
            {
                DataBase.LoadItems();
                Customer.LoadCustomers();
            }
            catch (DirectoryNotFoundException ex)
            {
                DataBase.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
        private void btnManager_Click(object sender, RoutedEventArgs e) //обработчик событий кнопêи переxода на зкран менеджерa 
        {
            MainMenu.Visibility = Visibility.Collapsed;
            managerGrid.Visibility = Visibility.Visible;
        }
        private void btnWorker_Click(object sender, RoutedEventArgs e) //обработчик событий кнопки перехода на экран работника
        {
            MainMenu.Visibility = Visibility.Collapsed;
            workerGrid.Visibility = Visibility.Visible;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e) //обработчик событий кнопки выжода из приложения
        {
            Application.Current.Shutdown();
        }
    }
}
