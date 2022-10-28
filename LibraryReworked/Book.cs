using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryReworked;

namespace Books
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool LoanedStatus { get; set; }

       

        public void SearchBooks()
        {
            Console.WriteLine("Ange författaren, eller författarens namn!");
            string svar = Console.ReadLine().ToLower();

                foreach (Book x in Program.bookList)
                {
                    if (x.Author.ToLower().Contains(svar) || x.Title.ToLower().Contains(svar))
                    {
                        Program.searchResults.Add(x);
                    }

                    
                    
                    Console.WriteLine("\nKlicka vart som helst för att återgå till huvudmenyn!");
                    Console.ReadKey();
                }
            if(Program.searchResults.Count() < 1)
            {
                Console.WriteLine("Inga böcker hittades!");
                Console.WriteLine("Klicka på vilken knapp som helst för att återgå till huvudmenyn!");
                Console.ReadKey();
            }
        }
        
        
        void CreateBook(string title, string author)
        {
            Book newBook = new Book();
            newBook.Title = title;
            newBook.Author = author;
            newBook.LoanedStatus = false;

            Program.bookList.Add(newBook);
        }
        public void NewBook()
        {
            Console.Write("Vad heter den nya boken? : ");
            string newTitle = Console.ReadLine();
            Console.Write("\nVad heter författaren? : ");
            string newAuthor = Console.ReadLine();

            CreateBook(newTitle, newAuthor);
        }
        public void LoanBook()
        {
            while (true)
            {

                switch (choice)
                {
                    case "1":
                        break;
                    case "2":
                        book.LoanBook();
                        break;
                    default:
                        Console.WriteLine("Felaktig inmatning, försök igen!");
                        break;
                }
            }
            
        }
    }
}
