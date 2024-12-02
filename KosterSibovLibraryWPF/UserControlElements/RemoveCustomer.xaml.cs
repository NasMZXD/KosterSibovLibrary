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
using System.IO;
using KosterSibovLibrary;

namespace KosterSibovLibraryWPF.UserControlElements
{
    /// <summary>
    /// Логика взаимодействия для RemoveCustomer.xaml
    /// </summary>
    public partial class RemoveCustomer : UserControl
    {
        public RemoveCustomer()
        {
            InitializeComponent();
            btnReturn.ButtonContent = Resex.btnReturn;
            btnEnter.ButtonContent = Resex.btnEnter;

            btnReturn.ButtonClickEvent += btnInput_Click;
            btnEnter.ButtonClickEvent += btnInput_Click;
        }
        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnEnter)
            {
                if (idBox.txtInput.Text != null)
                {
                    try
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you sure want to remove {idBox.txtInput.Text} from the club?", "Remove Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            Customer.RemoveCustomerFromClub(idBox.txtInput.Text);
                            Customer.DeleteFile(idBox.txtInput.Text);
                            MessageBox.Show($"The customer {idBox.txtInput.Text} has been successfully removed from the club!", "Customer Removed", MessageBoxButton.OK, MessageBoxImage.Information);
                            ReturnToWorkerMenu();
                        }
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        ErrorMessage(ex);
                        Customer.AddCustomerToClub(idBox.txtInput.Text);
                        idBox.txtInput.Focus();
                    }
                    catch (IllegalIdException ex)
                    {
                        ErrorMessage(ex); 
                        idBox.txtInput.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter the customer's id you would like to remove!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    idBox.txtInput.Focus();
                }
            }
            else
            {
                ReturnToWorkerMenu();
            }
        }

        private void ErrorMessage(Exception ex) //обработчик событий исключений
        {
            DataBase.LogException(ex);
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ReturnToWorkerMenu() //метод возврата в меню менеджера
        {
            Window mainWindow = Window.GetWindow(this);
            Grid RemoveCustomerGrid = (Grid)mainWindow.FindName("RemoveCustomerGrid");
            RemoveCustomerGrid.Visibility = Visibility.Collapsed;
            Grid workerGrid = (Grid)mainWindow.FindName("workerGrid"); workerGrid.Visibility = Visibility.Visible;
            idBox.txtInput.Text = string.Empty;
        }
    }
}
