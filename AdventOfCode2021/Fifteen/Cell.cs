namespace AdventOfCode2021.Fifteen;

public class Cell
{
    public Cell(char input)
    {
        Value = int.Parse(input.ToString());
        HasBeenVisited = false;
        // Keeps it from being evaluated early
        DifficultyScore = int.MaxValue;
    }

    public Cell(int input)
    {
        Value = input;
        HasBeenVisited = false;
        // Keeps it from being evaluated early
        DifficultyScore = int.MaxValue;
    }

    public int X { get; set; }

    public int Y { get; set; }

    public int Value { get; }

    public override string ToString()
    {
        return $"{Y},{X}";
    }

    public bool HasBeenVisited { get; set; }

    public int DifficultyScore { get; set; }
}