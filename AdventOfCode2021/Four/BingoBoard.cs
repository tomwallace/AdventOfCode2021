namespace AdventOfCode2021.Four;

public class BingoBoard
{
    public BingoBoard(List<string> rows, int id)
    {
        Id = id;
        Numbers = new List<List<Square>>();
        UnmarkedNumbers = new List<int>();
        for (var r = 0; r < rows.Count; r++)
        {
            var splits = rows[r].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => new Square(int.Parse(s))).ToList();
            Numbers.Add(splits);
            UnmarkedNumbers.AddRange(splits.Select(s => s.Number));
        }
    }

    public List<List<Square>> Numbers { get; set; }

    public List<int> UnmarkedNumbers { get; set; }

    public int Id { get; set; }

    public bool IsWinning()
    {
        // Check rows
        foreach (var row in Numbers)
        {
            if (row.All(r => r.IsSelected))
                return true;
        }

        // Check columns
        for (var c = 0; c < Numbers[0].Count; c++)
        {
            var found = true;
            for (var r = 0; r < Numbers.Count; r++)
            {
                if (!Numbers[r][c].IsSelected)
                {
                    found = false;
                    break;
                }
            }

            if (found)
                return true;
        }

        return false;
    }

    public int CalculateScore(int number)
    {
        return UnmarkedNumbers.Sum() * number;
    }

    public bool Contains(int number)
    {
        for (var r = 0; r < Numbers.Count; r++)
        {
            for (var c = 0; c < Numbers[r].Count; c++)
            {
                if (Numbers[r][c].Number == number)
                {
                    Numbers[r][c].IsSelected = true;
                    return true;
                }
            }
        }

        return false;
    }

    public void MarkNumber(int number)
    {
        if (Contains(number))
        {
            UnmarkedNumbers.Remove(number);
        }
    }
}