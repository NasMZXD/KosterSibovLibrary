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
using KosterSibovLibrary;

namespace KosterSibovLibraryWPF.UserControlElements
{
    /// <summary>
    /// Логика взаимодействия для Worker.xaml
    /// </summary>
    public partial class Worker : UserControl
    {
        public Worker()
        {
            InitializeComponent();
            AssignButtonNames();
            AssignButtonEvents();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            Grid workerGrid = (Grid)mainWindow.FindName("workerGrid");
            if (sender == sellItem)
            {
                int itemCount = DataBase.ItemCount();
                if (itemCount == 0)
                {
                    MessageBox.Show("There are currently no items available for sale!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    workerGrid.Visibility = Visibility.Collapsed;
                    Grid SellItemsGrid = (Grid)mainWindow.FindName("SellItemsGrid");
                    SellItemsGrid.Visibility = Visibility.Visible;

                    SellItems sellItemsControl = (SellItems)SellItemsGrid.Children[0];
                    sellItemsControl.DisplayItems();
                }
            }
            else if (sender == membership)
            {
                workerGrid.Visibility = Visibility.Collapsed;
                Grid AddCustomerGrid = (Grid)mainWindow.FindName("AddCustomerGrid");
                AddCustomerGrid.Visibility = Visibility.Visible;
            }
            else if (sender == removeMembership)
            {
                workerGrid.Visibility = Visibility.Collapsed;
                Grid RemoveCustomerGrid = (Grid)mainWindow.FindName("RemoveCustomerGrid");
                RemoveCustomerGrid.Visibility = Visibility.Visible;
            }
            else if (sender == mainMenu)
            {
                workerGrid.Visibility = Visibility.Collapsed;
                Grid mainMenu = (Grid)mainWindow.FindName("MainMenu");
                mainMenu.Visibility = Visibility.Visible;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void AssignButtonNames() //добавление названий кнопкам
        {
            membership.ButtonContent = Resex.btnClub;
            mainMenu.ButtonContent = Resex.btnMainMenu;
            exit.ButtonContent = Resex.btnExit;
            sellItem.ButtonContent = Resex.btnSellItems;
            removeMembership.ButtonContent = Resex.btnRemoveCustomer;
        }
        private void AssignButtonEvents() //присваивание кнопкам обрабботчика событий
        {
            membership.ButtonClickEvent += Button_Click;
            mainMenu.ButtonClickEvent += Button_Click;
            exit.ButtonClickEvent += Button_Click;
            sellItem.ButtonClickEvent += Button_Click;
            removeMembership.ButtonClickEvent += Button_Click;
        }
    }
}
