namespace AdventOfCode2021.Console.Commands;

/// <summary>
/// Clears the current CLI screen
/// </summary>
public class ClearCommand : ICommand
{
    /// <inheritdoc />
    public void Execute()
    {
        System.Console.Clear();
    }

    /// <inheritdoc />
    public bool HadErrorInCreation()
    {
        return false;
    }
}