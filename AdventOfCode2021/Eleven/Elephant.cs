namespace AdventOfCode2021.Eleven;

public class Elephant
{
    private int value;

    public Elephant(char input)
    {
        value = int.Parse(input.ToString());
        HasFlashed = false;
        Neighbors = new List<Elephant>();
    }

    public int X { get; set; }

    public int Y { get; set; }

    public int Value()
    {
        return value;
    }

    public bool HasFlashed { get; set; }

    public List<Elephant> Neighbors { get; set; }

    public void IncreaseEnergy()
    {
        if (HasFlashed)
            return;

        value++;

        if (value > 9)
        {
            HasFlashed = true;
            Neighbors.ForEach(n => n.IncreaseEnergy());
        }
    }

    public int ResetAndReturnFlashedCount()
    {
        if (HasFlashed)
        {
            HasFlashed = false;
            value = 0;
            return 1;
        }

        return 0;
    }

    public override string ToString()
    {
        return $"{Y},{X}";
    }
}