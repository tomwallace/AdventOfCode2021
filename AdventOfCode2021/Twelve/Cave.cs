namespace AdventOfCode2021.Twelve;

public class Cave
{
    public Cave(string input)
    {
        Id = input;
        IsStart = input == "start";
        IsEnd = input == "end";
        IsBig = input == input.ToUpper();

        Connected = new List<Cave>();
    }

    public string Id { get; }

    public List<Cave> Connected { get; set; }

    public bool IsStart { get; }

    public bool IsEnd { get; }

    public bool IsBig { get; }
}