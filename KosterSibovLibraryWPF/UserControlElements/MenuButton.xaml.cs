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
    /// Логика взаимодействия для MenuButton.xaml
    /// </summary>
    public partial class MenuButton : UserControl
    {
        public MenuButton()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register("ButtonContent", typeof(string), typeof(MenuButton), new PropertyMetadata("Button")); //регистрация зависимости значения кнопки
        public string ButtonContent //поле для присвоения значения кнопке
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }
        public event RoutedEventHandler ButtonClickEvent; //определение обработчика событий Ссылок: 1
        private void btnInput_Click(object sender, RoutedEventArgs e) //обработчик событий кнопки
        {
            ButtonClickEvent?.Invoke(this, e);
        }
    }
}
