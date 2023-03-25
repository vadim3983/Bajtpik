namespace Bajtpik.Bajtpik3;

public class Author3 : IAuthor3
{
    private readonly Dictionary<int, string> _author = new();
    private readonly int _birthYear;
    private readonly string _name;
    private readonly string _nickname;
    private readonly string _surname;

    public Author3(string name, string surname, int birthYear, string nickname)
    {
        _name = name;
        _surname = surname;
        _birthYear = birthYear;
        _nickname = nickname;

        _author[name.GetHashCode()] = name;
        _author[surname.GetHashCode()] = surname;
        _author[birthYear.ToString().GetHashCode()] = birthYear.ToString();
        if (!string.IsNullOrEmpty(nickname)) _author[nickname.GetHashCode()] = nickname;
    }

    public void PrintAuthor()
    {
        var name = _author[_name.GetHashCode()];
        var surname = _author[_surname.GetHashCode()];
        var birthYear = _author[_birthYear.ToString().GetHashCode()];
        var nickname = _author.ContainsKey(_nickname.GetHashCode()) ? _author[_nickname.GetHashCode()] : null;

        var output = $"{name} {surname}, {birthYear}";

        if (!string.IsNullOrEmpty(nickname)) output += $", {nickname}";

        Console.WriteLine(output);
    }

    public string GetName()
    {
        return _author[_name.GetHashCode()];
    }

    public string GetSurname()
    {
        return _author[_surname.GetHashCode()];
    }

    public int GetBirthYear()
    {
        return int.Parse(_author[_birthYear.ToString().GetHashCode()]);
    }
}