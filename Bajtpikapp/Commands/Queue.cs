using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Bajtpikapp.Commands;

namespace Bajtpikapp;

public class Queue : Command
{
    public List<Command> commands;

    public Queue(Data data, string[] arguments) : base(data, arguments)
    {
        commands = new List<Command>();
    }

    public override void Execute()
    {
        var firstArgument = args[1];

        switch (args.Length)
        {
            case 2:
                ExecuteCommandWithTwoArgs(firstArgument);
                break;
            case >= 3 and < 5:
                ExecuteCommandWithThreeOrFourArgs(firstArgument);
                break;
            default:
                Console.WriteLine("Invald number of arguments");
                break;
        }
    }

    private void ExecuteCommandWithTwoArgs(string firstArgument)
    {
        switch (firstArgument)
        {
            case "commit":
                CommitCommands();
                break;
            case "print":
                PrintCommands();
                break;
            case "dismiss":
                DismissCommands();
                break;
            default:
                Console.WriteLine("Invalid arguments");
                break;
        }
    }

    private void ExecuteCommandWithThreeOrFourArgs(string firstArgument)
    {
        string filename;
        string format;

        switch (firstArgument)
        {
            case "export":
                filename = args[2];
                format = args.Length == 3 ? "xml" : args[3];
                Export(filename, format);
                break;
            case "import":
                filename = args[2];
                Import(filename);
                break;
            default:
                Console.WriteLine("Invalid arguments");
                break;
        }
    }

    private void CommitCommands()
    {
        foreach (var c in commands)
            c.Execute();

        commands.Clear();
    }

    private void PrintCommands()
    {
        foreach (var c in commands)
            Console.WriteLine(c);
    }

    private void DismissCommands()
    {
        commands.Clear();
    }

    private void Export(string filename, string format)
    {
        switch (format)
        {
            case "xml":
                ExportAsXml(filename);
                break;
            case "txt":
                ExportAsTxt(filename);
                break;
            default:
                Console.WriteLine("Invalid format");
                break;
        }
    }

    private void ExportAsXml(string filename)
    {
        var file = new FileInfo(filename + ".xml");
        using var sw = file.AppendText();

        var writer = new XmlSerializer(typeof(List<Command>), new XmlRootAttribute("Commands"));
        writer.Serialize(sw, commands);
    }

    private void ExportAsTxt(string filename)
    {
        using TextWriter tw = new StreamWriter(filename + ".txt");

        foreach (var c in commands)
            tw.WriteLine(c);
    }

    private void Import(string filename)
    {
        var ext = filename.Split('.')[1];

        switch (ext)
        {
            case "xml":
                ImportFromXml(filename);
                break;
            case "txt":
                ImportFromTxt(filename);
                break;
            default:
                Console.WriteLine("Invalid file format");
                break;
        }
    }

    private void ImportFromXml(string filename)
    {
        using var reader = new StreamReader(filename);

        var deserializer = new XmlSerializer(typeof(List<Command>), new XmlRootAttribute("Commands"));
        commands = (List<Command>)deserializer.Deserialize(reader);
    }

    private void ImportFromTxt(string filename)
    {
        var lines = File.ReadAllLines(filename);

        foreach (var line in lines)
            AddCommandFromLine(line);
    }

    private void AddCommandFromLine(string line)
    {
        var arg = line.Split(' ');

        var commandType = arg[0];

        switch (commandType)
        {
            case "exit":
                commands.Add(new ExitCommand(Data, arg));
                break;
            case "add":
                commands.Add(new AddCommand(Data, arg, arg[1], arg[2]));
                break;
            case "delete":
            case "list":
            case "find":
            case "edit":
                HandleComplexCommand(line, commandType);
                break;
            default:
                Console.WriteLine("Unknown command");
                break;
        }
    }

    private void HandleComplexCommand(string line, string commandType)
    {
        var regex = CommandRegex();
        var match = regex.Match(line ?? throw new InvalidOperationException());

        if (!match.Success)
        {
            Console.WriteLine("Invalid command format.");
            return;
        }

        var className = match.Groups["className"].Value;

        switch (commandType)
        {
            case "list":
                commands.Add(new ListCommand(Data, new[] { commandType, className }));
                break;
            case "find":
            case "delete":
            case "edit":
                HandlePropertyBasedCommand(match, commandType, className);
                break;
        }
    }

    private void HandlePropertyBasedCommand(Match match, string commandType, string className)
    {
        var property = match.Groups["property"].Value;
        var operatorType = match.Groups["operator"].Value;
        var searchTerm = match.Groups["searchTerm"].Value;
        var args = new[] { commandType, className, property, operatorType, searchTerm };

        if (args[4].StartsWith('"') && args[4].EndsWith('"'))
            args[4] = args[4].Substring(1, args[4].Length - 2);

        switch (commandType)
        {
            case "find":
                commands.Add(new FindCommand(Data, args));
                break;
            case "delete":
                commands.Add(new DeleteCommand(Data, args));
                break;
            case "edit":
                commands.Add(new EditCommand(Data, args));
                break;
        }
    }

    private static Regex CommandRegex()
    {
        return new Regex(
            "^(?<command>\\w+)(?:\\s+(?<className>\\w+)(?:\\s+(?<property>\\w+)\\s+(?<operator>[=<>])\\s+(?<searchTerm>.+)|\\s+(?<representation>base|secondary))?)?$|^(?<command>edit)\\s+(?<className>\\w+)$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
    }
}