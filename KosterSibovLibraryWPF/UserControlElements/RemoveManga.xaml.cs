using KosterSibovLibrary;
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

namespace KosterSibovLibraryWPF.UserControlElements
{
    /// <summary>
    /// Логика взаимодействия для RemoveManga.xaml
    /// </summary>
    public partial class RemoveManga : UserControl
    {
        Manga manga = new Manga();
        public RemoveManga()
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
                    manga = (Manga)DataBase.FindItem(isbnBox.txtInput.Text);
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to remove the manga: {manga.Name}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        DataBase.RemoveItem(isbnBox.txtInput.Text);
                        DataBase.DeleteFile(manga);
                        manga = new Manga();
                        MessageBox.Show("The manga has been successfully removed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        ReturnToManagerMenu();
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
                    DataBase.AddItem(manga);
                    isbnBox.txtInput.Focus();
                }
                catch (InvalidCastException ex)
                {
                    DataBase.LogException(ex);
                    MessageBox.Show("The ISBN you entered belongs to a journal!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    isbnBox.txtInput.Focus();
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
            Grid RemoveMangaGrid = (Grid)mainWindow.FindName("RemoveMangaGrid");
            RemoveMangaGrid.Visibility = Visibility.Collapsed;
            Grid managerGrid = (Grid)mainWindow.FindName("managerGrid");
            managerGrid.Visibility = Visibility.Visible;
            isbnBox.txtInput.Text = string.Empty;
        }
    }
}
