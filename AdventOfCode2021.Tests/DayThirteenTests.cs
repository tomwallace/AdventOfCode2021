using AdventOfCode2021.Thirteen;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayThirteenTests
{
    [Fact]
    public void PathsThroughEnd()
    {
        var filePath = @"Thirteen\DayThirteenTestInputA.txt";
        var sut = new TransparentPaper(filePath);
        sut.ExecuteFolds(true);
        var result = sut.CountDots();

        Assert.Equal(17, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayThirteen();
        var result = sut.PartA();

        Assert.Equal("610", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayThirteen();
        var result = sut.PartB();

        Assert.Equal("PZFJHRFZ", result);
    }
}