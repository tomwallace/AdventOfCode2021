namespace AdventOfCode2021.Ten;

public class DayTen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Syntax Scoring";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 10;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Ten\DayTenInput.txt";
        var navSystem = new NavSystem(filePath);
        var score = navSystem.ScoreCorruptLines();

        return score.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Ten\DayTenInput.txt";
        var navSystem = new NavSystem(filePath);
        var score = navSystem.ScoreAutoComplete();

        return score.ToString();
    }
}