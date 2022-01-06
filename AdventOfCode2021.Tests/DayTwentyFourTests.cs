using AdventOfCode2021.TwentyFour;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayTwentyFourTests
{
    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyFour();
        var result = sut.PartA();

        Assert.Equal("41299994879959", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyFour();
        var result = sut.PartB();

        Assert.Equal("11189561113216", result);
    }
}