using AdventOfCode2021.Utility;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode2021.Twenty;

public class Image
{
    private Dictionary<string, bool> pixels;
    private readonly string imageEnhancementAlg;

    private ImageState state;

    public Image(string filePath)
    {
        var lines = FileUtility.ParseFileToList(filePath, line => line);
        imageEnhancementAlg = lines[0];

        pixels = new Dictionary<string, bool>();

        for (int y = 2; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[2].Count(); x++)
            {
                pixels.Add($"{y},{x}", lines[y][x] == '#');
            }
        }

        state = new ImageState(0, lines[2].Length - 1, 2, lines.Count - 1);

        //Print();
    }

    public void ApplyEnhancement(int times)
    {
        // For each step
        for (var i = 1; i <= times; i++)
        {
            var newPixels = new Dictionary<string, bool>();

            for (int y = state.YMin - 1; y <= state.YMax + 1; y++)
            {
                for (int x = state.XMin - 1; x <= state.XMax + 1; x++)
                {
                    var pixelString = FindPixelString(x, y);
                    var dec = Convert.ToInt32(pixelString, 2);
                    var value = imageEnhancementAlg[dec] == '#';

                    newPixels.Add($"{y},{x}", value);
                }
            }

            pixels = newPixels;

            state = new ImageState(state.XMin - 1, state.XMax + 1, state.YMin - 1, state.YMax + 1);

            //Print();
        }
    }

    public void Print()
    {
        Debug.WriteLine("");

        for (int y = state.YMin; y <= state.YMax; y++)
        {
            var builder = new StringBuilder();

            for (int x = state.XMin; x <= state.XMax; x++)
            {
                if (pixels.ContainsKey($"{y},{x}"))
                    builder.Append(pixels[$"{y},{x}"] ? "#" : ".");
                else
                    builder.Append(".");
            }

            Debug.WriteLine(builder.ToString());
        }

        Debug.WriteLine("");
    }

    public int CountLitPixels()
    {
        return pixels.Count(p => p.Value);
    }

    private string FindPixelString(int currentX, int currentY)
    {
        var builder = new StringBuilder();

        for (int modY = -1; modY <= 1; modY++)
        {
            for (int modX = -1; modX <= 1; modX++)
            {
                builder.Append(IsLit(currentX + modX, currentY + modY, state.XMin + 1) ? "1" : "0");
            }
        }

        return builder.ToString();
    }

    private bool IsLit(int x, int y, int minX)
    {
        // Handle case that sometimes "frame" of image is all "#" or all "."
        if (!pixels.ContainsKey($"{y},{x}") && minX % 2 == 0 && imageEnhancementAlg[0] == '#')
        {
            return true;
        }
        if (!pixels.ContainsKey($"{y},{x}"))
        {
            return false;
        }
        return pixels[$"{y},{x}"];
    }
}