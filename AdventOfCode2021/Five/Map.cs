namespace AdventOfCode2021.Five;

public class Map
{
    public Map(List<PointSet> pointSets, bool ignoreDiagonals)
    {
        Cells = new Dictionary<string, int>();
        foreach (var pointSet in pointSets)
        {
            if (ignoreDiagonals && pointSet.IsDiagonal())
            {
                continue;
            }

            var x = pointSet.One.X;
            var y = pointSet.One.Y;
            var point = new Point(x, y);
            do
            {
                point = new Point(x, y);
                if (Cells.ContainsKey(point.ToString()))
                    Cells[point.ToString()]++;
                else
                    Cells.Add(point.ToString(), 1);

                x += pointSet.XModifier();
                y += pointSet.YModifier();
            } while (pointSet.Two.ToString() != point.ToString());
        }
    }

    public Dictionary<string, int> Cells { get; set; }

    public int FindCellsWithMinScore(int minScore)
    {
        return Cells.Count(c => c.Value >= minScore);
    }
}