using Bajtpik;
using Bajtpik.Bajtpik2;
using Bajtpik.Bajtpik3;
using Newtonsoft.Json;

namespace Bajtpikapp;

public class Data
{
    public Bajtpik.ICollection<Author> author1 { get; set; }
    public Bajtpik.ICollection<Book> book1 { get; set; }
    public Bajtpik.ICollection<Newspaper> newspaper1 { get; set; }
    public Bajtpik.ICollection<Boardgame> boardgame1 { get; set; }

    public Author2 authors2 { get; set; }

    public Bajtpik.ICollection<Author2> author2 { get; set; }
    public Bajtpik.ICollection<Book2> book2 { get; set; }
    public Bajtpik.ICollection<Newspaper2> newspaper2 { get; set; }
    public Bajtpik.ICollection<Boardgame2> boardgame2 { get; set; }

    public Bajtpik.ICollection<GlobalData> globalData { get; set; }

    public GlobalData globalData2 { get; set; }
    
    public Dictionary<int, Author> Authors { get; } = new Dictionary<int, Author>();

    
}

public abstract class Command
{
    protected Data Data;

    protected Command(Data data)
    {
        Data = data;
    }

    public abstract void Execute(string[] args);
}

public class ListCommand : Command
{
    private readonly Dictionary<string, Action> printActions;

    public ListCommand(Data data) : base(data)
    {
        printActions = new Dictionary<string, Action>
        {
            {
                "author1",
                () =>
                {
                    CollectionAlgorithms.Print((IEnumerator<Author>)data.author1.ForwardIterator().GetEnumerator(),
                        _ => true, x => x.PrintAuthor());
                }
            },
            {
                "book1",
                () =>
                {
                    CollectionAlgorithms.Print((IEnumerator<Book>)data.book1.ForwardIterator().GetEnumerator(),
                        _ => true, x => x.PrintBook());
                }
            },
            {
                "newspaper1",
                () =>
                {
                    CollectionAlgorithms.Print(
                        (IEnumerator<Newspaper>)data.newspaper1.ForwardIterator().GetEnumerator(), _ => true,
                        x => x.PrintNewspaper());
                }
            },
            {
                "boardgame1",
                () =>
                {
                    CollectionAlgorithms.Print(
                        (IEnumerator<Boardgame>)data.boardgame1.ForwardIterator().GetEnumerator(), _ => true,
                        x => x.PrintBoardgame());
                }
            },
            {
                "author2", () =>
                {
                    CollectionAlgorithms.Print((IEnumerator<Author2>)data.author2.ForwardIterator().GetEnumerator(),
                        _ => true, x =>
                        {
                            for (var i = 1; i <= 15; i++) x.PrintAuthor2(i);
                        });
                }
            },
            {
                "newspaper2", () =>
                {
                    CollectionAlgorithms.Print(
                        (IEnumerator<Newspaper2>)data.newspaper2.ForwardIterator().GetEnumerator(), _ => true, x =>
                        {
                            for (var i = 1; i <= 4; i++) x.PrintNewspaper2(i);
                        });
                }
            },
            {
                "boardgame2", () =>
                {
                    CollectionAlgorithms.Print(
                        (IEnumerator<Boardgame2>)data.boardgame2.ForwardIterator().GetEnumerator(), _ => true, x =>
                        {
                            for (var i = 1; i <= 4; i++) x.PrintBoardgame2(i, data?.authors2);
                        });
                }
            },
            {
                "book2", () =>
                {
                    CollectionAlgorithms.Print((IEnumerator<Book2>)data.book2.ForwardIterator().GetEnumerator(),
                        _ => true, x =>
                        {
                            for (var i = 1; i <= 5; i++) x.PrintBook2(i, data?.authors2);
                        });
                }
            },
            {
                "author3",
                () =>
                {
                    CollectionAlgorithms.Print(
                        (IEnumerator<GlobalData>)data.globalData.ForwardIterator().GetEnumerator(), _ => true,
                        x => x.PrintAllAuthors());
                }
            },
            {
                "book3",
                () =>
                {
                    CollectionAlgorithms.Print(
                        (IEnumerator<GlobalData>)data.globalData.ForwardIterator().GetEnumerator(), _ => true,
                        x => x.PrintAllBooks());
                }
            },

            {
                "newspaper3",
                () =>
                {
                    CollectionAlgorithms.Print(
                        (IEnumerator<GlobalData>)data.globalData.ForwardIterator().GetEnumerator(), _ => true,
                        x => x.PrintAllNewspapers());
                }
            },
            {
                "boardgame3",
                () =>
                {
                    CollectionAlgorithms.Print(
                        (IEnumerator<GlobalData>)data.globalData.ForwardIterator().GetEnumerator(), _ => true,
                        x => x.PrintAllBoardgames());
                }
            }
        };
    }

