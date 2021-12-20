using AdventOfCode2021.Eighteen;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayEighteenTests
{
    [Fact]
    public void FindWinningBoardAndReturnScore()
    {
        string filePath = @"Eighteen\DayEighteenTestInputA.txt";

        Assert.Equal(4512, -1);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayEighteen();
        var result = sut.PartA();

        Assert.Equal("-1", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayEighteen();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
}