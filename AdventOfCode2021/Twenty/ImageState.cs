namespace AdventOfCode2021.Twenty;

public class ImageState
{
    public ImageState(int xMin, int xMax, int yMin, int yMax)
    {
        XMin = xMin;
        YMin = yMin;
        XMax = xMax;
        YMax = yMax;
    }

    public int XMin { get; set; }
    public int YMin { get; set; }
    public int XMax { get; set; }
    public int YMax { get; set; }
}