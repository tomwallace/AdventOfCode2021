namespace AdventOfCode2021.Twelve;

public class DayTwelve : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Passage Pathing";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 12;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Twelve\DayTwelveInput.txt";
        var caveSystem = new CaveSystem(filePath);
        var count = caveSystem.PathsThroughEnd(false);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Twelve\DayTwelveInput.txt";
        var caveSystem = new CaveSystem(filePath);
        var count = caveSystem.PathsThroughEnd(true);

        return count.ToString();
    }
}