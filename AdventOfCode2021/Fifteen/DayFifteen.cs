namespace AdventOfCode2021.Fifteen;

public class DayFifteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Chiton";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 15;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Fifteen\DayFifteenInput.txt";
        var cave = new Cave(filePath, false);
        var lowestRiskScore = cave.FindLowestRiskScore();

        return lowestRiskScore.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Fifteen\DayFifteenInput.txt";
        var cave = new Cave(filePath, true);
        var lowestRiskScore = cave.FindLowestRiskScore();

        return lowestRiskScore.ToString();
    }
}