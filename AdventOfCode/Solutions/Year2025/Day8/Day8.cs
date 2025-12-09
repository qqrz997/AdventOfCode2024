using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2025.Day8;

public class Day8 : IProblemSolution
{
    public void Run()
    {
        var input = ProblemInputs.GetInput()
            .GetLines()
            .Select(line => line.Split(','))
            .Select(parts => (x: long.Parse(parts[0]), y: long.Parse(parts[1])))
            .ToArray();
    
        Console.WriteLine(PartOne(input));
        Console.WriteLine(PartTwo());
    }

    private static long PartOne((long x, long y)[] input) => input
        .Aggregate(0L, (current, coord) => 
            input.Select(other => (Math.Abs(coord.x - other.x) + 1) * (Math.Abs(coord.y - other.y) + 1))
                .Prepend(current)
                .Max());

    private static int PartTwo()
    {
        throw new NotImplementedException();
    }
}