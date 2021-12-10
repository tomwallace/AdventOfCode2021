using AdventOfCode2021.Ten;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayTenTests
{
    [Theory]
    [InlineData("(]", true)]
    [InlineData("{()()()>", true)]
    [InlineData("(((()))}", true)]
    [InlineData("<([]){()}[{}])", true)]
    [InlineData("{()()()}", false)]
    [InlineData("[<>({}){}[([])<>]]", false)]
    public void LineForCorrupt(string input, bool isCorrupt)
    {
        var sut = new Line(input);

        Assert.Equal(isCorrupt, sut.IsCorrupt());
    }

    [Fact]
    public void NavSystem_Corrupt()
    {
        var filePath = @"Ten\DayTenTestInputA.txt";
        var sut = new NavSystem(filePath);
        var result = sut.ScoreCorruptLines();

        Assert.Equal(26397, result);
    }

    [Fact]
    public void NavSystem_AutoComplete()
    {
        var filePath = @"Ten\DayTenTestInputA.txt";
        var sut = new NavSystem(filePath);
        var result = sut.ScoreAutoComplete();

        Assert.Equal(288957, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTen();
        var result = sut.PartA();

        Assert.Equal("243939", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTen();
        var result = sut.PartB();

        Assert.Equal("2421222841", result);
    }
}