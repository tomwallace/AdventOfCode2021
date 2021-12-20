namespace AdventOfCode2021.Eighteen;

public class DayEighteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Snailfish";
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
        return "";
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Eighteen\DayEighteenInput.txt";
        return "";
    }
}

// TODO: Return to
public class SnailFishPair
{
    private int? one;
    private int? two;
    private SnailFishPair oneSnailFishPair;
    private SnailFishPair twoSnailFishPair;
    private SnailFishPair parent;

    public int GetOne()
    {
        if (one != null)
            return one.Value;


        return 0;
    }
}