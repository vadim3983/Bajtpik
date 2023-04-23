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
    public FindCommand(Data data) : base(data)
    {
    }

    public override void Execute(string[] args)
    {
        // Implement find command logic here
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