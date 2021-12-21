namespace AdventOfCode2021.TwentyOne;

public class DiceGame
{
    private readonly List<int> positions;
    private readonly List<int> scores;
    private int loser;
    private int diceRolled;
    private Dictionary<int, int> diceRolls;

    public DiceGame(List<int> startingPositions)
    {
        positions = startingPositions;
        scores = new List<int>() { 0, 0 };
        diceRolled = 0;

        loser = -1;

        diceRolls = GetRolls();
    }

    public void Play(int targetScore)
    {
        var diceValue = 1;
        var currentPlayer = 0;
        var hasWon = false;

        do
        {
            var position = positions[currentPlayer];
            var roll = diceValue + (diceValue + 1) + (diceValue + 2);
            diceRolled += 3;
            var newPosition = UpdatePosition(position, roll);

            positions[currentPlayer] = newPosition;
            scores[currentPlayer] += newPosition;

            if (scores[currentPlayer] >= targetScore)
            {
                hasWon = true;
                loser = currentPlayer == 0 ? 1 : 0;
            }
            else
            {
                // Iterate
                currentPlayer = currentPlayer == 0 ? 1 : 0;
                diceValue += 3;
            }
        } while (!hasWon);
    }

    public long PlayRecursiveAndReturnWinningUniverses()
    {
        var results = RecursivePlay(positions[0], positions[1], 0, 0, true);
        return results.P1Wins > results.P2Wins ? results.P1Wins : results.P2Wins;
    }

    private PlayerResults RecursivePlay(int p1Position, int p2Position, int p1Score, int p2Score,
        bool isPlayerOne)
    {
        var results = new PlayerResults();

        // Go over all possible dice rolls
        foreach (var diceRoll in diceRolls)
        {
            var p1Pos = p1Position;
            var p2Pos = p2Position;
            var p1S = p1Score;
            var p2S = p2Score;
            var hasWon = false;

            // TODO: Clean up
            if (isPlayerOne)
            {
                p1Pos += diceRoll.Key;

                // Rolls can only go up to 9 now, so can simplify position determination
                if (p1Pos > 10)
                    p1Pos -= 10;

                p1S += p1Pos;

                // Win condition
                if (p1S >= 21)
                {
                    // Add all of the possible roll combo wins
                    results.P1Wins += diceRoll.Value;
                    hasWon = true;
                }
            }
            else
            {
                p2Pos += diceRoll.Key;

                // Rolls can only go up to 9 now, so can simplify position determination
                if (p2Pos > 10)
                    p2Pos -= 10;

                p2S += p2Pos;

                // Win condition
                if (p2S >= 21)
                {
                    results.P2Wins += diceRoll.Value;
                    hasWon = true;
                }
            }

            // Iterate again if there has not been a winner, recursively
            if (!hasWon)
            {
                var recurse = RecursivePlay(p1Pos, p2Pos, p1S, p2S, !isPlayerOne);
                results.P1Wins += recurse.P1Wins * diceRoll.Value;
                results.P2Wins += recurse.P2Wins * diceRoll.Value;
            }
        }

        return results;
    }

    public int GetLoserOutput()
    {
        if (loser == -1)
            throw new Exception("Game has not been played yet");

        return scores[loser] * diceRolled;
    }

    private int UpdatePosition(int currentPosition, int roll)
    {
        var mod = (roll + currentPosition) % 10;
        // If we get a 0, it is actually a 10
        mod = mod == 0 ? 10 : mod;
        return mod;
    }

    // To make Part B more efficient, calculate the number of times each combo of three
    // dice occurs, as you can get the same number a number of different ways
    private Dictionary<int, int> GetRolls()
    {
        List<int> rolls = new List<int>();
        for (int i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                for (int k = 1; k <= 3; k++)
                {
                    rolls.Add(i + j + k);
                }
            }
        }

        return rolls.GroupBy(r => r).ToDictionary(r => r.Key, r => r.Count());
    }
}