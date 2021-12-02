namespace AdventOfCode2021.Two;

public class Instruction
{
    public Instruction(string input)
    {
        var split = input.Trim().Split(' ');
        Direction = split[0];
        Units = int.Parse(split[1]);
    }

    public string Direction { get; set; }

    public int Units { get; set; }
}