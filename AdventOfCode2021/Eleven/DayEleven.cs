using AdventOfCode2021.Utility;
using System.Diagnostics;

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

    // TODO: Return and work on
    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Elven\DayElevenInput.txt";
        return "";
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Eleven\DayElevenInput.txt";
        return "";
    }

    public int RunSteps(string filePath, int steps)
    {
        var cavernFloor = new CavernFloor(filePath);
        cavernFloor.Print();
        var totalFlashes = 0;

        for (var i = 0; i < steps; i++)
        {
            totalFlashes += cavernFloor.StepAndCountFlashes();
            cavernFloor.Print();
        }

        return totalFlashes;
    }
}

public class CavernFloor
{
    public CavernFloor(string filePath)
    {
        Elephants = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().Select(c => new Elephant(c)).ToList());
        for (var y = 0; y < Elephants.Count; y++)
        {
            for (var x = 0; x < Elephants[0].Count; x++)
            {
                Elephants[y][x].X = x;
                Elephants[y][x].Y = y;
            }
        }
    }

    public List<List<Elephant>> Elephants { get; set; }

    public void Print()
    {
        Debug.WriteLine("");

        foreach (var row in Elephants)
        {
            foreach (var elephant in row)
            {
                Debug.Write(elephant.Value);
            }
            Debug.WriteLine("");
        }
    }

    public int StepAndCountFlashes()
    {
        var flashes = 0;

        // Increase all by one
        for (var y = 0; y < Elephants.Count; y++)
        {
            for (var x = 0; x < Elephants[0].Count; x++)
            {
                Elephants[y][x].Value++;
            }
        }

        // Handle flashes
        for (var y = 0; y < Elephants.Count; y++)
        {
            for (var x = 0; x < Elephants[0].Count; x++)
            {
                var current = Elephants[y][x];
                var queue = new Queue<Elephant>();
                queue.Enqueue(current);
                do
                {
                    var selected = queue.Dequeue();
                    if (selected.Value > 9)
                    {
                        flashes++;
                        selected.Value = 0;
                        foreach (var elephant in FindNeighbors(selected))
                        {
                            elephant.Value++;
                            queue.Enqueue(elephant);
                        }
                    }
                } while (queue.Any());
            }
        }

        return flashes;
    }

    private List<Elephant> FindNeighbors(Elephant starting)
    {
        List<Elephant> neighbors = new List<Elephant>();
        var xMods = new int[] { +1, -1 };
        var yMods = new int[] { +1, -1 };
        foreach (var xMod in xMods)
        {
            foreach (var yMod in yMods)
            {
                if (xMod != 0 && yMod != 0 && InRange(starting.X + xMod, starting.Y + yMod))
                {
                    neighbors.Add(Elephants[starting.Y + yMod][starting.X + xMod]);
                }
            }
        }

        return neighbors;
    }

    private bool InRange(int x, int y)
    {
        return (x >= 0 && x < Elephants[0].Count) && (y >= 0 && y < Elephants.Count);
    }
}

public class Elephant
{
    public Elephant(char value)
    {
        Value = int.Parse(value.ToString());
    }

    public int X { get; set; }

    public int Y { get; set; }

    public int Value { get; set; }

    public override string ToString()
    {
        return $"{Y},{X}";
    }
}