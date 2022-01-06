using AdventOfCode2021.Eighteen;
using Xunit;

namespace AdventOfCode2021.Tests;

public class DayEighteenTests
{
    [Fact]
    public void SnailFishPair_Simple()
    {
        var input = "[1,2]";
        var sut = SnailFishPair.Parse(input);

        Assert.Equal(0, sut.Depth);
        Assert.Null(sut.Parent);
        Assert.Equal(1, sut.LeftNumber.Value);
        Assert.Equal(2, sut.RightNumber.Value);
    }

    [Fact]
    public void SnailFishPair_TwoDepth()
    {
        var input = "[9,[8,7]]";
        var parent = SnailFishPair.Parse(input);
        var rightChild = parent.Right;

        Assert.Equal(9, parent.LeftNumber.Value);
        Assert.Equal(1, rightChild.Depth);
        Assert.Equal(8, rightChild.LeftNumber.Value);
        Assert.Equal(7, rightChild.RightNumber.Value);
        Assert.Equal(parent, rightChild.Parent);
    }

    [Fact]
    public void SnailFishPair_TwoDepth_Right()
    {
        var input = "[[1,2],3]";
        var parent = SnailFishPair.Parse(input);
        var leftChild = parent.Left;

        Assert.Equal(3, parent.RightNumber.Value);
        Assert.Null(parent.Right);
        Assert.Equal(1, leftChild.Depth);
        Assert.Equal(1, leftChild.LeftNumber.Value);
        Assert.Equal(2, leftChild.RightNumber.Value);
        Assert.Equal(parent, leftChild.Parent);
        Assert.Equal(input, parent.ToString());
    }

    [Fact]
    public void SnailFishPair_Complex()
    {
        var input = "[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]";
        var parent = SnailFishPair.Parse(input);
        var depthFourLeft = parent.Left.Left.Left;
        var depthFourRight = parent.Left.Left.Right;

        Assert.Equal(1, depthFourLeft.LeftNumber.Value);
        Assert.Equal(3, depthFourLeft.RightNumber.Value);
        Assert.Equal(3, depthFourLeft.Depth);
        Assert.Equal(5, depthFourRight.LeftNumber.Value);
        Assert.Equal(3, depthFourRight.RightNumber.Value);
        Assert.Equal(3, depthFourRight.Depth);
        Assert.Equal(input, parent.ToString());
    }

    [Theory]
    [InlineData("[[[[[9,8],1],2],3],4]","[[[[0,9],2],3],4]")]
    [InlineData("[7,[6,[5,[4,[3,2]]]]]","[7,[6,[5,[7,0]]]]")]
    [InlineData("[[6,[5,[4,[3,2]]]],1]","[[6,[5,[7,0]]],3]")]
    [InlineData("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]","[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
    [InlineData("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]","[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
    public void SnailFishPair_Explode(string input, string expected)
    {
        var parent = SnailFishPair.Parse(input);
        Assert.Equal(input, parent.ToString());

        parent.Explode();

        Assert.Equal(expected, parent.ToString());
    }

    // TODO: Stuck on this value, where two of my digits are being switched somehow
    // TODO: Getting [[[[7,7],[7,8]],[[9,5],[8,7]]],[[[7,8],[0,8]],[[8,9],[9,0]]]]
    // TODO: Should  [[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]]

    [Theory]
    //[InlineData("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]","[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
    //[InlineData("[[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]],[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]]","[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]")]
    [InlineData("[[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]],[7,[5,[[3,8],[1,4]]]]]","[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]]")]
    public void SnailFishPair_ExplodeAndSplit(string input, string expected)
    {
        var parent = SnailFishPair.Parse(input);
        Assert.Equal(input, parent.ToString());

        parent.Reduce();

        Assert.Equal(expected, parent.ToString());
    }

    [Fact]
    public void MathProblem_AddInSequence_A()
    {
        string filePath = @"Eighteen\DayEighteenTestInputA.txt";
        var sut = new MathProblem(filePath);
        var result = sut.AddInSequence();

        Assert.Equal("[[[[1,1],[2,2]],[3,3]],[4,4]]", result.ToString());
    }

    [Fact]
    public void MathProblem_AddInSequence_B()
    {
        string filePath = @"Eighteen\DayEighteenTestInputB.txt";
        var sut = new MathProblem(filePath);
        var result = sut.AddInSequence();

        Assert.Equal("[[[[3,0],[5,3]],[4,4]],[5,5]]", result.ToString());
    }

    [Fact]
    public void MathProblem_AddInSequence_C()
    {
        string filePath = @"Eighteen\DayEighteenTestInputC.txt";
        var sut = new MathProblem(filePath);
        var result = sut.AddInSequence();

        Assert.Equal("[[[[5,0],[7,4]],[5,5]],[6,6]]", result.ToString());
    }
    
    [Fact]
    public void MathProblem_AddInSequence_D()
    {
        string filePath = @"Eighteen\DayEighteenTestInputD.txt";
        var sut = new MathProblem(filePath);
        var result = sut.AddInSequence();

        Assert.Equal("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", result.ToString());
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayEighteen();
        var result = sut.PartA();

        Assert.Equal("-1", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayEighteen();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
}