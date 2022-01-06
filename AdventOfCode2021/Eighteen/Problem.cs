using AdventOfCode2021.Utility;

namespace AdventOfCode2021.Eighteen;

public class Problem
{
    private readonly List<Node> rootNodes;

    public Problem(string filePath)
    {
        rootNodes = FileUtility.ParseFileToList(filePath, line => Node.Parse(line));
    }

    public Node AddInSequence()
    {
        var collectedNode = rootNodes.First();
        //collectedPair.Reduce();
        for (var i = 1; i < rootNodes.Count; i++)
        {
            var nextNode = rootNodes[i];
            //nextPair.Reduce();
            var sum = collectedNode.Add(nextNode);
            collectedNode = sum;
        }

        return collectedNode;
    }

    public long LargestMagnitudeOfAnyTwoPairs()
    {
        var maxMagnitude = 0L;
        for (var a = 0; a < rootNodes.Count; a++)
        {
            for (var b = 0; b < rootNodes.Count; b++)
            {
                if (a == b)
                    continue;

                var combined = rootNodes[a].Add(rootNodes[b]);
                maxMagnitude = Math.Max(maxMagnitude, combined.GetMagnitude());
            }
        }

        return maxMagnitude;
    }
}