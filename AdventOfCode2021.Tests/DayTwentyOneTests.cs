using AdventOfCode2021.TwentyOne;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayTwentyOneTests
{
    [Fact]
    public void GetWinningOutput()
    {
        var input = new List<int>() { 4, 8 };
        var sut = new DiceGame(input);
        sut.Play(1000);
        var result = sut.GetLoserOutput();

        Assert.Equal(739785, result);
    }

    [Fact]
    public void PlayRecursiveAndReturnWinningUniverses()
    {
        var input = new List<int>() { 4, 8 };
        var sut = new DiceGame(input);
        var result = sut.PlayRecursiveAndReturnWinningUniverses();

        Assert.Equal(444356092776315, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyOne();
        var result = sut.PartA();

        Assert.Equal("556206", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyOne();
        var result = sut.PartB();

        Assert.Equal("630797200227453", result);
    }
}