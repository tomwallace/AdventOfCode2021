using AdventOfCode2021.Utility;

namespace AdventOfCode2021.TwentyFour;

public class DayTwentyFour : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Arithmetic Logic Unit [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 24;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"TwentyFour\DayTwentyFourInput.txt";
        var highestModelNumber = CalculateNumber(filePath, true);

        return highestModelNumber.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"TwentyFour\DayTwentyFourInput.txt";
        var lowestModelNumber = CalculateNumber(filePath, false);

        return lowestModelNumber.ToString();
    }

    // I really struggled with this problem and went looking for suggestions.  With some hints from Reddit,
    // I looked at my input and discovered that the instructions MOSTLY repeat themselves every 18 lines.  The
    // places where they differ is line + 5 and line + 18, and then the only difference is in the second parameter
    // of the add operator.
    // I still struggled to get it to work, and below is some code that iterates over the rules as a stack and
    // derives the digits one by one.
    public long CalculateNumber(string filePath, bool largeModelNumber)
    {
        var input = FileUtility.ParseFileToList(filePath, line => line);

        Stack<(int sourceIndex, int offset)> inputStash = new();
        int[] finalDigits = new int[14];

        int targetIndex = 0;
        for (int block = 0; block < input.Count; block += 18)
        {
            int check = int.Parse(input[block + 5].Split(' ')[2]);
            int offset = int.Parse(input[block + 15].Split(' ')[2]);
            if (check > 0)
            {
                inputStash.Push((targetIndex, offset));
            }
            else
            {
                (int sourceIndex, int offset) rule = inputStash.Pop();
                int totalOffset = rule.offset + check;
                if (totalOffset > 0)
                {
                    if (largeModelNumber)
                    {
                        finalDigits[rule.sourceIndex] = 9 - totalOffset;
                        finalDigits[targetIndex] = 9;
                    }
                    else
                    {
                        finalDigits[rule.sourceIndex] = 1;
                        finalDigits[targetIndex] = 1 + totalOffset;
                    }
                }
                else
                {
                    if (largeModelNumber)
                    {
                        finalDigits[rule.sourceIndex] = 9;
                        finalDigits[targetIndex] = 9 + totalOffset;
                    }
                    else
                    {
                        finalDigits[rule.sourceIndex] = 1 - totalOffset;
                        finalDigits[targetIndex] = 1;
                    }
                }
            }
            targetIndex++;
        }
        return long.Parse(string.Join("", finalDigits));
    }
}