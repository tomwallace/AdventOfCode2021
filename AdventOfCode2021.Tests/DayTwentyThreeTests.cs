using AdventOfCode2021.TwentyThree;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayTwentyThreeTests
{
    [Fact]
    public void Solve()
    {
        string filePath = @"TwentyThree\DayTwentyThreeTestInputA.txt";
        var sut = new DayTwentyThree();
        var result = sut.FindLeastEnergy(sut.NormalInput(filePath, false));

        Assert.Equal(12521, result);
    }

    [Fact]
    public void Solve_Extended()
    {
        string filePath = @"TwentyThree\DayTwentyThreeTestInputA.txt";
        var sut = new DayTwentyThree();
        var result = sut.FindLeastEnergy(sut.NormalInput(filePath, true));

        Assert.Equal(44169, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyThree();
        var result = sut.PartA();

        Assert.Equal("16508", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyThree();
        var result = sut.PartB();

        Assert.Equal("43626", result);
    }
}