namespace AdventOfCode2021.Eighteen;

public class NodeCloner
{
    private Node? clonerCurrent;

    public Node Clone(Node? node)
    {
        if (node == null)
            return null;

        if (node.Number != null)
        {
            var clone = new Node
            {
                Number = node.Number,
                LeftNode = clonerCurrent,
            };

            if (clonerCurrent != null)
                clonerCurrent.RightNode = clone;
            clonerCurrent = clone;

            return clone;
        }
        else
        {
            return new Node
            {
                LeftChild = Clone(node.LeftChild),
                RightChild = Clone(node.RightChild),
            };
        }
    }
}