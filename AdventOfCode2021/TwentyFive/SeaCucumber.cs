namespace AdventOfCode2021.TwentyFive;

public class SeaCucumber
{
    public SeaCucumber(int x, int y, char c)
    {
        X = x;
        Y = y;
        MoveEast = c == '>';
        Moved = false;
    }

    public int X { get; set; }

    public int Y { get; set; }

    public bool MoveEast { get; }

    public bool Moved { get; set; }
}