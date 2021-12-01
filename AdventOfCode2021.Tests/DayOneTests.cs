using AdventOfCode2021.One;
using AdventOfCode2021.Utility;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayOneTests
{
    [Fact]
    public void CountSweepIncreases()
    {
        string filePath = @"One\DayOneTestInputA.txt";
        List<int> sweeps = FileUtility.ParseFileToList(filePath, line => int.Parse(line.Trim()));

        var sut = new DayOne();
        var result = sut.CountSweepIncreases(sweeps);

        Assert.Equal(7, result);
    }

    [Fact]
    public void CountSweepGroupIncreases()
    {
        string filePath = @"One\DayOneTestInputA.txt";
        List<int> sweeps = FileUtility.ParseFileToList(filePath, line => int.Parse(line.Trim()));

        var sut = new DayOne();
        var result = sut.CountSweepGroupIncreases(sweeps);

        Assert.Equal(5, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayOne();
        var result = sut.PartA();

        Assert.Equal("1215", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayOne();
        var result = sut.PartB();

        Assert.Equal("1150", result);
    }
}