namespace AdventOfCode2021.TwentyTwo;

public class CubeComparer : IEqualityComparer<Cube>
{
    public bool Equals(Cube c1, Cube c2)
    {
        if (c2 == null && c1 == null)
            return true;
        else if (c1 == null | c2 == null)
            return false;
        else if (c1.ToString() == c2.ToString())
            return true;
        else
            return false;
    }

    public int GetHashCode(Cube c)
    {
        return $"{c}".GetHashCode();
    }
}