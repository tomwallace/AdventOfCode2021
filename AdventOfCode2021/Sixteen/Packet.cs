using System.Text;

namespace AdventOfCode2021.Sixteen;

public class Packet
{
    public static readonly Dictionary<char, string> ConversionTable = new Dictionary<char, string>()
    {
        { '0', "0000" },
        { '1', "0001" },
        { '2', "0010" },
        { '3', "0011" },
        { '4', "0100" },
        { '5', "0101" },
        { '6', "0110" },
        { '7', "0111" },
        { '8', "1000" },
        { '9', "1001" },
        { 'A', "1010" },
        { 'B', "1011" },
        { 'C', "1100" },
        { 'D', "1101" },
        { 'E', "1110" },
        { 'F', "1111" }
    };

    public Packet(char[] bits, int startIndex)
    {
        SubPackets = new List<Packet>();
        StartIndex = startIndex;
        var i = startIndex;

        // Header
        Version = ToInt(i, 3, bits);
        i += 3;
        Id = ToInt(i, 3, bits);
        i += 3;

        // Literals
        if (IsLiteral)
        {
            var completedLiteralCharacter = false;
            var literalValueBuilder = new StringBuilder();
            do
            {
                // Last one
                if (bits[i] == '0')
                    completedLiteralCharacter = true;

                var literalChar = ToBitString(i + 1, 4, bits);
                literalValueBuilder.Append(literalChar);

                i += 5;
            } while (!completedLiteralCharacter);

            LiteralValue = ToLong(literalValueBuilder.ToString());

            EndIndex = i - 1;
        }
        // Operators
        else
        {
            LengthTypeId = ToInt(i, 1, bits);
            i += 1;

            if (LengthTypeId == 0)
            {
                var subPacketLength = ToInt(i, 15, bits);
                i += 15;
                var targetIndex = subPacketLength + i;
                do
                {
                    var packet = new Packet(bits, i);
                    SubPackets.Add(packet);
                    i = packet.EndIndex + 1;
                } while (i < targetIndex);

                EndIndex = targetIndex - 1;
            }
            else
            {
                var numberOfSubPackets = ToInt(i, 11, bits);
                i += 11;

                for (var numberOfPackets = 0; numberOfPackets < numberOfSubPackets; numberOfPackets++)
                {
                    var packet = new Packet(bits, i);
                    SubPackets.Add(packet);
                    i = packet.EndIndex + 1;
                }

                EndIndex = i - 1;
            }
        }
    }

    public int StartIndex { get; set; }

    public int EndIndex { get; set; }

    public int Version { get; set; }

    public int Id { get; set; }

    public bool IsLiteral => Id == 4;

    public ulong LiteralValue { get; set; }

    public int LengthTypeId { get; set; }

    public List<Packet> SubPackets { get; set; }

    public long SumVersions()
    {
        return Version + SubPackets.Sum(s => s.SumVersions());
    }

    public ulong GetResult()
    {
        ulong result = 0;

        switch (Id)
        {
            case 0:  // Sum
                foreach (var s in SubPackets)
                {
                    result += s.GetResult();
                }

                break;

            case 1: // Multiply
                result = 1;
                foreach (var s in SubPackets)
                {
                    result *= s.GetResult();
                }

                break;

            case 2:   // Min
                result = SubPackets.Min(s => s.GetResult());
                break;

            case 3:   // Max
                result = SubPackets.Max(s => s.GetResult());
                break;

            case 4:   // Literal
                result = LiteralValue;
                break;

            case 5:  // Greater Than
                result = SubPackets[0].GetResult() > SubPackets[1].GetResult() ? 1UL : 0;
                break;

            case 6:  // Less Than
                result = SubPackets[0].GetResult() < SubPackets[1].GetResult() ? 1UL : 0;
                break;

            case 7:  // Equal
                result = SubPackets[0].GetResult() == SubPackets[1].GetResult() ? 1UL : 0;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(Id));
        }

        return result;
    }

    private string ToBitString(int start, int size, char[] bits)
    {
        return new string(bits.Skip(start).Take(size).ToArray());
    }

    private int ToInt(int start, int size, char[] bits)
    {
        return Convert.ToInt32(ToBitString(start, size, bits), 2);
    }

    private ulong ToLong(string input)
    {
        return Convert.ToUInt64(input, 2);
    }
}