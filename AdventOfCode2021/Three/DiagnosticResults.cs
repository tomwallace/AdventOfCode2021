namespace AdventOfCode2021.Three;

public class DiagnosticResults
{
    public DiagnosticResults(int[] epsilonRate, int[] gammaRate)
    {
        EpsilonRate = string.Join("", epsilonRate);
        GammaRate = string.Join("", gammaRate);
    }

    public string EpsilonRate { get; set; }

    public string GammaRate { get; set; }

    public long GetPowerConsumption()
    {
        return Convert.ToInt32(EpsilonRate, 2) * Convert.ToInt32(GammaRate, 2);
    }
}