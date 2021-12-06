using AdventOfCode2021.Six;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DaySixTests
{
    [Fact]
    public void ReproduceFishPopulation_18()
    {
        var sut = new DaySix();
        var result = sut.ReproduceFishPopulation(18, DaySix.TestInput);

        Assert.Equal(26, result);
    }

    [Fact]
    public void ReproduceFishPopulation_80()
    {
        var sut = new DaySix();
        var result = sut.ReproduceFishPopulation(80, DaySix.TestInput);

        Assert.Equal(5934, result);
    }

    [Fact]
    public void ReproduceFishPopulation_256()
    {
        var sut = new DaySix();
        var result = sut.ReproduceFishPopulation(256, DaySix.TestInput);

        Assert.Equal(26984457539, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySix();
        var result = sut.PartA();

        Assert.Equal("386755", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySix();
        var result = sut.PartB();

        Assert.Equal("1732731810807", result);
    }
}