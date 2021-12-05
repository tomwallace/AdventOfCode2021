using AdventOfCode2021.Four;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayFourTests
{
    [Fact]
    public void FindWinningBoardAndReturnScore()
    {
        string filePath = @"Four\DayFourTestInputA.txt";

        var sut = new DayFour();
        var result = sut.FindWinningBoardAndReturnScore(filePath);

        Assert.Equal(4512, result);
    }

    [Fact]
    public void FindLastBoardToWinsScore()
    {
        string filePath = @"Four\DayFourTestInputA.txt";

        var sut = new DayFour();
        var result = sut.FindLastBoardToWinsScore(filePath);

        Assert.Equal(1924, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFour();
        var result = sut.PartA();

        Assert.Equal("11774", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFour();
        var result = sut.PartB();

        Assert.Equal("4495", result);
    }
}