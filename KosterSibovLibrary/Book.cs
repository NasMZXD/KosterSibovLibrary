using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KosterSibovLibrary
{
    public class Book : AbstractItem
    {
        public string? Summary { get; set; }
        public genre Genre { get; set; }
        public Book(string isbn, string name, string edition, int Quantity, string summary, genre genre, double Price)
            : base(isbn, name, edition, Quantity, Price)
        {
            Summary = summary;
            Genre = genre;
        }

        public Book()
        {

        }

        public override void IsFormValid()
        {
            if(String.IsNullOrWhiteSpace(Isbn) ||
               String.IsNullOrWhiteSpace(Name) ||
               String.IsNullOrWhiteSpace(Edition) ||
               String.IsNullOrWhiteSpace(Quantity.ToString()) ||
               String.IsNullOrWhiteSpace(Price.ToString()) ||
               String.IsNullOrWhiteSpace(Summary) ||
               String.IsNullOrWhiteSpace(Genre.ToString()))
            {
                throw new ArgumentNullException("One or more fields are not set!");
            }
        }

        public override string ToString()
        {
            return $"Book | ISBN: {Isbn} | Name: {Name} | Date of Print: {DateOfPrint} | Edition: {Edition} |" +
                $"Quantity in Shop: {Quantity} | Price: {Price:C} | Summary: {Summary} | Genre: {Genre}"; 
        }
    }
}
