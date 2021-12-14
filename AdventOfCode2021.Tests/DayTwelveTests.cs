using AdventOfCode2021.Twelve;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayTwelveTests
{
    [Theory]
    [InlineData(@"Twelve\DayTwelveTestInputA.txt", 10)]
    [InlineData(@"Twelve\DayTwelveTestInputB.txt", 19)]
    [InlineData(@"Twelve\DayTwelveTestInputC.txt", 226)]
    public void PathsThroughEnd(string filePath, int expected)
    {
        var sut = new CaveSystem(filePath);
        var result = sut.PathsThroughEnd(false);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(@"Twelve\DayTwelveTestInputA.txt", 36)]
    [InlineData(@"Twelve\DayTwelveTestInputB.txt", 103)]
    [InlineData(@"Twelve\DayTwelveTestInputC.txt", 3509)]
    public void PathsThroughEnd_AllowMultiple(string filePath, int expected)
    {
        var sut = new CaveSystem(filePath);
        var result = sut.PathsThroughEnd(true);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwelve();
        var result = sut.PartA();

        Assert.Equal("5756", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwelve();
        var result = sut.PartB();

        Assert.Equal("144603", result);
    }
}