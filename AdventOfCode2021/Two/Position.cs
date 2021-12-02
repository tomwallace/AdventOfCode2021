namespace AdventOfCode2021.Two;

public class Position
{
    public Position()
    {
        Horizontal = 0;
        Depth = 0;
        Aim = 0;
    }

    public int Horizontal { get; set; }

    public int Depth { get; set; }

    public int Aim { get; set; }

    public void ApplyInstructionSimple(Instruction instruction)
    {
        switch (instruction.Direction)
        {
            case "forward":
                Horizontal += instruction.Units;
                break;

            case "down":
                Depth += instruction.Units;
                break;

            case "up":
                Depth -= instruction.Units;
                break;

            default:
                throw new ArgumentException($"Unrecognized instruction.Direction of {instruction.Direction}");
        }
    }

    public void ApplyInstructionWithAim(Instruction instruction)
    {
        switch (instruction.Direction)
        {
            case "forward":
                Horizontal += instruction.Units;
                Depth += Aim * instruction.Units;
                break;

            case "down":
                Aim += instruction.Units;
                break;

            case "up":
                Aim -= instruction.Units;
                break;

            default:
                throw new ArgumentException($"Unrecognized instruction.Direction of {instruction.Direction}");
        }
    }

    public int GetPositionProduct()
    {
        return Horizontal * Depth;
    }
}