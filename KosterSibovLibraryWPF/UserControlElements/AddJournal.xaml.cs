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
using System.Xml.Linq;
using KosterSibovLibrary;

namespace KosterSibovLibraryWPF.UserControlElements
{
    /// <summary>
    /// Логика взаимодействия для AddJournal.xaml
    /// </summary>
    public partial class AddJournal : UserControl
    {
        Journal journal = new Journal();
        public AddJournal()
        {
            InitializeComponent();
        }
        private void btnReturn_Click(object sender, RoutedEventArgs e) //06работчик события кнопки возврата 
        {
            ReturnToManagerMenu();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e) // Обработчик события кнопки ввода
        {

            try
            {
                AssignJournalProperties();
                journal.IsFormValid();
                journal.IsIsbnValid(isbnx.txtInput.Text);
                journal.IsPriceDouble(pricex.txtInput.Text);
                journal.IsQuantityInt(quantityx.txtInput.Text);
                Journal actualJournal = new Journal(journal.Isbn, journal.Name, journal.Edition, journal.Quantity, journal.Price);
                DataBase.AddItem(actualJournal);
                DataBase.SaveItemInformation(actualJournal);
                MessageBox.Show($"{actualJournal.Name} has been successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearAllTextBoxes();
                ReturnToManagerMenu();
            }
            catch (ItemAlreadyExistsException ex)
            {
                ErrorMessage(ex);
            }
            catch (IllegalIsbnException ex)
            {
                ErrorMessage(ex);
            }
            catch(FormatException ex)
            {
                ErrorMessage(ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                ErrorMessage(ex);
                DataBase.RemoveItem(isbnx.txtInput.Text);
            }
        }
        private void ErrorMessage(Exception ex) //обработчик события исключения
        {
            DataBase.LogException(ex);
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void ClearAllTextBoxes() //метод отчистки всех полей ввода
        {
            isbnx.txtInput.Clear();
            namex.txtInput.Clear();
            editionx.txtInput.Clear();
            quantityx.txtInput.Clear();
            pricex.txtInput.Clear();
        }


        private void ReturnToManagerMenu() // метод возврата в меню менеджера
        {
            Window mainWindow = Window.GetWindow(this);
            Grid AddJournalGrid = (Grid)mainWindow.FindName("AddJournalGrid");
            AddJournalGrid.Visibility = Visibility.Collapsed;
            Grid managerGrid = (Grid)mainWindow.FindName("managerGrid");
            managerGrid.Visibility = Visibility.Visible;
            ClearAllTextBoxes();
        }
        private void AssignJournalProperties() // метод присвоения вводимых значений новому журналу
        {
            journal.Isbn = isbnx.txtInput.Text;
            journal.Name = namex.txtInput.Text; journal.Edition = editionx.txtInput.Text;
            journal.DateOfPrint = DateTime.Now;
        }
    }
}
