using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
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
    /// Логика взаимодействия для EditManga2.xaml
    /// </summary>
    public partial class EditManga2 : UserControl
    {
        Manga manga = new Manga();
        public EditManga2()
        {
            InitializeComponent();
            btnEnter.ButtonContent = Resex.btnEnter;
            btnReturn.ButtonContent = Resex.btnReturn;

            btnEnter.ButtonClickEvent += btnInput_Click;
            btnReturn.ButtonClickEvent += btnInput_Click;
        }

        public void PopulateComboBox()
        {
            if (comboBox.Items.Count == 0)
            {
                comboBox.Items.Add($"ISBN ({manga.Isbn})");
                comboBox.Items.Add($"Name ({manga.Name})");
                comboBox.Items.Add($"Edition ({manga.Edition})");
                comboBox.Items.Add($"Quantity ({manga.Quantity})");
                comboBox.Items.Add($"Price ({manga.Price})");
                comboBox.Items.Add($"Chapters count ({manga.chapterCount})");
                comboBox.Items.Add($"Type of Manga ({manga.mangaType})");
            }
        }
        public void ClearComboBox()
        {
            comboBox.Items.Clear();
        }
        public void FindManga() //метод нахождения книги по номеру
        {
            try
            {
                manga = new Manga();
                manga = (Manga)DataBase.FindItem(EditManga.Isbn);
            }
            catch (InvalidCastException ex)
            {
                DataBase.LogException(ex);
                ReturnToFirstMenu();
                MessageBox.Show("The ISBN you entered belongs to a journal!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnInput_Click(object sender, RoutedEventArgs e) //oбpaботчик события кнопки ввода if (sender == btnEnter)
        {
            if (sender == btnEnter)
            {
                if (comboBox.SelectedItem != null)
                {
                    ProceedToChangeProperty();
                }
                else
                {
                    MessageBox.Show("Please choose an option!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ReturnToFirstMenu();
            }
        }
        private void btnEnter2_Click(object sender, RoutedEventArgs e) //обработчик события кнопки подтверждения изменяемой опции 
        {
            if (comboBox.SelectedIndex == 0)
            {
                EditIsbn();
            }
            else if (comboBox.SelectedIndex == 1)
            {
                EditName();
            }
            else if (comboBox.SelectedIndex == 2)
            {
                EditEdition();
            }
            else if (comboBox.SelectedIndex == 3)
            {
                EditQuantity();
            }
            else if (comboBox.SelectedIndex == 4)
            {
                EditPrice();
            }
            else if (comboBox.SelectedIndex == 5)
            {
                EditChapterCount();
            }
            else
            {
                EditMangaType();
            }
        }

        private void btnReturn2_Click(object sender, RoutedEventArgs e) //обработчик события кнопки возврата в меню выбора опций
        {
            ReturnToOptionSelectionMenu();
        }

        private void ProceedToChangeProperty()
        {
            if (comboBox.SelectedIndex == 6)
            {

                btnEnter.Visibility = Visibility.Collapsed; 
                btnReturn.Visibility = Visibility.Collapsed; 
                viewBoxCombo.Visibility = Visibility.Collapsed;
                viewBoxEnter2.Visibility = Visibility.Visible; 
                viewBoxReturn2.Visibility = Visibility.Visible;
                viewBoxComboMangaType.Visibility = Visibility.Visible;
                comboBoxMangaType.ItemsSource = Enum.GetValues(typeof(genre)); 
                title.Text = $"Please choose a new Manga Type:";
            }
            else
            {

                btnEnter.Visibility = Visibility.Collapsed; 
                btnReturn.Visibility = Visibility.Collapsed;
                viewBoxCombo.Visibility = Visibility.Collapsed;
                viewBoxEnter2.Visibility = Visibility.Visible;
                viewBoxReturn2.Visibility = Visibility.Visible; 
                viewBoxTextBox.Visibility = Visibility.Visible;
                string boundText = "";
                for (int i = 0; i < comboBox.SelectedItem.ToString()!.Length; i++)
                {
                    if (Char.IsLetter(comboBox.SelectedItem.ToString()![i])) boundText += comboBox.SelectedItem.ToString()![i];
                    else break;
                }
                editTextBox.BoundText = $"Enter new {boundText}"; title.Text = $"Please choose a new {boundText}";
            }//метод перехода в меню изменения выбранной опции
        }
        private void ReturnToOptionSelectionMenu() //метод возврата в меню выбора опции
        {
            if (comboBox.SelectedIndex == 6)
            {
                HideGenreComboBox();
            }
            btnEnter.Visibility = Visibility.Visible;
            btnReturn.Visibility = Visibility.Visible;
            viewBoxCombo.Visibility = Visibility.Visible;
            viewBoxTextBox.Visibility = Visibility.Collapsed;
            viewBoxReturn2.Visibility = Visibility.Collapsed;
            viewBoxEnter2.Visibility = Visibility.Collapsed;
            editTextBox.txtInput.Text = string.Empty;
            comboBox.SelectedItem = null;
            comboBox.Text = "Option";
            title.Text = "Please choose the option you would like to edit:";
        }
        private void HideGenreComboBox() //метод скрытия блока жанра
        {
            viewBoxComboMangaType.Visibility = Visibility.Collapsed;
            comboBoxMangaType.SelectedItem = null;
            comboBoxMangaType.Text = "Manga Type";
        }

        private void ReturnToManagerMenu() //метод возврата в меню менеджера
        {
            Window mainWindow = Window.GetWindow(this);
            Grid editMangaGrid2 = (Grid)mainWindow.FindName("editMangaGrid2");
            editMangaGrid2.Visibility = Visibility.Collapsed;
            Grid managerGrid = (Grid)mainWindow.FindName("managerGrid");
            managerGrid.Visibility = Visibility.Visible;
        }
        private void ReturnToFirstMenu()
        {
            Window mainWindow = Window.GetWindow(this);
            Grid editMangaGrid2 = (Grid)mainWindow.FindName("editMangaGrid2");
            editMangaGrid2.Visibility = Visibility.Collapsed;

            Grid editMangaGrid = (Grid)mainWindow.FindName("editMangaGrid");
            editMangaGrid.Visibility = Visibility.Visible;
            comboBox.SelectedValue = null;
            comboBox.Text = "Option";
            comboBox.Foreground = Brushes.DarkGray;
        }
        private void EditIsbn()
        {
            string oldIsbn = manga.Isbn;
            try
            {
                manga.IsIsbnValid(editTextBox.txtInput.Text);
                DataBase.IsIsbnAvailable(editTextBox.txtInput.Text);
                manga.Isbn = editTextBox.txtInput.Text;
                DataBase.SaveItemInformation(manga);
                MessageBox.Show("The ISBN of the manga has been successfully changed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ReturnToOptionSelectionMenu();
                ReturnToManagerMenu();
            }
            catch (IllegalIsbnException ex)
            {
                ErrorMessage(ex);
                editTextBox.txtInput.Focus();
            }
            catch (DirectoryNotFoundException ex)
            {
                ErrorMessage(ex);
                manga.Isbn = oldIsbn;
                editTextBox.txtInput.Focus();
            }
        }
        private void ErrorMessage(Exception ex)
        {
            DataBase.LogException(ex);
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EditName()
        {
            if (string.IsNullOrWhiteSpace(editTextBox.txtInput.Text))
            {
                MessageBox.Show("Field is empty!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                editTextBox.txtInput.Focus();
            }
            else
            {
                string oldName = manga.Name;
                try
                {
                    manga.Name = editTextBox.txtInput.Text;
                    DataBase.SaveItemInformation(manga);
                    MessageBox.Show("The Name of the manga has been successfully changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ReturnToOptionSelectionMenu();
                    ReturnToManagerMenu();
                }
                catch (DirectoryNotFoundException ex)
                {
                    DataBase.LogException(ex);
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    manga.Name = oldName;
                    editTextBox.txtInput.Focus();
                }
            }
        }
        private void EditEdition() //Редактировать издание
        {
            if (string.IsNullOrWhiteSpace(editTextBox.txtInput.Text))
            {
                MessageBox.Show("Field is empty!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                editTextBox.txtInput.Focus();
            }
            else
            {
                string oldEdition = manga.Edition;
                try
                {
                    manga.Edition = editTextBox.txtInput.Text;
                    DataBase.SaveItemInformation(manga);
                    MessageBox.Show("The Edition of the manga has been successfully changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ReturnToOptionSelectionMenu();
                    ReturnToManagerMenu();
                }
                catch (DirectoryNotFoundException ex)
                {
                    DataBase.LogException(ex);
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    manga.Edition = oldEdition;
                    editTextBox.txtInput.Focus();
                }
            }
        }
        private void EditQuantity()
        {
            int oldQuantity = manga.Quantity;
            try
            {
                manga.IsQuantityInt(editTextBox.txtInput.Text);
                DataBase.SaveItemInformation(manga);
                MessageBox.Show("The Edition of the manga has been successfully changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ReturnToOptionSelectionMenu();
                ReturnToManagerMenu();
            }
            catch (FormatException ex)
            {
                DataBase.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                editTextBox.txtInput.Focus();
            }
            catch (DirectoryNotFoundException ex)
            {
                DataBase.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                manga.Quantity = oldQuantity;
                editTextBox.txtInput.Focus();
            }
        }

        private void EditPrice()
        {
            double oldPrice = manga.Price;
            try
            {
                manga.IsPriceDouble(editTextBox.txtInput.Text);
                DataBase.SaveItemInformation(manga);
                MessageBox.Show("The Price of the manga has been successfully changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ReturnToOptionSelectionMenu();
                ReturnToManagerMenu();
            }
            catch (FormatException ex)
            {
                DataBase.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                editTextBox.txtInput.Focus();
            }
            catch (DirectoryNotFoundException ex)
            {
                DataBase.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                manga.Price = oldPrice;
                editTextBox.txtInput.Focus();
            }
        }
        private void EditChapterCount()
        {
            double oldChapters = manga.chapterCount;
            try
            {
                manga.IsChaptersDouble(editTextBox.txtInput.Text);
                DataBase.SaveItemInformation(manga);
                MessageBox.Show("The chapters count of the manga has been successfully changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ReturnToOptionSelectionMenu();
                ReturnToManagerMenu();
            }
            catch (FormatException ex)
            {
                DataBase.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                editTextBox.txtInput.Focus();
            }
            catch (DirectoryNotFoundException ex)
            {
                DataBase.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                manga.chapterCount = oldChapters;
                editTextBox.txtInput.Focus();
            }
        }
        private void EditMangaType()
        {
            if(comboBoxMangaType.SelectedItem != null)
            {
                MangaType oldMangaType = manga.mangaType;
                try
                {
                    manga.mangaType = (MangaType)comboBoxMangaType.SelectedItem;
                    DataBase.SaveItemInformation(manga);
                    MessageBox.Show("The type of the manga has been successfully changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ReturnToOptionSelectionMenu();
                    ReturnToManagerMenu();
                }
                catch (DirectoryNotFoundException ex)
                {
                    DataBase.LogException(ex);
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    manga.mangaType = oldMangaType;
                    editTextBox.txtInput.Focus();
                }
            }
            else
            {
                MessageBox.Show("Please pick a type of manga!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void comboBox_DropDownOpened(object sender, EventArgs e) //Раскрытие меню выбора
        {
            comboBox.Foreground = Brushes.Black;
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e) //закрытие списка выбора
        {
            if (comboBox.SelectedItem != null) comboBox.Foreground = Brushes.Black;
            else comboBox.Foreground = Brushes.DarkGray;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор 
        {
            comboBox.Foreground = Brushes.Black;
        }

        private void comboBoxMangaType_SelectionChanged(object sender, SelectionChangedEventArgs e) // выбор жанра
        {
            comboBoxMangaType.Foreground = Brushes.Black;
        }

        private void comboBoxMangaType_DropDownOpened(object sender, EventArgs e) //раскрытие меню выбора жанра
        {
            comboBoxMangaType.Foreground = Brushes.Black;
        }

        private void comboBoxMangaType_DropDownClosed(object sender, EventArgs e) //закрытие меню выбора жанра
        {
            if (comboBoxMangaType.SelectedItem != null) comboBoxMangaType.Foreground = Brushes.Black;
            else comboBoxMangaType.Foreground = Brushes.DarkGray;
        }
    }
}

