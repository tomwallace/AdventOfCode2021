namespace AdventOfCode2021.TwentyTwo;

public class DayTwentyTwo : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Reactor Reboot";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 22;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
        var reactor = new ReactorRefactored(filePath, true);
        var turnedOn = reactor.GetTotalOn();

        return turnedOn.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
        var reactor = new ReactorRefactored(filePath, false);
        var turnedOn = reactor.GetTotalOn();

        return turnedOn.ToString();
    }
}