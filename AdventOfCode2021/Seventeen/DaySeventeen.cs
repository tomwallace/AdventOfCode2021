namespace AdventOfCode2021.Seventeen;

public class DaySeventeen : IAdventProblemSet
{
    // target area: x=117..164, y=-140..-89
    public readonly TargetRange Input = new TargetRange() { MinX = 117, MaxX = 164, MinY = -140, MaxY = -89 };

    /// <inheritdoc />
    public string Description()
    {
        return "Trick Shot";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 17;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var successfulShots = DetermineHighestElevation(Input);
        var highest = successfulShots.Select(s => s.Value).Max(s => s);
        return highest.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var successfulShots = DetermineHighestElevation(Input);
        var count = successfulShots.Count;
        return count.ToString();
    }

    public Dictionary<string, int> DetermineHighestElevation(TargetRange targetRange)
    {
        var maxElevations = new Dictionary<string, int>();
        // Had to look up the math on this one, as the brute force method was taking too long
        var minVelocity = (int)Math.Floor((1 + Math.Sqrt(1 + targetRange.MinX * 8)) / 2);
        for (int x = minVelocity; x <= targetRange.MaxX; x++)
        {
            for (int y = targetRange.MinY; y <= (targetRange.MinY * -1); y++)
            {
                var probeShot = new ProbeShot(x, y, targetRange);

                if (probeShot.DidItHit())
                    maxElevations.Add($"{x},{y}", probeShot.MaxY);
            }
        }

        return maxElevations;
    }
}