namespace AdventOfCode2021.Nine;

public class Cell
{
    public Cell(char height)
    {
        Height = int.Parse(height.ToString());
        IsLowPoint = false;
        RiskLevel = 0;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public string Id => $"{Y},{X}";

    public int Height { get; }

    public bool IsLowPoint { get; set; }

    public int RiskLevel { get; set; }

    public override string ToString()
    {
        return Id;
    }

    public string ToOutput()
    {
        return IsLowPoint ? $"{Height}" : ".";
    }
}