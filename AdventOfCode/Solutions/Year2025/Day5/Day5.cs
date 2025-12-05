using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2025.Day5;

public class Day5 : IProblemSolution
{
    public void Run()
    {
        var input = ProblemInputs.GetInput().GetLines();
        var rangesRegex = new Regex(@"(\d*)-(\d*)", RegexOptions.Compiled);
        var ranges = input
            .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => rangesRegex.Match(line))
            .Where(match => match.Success)
            .Select(match => (
                    Min: long.Parse(match.Groups[1].Value),
                    Max: long.Parse(match.Groups[2].Value)))
            .ToArray();
        var ids = input
            .Skip(ranges.Length + 1)
            .Select(long.Parse)
            .ToArray();
        
        Console.WriteLine(PartOne(ranges, ids));
        Console.WriteLine(PartTwo(ranges));
    }

    private static int PartOne((long Min, long Max)[] ranges, long[] ids)
    {
        int sum = 0;
        foreach (var id in ids)
        {
            foreach (var (min, max) in ranges)
            {
                if (id >= min && id <= max)
                {
                    sum++;
                    break;
                }
            }
        }
        return sum;
    }

    private static long PartTwo((long Min, long Max)[] ranges)
    {
        var ordered = ranges.Distinct()
            .OrderBy(range => range.Min)
            .ThenBy(range => range.Max)
            .ToArray();

        var empty = (1, 0);
        int offset = 0;
        for (int i = 0; i < ordered.Length - 1; i++)
        {
            var current = ordered[i - offset];
            var next = ordered[i + 1];
            if (current.Min == next.Min && current.Max <= next.Max)
                ordered[i] = empty;
            else if (current.Max == next.Max && next.Min >= current.Min
                     || current.Max > next.Min && current.Max > next.Max)
            {
                ordered[i + 1] = empty;
                offset++;
                continue;
            }
            else if (next.Min <= current.Max)
                ordered[i + 1].Min = current.Max + 1;
            offset = 0;
        }

        return ordered.Select(tuple => tuple.Max - tuple.Min + 1).Sum();
    }
}