using KosterSibovLibrary;
using System.IO;
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

namespace KosterSibovLibraryWPF.UserControlElements
{
    /// <summary>
    /// Логика взаимодействия для RemoveJournal.xaml
    /// </summary>
    public partial class RemoveJournal : UserControl
    {
        Journal journal = new Journal();
        public RemoveJournal()
        {
            InitializeComponent();

            btnreturn.ButtonContent = Resex.btnReturn;
            btnEnter.ButtonContent = Resex.btnEnter;

            btnreturn.ButtonClickEvent += btnInput_Click;
            btnEnter.ButtonClickEvent += btnInput_Click;
        }

        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
            if(sender == btnEnter)
            {
                try
                {
                    journal = (Journal)DataBase.FindItem(isbnBox.txtInput.Text);
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to remove the journal: {journal.Name}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        DataBase.RemoveItem(isbnBox.txtInput.Text);
                        DataBase.DeleteFile(journal);
                        journal = new Journal();
                        MessageBox.Show("The journal has been successfully removed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information); ReturnToManagerMenu();
                    }
                }
                catch (IllegalIsbnException ex)
                {
                    ErrorMessage(ex);
                    isbnBox.txtInput.Focus();
                }
                catch (DirectoryNotFoundException ex)
                {
                    ErrorMessage(ex);
                    DataBase.AddItem(journal);
                    isbnBox.txtInput.Focus();
                }
                catch (InvalidCastException ex)
                {
                    DataBase.LogException(ex);
                    MessageBox.Show("The ISBN you entered belongs to a book!", "ERORR", MessageBoxButton.OK, MessageBoxImage.Error); isbnBox.txtInput.Focus();
                }
            }
            else
            {
                ReturnToManagerMenu();
            }
        }

        private void ErrorMessage(Exception ex) //обработчик событий исключений
        {
            DataBase.LogException(ex);
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ReturnToManagerMenu() //метод возврата в меню менеджера
        {
            Window mainWindow = Window.GetWindow(this);
            Grid RemoveJournalGrid = (Grid)mainWindow.FindName("RemoveJournalGrid");
            RemoveJournalGrid.Visibility = Visibility.Collapsed;
            Grid managerGrid = (Grid)mainWindow.FindName("managerGrid"); 
            managerGrid.Visibility = Visibility.Visible;
            isbnBox.txtInput.Text = string.Empty;
        }
    }
}
