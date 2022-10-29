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
        //här skapar vi templaten för alla böcker som vi kommer att skapa och som finns i biblioteket.
        Output output = new Output();

        public void EditBook() //redigera böcker som finns i biblioteket (bookList) listan.
        {
            SearchBooks(); //sök efter bok.
            output.PrintSearched(Program.searchResults); //skriv ut alla hittade böcker.

            if(Program.searchResults.Count() > 0) // om den searchbooks hittar några böcker. > 0
            {
                
            Console.WriteLine("Vilken bok vill du redigera?");
            int bookChoice = int.Parse(Console.ReadLine()); //här behövs det en validator för om inputten som använder ger funkar. om den är utanför index eller inte ens en siffra kraschar den, Kan nog göras med try catch. eller en while loop.
                
                        foreach(Book book in Program.bookList) //kollar om boken som är vald finns i biblioteket (vilket den gör självklart) och sedan tar den den boken och redigeras dess information istället för den i den temporära lista (searchresults)
                {
                    if(book.Title == Program.searchResults[bookChoice - 1].Title && book.Author == Program.searchResults[bookChoice - 1].Author)  //väljer den boken som sagt ovan.
                    {
                        Console.Clear();
                        Console.WriteLine($"Du valde {book.Title} av {book.Author}");
                        output.Printer(book);
                        Console.WriteLine("Vad vill du ändra?");
                        Console.WriteLine("1 - Titel");
                        Console.WriteLine("2 - Författare");
                        Console.WriteLine("3 - Titel & Författare");
                        int choice2 = int.Parse(Console.ReadLine()); //här behövs det också någon form av validator för använders input så dem inte kraschar programmet. lol
                        
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
        

        public void SearchBooks() //söker efter en bok. kollar igenom alla böcker i booklist och jämför med användarens input.
        {
            Program.searchResults.Clear(); //rensar temporära listan så den inte sparar alla sökningar
            Console.WriteLine("Ange författaren, eller författarens namn!");
            string svar = Console.ReadLine().ToLower();

                foreach (Book x in Program.bookList)
                {
                    if (x.Author.ToLower().Contains(svar) || x.Title.ToLower().Contains(svar))
                    {
                        Program.searchResults.Add(x); //lägger till i searcresults (temporär lista)
                    }
                }
            if(Program.searchResults.Count() < 1) //om den inte hittar några resultat skriver den ut detta.
            {
                Console.WriteLine("Inga böcker hittades!");
                Console.WriteLine("Klicka på vilken knapp som helst för att återgå till huvudmenyn!");
                Console.ReadKey();
            }
        }

        void CreateBook(string title, string author) //för att skapa en ny bok, väldigt enkelt kommando. den tar två argument som kommer från newbook()
        {
            Book newBook = new Book();
            newBook.Title = title;
            newBook.Author = author;
            newBook.LoanedStatus = false;

            Program.bookList.Add(newBook); //lägger in den i biblioteket.
        }
        public void NewBook() //Frågar användaren frågor angående ny bok, skickar sedan titel och author till createbook för skapelse av nytt objekt.
        {
            Console.Write("Vad heter den nya boken? : ");
            string newTitle = Console.ReadLine();
            Console.Write("\nVad heter författaren? : ");
            string newAuthor = Console.ReadLine();

            CreateBook(newTitle, newAuthor); // ^^
        }
        public void LoanBook() //för att låna en bok från biblioteket och lägger in den i loanedbooks listan.
        {
            while (true)
            {

                Console.WriteLine("Hur vill du leta reda på en bok?");
                Console.WriteLine("1 - Sök efter en specifik bok");
                Console.WriteLine("2 - Visa alla böcker i biblioteket!");
                Console.WriteLine("3 - Avbryt");
                string choice = Console.ReadLine();

                switch (choice) // för att validera inputten.
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
                        } //egentligen kan detta bytas ut med en direkt metod från searchBooks() men den har extra console writelines och funktioner vilket förstör just här. SearchBooks() funktionen kan skrivas om för att användas globalt.
                        if (Program.searchResults.Count() >= 1)
                        {
                            output.PrintSearched(Program.searchResults);

                            Console.WriteLine("Skriv den siffran på den boken som du vill välja!");
                            string choice2 = Console.ReadLine();

                            int bookNum; //validerar om inputten är en siffra, kan förbättras.
                            while (!int.TryParse(choice2, out bookNum))
                            {
                                Console.WriteLine("Denna bok finns inte!");
                                choice2 = Console.ReadLine();
                            }
                            if (bookNum > 0 && bookNum <= Program.searchResults.Count() && Program.searchResults[bookNum-1].LoanedStatus == false) //kollar om inputten är validerat nummer inom rangen av listan.
                            {
                                Program.searchResults[bookNum - 1].LoanedStatus = true; //ändrar statusen för boken i fråga.
                                Console.WriteLine($"Boken {Program.searchResults[bookNum-1].Title} av {Program.searchResults[bookNum-1].Author} är nu utlånad!");
                                Console.WriteLine("Klicka på vilken knapp som helst för att återgå till huvudmenyn!");

                                foreach(Book x in Program.bookList)
                                {
                                    if(x.Title == Program.searchResults[bookNum - 1].Title && x.Author == Program.searchResults[bookNum - 1].Author)
                                    {
                                        x.LoanedStatus = true; //synkar med boken i stora bibliotekslistan.
                                        Program.loanedBooks.Add(x); //lägger till boken i dina lånade böcker.
                                    }
                                }

                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                if(bookNum > 0 && bookNum <= Program.searchResults.Count() && Program.searchResults[bookNum-1].LoanedStatus == true) //om boken redan är lånad, och inom range. om boken annars är utanför range så skriver den ut att boken inte finns och breakar.
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
                        output.PrintBooks(Program.bookList); //skriver ut alla böcker
                        
                        Console.WriteLine("\nSkriv numret på den bok som du vill låna!");
                        string choice3 = Console.ReadLine();
                        
                        int bookNum2; //välj en av böckerna i listan.
                        
                        while (!int.TryParse(choice3, out bookNum2))
                        {
                            Console.WriteLine("Denna bok finns inte!");
                            choice3 = Console.ReadLine(); //validerar inputen.
                        }
                            
                        if (bookNum2 > 0 && bookNum2 <= Program.bookList.Count() && Program.bookList[bookNum2-1].LoanedStatus == false) //kollar först om den är inom range för listans längd. sedan om den är det ändrar den loanedStatus osv.
                        {
                            Program.bookList[bookNum2 - 1].LoanedStatus = true;
                            Console.WriteLine($"Boken {Program.bookList[bookNum2-1].Title} av {Program.bookList[bookNum2-1].Author} är nu utlånad!");
                            Program.loanedBooks.Add(Program.bookList[bookNum2-1]);
                            Console.WriteLine("Klicka på vilken knapp som helst för att återgå till huvudmenyn!");
                            Console.ReadKey();
                            break;
                        }
                        else //om boken redan är lånad dvs book[x].LoanedStatus == true.
                        {
                            Console.WriteLine("Denna bok är redan utlånad!");
                            Console.WriteLine("Klicka på vilken knapp som helst för att återgå till huvudmenyn!");
                            Console.ReadKey();
                            break;
                        }
                        case "3": // om använder väljer '3', aka avbryter programmet.
                            break;
                        
                    default: //om inmatningen är ute och cyklar!
                        Console.WriteLine("Felaktig inmatning, försök igen!");
                        break;
                }
                break;
            }
            
        }
    }
}