using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books;
using Newtonsoft.Json.Serialization;
using Newtonsoft;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.Xml.Schema;
using Newtonsoft.Json.Converters;

namespace LibraryReworked
{
    internal class Program
    {
        public static List<Book> bookList = new List<Book>();
        public static List<Book> searchResults = new List<Book>();
        public static List<Book> loanedBooks = new List<Book>();
        static void Main(string[] args)
        {

            //Save(); Första gången du startar programmet måste du skriva detta. Detta för att skapa filerna.

            void SaveLoaned()
            {

                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;

                using (StreamWriter sw = new StreamWriter(@"loaneddata.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, loanedBooks);
                }

            }

             void LoadLoaned()
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;


                using (StreamReader file = File.OpenText(@"loaneddata.json"))
                {
                    loanedBooks = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(@"loaneddata.json"));
                    serializer = new JsonSerializer();
                    loanedBooks = (List<Book>)serializer.Deserialize(file, typeof(List<Book>));
                }

            }
             void SaveLibrary()
            {

                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;

                using (StreamWriter sw = new StreamWriter(@"librarydata.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, bookList);
                }

            }

             void LoadLibrary()
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;


                using (StreamReader file = File.OpenText(@"librarydata.json"))
                {
                    bookList = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(@"librarydata.json"));
                    serializer = new JsonSerializer();
                    bookList = (List<Book>)serializer.Deserialize(file, typeof(List<Book>));
                }

            }

             void Save()
            {
                {
                    SaveLibrary();
                    SaveLoaned();
                }
            }

            void Load()
            {
                LoadLibrary();
                LoadLoaned();
            }
            while (true)
            {
                Console.Clear();
                Load();
                Console.WriteLine("Biblioteket innehåller {0} böcker", bookList.Count());
                Console.WriteLine("Välkommen till biblioteket!");
                Console.WriteLine("Vad vill du göra?");
                Console.WriteLine("1. Lägg till en ny bok");
                Console.WriteLine("2. Låna en bok");
                Console.WriteLine("3. Sök efter en bok");
                Console.WriteLine("4. Se alla lånade böcker");
                Console.WriteLine("5. Se alla böcker");
                Console.WriteLine("6. Redigera bok");
                Console.WriteLine("7. Avsluta programmet!");
                Console.Write("Välj ett alternativ: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                Book book = new Book();
                Output output = new Output();

                switch (choice)
                {
                    case "1":
                        book.NewBook();
                        Save();
                        break;
                    case "2":
                        book.LoanBook();
                        Save();
                        break;
                    case "3":
                        book.SearchBooks();
                        output.PrintSearched(searchResults);
                        Console.WriteLine("\nKlicka vart som helst för att återgå till huvudmenyn!");
                        Save();
                        Console.ReadKey();
                        break;
                    case "4":
                        output.PrintLoaned(loanedBooks);
                        Console.WriteLine("\nKlicka vart som helst för att återgå till huvudmenyn!");
                        Save();
                        Console.ReadKey();
                        break;
                    case "5":
                        output.PrintBooks(bookList);
                        Console.WriteLine("\nKlicka vart som helst för att återgå till huvudmenyn!");
                        Save();
                        Console.ReadKey();
                        break;
                    case "6":
                        book.EditBook();
                        Save();
                        break;
                    case "7":
                        Environment.Exit(0);
                        Save();
                        break;
                    default:
                        Console.WriteLine("Felaktig inmatning, försök igen!");
                        break;
                }

            }
        }


    }
}
