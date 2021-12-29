using AdventOfCode2021.TwentyTwo;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayTwentyTwoTests
{
    [Theory]
    [InlineData(@"TwentyTwo\DayTwentyTwoTestInputA.txt", 39)]
    [InlineData(@"TwentyTwo\DayTwentyTwoTestInputB.txt", 590784)]
    public void CoresTurnedOn(string filePath, int expected)
    {
        var sut = new ReactorRefactored(filePath, true);
        var result = sut.GetTotalOn();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CoresTurnedOn_Full()
    {
        var filePath = @"TwentyTwo\DayTwentyTwoTestInputC.txt";
        var sut = new ReactorRefactored(filePath, false);
        var result = sut.GetTotalOn();

        Assert.Equal(2758514936282235, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyTwo();
        var result = sut.PartA();

        Assert.Equal("601104", result);
    }

    // 1262883317221163 too low
    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyTwo();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
}