    public override void Execute(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Invalid number of arguments");
            return;
        }

        var className = args[1];
        if (printActions.TryGetValue(className, out var action))
            action();
        else
            Console.WriteLine($"Unknown class name: {className}");
    }
}

public class FindCommand : Command
{
    private readonly Dictionary<string, Action<string, string, string>> findActions;

    public FindCommand(Data data) : base(data)
    {
        findActions = new Dictionary<string, Action<string, string, string>>
        {
            {
                "author1",
                (searchTerm, operatorType, propertyName) =>
                {
                    Func<Author, bool> predicate;

                    switch (propertyName)
                    {
                        case "name" when operatorType != "=":
                            Console.WriteLine("Invalid operator for name property.");
                            return;
                        case "name":
                            predicate = author => author.Name == searchTerm;
                            break;
                        case "birthyear":
                        {
                            if (!int.TryParse(searchTerm, out var value))
                            {
                                Console.WriteLine("Invalid format for birthYear.");
                                return;
                            }

                            predicate = operatorType switch
                            {
                                "=" => author => author.BirthYear == value,
                                "<" => author => author.BirthYear < value,
                                ">" => author => author.BirthYear > value,
                                _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                            };
                            break;
                        }
                        default:
                            Console.WriteLine($"Invalid property name: {propertyName}");
                            return;
                    }

                    CollectionAlgorithms.Print((IEnumerator<Author>)data.author1.ForwardIterator().GetEnumerator(),
                        predicate, x => x.PrintAuthor());
                }
            },
            {
                "book1",
                (searchTerm, operatorType, propertyName) =>
                {
                    Func<Book, bool> predicate;

                    switch (propertyName)
                    {
                        case "title" when operatorType != "=":
                            Console.WriteLine("Invalid operator for name property.");
                            return;
                        case "title":
                            predicate = book => book.Title == searchTerm;
                            break;
                        case "author" when operatorType != "=":
                            Console.WriteLine("Invalid operator for author property.");
                            return;

                        case "author":
                            predicate = book =>
                                book.Authors.Any(author => author.Name == searchTerm || author.Surname == searchTerm);
                            break;
                        case "year":
                        case "pagecount":
                        {
                            if (!int.TryParse(searchTerm, out var value))
                            {
                                Console.WriteLine("Invalid format for year or pageCount.");
                                return;
                            }

                            predicate = operatorType switch
                            {
                                "=" => propertyName == "year"
                                    ? book => book.Year == value
                                    : book => book.PageCount == value,
                                "<" => propertyName == "year"
                                    ? book => book.Year < value
                                    : book => book.PageCount < value,
                                ">" => propertyName == "year"
                                    ? book => book.Year > value
                                    : book => book.PageCount > value,
                                _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                            };
                            break;
                        }
                        default:
                            Console.WriteLine($"Invalid property name: {propertyName}");
                            return;
                    }

                    CollectionAlgorithms.Print((IEnumerator<Book>)data.book1.ForwardIterator().GetEnumerator(),
                        predicate, x => x.PrintBook());
                }
            },
            {
                "newspaper1",
                (searchTerm, operatorType, propertyName) =>
                {
                    Func<Newspaper, bool> predicate;

                    switch (propertyName)
                    {
                        case "title" when operatorType != "=":
                            Console.WriteLine("Invalid operator for name property.");
                            return;
                        case "title":
                            predicate = newspaper => newspaper.Title == searchTerm;
                            break;
                        case "year":
                        case "pagecount":
                        {
                            if (!int.TryParse(searchTerm, out var value))
                            {
                                Console.WriteLine("Invalid format for year or pageCount.");
                                return;
                            }

                            predicate = operatorType switch
                            {
                                "=" => propertyName == "year"
                                    ? newspaper => newspaper.Year == value
                                    : newspaper => newspaper.PageCount == value,
                                "<" => propertyName == "year"
                                    ? newspaper => newspaper.Year < value
                                    : newspaper => newspaper.PageCount < value,
                                ">" => propertyName == "year"
                                    ? newspaper => newspaper.Year > value
                                    : newspaper => newspaper.PageCount > value,
                                _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                            };
                            break;
                        }
                        default:
                            Console.WriteLine($"Invalid property name: {propertyName}");
                            return;
                    }

                    CollectionAlgorithms.Print(
                        (IEnumerator<Newspaper>)data.newspaper1.ForwardIterator().GetEnumerator(), predicate,
                        x => x.PrintNewspaper());
                }
            },
            {
                "boardgame1",

                (searchTerm, operatorType, propertyName) =>
                {
                    Func<Boardgame, bool> predicate;

                    switch (propertyName)
                    {
                        case "title" when operatorType != "=":
                            Console.WriteLine("Invalid operator for name property.");
                            return;
                        case "title":
                            predicate = boardgame => boardgame.Title == searchTerm;
                            break;
                        case "minplayers":
                        case "maxplayers":
                        case "difficulty":
                        {
                            if (!int.TryParse(searchTerm, out var value))
                            {
                                Console.WriteLine("Invalid format for minPlayers, maxPlayers or difficulty.");
                                return;
                            }

                            predicate = operatorType switch
                            {
                                "=" => propertyName == "minplayers"
                                    ? boardgame => boardgame.MinPlayers == value
                                    : propertyName == "maxplayers"
                                        ? boardgame => boardgame.MaxPlayers == value
                                        : boardgame => boardgame.Difficulty == value,

                                "<" => propertyName == "minplayers"
                                    ? boardgame => boardgame.MinPlayers < value
                                    : propertyName == "maxplayers"
                                        ? boardgame => boardgame.MaxPlayers < value
                                        : boardgame => boardgame.Difficulty < value,

                                ">" => propertyName == "minplayers"
                                    ? boardgame => boardgame.MinPlayers > value
                                    : propertyName == "maxplayers"
                                        ? boardgame => boardgame.MaxPlayers > value
                                        : boardgame => boardgame.Difficulty > value,

                                _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                            };
                            break;
                        }
                        case "author" when operatorType != "=":
                            Console.WriteLine("Invalid operator for author property.");
                            return;

                        case "author":
                            predicate = boardgame => boardgame.Authors.Any(author =>
                                author.Name == searchTerm || author.Surname == searchTerm);
                            break;
                        default:
                            Console.WriteLine($"Invalid property name: {propertyName}");
                            return;
                    }

                    CollectionAlgorithms.Print(
                        (IEnumerator<Boardgame>)data.boardgame1.ForwardIterator().GetEnumerator(),
                        predicate, x => x.PrintBoardgame());
                }
            },
            {
                "author2",
                (searchTerm, operatorType, propertyName) =>
                {
                    Func<Author, bool> predicate;

                    switch (propertyName)
                    {
                        case "name" when operatorType != "=":
                            Console.WriteLine("Invalid operator for name property.");
                            return;
                        case "name":
                            predicate = author => author.Name == searchTerm;
                            break;
                        case "birthyear":
                        {
                            if (!int.TryParse(searchTerm, out var value))
                            {
                                Console.WriteLine("Invalid format for birthYear.");
                                return;
                            }

                            predicate = operatorType switch
                            {
                                "=" => author => author.BirthYear == value,
                                "<" => author => author.BirthYear < value,
                                ">" => author => author.BirthYear > value,
                                _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                            };
                            break;
                        }
                        default:
                            Console.WriteLine($"Invalid property name: {propertyName}");
                            return;
                    }

                    CollectionAlgorithms.Print((IEnumerator<Author>)data.author1.ForwardIterator().GetEnumerator(),
                        predicate, x => x.PrintAuthor());
                }
            },
            {
                "book2",
                (searchTerm, operatorType, propertyName) =>
                {
                    Func<Book, bool> predicate;

                    switch (propertyName)
                    {
                        case "title" when operatorType != "=":
                            Console.WriteLine("Invalid operator for name property.");
                            return;
                        case "title":
                            predicate = book => book.Title == searchTerm;
                            break;
                        case "author" when operatorType != "=":
                            Console.WriteLine("Invalid operator for author property.");
                            return;

                        case "author":
                            predicate = book =>
                                book.Authors.Any(author => author.Name == searchTerm || author.Surname == searchTerm);
                            break;
                        case "year":
                        case "pagecount":
                        {
                            if (!int.TryParse(searchTerm, out var value))
                            {
                                Console.WriteLine("Invalid format for year or pageCount.");
                                return;
                            }

                            predicate = operatorType switch
                            {
                                "=" => propertyName == "year"
                                    ? book => book.Year == value
                                    : book => book.PageCount == value,
                                "<" => propertyName == "year"
                                    ? book => book.Year < value
                                    : book => book.PageCount < value,
                                ">" => propertyName == "year"
                                    ? book => book.Year > value
                                    : book => book.PageCount > value,
                                _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                            };
                            break;
                        }
                        default:
                            Console.WriteLine($"Invalid property name: {propertyName}");
                            return;
                    }

                    CollectionAlgorithms.Print((IEnumerator<Book>)data.book1.ForwardIterator().GetEnumerator(),
                        predicate, x => x.PrintBook());
                }
            },
            {
                "newspaper2",
                (searchTerm, operatorType, propertyName) =>
                {
                    Func<Newspaper, bool> predicate;

                    switch (propertyName)
                    {
                        case "title" when operatorType != "=":
                            Console.WriteLine("Invalid operator for name property.");
                            return;
                        case "title":
                            predicate = newspaper => newspaper.Title == searchTerm;
                            break;
                        case "year":
                        case "pagecount":
                        {
                            if (!int.TryParse(searchTerm, out var value))
                            {
                                Console.WriteLine("Invalid format for year or pageCount.");
                                return;
                            }

                            predicate = operatorType switch
                            {
                                "=" => propertyName == "year"
                                    ? newspaper => newspaper.Year == value
                                    : newspaper => newspaper.PageCount == value,
                                "<" => propertyName == "year"
                                    ? newspaper => newspaper.Year < value
                                    : newspaper => newspaper.PageCount < value,
                                ">" => propertyName == "year"
                                    ? newspaper => newspaper.Year > value
                                    : newspaper => newspaper.PageCount > value,
                                _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                            };
                            break;
                        }
                        default:
                            Console.WriteLine($"Invalid property name: {propertyName}");
                            return;
                    }

                    CollectionAlgorithms.Print(
                        (IEnumerator<Newspaper>)data.newspaper1.ForwardIterator().GetEnumerator(), predicate,
                        x => x.PrintNewspaper());
                }
            },
            {
                "boardgame2",

                (searchTerm, operatorType, propertyName) =>
                {
                    Func<Boardgame, bool> predicate;

                    switch (propertyName)
                    {
                        case "title" when operatorType != "=":
                            Console.WriteLine("Invalid operator for name property.");
                            return;
                        case "title":
                            predicate = boardgame => boardgame.Title == searchTerm;
                            break;
                        case "minplayers":
                        case "maxplayers":
                        case "difficulty":
                        {
                            if (!int.TryParse(searchTerm, out var value))
                            {
                                Console.WriteLine("Invalid format for minPlayers, maxPlayers or difficulty.");
                                return;
                            }

                            predicate = operatorType switch
                            {
                                "=" => propertyName == "minplayers"
                                    ? boardgame => boardgame.MinPlayers == value
                                    : propertyName == "maxplayers"
                                        ? boardgame => boardgame.MaxPlayers == value
                                        : boardgame => boardgame.Difficulty == value,

                                "<" => propertyName == "minplayers"
                                    ? boardgame => boardgame.MinPlayers < value
                                    : propertyName == "maxplayers"
                                        ? boardgame => boardgame.MaxPlayers < value
                                        : boardgame => boardgame.Difficulty < value,

                                ">" => propertyName == "minplayers"
                                    ? boardgame => boardgame.MinPlayers > value
                                    : propertyName == "maxplayers"
                                        ? boardgame => boardgame.MaxPlayers > value
                                        : boardgame => boardgame.Difficulty > value,

                                _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                            };
                            break;
                        }
                        case "author" when operatorType != "=":
                            Console.WriteLine("Invalid operator for author property.");
                            return;

                        case "author":
                            predicate = boardgame => boardgame.Authors.Any(author =>
                                author.Name == searchTerm || author.Surname == searchTerm);
                            break;
                        default:
                            Console.WriteLine($"Invalid property name: {propertyName}");
                            return;
                    }

                    CollectionAlgorithms.Print(
                        (IEnumerator<Boardgame>)data.boardgame1.ForwardIterator().GetEnumerator(),
                        predicate, x => x.PrintBoardgame());
                }
            },
            {
                "author3",
                (searchTerm, operatorType, propertyName) =>
                {
                    data.globalData2.SearchAuthors(searchTerm, operatorType, propertyName);
                }
            },
            {
                "book3",
                (searchTerm, operatorType, propertyName) =>
                {
                    data.globalData2.SearchBooks(searchTerm, operatorType, propertyName);
                }
            },
            {
                "newspaper3",
                (searchTerm, operatorType, propertyName) =>
                {
                    data.globalData2.SearchNewspapers(searchTerm, operatorType, propertyName);
                }
            },
            {
                "boardgame3",

                (searchTerm, operatorType, propertyName) =>
                {
                    data.globalData2.SearchBoardgames(searchTerm, operatorType, propertyName);
                }
            }
        };
    }

