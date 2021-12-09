using AdventOfCode2021.Nine;
using System.Linq;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayNineTests
{
    [Fact]
    public void CountUniqueNumbersInOutputs()
    {
        var filePath = @"Nine\DayNineTestInputA.txt";
        var sut = new Map(filePath);
        sut.Print();
        var result = sut.CalculateTotalRiskLevel();

        Assert.Equal(15, result);
    }

    [Fact]
    public void CalculateWatershedSizes()
    {
        var filePath = @"Nine\DayNineTestInputA.txt";
        var sut = new Map(filePath);
        var result = sut.CalculateWatershedSizes();
        var sorted = result.OrderByDescending(o => o).ToList();
        var product = sorted[0] * sorted[1] * sorted[2];

        Assert.Equal(4, sorted.Count());
        Assert.Equal(1134, product);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayNine();
        var result = sut.PartA();

        Assert.Equal("508", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayNine();
        var result = sut.PartB();

        Assert.Equal("1564640", result);
    }
}