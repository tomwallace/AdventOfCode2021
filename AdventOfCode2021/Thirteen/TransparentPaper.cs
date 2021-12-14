using AdventOfCode2021.Utility;
using System.Diagnostics;

namespace AdventOfCode2021.Thirteen;

public class TransparentPaper
{
    private List<Command> commands;
    private int minX;
    private int maxX;
    private int minY;
    private int maxY;

    public TransparentPaper(string filePath)
    {
        var lines = FileUtility.ParseFileToList(filePath, line => line);
        commands = new List<Command>();
        Dots = new Dictionary<string, Dot>();

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
                continue;

            if (line.Contains("fold along"))
                commands.Add(new Command(line));
            else
            {
                var dot = new Dot(line);
                Dots.Add(dot.ToString(), dot);
            }
        }

        minX = Dots.Select(d => d.Value.X).Min(d => d);
        maxX = Dots.Select(d => d.Value.X).Max(d => d);
        minY = Dots.Select(d => d.Value.Y).Min(d => d);
        maxY = Dots.Select(d => d.Value.Y).Max(d => d);
    }

    public Dictionary<string, Dot> Dots { get; set; }

    public void Print()
    {
        Debug.WriteLine("");

        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                if (Dots.ContainsKey($"{x},{y}"))
                    Debug.Write("#");
                else
                    Debug.Write(".");
            }

            Debug.WriteLine("");
        }

        Debug.WriteLine("");
    }

    public void ExecuteFolds(bool onlyDoFirstFold)
    {
        for (int i = 0; i < (onlyDoFirstFold ? 1 : commands.Count); i++)
        {
            if (commands[i].Operator == "y")
                FoldUp(commands[i].Amount);
            else
                FoldLeft(commands[i].Amount);
        }
    }

    public int CountDots()
    {
        return Dots.Count();
    }

    private void FoldUp(int foldY)
    {
        var dotsToTransform = Dots.Where(d => d.Value.Y > foldY)
            .Select(d => d.Value)
            .ToList();
        foreach (var dot in dotsToTransform)
        {
            Dots.Remove(dot.ToString());
            dot.Y = foldY - (dot.Y - foldY);
            if (!Dots.ContainsKey(dot.ToString()))
                Dots.Add(dot.ToString(), dot);
        }

        maxY = foldY - 1;
    }

    private void FoldLeft(int foldX)
    {
        var dotsToTransform = Dots.Where(d => d.Value.X > foldX)
            .Select(d => d.Value)
            .ToList();
        foreach (var dot in dotsToTransform)
        {
            Dots.Remove(dot.ToString());
            dot.X = foldX - (dot.X - foldX);
            if (!Dots.ContainsKey(dot.ToString()))
                Dots.Add(dot.ToString(), dot);
        }

        maxX = foldX - 1;
    }
}