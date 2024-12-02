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
using Microsoft.Office.Interop.Word;

namespace KosterSibovLibraryWPF.UserControlElements
{
    /// <summary>
    /// Логика взаимодействия для Receipts.xaml
    /// </summary>
    public partial class Receipts : UserControl
    {
        public Receipts()
        {
            InitializeComponent();
            btnReturn.ButtonContent = Resex.btnReturn;
            btnWord.ButtonContent = Resex.btnWord;

            btnReturn.ButtonClickEvent += BtnReturn_ButtonClickEvent; 
            btnWord.ButtonClickEvent += BtnWord_ButtonClickEvent; 
            DisplayReceipts();
        }
        private void BtnWord_ButtonClickEvent(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                wordApp.Visible = true;
                Document doc = wordApp.Documents.Add();
                Microsoft.Office.Interop.Word.Range range = doc.Content;
                range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                range.InsertAfter(title.Text + "\n\n"); List<string> items = DataBase.GetReceipts();
                foreach (string item in items)
                {
                    range.InsertAfter(item.ToString() + "\n\n");
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            }
            catch (DirectoryNotFoundException ex)
            {
                DataBase.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnReturn_ButtonClickEvent(object sender, RoutedEventArgs e)
        {
            ReturnToMenu();
        }

        private void ReturnToMenu()
        {
            System.Windows.Window mainWindow = System.Windows.Window.GetWindow(this); 
            Grid ReceiptsGrid = (Grid)mainWindow.FindName("ReceiptsGrid");
            ReceiptsGrid.Visibility = Visibility.Collapsed;
            Grid managerGrid = (Grid)mainWindow.FindName("managerGrid"); 
            managerGrid.Visibility = Visibility.Visible;
        }

        public void DisplayReceipts()
        {
            try
            {
                listBox.ItemsSource = null;
                listBox.ItemsSource = DataBase.GetReceipts();
            }
            catch (DirectoryNotFoundException ex)
            {
                DataBase.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
