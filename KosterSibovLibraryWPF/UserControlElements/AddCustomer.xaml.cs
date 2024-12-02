using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : UserControl
    {
        const int MEMBERSHIPPRICE = 25;
        public AddCustomer()
        {
            InitializeComponent();
            btnEnter.ButtonContent = Resex.btnEnter;
            btnreturn.ButtonContent = Resex.btnReturn;
            btnPayEnter.ButtonContent = Resex.btnPay;
            btnPayReturn.ButtonContent = Resex.btnReturn;

            btnEnter.ButtonClickEvent += btnInput_Click;
            btnreturn.ButtonClickEvent += btnInput_Click;
            btnPayEnter.ButtonClickEvent += BtnPayEnter_ButtonClickEvent;
            btnPayReturn.ButtonClickEvent += BtnPayReturn_ButtonClickEvent;
        }
        private void btnInput_Click(object sender, RoutedEventArgs e) //oбpa6отчик события нажатия кнопки if (sender == btnEnter)
        {
            if (sender == btnEnter)
            {
                try
                {
                    if (idBox != null)
                    {
                        Customer.IsIdValid(idBox.txtInput.Text);
                        Customer.DoesIdExistInClub(idBox.txtInput.Text);
                        ProceedToPayMеnu();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter the customer's ID!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (IllegalIdException ex)
                {
                    DataBase.LogException(ex);
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    idBox.txtInput.Focus();
                }
            }
            else // return
            {
                ReturnToWorkerMenuFromStart();
            }
        }
        private void ReturnToWorkerMenuFromStart() //метод возврата в меню работника из начального окна добавления
        {
            Window mainWindow = Window.GetWindow(this);
            Grid AddCustomerGrid = (Grid)mainWindow.FindName("AddCustomerGrid");
            AddCustomerGrid.Visibility = Visibility.Collapsed;

            Grid workerGrid = (Grid)mainWindow.FindName("workerGrid");
            workerGrid.Visibility = Visibility.Visible;

            idBox.txtInput.Text = string.Empty;
        }
        private void BtnPayEnter_ButtonClickEvent(object sender, RoutedEventArgs e) //обработчик события кнопки для оплаты членского взноса
        {
            if (payBox.txtInput.Text == MEMBERSHIPPRICE.ToString())
            {
                try
                {
                    Customer.AddCustomerToClub(idBox.txtInput.Text);
                    Customer.SaveCustomer(idBox.txtInput.Text);
                    MessageBox.Show($"Customer {idBox.txtInput.Text} has been successfully added to the club! ", "Customer Added", MessageBoxButton.OK, MessageBoxImage.Information);
                    ReturnToWorkerMenuEnd();
                }
                catch (DirectoryNotFoundException ex)
                {
                    Customer.RemoveCustomerFromClub(idBox.txtInput.Text);
                    ErrorMessage(ex);
                    payBox.txtInput.Focus();
                }
                catch (IllegalIdException ex)
                {
                    ErrorMessage(ex);
                    payBox.txtInput.Focus();
                }
            }
            else
            {
                MessageBox.Show("Please pay the specified amount!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); payBox.txtInput.Focus();
            }
        }
        private void ErrorMessage(Exception ex) //метод обработчика событий
        {
            DataBase.LogException(ex);
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void BtnPayReturn_ButtonClickEvent(object sender, RoutedEventArgs e) //обработчик события кнопки возврата из меню оплаты
        {
            ReturnToIdMenu();
        }
        private void ProceedToPayMеnu() //метод перехода в меню оплаты
        {
            title.Text = $"You need to pay {MEMBERSHIPPRICE:C}";
            btnEnter.Visibility = Visibility.Collapsed;
            btnreturn.Visibility = Visibility.Collapsed;
            viewBox1.Visibility = Visibility.Collapsed;
            btnPayEnter.Visibility = Visibility.Visible;
            btnPayReturn.Visibility = Visibility.Visible;
            viewBox2.Visibility = Visibility.Visible;
        }
        private void ReturnToWorkerMenuEnd() //метод возвращения в меню работника из меню оплаты
        {
            ReturnToIdMenu();
            ReturnToWorkerMenuFromStart();
        }
        private void ReturnToIdMenu() //метод возвращения в меню добавления покупателя
        {
            title.Text = "Please enter the customer's ID number you would like to add:";
            btnEnter.Visibility = Visibility.Visible;
            btnreturn.Visibility = Visibility.Visible;
            viewBox1.Visibility = Visibility.Visible;
            btnPayEnter.Visibility = Visibility.Collapsed;
            btnPayReturn.Visibility = Visibility.Collapsed;
            viewBox2.Visibility = Visibility.Collapsed;
            payBox.txtInput.Text = string.Empty;
        }
    }
}
