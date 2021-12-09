namespace AdventOfCode2021.Nine;

public class DayNine : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Smoke Basin";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 9;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Nine\DayNineInput.txt";
        var map = new Map(filePath);

        return map.CalculateTotalRiskLevel().ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Nine\DayNineInput.txt";
        var map = new Map(filePath);
        var sizes = map.CalculateWatershedSizes()
            .OrderByDescending(o => o)
            .ToList();
        var product = sizes[0] * sizes[1] * sizes[2];

        return product.ToString();
    }
}