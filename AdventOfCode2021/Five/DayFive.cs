using AdventOfCode2021.Utility;

namespace AdventOfCode2021.Five;

public class DayFive : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Hydrothermal Venture";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 5;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Five\DayFiveInput.txt";
        var pointSets = FileUtility.ParseFileToList(filePath, line => new PointSet(line.Trim()));
        var map = new Map(pointSets, true);
        var count = map.FindCellsWithMinScore(2);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Five\DayFiveInput.txt";
        var pointSets = FileUtility.ParseFileToList(filePath, line => new PointSet(line.Trim()));
        var map = new Map(pointSets, false);
        var count = map.FindCellsWithMinScore(2);

        return count.ToString();
    }
}