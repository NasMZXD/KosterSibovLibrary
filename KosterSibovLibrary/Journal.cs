using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KosterSibovLibrary
{
    public class Journal : AbstractItem //определение класса журналов, наследуемого от абстрактоного класса
    {
        public Journal(string Isbn, string Name, string Edition, int Quantity, double Price)
        : base(Isbn, Name, Edition, Quantity, Price) //создание конструктора журнала с базовыми параметрами 
        {

        }
        public Journal() : base() // создание пустого конструктора для возможного обновления параметров
        { 

        }
        public override void IsFormValid() //проверка на валидность заполнения данных о журнале
        {
            if (string.IsNullOrWhiteSpace(Isbn) ||
            String.IsNullOrWhiteSpace(Name) ||
            String.IsNullOrWhiteSpace(Edition) ||
            String.IsNullOrWhiteSpace(Price.ToString()) ||
            String.IsNullOrWhiteSpace(Quantity.ToString()))
            {
                throw new ArgumentNullException("One or more fields are not set!");
            }
        }
        public override string ToString() //перезапись метода для вывода необходимой информации
        {
            return $"Journal | ISBN: {Isbn} | Name: {Name} | Date of Print: {DateOfPrint} Edition: {Edition} | " +
            $" Quantity in Shop: {Quantity} | Price: {Price:C}";
        }
    }
}
