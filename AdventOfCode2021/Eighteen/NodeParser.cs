namespace AdventOfCode2021.Eighteen;

public class NodeParser
{
    private Node? parserCurrent;

    public (Node node, int idx) ParseNode(ReadOnlySpan<char> txt, Node? parent)
    {
        var node = new Node();

        if (char.IsNumber(txt[0]))
        {
            node.LeftNode = parserCurrent;
            if (parserCurrent != null)
                parserCurrent.RightNode = node;
            parserCurrent = node;

            node.Number = txt[0] - '0';
            return (node, 1);
        }

        (node.LeftChild, var ll) = ParseNode(txt[1..], node);
        (node.RightChild, var rl) = ParseNode(txt[(1 + ll + 1)..], node);

        return (
            node,
            1 + ll + 1 + rl + 1);
    }
}