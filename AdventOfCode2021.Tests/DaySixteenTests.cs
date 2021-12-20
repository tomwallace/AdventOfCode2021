using AdventOfCode2021.Sixteen;
using System.Linq;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DaySixteenTests
{
    [Fact]
    public void Packet_Literal()
    {
        var hexString = "D2FE28";
        var bits = hexString.ToCharArray().SelectMany(c => Packet.ConversionTable[c]).ToArray();

        var sut = new Packet(bits, 0);

        Assert.True(sut.IsLiteral);
        Assert.Equal(2021UL, sut.LiteralValue);
        Assert.Equal(20, sut.EndIndex);
    }

    [Fact]
    public void Packet_Operator_Length_0()
    {
        var hexString = "38006F45291200";
        var bits = hexString.ToCharArray().SelectMany(c => Packet.ConversionTable[c]).ToArray();

        var sut = new Packet(bits, 0);

        Assert.False(sut.IsLiteral);
        Assert.Equal(2, sut.SubPackets.Count);
        Assert.Equal(48, sut.EndIndex);
    }

    [Fact]
    public void Packet_Operator_Length_1()
    {
        var hexString = "EE00D40C823060";
        var bits = hexString.ToCharArray().SelectMany(c => Packet.ConversionTable[c]).ToArray();

        var sut = new Packet(bits, 0);

        Assert.False(sut.IsLiteral);
        Assert.Equal(3, sut.SubPackets.Count);
    }

    [Theory]
    [InlineData("8A004A801A8002F478", 16)]
    [InlineData("620080001611562C8802118E34", 12)]
    [InlineData("C0015000016115A2E0802F182340", 23)]
    [InlineData("A0016C880162017C3686B18A3D4780", 31)]
    public void Packet_SumVersions(string hexString, int expected)
    {
        var bits = hexString.ToCharArray().SelectMany(c => Packet.ConversionTable[c]).ToArray();

        var sut = new Packet(bits, 0);

        Assert.False(sut.IsLiteral);
        Assert.Equal(expected, sut.SumVersions());
    }

    [Theory]
    [InlineData("C200B40A82", 3)]
    [InlineData("04005AC33890", 54)]
    [InlineData("880086C3E88112", 7)]
    [InlineData("CE00C43D881120", 9)]
    public void Packet_GetResult(string hexString, ulong expected)
    {
        var bits = hexString.ToCharArray().SelectMany(c => Packet.ConversionTable[c]).ToArray();

        var sut = new Packet(bits, 0);

        Assert.False(sut.IsLiteral);
        Assert.Equal(expected, sut.GetResult());
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySixteen();
        var result = sut.PartA();

        Assert.Equal("927", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySixteen();
        var result = sut.PartB();

        Assert.Equal("1725277876501", result);
    }
}