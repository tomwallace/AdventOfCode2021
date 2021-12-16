using AdventOfCode2021.Utility;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode2021.Fifteen;

public class Cave
{
    public Cave(string filePath, bool compositeCave)
    {
        if (!compositeCave)
            Grid = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().Select(c => new Cell(c)).ToList());
        else
        {
            // Build a compositeCave which is 5x5 grids pieced together, with each grid getting a bonus difficulty
            var lines = FileUtility.ParseFileToList(filePath, line => line);

            Grid = new List<List<Cell>>();

            for (int yGridMod = 0; yGridMod < 5; yGridMod++)
            {
                for (int r = 0; r < lines.Count; r++)
                {
                    var row = new List<Cell>();

                    for (int xGridMod = 0; xGridMod < 5; xGridMod++)
                    {
                        for (int c = 0; c < lines[r].Length; c++)
                        {
                            int value = int.Parse(lines[r][c].ToString()) + xGridMod + yGridMod;
                            row.Add(new Cell(value > 9 ? value - 9 : value));
                        }
                    }

                    Grid.Add(row);
                }
            }
        }

        Print();

        // Assign coordinates
        for (var y = 0; y < Grid.Count; y++)
        {
            for (var x = 0; x < Grid[0].Count; x++)
            {
                Grid[y][x].X = x;
                Grid[y][x].Y = y;
            }
        }
    }

    public List<List<Cell>> Grid { get; }

    public void Print()
    {
        Debug.WriteLine("");

        for (var y = 0; y < Grid.Count; y++)
        {
            var builder = new StringBuilder();

            for (var x = 0; x < Grid[0].Count; x++)
            {
                builder.Append(Grid[y][x].Value);
            }

            Debug.WriteLine(builder.ToString());
        }

        Debug.WriteLine("");
    }

    public int FindLowestRiskScore()
    {
        var target = Grid[Grid.Count - 1][Grid[0].Count - 1];

        var queue = new PriorityQueue<Cell, int>();
        var start = Grid[0][0];
        start.DifficultyScore = 0;
        queue.Enqueue(start, 0);

        do
        {
            // My original solution, which used a dictionary to keep the lowest score to reach a given Cell
            // and discard any higher score that reached the same cell, worked fine for Part A, but failed
            // to be performant enough in Part B.  Refactoring now to use the Dijkstra Algorithm after looking
            // around and some other solutions.
            // https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
            var current = queue.Dequeue();

            // If we have visited the Cell already, we can ignore based on the algorithm and priority queue
            if (current.HasBeenVisited)
                continue;

            current.HasBeenVisited = true;

            // Exit condition, where the difficulty score was already set as a neighbor
            if (current == target)
                return target.DifficultyScore;

            // Now handle the neighbors
            foreach (var neighbor in FindNeighbors(current))
            {
                var alternateOption = current.DifficultyScore + neighbor.Value;
                if (alternateOption < neighbor.DifficultyScore)
                {
                    // If the alternate is better, use that score
                    neighbor.DifficultyScore = alternateOption;
                }

                // Only enqueue the neighbor if it has been visited.  Use the DifficultyScore to
                // set queue priority so we always get the best choice first
                if (neighbor.DifficultyScore != int.MaxValue)
                {
                    queue.Enqueue(neighbor, neighbor.DifficultyScore);
                }
            }
        } while (queue.Count > 0);

        // Should not reach here
        throw new Exception("Should never reach here");
    }

    private List<Cell> FindNeighbors(Cell current)
    {
        var neighbors = new List<Cell>();
        var xMods = new int[] { +1, -1 };
        var yMods = new int[] { +1, -1 };

        foreach (var xMod in xMods)
        {
            if (InRange(current.X + xMod, current.Y))
                neighbors.Add(Grid[current.Y][current.X + xMod]);
        }

        foreach (var yMod in yMods)
        {
            if (InRange(current.X, current.Y + yMod))
                neighbors.Add(Grid[current.Y + yMod][current.X]);
        }

        return neighbors;
    }

    private bool InRange(int x, int y)
    {
        return (x >= 0 && x < Grid[0].Count) && (y >= 0 && y < Grid.Count);
    }
}