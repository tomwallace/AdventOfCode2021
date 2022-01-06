using AdventOfCode2021.Nineteen;
using System.Linq;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayNineteenTests
{
    [Fact]
    public void GetTotalBeacons()
    {
        string filePath = @"Nineteen\DayNineteenTestInputA.txt";

        var sut = new DayNineteen();
        var result = sut.LocateScanners(filePath)
            .SelectMany(scanner => scanner.GetBeaconsInWorld())
            .Distinct()
            .Count();

        Assert.Equal(79, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayNineteen();
        var result = sut.PartA();

        Assert.Equal("496", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayNineteen();
        var result = sut.PartB();

        Assert.Equal("14478", result);
    }
}