namespace AdventOfCode2021.Ten;

public class Line
{
    private readonly char[] startChunk = new[] { '(', '[', '{', '<' };
    private readonly Dictionary<char, char> matches = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
    private readonly Dictionary<char, int> corruptScores = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
    private readonly Dictionary<char, int> autoCompleteScores = new Dictionary<char, int>() { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };
    private readonly Stack<char> remainingOpenChunks;

    private char? corruptChar;

    public Line(string line)
    {
        Value = line;
        remainingOpenChunks = new Stack<char>();

        EvaluateChunks();
    }

    public string Value { get; set; }

    public bool IsCorrupt()
    {
        return corruptChar != null;
    }

    public int GetCorruptScore()
    {
        if (!IsCorrupt())
            return 0;

        return corruptScores[corruptChar.Value];
    }

    public long GetAutoCompleteScore()
    {
        if (IsCorrupt())
            return 0;

        long score = 0;
        foreach (var closingChunk in RemainingClosingChunks())
        {
            score = (score * 5) + autoCompleteScores[closingChunk];
        }

        return score;
    }

    private List<char> RemainingClosingChunks()
    {
        var remainingClosingChunks = new List<char>();
        while (remainingOpenChunks.Any())
        {
            var closing = matches[remainingOpenChunks.Pop()];
            remainingClosingChunks.Add(closing);
        }

        return remainingClosingChunks;
    }

    private void EvaluateChunks()
    {
        var position = 0;
        while (position < Value.Length)
        {
            var currentChar = Value[position];
            if (startChunk.Contains(currentChar))
            {
                remainingOpenChunks.Push(currentChar);
                position++;
            }
            else
            {
                // No closing char or does not match
                if (remainingOpenChunks.Count == 0 || matches[remainingOpenChunks.Peek()] != currentChar)
                {
                    corruptChar = currentChar;
                    return;
                }

                // Otherwise remove from stack
                remainingOpenChunks.Pop();
                position++;
            }
        }
    }
}