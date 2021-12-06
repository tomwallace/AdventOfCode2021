namespace AdventOfCode2021.Five;

public class PointSet
{
    public PointSet(string input)
    {
        // Ex:  0,9 -> 5,9
        var split = input.Split(new string[] { " -> " }, StringSplitOptions.None).Select(s => new Point(s)).ToArray();
        One = split[0];
        Two = split[1];
    }

    public Point One { get; set; }

    public Point Two { get; set; }

    public bool IsDiagonal()
    {
        return !(One.X == Two.X || One.Y == Two.Y);
    }

    public int XModifier()
    {
        if (One.X == Two.X)
            return 0;

        return One.X < Two.X ? 1 : -1;
    }

    public int YModifier()
    {
        if (One.Y == Two.Y)
            return 0;

        return One.Y < Two.Y ? 1 : -1;
    }
}