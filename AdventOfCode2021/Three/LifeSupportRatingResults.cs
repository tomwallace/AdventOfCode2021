namespace AdventOfCode2021.Three;

public class LifeSupportRatingResults
{
    public string OxygenGeneratorRating { get; set; }

    public string CO2ScrubberRating { get; set; }

    public long GetLifeSupportRating()
    {
        return Convert.ToInt32(OxygenGeneratorRating, 2) * Convert.ToInt32(CO2ScrubberRating, 2);
    }
}