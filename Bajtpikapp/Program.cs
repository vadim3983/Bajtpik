using System.Text.RegularExpressions;
using Bajtpik;
using Bajtpik.Adapters;
using Bajtpik.Bajtpik2;
using Bajtpik.Bajtpik3;

namespace Bajtpikapp;

public abstract partial class main
{
    private static readonly Data Data = new();

    private static Dictionary<string, Command>? _commands = new();

    private static void Main()
    {
        GlobalData myHashMaps = new();

        Bajtpik.ICollection<Author> doublyLinkedList = new DoublyLinkedList<Author>();

        Bajtpik.ICollection<Author> author1Heap = new Heap<Author>((a, b) =>
        {
            if (a == null || b == null)
                throw new ArgumentException("Cannot compare null values in a heap.");

            return a.BirthYear.CompareTo(b.BirthYear);
        });

        //authors1
        var author1 = new Author { Name = "Douglas", Surname = "Adams", BirthYear = 1952 };
        var author2 = new Author { Name = "Tom", Surname = "Wolfe", BirthYear = 1930 };
        var author3 = new Author { Name = "Elmar", Surname = "Eisemann", BirthYear = 1978 };
        var author4 = new Author { Name = "Michael", Surname = "Schwarz", BirthYear = 1970 };
        var author5 = new Author { Name = "Ulf", Surname = "Assarsson", BirthYear = 1975 };
        var author6 = new Author { Name = "Michael", Surname = "Wimmer", BirthYear = 1980 };
        var author7 = new Author { Name = "Frank", Surname = "Herbert", BirthYear = 1920 };
        var author8 = new Author { Name = "Terry", Surname = "Pratchett", BirthYear = 1948 };
        var author9 = new Author { Name = "Neil", Surname = "Gaiman", BirthYear = 1960 };
        var author10 = new Author { Name = "Jamey", Surname = "Stegmaier", BirthYear = 1975 };
        var author11 = new Author { Name = "Jakub", Surname = "Różalski", BirthYear = 1981, Nickname = "Mr. Werewolf" };
        var author12 = new Author { Name = "Klaus", Surname = "Teuber", BirthYear = 1952 };
        var author13 = new Author { Name = "Alfred", Surname = "Butts", BirthYear = 1899 };
        var author14 = new Author { Name = "James", Surname = "Brunot", BirthYear = 1902 };
        var author15 = new Author { Name = "Christian T.", Surname = "Petersen", BirthYear = 1970 };

        author1Heap.Add(author1);
        author1Heap.Add(author2);
        author1Heap.Add(author3);
        author1Heap.Add(author4);
        author1Heap.Add(author5);
        author1Heap.Add(author6);
        author1Heap.Add(author7);
        author1Heap.Add(author8);
        author1Heap.Add(author9);
        author1Heap.Add(author10);
        author1Heap.Add(author11);
        author1Heap.Add(author12);
        author1Heap.Add(author13);
        author1Heap.Add(author14);
        author1Heap.Add(author15);

        Console.WriteLine("Heap:");

        CollectionAlgorithms.Print(author1Heap.ForwardIterator().GetEnumerator(), _ => true, x => x?.PrintAuthor());

        Console.WriteLine("\n");


        author1Heap.Delete(author1);

        Console.WriteLine("Heap:Deleted first element(Douglas Adams):");

        CollectionAlgorithms.Print(author1Heap.ForwardIterator().GetEnumerator(), _ => true, x => x?.PrintAuthor());

        Console.WriteLine("\n");

        Console.WriteLine("Heap:Condition");

        var a = CollectionAlgorithms.CountIf(author1Heap.ForwardIterator().GetEnumerator(), x => x is { BirthYear: > 1950 });

        Console.WriteLine(a);

        Console.WriteLine("\n");

        CollectionAlgorithms.ForEach(author1Heap.ForwardIterator().GetEnumerator(), x => x?.PrintAuthor());


        Console.WriteLine("\n");
        doublyLinkedList.Add(author1);
        doublyLinkedList.Add(author2);
        doublyLinkedList.Add(author3);
        doublyLinkedList.Add(author4);
        doublyLinkedList.Add(author5);
        doublyLinkedList.Add(author6);
        doublyLinkedList.Add(author7);
        doublyLinkedList.Add(author8);
        doublyLinkedList.Add(author9);
        doublyLinkedList.Add(author10);
        doublyLinkedList.Add(author11);
        doublyLinkedList.Add(author12);
        doublyLinkedList.Add(author13);
        doublyLinkedList.Add(author14);
        doublyLinkedList.Add(author15);
        //authors2
        var myAuthor = new Author2();

        myAuthor.AddAuthor("Douglas", "Adams", 1952, null);
        myAuthor.AddAuthor("Tom", "Wolfe", 1930, null);
        myAuthor.AddAuthor("Elmar", "Eisemann", 1978, null);
        myAuthor.AddAuthor("Michael", "Schwarz", 1970, null);
        myAuthor.AddAuthor("Ulf", "Assarsson", 1975, null);
        myAuthor.AddAuthor("Michael", "Wimmer", 1980, null);
        myAuthor.AddAuthor("Frank", "Herbert", 1920, null);
        myAuthor.AddAuthor("Terry", "Pratchett", 1948, null);
        myAuthor.AddAuthor("Neil", "Gaiman", 1960, null);
        myAuthor.AddAuthor("Jamey", "Stegmaier", 1975, null);
        myAuthor.AddAuthor("Jacub", "Różalski", 1981, "Mr. Werewolf");
        myAuthor.AddAuthor("Klaus", "Teuber", 1952, null);
        myAuthor.AddAuthor("Alfred", "Butts", 1899, null);
        myAuthor.AddAuthor("James", "Brunot", 1902, null);
        myAuthor.AddAuthor("Christian T.", "Petersen", 1970, null);
        //Authors3

        var author3_1 = new Author3(myHashMaps, "Douglas", "Adams", 1952, "");
        var author3_2 = new Author3(myHashMaps, "Tom", "Wolfe", 1930, "");
        var author3_3 = new Author3(myHashMaps, "Elmar", "Eisemann", 1978, "");
        var author3_4 = new Author3(myHashMaps, "Michael", "Schwarz", 1970, "");
        var author3_5 = new Author3(myHashMaps, "Ulf", "Assarsson", 1975, "");
        var author3_6 = new Author3(myHashMaps, "Michael", "Wimmer", 1980, "");
        var author3_7 = new Author3(myHashMaps, "Frank", "Herbert", 1920, "");
        var author3_8 = new Author3(myHashMaps, "Terry", "Pratchett", 1948, "");
        var author3_9 = new Author3(myHashMaps, "Neil", "Gaiman", 1960, "");
        var author3_10 = new Author3(myHashMaps, "Jamey", "Stegmaier", 1975, "");
        var author3_11 = new Author3(myHashMaps, "Jakub", "Różalski", 1981, "Mr. Werewolf");
        var author3_12 = new Author3(myHashMaps, "Klaus", "Teuber", 1952, "");
        var author3_13 = new Author3(myHashMaps, "Alfred", "Butts", 1899, "");
        var author3_14 = new Author3(myHashMaps, "James", "Brunot", 1902, "");
        var author3_15 = new Author3(myHashMaps, "Christian", "Petersen", 1970, "");

        var authorkey1 = myHashMaps.GetAuthorKey("Douglas", "Adams", 1952);
        var authorkey2 = myHashMaps.GetAuthorKey("Tom", "Wolfe", 1930);
        var authorkey3 = myHashMaps.GetAuthorKey("Elmar", "Eisemann", 1978);
        var authorkey4 = myHashMaps.GetAuthorKey("Michael", "Schwarz", 1970);
        var authorkey5 = myHashMaps.GetAuthorKey("Ulf", "Assarsson", 1975);
        var authorkey6 = myHashMaps.GetAuthorKey("Michael", "Wimmer", 1980);
        var authorkey7 = myHashMaps.GetAuthorKey("Frank", "Herbert", 1920);
        var authorkey8 = myHashMaps.GetAuthorKey("Terry", "Pratchett", 1948);
        var authorkey9 = myHashMaps.GetAuthorKey("Neil", "Gaiman", 1960);
        var authorkey10 = myHashMaps.GetAuthorKey("Jamey", "Stegmaier", 1975);
        var authorkey11 = myHashMaps.GetAuthorKey("Jakub", "Różalski", 1981);
        var authorkey12 = myHashMaps.GetAuthorKey("Klaus", "Teuber", 1952);
        var authorkey13 = myHashMaps.GetAuthorKey("Alfred", "Butts", 1899);
        var authorkey14 = myHashMaps.GetAuthorKey("James", "Brunot", 1902);
        var authorkey15 = myHashMaps.GetAuthorKey("Christian", "Petersen", 1970);


        //books 1
        var book1 = new Book { Title = "The Hitchhiker's Guide to the Galaxy", Year = 1987, PageCount = 320 };
        var book2 = new Book { Title = "The Right Stuff", Year = 1993, PageCount = 512 };
        var book3 = new Book { Title = "Real-Time Shadows", Year = 2011, PageCount = 383 };
        var book4 = new Book { Title = "Mesjasz Diuny", Year = 1972, PageCount = 272 };
        var book5 = new Book { Title = "Dobry Omen", Year = 1990, PageCount = 416 };
        
        book1.Authors = new List<Author?> { author1 };
        book2.Authors = new List<Author?> { author2 };
        book3.Authors = new List<Author?>
        {
            author3,
            author4,
            author5,
            author6
        };
        book4.Authors = new List<Author?> { author7 };
        book5.Authors = new List<Author?>
        {
            author8,
            author9
        };

        //books 2
        var myBook = new Book2();

        myBook.AddBook("The Hitchhiker's Guide to the Galaxy", new List<int> { 1 }, 1987, 320);
        myBook.AddBook("The Right Stuff", new List<int> { 2 }, 1993, 512);
        myBook.AddBook("Real-Time Shadows", new List<int> { 3, 4, 5, 6 }, 2011, 383);
        myBook.AddBook("Mesjasz Diuny", new List<int> { 7 }, 1972, 272);
        myBook.AddBook("Dobry Omen", new List<int> { 8, 9 }, 1990, 416);

        //books 3

        var book3_1 = new Book3(myHashMaps, "The Hitchhiker's Guide to the Galaxy", new List<int> { authorkey1 }, 1987,
            320);
        var book3_2 = new Book3(myHashMaps, "The Right Stuff", new List<int> { authorkey2 }, 1993, 512);
        var book3_3 = new Book3(myHashMaps, "Real-Time Shadows",
            new List<int> { authorkey3, authorkey4, authorkey5, authorkey6 }, 2011, 383);
        var book3_4 = new Book3(myHashMaps, "Mesjasz Diuny", new List<int> { authorkey7 }, 1972, 272);
        var book3_5 = new Book3(myHashMaps, "Dobry Omen", new List<int> { authorkey8, authorkey9 }, 1990, 416);

        //Newspaper1
        var newspaper1 = new Newspaper
            { Title = "International Journal of Human-Computer Studies", Year = 1980, PageCount = 300 };
        var newspaper2 = new Newspaper { Title = "Nature", Year = 1869, PageCount = 200 };
        var newspaper3 = new Newspaper { Title = "National Geographic", Year = 2001, PageCount = 106 };
        var newspaper4 = new Newspaper { Title = "Pixel", Year = 2015, PageCount = 115 };

        //Newspaper2
        var myNewspaper = new Newspaper2();
        myNewspaper.AddNewspaper("International Journal of Human-Computer Studies", 1980, 300);
        myNewspaper.AddNewspaper("Nature", 1869, 200);
        myNewspaper.AddNewspaper("National Geographic", 2001, 106);
        myNewspaper.AddNewspaper("Pixel", 2015, 115);

        //Newspaper3
        var myNewspaper3_1 = new Newspaper3(myHashMaps, "International Journal of Human-Computer Studies", 1980, 300);
        var myNewspaper3_2 = new Newspaper3(myHashMaps, "Nature", 1869, 200);
        var myNewspaper3_3 = new Newspaper3(myHashMaps, "National Geographic", 2001, 106);
        var myNewspaper3_4 = new Newspaper3(myHashMaps, "Pixel", 2015, 115);

        //Boardgames 1
        var boardGame1 = new Boardgame { Title = "Scythe", MinPlayers = 1, MaxPlayers = 5, Difficulty = 7 };
        var boardGame2 = new Boardgame { Title = "Catan", MinPlayers = 3, MaxPlayers = 4, Difficulty = 6 };
        var boardGame3 = new Boardgame { Title = "Scrabble", MinPlayers = 2, MaxPlayers = 4, Difficulty = 5 };
        var boardGame4 = new Boardgame { Title = "Twilight Imperium", MinPlayers = 3, MaxPlayers = 8, Difficulty = 9 };

        boardGame1.Authors = new List<Author?>();
        boardGame2.Authors = new List<Author?>();
        boardGame3.Authors = new List<Author?>();
        boardGame4.Authors = new List<Author?>();

        boardGame1.Authors.Add(author10);
        boardGame1.Authors.Add(author11);
        boardGame2.Authors.Add(author12);
        boardGame3.Authors.Add(author13);
        boardGame3.Authors.Add(author14);
        boardGame4.Authors.Add(author15);

        //Boardgames 2
        var myBoardgame = new Boardgame2();

        myBoardgame.AddBoardgame("Scythe", 1, 5, 7, new List<int> { 10, 11 });
        myBoardgame.AddBoardgame("Catan", 3, 4, 6, new List<int> { 12 });
        myBoardgame.AddBoardgame("Scrabble", 2, 4, 5, new List<int> { 13, 14 });
        myBoardgame.AddBoardgame("Twilight Imperium", 3, 8, 9, new List<int> { 15 });

        //Boardgames 3

        var myBoardgame3_1 = new Boardgame3(myHashMaps, "Scythe", 1, 5, 7, new List<int> { authorkey10, authorkey11 });
        var myBoardgame3_2 = new Boardgame3(myHashMaps, "Catan", 3, 4, 6, new List<int> { authorkey12 });
        var myBoardgame3_3 =
            new Boardgame3(myHashMaps, "Scrabble", 2, 4, 5, new List<int> { authorkey13, authorkey14 });
        var myBoardgame3_4 = new Boardgame3(myHashMaps, "Twilight Imperium", 3, 8, 9, new List<int> { authorkey15 });
        
        Console.WriteLine("\n");
        
        _commands = new Dictionary<string, Command>
        {
            { "list", new ListCommand(Data) },
            { "find", new FindCommand(Data) },
            { "exit", new ExitCommand(Data) }
        };

        Console.WriteLine("\n");
        Console.WriteLine("Welcome to the library!");
        
        Bajtpik.ICollection<Book> book1Vector = new Vector<Book>();
        book1Vector.Add( book1 );
        book1Vector.Add( book2 );
        book1Vector.Add( book3 );
        book1Vector.Add( book4 );
        book1Vector.Add( book5 );

        Bajtpik.ICollection<Newspaper> newspaper1List = new DoublyLinkedList<Newspaper>();
        newspaper1List.Add( newspaper1 );
        newspaper1List.Add( newspaper2 );
        newspaper1List.Add( newspaper3 );
        newspaper1List.Add( newspaper4 );
        
        Bajtpik.ICollection<Boardgame> boardgame1List = new DoublyLinkedList<Boardgame>();
        
        boardgame1List.Add( boardGame1 );
        boardgame1List.Add( boardGame2 );
        boardgame1List.Add( boardGame3 );
        boardgame1List.Add( boardGame4 );
        
        Bajtpik.ICollection<Author2> author12List = new DoublyLinkedList<Author2>();
        
        author12List.Add(myAuthor);

        Bajtpik.ICollection<Book2> book2List = new DoublyLinkedList<Book2>();
        
        book2List.Add(myBook);
        
        Bajtpik.ICollection<Newspaper2> newspaper2List = new DoublyLinkedList<Newspaper2>();

        newspaper2List.Add(myNewspaper);
        
        Bajtpik.ICollection<Boardgame2> boardgame2List = new DoublyLinkedList<Boardgame2>();
        
        boardgame2List.Add(myBoardgame);
        
        Bajtpik.ICollection<GlobalData> hashmaps = new Vector<GlobalData>();
        
        hashmaps.Add(myHashMaps);
        
        Data.author1 = author1Heap;
        Data.book1 = book1Vector;
        Data.newspaper1 = newspaper1List;
        Data.boardgame1 = boardgame1List;
        Data.author2 = author12List;
        Data.book2 = book2List;
        Data.newspaper2 = newspaper2List;
        Data.boardgame2 = boardgame2List;
        Data.authors2 = myAuthor;
        Data.globalData = hashmaps;
        

        while (true)
        {
            Console.Write("Enter command: ");
            var input = Console.ReadLine();
            
            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
            {
                _commands["exit"].Execute(null!);
            }
            
            var regex = MyRegex();
            var match = regex.Match(input ?? throw new InvalidOperationException());

            if (match.Success)
            {
                var command = match.Groups["command"].Value;
                var className = match.Groups["className"].Value;

                if (command == "list")
                {
                    var args = new[] { command, className };

                    if (_commands.TryGetValue(command, out var value))
                    {
                        try
                        {
                            value.Execute(args);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown command.");
                    }
                }
                else
                {
                    string property = match.Groups["property"].Value;
                    string operatorType = match.Groups["operator"].Value;
                    string searchTerm = match.Groups["searchTerm"].Value;

                    string[] args = new[] { command, className, property, operatorType, searchTerm };

                    if (_commands.TryGetValue(command, out var value))
                    {
                        try
                        {
                            value.Execute(args);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                    }
                    
                    else
                    {
                        Console.WriteLine("Unknown command.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid command format.");
            }
        }
    }

    [GeneratedRegex("^(?<command>\\w+)\\s+(?<className>\\w+)(?:\\s+(?<property>\\w+)\\s+(?<operator>[=<>])\\s+(?<searchTerm>.+))?$")]
    private static partial Regex MyRegex();
}
