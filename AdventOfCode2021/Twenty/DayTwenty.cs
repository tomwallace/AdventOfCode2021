namespace AdventOfCode2021.Twenty;

public class DayTwenty : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Trench Map [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 20;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Twenty\DayTwentyInput.txt";
        var image = new Image(filePath);
        image.ApplyEnhancement(2);

        return image.CountLitPixels().ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Twenty\DayTwentyInput.txt";
        var image = new Image(filePath);
        image.ApplyEnhancement(50);

        return image.CountLitPixels().ToString();
    }
}