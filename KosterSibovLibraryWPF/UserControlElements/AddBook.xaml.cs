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
    /// Логика взаимодействия для AddBook.xaml
    /// </summary>
    public partial class AddBook : UserControl
    {
        Book book = new Book();
        public AddBook()
        {
            InitializeComponent();
            comboBox.ItemsSource = Enum.GetValues(typeof(genre));
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e) //метод обработчика событий для кнопки возврата
        {
            ReturnToManagerMenu();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AssignBookProperties(); 
                IsComboBoxNull();
                book.IsFormValid();
                book.IsIsbnValid(isbnx.txtInput.Text);
                book.IsPriceDouble(pricex.txtInput.Text);
                book.IsQuantityInt(quantityx.txtInput.Text);
                Book actualBook = new Book(isbnx.txtInput.Text, namex.txtInput.Text, editionx.txtInput.Text, book.Quantity, summaryx.txtInput.Text, book.Genre, book.Price); 
                DataBase.AddItem(actualBook);
                DataBase.SaveItemInformation(actualBook);
                MessageBox.Show($"{actualBook.Name} has been successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearAllTextBoxes();
                ReturnToManagerMenu();
            }
            catch (ArgumentNullException ex)
            {
                ErrorMessage(ex);
            }
            catch (FormatException ex)
            {
                ErrorMessage(ex);
            }
            catch (ItemAlreadyExistsException ex)
            {
                ErrorMessage(ex);
            }
            catch (IllegalIsbnException ex)
            {
                ErrorMessage(ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                ErrorMessage(ex);
                DataBase.RemoveItem(isbnx.txtInput.Text);
            }
        }
        private void ErrorMessage(Exception ex) //метод обработчика исключений
        {
            DataBase.LogException(ex);
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void ClearAllTextBoxes() //метод отчистки всех блоков текста

        {
            isbnx.txtInput.Clear();
            namex.txtInput.Clear();
            editionx.txtInput.Clear();
            quantityx.txtInput.Clear();
            summaryx.txtInput.Clear();
            pricex.txtInput.Clear();
            comboBox.SelectedItem = null;
            comboBox.Text = "Genre";
            comboBox.Foreground = Brushes.DarkGray;
        }
        private void ReturnToManagerMenu() // метод возврата в меню менеджера
        {
            Window mainWindow = Window.GetWindow(this);
            Grid addBookGrid = (Grid)mainWindow.FindName("addBookGrid");
            addBookGrid.Visibility = Visibility.Collapsed;
            Grid managerGrid = (Grid)mainWindow.FindName("managerGrid"); managerGrid.Visibility = Visibility.Visible;
            ClearAllTextBoxes();
        }
        private void AssignBookProperties() //метод присвоения вводимых значений новой книге
        {
            book.Isbn = isbnx.txtInput.Text;
            book.Name = namex.txtInput.Text;
            book.Edition = editionx.txtInput.Text; 
            book.Summary = summaryx.txtInput.Text; 
            book.DateOfPrint = DateTime.Now;
        }

        private void IsComboBoxNull() //метод проверки пустого поля жанра
        {
            if (comboBox.SelectedItem == null)
            {
                throw new ArgumentNullException("Please select a genre!");
            }
            else book.Genre = (genre)comboBox.SelectedItem;
        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs е) //метод обработки при выбора жанра
        {
            comboBox.Foreground = Brushes.Black;
        }
        private void comboBox_DropDownOpened(object sender, EventArgs e) //Meтод обработêи при раскрытии мено выбора жанра
        {
            comboBox.Foreground = Brushes.Black;
        }
        private void comboBox_DropDownClosed(object sender, EventArgs e) ///метод обpaботêи закрытия списка выбора жанра
        {
            if (comboBox.SelectedItem != null) comboBox.Foreground = Brushes.Black;
            else comboBox.Foreground = Brushes.DarkGray;
        }
    }
}
