using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books;

namespace LibraryReworked
{
    internal class Program
    {
        public static List<Book> bookList = new List<Book>();
        public static List<Book> searchResults = new List<Book>();
        public static List<Book> loanedBooks = new List<Book>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Välkommen till biblioteket!");
                Console.WriteLine("Vad vill du göra?");
                Console.WriteLine("1. Lägg till en ny bok");
                Console.WriteLine("2. Låna en bok");
                Console.WriteLine("3. Sök efter en bok");
                Console.WriteLine("4. Se alla lånade böcker");
                Console.WriteLine("5. Se alla böcker");
                Console.WriteLine("6. Avsluta programmet");
                Console.Write("Välj ett alternativ: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                Book book = new Book();
                Output output = new Output();
                
                switch (choice)
                {
                    case "1":
                        book.NewBook();
                        break;
                    case "2":
                        book.LoanBook();
                        break;
                    case "3":
                        book.SearchBooks();
                        break;
                    case "4":
                        output.PrintLoaned(loanedBooks);
                        break;
                    case "5":
                        output.PrintBooks(bookList);
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Felaktig inmatning, försök igen!");
                        break;
                }

            }
        }
    }
}
