namespace AdventOfCode2021.TwentyOne;

public class DayTwentyOne : IAdventProblemSet
{
    public readonly List<int> Input = new List<int>() { 7, 8 };

    /// <inheritdoc />
    public string Description()
    {
        return "Dirac Dice";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 21;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var diceGame = new DiceGame(Input);
        diceGame.Play(1000);
        var output = diceGame.GetLoserOutput();

        return output.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var diceGame = new DiceGame(Input);
        var output = diceGame.PlayRecursiveAndReturnWinningUniverses();

        return output.ToString();
    }
}