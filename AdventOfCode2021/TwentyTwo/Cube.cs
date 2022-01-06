using System.Text.RegularExpressions;

namespace AdventOfCode2021.TwentyTwo;

public class Cube
{
    public Cube(string input)
    {
        // Ex: on x=-20..26,y=-36..17,z=-47..7
        var splitSpace = input.Split(' ');
        TurnOn = splitSpace[0] == "on";
        // Looked up a RegEx to pull out any numbers from the string
        var numbers = Regex.Matches(input, @"-?\d+").ToList();

        // X
        MinX = int.Parse(numbers[0].Value);
        MaxX = int.Parse(numbers[1].Value);

        // Y
        MinY = int.Parse(numbers[2].Value);
        MaxY = int.Parse(numbers[3].Value);

        // Z
        MinZ = int.Parse(numbers[4].Value);
        MaxZ = int.Parse(numbers[5].Value);
    }

    public Cube(int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
    {
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
        MinZ = minZ;
        MaxZ = maxZ;
    }

    public bool TurnOn { get; set; }

    public int MinX { get; set; }

    public int MaxX { get; set; }

    public int MinY { get; set; }

    public int MaxY { get; set; }

    public int MinZ { get; set; }

    public int MaxZ { get; set; }

    public bool IsInitialization()
    {
        return MinX >= -50 && MaxX <= 50 && MinY >= -50 && MaxY <= 50 && MinZ >= -50 && MinZ <= 50;
    }

    public Cube Overlap(Cube c2)
    {
        var cube = new Cube(Math.Max(MinX, c2.MinX), Math.Min(MaxX, c2.MaxX),
            Math.Max(MinY, c2.MinY), Math.Min(MaxY, c2.MaxY),
            Math.Max(MinZ, c2.MinZ), Math.Min(MaxZ, c2.MaxZ));
        cube.TurnOn = !c2.TurnOn;

        return cube;
    }

    public bool IsValid()
    {
        return MinX <= MaxX
               && MinY <= MaxY
               && MinZ <= MaxZ;
    }

    public long GetCubeVolume()
    {
        return (MaxX - MinX + 1L) * (MaxY - MinY + 1L) * (MaxZ - MinZ + 1L);
    }

    public override string ToString()
    {
        return $"{(TurnOn ? "On:" : "Off:")}{MinX},{MaxX},{MinY},{MaxY},{MinZ},{MaxZ}";
    }

    public override bool Equals(object obj)
    {
        CubeComparer comparer = new CubeComparer();
        return comparer.Equals(this, (Cube)obj);
    }

    public override int GetHashCode()
    {
        CubeComparer comparer = new CubeComparer();
        return comparer.GetHashCode(this);
    }
}