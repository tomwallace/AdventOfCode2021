namespace AdventOfCode2021.Twelve;

public class TravelStep
{
    public TravelStep(Cave current)
    {
        Current = current;
        Visited = new List<Cave>();
    }

    public TravelStep(Cave current, Cave smallCaveCanVisitTwice)
    {
        Current = current;
        SmallCaveCanVisitTwice = smallCaveCanVisitTwice;
        Visited = new List<Cave>();
    }

    public TravelStep(Cave current, List<Cave> visited, Cave smallCaveCanVisitTwice)
    {
        Current = current;
        Visited = visited.ToList();
        SmallCaveCanVisitTwice = smallCaveCanVisitTwice;
    }

    public Cave Current { get; set; }

    public List<Cave> Visited { get; set; }

    public Cave SmallCaveCanVisitTwice { get; set; }

    public override string ToString()
    {
        return string.Join(",", Visited.Select(v => v.Id));
    }
}