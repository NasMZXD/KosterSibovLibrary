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
    /// Логика взаимодействия для EditManga.xaml
    /// </summary>
    public partial class EditManga : UserControl
    {
        public static string Isbn { get; set; }
        public EditManga()
        {
            InitializeComponent();

            btnEnter.ButtonContent = Resex.btnEnter;
            btnReturn.ButtonContent = Resex.btnReturn;
            btnEnter.ButtonClickEvent += btnInput_Click;
            btnReturn.ButtonClickEvent += btnInput_Click;
        }
        private void btnInput_Click(object sender, RoutedEventArgs e) //oбpаботчик события кнопки
        {
            if (sender == btnEnter)
            {
                try
                {
                    DataBase.IsIsbnValid(isbnBox.txtInput.Text);
                    Isbn = isbnBox.txtInput.Text;
                    ProceedToNextMenu();
                }
                catch (IllegalIsbnException ex)
                {
                    DataBase.LogException(ex);
                    MessageBox.Show(ex.Message, "ERORR", MessageBoxButton.OK, MessageBoxImage.Error);
                    isbnBox.txtInput.Focus();
                }
            }
            else
            {
                ReturnToManagerMenu();
            }
        }
        private void ReturnToManagerMenu()
        {
            Window mainWindow = Window.GetWindow(this);
            Grid editMangaGrid = (Grid)mainWindow.FindName("editMangaGrid");
            editMangaGrid.Visibility = Visibility.Collapsed;

            Grid managerGrid = (Grid)mainWindow.FindName("managerGrid");
            managerGrid.Visibility = Visibility.Visible;

            isbnBox.txtInput.Text = string.Empty;
        }

        private void ProceedToNextMenu()
        {
            Window mainWindow = Window.GetWindow(this);
            Grid editMangaGrid = (Grid)mainWindow.FindName("editMangaGrid");
            editMangaGrid.Visibility = Visibility.Collapsed;

            Grid editMangaGrid2 = (Grid)mainWindow.FindName("editMangaGrid2");
            editMangaGrid2.Visibility = Visibility.Visible;

            EditManga2 editMangaGrid2Control = (EditManga2)editMangaGrid2.Children[0];
            editMangaGrid2Control.ClearComboBox();
            editMangaGrid2Control.FindManga();
            editMangaGrid2Control.PopulateComboBox();
        }
    }
}
