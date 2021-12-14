using System.Text;
using AdventOfCode2021.Utility;
using Microsoft.Extensions.Options;

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
        var difference = FindDifferenceBetweenMostAndLeastCommon(filePath, 10);
        return difference.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Fourteen\DayFourteenInput.txt";
        return "";
    }

    public long FindDifferenceBetweenMostAndLeastCommon(string filePath, int stepsToTake)
    {
        var inputs = FileUtility.ParseFileToList(filePath, line => line);
        var polymerTemplate = inputs[0];
        var pairInsertionRules = new Dictionary<string, string>();

        for (var i = 2; i < inputs.Count; i++)
        {
            var split = inputs[i].Split(new string[] {" -> "}, StringSplitOptions.RemoveEmptyEntries);
            pairInsertionRules.Add(split[0], split[1]);
        }

        var currentTemplate = new string(polymerTemplate);

        var stepCharacterDifferences = new HashSet<long>();

        // Perform the steps
        for (var i = 0; i < stepsToTake; i++)
        {
            var builder = new StringBuilder();
            builder.Append(currentTemplate[0]);

            for (var c = 1; c < currentTemplate.Length; c++)
            {
                var key = $"{currentTemplate[c - 1]}{currentTemplate[c]}";
                if (pairInsertionRules.ContainsKey(key))
                {
                    builder.Append(pairInsertionRules[key]);
                }
                builder.Append(currentTemplate[c]);
            }

            currentTemplate = builder.ToString();
            
            // Check to see if we have gotten that score before
            var difference = DetermineCharacterCountDifference(currentTemplate);
            if (stepCharacterDifferences.Contains(difference))
            {
                // Determine the amount steps covered
                var remainder = 0;
            }
            else
            {
                stepCharacterDifferences.Add(difference);
            }
        }

        return DetermineCharacterCountDifference(currentTemplate);
    }

    private long DetermineCharacterCountDifference(string currentTemplate)
    {
        // Calculate the character count
        var characterCount = new Dictionary<char, long>();
        for (var i = 0; i < currentTemplate.Length; i++)
        {
            if (!characterCount.ContainsKey(currentTemplate[i]))
            {
                characterCount.Add(currentTemplate[i], 1);
            }
            else
            {
                characterCount[currentTemplate[i]]++;
            }
        }

        var minCount = characterCount.Select(c => c.Value).Min(c => c);
        var maxCount = characterCount.Select(c => c.Value).Max(c => c);

        return maxCount - minCount;
    }
}

public class Rule
{

}