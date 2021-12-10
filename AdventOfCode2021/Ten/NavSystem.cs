using AdventOfCode2021.Utility;

namespace AdventOfCode2021.Ten;

public class NavSystem
{
    public NavSystem(string filePath)
    {
        Lines = FileUtility.ParseFileToList(filePath, line => new Line(line));
    }

    public List<Line> Lines { get; }

    public int ScoreCorruptLines()
    {
        return Lines.Sum(l => l.GetCorruptScore());
    }

    public long ScoreAutoComplete()
    {
        var lineScores = Lines.Where(l => !l.IsCorrupt())
            .Select(l => l.GetAutoCompleteScore());
        var sortedLineScores = lineScores.OrderBy(l => l).ToList();

        // They always want the middle one
        return sortedLineScores[(sortedLineScores.Count() - 1) / 2];
    }
}