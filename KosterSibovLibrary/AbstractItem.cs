using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KosterSibovLibrary
{
    public abstract class AbstractItem
    {
        public string Isbn { get; set; }
        public string Name { get; set; }
        public string Edition { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime DateOfPrint { get; set; }

        public AbstractItem(string isbn, string name, string edition, int quantity, double price)
        {
            Isbn = isbn;
            Name = name;
            Edition = edition;
            Quantity = quantity;
            Price = price;
            DateOfPrint = DateTime.Now;
        }
        public AbstractItem()
        {

        }

        public abstract void IsFormValid();

        public void IsPriceDouble(string price)
        {
            if (!double.TryParse(price, out double parsedPrice) || parsedPrice <= 0 || parsedPrice > 9999)
                throw new FormatException("The price must be a positive number and no more than 9999!");
            Price = parsedPrice;
        }

        public void IsQuantityInt(string quantity)
        {
            if (!int.TryParse(quantity, out int parsedQuantity) || parsedQuantity < 1 || parsedQuantity > 500)
                throw new FormatException("The Quantity must be a whole positive number and no more than 500!");
            Quantity = parsedQuantity;
        }

        public void IsIsbnValid(string isbn)
        {
            string isbnPattern = @"^\d{6}$";
            Regex IsIsbnValid = new Regex(isbnPattern);

            if (!IsIsbnValid.IsMatch(isbn)) throw new IllegalIsbnException("The ISBN must be exaclty 6 digits!");
        }

        public void SubstractItemFromLibrary()
        {
            if (Quantity == 0)
                throw new ItemOutOfStockExceptions($"{Name} is out of stock!");
            Quantity--;
        }

        public string PartialToString()
        {
            if (GetType() == typeof(Book))
            {
                return $"ISBN: {Isbn} | Name: {Name} | Price: {Price:C} | Book";
            }
            else if (GetType() == typeof(Journal))
            {
                return $"ISBN: {Isbn} | Name: {Name} | Price: {Price:C} | Journal";
            }
            else
            {
                return $"ISBN: {Isbn} | Name: {Name} | Price: {Price:C} | Manga";
            }
        }
    }
}
