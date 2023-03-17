﻿using Bajtpik.Bajtpik2;

namespace Bajtpik;

public abstract class main
{
    private static void Main()
    { 
        /*
Book - title, authors, year, pageCount
1. The Hitchhiker's Guide to the Galaxy, Douglas Adams, 1987, 320
2. The Right Stuff, Tom Wolfe, 1993, 512
3. Real-Time Shadows, [Eisemann, Schwarz, Assarsson, Wimmer], 2011, 383
4. Mesjasz Diuny, Frank Herbert, 1972, 272
5. Dobry Omen, [Terry Pratchett, Neil Gaiman], 1990, 416
         */
        var book1 = new Book {Title = "The Hitchhiker's Guide to the Galaxy", Year = 1987, PageCount = 320};
        var book2 = new Book {Title = "The Right Stuff", Year = 1993, PageCount = 512};
        var book3 = new Book {Title = "Real-Time Shadows", Year = 2011, PageCount = 383};
        var book4 = new Book {Title = "Mesjasz Diuny", Year = 1972, PageCount = 272};
        var book5 = new Book {Title = "Dobry Omen", Year = 1990, PageCount = 416};
        
        /*
         Author - name, surname, birthYear, nickname
1. Douglas, Adams, 1952
2. Tom, Wolfe, 1930
3. Elmar, Eisemann, 1978
4. Michael, Schwarz, 1970
5. Ulf, Assarsson, 1975
6. Michael, Wimmer, 1980
7. Frank, Herbert, 1920
8. Terry, Pratchett, 1948
9. Neil, Gaiman, 1960
10. Jamey, Stegmaier, 1975
11. Jakub, Różalski, 1981, “Mr. Werewolf”
12. Klaus, Teuber, 1952
13. Alfred, Butts, 1899
14. James, Brunot, 1902
15. Christian T., Petersen, 1970
         */
        
        var author1 = new Author {Name = "Douglas", Surname = "Adams", BirthYear = 1952};
        var author2 = new Author {Name = "Tom", Surname = "Wolfe", BirthYear = 1930};
        var author3 = new Author {Name = "Elmar", Surname = "Eisemann", BirthYear = 1978};
        var author4 = new Author {Name = "Michael", Surname = "Schwarz", BirthYear = 1970};
        var author5 = new Author {Name = "Ulf", Surname = "Assarsson", BirthYear = 1975};
        var author6 = new Author {Name = "Michael", Surname = "Wimmer", BirthYear = 1980};
        var author7 = new Author {Name = "Frank", Surname = "Herbert", BirthYear = 1920};
        var author8 = new Author {Name = "Terry", Surname = "Pratchett", BirthYear = 1948};
        var author9 = new Author {Name = "Neil", Surname = "Gaiman", BirthYear = 1960};
        var author10 = new Author {Name = "Jamey", Surname = "Stegmaier", BirthYear = 1975};
        var author11 = new Author {Name = "Jakub", Surname = "Różalski", BirthYear = 1981, Nickname = "Mr. Werewolf"};
        var author12 = new Author {Name = "Klaus", Surname = "Teuber", BirthYear = 1952};
        var author13 = new Author {Name = "Alfred", Surname = "Butts", BirthYear = 1899};
        var author14 = new Author {Name = "James", Surname = "Brunot", BirthYear = 1902};
        var author15 = new Author {Name = "Christian T.", Surname = "Petersen", BirthYear = 1970};
        
        var newspaper1 = new Newspaper {Title = "International Journal of Human-Computer Studies", Year = 1980, PageCount = 300};
        var newspaper2 = new Newspaper {Title = "Nature", Year = 1869, PageCount = 200};
        var newspaper3 = new Newspaper {Title = "National Geographic", Year = 2001, PageCount = 106};
        var newspaper4 = new Newspaper {Title = "Pixel", Year = 2015, PageCount = 115};
        
// create on object of tyoe Newspaper2 and assign it to newspaper2

        
        
        
        




        //object reference not set to an instance of an object
        book1.Authors = new List<Author>();
        book2.Authors = new List<Author>();
        book3.Authors = new List<Author>();
        book4.Authors = new List<Author>();
        book5.Authors = new List<Author>();
        
        book1.Authors.Add(author1);
        book2.Authors.Add(author2);
        book3.Authors.Add(author3);
        book3.Authors.Add(author4);
        book3.Authors.Add(author5);
        book3.Authors.Add(author6);
        book4.Authors.Add(author7);
        book5.Authors.Add(author8);
        book5.Authors.Add(author9);
     
        book1.PrintBook();
        book2.PrintBook();
        book3.PrintBook();
        book4.PrintBook();
        book5.PrintBook();
        
        Console.WriteLine("\n");
        
        newspaper1.PrintNewspaper();
        newspaper2.PrintNewspaper();
        newspaper3.PrintNewspaper();
        newspaper4.PrintNewspaper();
        
        

    }
}