using System.Text;

namespace AdventOfCode2021.Eight;

public class Analysis
{
    private readonly Dictionary<int, string> foundNumbers;

    public Analysis(string input)
    {
        var split = input.Split(new string[] { " | " }, StringSplitOptions.None);
        Combinations = split[0].Split(' ').Select(Alphebetize).ToList();
        Outputs = split[1].Split(' ').Select(Alphebetize).ToList();
        foundNumbers = new Dictionary<int, string>();

        ConductAnalysis();
    }

    public List<string> Combinations { get; set; }

    public List<string> Outputs { get; set; }

    public int CalculateOutputValue()
    {
        var builder = new StringBuilder();
        foreach (var output in Outputs)
        {
            builder.Append(foundNumbers.Single(f => f.Value == output).Key.ToString());
        }

        return int.Parse(builder.ToString());
    }

    private void ConductAnalysis()
    {
        // Add the numbers we know
        var one = FindCombosMatchingSize(2).Single();
        foundNumbers.Add(1, one);
        var four = FindCombosMatchingSize(4).Single();
        foundNumbers.Add(4, four);
        var seven = FindCombosMatchingSize(3).Single();
        foundNumbers.Add(7, seven);
        var eight = FindCombosMatchingSize(7).Single();
        foundNumbers.Add(8, eight);

        // *** Deal with the six digit numbers - 0, 6, 9
        var sixDigits = FindCombosMatchingSize(6);
        // 9 has all the characters in 4 and 7
        var nine = sixDigits.Single(s => s.Except(four).Except(seven).Count() == 1);
        foundNumbers.Add(9, nine);
        sixDigits.Remove(nine);
        // Between 0 and 6, only 6 is missing a digit from 1
        var six = sixDigits.Single(s => one.Except(s).Count() == 1);
        foundNumbers.Add(6, six);
        sixDigits.Remove(six);
        // That leaves 0
        var zero = sixDigits[0];
        foundNumbers.Add(0, zero);

        // *** Deal with the five digit numbers - 2, 3, 5
        var fiveDigits = FindCombosMatchingSize(5);
        // 3 is the only number with 5 digits and both of number 1 in it
        var three = fiveDigits.Single(f => one.Except(f).Count() == 0);
        foundNumbers.Add(3, three);
        fiveDigits.Remove(three);
        // 5 is only missing one digit from 6
        var five = fiveDigits.Single(f => six.Except(f).Count() == 1);
        foundNumbers.Add(5, five);
        fiveDigits.Remove(five);
        // That leaves 2
        var two = fiveDigits.First();
        foundNumbers.Add(2, two);
    }

    private List<string> FindCombosMatchingSize(int size)
    {
        return Combinations.Where(c => c.Length == size).ToList();
    }

    private string Alphebetize(string input)
    {
        var array = input.ToCharArray();
        Array.Sort(array);
        return new string(array);
    }
}