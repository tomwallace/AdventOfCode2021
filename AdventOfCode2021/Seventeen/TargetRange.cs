namespace AdventOfCode2021.Seventeen;

public class TargetRange
{
    public int MinX { get; set; }

    public int MaxX { get; set; }

    public int MinY { get; set; }

    public int MaxY { get; set; }

    public bool InTargetRange(int currentX, int currentY)
    {
        return currentX >= MinX && currentX <= MaxX && currentY >= MinY && currentY <= MaxY;
    }

    public bool PastTargetRange(int currentX, int currentY)
    {
        return currentX > MaxX || currentY < MinY;
    }
}