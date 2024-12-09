using System.Text.RegularExpressions;

var equations = File.ReadAllLines("input.txt")
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

return;

static long PartOne(Equation[] equations) => equations
    .Where(equation => 
        GetAllPossibleResults(equation, [Add, Mult])
            .Any(r => r == equation.Result))
    .Sum(e => e.Result);

static long PartTwo(Equation[] equations) => equations
    .Where(equation => 
        GetAllPossibleResults(equation, [Add, Mult, Concat])
            .Any(r => r == equation.Result))
    .Sum(e => e.Result);

static long Add(long a, long b) => a + b;
static long Mult(long a, long b) => a * b;
static long Concat(long a, long b) => long.Parse($"{a}{b}");

static IEnumerable<long> GetAllPossibleResults(Equation equation, List<Func<long, long, long>> operations)
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

internal record Equation(long Result, int[] Constants);