namespace AdventOfCode2021.Thirteen;

public class Dot
{
    public Dot(string input)
    {
        var split = input.Split(",");

        X = int.Parse(split[0]);
        Y = int.Parse(split[1]);
    }

    public int X { get; set; }

    public int Y { get; set; }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}