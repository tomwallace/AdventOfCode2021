using AdventOfCode2021.Eleven;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayElevenTests
{
    [Fact]
    public void RunSteps()
    {
        var filePath = @"Eleven\DayElevenTestInputA.txt";
        var sut = new DayEleven();
        var result = sut.RunSteps(filePath, 100);

        Assert.Equal(1656, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayEleven();
        var result = sut.PartA();

        Assert.Equal("-1", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayEleven();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
}