    public override void Execute(string[] args)
    {
        if (args.Length != 5)
        {
            Console.WriteLine("Invalid number of arguments");
            return;
        }

        var className = args[1];
        var propertyName = args[2];
        var operatorType = args[3];
        var searchTerm = args[4];

        if (findActions.TryGetValue(className, out var action))
            action(searchTerm, operatorType, propertyName);
        else
            Console.WriteLine($"Unknown class name: {className}");
    }
}

public class AddCommand : Command
{
    private readonly string _className;
    private readonly bool _isBaseRepresentation;

    public AddCommand(Data data, string className, string representation) : base(data)
    {
        _className = className;
        _isBaseRepresentation = representation.Equals("base", StringComparison.OrdinalIgnoreCase);
    }

    public override void Execute(string[] args)
    {
        Dictionary<string, string> inputFields = new Dictionary<string, string>();
        string[] availableFields = GetAvailableFields();

        Console.WriteLine($"[Available fields: {string.Join(", ", availableFields)}]");

        if (GetAvailableFields().Length == 0)
        {
            Console.WriteLine($"[{_className} type does not support adding objects]");
            return;
        }
        while (true)
        {
            string input = Console.ReadLine();
            if (input.Equals("DONE", StringComparison.OrdinalIgnoreCase))
            {
                if (TryCreateObject(inputFields, out object newObj))
                {
                    AddObjectToData(newObj);
                    Console.WriteLine($"[{_className} created]");
                }
                else
                {
                    Console.WriteLine($"[{_className} creation failed]");
                }

                break;
            }

            if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[{_className} creation abandoned]");
                break;
            }
            string[] inputParts = input.Split('=');
            if (inputParts.Length == 2 && availableFields.Contains(inputParts[0]))
            {
                inputFields[inputParts[0]] = inputParts[1];
            }
            else
            {
                Console.WriteLine($"[Enter field name and value in format: field = value]");
            }
        }
    }

    private string[] GetAvailableFields()
    {
        return _className.ToLowerInvariant() switch
        {   
            "author" => new[] { "name", "surname", "birthyear", "nickname" },
            "book" => new[] { "title", "authors", "year", "pageCount" },
            "newspaper" => new[] { "title", "year", "pageCount" },
            "boardgame" => new[] { "title", "minplayers", "maxplayers", "difficulty", "authors" },
            _ => Array.Empty<string>()
        };
    }

    private bool TryCreateObject(Dictionary<string, string> inputFields, out object newObj)
    {
        switch (_className.ToLowerInvariant())
        {
            case "book":
                return TryCreateBook(inputFields, out newObj);
            case "author":
                return TryCreateAuthor(inputFields, out newObj);
            case "newspaper":
                return TryCreateNewspaper(inputFields, out newObj);
            case "boardgame":
                return TryCreateBoardgame(inputFields, out newObj);
            default:
                newObj = null!;
                return false;
        }
    }
    
    private bool TryCreateBook(Dictionary<string, string> inputFields, out object newObj)
    {
        bool titleProvided = inputFields.TryGetValue("title", out string title);
        int year = 0;
        bool yearProvided = inputFields.TryGetValue("year", out string yearStr) && int.TryParse(yearStr, out year);
        int pageCount = 0;
        bool pageCountProvided = inputFields.TryGetValue("pageCount", out string pageCountStr) && int.TryParse(pageCountStr, out pageCount);
        bool authorsProvided = inputFields.TryGetValue("authors", out string authorsStr);
        List<Author?> authors = new List<Author?>() {};
        
        if (authorsProvided)
        {
            string[] authorsNames = authorsStr.Split(',');
            foreach (string authorName in authorsNames)
            {
                string[] authorNameParts = authorName.Split(' ');
                if (authorNameParts.Length == 2)
                {
                    authors.Add(new Author { Name = authorNameParts[0], Surname = authorNameParts[1] });
                }
                else
                {
                    Console.WriteLine($"[Invalid author name: {authorName}]");
                }
            }
        }
        if (!(titleProvided || yearProvided || pageCountProvided || authorsProvided))
        {
            Console.WriteLine("[Warning: At least one field should be provided]");
        }

        newObj = _isBaseRepresentation
            ? new Book {Title = title ?? "", Year = year, PageCount = pageCount, Authors = authors }
            : new Book3(Data.globalData2, title ?? "", authors , year, pageCount);

        return titleProvided || yearProvided || pageCountProvided || authorsProvided;
    }
    
    private bool TryCreateBoardgame(Dictionary<string, string> inputFields, out object newObj)
    {
        bool titleProvided = inputFields.TryGetValue("title", out string title);
        int minPlayers = 0;
        bool minPlayersProvided = inputFields.TryGetValue("minplayers", out string minPlayersStr) && int.TryParse(minPlayersStr, out minPlayers);
        int maxPlayers = 0;
        bool maxPlayersProvided = inputFields.TryGetValue("maxplayers", out string maxPlayersStr) && int.TryParse(maxPlayersStr, out maxPlayers);
        int difficulty = 0;
        bool difficultyProvided = inputFields.TryGetValue("difficulty", out string difficultyStr) && int.TryParse(difficultyStr, out difficulty);
        bool authorsProvided = inputFields.TryGetValue("authors", out string authorsStr);
        List<Author?> authors = new List<Author?>() {};
        
        if (authorsProvided)
        {
            string[] authorsNames = authorsStr.Split(',');
            foreach (string authorName in authorsNames)
            {
                string[] authorNameParts = authorName.Split(' ');
                if (authorNameParts.Length == 2)
                {
                    authors.Add(new Author { Name = authorNameParts[0], Surname = authorNameParts[1] });
                }
                else
                {
                    Console.WriteLine($"[Invalid author name: {authorName}]");
                }
            }
        }
        
        if (!(titleProvided || minPlayersProvided || maxPlayersProvided || difficultyProvided || authorsProvided))
        {
            Console.WriteLine("[Warning: At least one field should be provided]");
        }
        
        if (minPlayers > maxPlayers)
        {
            Console.WriteLine("[Warning: Min players should be less or equal to max players]");
        }
        
        newObj = _isBaseRepresentation
            ? new Boardgame {Title = title ?? "", MinPlayers = minPlayers, MaxPlayers = maxPlayers, Difficulty = difficulty, Authors = authors }
            : new Boardgame3(Data.globalData2, title ?? "", minPlayers, maxPlayers, difficulty, authors);
        
        return titleProvided || minPlayersProvided || maxPlayersProvided || difficultyProvided || authorsProvided;
    }
    
    private bool TryCreateAuthor(Dictionary<string, string> inputFields, out object newObj)
    {
        bool nameProvided = inputFields.TryGetValue("name", out string name);
        bool surnameProvided = inputFields.TryGetValue("surname", out string surname);
        int birthYear = 0;
        bool birthYearProvided = inputFields.TryGetValue("birthyear", out string birthYearStr) && int.TryParse(birthYearStr, out birthYear);
        bool nicknameProvided = inputFields.TryGetValue("nickname", out string nickname);

        if (!birthYearProvided && inputFields.ContainsKey("birthYear"))
        {
            Console.WriteLine("[Warning: birthYear should be an integer]");
        }

        if (!(nameProvided || surnameProvided || birthYearProvided || nicknameProvided))
        {
            Console.WriteLine("[Warning: At least one field should be provided]");
        }

        newObj = _isBaseRepresentation
            ? new Author { Name = name ?? "", Surname = surname ?? "", BirthYear = birthYear, Nickname = nickname ?? "" }
            :new Author3(Data.globalData2, name ?? "", surname ?? "", birthYear, nickname ?? "");

        return nameProvided || surnameProvided || birthYearProvided || nicknameProvided;
    }


    private bool TryCreateNewspaper(Dictionary<string, string> inputFields, out object newObj)
    {
        bool titleProvided = inputFields.TryGetValue("title", out string title);
        int year = 0;
        bool yearProvided = inputFields.TryGetValue("year", out string yearStr) && int.TryParse(yearStr, out year);
        int pageCount = 0;
        bool pageCountProvided = inputFields.TryGetValue("pageCount", out string pageCountStr) && int.TryParse(pageCountStr, out pageCount);
        
        if (!(titleProvided || yearProvided || pageCountProvided))
        {
            Console.WriteLine("[Warning: At least one field should be provided]");
        }
        
        if (!yearProvided && inputFields.ContainsKey("year"))
        {
            Console.WriteLine("[Warning: year should be an integer]");
        }
        
        if (!pageCountProvided && inputFields.ContainsKey("pageCount"))
        {
            Console.WriteLine("[Warning: pageCount should be an integer]");
        }

        newObj = _isBaseRepresentation
            ? new Newspaper { Title = title ?? "", Year = year, PageCount = pageCount }
            : new Newspaper3(Data.globalData2, title ?? "", year, pageCount);
        
        return titleProvided || yearProvided || pageCountProvided;
    }

    private void AddObjectToData(object newObj)
    {

        switch (_className.ToLowerInvariant())
        {
            case "book":
                if (_isBaseRepresentation)
                {
                    Data.book1.Add((Book)newObj);
                }
                break;
            case "author":
                if (_isBaseRepresentation)
                {
                    Data.author1.Add((Author)newObj);
                }
                break;
            case "newspaper":
                if (_isBaseRepresentation)
                {
                    Data.newspaper1.Add((Newspaper)newObj);
                }
                break;
            
            case "boardgame":
                if (_isBaseRepresentation)
                {
                    Data.boardgame1.Add((Boardgame)newObj);
                }
                break;
        }
    }
}


public class SaveCommand : Command
{
    private readonly Data _data;

    public SaveCommand(Data data) : base(data)
    {
        _data = data;
    }

    public override void Execute(string[] args)
    {
        SaveDataToFile(_data);
    }

    private void SaveDataToFile(Data data)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText("data.json", json);
    }

}


public class ExitCommand : Command
{
    public ExitCommand(Data data) : base(data)
    {
    }

    public override void Execute(string[] args)
    {
        Environment.Exit(0);
    }
} 
