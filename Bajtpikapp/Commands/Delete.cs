using System.Diagnostics;
using Bajtpik;

namespace Bajtpikapp;

public class DeleteCommand : Command
{
    private readonly Dictionary<string, Action<string, string, string>> deleteActions;

    private DeleteCommand()
    {
    }

    public DeleteCommand(Data data, string[] arguments) : base(data, arguments)
    {
        deleteActions = InitializeDeleteActions();
    }

    private Dictionary<string, Action<string, string, string>> InitializeDeleteActions()
    {
        return new Dictionary<string, Action<string, string, string>>
        {
            { "author", HandleAuthorDeletion },
            { "book", HandleBookDeletion },
            { "newspaper", HandleNewspaperDeletion },
            { "boardgame", HandleBoardgameDeletion }
        };
    }

    public override string ToString()
    {
        return string.Join(" ", args);
    }

    public override void Execute()
    {
        Console.WriteLine(this);

        if (args.Length != 5)
        {
            Console.WriteLine("Invalid number of arguments");
            return;
        }

        var className = args[1];
        var propertyName = args[2];
        var operatorType = args[3];
        var searchTerm = args[4];
        if (deleteActions.TryGetValue(className, out var action))
            action(searchTerm, operatorType, propertyName);
        else
            Console.WriteLine($"Unknown class name: {className}");
    }

    private static Func<T, bool> GetNumericPredicate<T>(string searchTerm, string operatorType, string propertyName,
        Func<T, int> propertyGetter)
    {
        if (int.TryParse(searchTerm, out var value))
            return operatorType switch
            {
                "=" => entity => propertyGetter(entity) == value,
                "<" => entity => propertyGetter(entity) < value,
                ">" => entity => propertyGetter(entity) > value,
                _ => throw new ArgumentException($"Invalid operator: {operatorType}")
            };
        Console.WriteLine($"Invalid format for {propertyName}.");
        return null!;
    }

    private static void DeleteMatchingEntities<T>(IEnumerable<T> entities, Func<T, bool> predicate,
        Action<T> deleteAction)
    {
        using var enumerator = entities.GetEnumerator();
        while (enumerator.MoveNext())
        {
            Debug.Assert(enumerator.Current != null, "enumerator.Current != null");
            if (!predicate(enumerator.Current)) continue;
            deleteAction(enumerator.Current);
            Console.WriteLine("Item deleted successfully.");
            return;
        }

        Console.WriteLine("No matching item found to delete.");
    }

    private void HandleAuthorDeletion(string searchTerm, string operatorType, string propertyName)
    {
        var predicate = GetPredicateForAuthorDeletion(searchTerm, operatorType, propertyName);
        DeleteMatchingEntities(Data.author1, predicate, author => Data.author1.Delete(author));
    }

    private static Func<Author, bool> GetPredicateForAuthorDeletion(string searchTerm, string operatorType,
        string propertyName)
    {
        switch (propertyName)
        {
            case "name":
                if (operatorType == "=") return author => author.Name == searchTerm;
                Console.WriteLine("Invalid operator for name property.");
                return null!;

            case "birthyear":
                return GetNumericPredicate<Author>(searchTerm, operatorType, propertyName,
                    author => (int)author.BirthYear);

            default:
                Console.WriteLine($"Invalid property name: {propertyName}");
                return null!;
        }
    }

    private void HandleBookDeletion(string searchTerm, string operatorType, string propertyName)
    {
        var predicate = GetPredicateForBookDeletion(searchTerm, operatorType, propertyName);
        DeleteMatchingEntities(Data.book1, predicate, book => Data.book1.Delete(book));
    }

    private Func<Book, bool> GetPredicateForBookDeletion(string searchTerm, string operatorType, string propertyName)
    {
        switch (propertyName)
        {
            case "title":
                if (operatorType == "=") return book => book.Title == searchTerm;
                Console.WriteLine("Invalid operator for title property.");
                return null!;

            case "author":
                if (operatorType == "=")
                    return book =>
                        book.Authors.Any(author => author.Name == searchTerm || author.Surname == searchTerm);
                Console.WriteLine("Invalid operator for author property.");
                return null!;

            case "year":
            case "pagecount":
                return GetNumericPredicate<Book>(searchTerm, operatorType, propertyName,
                    propertyName == "year" ? newspaper => (int)newspaper.Year : newspaper => (int)newspaper.PageCount);

            default:
                Console.WriteLine($"Invalid property name: {propertyName}");
                return null!;
        }
    }

    private void HandleNewspaperDeletion(string searchTerm, string operatorType, string propertyName)
    {
        var predicate = GetPredicateForNewspaperDeletion(searchTerm, operatorType, propertyName);

        DeleteMatchingEntities(Data.newspaper1, predicate, newspaper => Data.newspaper1.Delete(newspaper));
    }

    private Func<Newspaper, bool> GetPredicateForNewspaperDeletion(string searchTerm, string operatorType,
        string propertyName)
    {
        switch (propertyName)
        {
            case "title":
                if (operatorType == "=") return newspaper => newspaper.Title == searchTerm;
                Console.WriteLine("Invalid operator for title property.");
                return null!;

            case "year":
            case "pagecount":
                return GetNumericPredicate<Newspaper>(searchTerm, operatorType, propertyName,
                    propertyName == "year" ? newspaper => (int)newspaper.Year : newspaper => (int)newspaper.PageCount);
            default:
                Console.WriteLine($"Invalid property name: {propertyName}");
                return null;
        }
    }

    private void HandleBoardgameDeletion(string searchTerm, string operatorType, string propertyName)
    {
        var predicate = GetPredicateForBoardgameDeletion(searchTerm, operatorType, propertyName);

        DeleteMatchingEntities(Data.boardgame1, predicate, boardgame => Data.boardgame1.Delete(boardgame));
    }

    private Func<Boardgame, bool> GetPredicateForBoardgameDeletion(string searchTerm, string operatorType,
        string propertyName)
    {
        switch (propertyName)
        {
            case "title":
                if (operatorType == "=") return boardgame => boardgame.Title == searchTerm;
                Console.WriteLine("Invalid operator for title property.");
                return null;

            case "minplayers":
            case "maxplayers":
            case "difficulty":
                return GetNumericPredicate<Boardgame>(searchTerm, operatorType, propertyName, propertyName switch
                {
                    "minplayers" => boardgame => boardgame.MinPlayers,
                    "maxplayers" => boardgame => boardgame.MaxPlayers,
                    _ => boardgame => boardgame.Difficulty
                });

            case "author" when operatorType != "=":
                Console.WriteLine("Invalid operator for author property.");
                return null!;

            case "author":
                return boardgame => boardgame.Authors.Any(author =>
                    author?.Name == searchTerm || author.Surname == searchTerm);

            default:
                Console.WriteLine($"Invalid property name: {propertyName}");
                return null!;
        }
    }
}