using AdventOfCode2021.Seven;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DaySevenTests
{
    [Fact]
    public void FindFuelCostsForAllPositions()
    {
        var sut = new DaySeven();
        var result = sut.FindFuelCostsForAllPositions(DaySeven.TestInput, false);

        Assert.Equal(37, result[2]);
        Assert.Equal(41, result[1]);
        Assert.Equal(39, result[3]);
        Assert.Equal(71, result[10]);
    }

    [Fact]
    public void FindFuelCostsForAllPositions_WithProgressiveFuelIncreases()
    {
        var sut = new DaySeven();
        var result = sut.FindFuelCostsForAllPositions(DaySeven.TestInput, true);

        Assert.Equal(206, result[2]);
        Assert.Equal(168, result[5]);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySeven();
        var result = sut.PartA();

        Assert.Equal("336131", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySeven();
        var result = sut.PartB();

        Assert.Equal("92676646", result);
    }
}