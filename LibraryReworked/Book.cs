using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LibraryReworked;

namespace Books
{
    public class Book 
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool LoanedStatus { get; set; }

        Output output = new Output();

        public void EditBook()
        {
            SearchBooks();
            output.PrintSearched(Program.searchResults);

            if(Program.searchResults.Count() > 0)
            {
                
            Console.WriteLine("Vilken bok vill du redigera?");
            int bookChoice = int.Parse(Console.ReadLine());
                
                        foreach(Book book in Program.bookList)
                {
                    if(book.Title == Program.searchResults[bookChoice - 1].Title && book.Author == Program.searchResults[bookChoice - 1].Author)
                    {
                        Console.Clear();
                        Console.WriteLine($"Du valde {book.Title} av {book.Author}");
                        output.Printer(book);
                        Console.WriteLine("Vad vill du ändra?");
                        Console.WriteLine("1 - Titel");
                        Console.WriteLine("2 - Författare");
                        Console.WriteLine("3 - Titel & Författare");
                        int choice2 = int.Parse(Console.ReadLine());
                        
                        if(choice2 == 1)
                        {
                            Console.WriteLine("Vad ska den nya titeln vara?");
                            string newTitle = Console.ReadLine();

                            book.Title = newTitle;
                        }
                        if(choice2 == 2)
                        {
                            Console.WriteLine("Vad ska den nya författaren heta?");
                            string newAuthor = Console.ReadLine();
                                
                            book.Author = newAuthor;
                                
                        }
                        if(choice2 == 3)
                            {
                                Console.WriteLine("Vad ska den nya titeln vara?");
                                string newTitle = Console.ReadLine();
                                Console.WriteLine("Vad ska den nya författaren heta?");
                                string newAuthor = Console.ReadLine();
                                book.Title = newTitle;
                                book.Author = newAuthor;
                            }
                        
                    }
                }
            }
            else
            {                    Console.ReadKey();
                }
            }
        

        public void SearchBooks()
        {
            Program.searchResults.Clear();
            Console.WriteLine("Ange författaren, eller författarens namn!");
            string svar = Console.ReadLine().ToLower();

                foreach (Book x in Program.bookList)
                {
                    if (x.Author.ToLower().Contains(svar) || x.Title.ToLower().Contains(svar))
                    {
                        Program.searchResults.Add(x);
                    }
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

                Console.WriteLine("Hur vill du leta reda på en bok?");
                Console.WriteLine("1 - Sök efter en specifik bok");
                Console.WriteLine("2 - Visa alla böcker i biblioteket!");
                Console.WriteLine("3 - Avbryt");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        Console.WriteLine("Ange författaren, eller författarens namn!");
                        string svar = Console.ReadLine().ToLower();
                        Program.searchResults.Clear();
                        foreach (Book x in Program.bookList)
                        {
                            if (x.Author.ToLower().Contains(svar) || x.Title.ToLower().Contains(svar))
                            {
                                Program.searchResults.Add(x);
                            }
                        }
                        if (Program.searchResults.Count() >= 1)
                        {
                            output.PrintSearched(Program.searchResults);

                            Console.WriteLine("Skriv den siffran på den boken som du vill välja!");
                            string choice2 = Console.ReadLine();

                            int bookNum;
                            while (!int.TryParse(choice2, out bookNum))
                            {
                                Console.WriteLine("Denna bok finns inte!");
                                choice2 = Console.ReadLine();
                            }
                            if (bookNum > 0 && bookNum <= Program.searchResults.Count() && Program.searchResults[bookNum-1].LoanedStatus == false)
                            {
                                Program.searchResults[bookNum - 1].LoanedStatus = true;
                                Console.WriteLine($"Boken {Program.searchResults[bookNum-1].Title} av {Program.searchResults[bookNum-1].Author} är nu utlånad!");
                                Console.WriteLine("Klicka på vilken knapp som helst för att återgå till huvudmenyn!");

                                foreach(Book x in Program.bookList)
                                {
                                    if(x.Title == Program.searchResults[bookNum - 1].Title && x.Author == Program.searchResults[bookNum - 1].Author)
                                    {
                                        x.LoanedStatus = true;
                                        Program.loanedBooks.Add(x);
                                    }
                                }

                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                if(bookNum > 0 && bookNum <= Program.searchResults.Count() && Program.searchResults[bookNum-1].LoanedStatus == true)
                                {
                                    Console.WriteLine("Denna bok är redan utlånad!");
                                    Console.WriteLine("Klicka på vilken knapp som helst för att återgå till huvudmenyn!");
                                    Console.ReadKey();
                                break;
                                }
                                else
                                {
                                 
                                Console.WriteLine("Denna bok finns inte!");
                                Console.WriteLine("Klicka på vilken knapp som helst för att återgå till huvudmenyn!");
                                Console.ReadKey();
                                break;
                                    
                                }
                            }
                        }
                            
                                       
                            

                        break;
                    case "2":
                        output.PrintBooks(Program.bookList);
                        
                        Console.WriteLine("\nSkriv numret på den bok som du vill låna!");
                        string choice3 = Console.ReadLine();
                        
                        int bookNum2;
                        
                        while (!int.TryParse(choice3, out bookNum2))
                        {
                            Console.WriteLine("Denna bok finns inte!");
                            choice3 = Console.ReadLine();
                        }
                            
                        if (bookNum2 > 0 && bookNum2 <= Program.bookList.Count() && Program.bookList[bookNum2-1].LoanedStatus == false)
                        {
                            Program.bookList[bookNum2 - 1].LoanedStatus = true;
                            Console.WriteLine($"Boken {Program.bookList[bookNum2-1].Title} av {Program.bookList[bookNum2-1].Author} är nu utlånad!");
                            Program.loanedBooks.Add(Program.bookList[bookNum2-1]);
                            Console.WriteLine("Klicka på vilken knapp som helst för att återgå till huvudmenyn!");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Denna bok är redan utlånad!");
                            Console.WriteLine("Klicka på vilken knapp som helst för att återgå till huvudmenyn!");
                            Console.ReadKey();
                            break;
                        }
                        case "3":
                            break;
                        
                    default:
                        Console.WriteLine("Felaktig inmatning, försök igen!");
                        break;
                }
                break;
            }
            
        }
    }
}