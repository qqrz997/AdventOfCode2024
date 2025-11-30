using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2024.Day7;

internal class Day7 : IProblemSolution
{
    public void Run()
    {
        var input = ProblemInputs.GetInput().GetLines();
        
        var equations = input
            .Select(str => new Regex(@"(?<result>\d*): (?<constants>.*)", RegexOptions.Singleline).Match(str))
            .Select(match =>
            {
                var result = long.Parse(match.Groups["result"].Value);
                var constants = match.Groups["constants"].Value
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();
                return new Equation(result, constants);
            })
            .ToArray();
        
        Console.WriteLine(PartOne(equations));
        Console.WriteLine(PartTwo(equations));
    }

    private static long PartOne(Equation[] equations) => equations
        .Where(equation => 
            GetAllPossibleResults(equation, [Add, Mult])
                .Any(r => r == equation.Result))
        .Sum(e => e.Result);

    private static long PartTwo(Equation[] equations) => equations
        .Where(equation => 
            GetAllPossibleResults(equation, [Add, Mult, Concat])
                .Any(r => r == equation.Result))
        .Sum(e => e.Result);

    private static long Add(long a, long b) => a + b;
    private static long Mult(long a, long b) => a * b;
    public static long Concat(long a, long b) => long.Parse($"{a}{b}");

    private static List<long> GetAllPossibleResults(Equation equation, List<Func<long, long, long>> operations)
    {
        List<long> results = [equation.Constants.First()];
        
        for (int i = 0; i < equation.Constants.Length - 1; i++)
        {
            var next = equation.Constants[i + 1];
            List<long> tempResults = [];
            foreach (var operation in operations)
            {
                tempResults.AddRange(results.Select(x => operation(x, next)));
            }
            results = tempResults;
        }
        
        return results;
    }
        
    private record Equation(long Result, int[] Constants);
}