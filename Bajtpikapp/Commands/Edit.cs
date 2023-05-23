using System.Text;
using Bajtpik;

namespace Bajtpikapp.Commands;

public class EditCommand : Command
{
    private EditCommand()
    {
    }

    public EditCommand(Data data, string[] arguments) : base(data, arguments)
    {
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var str in args) sb.Append(str + " ");
        sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }

    public override void Execute()
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

        switch (className.ToLowerInvariant())
        {
            case "author1":
                ExecuteForAuthor1(propertyName, operatorType, searchTerm);
                break;
            case "book1":
                ExecuteForBook1(propertyName, operatorType, searchTerm);
                break;
            case "newspaper1":
                ExecuteForNewspaper1(propertyName, operatorType, searchTerm);
                break;
            case "boardgame1":
                ExecuteForBoardgame1(propertyName, operatorType, searchTerm);
                break;
            case "author3":
                ExecuteForAuthor3(propertyName, operatorType, searchTerm);
                break;
            case "book3":
                ExecuteForBook3(propertyName, operatorType, searchTerm);
                break;
            case "newspaper3":
                ExecuteForNewspaper3(propertyName, operatorType, searchTerm);
                break;
            case "boardgame3":
                ExecuteForBoardgame3(propertyName, operatorType, searchTerm);
                break;
            default:
                Console.WriteLine($"Invalid class name: {className}");
                return;
        }
    }


    private void ExecuteForBoardgame3(string propertyName, string operatorType, string searchTerm)
    {
        Func<KeyValuePair<int, string>, bool> predicate;

        switch (propertyName)
        {
            case "title" when operatorType != "=":
                Console.WriteLine("Invalid operator for title property.");
                return;
            case "title":
                predicate = kvp =>
                {
                    var boardgameDetails = kvp.Value.Split(',');
                    var boardgameTitle = boardgameDetails[0].Trim();
                    return boardgameTitle.Equals(searchTerm, StringComparison.OrdinalIgnoreCase);
                };
                break;
            case "year":
            {
                if (!int.TryParse(searchTerm, out var value))
                {
                    Console.WriteLine("Invalid format for year.");
                    return;
                }

                predicate = Predicate(operatorType, value);
                break;
            }
            default:
                Console.WriteLine($"Invalid property name: {propertyName}");
                return;
        }

        var matchedBoardgames = Data.globalData2.GetBoardGameDictionary().Where(predicate).ToList();

        if (matchedBoardgames.Count == 0)
        {
            Console.WriteLine("No boardgames found matching the specified criteria.");
            return;
        }

        var availableFields = GetAvailableFieldsBoardGame();
        Console.WriteLine($"[Available fields: {string.Join(", ", availableFields)}]");

        var inputFields = new Dictionary<string, string>();
        while (true)
        {
            var input = Console.ReadLine();
            if (input.Equals("DONE", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var boardgameKvp in matchedBoardgames)
                {
                    var boardgameDetails = boardgameKvp.Value.Split(',');
                    var boardgameTitle = boardgameDetails[0].Trim();
                    var boardgameYear = int.Parse((ReadOnlySpan<char>)boardgameDetails[1].Trim()).ToString();
                    if (Data.globalData2.UpdateBoardGame(inputFields, boardgameTitle, boardgameYear))
                        Console.WriteLine((object?)$"[Boardgame updated: {boardgameTitle}]");
                    else
                        Console.WriteLine($"[Boardgame update failed: {boardgameTitle}]");
                }

                break;
            }

            if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[Boardgame update abandoned]");
                break;
            }

            var inputParts = input.Split('=');
            if (inputParts.Length == 2 && availableFields.Contains(inputParts[0]))
                inputFields[inputParts[0]] = inputParts[1];
            else
                Console.WriteLine("[Enter field name and value in format: field = value]");
        }
    }

    private static Func<KeyValuePair<int, string>, bool> Predicate(string operatorType, int value)
    {
        Func<KeyValuePair<int, string>, bool> predicate;
        predicate = operatorType switch
        {
            "=" => kvp =>
            {
                var boardgameDetails = kvp.Value.Split(',');
                return int.TryParse(boardgameDetails[1].Trim(), out var boardgameYear) &&
                       boardgameYear == value;
            },
            "<" => kvp =>
            {
                var boardgameDetails = kvp.Value.Split(',');
                return int.TryParse(boardgameDetails[1].Trim(), out var boardgameYear) &&
                       boardgameYear < value;
            },
            ">" => kvp =>
            {
                var boardgameDetails = kvp.Value.Split(',');
                return int.TryParse(boardgameDetails[1].Trim(), out var boardgameYear) &&
                       boardgameYear > value;
            },
            _ => throw new ArgumentException($"Invalid operator: {operatorType}")
        };
        return predicate;
    }


    private void ExecuteForBook3(string propertyName, string operatorType, string searchTerm)
    {
        Func<KeyValuePair<int, string>, bool> predicate;

        switch (propertyName)
        {
            case "title" when operatorType != "=":
                Console.WriteLine("Invalid operator for title property.");
                return;
            case "title":
                predicate = kvp =>
                {
                    var bookDetails = kvp.Value.Split(',');
                    return bookDetails[0].Trim().Equals(searchTerm, StringComparison.OrdinalIgnoreCase);
                };
                break;
            case "year":
            {
                if (!int.TryParse(searchTerm, out var value))
                {
                    Console.WriteLine("Invalid format for year.");
                    return;
                }

                predicate = operatorType switch
                {
                    "=" => kvp =>
                    {
                        var bookDetails = kvp.Value.Split(',');
                        return int.TryParse(bookDetails[1], out var year) && year == value;
                    },
                    "<" => kvp =>
                    {
                        var bookDetails = kvp.Value.Split(',');
                        return int.TryParse(bookDetails[1], out var year) && year < value;
                    },
                    ">" => kvp =>
                    {
                        var bookDetails = kvp.Value.Split(',');
                        return int.TryParse(bookDetails[1], out var year) && year > value;
                    },
                    _ => kvp =>
                    {
                        Console.WriteLine("Invalid operator for year property.");
                        return false;
                    }
                };
                break;
            }
            case "author":
                predicate = kvp =>
                {
                    var bookDetails = kvp.Value.Split(',');
                    return bookDetails[2].Trim().Equals(searchTerm, StringComparison.OrdinalIgnoreCase);
                };
                break;
            default:
                Console.WriteLine($"Invalid property name: {propertyName}");
                return;
        }

        var matchedBooks = Data.globalData2.GetBookDictionary().Where(predicate).ToList();

        if (matchedBooks.Count == 0)
        {
            Console.WriteLine("No authors found matching the specified criteria.");
            return;
        }

        var availableFields = GetAvailableFieldsBook();
        Console.WriteLine($"[Available fields: {string.Join(", ", availableFields)}]");

        var inputFields = new Dictionary<string, string>();
        while (true)
        {
            var input = Console.ReadLine();
            if (input.Equals("DONE", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var bookkvp in matchedBooks)
                {
                    var title = bookkvp.Value.Split(',')[0].Trim();
                    var author = bookkvp.Value.Split(',')[2].Trim();
                    var pages = bookkvp.Value.Split(',')[3].Trim();
                    var year = bookkvp.Value.Split(',')[4].Trim();

                    Console.WriteLine(Data.globalData2.UpdateBook(inputFields, title, author, pages)
                        ? $"[Author updated: {title} {author}]"
                        : $"[Author update failed: {author} {author}]");
                }

                break;
            }

            if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[Author update abandoned]");
                break;
            }

            var inputParts = input.Split('=');
            if (inputParts.Length == 2 && availableFields.Contains(inputParts[0]))
                inputFields[inputParts[0]] = inputParts[1];
            else
                Console.WriteLine("[Enter field name and value in format: field = value]");
        }
    }

    private void ExecuteForNewspaper3(string propertyName, string operatorType, string searchTerm)
    {
        Func<KeyValuePair<int, string>, bool> predicate;

        switch (propertyName)
        {
            case "title" when operatorType != "=":
                Console.WriteLine("Invalid operator for title property.");
                return;
            case "title":
                predicate = kvp =>
                {
                    var newspaperDetails = kvp.Value.Split(',');
                    var newspaperTitle = newspaperDetails[0].Trim();
                    return newspaperTitle.Equals(searchTerm, StringComparison.OrdinalIgnoreCase);
                };
                break;
            case "year":
            {
                if (!int.TryParse(searchTerm, out var value))
                {
                    Console.WriteLine("Invalid format for year.");
                    return;
                }

                predicate = operatorType switch
                {
                    "=" => kvp =>
                    {
                        var newspaperDetails = kvp.Value.Split(',');
                        return int.TryParse(newspaperDetails[1].Trim(), out var newspaperYear) &&
                               newspaperYear == value;
                    },
                    "<" => kvp =>
                    {
                        var newspaperDetails = kvp.Value.Split(',');
                        return int.TryParse(newspaperDetails[1].Trim(), out var newspaperYear) &&
                               newspaperYear < value;
                    },
                    ">" => kvp =>
                    {
                        var newspaperDetails = kvp.Value.Split(',');
                        return int.TryParse(newspaperDetails[1].Trim(), out var newspaperYear) &&
                               newspaperYear > value;
                    },
                    _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                };
                break;
            }
            default:
                Console.WriteLine($"Invalid property name: {propertyName}");
                return;
        }

        var matchedNewspapers = Data.globalData2.GetNewspaperDictionary().Where(predicate).ToList();

        if (matchedNewspapers.Count == 0)
        {
            Console.WriteLine("No newspapers found matching the specified criteria.");
            return;
        }

        var availableFields = GetAvailableFieldsNewspaper();
        Console.WriteLine($"[Available fields: {string.Join(", ", availableFields)}]");

        var inputFields = new Dictionary<string, string>();
        while (true)
        {
            var input = Console.ReadLine();
            if (input.Equals("DONE", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var newspaperKvp in matchedNewspapers)
                {
                    var newspaperDetails = newspaperKvp.Value.Split(',');
                    var newspaperTitle = newspaperDetails[0].Trim();
                    var newspaperYear = Convert.ToInt32(newspaperDetails[1].Trim());
                    Console.WriteLine(Data.globalData2.UpdateNewspaper(inputFields, newspaperTitle, newspaperYear)
                        ? $"[Newspaper updated: {newspaperTitle}]"
                        : $"[Newspaper update failed: {newspaperTitle}]");
                }

                break;
            }

            if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[Newspaper update abandoned]");
                break;
            }

            var inputParts = input.Split('=');
            if (inputParts.Length == 2 && availableFields.Contains(inputParts[0]))
                inputFields[inputParts[0]] = inputParts[1];
            else
                Console.WriteLine("[Enter field name and value in format: field = value]");
        }
    }


    private void ExecuteForAuthor3(string propertyName, string operatorType, string searchTerm)
    {
        Func<KeyValuePair<int, string>, bool> predicate;

        switch (propertyName)
        {
            case "name" when operatorType != "=":
                Console.WriteLine("Invalid operator for name property.");
                return;
            case "name":
                predicate = kvp =>
                {
                    var authorDetails = kvp.Value.Split(',');
                    var fullName = authorDetails[0].Trim();
                    return fullName.Equals(searchTerm, StringComparison.OrdinalIgnoreCase);
                };
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
                    "=" => kvp =>
                    {
                        var authorDetails = kvp.Value.Split(',');
                        return int.TryParse(authorDetails[1].Trim(), out var authorBirthYear) &&
                               authorBirthYear == value;
                    },
                    "<" => kvp =>
                    {
                        var authorDetails = kvp.Value.Split(',');
                        return int.TryParse(authorDetails[1].Trim(), out var authorBirthYear) &&
                               authorBirthYear < value;
                    },
                    ">" => kvp =>
                    {
                        var authorDetails = kvp.Value.Split(',');
                        return int.TryParse(authorDetails[1].Trim(), out var authorBirthYear) &&
                               authorBirthYear > value;
                    },
                    _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                };
                break;
            }
            default:
                Console.WriteLine($"Invalid property name: {propertyName}");
                return;
        }

        var matchedAuthors = Data.globalData2.GetAuthorDictionary().Where(predicate).ToList();

        if (matchedAuthors.Count == 0)
        {
            Console.WriteLine("No authors found matching the specified criteria.");
            return;
        }

        var availableFields = GetAvailableFieldsAuthor();
        Console.WriteLine($"[Available fields: {string.Join(", ", availableFields)}]");

        var inputFields = new Dictionary<string, string>();
        while (true)
        {
            var input = Console.ReadLine();
            if (input.Equals("DONE", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var authorKvp in matchedAuthors)
                {
                    var authorDetails = authorKvp.Value.Split(',');
                    var authorName = authorDetails[0].Trim().Split(' ')[0];
                    var authorSurname = authorDetails[0].Trim().Split(' ')[1];
                    Console.WriteLine(Data.globalData2.UpdateAuthor(inputFields, authorName, authorSurname)
                        ? $"[Author updated: {authorName} {authorSurname}]"
                        : $"[Author update failed: {authorName} {authorSurname}]");
                }

                break;
            }

            if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[Author update abandoned]");
                break;
            }

            var inputParts = input.Split('=');
            if (inputParts.Length == 2 && availableFields.Contains(inputParts[0]))
                inputFields[inputParts[0]] = inputParts[1];
            else
                Console.WriteLine("[Enter field name and value in format: field = value]");
        }
    }

    private void ExecuteForBook1(string propertyName, string operatorType, string searchTerm)
    {
        Func<Book, bool> predicate;
        switch (propertyName)
        {
            case "title" when operatorType != "=":
                Console.WriteLine("Invalid operator for title property.");
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

        var iterator = Data.book1.ForwardIterator().GetEnumerator();
        var updatedBooks = new List<Book>();

        CollectionAlgorithms.ForEach(iterator, book =>
        {
            if (predicate(book)) updatedBooks.Add(book);
        });

        if (updatedBooks.Count == 0)
        {
            Console.WriteLine("No books found matching the specified criteria.");
            return;
        }

        var availableFields = GetAvailableFieldsBook();
        Console.WriteLine($"[Available fields: {string.Join(", ", availableFields)}]");

        var inputFields = new Dictionary<string, string>();
        while (true)
        {
            var input = Console.ReadLine();
            if (input.Equals("DONE", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var book in updatedBooks)
                    Console.WriteLine(TryUpdateObjectBook(inputFields, book)
                        ? $"[Book updated: {book.Title}]"
                        : $"[Book update failed: {book.Title}]");
                break;
            }

            if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[Book update abandoned]");
                break;
            }

            var inputParts = input.Split('=');
            if (inputParts.Length == 2 && availableFields.Contains(inputParts[0]))
                inputFields[inputParts[0]] = inputParts[1];
            else
                Console.WriteLine("[Enter field name and value in format: field = value]");
        }
    }

    private void ExecuteForAuthor1(string propertyName, string operatorType, string searchTerm)
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

        var iterator = Data.author1.ForwardIterator().GetEnumerator();
        var updatedAuthors = new List<Author>();

        CollectionAlgorithms.ForEach(iterator, author =>
        {
            if (predicate(author)) updatedAuthors.Add(author);
        });

        if (updatedAuthors.Count == 0)
        {
            Console.WriteLine("No authors found matching the specified criteria.");
            return;
        }

        var availableFields = GetAvailableFieldsAuthor();
        Console.WriteLine($"[Available fields: {string.Join(", ", availableFields)}]");

        var inputFields = new Dictionary<string, string>();
        while (true)
        {
            var input = Console.ReadLine();
            if (input.Equals("DONE", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var author in updatedAuthors)
                    Console.WriteLine(TryUpdateObjectAuthor(inputFields, author)
                        ? $"[Author updated: {author.Name}]"
                        : $"[Author update failed: {author.Name}]");
                break;
            }

            if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[Author update abandoned]");
                break;
            }

            var inputParts = input.Split('=');
            if (inputParts.Length == 2 && availableFields.Contains(inputParts[0]))
                inputFields[inputParts[0]] = inputParts[1];
            else
                Console.WriteLine("[Enter field name and value in format: field = value]");
        }
    }


    private void ExecuteForNewspaper1(string propertyName, string operatorType, string searchTerm)
    {
        Func<Newspaper, bool> predicate;
        switch (propertyName)
        {
            case "title" when operatorType != "=":
                Console.WriteLine("Invalid operator for title property.");
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

        var iterator = Data.newspaper1.ForwardIterator().GetEnumerator();
        var updatedNewspapers = new List<Newspaper>(); // Store the updated newspapers

        CollectionAlgorithms.ForEach(iterator, newspaper =>
        {
            if (predicate(newspaper)) updatedNewspapers.Add(newspaper);
        });

        if (updatedNewspapers.Count == 0)
        {
            Console.WriteLine("No newspapers found matching the specified criteria.");
            return;
        }

        var availableFields = GetAvailableFieldsNewspaper();
        Console.WriteLine($"[Available fields: {string.Join(", ", availableFields)}]");

        var inputFields = new Dictionary<string, string>();
        while (true)
        {
            var input = Console.ReadLine();
            if (input.Equals("DONE", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var newspaper in updatedNewspapers)
                    if (TryUpdateObjectNewspaper(inputFields, newspaper))
                        Console.WriteLine($"[Newspaper updated: {newspaper.Title}]");
                    else
                        Console.WriteLine($"[Newspaper update failed: {newspaper.Title}]");
                break;
            }

            if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[Newspaper update abandoned]");
                break;
            }

            var inputParts = input.Split('=');
            if (inputParts.Length == 2 && availableFields.Contains(inputParts[0]))
                inputFields[inputParts[0]] = inputParts[1];
            else
                Console.WriteLine("[Enter field name and value in format: field = value]");
        }
    }

    private void ExecuteForBoardgame1(string propertyName, string operatorType, string searchTerm)
    {
        Func<Boardgame, bool> predicate;
        switch (propertyName)
        {
            case "title" when operatorType != "=":
                Console.WriteLine("Invalid operator for title property.");
                return;
            case "title":
                predicate = boardGame => boardGame.Title == searchTerm;
                break;
            case "minplayers":
            case "maxplayers":
            case "difficulty":
            case "author":
            {
                if (!int.TryParse(searchTerm, out var value))
                {
                    Console.WriteLine("Invalid format for minplayers, maxplayers, or difficulty.");
                    return;
                }

                predicate = operatorType switch
                {
                    "=" => propertyName == "minplayers"
                        ? boardGame => boardGame.MinPlayers == value
                        : propertyName == "maxplayers"
                            ? boardGame => boardGame.MaxPlayers == value
                            : propertyName == "difficulty"
                                ? boardGame => boardGame.Difficulty == value
                                : boardGame => boardGame.Authors.Any(author =>
                                    author.Name == searchTerm || author.Surname == searchTerm),
                    "<" => propertyName == "minplayers"
                        ? boardGame => boardGame.MinPlayers < value
                        : propertyName == "maxplayers"
                            ? boardGame => boardGame.MaxPlayers < value
                            : propertyName == "difficulty"
                                ? boardGame => boardGame.Difficulty < value
                                : throw new ArgumentException($"Invalid operator: {operatorType}"),
                    ">" => propertyName == "minplayers"
                        ? boardGame => boardGame.MinPlayers > value
                        : propertyName == "maxplayers"
                            ? boardGame => boardGame.MaxPlayers > value
                            : propertyName == "difficulty"
                                ? boardGame => boardGame.Difficulty > value
                                : throw new ArgumentException($"Invalid operator: {operatorType}"),
                    _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                };
                break;
            }
            default:
                Console.WriteLine($"Invalid property name: {propertyName}");
                return;
        }

        var iterator = Data.boardgame1.ForwardIterator().GetEnumerator();
        var updatedBoardGames = new List<Boardgame>();

        CollectionAlgorithms.ForEach(iterator, boardGame =>
        {
            if (predicate(boardGame)) updatedBoardGames.Add(boardGame);
        });

        if (updatedBoardGames.Count == 0)
        {
            Console.WriteLine("No board games found matching the specified criteria.");
            return;
        }

        var availableFields = GetAvailableFieldsBoardGame();
        Console.WriteLine($"[Available fields: {string.Join(", ", availableFields)}]");

        var inputFields = new Dictionary<string, string>();
        while (true)
        {
            var input = Console.ReadLine();
            if (input.Equals("DONE", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var boardGame in updatedBoardGames)
                    if (TryUpdateObjectBoardGame(inputFields, boardGame))
                        Console.WriteLine($"[BoardGame updated: {boardGame.Title}]");
                    else
                        Console.WriteLine($"[BoardGame update failed: {boardGame.Title}]");
                break;
            }

            if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[BoardGame update abandoned]");
                break;
            }

            var inputParts = input.Split('=');
            if (inputParts.Length == 2 && availableFields.Contains(inputParts[0]))
                inputFields[inputParts[0]] = inputParts[1];
            else
                Console.WriteLine("[Enter field name and value in format: field = value]");
        }
    }

    private string[] GetAvailableFieldsAuthor()
    {
        return new[] { "name", "surname", "birthyear", "nickname" };
    }

    private string[] GetAvailableFieldsBook()
    {
        return new[] { "title", "author", "year", "pagecount" };
    }

    private string[] GetAvailableFieldsNewspaper()
    {
        return new[] { "title", "year", "pagecount" };
    }

    private string[] GetAvailableFieldsBoardGame()
    {
        return new[] { "title", "minplayers", "maxplayers", "difficulty", "author" };
    }

    private bool TryUpdateObjectAuthor(Dictionary<string, string> inputFields, Author author)
    {
        if (inputFields.TryGetValue("name", out var newName)) author.Name = newName;
        if (inputFields.TryGetValue("surname", out var newSurname)) author.Surname = newSurname;
        if (inputFields.TryGetValue("birthyear", out var newBirthYearStr) &&
            int.TryParse(newBirthYearStr, out var newBirthYear)) author.BirthYear = newBirthYear;
        if (inputFields.TryGetValue("nickname", out var newNickname)) author.Nickname = newNickname;
        return true;
    }

    private bool TryUpdateObjectBook(Dictionary<string, string> inputFields, Book book)
    {
        if (inputFields.TryGetValue("title", out var newTitle)) book.Title = newTitle;
        if (inputFields.TryGetValue("author", out var newAuthor)) book.Authors[0] = new Author { Name = newAuthor };
        if (inputFields.TryGetValue("year", out var newYearStr) && int.TryParse(newYearStr, out var newYear))
            book.Year = newYear;
        if (inputFields.TryGetValue("pagecount", out var newPageCountStr) &&
            int.TryParse(newPageCountStr, out var newPageCount)) book.PageCount = newPageCount;
        return true;
    }

    private bool TryUpdateObjectNewspaper(Dictionary<string, string> inputFields, Newspaper newspaper)
    {
        if (inputFields.TryGetValue("title", out var newTitle)) newspaper.Title = newTitle;
        if (inputFields.TryGetValue("year", out var newYearStr) && int.TryParse(newYearStr, out var newYear))
            newspaper.Year = newYear;
        if (inputFields.TryGetValue("pagecount", out var newPageCountStr) &&
            int.TryParse(newPageCountStr, out var newPageCount)) newspaper.PageCount = newPageCount;
        return true;
    }

    private bool TryUpdateObjectBoardGame(Dictionary<string, string> inputFields, Boardgame boardGame)
    {
        if (inputFields.TryGetValue("title", out var newTitle)) boardGame.Title = newTitle;
        if (inputFields.TryGetValue("minplayers", out var newMinPlayersStr) &&
            int.TryParse(newMinPlayersStr, out var newMinPlayers)) boardGame.MinPlayers = newMinPlayers;
        if (inputFields.TryGetValue("maxplayers", out var newMaxPlayersStr) &&
            int.TryParse(newMaxPlayersStr, out var newMaxPlayers)) boardGame.MaxPlayers = newMaxPlayers;
        if (inputFields.TryGetValue("difficulty", out var newDifficultyStr) &&
            int.TryParse(newDifficultyStr, out var newDifficulty)) boardGame.Difficulty = newDifficulty;
        if (inputFields.TryGetValue("author", out var newAuthor))
            boardGame.Authors[0] = new Author { Name = newAuthor };
        return true;
    }
}