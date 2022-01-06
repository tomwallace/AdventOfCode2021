using AdventOfCode2021.Utility;

namespace AdventOfCode2021.Nineteen;

// I was, frankly, unable to get this problem to work.  I spent a good deal of time on it, and even with adapting parts of some
// other solutions, I could only solve the sample problem and not Part A.  After a fair amount of frustration, I used another
// person's solution, who explains what is going on to some degree.  The source of that code is here:
// https://github.com/encse/adventofcode/blob/master/2021/Day19/Solution.cs
public class DayNineteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Beacon Scanner [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 19;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Nineteen\DayNineteenInput.txt";
        var count = LocateScanners(filePath)
            .SelectMany(scanner => scanner.GetBeaconsInWorld())
            .Distinct()
            .Count();

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Nineteen\DayNineteenInput.txt";
        var scanners = LocateScanners(filePath);
        return (
            from sA in scanners
            from sB in scanners
            where sA != sB
            select
                Math.Abs(sA.center.x - sB.center.x) +
                Math.Abs(sA.center.y - sB.center.y) +
                Math.Abs(sA.center.z - sB.center.z)
        ).Max().ToString();
    }

    public HashSet<Scanner> LocateScanners(string filePath)
    {
        var scanners = new HashSet<Scanner>(Parse(filePath));
        var locatedScanners = new HashSet<Scanner>();
        var q = new Queue<Scanner>();

        // when a scanner is located, it gets into the queue so that we can
        // explore its neighbours.

        locatedScanners.Add(scanners.First());
        q.Enqueue(scanners.First());

        scanners.Remove(scanners.First());

        while (q.Any())
        {
            var scannerA = q.Dequeue();
            foreach (var scannerB in scanners.ToArray())
            {
                var maybeLocatedScanner = TryToLocate(scannerA, scannerB);
                if (maybeLocatedScanner != null)
                {
                    locatedScanners.Add(maybeLocatedScanner);
                    q.Enqueue(maybeLocatedScanner);

                    scanners.Remove(scannerB); // sic!
                }
            }
        }

        return locatedScanners;
    }

    public Scanner TryToLocate(Scanner scannerA, Scanner scannerB)
    {
        var beaconsInA = scannerA.GetBeaconsInWorld().ToArray();

        foreach (var (beaconInA, beaconInB) in PotentialMatchingBeacons(scannerA, scannerB))
        {
            // now try to find the orientation for B:
            var rotatedB = scannerB;
            for (var rotation = 0; rotation < 24; rotation++, rotatedB = rotatedB.Rotate())
            {
                // Moving the rotated scanner so that beaconA and beaconB overlaps. Are there 12 matches?
                var beaconInRotatedB = rotatedB.Transform(beaconInB);

                var locatedB = rotatedB.Translate(new Coord(
                    beaconInA.x - beaconInRotatedB.x,
                    beaconInA.y - beaconInRotatedB.y,
                    beaconInA.z - beaconInRotatedB.z
                ));

                if (locatedB.GetBeaconsInWorld().Intersect(beaconsInA).Count() >= 12)
                {
                    return locatedB;
                }
            }
        }

        // no luck
        return null;
    }

    public IEnumerable<(Coord beaconInA, Coord beaconInB)> PotentialMatchingBeacons(Scanner scannerA, Scanner scannerB)
    {
        // If we had a matching beaconInA and beaconInB and moved the center
        // of the scanners to these then we would find at least 12 beacons
        // with the same coordinates.

        // The only problem is that the rotation of scannerB is not fixed yet.

        // We need to make our check invariant to that:

        // After the translation, we could form a set from each scanner
        // taking the absolute values of the x y and z coordinates of their beacons
        // and compare those.

        IEnumerable<int> absCoordinates(Scanner scanner) =>
            from coord in scanner.GetBeaconsInWorld()
            from v in new[] { coord.x, coord.y, coord.z }
            select Math.Abs(v);

        // This is the same no matter how we rotate scannerB, so the two sets should
        // have at least 3 * 12 common values (with multiplicity).

        // 🐦 We can also considerably speed up the search with the pigeonhole principle
        // which says that it's enough to take all but 11 beacons from A and B.
        // If there is no match amongst those, there cannot be 12 matching pairs:
        IEnumerable<T> pick<T>(IEnumerable<T> ts) => ts.Take(ts.Count() - 11);

        foreach (var beaconInA in pick(scannerA.GetBeaconsInWorld()))
        {
            var absA = absCoordinates(
                scannerA.Translate(new Coord(-beaconInA.x, -beaconInA.y, -beaconInA.z))
            ).ToHashSet();

            foreach (var beaconInB in pick(scannerB.GetBeaconsInWorld()))
            {
                var absB = absCoordinates(
                    scannerB.Translate(new Coord(-beaconInB.x, -beaconInB.y, -beaconInB.z))
                );

                if (absB.Count(d => absA.Contains(d)) >= 3 * 12)
                {
                    yield return (beaconInA, beaconInB);
                }
            }
        }
    }

    private Scanner[] Parse(string filePath)
    {
        var lines = FileUtility.ParseFileToList(filePath, line => line);
        var scanners = new HashSet<Scanner>();

        var beacons = new List<Coord>();
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (line == "")
            {
                var scanner = new Scanner(new Coord(0, 0, 0), 0, beacons);
                scanners.Add(scanner);
                beacons = new List<Coord>();
                continue;
            }

            if (line.Contains("---"))
            {
                continue;
            }

            var split = line.Split(',');
            var beacon = new Coord(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
            beacons.Add(beacon);
        }
        var lastScanner = new Scanner(new Coord(0, 0, 0), 0, beacons);
        scanners.Add(lastScanner);

        return scanners.ToArray();
    }
}

public record Coord(int x, int y, int z);
public record Scanner(Coord center, int rotation, List<Coord> beaconsInLocal)
{
    public Scanner Rotate() => new Scanner(center, rotation + 1, beaconsInLocal);
    public Scanner Translate(Coord t) => new Scanner(
        new Coord(center.x + t.x, center.y + t.y, center.z + t.z), rotation, beaconsInLocal);

    public Coord Transform(Coord coord)
    {
        var (x, y, z) = coord;

        // rotate coordinate system so that x-axis points in the possible 6 directions
        switch (rotation % 6)
        {
            case 0: (x, y, z) = (x, y, z); break;
            case 1: (x, y, z) = (-x, y, -z); break;
            case 2: (x, y, z) = (y, -x, z); break;
            case 3: (x, y, z) = (-y, x, z); break;
            case 4: (x, y, z) = (z, y, -x); break;
            case 5: (x, y, z) = (-z, y, x); break;
        }

        // rotate around x-axis:
        switch ((rotation / 6) % 4)
        {
            case 0: (x, y, z) = (x, y, z); break;
            case 1: (x, y, z) = (x, -z, y); break;
            case 2: (x, y, z) = (x, -y, -z); break;
            case 3: (x, y, z) = (x, z, -y); break;
        }

        return new Coord(center.x + x, center.y + y, center.z + z);
    }

    public IEnumerable<Coord> GetBeaconsInWorld()
    {
        return beaconsInLocal.Select(Transform);
    }
}