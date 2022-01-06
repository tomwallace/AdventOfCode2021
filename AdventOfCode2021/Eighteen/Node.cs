namespace AdventOfCode2021.Eighteen;

public class Node
{
    public int? Number { get; set; }

    public Node LeftChild { get; set; }

    public Node RightChild { get; set; }

    public Node LeftNode { get; set; }

    public Node RightNode { get; set; }

    public long GetMagnitude()
    {
        return Number ?? (LeftChild.GetMagnitude() * 3 + RightChild.GetMagnitude() * 2);
    }

    public override string ToString()
    {
        return Number != null ? Number.ToString() : $"[{LeftChild},{RightChild}]";
    }

    public Node Clone()
    {
        return new NodeCloner().Clone(this);
    }

    public static Node Parse(string input)
    {
        return new NodeParser().ParseNode(input, null).node;
    }

    public void Reduce()
    {
        var actionsRemain = false;

        do
        {
            actionsRemain = false;

            // Try Explode first
            actionsRemain = TryExplode();

            // Then Split
            if (!actionsRemain)
                actionsRemain = TrySplit();
        } while (actionsRemain);
    }

    // Assumes this is the root of the explode attempt
    public bool TryExplode()
    {
        var nodeNeedingExplode = FindNode(this, 0, static (n, level) =>
            level >= 4
            && n.Number == null
            && n.LeftChild.Number != null
            && n.RightChild.Number != null);

        if (nodeNeedingExplode == null)
            return false;

        var left = nodeNeedingExplode.LeftChild.Number.Value;
        var right = nodeNeedingExplode.RightChild.Number.Value;

        nodeNeedingExplode.Number = 0;
        nodeNeedingExplode.LeftNode = nodeNeedingExplode.LeftChild.LeftNode;
        if (nodeNeedingExplode.LeftNode != null)
        {
            nodeNeedingExplode.LeftNode.RightNode = nodeNeedingExplode;
            nodeNeedingExplode.LeftNode.Number += left;
        }
        nodeNeedingExplode.LeftChild = null;

        nodeNeedingExplode.RightNode = nodeNeedingExplode.RightChild.RightNode;
        if (nodeNeedingExplode.RightNode != null)
        {
            nodeNeedingExplode.RightNode.LeftNode = nodeNeedingExplode;
            nodeNeedingExplode.RightNode.Number += right;
        }
        nodeNeedingExplode.RightChild = null;

        return true;
    }

    // Assumes this is the root of the split attempt
    public bool TrySplit()
    {
        var nodeNeedingSplit = FindNode(this, 0, static (n, _) => n.Number >= 10);

        if (nodeNeedingSplit == null)
            return false;

        var leftNumber = nodeNeedingSplit.Number / 2;
        var rightNumber = nodeNeedingSplit.Number - leftNumber;
        nodeNeedingSplit.Number = default;
        nodeNeedingSplit.LeftChild = new Node { LeftNode = nodeNeedingSplit.LeftNode, Number = leftNumber, };
        nodeNeedingSplit.RightChild = new Node { RightNode = nodeNeedingSplit.RightNode, Number = rightNumber, };

        nodeNeedingSplit.LeftChild.RightNode = nodeNeedingSplit.RightChild;
        nodeNeedingSplit.RightChild.LeftNode = nodeNeedingSplit.LeftChild;

        if (nodeNeedingSplit.LeftNode != null)
            nodeNeedingSplit.LeftNode.RightNode = nodeNeedingSplit.LeftChild;
        if (nodeNeedingSplit.RightNode != null)
            nodeNeedingSplit.RightNode.LeftNode = nodeNeedingSplit.RightChild;

        nodeNeedingSplit.LeftNode = nodeNeedingSplit.RightNode = null;

        return true;
    }

    public Node Add(Node newNode)
    {
        var left = Clone();
        var right = newNode.Clone();

        var leftLink = left;
        while (leftLink.RightChild != null)
            leftLink = leftLink.RightChild;

        var rightLink = right;
        while (rightLink.LeftChild != null)
            rightLink = rightLink.LeftChild;

        (leftLink.RightNode, rightLink.LeftNode) =
            (rightLink, leftLink);

        var node = new Node { LeftChild = left, RightChild = right };

        node.Reduce();

        return node;
    }

    private Node FindNode(Node root, int level, Func<Node, int, bool> predicate)
    {
        if (predicate(root, level))
            return root;
        if (root.Number != null)
            return null;

        return FindNode(root.LeftChild, level + 1, predicate)
               ?? FindNode(root.RightChild, level + 1, predicate);
    }
}