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
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : UserControl
    {
        public Manager()
        {
            InitializeComponent();
            AssignNamesToButtons();
            AssignEventToButtons();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            Grid managerGrid = (Grid)mainWindow.FindName("managerGrid");
            if (sender == addBook)
            {
                managerGrid.Visibility = Visibility.Collapsed;
                Grid addBookGrid = (Grid)mainWindow.FindName("addBookGrid");
                addBookGrid.Visibility = Visibility.Visible;
            }
            else if (sender == addJournal)
            {
                managerGrid.Visibility = Visibility.Collapsed;
                Grid AddJournalGrid = (Grid)mainWindow.FindName("AddJournalGrid");
                AddJournalGrid.Visibility = Visibility.Visible;
            }
            else if (sender == addManga)
            {
                managerGrid.Visibility = Visibility.Collapsed;
                Grid AddMangaGrid = (Grid)mainWindow.FindName("AddMangaGrid");
                AddMangaGrid.Visibility = Visibility.Visible;
            }
            else if (sender == editBook)
            {
                managerGrid.Visibility = Visibility.Collapsed;
                Grid editBookGrid = (Grid)mainWindow.FindName("editBookGrid");
                editBookGrid.Visibility = Visibility.Visible;
            }
            else if (sender == editJournal)
            {
                managerGrid.Visibility = Visibility.Collapsed;
                Grid EditJournalGrid = (Grid)mainWindow.FindName("EditJournalGrid");
                EditJournalGrid.Visibility = Visibility.Visible;
            }
            else if (sender == editManga)
            {
                managerGrid.Visibility = Visibility.Collapsed;
                Grid EditMangaGrid = (Grid)mainWindow.FindName("EditMangaGrid");
                EditMangaGrid.Visibility = Visibility.Visible;
            }
            else if (sender == removeBook)
            {
                managerGrid.Visibility = Visibility.Collapsed;
                Grid RemoveBookGrid = (Grid)mainWindow.FindName("RemoveBookGrid");
                RemoveBookGrid.Visibility = Visibility.Visible;
            }
            else if (sender == removeJournal)
            {
                managerGrid.Visibility = Visibility.Collapsed;
                Grid RemoveJournalGrid = (Grid)mainWindow.FindName("RemoveJournalGrid");
                RemoveJournalGrid.Visibility = Visibility.Visible;
            }
            else if (sender == removeManga)
            {
                managerGrid.Visibility = Visibility.Collapsed;
                Grid RemoveMangaGrid = (Grid)mainWindow.FindName("RemoveMangaGrid");
                RemoveMangaGrid.Visibility = Visibility.Visible;
            }
            else if (sender == showAvailableBooks)
            {
                int bookCount = DataBase.BookCount();
                if (bookCount == 0)
                {
                    MessageBox.Show($"There are currently {bookCount} books available for display!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    managerGrid.Visibility = Visibility.Collapsed;
                    Grid ShowBooksGrid = (Grid)mainWindow.FindName("ShowBooksGrid");
                    ShowBooksGrid.Visibility = Visibility.Visible;
                    ShowBooks showBooksControl = (ShowBooks)ShowBooksGrid.Children[0];
                    showBooksControl.DisplayBooks();
                }
            }
            else if (sender == showAvailableJournals)
            {
                int journalCount = DataBase.JournalCount();
                if (journalCount == 0)
                {
                    MessageBox.Show("There are currently no journals available for display!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    managerGrid.Visibility = Visibility.Collapsed;
                    Grid ShowJournalsGrid = (Grid)mainWindow.FindName("ShowJournalsGrid");
                    ShowJournalsGrid.Visibility = Visibility.Visible;

                    ShowJournals showJournalsControl = (ShowJournals)ShowJournalsGrid.Children[0];
                    showJournalsControl.DisplayJournals();
                }
            }
            else if (sender == showAvailableManga)
            {
                int mangaCount = DataBase.MangaCount();
                if (mangaCount == 0)
                {
                    MessageBox.Show($"There are currently {mangaCount} Manga available for display!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    managerGrid.Visibility = Visibility.Collapsed;
                    Grid ShowMangaGrid = (Grid)mainWindow.FindName("ShowMangaGrid");
                    ShowMangaGrid.Visibility = Visibility.Visible;
                    ShowManga showMangaControl = (ShowManga)ShowMangaGrid.Children[0];
                    showMangaControl.DisplayManga();
                }
            }
            else if (sender == showAvailableItems)
            {
                int itemCount = DataBase.ItemCount();
                if (itemCount == 0)
                {
                    MessageBox.Show("There are currently no items available for display!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    managerGrid.Visibility = Visibility.Collapsed;
                    Grid ShowAvailableItemsGrid = (Grid)mainWindow.FindName("ShowAvailableItemsGrid");
                    ShowAvailableItemsGrid.Visibility = Visibility.Visible;
                    ShowAvailableItems ShowAvailableItemsControl = (ShowAvailableItems)ShowAvailableItemsGrid.Children[0];
                    ShowAvailableItemsControl.DisplayItems();
                }
            }
            else if (sender == managePurchases)
            {
                try
                {
                    DataBase.DoesReceiptsDirExist();
                    managerGrid.Visibility = Visibility.Collapsed;
                    Grid ReceiptsGrid = (Grid)mainWindow.FindName("ReceiptsGrid");
                    ReceiptsGrid.Visibility = Visibility.Visible;
                    Receipts ReceiptsControl = (Receipts)ReceiptsGrid.Children[0];
                    ReceiptsControl.DisplayReceipts();
                }
                catch (DirectoryNotFoundException ex)
                {
                    DataBase.LogException(ex);
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (sender == manageExceptions)
            {
                try
                {
                    DataBase.DoesExceptionsDirExist();
                    managerGrid.Visibility = Visibility.Collapsed;
                    Grid ManageExceptionsGrid = (Grid)mainWindow.FindName("ManageExceptionsGrid");
                    ManageExceptionsGrid.Visibility = Visibility.Visible;
                    ManageException ManageExceptionsControl = (ManageException)ManageExceptionsGrid.Children[0];
                    ManageExceptionsControl.DisplayExceptions();
                }
                catch (DirectoryNotFoundException ex)
                {
                    DataBase.LogException(ex);
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (sender == clubCustomers)
            {
                int customerCount = Customer.GetCustomersCount();
                if (customerCount == 0)
                {
                    MessageBox.Show("There are currently no customers in the club!", "ERORR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    managerGrid.Visibility = Visibility.Collapsed;
                    Grid ShowClubCustomersGrid = (Grid)mainWindow.FindName("ShowClubCustomersGrid");
                    ShowClubCustomersGrid.Visibility = Visibility.Visible;
                    ShowClubCustomers ShowClubCustomersControl = (ShowClubCustomers)ShowClubCustomersGrid.Children[0];
                    ShowClubCustomersControl.DisplayCustomersIds();
                }
            }
            else if (sender == mainmenu)
            {
                managerGrid.Visibility = Visibility.Collapsed;
                Grid mainMenu = (Grid)mainWindow.FindName("MainMenu");
                mainMenu.Visibility = Visibility.Visible;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
        private void AssignEventToButtons() //добавление названий кнопкам
        {
            addBook.ButtonClickEvent += Button_Click;
            addJournal.ButtonClickEvent += Button_Click;
            addManga.ButtonClickEvent += Button_Click;
            editBook.ButtonClickEvent += Button_Click;
            editJournal.ButtonClickEvent += Button_Click;
            editManga.ButtonClickEvent += Button_Click;
            removeBook.ButtonClickEvent += Button_Click;                                                          
            removeJournal.ButtonClickEvent += Button_Click;
            removeManga.ButtonClickEvent += Button_Click;
            showAvailableBooks.ButtonClickEvent += Button_Click;
            showAvailableJournals.ButtonClickEvent += Button_Click;
            showAvailableManga.ButtonClickEvent += Button_Click;
            showAvailableItems.ButtonClickEvent += Button_Click;
            managePurchases.ButtonClickEvent += Button_Click;
            manageExceptions.ButtonClickEvent += Button_Click;
            clubCustomers.ButtonClickEvent += Button_Click;
            mainmenu.ButtonClickEvent += Button_Click;
            exit.ButtonClickEvent += Button_Click;
        }
        private void AssignNamesToButtons() //присваивание кнопкам обрабботчика событий
        {
            addBook.ButtonContent = Resex.btnAddBook;
            addJournal.ButtonContent = Resex.btnAddJournal;
            addManga.ButtonContent = Resex.btnAddManga;
            editBook.ButtonContent = Resex.btnEditBook;
            editJournal.ButtonContent = Resex.btnEditJournal;
            editManga.ButtonContent = Resex.btnEditManga;
            removeBook.ButtonContent = Resex.btnRemoveBook;
            removeJournal.ButtonContent = Resex.btnRemoveJournal;
            removeManga.ButtonContent = Resex.btnRemoveManga;
            showAvailableBooks.ButtonContent = Resex.btnShowBooks;
            showAvailableJournals.ButtonContent = Resex.btnShowJournals;
            showAvailableManga.ButtonContent = Resex.btnShowManga;
            showAvailableItems.ButtonContent = Resex.btnShowItems;
            managePurchases.ButtonContent = Resex.btnPurchases;
            manageExceptions.ButtonContent = Resex.btnManageExceptions;
            clubCustomers.ButtonContent = Resex.btnClubCustomers;
            mainmenu.ButtonContent = Resex.btnMainMenu; 
            exit.ButtonContent = Resex.btnExit;
        }
    }
}

