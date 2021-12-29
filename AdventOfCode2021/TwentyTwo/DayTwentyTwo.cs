using AdventOfCode2021.Utility;

namespace AdventOfCode2021.TwentyTwo;

public class DayTwentyTwo : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Reactor Reboot";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 22;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
        var reactor = new Reactor(filePath, true);
        var turnedOn = reactor.CoresTurnedOn();

        return turnedOn.ToString();
    }

    // TODO: Still need to finish Part B
    // TODO: Clean up
    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
        var reactor = new ReactorRefactored(filePath, false);
        var turnedOn = reactor.GetTotalOn();

        return turnedOn.ToString();
    }

}

public class Reactor
{
    private Dictionary<string, bool> core;

    public Reactor(string filePath, bool onlyInitialization)
    {
        core = new Dictionary<string, bool>();
        var cubes = FileUtility.ParseFileToList(filePath, line => new Cube(line));

        foreach (var cube in cubes.Where(c => c.IsInitialization() == onlyInitialization))
        {
            for (int x = cube.MinX; x <= cube.MaxX; x++)
            {
                for (int y = cube.MinY; y <= cube.MaxY; y++)
                {
                    for (int z = cube.MinZ; z <= cube.MaxZ; z++)
                    {
                        string key = $"{x},{y},{z}";
                        if (cube.TurnOn)
                        {
                            if (!core.ContainsKey(key))
                                core[key] = true;
                        }
                        else
                        {
                            if (core.ContainsKey(key))
                                core.Remove(key);
                        }
                    }
                }
            }
        }
    }

    public long CoresTurnedOn()
    {
        return core.Count;
    }
}

public class ReactorRefactored
{
    private long totalOn;
    
    public ReactorRefactored(string filePath, bool onlyInitialization)
    {
        // I had to refactor the reactor mechanism because Part B clearly cannot step through each point, even
        // with a dictionary because of the volume of steps.
        // So now just track cubes, splitting them up as needed, and calculate volume of all
        var cubes = new Dictionary<Cube, int>();
        var steps = FileUtility.ParseFileToList(filePath, line => new Cube(line));

        foreach (var step in steps.Where(s => s.IsInitialization() == onlyInitialization))
        {
            // Used to determine whether to add, subtract, or disregard a cube when totaling
            var sign = step.TurnOn ? 1 : -1;

            var newCubes = new Dictionary<Cube, int>();

            foreach (var cube in cubes)
            {
                var cubeKey = cube.Key;
                var cubeSign = cube.Value;

                // Determine if any overlap
                var tmpMinX = Math.Max(step.MinX, cubeKey.MinX);
                var tmpMaxX = Math.Min(step.MaxX, cubeKey.MaxX);
                var tmpMinY = Math.Max(step.MinY, cubeKey.MinY);
                var tmpMaxY = Math.Min(step.MaxY, cubeKey.MaxY);
                var tmpMinZ = Math.Max(step.MinZ, cubeKey.MinZ);
                var tmpMaxZ = Math.Min(step.MaxZ, cubeKey.MaxZ);

                var tmpCube = new Cube(tmpMinX, tmpMaxX, tmpMinY, tmpMaxY, tmpMinZ, tmpMaxZ);

                // Determine if we have an intersection
                if (tmpMinX <= tmpMaxX && tmpMinY <= tmpMaxY && tmpMinZ <= tmpMaxZ)
                    newCubes[tmpCube] = newCubes.GetValueOrDefault(tmpCube, 0) - cubeSign;
            }

            // Handle turning on
            if (sign == 1)
                newCubes[step] = newCubes.GetValueOrDefault(step, 0) + sign;

            //Add or update the value of the cubes.
            foreach(var cube in newCubes)
            {
                cubes[cube.Key] = cubes.GetValueOrDefault(cube.Key, 0) + cube.Value;
            }

        }

        totalOn = cubes.Sum(a =>
            (a.Key.MaxX - a.Key.MinX + 1L) * (a.Key.MaxY - a.Key.MinY + 1L) * (a.Key.MaxZ - a.Key.MinZ + 1L) * a.Value);
    }

    public long GetTotalOn()
    {
        return totalOn;
    }
}

public class Cube
{
    public Cube(string input)
    {
        // Ex: on x=-20..26,y=-36..17,z=-47..7
        var splitSpace = input.Split(' ');
        TurnOn = splitSpace[0] == "on";
        var splitComma = splitSpace[1].Split(',');

        // X
        var numbers = splitComma[0].Split('=')[1].Split(new string[] { ".." }, StringSplitOptions.None);
        MinX = int.Parse(numbers[0]);
        MaxX = int.Parse(numbers[1]);

        // Y
        numbers = splitComma[1].Split('=')[1].Split(new string[] { ".." }, StringSplitOptions.None);
        MinY = int.Parse(numbers[0]);
        MaxY = int.Parse(numbers[1]);

        // Z
        numbers = splitComma[2].Split('=')[1].Split(new string[] { ".." }, StringSplitOptions.None);
        MinZ = int.Parse(numbers[0]);
        MaxZ = int.Parse(numbers[1]);
    }

    public Cube(int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
    {
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
        MinZ = minZ;
        MaxZ = maxZ;
    }
    
    public bool TurnOn { get; set; }

    public int MinX { get; set; }

    public int MaxX { get; set; }

    public int MinY { get; set; }

    public int MaxY { get; set; }

    public int MinZ { get; set; }

    public int MaxZ { get; set; }

    public bool IsInitialization()
    {
        return MinX >= -50 && MaxX <= 50 && MinY >= -50 && MaxY <= 50 && MinZ >= -50 && MinZ <= 50;
    }

    public override string ToString()
    {
        return $"{MinX},{MaxX},{MinY},{MaxY},{MinZ},{MaxZ}";
    }

    public override bool Equals(object obj)
    {
        CubeComparer comparer = new CubeComparer();
        return comparer.Equals(this, (Cube)obj);
    }

    public override int GetHashCode()
    {
        CubeComparer comparer = new CubeComparer();
        return comparer.GetHashCode(this);
    }
}

public class CubeComparer : IEqualityComparer<Cube>
{
    public bool Equals(Cube c1, Cube c2)
    {
        if (c2 == null && c1 == null)
            return true;
        else if (c1 == null | c2 == null)
            return false;
        else if (c1.ToString() == c2.ToString())
            return true;
        else
            return false;
    }

    public int GetHashCode(Cube c)
    {
        return $"{c}".GetHashCode();
    }
}