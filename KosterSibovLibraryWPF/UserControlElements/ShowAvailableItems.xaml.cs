using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
using Microsoft.Office.Interop.Word;


namespace KosterSibovLibraryWPF.UserControlElements
{
    /// <summary>
    /// Логика взаимодействия для ShowAvailableItems.xaml
    /// </summary>
    public partial class ShowAvailableItems : UserControl
    {
        public ShowAvailableItems()
        {
            InitializeComponent();
            btnReturn.ButtonContent = Resex.btnReturn;
            btnWord.ButtonContent = Resex.btnWord;
            btnFind.ButtonContent = Resex.btnFind;
            btnRevert.ButtonContent = Resex.btnShowItems;
            btnReturn.ButtonClickEvent += BtnReturn_ButtonClickEvent;
            btnWord.ButtonClickEvent += BtnWord_ButtonClickEvent;
            btnFind.ButtonClickEvent += BtnFind_ButtonClickEvent;
            btnRevert.ButtonClickEvent += BtnRevert_ButtonClickEvent;
            DisplayItems();
        }

        private void BtnFind_ButtonClickEvent(object sender, RoutedEventArgs e) //обработчик события кнопки поиска по имени
        {
            if (textBox.txtInput.Text == string.Empty)
            {
                MessageBox.Show("Please enter a name in the text box in order to sort the list!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    DataBase.FilterItemsByName(textBox.txtInput.Text);
                    ListBox.ItemsSource = null;
                    ListBox.ItemsSource = DataBase.FilterItemsByName(textBox.txtInput.Text);
                }
                catch (System.ArgumentException ex)
                {
                    DataBase.LogException(ex);
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void BtnRevert_ButtonClickEvent(object sender, RoutedEventArgs e) //обработчик события обновления списка
        {
            DisplayItems();
        }

        public void DisplayItems() //метод отображения товаров
        {
            ListBox.ItemsSource = null;
            ListBox.ItemsSource = DataBase.GetItems();
        }

        private void BtnReturn_ButtonClickEvent(object sender, RoutedEventArgs e) //обработчик события кнопки возврата
        {
            System.Windows.Window mainWindow = System.Windows.Window.GetWindow(this);
            Grid managerGrid = (Grid)mainWindow.FindName("managerGrid");
            managerGrid.Visibility = Visibility.Visible;
            Grid ShowAvailableItemsGrid = (Grid)mainWindow.FindName("ShowAvailableItemsGrid");
            ShowAvailableItemsGrid.Visibility = Visibility.Collapsed;
        }

        private void BtnWord_ButtonClickEvent(object sender, RoutedEventArgs e) // обработчик события кнопки вывода информации в Word
        {
            try
            {
                Microsoft.Office.Interop.Word.Application receiptsWordApp = new Microsoft.Office.Interop.Word.Application();
                receiptsWordApp.Visible = true;
                Microsoft.Office.Interop.Word.Document doc = receiptsWordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Content;
                range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                range.InsertAfter(title.Text + "\n\n");
                List<AbstractItem> items = DataBase.GetItems();
                foreach (AbstractItem item in items)
                {
                    range.InsertAfter(item.ToString() + "\n\n");
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                DataBase.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
