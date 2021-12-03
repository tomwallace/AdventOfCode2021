using AdventOfCode2021.Utility;

namespace AdventOfCode2021.Three;

public class DayThree : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Binary Diagnostic";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 3;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var filePath = @"Three\DayThreeInput.txt";
        var lines = FileUtility.ParseFileToList(filePath, line => line.Trim().ToCharArray());
        var results = DetermineDiagnosticResults(lines);

        return results.GetPowerConsumption().ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var filePath = @"Three\DayThreeInput.txt";
        var lines = FileUtility.ParseFileToList(filePath, line => line.Trim().ToCharArray());
        var results = CalculateLifeSupportRating(lines);

        return results.GetLifeSupportRating().ToString();
    }

    public DiagnosticResults DetermineDiagnosticResults(List<char[]> reportLines)
    {
        var epsilonRate = new int[reportLines[0].Length];
        var gammaRate = new int[reportLines[0].Length];

        for (int p = 0; p < reportLines[0].Length; p++)
        {
            var evaluation = EvaluateLines(reportLines, p);

            if (evaluation.Zeros <= evaluation.Ones)
            {
                epsilonRate[p] = 0;
                gammaRate[p] = 1;
            }
            else
            {
                epsilonRate[p] = 1;
                gammaRate[p] = 0;
            }
        }

        return new DiagnosticResults(epsilonRate, gammaRate);
    }

    public LifeSupportRatingResults CalculateLifeSupportRating(List<char[]> startingReportLines)
    {
        var results = new LifeSupportRatingResults();

        // Work with OxygenGeneratorRating
        var oxygenReportLines = startingReportLines.ToList();
        var currentChar = 0;
        do
        {
            oxygenReportLines = FilterReportLines(oxygenReportLines, currentChar, true);
            currentChar++;
        } while (oxygenReportLines.Count > 1);

        results.OxygenGeneratorRating = string.Join("", oxygenReportLines[0]);

        // Work with CO2ScrubberRating
        var co2ReportLines = startingReportLines.ToList();
        currentChar = 0;
        do
        {
            co2ReportLines = FilterReportLines(co2ReportLines, currentChar, false);
            currentChar++;
        } while (co2ReportLines.Count > 1);

        results.CO2ScrubberRating = string.Join("", co2ReportLines[0]);

        return results;
    }

    private List<char[]> FilterReportLines(List<char[]> reportLines, int currentChar, bool isO2Rating)
    {
        var evaluation = EvaluateLines(reportLines, currentChar);

        var charLookingFor = 't';
        if (isO2Rating)
            charLookingFor = evaluation.Ones >= evaluation.Zeros ? '1' : '0';
        else
            charLookingFor = evaluation.Zeros <= evaluation.Ones ? '0' : '1';

        // Loop through again and keep the qualifying records
        var remainingLines = reportLines.Where(r => r[currentChar] == charLookingFor).ToList();

        return remainingLines;
    }

    private LineEvaluation EvaluateLines(List<char[]> reportLines, int currentChar)
    {
        var evaluation = new LineEvaluation();

        for (int l = 0; l < reportLines.Count; l++)
        {
            if (reportLines[l][currentChar] == '0')
                evaluation.Zeros++;
            else
                evaluation.Ones++;
        }

        return evaluation;
    }
}