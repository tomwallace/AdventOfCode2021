using AdventOfCode2021.Utility;

namespace AdventOfCode2021.Twelve;

public class CaveSystem
{
    public CaveSystem(string filePath)
    {
        Caves = new Dictionary<string, Cave>();

        var lines = FileUtility.ParseFileToList(filePath, line => line);
        foreach (var line in lines)
        {
            var split = line.Split('-');
            if (!Caves.ContainsKey(split[0]))
                Caves.Add(split[0], new Cave(split[0]));

            if (!Caves.ContainsKey(split[1]))
                Caves.Add(split[1], new Cave(split[1]));
        }

        // Connect them
        foreach (var line in lines)
        {
            var split = line.Split('-');
            Caves[split[0]].Connected.Add(Caves[split[1]]);
            Caves[split[1]].Connected.Add(Caves[split[0]]);
        }
    }

    public Dictionary<string, Cave> Caves { get; set; }

    public int PathsThroughEnd(bool allowTwoSmallCaveVisits)
    {
        var successfulPaths = new HashSet<string>();
        var queue = new Queue<TravelStep>();
        var start = Caves.Single(c => c.Value.IsStart).Value;
        if (allowTwoSmallCaveVisits)
        {
            foreach (var smallCave in Caves.Where(c => !c.Value.IsBig))
            {
                queue.Enqueue(new TravelStep(start, smallCave.Value));
            }
        }
        else
        {
            queue.Enqueue(new TravelStep(start));
        }

        do
        {
            var currentStep = queue.Dequeue();
            currentStep.Visited.Add(currentStep.Current);
            // End condition
            if (currentStep.Current.IsEnd)
                successfulPaths.Add(currentStep.ToString());

            foreach (var neighbor in currentStep.Current.Connected)
            {
                // Ensure we only travel to small caves once
                if (!allowTwoSmallCaveVisits && !neighbor.IsBig && currentStep.Visited.Any(v => v == neighbor))
                    continue;

                if (allowTwoSmallCaveVisits && !neighbor.IsBig)
                {
                    if ((currentStep.SmallCaveCanVisitTwice == null || neighbor != currentStep.SmallCaveCanVisitTwice || neighbor.IsStart || neighbor.IsEnd)
                        && currentStep.Visited.Any(v => v == neighbor))
                        continue;

                    if (currentStep.SmallCaveCanVisitTwice != null && neighbor == currentStep.SmallCaveCanVisitTwice
                                                                   && currentStep.Visited.Count(v => v == neighbor) == 2)
                        continue;
                }

                queue.Enqueue(new TravelStep(neighbor, currentStep.Visited, currentStep.SmallCaveCanVisitTwice));
            }
        } while (queue.Any());

        var sorted = successfulPaths.OrderBy(s => s);
        return successfulPaths.Count;
    }
}