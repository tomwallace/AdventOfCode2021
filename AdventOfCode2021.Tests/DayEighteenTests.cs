using AdventOfCode2021.Eighteen;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayEighteenTests
{
    [Fact]
    public void Node_Simple()
    {
        var input = "[1,2]";
        var sut = Node.Parse(input);

        Assert.Equal(input, sut.ToString());
        Assert.Equal(1, sut.LeftChild.Number);
        Assert.Equal(2, sut.RightChild.Number);
        Assert.Null(sut.LeftNode);
        Assert.Null(sut.RightNode);
    }

    [Fact]
    public void Node_TwoDepth()
    {
        var input = "[9,[8,7]]";
        var sut = Node.Parse(input);

        Assert.Equal(input, sut.ToString());
        Assert.Equal(9, sut.LeftChild.Number);

        var right = sut.RightChild;

        Assert.Equal(8, right.LeftChild.Number);
        Assert.Equal(7, right.RightChild.Number);
        Assert.Equal(sut.LeftChild, right.LeftChild.LeftNode);
        Assert.Null(right.RightChild.RightNode);
    }

    [Theory]
    [InlineData("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
    [InlineData("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
    [InlineData("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
    [InlineData("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
    [InlineData("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
    public void Node_Explode(string input, string expected)
    {
        var root = Node.Parse(input);
        Assert.Equal(input, root.ToString());

        var result = root.TryExplode();

        Assert.True(result);
        Assert.Equal(expected, root.ToString());
    }

    [Theory]
    [InlineData("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
    [InlineData("[[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]", "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]")]
    [InlineData("[[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]],[7,[5,[[3,8],[1,4]]]]]", "[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]]")]
    public void Node_Reduce(string input, string expected)
    {
        var root = Node.Parse(input);
        Assert.Equal(input, root.ToString());

        root.Reduce();

        Assert.Equal(expected, root.ToString());
    }

    [Fact]
    public void Problem_AddInSequence_A()
    {
        string filePath = @"Eighteen\DayEighteenTestInputA.txt";
        var sut = new Problem(filePath);
        var result = sut.AddInSequence();

        Assert.Equal("[[[[1,1],[2,2]],[3,3]],[4,4]]", result.ToString());
        Assert.Equal(445, result.GetMagnitude());
    }

    [Fact]
    public void Problem_AddInSequence_B()
    {
        string filePath = @"Eighteen\DayEighteenTestInputB.txt";
        var sut = new Problem(filePath);
        var result = sut.AddInSequence();

        Assert.Equal("[[[[3,0],[5,3]],[4,4]],[5,5]]", result.ToString());
        Assert.Equal(791, result.GetMagnitude());
    }

    [Fact]
    public void Problem_AddInSequence_C()
    {
        string filePath = @"Eighteen\DayEighteenTestInputC.txt";
        var sut = new Problem(filePath);
        var result = sut.AddInSequence();

        Assert.Equal("[[[[5,0],[7,4]],[5,5]],[6,6]]", result.ToString());
        Assert.Equal(1137, result.GetMagnitude());
    }

    [Fact]
    public void Problem_AddInSequence_D()
    {
        string filePath = @"Eighteen\DayEighteenTestInputD.txt";
        var sut = new Problem(filePath);
        var result = sut.AddInSequence();

        Assert.Equal("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", result.ToString());
        Assert.Equal(3488, result.GetMagnitude());
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayEighteen();
        var result = sut.PartA();

        Assert.Equal("3051", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayEighteen();
        var result = sut.PartB();

        Assert.Equal("4812", result);
    }
}