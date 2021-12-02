using AdventOfCode2021.Two;
using AdventOfCode2021.Utility;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayTwoTests
{
    [Fact]
    public void FollowInstructionsSimpleAndReturnPosition()
    {
        string filePath = @"Two\DayTwoTestInputA.txt";
        List<Instruction> instructions = FileUtility.ParseFileToList(filePath, line => new Instruction(line));

        var sut = new DayTwo();
        var result = sut.FollowInstructionsSimpleAndReturnPosition(instructions);

        Assert.Equal(150, result.GetPositionProduct());
    }

    [Fact]
    public void FollowInstructionsWithAimAndReturnPosition()
    {
        string filePath = @"Two\DayTwoTestInputA.txt";
        List<Instruction> instructions = FileUtility.ParseFileToList(filePath, line => new Instruction(line));

        var sut = new DayTwo();
        var result = sut.FollowInstructionsWithAimAndReturnPosition(instructions);

        Assert.Equal(900, result.GetPositionProduct());
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwo();
        var result = sut.PartA();

        Assert.Equal("2150351", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwo();
        var result = sut.PartB();

        Assert.Equal("1842742223", result);
    }
}