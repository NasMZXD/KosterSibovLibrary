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
    public partial class AddManga : UserControl
    {
        Manga manga = new Manga();
        public AddManga()
        {
            InitializeComponent();
            comboBox.ItemsSource = Enum.GetValues(typeof(MangaType));
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e) //метод обработчика событий для кнопки возврата
        {
            ReturnToManagerMenu();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AssignMangaProperties(); 
                IsComboBoxNull();
                manga.IsFormValid();
                manga.IsIsbnValid(isbnx.txtInput.Text);
                manga.IsPriceDouble(pricex.txtInput.Text);
                manga.IsQuantityInt(quantityx.txtInput.Text);
                Manga actualManga = new Manga(isbnx.txtInput.Text, namex.txtInput.Text, editionx.txtInput.Text, manga.Quantity, manga.chapterCount, manga.mangaType, manga.Price); 
                DataBase.AddItem(actualManga);
                DataBase.SaveItemInformation(actualManga);
                MessageBox.Show($"{actualManga.Name} has been successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
            chapterCountx.txtInput.Clear();
            pricex.txtInput.Clear();
            comboBox.SelectedItem = null;
            comboBox.Text = "Genre";
            comboBox.Foreground = Brushes.DarkGray;
        }
        private void ReturnToManagerMenu() // метод возврата в меню менеджера
        {
            Window mainWindow = Window.GetWindow(this);
            Grid addMangaGrid = (Grid)mainWindow.FindName("addMangaGrid");
            addMangaGrid.Visibility = Visibility.Collapsed;
            Grid managerGrid = (Grid)mainWindow.FindName("managerGrid"); 
            managerGrid.Visibility = Visibility.Visible;
            ClearAllTextBoxes();
        }
        private void AssignMangaProperties() //метод присвоения вводимых значений новой книге
        {
            manga.Isbn = isbnx.txtInput.Text;
            manga.Name = namex.txtInput.Text;
            manga.Edition = editionx.txtInput.Text;
            manga.DateOfPrint = DateTime.Now;
        }

        private void IsComboBoxNull() //метод проверки пустого поля жанра
        {
            if (comboBox.SelectedItem == null)
            {
                throw new ArgumentNullException("Please select a genre!");
            }
            else manga.mangaType = (MangaType)comboBox.SelectedItem;
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
