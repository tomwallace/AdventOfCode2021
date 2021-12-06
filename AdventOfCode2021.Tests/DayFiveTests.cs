using AdventOfCode2021.Five;
using AdventOfCode2021.Utility;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayFiveTests
{
    [Fact]
    public void FindCellsWithMinScore_IgnoreDiagonals()
    {
        var filePath = @"Five\DayFiveTestInputA.txt";
        var pointSets = FileUtility.ParseFileToList(filePath, line => new PointSet(line.Trim()));

        var sut = new Map(pointSets, true);
        var result = sut.FindCellsWithMinScore(2);

        Assert.Equal(5, result);
    }

    [Fact]
    public void FindCellsWithMinScore_Diagonals()
    {
        var filePath = @"Five\DayFiveTestInputA.txt";
        var pointSets = FileUtility.ParseFileToList(filePath, line => new PointSet(line.Trim()));

        var sut = new Map(pointSets, false);
        var result = sut.FindCellsWithMinScore(2);

        Assert.Equal(12, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFive();
        var result = sut.PartA();

        Assert.Equal("8622", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFive();
        var result = sut.PartB();

        Assert.Equal("22037", result);
    }
}