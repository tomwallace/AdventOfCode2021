using AdventOfCode2021.Utility;
using System.Diagnostics;

namespace AdventOfCode2021.Eighteen;

public class DayEighteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Snailfish [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 18;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Eighteen\DayEighteenInput.txt";
        return "";
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Eighteen\DayEighteenInput.txt";
        return "";
    }
}

public class MathProblem
{
    private readonly List<SnailFishPair> pairs;

    public MathProblem(string filePath)
    {
        pairs = FileUtility.ParseFileToList(filePath, line => SnailFishPair.Parse(line));
    }

    public SnailFishPair AddInSequence()
    {
        var collectedPair = pairs.First().Clone();
        //collectedPair.Reduce();
        for (var i = 1; i < pairs.Count; i++)
        {
            var nextPair = pairs[i].Clone();
            //nextPair.Reduce();
            var sum = collectedPair.Add(nextPair);
            collectedPair = sum.Clone();
        }

        return collectedPair;
    }
}

// TODO: Return to
public class SnailFishPair
{
    // Used to make a starting nested SnailFishPair
    public static SnailFishPair Parse(string input)
    {
        var stack = new Stack<SnailFishPair>();
        var current = new SnailFishPair();
        var depth = 0;
        current.Depth = depth;
        for (var i = 1; i < input.Length - 1; i++)
        {
            var c = input[i];
            // Starting a new SnailFishPair
            if (c == '[')
            {
                depth++;
                stack.Push(current);
                current = current.AddChild(new SnailFishPair());
            }
            // Ending a SnailFishPair
            else if (c == ']')
            {
                depth--;
                current = stack.Pop();
            }
            // Skip commas
            else if (c == ',')
                continue;
            // Must be a number
            else
            {
                var num = int.Parse(c.ToString());
                if (current.LeftNumber == null && current.Left == null)
                    current.LeftNumber = new Number(num);
                else
                    current.RightNumber = new Number(num);
            }
        }

        return current;
    }

    public SnailFishPair? Left { get; set; }

    public SnailFishPair? Right { get; set; }

    public SnailFishPair? Parent { get; set; }

    public int Depth { get; set; }

    public Number? LeftNumber { get; set; }

    public Number? RightNumber { get; set; }

    public void Reduce()
    {
        var actionsRemain = false;

        do
        {
            //Debug.WriteLine(this.ToString());

            actionsRemain = false;

            // Try Explode first
            actionsRemain = Explode();

            // Then Split
            if (!actionsRemain)
                actionsRemain = Split();
        } while (actionsRemain);
    }

    public SnailFishPair Add(SnailFishPair right)
    {
        var newPair = new SnailFishPair();
        newPair.Left = this;
        Parent = newPair;
        newPair.Right = right;
        right.Parent = newPair;

        newPair.Left.IncreaseDepth();
        newPair.Right.IncreaseDepth();

        newPair.Reduce();

        return newPair;
    }

    public SnailFishPair Clone()
    {
        var left = Left?.Clone();
        var right = Right?.Clone();
        var leftNumber = LeftNumber?.Clone();
        var rightNumber = RightNumber?.Clone();

        var newPair = new SnailFishPair();
        newPair.Left = left;
        newPair.Right = right;
        newPair.LeftNumber = leftNumber;
        newPair.RightNumber = rightNumber;
        newPair.Depth = Depth;

        if (left != null)
            left.Parent = newPair;

        if (right != null)
            right.Parent = newPair;

        return newPair;
    }

    public void IncreaseDepth()
    {
        Depth++;
        if (Left != null)
            Left.IncreaseDepth();
        if (Right != null)
            Right.IncreaseDepth();
    }

    public SnailFishPair? FindPairThatCanExplode()
    {
        if (Depth >= 4 && LeftNumber != null && RightNumber != null)
            return this;

        // Search Left first
        var exploder = Left?.FindPairThatCanExplode();
        if (exploder != null)
            return exploder;

        // Then search Right
        exploder = Right?.FindPairThatCanExplode();
        if (exploder != null)
            return exploder;

        // Did not find one
        return null;
    }

    public bool Explode()
    {
        if (Depth <= 3)
        {
            var exploded = Left == null ? false : Left.Explode();
            if (!exploded)
                exploded = Right == null ? false : Right.Explode();
            return exploded;
        }

        var leftNeighbor = FindLeftNeighbor();
        var rightNeighbor = FindRightNeighbor();

        if (leftNeighbor == null && rightNeighbor == null)
        {
            // Nowhere to explode, no-op
            return false;
        }

        if (rightNeighbor != null)
        {
            if (leftNeighbor == null)
            {
                rightNeighbor.Value += RightNumber.Value;
            }
            else
            {
                // Can explode to both sides
                rightNeighbor.Value += RightNumber.Value;
                leftNeighbor.Value += LeftNumber.Value;
            }
        }
        else
        {
            if (leftNeighbor != null)
            {
                leftNeighbor.Value += LeftNumber.Value;
            }
        }

        // Remove the node
        if (IsLeftChild)
        {
            if (Parent.LeftNumber == null)
                Parent.LeftNumber = new Number(0);
            else
                Parent.LeftNumber.Value = 0;

            Parent.Left = null;
        }
        else if (IsRightChild)
        {
            if (Parent.RightNumber == null)
                Parent.RightNumber = new Number(0);
            else
                Parent.RightNumber.Value = 0;

            Parent.Right = null;
        }

        return true;
    }

