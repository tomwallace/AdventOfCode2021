using AdventOfCode2021.Utility;
using System.Diagnostics;

namespace AdventOfCode2021.Nine;

public class Map
{
    private readonly List<Cell> lowPointCells;

    public Map(string filePath)
    {
        var cells = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().Select(c => new Cell(c)).ToList());
        Cells = cells.Select(c => c.ToArray()).ToArray();
        lowPointCells = new List<Cell>();

        AnalyzeNeighbors();
    }

    // Y,X is the coordinate
    public Cell[][] Cells { get; set; }

    public List<int> CalculateWatershedSizes()
    {
        var watershedSizes = new List<int>();
        // Use each lowPointCell to determine its watershed
        foreach (var lowPointCell in lowPointCells)
        {
            var watershedSize = 0;
            var seen = new HashSet<string>();
            var queue = new Queue<Cell>();
            queue.Enqueue(lowPointCell);

            do
            {
                var current = queue.Dequeue();
                seen.Add(current.ToString());
                watershedSize++;

                var neighbors = FindNeighbors(current.X, current.Y);
                foreach (var neighbor in neighbors.Where(n => !seen.Contains(n.ToString()) && n.Height < 9))
                {
                    // Ensure queue does not already contain the cell
                    if (!queue.Any(q => q.Id == neighbor.Id))
                        queue.Enqueue(neighbor);
                }
            } while (queue.Any());

            watershedSizes.Add(watershedSize);
        }

        return watershedSizes;
    }

    public int CalculateTotalRiskLevel()
    {
        var total = 0;
        for (int y = 0; y < Cells.Length; y++)
        {
            for (int x = 0; x < Cells[y].Length; x++)
            {
                var currentCell = Cells[y][x];
                if (currentCell.IsLowPoint)
                    total += currentCell.RiskLevel;
            }
        }

        return total;
    }

    public void Print()
    {
        Debug.WriteLine("");
        for (int y = 0; y < Cells.Length; y++)
        {
            for (int x = 0; x < Cells[y].Length; x++)
            {
                var currentCell = Cells[y][x];
                Debug.Write(currentCell.ToOutput());
            }

            Debug.WriteLine("");
        }
        Debug.WriteLine("");
    }

    private void AnalyzeNeighbors()
    {
        for (int y = 0; y < Cells.Length; y++)
        {
            for (int x = 0; x < Cells[y].Length; x++)
            {
                var currentCell = Cells[y][x];
                currentCell.X = x;
                currentCell.Y = y;
                var neighbors = FindNeighbors(x, y);
                if (neighbors.All(n => currentCell.Height < n.Height))
                {
                    currentCell.IsLowPoint = true;
                    lowPointCells.Add(currentCell);
                    currentCell.RiskLevel = currentCell.Height + 1;
                }
            }
        }
    }

    private List<Cell> FindNeighbors(int currentX, int currentY)
    {
        var neighbors = new List<Cell>();
        var xMods = new int[] { +1, -1 };
        var yMods = new int[] { +1, -1 };

        // No diagonals
        foreach (var xMod in xMods)
        {
            if (InRange(currentX + xMod, currentY))
                neighbors.Add(Cells[currentY][currentX + xMod]);
        }

        foreach (var yMod in yMods)
        {
            if (InRange(currentX, currentY + yMod))
                neighbors.Add(Cells[currentY + yMod][currentX]);
        }

        return neighbors;
    }

    private bool InRange(int x, int y)
    {
        return (x >= 0 && x < Cells[0].Length) && (y >= 0 && y < Cells.Length);
    }
}