using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2025.Day2;

public class Day2 : IProblemSolution
{
    public void Run()
    {
        var input = ProblemInputs.GetInput().Text;

        var ranges = input.Split(',');
        var ids = ranges
            .Select(str => str.Split('-'))
            .Select(strs => (Min: long.Parse(strs[0]), Max: long.Parse(strs[1])))
            .SelectMany(tuple => LongRange(tuple.Min, tuple.Max))
            .Select(id => id.ToString())
            .ToArray();
        
        Console.WriteLine(PartOne(ids));
        Console.WriteLine(PartTwo(ids));
    }

    private static long PartOne(string[] ids)
    {
        long count = 0;
        foreach (var id in ids)
        {
            var leftHalf = id[..(id.Length / 2)];
            var rightHalf = id[(id.Length / 2)..];
            if (leftHalf == rightHalf)
                count += long.Parse(id);
        }
        return count;
    }

    private static long PartTwo(string[] ids)
    {
        long count = 0;
        foreach (var id in ids)
        {
            for (int i = id.Length / 2; i > 0; i--)
            {
                if (id.Length % i != 0)
                    continue;
                if (id.Chunk(i).AllElementsEqual())
                    continue;
                count += long.Parse(id);
                break;
            }
        }
        return count;
    }
    
    private static IEnumerable<long> LongRange(long min, long max)
    {
        long current = min;
        while (current <= max)
        {
            yield return current;
            current++;
        }
    }
}

file static class Extensions
{
    public static IEnumerable<string> Chunk(this string str, int chunkSize) => Enumerable
        .Range(0, str.Length / chunkSize)
        .Select(x => str.Substring(x * chunkSize, chunkSize));

    public static bool AllElementsEqual<T>(this IEnumerable<T> seq) =>
        seq.Distinct().Skip(1).Any();
}