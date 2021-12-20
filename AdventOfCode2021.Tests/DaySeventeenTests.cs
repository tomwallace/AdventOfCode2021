using AdventOfCode2021.Seventeen;
using System.Linq;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DaySeventeenTests
{
    [Theory]
    [InlineData(7, 2, true)]
    [InlineData(6, 3, true)]
    [InlineData(9, 0, true)]
    [InlineData(17, -4, false)]
    public void ProbeShot_Hits(int x, int y, bool expected)
    {
        var target = new TargetRange() { MinX = 20, MaxX = 30, MinY = -10, MaxY = -5 };
        var sut = new ProbeShot(x, y, target);
        var result = sut.DidItHit();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void DetermineHighestElevation_Highest()
    {
        var target = new TargetRange() { MinX = 20, MaxX = 30, MinY = -10, MaxY = -5 };
        var sut = new DaySeventeen();
        var result = sut.DetermineHighestElevation(target);
        var highest = result.Select(s => s.Value).Max(s => s);

        Assert.Equal(45, highest);
    }

    [Fact]
    public void DetermineHighestElevation_IndividualShots()
    {
        var target = new TargetRange() { MinX = 20, MaxX = 30, MinY = -10, MaxY = -5 };
        var sut = new DaySeventeen();
        var result = sut.DetermineHighestElevation(target);

        Assert.Equal(112, result.Count);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySeventeen();
        var result = sut.PartA();

        Assert.Equal("9730", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySeventeen();
        var result = sut.PartB();

        Assert.Equal("4110", result);
    }
}