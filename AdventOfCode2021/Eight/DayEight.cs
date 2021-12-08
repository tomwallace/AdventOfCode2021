using AdventOfCode2021.Utility;

namespace AdventOfCode2021.Eight;

public class DayEight : IAdventProblemSet
{
    public int[] UniqueDigits = new[] { 2, 4, 3, 7 };

    /// <inheritdoc />
    public string Description()
    {
        return "Seven Segment Search [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 8;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Eight\DayEightInput.txt";
        int count = CountUniqueNumbersInOutputs(filePath);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Eight\DayEightInput.txt";
        int total = TotalValuesOfAllAnalyses(filePath);

        return total.ToString();
    }

    public int CountUniqueNumbersInOutputs(string filePath)
    {
        var analyses = FileUtility.ParseFileToList(filePath, line => new Analysis(line));
        var count = analyses.Sum(a => a.Outputs.Count(o => UniqueDigits.Contains(o.Count())));
        return count;
    }

    public int TotalValuesOfAllAnalyses(string filePath)
    {
        var analyses = FileUtility.ParseFileToList(filePath, line => new Analysis(line));
        var total = analyses.Sum(a => a.CalculateOutputValue());
        return total;
    }
}