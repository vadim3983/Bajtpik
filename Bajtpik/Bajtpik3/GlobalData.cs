﻿using Bajtpik.Bajtpik3.IBajtpik3;

namespace Bajtpik.Bajtpik3;

public class GlobalData : IGlobalData
{
    private readonly Dictionary<int, string> _authors;
    private readonly Dictionary<int, string> _boardgames;
    private readonly Dictionary<int, string> _books;
    private readonly Dictionary<int, string> _newspapers;


    public GlobalData()
    {
        _newspapers = new Dictionary<int, string>();
        _authors = new Dictionary<int, string>();
        _books = new Dictionary<int, string>();
        _boardgames = new Dictionary<int, string>();
    }

    public Dictionary<int, string> GetBookDictionary()
    {
        return _books;
    }

    public Dictionary<int, string> GetAuthorDictionary()
    {
        return _authors;
    }

    public Dictionary<int, string> GetBoardGameDictionary()
    {
        return _boardgames;
    }

    public int GetAuthorKey(string name, string surname, int? birthYear = null)
    {
        foreach (var authorEntry in _authors)
        {
            var authorDetails = authorEntry.Value.Split(',');

            var nameParts = authorDetails[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length < 2) continue;

            var firstName = nameParts[0].Trim();
            var lastName = nameParts[1].Trim();

            if (!firstName.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                !lastName.Equals(surname, StringComparison.OrdinalIgnoreCase)) continue;

            if (birthYear.HasValue)
            {
                if (int.TryParse(authorDetails[1].Trim(), out var authorBirthYear) &&
                    birthYear.Value == authorBirthYear)
                    return authorEntry.Key;
            }
            else
            {
                return authorEntry.Key;
            }
        }

        return -1;
    }

    public void PrintAllNewspapers()
    {
        PrintItems(_newspapers);
    }

    public void PrintAllAuthors()
    {
        PrintItems(_authors);
    }

    public void PrintAllBooks()
    {
        PrintItems(_books);
    }

    public void PrintAllBoardgames()
    {
        PrintItems(_boardgames);
    }

    private static void PrintItems(Dictionary<int, string> items)
    {
        foreach (var kvp in items)
            Console.WriteLine(kvp.Value);
    }

    public void AddAuthor(string name, string surname, int? birthYear, string nickname)
    {
        var value = nickname == "" ? $"{name} {surname}, {birthYear}" : $"{name} {surname}, {birthYear}, {nickname}";
        var key = (name + surname + birthYear).GetHashCode();
        _authors.Add(key, value);
    }

    public void AddNewspaper(string title, int year, int pageCount)
    {
        var value = $"{title}, {year}, {pageCount} ";
        var key = (title + year + pageCount).GetHashCode();
        _newspapers.Add(key, value);
    }
    

    public void SearchAuthors(string searchTerm, string operatorType, string propertyName)
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

        var matchedAuthors = _authors.Where(predicate).ToList();
        if (matchedAuthors.Count == 0)
        {
            Console.WriteLine("No authors found.");
            return;
        }

