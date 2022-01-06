using AdventOfCode2021.Utility;

namespace AdventOfCode2021.TwentyTwo;

public class ReactorRefactored
{
    private readonly long totalOn;

    public ReactorRefactored(string filePath, bool onlyInitialization)
    {
        // I had to refactor the reactor mechanism because Part B clearly cannot step through each point, even
        // with a dictionary because of the volume of steps.
        // So now just track cubes, splitting them up as needed, and calculate volume of all
        var cubes = new List<Cube>();
        var steps = FileUtility.ParseFileToList(filePath, line => new Cube(line));

        if (onlyInitialization)
            steps = steps.Where(s => s.IsInitialization()).ToList();

        foreach (var step in steps)
        {
            // Create new cubes for the overlap and add any that are valid
            cubes.AddRange(
                cubes.Select(c => step.Overlap(c))
                    .Where(o => o.IsValid())
                    .ToList()
            );

            // If the step is turned on, add it too
            if (step.TurnOn)
                cubes.Add(step);
        }

        // Take the volume of all cubes, making sure to subtract those that are turned off
        totalOn = cubes.Sum(c => c.GetCubeVolume() * (c.TurnOn ? 1 : -1));
    }

    public long GetTotalOn()
    {
        return totalOn;
    }
}