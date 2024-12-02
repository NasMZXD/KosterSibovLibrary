using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KosterSibovLibrary
{
    public class Manga : AbstractItem
    {
        public double chapterCount { get; set; }
        public MangaType mangaType { get; set; }
        public Manga(string isbn, string name, string edition, int Quantity, double ChapterCount, MangaType MangaType_, double Price)
            : base(isbn, name, edition, Quantity, Price)
        {
            chapterCount = ChapterCount;
            mangaType = MangaType_;
        }

        public Manga()
        {

        }

        public override void IsFormValid()
        {
            if (String.IsNullOrWhiteSpace(Isbn) ||
               String.IsNullOrWhiteSpace(Name) ||
               String.IsNullOrWhiteSpace(Edition) ||
               String.IsNullOrWhiteSpace(Quantity.ToString()) ||
               String.IsNullOrWhiteSpace(Price.ToString()) ||
               String.IsNullOrWhiteSpace(chapterCount.ToString()) ||
               String.IsNullOrWhiteSpace(mangaType.ToString()))
            {
                throw new ArgumentNullException("One or more fields are not set!");
            }
        }

        public void IsChaptersDouble(string chapterCount_)
        {
            if (!double.TryParse(chapterCount_, out double parsedChapterCount) || parsedChapterCount < 1 || parsedChapterCount > 500)
                throw new FormatException("The Chapter count must be a whole positive number and no more than 500!");
            chapterCount = parsedChapterCount;
        }

        public override string ToString()
        {
            return $"Manga | ISBN: {Isbn} | Name: {Name} | Date of Print: {DateOfPrint} | Edition: {Edition} |" +
                $"Quantity in Shop: {Quantity} | Price: {Price:C} | Chapter Count: {chapterCount} | Type of Manga: {mangaType}";
        }
    }
}
