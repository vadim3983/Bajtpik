using Bajtpik.Adapters;
using Bajtpik.Bajtpik2;
using Bajtpik.Bajtpik3;

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

        //Authors3

        var author3_1 = new Author3("Douglas", "Adams", 1952, "");
        var author3_2 = new Author3("Tom", "Wolfe", 1930, "");
        var author3_3 = new Author3("Elmar", "Eisemann", 1978, "");
        var author3_4 = new Author3("Michael", "Schwarz", 1970, "");
        var author3_5 = new Author3("Ulf", "Assarsson", 1975, "");
        var author3_6 = new Author3("Michael", "Wimmer", 1980, "");
        var author3_7 = new Author3("Frank", "Herbert", 1920, "");
        var author3_8 = new Author3("Terry", "Pratchett", 1948, "");
        var author3_9 = new Author3("Neil", "Gaiman", 1960, "");
        var author3_10 = new Author3("Jamey", "Stegmaier", 1975, "");
        var author3_11 = new Author3("Jakub", "Różalski", 1981, "Mr. Werewolf");
        var author3_12 = new Author3("Klaus", "Teuber", 1952, "");
        var author3_13 = new Author3("Alfred", "Butts", 1899, "");
        var author3_14 = new Author3("James", "Brunot", 1902, "");
        var author3_15 = new Author3("Christian T.", "Petersen", 1970, "");

        //author 3 to 1 adapter

        var authorAdapter2 = new AuthorAdapter2(author3_1);
        var authorAdapter3 = new AuthorAdapter2(author3_2);
        var authorAdapter4 = new AuthorAdapter2(author3_3);
        var authorAdapter5 = new AuthorAdapter2(author3_4);
        var authorAdapter6 = new AuthorAdapter2(author3_5);
        var authorAdapter7 = new AuthorAdapter2(author3_6);
        var authorAdapter8 = new AuthorAdapter2(author3_7);
        var authorAdapter9 = new AuthorAdapter2(author3_8);
        var authorAdapter10 = new AuthorAdapter2(author3_9);
        var authorAdapter11 = new AuthorAdapter2(author3_10);
        var authorAdapter12 = new AuthorAdapter2(author3_11);
        var authorAdapter13 = new AuthorAdapter2(author3_12);
        var authorAdapter14 = new AuthorAdapter2(author3_13);
        var authorAdapter15 = new AuthorAdapter2(author3_14);
        var authorAdapter16 = new AuthorAdapter2(author3_15);


        //books 1
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

        //books 2
        var myBook = new Book2();

        myBook.AddBook("The Hitchhiker's Guide to the Galaxy", new List<int> { 1 }, 1987, 320);
        myBook.AddBook("The Right Stuff", new List<int> { 2 }, 1993, 512);
        myBook.AddBook("Real-Time Shadows", new List<int> { 3, 4, 5, 6 }, 2011, 383);
        myBook.AddBook("Mesjasz Diuny", new List<int> { 7 }, 1972, 272);
        myBook.AddBook("Dobry Omen", new List<int> { 8, 9 }, 1990, 416);

        //books 3

        var book3_1 = new Book3("The Hitchhiker's Guide to the Galaxy", 1987, 320);
        var book3_2 = new Book3("The Right Stuff", 1993, 512);
        var book3_3 = new Book3("Real-Time Shadows", 2011, 383);
        var book3_4 = new Book3("Mesjasz Diuny", 1972, 272);
        var book3_5 = new Book3("Dobry Omen", 1990, 416);

        //authors 3 to 1 adapter
        var bookAdapter3_1 = new BookAdapter2(book3_1, new List<Author3> { author3_1 });
        var bookAdapter3_2 = new BookAdapter2(book3_2, new List<Author3> { author3_2 });
        var bookAdapter3_3 =
            new BookAdapter2(book3_3, new List<Author3> { author3_3, author3_4, author3_5, author3_6 });
        var bookAdapter3_4 = new BookAdapter2(book3_4, new List<Author3> { author3_7 });
        var bookAdapter3_5 = new BookAdapter2(book3_5, new List<Author3> { author3_8, author3_9 });

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
        var myNewspaper3_1 = new Newspaper3("International Journal of Human-Computer Studies", 1980, 300);
        var myNewspaper3_2 = new Newspaper3("Nature", 1869, 200);
        var myNewspaper3_3 = new Newspaper3("National Geographic", 2001, 106);
        var myNewspaper3_4 = new Newspaper3("Pixel", 2015, 115);

        //Newspaper adapter 3 to 1

        var myNewspaperAdapter3_1 = new NewspaperAdapter2(myNewspaper3_1);
        var myNewspaperAdapter3_2 = new NewspaperAdapter2(myNewspaper3_2);
        var myNewspaperAdapter3_3 = new NewspaperAdapter2(myNewspaper3_3);
        var myNewspaperAdapter3_4 = new NewspaperAdapter2(myNewspaper3_4);

        //Boardgames 1
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

        //Boardgames 2
        var myBoardgame = new Boardgame2();

        myBoardgame.AddBoardgame("Scythe", 1, 5, 7, new List<int> { 10, 11 });
        myBoardgame.AddBoardgame("Catan", 3, 4, 6, new List<int> { 12 });
        myBoardgame.AddBoardgame("Scrabble", 2, 4, 5, new List<int> { 13, 14 });
        myBoardgame.AddBoardgame("Twilight Imperium", 3, 8, 9, new List<int> { 15 });

        //Boardgames 3

        var myBoardgame3_1 = new Boardgame3("Scythe", 1, 5, 7);
        var myBoardgame3_2 = new Boardgame3("Catan", 3, 4, 6);
        var myBoardgame3_3 = new Boardgame3("Scrabble", 2, 4, 5);
        var myBoardgame3_4 = new Boardgame3("Twilight Imperium", 3, 8, 9);

        //boardgames3 using adapter 3 to 1

        var myBoardgameAdapter2_1 = new BoardgameAdapter2(myBoardgame3_1, new List<Author3> { author3_10, author3_11 });
        var myBoardgameAdapter2_2 = new BoardgameAdapter2(myBoardgame3_2, new List<Author3> { author3_12 });
        var myBoardgameAdapter2_3 = new BoardgameAdapter2(myBoardgame3_3, new List<Author3> { author3_13, author3_14 });
        var myBoardgameAdapter2_4 = new BoardgameAdapter2(myBoardgame3_4, new List<Author3> { author3_15 });
        var myBoardgames3 = new List<BoardgameAdapter2>
            { myBoardgameAdapter2_1, myBoardgameAdapter2_2, myBoardgameAdapter2_3, myBoardgameAdapter2_4 };


        Console.WriteLine("Books: representation 1");
        book1.PrintBook();
        book2.PrintBook();
        book3.PrintBook();
        book4.PrintBook();
        book5.PrintBook();

        Console.WriteLine("\n");

        Console.WriteLine("Print books if author was born after 1970");
        var books = new List<Book> { book1, book2, book3, book4, book5 };
        //use printBooKAuthorbornAfter1970
        foreach (var book in books) book.PrintBookAuthorBornAfter1970();


        Console.WriteLine("\n");

        Console.WriteLine("Books: representation 2");

        myBook.PrintBook2(1, myAuthor);
        myBook.PrintBook2(2, myAuthor);
        myBook.PrintBook2(3, myAuthor);
        myBook.PrintBook2(4, myAuthor);
        myBook.PrintBook2(5, myAuthor);

        Console.WriteLine("\n");

        Console.WriteLine("Print books with adapter 2 to 1");
        var bookAdapter = new BooksAdapter(myBook, myAuthor);
        bookAdapter.PrintBook();
        bookAdapter.PrintBook();
        bookAdapter.PrintBook();
        bookAdapter.PrintBook();
        bookAdapter.PrintBook();

        Console.WriteLine("\n");

        Console.WriteLine("Books: representation 3");

        book3_1.PrintBook(new List<Author3> { author3_1 });
        book3_2.PrintBook(new List<Author3> { author3_2 });
        book3_3.PrintBook(new List<Author3> { author3_3, author3_4, author3_5, author3_6 });
        book3_4.PrintBook(new List<Author3> { author3_7 });
        book3_5.PrintBook(new List<Author3> { author3_8, author3_9 });

        Console.WriteLine("\n");

        Console.WriteLine("Print books with adapter 3 to 1");
        bookAdapter3_1.PrintBook();
        bookAdapter3_2.PrintBook();
        bookAdapter3_3.PrintBook();
        bookAdapter3_4.PrintBook();
        bookAdapter3_5.PrintBook();

        Console.WriteLine("\n");

        Console.WriteLine("\n");

        Console.WriteLine("Newspapers: representation 1");

        var myNewspaper1 = new List<Newspaper> { newspaper1, newspaper2, newspaper3, newspaper4 };
        foreach (var newspaper in myNewspaper1) newspaper.PrintNewspaper();

        Console.WriteLine("\n");

        Console.WriteLine("Newspapers: representation 2");

        for (var i = 1; i <= 4; i++) myNewspaper.PrintNewspaper2(i);

        Console.WriteLine("\n");

        //Newspaper adapter
        var myNewspaperAdapter = new NewspaperAdapter(myNewspaper);

        Console.WriteLine("Print newspaper using adapter 2 to 1");

        for (var i = 1; i <= 4; i++) myNewspaperAdapter.PrintNewspaper();

        //print newspaper3

        Console.WriteLine("\n");

        Console.WriteLine("Newspapers: representation 3");
        var myNewspaper3 = new List<Newspaper3> { myNewspaper3_1, myNewspaper3_2, myNewspaper3_3, myNewspaper3_4 };
        foreach (var newspaper in myNewspaper3) newspaper.PrintNewspaper();

        Console.WriteLine("\n");

        Console.WriteLine("Print newspaper using adapter 3 to 1");

        var myNewspaperAdapter3 = new List<INewspaper>
            { myNewspaperAdapter3_1, myNewspaperAdapter3_2, myNewspaperAdapter3_3, myNewspaperAdapter3_4 };
        foreach (var newspaper in myNewspaperAdapter3) newspaper.PrintNewspaper();

        Console.WriteLine("\n");

        Console.WriteLine("Boardgames:");

        var boardgames1 = new List<Boardgame> { boardGame1, boardGame2, boardGame3, boardGame4 };
        foreach (var boardgame in boardgames1) boardgame.PrintBoardgame();

        Console.WriteLine("\n");

        Console.WriteLine("Boardgames2:");

        for (var i = 1; i <= 4; i++) myBoardgame.PrintBoardgame2(i, myAuthor);

        Console.WriteLine("\n");

        Console.WriteLine("Print boardgames using adapter 2 to 1");
        var boardgameAdapter = new BoardGamesAdapter(myBoardgame, myAuthor);

        for (var i = 1; i <= 4; i++) boardgameAdapter.PrintBoardgame();

        Console.WriteLine("\n");

        Console.WriteLine("Boardgames: representation 3");
        myBoardgame3_1.PrintBoardgame(new List<Author3> { author3_10, author3_11 });
        myBoardgame3_2.PrintBoardgame(new List<Author3> { author3_12 });
        myBoardgame3_3.PrintBoardgame(new List<Author3> { author3_13, author3_14 });
        myBoardgame3_4.PrintBoardgame(new List<Author3> { author3_15 });

        Console.WriteLine("\n");

        Console.WriteLine("Print boardgames using adapter 3 to 1");
        foreach (var boardgame in myBoardgames3) boardgame.PrintBoardgame();

        Console.WriteLine("\n");

        Console.WriteLine("Print boardgames if author is born after 1970");
        foreach (var boardgame in boardgames1) boardgame.PrintBoardgameAuthorBornAfter1970();

        Console.WriteLine("\n");

        Console.WriteLine("Authors:");

        var authors1 = new List<Author>
        {
            author1, author2, author3, author4, author5, author6, author7, author8, author9, author10, author11,
            author12, author13, author14, author15
        };
        authors1.ForEach(a => a.PrintAuthor());

        Console.WriteLine("\n");

        //print authors2

        Console.WriteLine("Authors2:");

        for (var i = 1; i <= 15; i++) myAuthor.PrintAuthor2(i);

        Console.WriteLine("\n");

        Console.WriteLine("Print Authors using adapter 2 to 1");

        var myAuthorAdapter = new AuthorAdapter(myAuthor);

        for (var i = 1; i <= 15; i++) myAuthorAdapter.PrintAuthor();

        Console.WriteLine("\n");

        Console.WriteLine("Authors3:");

        var authors3 = new List<Author3>
        {
            author3_1, author3_2, author3_3, author3_4, author3_5, author3_6, author3_7, author3_8, author3_9,
            author3_10, author3_11, author3_12, author3_13, author3_14, author3_15
        };
        authors3.ForEach(author => author.PrintAuthor());

        Console.WriteLine("\n");
        Console.WriteLine("Print Authors3 using adapter 3 to 1");

        var authorAdapterList = new List<AuthorAdapter2>
        {
            authorAdapter2, authorAdapter3, authorAdapter4, authorAdapter5, authorAdapter6, authorAdapter7,
            authorAdapter8, authorAdapter9, authorAdapter10, authorAdapter11, authorAdapter12, authorAdapter13,
            authorAdapter14, authorAdapter15, authorAdapter16
        };
        authorAdapterList.ForEach(author => author.PrintAuthor());

        Console.WriteLine("\n");

        Console.WriteLine("'Print books if author was born after 1970' with adapter 3 to 1");

        var myBooks = new List<BookAdapter2>
            { bookAdapter3_1, bookAdapter3_2, bookAdapter3_3, bookAdapter3_4, bookAdapter3_5 };
        foreach (var book in myBooks) book.PrintBookAuthorBornAfter1970();

        Console.WriteLine("\n");

        Console.WriteLine("Print Boardgames if author was born after 1970 with adapter 3 to 1");

        foreach (var boardgame in myBoardgames3) boardgame.PrintBoardgameAuthorBornAfter1970();
    }
}