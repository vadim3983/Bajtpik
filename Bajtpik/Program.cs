using Bajtpik.Adapters;
using Bajtpik.Bajtpik2;

namespace Bajtpik;

public abstract class main
{
    private static void Main()
    {
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


        //books
        var book1 = new Book { Title = "The Hitchhiker's Guide to the Galaxy", Year = 1987, PageCount = 320 };
        var book2 = new Book { Title = "The Right Stuff", Year = 1993, PageCount = 512 };
        var book3 = new Book { Title = "Real-Time Shadows", Year = 2011, PageCount = 383 };
        var book4 = new Book { Title = "Mesjasz Diuny", Year = 1972, PageCount = 272 };
        var book5 = new Book { Title = "Dobry Omen", Year = 1990, PageCount = 416 };

        book1.Authors = new List<Author> { author1 };
        book2.Authors = new List<Author> { author2 };
        book3.Authors = new List<Author>
        {
            author3,
            author4,
            author5,
            author6
        };
        book4.Authors = new List<Author> { author7 };
        book5.Authors = new List<Author>
        {
            author8,
            author9
        };

        Console.WriteLine("Books:");
        book1.PrintBook();
        book2.PrintBook();
        book3.PrintBook();
        book4.PrintBook();
        book5.PrintBook();

        Console.WriteLine("\n");

        //newspapers
        var newspaper1 = new Newspaper
            { Title = "International Journal of Human-Computer Studies", Year = 1980, PageCount = 300 };
        var newspaper2 = new Newspaper { Title = "Nature", Year = 1869, PageCount = 200 };
        var newspaper3 = new Newspaper { Title = "National Geographic", Year = 2001, PageCount = 106 };
        var newspaper4 = new Newspaper { Title = "Pixel", Year = 2015, PageCount = 115 };

        Console.WriteLine("Newspapers: representation 1");
        newspaper1.PrintNewspaper();
        newspaper2.PrintNewspaper();
        newspaper3.PrintNewspaper();
        newspaper4.PrintNewspaper();

        Console.WriteLine("\n");

        //Newspaper2
        var myNewspaper = new Newspaper2();
        myNewspaper.AddNewspaper("International Journal of Human-Computer Studies", 1980, 300);
        myNewspaper.AddNewspaper("Nature", 1869, 200);
        myNewspaper.AddNewspaper("National Geographic", 2001, 106);
        myNewspaper.AddNewspaper("Pixel", 2015, 115);

        Console.WriteLine("Newspapers: representation 2");

        myNewspaper.PrintNewspaper2(1);
        myNewspaper.PrintNewspaper2(2);
        myNewspaper.PrintNewspaper2(3);
        myNewspaper.PrintNewspaper2(4);

        Console.WriteLine("\n");

        Console.WriteLine("Print newspaper using adapter");

        var myNewspaperAdapter = new NewspaperAdapter(myNewspaper);

        myNewspaperAdapter.PrintNewspaper();
        myNewspaperAdapter.PrintNewspaper();
        myNewspaperAdapter.PrintNewspaper();
        myNewspaperAdapter.PrintNewspaper();

        Console.WriteLine("\n");

        var boardGame1 = new Boardgame { Title = "Scythe", MinPlayers = 1, MaxPlayers = 5, Difficulty = 7 };
        var boardGame2 = new Boardgame { Title = "Catan", MinPlayers = 3, MaxPlayers = 4, Difficulty = 6 };
        var boardGame3 = new Boardgame { Title = "Scrabble", MinPlayers = 2, MaxPlayers = 4, Difficulty = 5 };
        var boardGame4 = new Boardgame { Title = "Twilight Imperium", MinPlayers = 3, MaxPlayers = 8, Difficulty = 9 };

        boardGame1.Authors = new List<Author>();
        boardGame2.Authors = new List<Author>();
        boardGame3.Authors = new List<Author>();
        boardGame4.Authors = new List<Author>();

        boardGame1.Authors.Add(author10);
        boardGame1.Authors.Add(author11);
        boardGame2.Authors.Add(author12);
        boardGame3.Authors.Add(author13);
        boardGame3.Authors.Add(author14);
        boardGame4.Authors.Add(author15);

        Console.WriteLine("Boardgames:");

        boardGame1.PrintBoardgame();
        boardGame2.PrintBoardgame();
        boardGame3.PrintBoardgame();
        boardGame4.PrintBoardgame();

        Console.WriteLine("\n");

        //Boardgame2

        var myBoardgame = new Boardgame2();

        myBoardgame.AddBoardgame("Scythe", 1, 5, 7, myAuthor.GetAuthor(10));

        myBoardgame.AddBoardgame("Catan", 3, 4, 6, myAuthor.GetAuthor(12));
        myBoardgame.AddBoardgame("Scrabble", 2, 4, 5, myAuthor.GetAuthor(13));
        myBoardgame.AddBoardgame("Twilight Imperium", 3, 8, 9, myAuthor.GetAuthor(15));

        //print authors
        Console.WriteLine("Authors:");
        author1.PrintAuthor();
        author2.PrintAuthor();
        author3.PrintAuthor();
        author4.PrintAuthor();
        author5.PrintAuthor();
        author6.PrintAuthor();
        author7.PrintAuthor();
        author8.PrintAuthor();
        author9.PrintAuthor();
        author10.PrintAuthor();
        author11.PrintAuthor();
        author12.PrintAuthor();
        author13.PrintAuthor();
        author14.PrintAuthor();
        author15.PrintAuthor();

        Console.WriteLine("\n");
        //print authors2

        Console.WriteLine("Authors2:");
        myAuthor.PrintAuthor2(1);
        myAuthor.PrintAuthor2(2);
        myAuthor.PrintAuthor2(3);
        myAuthor.PrintAuthor2(4);
        myAuthor.PrintAuthor2(5);
        myAuthor.PrintAuthor2(6);
        myAuthor.PrintAuthor2(7);
        myAuthor.PrintAuthor2(8);
        myAuthor.PrintAuthor2(9);
        myAuthor.PrintAuthor2(10);
        myAuthor.PrintAuthor2(11);
        myAuthor.PrintAuthor2(12);
        myAuthor.PrintAuthor2(13);
        myAuthor.PrintAuthor2(14);
        myAuthor.PrintAuthor2(15);

        Console.WriteLine("\n");

        Console.WriteLine("Print Authors using adapter");

        var myAuthorAdapter = new AuthorAdapter(myAuthor);

        for (var i = 1; i <= 15; i++) myAuthorAdapter.PrintAuthor();
    }
}