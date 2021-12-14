namespace AdventOfCode2021.Eleven;

public class DayEleven : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Dumbo Octopus";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 11;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Eleven\DayElevenInput.txt";
        var total = RunSteps(filePath, 100);
        return total.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Eleven\DayElevenInput.txt";
        var step = StepAllElephantsFlash(filePath);
        return step.ToString();
    }

    public int RunSteps(string filePath, int steps)
    {
        var cavernFloor = new CavernFloor(filePath);
        var totalFlashes = 0;

        for (var i = 0; i < steps; i++)
        {
            totalFlashes += cavernFloor.StepAndCountFlashes();
        }

        return totalFlashes;
    }

    public int StepAllElephantsFlash(string filePath)
    {
        var cavernFloor = new CavernFloor(filePath);
        var stepFlashCount = 0;
        var step = 0;
        do
        {
            step++;
            stepFlashCount = cavernFloor.StepAndCountFlashes();
        } while (stepFlashCount != cavernFloor.TotalNumberOfElephants);

        return step;
    }
}