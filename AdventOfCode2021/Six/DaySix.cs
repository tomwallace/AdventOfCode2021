namespace AdventOfCode2021.Six;

public class DaySix : IAdventProblemSet
{
    public static List<int> TestInput = new List<int>() { 3, 4, 3, 1, 2 };
    public static List<int> ActualInput = new List<int>() { 1, 3, 4, 1, 5, 2, 1, 1, 1, 1, 5, 1, 5, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 2, 1, 5, 1, 1, 1, 1, 1, 4, 4, 1, 1, 4, 1, 1, 2, 3, 1, 5, 1, 4, 1, 2, 4, 1, 1, 1, 1, 1, 1, 1, 1, 2, 5, 3, 3, 5, 1, 1, 1, 1, 4, 1, 1, 3, 1, 1, 1, 2, 3, 4, 1, 1, 5, 1, 1, 1, 1, 1, 2, 1, 3, 1, 3, 1, 2, 5, 1, 1, 1, 1, 5, 1, 5, 5, 1, 1, 1, 1, 3, 4, 4, 4, 1, 5, 1, 1, 4, 4, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 3, 2, 1, 4, 1, 1, 4, 1, 5, 5, 1, 2, 2, 1, 5, 4, 2, 1, 1, 5, 1, 5, 1, 3, 1, 1, 1, 1, 1, 4, 1, 2, 1, 1, 5, 1, 1, 4, 1, 4, 5, 3, 5, 5, 1, 2, 1, 1, 1, 1, 1, 3, 5, 1, 2, 1, 2, 1, 3, 1, 1, 1, 1, 1, 4, 5, 4, 1, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 5, 1, 1, 4, 1, 5, 2, 4, 1, 1, 1, 2, 1, 1, 4, 4, 1, 2, 1, 1, 1, 1, 5, 3, 1, 1, 1, 1, 4, 1, 4, 1, 1, 1, 1, 1, 1, 3, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 5, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

    /// <inheritdoc />
    public string Description()
    {
        return "Lanternfish";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 6;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var fishPopulationSize = ReproduceFishPopulation(80, DaySix.ActualInput);
        return fishPopulationSize.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var fishPopulationSize = ReproduceFishPopulation(256, DaySix.ActualInput);
        return fishPopulationSize.ToString();
    }

    public long ReproduceFishPopulation(int days, List<int> startingFish)
    {
        var currentFishPerDay = GetNewFishPerDay();
        // Populate initial set
        for (var i = 6; i >= 0; i--)
        {
            currentFishPerDay[i] += startingFish.Count(f => f == i);
        }

        for (var day = 0; day < days; day++)
        {
            // Run through the simulation
            var newFishPerDay = GetNewFishPerDay();
            for (var i = 8; i >= 0; i--)
            {
                if (i == 0)
                {
                    newFishPerDay[8] = currentFishPerDay[i];
                    newFishPerDay[6] += currentFishPerDay[i];
                }
                else
                {
                    newFishPerDay[i - 1] = currentFishPerDay[i];
                }
            }

            currentFishPerDay = newFishPerDay;
        }

        return currentFishPerDay.Sum(f => f.Value);
    }

    private Dictionary<int, long> GetNewFishPerDay()
    {
        return new Dictionary<int, long>() { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 } };
    }
}