using AdventOfCode2021.TwentyFive;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayTwentyFiveTests
{
    [Fact]
    public void StepReturnMoved()
    {
        var filePath = @"TwentyFive\DayTwentyFiveTestInputA.txt";
        var sut = new SeaFloor(filePath);
        var result = sut.StepReturnMoved();

        Assert.Equal(24, result);
    }

    [Fact]
    public void StepUntilNoMoved()
    {
        var filePath = @"TwentyFive\DayTwentyFiveTestInputA.txt";
        var sut = new SeaFloor(filePath);
        var result = sut.StepUntilNoMoved();

        Assert.Equal(58, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyFive();
        var result = sut.PartA();

        Assert.Equal("456", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyFive();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
}