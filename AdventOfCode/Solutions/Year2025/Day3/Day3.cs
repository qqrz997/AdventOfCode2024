using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2025.Day3;

public class Day3 : IProblemSolution
{
    public void Run()
    {
        var input = ProblemInputs.GetInput()
            .GetLines()
            .Select(arr => arr
                .Select(c => (int)char.GetNumericValue(c))
                .ToList())
            .ToArray();

        foreach (var line in input)
        {
            Console.WriteLine(string.Join("", line));
        }
        
        Console.WriteLine(PartOne(input));
        Console.WriteLine(PartTwo(input));
    }

    private static int PartOne(List<int>[] input)
    {
        int sum = 0;
        foreach (var line in input)
        {
            int largest = 0;
            for (int pointer = 0; pointer < line.Count - 2; pointer++)
            {
                for (int i = pointer + 1; i < line.Count - 1; i++)
                {
                    int n = line[pointer] * 10 + line[i];
                    if (largest < n)
                    {
                        largest = n;
                    }
                }
            }
            sum += largest;
        }
        return sum;
    }

    private static long PartTwo(List<int>[] input)
    {
        long sum = 0;
        foreach (var line in input)
        {
            int magnitude = 12;
            int pointer = 0;
            while (magnitude > 0)
            {
                magnitude--;
                var largest = line[pointer..^(magnitude)];
                pointer += largest.IndexOf(largest.Max());
                sum += line[pointer] * (long)Math.Pow(10, magnitude);
                pointer++;
            }
        }
        return sum;
    }
}