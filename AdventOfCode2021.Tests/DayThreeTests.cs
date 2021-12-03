using AdventOfCode2021.Three;
using AdventOfCode2021.Utility;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayThreeTests
{
    [Fact]
    public void DetermineDiagnosticResults()
    {
        string filePath = @"Three\DayThreeTestInputA.txt";
        var lines = FileUtility.ParseFileToList(filePath, line => line.Trim().ToCharArray());

        var sut = new DayThree();
        var result = sut.DetermineDiagnosticResults(lines);

        Assert.Equal("01001", result.EpsilonRate);
        Assert.Equal("10110", result.GammaRate);
        Assert.Equal(198, result.GetPowerConsumption());
    }

    [Fact]
    public void CalculateLifeSupportRating()
    {
        string filePath = @"Three\DayThreeTestInputA.txt";
        var lines = FileUtility.ParseFileToList(filePath, line => line.Trim().ToCharArray());

        var sut = new DayThree();
        var result = sut.CalculateLifeSupportRating(lines);

        Assert.Equal("10111", result.OxygenGeneratorRating);
        Assert.Equal("01010", result.CO2ScrubberRating);
        Assert.Equal(230, result.GetLifeSupportRating());
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayThree();
        var result = sut.PartA();

        Assert.Equal("3687446", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayThree();
        var result = sut.PartB();

        Assert.Equal("4406844", result);
    }
}