namespace AdventOfCode2021.Thirteen;

public class Command
{
    // fold along y=7
    public Command(string input)
    {
        var trimmed = input.Replace("fold along ", "");
        var split = trimmed.Split("=");
        Operator = split[0];
        Amount = int.Parse(split[1]);
    }

    public string Operator { get; set; }

    public int Amount { get; set; }
}