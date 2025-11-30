using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2024.Day3;

internal class Day3 : IProblemSolution
{
    public void Run()
    {
        var input = ProblemInputs.GetInput().Text;

        Console.WriteLine(PartOne(input));
        Console.WriteLine(PartTwo(input));
    }

    private static int PartOne(string input) =>
        RegularExpressions.Mul.Matches(input)
            .Select(m => int.Parse(m.Groups["a"].Value) * int.Parse(m.Groups["b"].Value))
            .Sum();

    private static int PartTwo(string input) =>
        PartOne(RegularExpressions.BetweenDontAndDo.Replace(input, string.Empty));

    private static class RegularExpressions
    {
        public static Regex Mul { get; } = new(
            @"mul\((?<a>\d{1,3}),(?<b>\d{1,3})\)", 
            RegexOptions.Multiline | RegexOptions.Compiled);
    
        public static Regex BetweenDontAndDo { get; } = new(
            @"don't\(\).*?do\(\)|don't\(\).*", 
            RegexOptions.Singleline | RegexOptions.Compiled);
    }
}