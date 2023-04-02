﻿namespace Bajtpik;

public class Book : IPublication, IBook
{
    public List<Author?> Authors { get; set; }


    public void PrintBook()
    {
        Console.WriteLine(
            $"{Title} , {(Authors.Count > 1 ? "[" : "")}{string.Join(", ", Authors.Select(a => $"{a.Name} {a.Surname}"))}{(Authors.Count > 1 ? "]" : "")}, {Year} , {PageCount}");
    }

    public void PrintBookAuthorBornAfter1970()
    {
        if (Authors.Any(author => author.BirthYear > 1970)) PrintBook();
    }

    public string Title { get; set; }
    public int Year { get; set; }
    public int? PageCount { get; set; }
}