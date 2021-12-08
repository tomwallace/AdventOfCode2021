using AdventOfCode2021.Eight;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayEightTests
{
    [Fact]
    public void CountUniqueNumbersInOutputs()
    {
        var filePath = @"Eight\DayEightTestInputA.txt";

        var sut = new DayEight();
        var result = sut.CountUniqueNumbersInOutputs(filePath);

        Assert.Equal(26, result);
    }

    [Theory]
    [InlineData(5353, "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf")]
    [InlineData(8394, "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe")]
    [InlineData(9781, "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc")]
    [InlineData(1197, "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg")]
    [InlineData(9361, "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb")]
    [InlineData(4873, "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea")]
    [InlineData(8418, "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb")]
    [InlineData(4548, "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe")]
    [InlineData(1625, "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef")]
    public void CalculateOutputValue(int expected, string input)
    {
        var sut = new Analysis(input);
        var result = sut.CalculateOutputValue();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TotalValuesOfAllAnalyses()
    {
        var filePath = @"Eight\DayEightTestInputA.txt";

        var sut = new DayEight();
        var result = sut.TotalValuesOfAllAnalyses(filePath);

        Assert.Equal(61229, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayEight();
        var result = sut.PartA();

        Assert.Equal("519", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayEight();
        var result = sut.PartB();

        Assert.Equal("1027483", result);
    }
}