using AdventOfCode2021.Utility;

namespace AdventOfCode2021.Two;

public class DayTwo : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Dive!";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 2;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Two\DayTwoInput.txt";
        List<Instruction> instructions = FileUtility.ParseFileToList(filePath, line => new Instruction(line));
        var position = FollowInstructionsSimpleAndReturnPosition(instructions);

        return position.GetPositionProduct().ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Two\DayTwoInput.txt";
        List<Instruction> instructions = FileUtility.ParseFileToList(filePath, line => new Instruction(line));
        var position = FollowInstructionsWithAimAndReturnPosition(instructions);

        return position.GetPositionProduct().ToString();
    }

    public Position FollowInstructionsSimpleAndReturnPosition(List<Instruction> instructions)
    {
        var position = new Position();
        instructions.ForEach(i => position.ApplyInstructionSimple(i));

        return position;
    }

    public Position FollowInstructionsWithAimAndReturnPosition(List<Instruction> instructions)
    {
        var position = new Position();
        instructions.ForEach(i => position.ApplyInstructionWithAim(i));

        return position;
    }
}