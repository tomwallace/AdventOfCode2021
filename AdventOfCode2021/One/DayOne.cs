﻿using AdventOfCode2021.Utility;

namespace AdventOfCode2021.One;

public class DayOne : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Sonar Sweep";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 1;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"One\DayOneInput.txt";
        List<int> sweeps = FileUtility.ParseFileToList(filePath, line => int.Parse(line.Trim()));
        int increases = CountSweepIncreases(sweeps);

        return increases.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"One\DayOneInput.txt";
        List<int> sweeps = FileUtility.ParseFileToList(filePath, line => int.Parse(line.Trim()));
        int increases = CountSweepGroupIncreases(sweeps);

        return increases.ToString();
    }

    public int CountSweepIncreases(List<int> sweeps)
    {
        var increases = 0;

        for (int i = 1; i < sweeps.Count; i++)
        {
            if (sweeps[i] > sweeps[i - 1])
                increases++;
        }

        return increases;
    }

    public int CountSweepGroupIncreases(List<int> sweeps)
    {
        var increases = 0;
        var previousSum = -1;

        for (int i = 2; i < sweeps.Count; i++)
        {
            var currentSum = sweeps[i - 2] + sweeps[i - 1] + sweeps[i];
            if (previousSum > 0 && currentSum > previousSum)
                increases++;

            previousSum = currentSum;
        }

        return increases;
    }
}