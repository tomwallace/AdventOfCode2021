namespace AdventOfCode2021.Five;

public class Point
{
    public Point(string input)
    {
        var split = input.Split(',').Select(s => int.Parse(s)).ToArray();
        X = split[0];
        Y = split[1];
    }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }

    public int Y { get; set; }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}