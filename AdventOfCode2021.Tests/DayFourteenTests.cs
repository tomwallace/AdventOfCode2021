using AdventOfCode2021.Fourteen;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayFourteenTests
{
    [Fact]
    public void FindDifferenceBetweenMostAndLeastCommon_10()
    {
        var filePath = @"Fourteen\DayFourteenTestInputA.txt";
        var sut = new DayFourteen();
        var result = sut.FindDifferenceBetweenMostAndLeastCommon(filePath, 10);

        Assert.Equal(1588, result);
    }

    // TODO: Re-enable later - currently runs forever and fails with out of memory
    /*
    [Fact]
    public void FindDifferenceBetweenMostAndLeastCommon_40()
    {
        var filePath = @"Fourteen\DayFourteenTestInputA.txt";
        var sut = new DayFourteen();
        var result = sut.FindDifferenceBetweenMostAndLeastCommon(filePath, 40);

        Assert.Equal(2188189693529, result);
    }
    */
    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFourteen();
        var result = sut.PartA();

        Assert.Equal("3143", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFourteen();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
}