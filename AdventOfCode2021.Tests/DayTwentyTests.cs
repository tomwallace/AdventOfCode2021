using AdventOfCode2021.Twenty;
using System;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayTwentyTests
{
    [Fact]
    public void SanityTest()
    {
        var input = "000100010";
        var result = Convert.ToInt32(input, 2);

        Assert.Equal(34, result);
    }

    [Fact]
    public void Image_CountLitPixels()
    {
        string filePath = @"Twenty\DayTwentyTestInputA.txt";
        var sut = new Image(filePath);
        sut.ApplyEnhancement(2);
        var result = sut.CountLitPixels();

        Assert.Equal(35, result);
    }

    [Fact]
    public void Image_CountLitPixels_50()
    {
        string filePath = @"Twenty\DayTwentyTestInputA.txt";
        var sut = new Image(filePath);
        sut.ApplyEnhancement(50);
        var result = sut.CountLitPixels();

        Assert.Equal(3351, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwenty();
        var result = sut.PartA();

        Assert.Equal("5663", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwenty();
        var result = sut.PartB();

        Assert.Equal("19638", result);
    }
}