        PrintItems(matchedAuthors);
    }

    public void SearchNewspapers(string searchTerm, string operatorType, string propertyName)
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
                    var title = newspaperDetails[0].Trim();
                    return title.Equals(searchTerm, StringComparison.OrdinalIgnoreCase);
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
                        return int.TryParse(newspaperDetails[1].Trim(), out var newspaperYear) && newspaperYear < value;
                    },
                    ">" => kvp =>
                    {
                        var newspaperDetails = kvp.Value.Split(',');
                        return int.TryParse(newspaperDetails[1].Trim(), out var newspaperYear) && newspaperYear > value;
                    },
                    _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                };
                break;
            }
            case "pagecount":
            {
                if (!int.TryParse(searchTerm, out var value))
                {
                    Console.WriteLine("Invalid format for pageCount.");
                    return;
                }

                predicate = operatorType switch
                {
                    "=" => kvp =>
                    {
                        var newspaperDetails = kvp.Value.Split(',');
                        return int.TryParse(newspaperDetails[2].Trim(), out var newspaperYear) &&
                               newspaperYear == value;
                    },
                    "<" => kvp =>
                    {
                        var newspaperDetails = kvp.Value.Split(',');
                        return int.TryParse(newspaperDetails[2].Trim(), out var newspaperYear) && newspaperYear < value;
                    },
                    ">" => kvp =>
                    {
                        var newspaperDetails = kvp.Value.Split(',');
                        return int.TryParse(newspaperDetails[2].Trim(), out var newspaperYear) && newspaperYear > value;
                    },
                    _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                };
                break;
            }

            default:
                Console.WriteLine($"Invalid property name: {propertyName}");
                return;
        }

        var matchedNewspapers = _newspapers.Where(predicate).ToList();
        if (!matchedNewspapers.Any())
            Console.WriteLine("No newspapers found.");
        PrintItems(matchedNewspapers);
    }

    public void SearchBooks(string searchTerm, string operatorType, string propertyName)
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
                    var title = bookDetails[0].Trim();
                    return title.Equals(searchTerm, StringComparison.OrdinalIgnoreCase);
                };
                break;

            case "author":
                predicate = kvp =>
                {
                    var bookDetails = kvp.Value.Split(',');
                    var authors = bookDetails[1].Trim().Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    return authors.Any(author => author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                };
                break;

            case "year":
            {
                if (!int.TryParse(searchTerm, out var value))
                {
                    Console.WriteLine("Invalid format for pageCount.");
                    return;
                }

                predicate = operatorType switch
                {
                    "=" => kvp =>
                    {
                        var bookDetails = kvp.Value.Split(',');
                        return int.TryParse(bookDetails[^2].Trim(), out var pageCount) && pageCount == value;
                    },
                    "<" => kvp =>
                    {
                        var bookDetails = kvp.Value.Split(',');
                        return int.TryParse(bookDetails[^2].Trim(), out var pageCount) && pageCount < value;
                    },
                    ">" => kvp =>
                    {
                        var bookDetails = kvp.Value.Split(',');
                        return int.TryParse(bookDetails[^2].Trim(), out var pageCount) && pageCount > value;
                    },
                    _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                };
                break;
            }
            case "pagecount":
            {
                if (!int.TryParse(searchTerm, out var value))
                {
                    Console.WriteLine("Invalid format for pageCount.");
                    return;
                }

                predicate = operatorType switch
                {
                    "=" => kvp =>
                    {
                        var bookDetails = kvp.Value.Split(',');
                        return int.TryParse(bookDetails[^1].Trim(), out var pageCount) && pageCount == value;
                    },
                    "<" => kvp =>
                    {
                        var bookDetails = kvp.Value.Split(',');
                        return int.TryParse(bookDetails[^1].Trim(), out var pageCount) && pageCount < value;
                    },
                    ">" => kvp =>
                    {
                        var bookDetails = kvp.Value.Split(',');
                        return int.TryParse(bookDetails[^1].Trim(), out var pageCount) && pageCount > value;
                    },
                    _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                };
                break;
            }
            default:
                Console.WriteLine($"Invalid property name: {propertyName}");
                return;
        }

        var matchedBooks = _books.Where(predicate).ToList();
        if (!matchedBooks.Any())
            Console.WriteLine("No books found.");
        PrintItems(matchedBooks);
    }

    public void SearchBoardgames(string searchTerm, string operatorType, string propertyName)
    {
        Func<KeyValuePair<int, string>, bool> predicate;

        switch (propertyName)
        {
            case "title":
                predicate = kvp =>
                {
                    var boardgameDetails = kvp.Value.Split(',');
                    var title = boardgameDetails[0].Trim().Trim('"');
                    return title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
                };
                break;
            case "minPlayers":
            case "maxPlayers":
            case "difficulty":
            {
                if (!int.TryParse(searchTerm, out var value))
                {
                    Console.WriteLine("Invalid format for numeric property.");
                    return;
                }

                var propertyIndex = propertyName switch
                {
                    "minplayers" => 1,
                    "maxplayers" => 2,
                    "difficulty" => 3,
                    _ => throw new ArgumentException($"Invalid property name: {propertyName}")
                };

                predicate = operatorType switch
                {
                    "=" => kvp =>
                    {
                        var boardgameDetails = kvp.Value.Split(',');
                        return int.TryParse(boardgameDetails[propertyIndex].Trim(), out var propertyValue) &&
                               propertyValue == value;
                    },
                    "<" => kvp =>
                    {
                        var boardgameDetails = kvp.Value.Split(',');
                        return int.TryParse(boardgameDetails[propertyIndex].Trim(), out var propertyValue) &&
                               propertyValue < value;
                    },
                    ">" => kvp =>
                    {
                        var boardgameDetails = kvp.Value.Split(',');
                        return int.TryParse(boardgameDetails[propertyIndex].Trim(), out var propertyValue) &&
                               propertyValue > value;
                    },
                    _ => throw new ArgumentException($"Invalid operator: {operatorType}")
                };
                break;
            }
            case "author":
                predicate = kvp =>
                {
                    var boardgameDetails = kvp.Value.Split(',');
                    var authors = boardgameDetails[4].Trim()
                        .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    return authors.Any(author => author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                };
                break;
            default:
                Console.WriteLine($"Invalid property name: {propertyName}");
                return;
        }

        var matchedBoardgames = _boardgames.Where(predicate).ToList();
        if (!matchedBoardgames.Any())
            Console.WriteLine("No boardgames found.");
        PrintItems(matchedBoardgames);
    }


    private static void PrintItems(List<KeyValuePair<int, string>> items)
    {
        foreach (var kvp in items)
            Console.WriteLine(kvp.Value);
    }


    public void AddBook(string title, IEnumerable<int> authorKeys, int year, int pageCount)
    {
        var authorNames = GetAuthorNames(authorKeys);
        if (!authorNames.Any()) return;
        var authors = authorNames.Count > 1 ? $"{string.Join(", ", authorNames)}" : authorNames[0];
        var value = $"{title}, {authors}, {year}, {pageCount}";
        var key = (title + string.Join("", authorNames) + year + pageCount).GetHashCode();
        _books.Add(key, value);
    }

    public void AddBoardgame(string title, int minPlayers, int maxPlayers, int difficulty, IEnumerable<int> authorKeys)
    {
        var authorNames = GetAuthorNames(authorKeys);
        if (!authorNames.Any()) return;
        var authors = authorNames.Count > 1 ? $"{string.Join(", ", authorNames)}" : authorNames[0];
        var value = $"\"{title}\", {minPlayers}, {maxPlayers}, {difficulty}, {authors}";
        var key = (title + minPlayers + maxPlayers + difficulty + string.Join("", authorNames)).GetHashCode();
        _boardgames.Add(key, value);
    }

    private List<string> GetAuthorNames(IEnumerable<int> authorKeys)
    {
        return authorKeys
            .Select(authorKey =>
                _authors.TryGetValue(authorKey, out var authorInfo) ? authorInfo.Split(',')[0].Trim() : null)
            .Where(authorName => !string.IsNullOrEmpty(authorName))
            .ToList()!;
    }
    
    public void AddBook(Book3 title)
    {
        var authorNames = (title.Authors.Select(author => author?.Name)).ToList() ;
        if (!authorNames.Any()) return;
        var authors = authorNames.Count > 1 ? $"{string.Join(", ", authorNames)}" : authorNames[0];
        var value = $"{title.Title}, {authors}, {title.Year}, {title.PageCount}";
        var key = (title.Title + string.Join("", authorNames) + title.Year + title.PageCount).GetHashCode();
        _books.Add(key, value);
    }
    
    public void AddBoardgame(Boardgame3 title)
    {
        var authorNames = (title.Authors.Select(author => author?.Name)).ToList() ;
        if (!authorNames.Any()) return;
        var authors = authorNames.Count > 1 ? $"{string.Join(", ", authorNames)}" : authorNames[0];
        var value = $"\"{title.Title}\", {title.MinPlayers}, {title.MaxPlayers}, {title.Difficulty}, {authors}";
        var key = (title.Title + title.MinPlayers + title.MaxPlayers + title.Difficulty + string.Join("", authorNames)).GetHashCode();
        _boardgames.Add(key, value);
    }
}