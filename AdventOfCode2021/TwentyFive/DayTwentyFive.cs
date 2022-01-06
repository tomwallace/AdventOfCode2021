namespace AdventOfCode2021.TwentyFive;

public class DayTwentyFive : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Sea Cucumber";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 25;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"TwentyFive\DayTwentyFiveInput.txt";
        var seaFloor = new SeaFloor(filePath);
        var steps = seaFloor.StepUntilNoMoved();

        return steps.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        // There is no Part B for Day 25.  You get it by completing all of the other puzzles.
        return "Got all the stars!";
    }
}