using Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryReworked
{
    internal class Output
    {
        
        public void Printer(Book bok) // Skriver ut specifierad objekt Book som argument vid användning av funktion.
        {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Titel: " + bok.Title + " | Författare: " + bok.Author + " | Lånad: " + bok.LoanedStatus);
                Console.WriteLine("---------------------------------------------");
        }

        public void PrintSearched(List<Book> searchResults) //Skriver ut alla böcker i listan searchresults.  
        {
            int bookCounter = 0;

            foreach (Book x in searchResults)
            {
                Console.WriteLine($"--{bookCounter + 1}/{searchResults.Count}----------------------------------------");
                Console.WriteLine("Titel: " + x.Title + " | Författare: " + x.Author + " | Lånad: " + x.LoanedStatus);
                Console.WriteLine("---------------------------------------------");
                bookCounter++;
            }
        }

        

        public void PrintLoaned(List<Book> loanedBooks) //Skriver ut alla böcker i listan loanedbooks dvs lånade böcker.
        {
            int bookCounter = 0;

            if (loanedBooks.Count() < 1)
            {
                Console.WriteLine("Det finns inga lånade böcker!");

            }
            else
            {
                foreach (Book x in loanedBooks)
                {
                    Console.WriteLine($"--{bookCounter + 1}/{loanedBooks.Count}----------------------------------------");
                    Console.WriteLine("Titel: " + x.Title + " | Författare: " + x.Author + " | Lånad: " + x.LoanedStatus);
                    Console.WriteLine("---------------------------------------------");
                    bookCounter++;
                }
            }
        }
        public void PrintBooks(List<Book> bookList) //SKriver ut alla objekt book i listan booklist.
        {
            int bookCounter = 0;


            if (bookList.Count() < 1)
            {
                Console.WriteLine("Det finns inga böcker i biblioteket!");
            }
            else
            {
                foreach (Book book in bookList)
                {
                    Console.WriteLine($"--{bookCounter + 1}/{bookList.Count}----------------------------------------");
                    Console.WriteLine("Titel: " + book.Title + " | Författare: " + book.Author + " | Lånad: " + book.LoanedStatus);
                    Console.WriteLine("---------------------------------------------");
                    bookCounter++;
                }
            }
        }
    }
}
