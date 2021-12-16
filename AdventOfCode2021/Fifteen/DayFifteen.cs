using System.Diagnostics;
using System.Text;
using AdventOfCode2021.Utility;

namespace AdventOfCode2021.Fifteen;

public class DayFifteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Chiton";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 15;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Fifteen\DayFifteenInput.txt";
        var cave = new Cave(filePath, false);
        var lowestRiskScore = cave.FindLowestRiskScore();
        
        return lowestRiskScore.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Fifteen\DayFifteenInput.txt";
        var cave = new Cave(filePath, true);
        var lowestRiskScore = cave.FindLowestRiskScore();
        
        return lowestRiskScore.ToString();
    }
}

// TODO: Refactor to see if we can have a Grid shared class that has some of this logic - Grid<T>
public class Cave
{
    public Cave(string filePath, bool compositeCave)
    {
        if (!compositeCave)
            Grid = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().Select(c => new Cell(c)).ToList());
        else
        {
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
        
        var queue = new Queue<PathStep>();
        var step = new PathStep();
        step.Current = Grid[0][0];
        queue.Enqueue(step);

        var cellMinScore = new Dictionary<string, int>();

        do
        {
            var current = queue.Dequeue();

            // Check to see if have been here more efficiently
            if (cellMinScore.ContainsKey(current.Current.ToString()) &&
                cellMinScore[current.Current.ToString()] <= current.RiskScore)
                continue;
            else
            {
                if (cellMinScore.ContainsKey(current.Current.ToString()))
                    cellMinScore[current.Current.ToString()] = current.RiskScore;
                else
                    cellMinScore.Add(current.Current.ToString(), current.RiskScore);
            }

            // Check to see if we have reached our destination
            if (current.Current.ToString() == target.ToString())
            {
                continue;
            }

            current.Visited.Add(current.Current.ToString());

            foreach (var neighbor in FindNeighbors(current.Current))
            {
                // Check to see if we have been to the neighbor more efficiently too
                if (cellMinScore.ContainsKey(neighbor.ToString()) &&
                    cellMinScore[neighbor.ToString()] <= current.RiskScore + neighbor.Value)
                    continue;
                
                if (current.Visited.All(v => v != neighbor.ToString()))
                {
                    var nextStep = new PathStep(current.Visited, current.RiskScore + neighbor.Value, current.Steps + 1, neighbor);
                    queue.Enqueue(nextStep);
                }
            }

        } while (queue.Any());

        return cellMinScore[target.ToString()];
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

public class Cell
{
    public Cell(char input)
    {
        Value = int.Parse(input.ToString());
    }

    public Cell(int input)
    {
        Value = input;
    }

    public int X { get; set; }

    public int Y { get; set; }

    public int Value { get; }

    public override string ToString()
    {
        return $"{Y},{X}";
    }
}

public class PathStep
{
    public PathStep()
    {
        Visited = new HashSet<string>();
    }

    public PathStep(HashSet<string> visited, int riskScore, int steps, Cell current)
    {
        Visited = new HashSet<string>(visited);
        RiskScore = riskScore;
        Steps = steps;
        Current = current;
    }

    public Cell Current { get; set; }
    
    public HashSet<string> Visited { get; set; }

    public int RiskScore { get; set; }

    public int Steps { get; set; }
}