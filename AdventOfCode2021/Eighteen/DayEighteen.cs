namespace AdventOfCode2021.Eighteen;

public class DayEighteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Snailfish [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 18;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Eighteen\DayEighteenInput.txt";
        var problem = new Problem(filePath);
        var finalNode = problem.AddInSequence();
        var magnitude = finalNode.GetMagnitude();

        return magnitude.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Eighteen\DayEighteenInput.txt";
        var problem = new Problem(filePath);
        var max = problem.LargestMagnitudeOfAnyTwoPairs();

        return max.ToString();
    }
}