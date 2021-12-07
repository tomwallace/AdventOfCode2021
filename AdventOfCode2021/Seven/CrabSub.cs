namespace AdventOfCode2021.Seven;

public class CrabSub
{
    public CrabSub(int location)
    {
        Location = location;
    }

    public int Location { get; }

    public int MoveCostInFuel(int targetLocation, bool applyFuelStepIncrease)
    {
        if (!applyFuelStepIncrease)
            return Math.Abs(targetLocation - Location);

        // Fuel cost increases by 1 for every step taken
        var numberOfSteps = Math.Abs(targetLocation - Location);
        var stepCosts = Enumerable.Range(0, numberOfSteps).ToArray();
        return numberOfSteps + stepCosts.Sum();
    }
}