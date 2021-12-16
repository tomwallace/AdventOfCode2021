using AdventOfCode2021.Fifteen;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayFifteenTests
{
    [Fact]
    public void FindLowestRiskScore()
    {
        var filePath = @"Fifteen\DayFifteenTestInputA.txt";
        var sut = new Cave(filePath, false);
        var result = sut.FindLowestRiskScore();

        Assert.Equal(40, result);
    }

    [Fact]
    public void FindLowestRiskScore_CompositeCave()
    {
        var filePath = @"Fifteen\DayFifteenTestInputA.txt";
        var sut = new Cave(filePath, true);
        var result = sut.FindLowestRiskScore();

        Assert.Equal(315, result);
    }

    // Note: took 2.8 min
    /*
    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFifteen();
        var result = sut.PartA();

        Assert.Equal("745", result);
    }
    */

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFifteen();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
}