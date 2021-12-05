using AdventOfCode2021.Utility;

namespace AdventOfCode2021.Four;

public class DayFour : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Giant Squid";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 4;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Four\DayFourInput.txt";
        int score = FindWinningBoardAndReturnScore(filePath);
        return score.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Four\DayFourInput.txt";
        int score = FindLastBoardToWinsScore(filePath);
        return score.ToString();
    }

    public int FindWinningBoardAndReturnScore(string filePath)
    {
        List<string> lines = FileUtility.ParseFileToList(filePath, line => line);
        int[] drawnNumbers = lines[0].Split(',').Select(c => int.Parse(c)).ToArray();

        var boards = GetBingoBoards(lines);

        // Run game
        foreach (var number in drawnNumbers)
        {
            foreach (var board in boards)
            {
                board.MarkNumber(number);
                if (board.IsWinning())
                    return board.CalculateScore(number);
            }
        }

        // Should never get here
        throw new Exception("No board won");
    }

    public int FindLastBoardToWinsScore(string filePath)
    {
        List<string> lines = FileUtility.ParseFileToList(filePath, line => line);
        int[] drawnNumbers = lines[0].Split(',').Select(c => int.Parse(c)).ToArray();

        var boards = GetBingoBoards(lines);

        var boardsNotWon = Enumerable.Range(1, boards.Count).ToList();

        // Run game
        foreach (var number in drawnNumbers)
        {
            foreach (var board in boards)
            {
                board.MarkNumber(number);
                if (board.IsWinning())
                    boardsNotWon.Remove(board.Id);

                if (boardsNotWon.Count == 0)
                    return board.CalculateScore(number);
            }
        }

        // Should never get here
        throw new Exception("At least one board never won");
    }

    private List<BingoBoard> GetBingoBoards(List<string> lines)
    {
        // Get boards
        List<BingoBoard> boards = new List<BingoBoard>();
        int id = 1;

        for (var i = 2; i < lines.Count; i += 6)
        {
            List<string> rows = new List<string>();
            for (var r = 0; r < 5; r++)
            {
                rows.Add(lines[i + r]);
            }

            boards.Add(new BingoBoard(rows, id));
            id++;
        }

        return boards;
    }
}