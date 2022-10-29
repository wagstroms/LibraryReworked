using System;
using System.Collections.Generic;
using System.Linq;
using Books;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Converters;


namespace LibraryReworked
{
    


    internal class Program
    {
        //skapa listorna som kommer användas för att spara lånade böcker, sökresultat och alla böcker.
        public static List<Book> bookList = new List<Book>();
        public static List<Book> searchResults = new List<Book>();
        public static List<Book> loanedBooks = new List<Book>();
        static void Main(string[] args)
        {

            //Save(); Första gången du startar programmet måste du skriva detta. Detta för att skapa filerna.

            void SaveLoaned() //sprarar listan loanedbooks.
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

             void LoadLoaned() //laddar in datan från den sparade json filen med lånade böcker loanedbooks
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
             void SaveLibrary() //sprarar alla böcker i biblioteket
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

             void LoadLibrary() //laddar in alla böcker i biblioteket.
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

             void Save() //multifunktionellt kommando som kör två funktioner.
            {
                {
                    SaveLibrary();
                    SaveLoaned();
                }
            }

            void Load() //samma som ovan fast laddar in.
            {
                LoadLibrary();
                LoadLoaned();
            }

            
            while (true) //startar huvudmenyn, loopar för enkel åtkomlighet. kan annars göras som egen funktion och runnas vid slutet av alla underfunktioner.
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
                //nu använder vi objekten för att kunna referera till de olika metoder och funktioner inom de diverse klasserna.
                switch (choice) //olika cases beroende på vad användern ger för input.
                {
                    case "1":
                        book.NewBook();
                        Save();
                        break; // kör newbook och sparar sedan den nya boken efter dess skapelse
                    case "2":
                        book.LoanBook();
                        Save();
                        break; //kör loanbook och sparar sedan ner den lånade boken samt sparar uppdateringen av book.LoanedStatus i båda biblioteken.
                    case "3":
                        book.SearchBooks(); //kör först sökfunktionen där den låter användaren söka efter en bok beroende på författare & titel.
                        output.PrintSearched(searchResults); // skriver ut de böcker som hittades genom searchbooks() och som då blivit sparade i listan searchresults.
                        Console.WriteLine("\nKlicka vart som helst för att återgå till huvudmenyn!");
                        Save(); //sparar data, men denna behövs inte här då det inte finns något nytt att spara.
                        Console.ReadKey();
                        break;
                    case "4":
                        output.PrintLoaned(loanedBooks); //skriver ut ala böcker som du har lånat dvs alla böcker i listan loanedbooks.
                        Console.WriteLine("\nKlicka vart som helst för att återgå till huvudmenyn!");
                        Save(); //sparar data, men denna behövs inte här då det inte finns något nytt att spara.
                        Console.ReadKey();
                        break;
                    case "5":
                        output.PrintBooks(bookList); //skriver ut alla böcker i biblioteket enligt format i printbooks.
                        Console.WriteLine("\nKlicka vart som helst för att återgå till huvudmenyn!");
                        Save();//onödigt call.
                        Console.ReadKey();
                        break;
                    case "6":
                        book.EditBook(); //Påbörjar edit book funktionen, låter användaren välja en bok genom 2 metoder, sedan kan användaren ändra 2 olika saker samt allt i en bok.
                        Save(); // viktigt att spara den nya datan.
                        break;
                    case "7":
                        Environment.Exit(0); //stänger ner programmet.
                        Save();
                        break;
                    default:
                        Console.WriteLine("Felaktig inmatning, försök igen!");
                        break; //om inte inputten matchar någon av "casen" så kommer detta felmeddelande. men i och med att den breakar ut direkt utan någon Console.ReadKey() så kommer inte användaren se detta meddelande utan direkt att skriva in något nytt.
                }

            }
        }


    }
}
