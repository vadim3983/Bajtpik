using Bajtpik;
using Bajtpik.Bajtpik2;
using Bajtpik.Bajtpik3;

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
                        case "name" when operatorType != "=":
                            Console.WriteLine("Invalid operator for name property.");
                            return;
                        case "name":
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
                        case "name" when operatorType != "=":
                            Console.WriteLine("Invalid operator for name property.");
                            return;
                        case "name":
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
                        case "name" when operatorType != "=":
                            Console.WriteLine("Invalid operator for name property.");
                            return;
                        case "name":
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