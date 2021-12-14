using AdventOfCode2021.Utility;
using System.Diagnostics;

namespace AdventOfCode2021.Eleven;

public class CavernFloor
{
    public CavernFloor(string filePath)
    {
        Elephants = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().Select(c => new Elephant(c)).ToList());
        TotalNumberOfElephants = Elephants.Count * Elephants[0].Count;
        for (var y = 0; y < Elephants.Count; y++)
        {
            for (var x = 0; x < Elephants[0].Count; x++)
            {
                Elephants[y][x].X = x;
                Elephants[y][x].Y = y;
            }
        }

        for (var y = 0; y < Elephants.Count; y++)
        {
            for (var x = 0; x < Elephants[0].Count; x++)
            {
                Elephants[y][x].Neighbors = FindNeighbors(Elephants[y][x]);
            }
        }
    }

    public List<List<Elephant>> Elephants { get; set; }

    public int TotalNumberOfElephants { get; set; }

    public void Print()
    {
        Debug.WriteLine("");

        foreach (var row in Elephants)
        {
            foreach (var elephant in row)
            {
                Debug.Write(elephant.Value());
            }
            Debug.WriteLine("");
        }
    }

    public int StepAndCountFlashes()
    {
        var flashes = 0;

        // Increase
        for (var y = 0; y < Elephants.Count; y++)
        {
            for (var x = 0; x < Elephants[0].Count; x++)
            {
                Elephants[y][x].IncreaseEnergy();
            }
        }

        // Reset and count
        for (var y = 0; y < Elephants.Count; y++)
        {
            for (var x = 0; x < Elephants[0].Count; x++)
            {
                flashes += Elephants[y][x].ResetAndReturnFlashedCount();
            }
        }

        return flashes;
    }

    private List<Elephant> FindNeighbors(Elephant starting)
    {
        List<Elephant> neighbors = new List<Elephant>();
        var xMods = new int[] { +1, 0, -1 };
        var yMods = new int[] { +1, 0, -1 };
        foreach (var xMod in xMods)
        {
            foreach (var yMod in yMods)
            {
                var x = starting.X + xMod;
                var y = starting.Y + yMod;
                if (!(x == starting.X && y == starting.Y) && InRange(x, y))
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