    public bool Split()
    {
        if (LeftNumber?.Value >= 10)
        {
            var newPair = MakeNewSplitSnailFishPair(LeftNumber.Value, Depth);
            Left = newPair;
            LeftNumber = null;

            return true;
        }

        if (RightNumber?.Value >= 10)
        {
            var newPair = MakeNewSplitSnailFishPair(RightNumber.Value, Depth);
            Right = newPair;
            RightNumber = null;

            return true;
        }

        if (LeftNumber == null || RightNumber == null)
        {
            var split = Left == null ? false : Left.Split();
            if (!split)
                split = Right == null ? false : Right.Split();
            return split;
        }

        return false;
    }

    private SnailFishPair MakeNewSplitSnailFishPair(int value, int depth)
    {
        // Rounded down
        var newLeft = value / 2;
        // Rounded up
        var newRight = value - newLeft;    // (int)Math.Ceiling((double)value / (double)2);

        Debug.WriteLine($"Left: {newLeft}, Right: {newRight}, Original: {value}");

        var newPair = new SnailFishPair();
        newPair.LeftNumber = new Number(newLeft);
        newPair.RightNumber = new Number(newRight);
        newPair.Depth = depth + 1;
        newPair.Parent = this;

        return newPair;
    }

    private Number? FindRightNeighbor()
    {
        var pair = this;
        while (pair != null)
        {
            if (pair.IsLeftChild)
            {
                if (pair.Parent?.Right != null)
                    return pair.Parent.Right.FindLeaf(rightLeaf: false);

                return pair.Parent?.FindLeaf(rightLeaf: true);
            }

            pair = pair.Parent;
        }

        return null;
    }

    private Number? FindLeftNeighbor()
    {
        var pair = this;
        while (pair != null)
        {
            if (pair.IsRightChild)
            {
                if (pair.Parent?.Left != null)
                    return pair.Parent.Left.FindLeaf(rightLeaf: true);

                return pair.Parent?.FindLeaf(rightLeaf: false);
            }

            pair = pair.Parent;
        }

        return null;
    }

    private Number? FindLeaf(bool rightLeaf)
    {
        var pair = this;
        while (true)
        {
            if (rightLeaf && pair.RightNumber != null)
                return pair.RightNumber;

            if (!rightLeaf && pair.LeftNumber != null)
                return pair.LeftNumber;

            pair = rightLeaf ? pair.Right : pair.Left;
        }
    }

    public bool IsLeftChild => Parent?.Left == this;

    public bool IsRightChild => Parent?.Right == this;

    public bool CanExplodeLeft => Depth >= 4 && LeftNumber != null;

    public bool CanExplodeRight => Depth >= 4 && RightNumber != null;

    public void ExplodeLeft(int? value)
    {
        if (value != null && LeftNumber != null)
            LeftNumber.Value += value.Value;
        else if (Parent != null)
            Parent.ExplodeLeft(value);
    }

    public void ExplodeRight(int? value)
    {
        if (value != null && RightNumber != null)
            RightNumber.Value += value.Value;
        else if (Parent != null)
            Parent.ExplodeRight(value);
    }

    public SnailFishPair AddChild(SnailFishPair child)
    {
        if (Left == null && LeftNumber == null)
            Left = child;
        else if (Right == null && RightNumber == null)
            Right = child;
        else
            throw new Exception("Cannot add a child when both One and Two are already set");

        child.Depth = Depth + 1;
        child.Parent = this;

        return child;
    }

    public override string ToString()
    {
        return $"[{(LeftNumber != null ? LeftNumber.Value : Left?.ToString())},{(RightNumber != null ? RightNumber.Value : Right?.ToString())}]";
    }

    public int GetLeft()
    {
        if (LeftNumber != null)
            return LeftNumber.Value;

        return Left.GetLeft();
    }

    public int GetRight()
    {
        if (RightNumber != null)
            return RightNumber.Value;

        return Right.GetRight();
    }
}

public class Number
{
    public Number(int value)
    {
        Value = value;
    }

    public Number Clone()
    {
        return new Number(Value);
    }

    public int Value { get; set; }
}