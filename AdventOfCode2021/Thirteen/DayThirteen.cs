namespace AdventOfCode2021.Thirteen;

public class DayThirteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Transparent Origami";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 13;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Thirteen\DayThirteenInput.txt";
        var paper = new TransparentPaper(filePath);
        paper.ExecuteFolds(true);
        var count = paper.CountDots();

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Thirteen\DayThirteenInput.txt";
        var paper = new TransparentPaper(filePath);
        paper.ExecuteFolds(false);
        paper.Print();

        // Should be 8 capital letters - gotten from viewing the output
        return "PZFJHRFZ";
    }
}