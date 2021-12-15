using AdventOfCode2021.Utility;

namespace AdventOfCode2021.Fourteen;

public class DayFourteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Extended Polymerization";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 14;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Fourteen\DayFourteenInput.txt";
        var difference = FastFindDifferenceBetweenMostAndLeastCommon(filePath, 10);
        return difference.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Fourteen\DayFourteenInput.txt";
        var difference = FastFindDifferenceBetweenMostAndLeastCommon(filePath, 40);
        return difference.ToString();
    }

    public long FastFindDifferenceBetweenMostAndLeastCommon(string filePath, int stepsToTake)
    {
        var inputs = FileUtility.ParseFileToList(filePath, line => line);
        var polymerTemplate = inputs[0];
        var pairInsertionRules = new Dictionary<string, string>();

        // Create PairInsertionRules
        for (var i = 2; i < inputs.Count; i++)
        {
            var split = inputs[i].Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
            pairInsertionRules.Add(split[0], split[1]);
        }

        // Work with counts of pairs, rather than the real string, to make this manageable in large runs
        var pairCounts = new Dictionary<string, long>();
        foreach (var e in polymerTemplate.Zip(polymerTemplate.Skip(1)))
        {
            if (pairCounts.ContainsKey($"{e.First}{e.Second}"))
                pairCounts[$"{e.First}{e.Second}"] += 1;
            else
                pairCounts.Add($"{e.First}{e.Second}", 1);
        }

        // Keep track of numbers of letters as we go, so we don't have to count at the end
        var letterCounts = new Dictionary<string, long>();
        foreach (var e in polymerTemplate.ToCharArray())
        {
            if (letterCounts.ContainsKey(e.ToString()))
                letterCounts[e.ToString()] += 1;
            else
                letterCounts.Add(e.ToString(), 1);
        }

        // Perform the steps
        for (var i = 0; i < stepsToTake; i++)
        {
            var currentKeys = pairCounts.Keys.ToList();
            // Create a new dictionary to hold the steps results
            var newPairCounts = new Dictionary<string, long>();

            foreach (var pairKey in currentKeys)
            {
                var count = pairCounts[pairKey];

                if (pairInsertionRules.ContainsKey(pairKey))
                {
                    // Update the letter counts
                    var middleLetter = pairInsertionRules[pairKey];
                    if (letterCounts.ContainsKey(middleLetter))
                        letterCounts[middleLetter] += count;
                    else
                        letterCounts.Add(middleLetter, count);

                    // Set the new letters - accounting for both sets of new pairs
                    if (newPairCounts.ContainsKey($"{pairKey[0]}{middleLetter}"))
                        newPairCounts[$"{pairKey[0]}{middleLetter}"] += count;
                    else
                        newPairCounts.Add($"{pairKey[0]}{middleLetter}", count);

                    if (newPairCounts.ContainsKey($"{middleLetter}{pairKey[1]}"))
                        newPairCounts[$"{middleLetter}{pairKey[1]}"] += count;
                    else
                        newPairCounts.Add($"{middleLetter}{pairKey[1]}", count);
                }
            }

            pairCounts = newPairCounts;
        }

        var minCount = letterCounts.Select(c => c.Value).Min(c => c);
        var maxCount = letterCounts.Select(c => c.Value).Max(c => c);

        return maxCount - minCount;
    }
}