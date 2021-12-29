using AdventOfCode2021.Utility;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode2021.TwentyFive;

public class SeaFloor
{
    private SeaCucumber?[,] floor;
    private int maxX;
    private int maxY;

    public SeaFloor(string filePath)
    {
        var lines = FileUtility.ParseFileToList(filePath, line => line);

        maxX = lines[0].Count();
        maxY = lines.Count;

        floor = new SeaCucumber?[maxY, maxX];

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var c = lines[y][x];
                floor[y, x] = c == '.' ? null : new SeaCucumber(x, y, c);
            }
        }

        //Print();
    }

    public void Print()
    {
        Debug.WriteLine("");
        for (int y = 0; y < maxY; y++)
        {
            var builder = new StringBuilder();
            for (int x = 0; x < maxX; x++)
            {
                builder.Append(floor[y, x] == null ? "." : floor[y, x].MoveEast ? ">" : "V");
            }
            Debug.WriteLine(builder.ToString());
        }
        Debug.WriteLine("");
    }

    public int StepReturnMoved()
    {
        var moved = 0;
        var newFloor = new SeaCucumber?[maxY, maxX];
        // Move east ones first
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var current = floor[y, x];
                if (current != null && current.MoveEast)
                {
                    var newX = GetNewX(x);
                    if (floor[y, newX] == null)
                    {
                        newFloor[y, newX] = current;
                        moved++;
                        current.Moved = true;
                    }
                    else
                    {
                        newFloor[y, x] = current;
                    }
                }
            }
        }

        // Remove all the moved ones
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var current = floor[y, x];
                if (current != null && current.Moved)
                {
                    current.Moved = false;
                    floor[y, x] = null;
                }
            }
        }

        // Now check south moving ones
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var current = floor[y, x];
                if (current != null && !current.MoveEast)
                {
                    var newY = GetNewY(y);
                    if (newFloor[newY, x] == null && floor[newY, x] == null)
                    {
                        newFloor[newY, x] = current;
                        moved++;
                    }
                    else
                    {
                        newFloor[y, x] = current;
                    }
                }
            }
        }

        floor = newFloor;

        return moved;
    }

    public int StepUntilNoMoved()
    {
        var step = 0;
        do
        {
            step++;

            //Debug.WriteLine($"Step {step}");

            var moves = StepReturnMoved();
            if (moves == 0)
                return step;

            //Print();
            //Debug.WriteLine("");
        } while (true);
    }

    private int GetNewX(int x)
    {
        x++;
        if (x >= maxX)
            return 0;

        return x;
    }

    private int GetNewY(int y)
    {
        y++;
        if (y >= maxY)
            return 0;

        return y;
    }
}