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
                        output.PrintSearched(searchResults);
                        Console.WriteLine("\nKlicka vart som helst för att återgå till huvudmenyn!");
                        Console.ReadKey();
                        break;
                    case "4":
                        output.PrintLoaned(loanedBooks);
                        Console.WriteLine("\nKlicka vart som helst för att återgå till huvudmenyn!");
                        Console.ReadKey();
                        break;
                    case "5":
                        output.PrintBooks(bookList);
                        Console.WriteLine("\nKlicka vart som helst för att återgå till huvudmenyn!");
                        Console.ReadKey();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    case "7":
                        book.EditBook();
                        break;
                    default:
                        Console.WriteLine("Felaktig inmatning, försök igen!");
                        break;
                }

            }
        }
        void SaveLoaned()
            {

                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;

                using (StreamWriter sw = new StreamWriter(@"C:\Users\Simon\Documents\VISUAL STUDIO\FotbollsTestREWORKED\Library\Library\loaneddata.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, loaned);
                }

            }

            void LoadLoaned()
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;


                using (StreamReader file = File.OpenText(@"C:\Users\Simon\Documents\VISUAL STUDIO\FotbollsTestREWORKED\Library\Library\loaneddata.json"))
                {
                    loaned = JsonConvert.DeserializeObject<List<Bok>>(File.ReadAllText(@"C:\Users\Simon\Documents\VISUAL STUDIO\FotbollsTestREWORKED\Library\Library\loaneddata.json"));
                    serializer = new JsonSerializer();
                    loaned = (List<Bok>)serializer.Deserialize(file, typeof(List<Bok>));
                }

            }

        

    }
}
