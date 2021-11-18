using AdventOfCode2021.Console.Commands;

namespace AdventOfCode2021.Console;

public class CommandFactory : ICommandFactory
{
    private readonly VersionCommand _versionCommand;
    public CommandFactory(VersionCommand versionCommand)
    {
        _versionCommand = versionCommand ?? throw new ArgumentNullException(nameof(versionCommand));
    }
    
    /// <inheritdoc />
    public ICommand Build(string command)
    {
        string[]? commandSplit = command?.Split(' ');

        ICommand createdCommand;

        switch (commandSplit[0])
        {
            case "/help":
            case "/h":
                createdCommand = new HelpCommand();
                break;

            case "/version":
            case "/v":
                createdCommand = _versionCommand;
                break;

            case "/clear":
            case "/c":
                createdCommand = new ClearCommand();
                break;

            case "/quit":
            case "/q":
                createdCommand = new QuitCommand();
                break;

            case "/list":
            case "/l":
                createdCommand = new ListCommand();
                break;

            case "/run":
            case "/r":
                createdCommand = new RunCommand(commandSplit);
                break;

            default:
                createdCommand = new DefaultCommand();
                break;
        }

        return createdCommand;
    }
}