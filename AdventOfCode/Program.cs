using System.Reflection;
using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode;

internal static class Program
{
    public static void Main()
    {
        Console.WriteLine("Advent of Code");

        var problemSolutionInterface = typeof(IProblemSolution);
        var problemSolutions = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && problemSolutionInterface.IsAssignableFrom(t));

        var namespaceRegex = new Regex(@"Year(\d*)\.Day(\d*)", RegexOptions.Compiled);
        
        var problemInfoPairs = problemSolutions.ToDictionary(t =>
        {
            var match = namespaceRegex.Match(t.Namespace ?? string.Empty);
            string year = match.Groups[1].Value;
            string day = match.Groups[2].Value;
            return new SolutionInfo(year, day);
        });

        Console.WriteLine("Displaying available solutions:");
        
        foreach (var group in problemInfoPairs.GroupBy(kvp => kvp.Key.Year))
        {
            var days = group.Select(kvp => int.Parse(kvp.Key.Day)).Order();
            Console.WriteLine($"Year {group.Key} - Days: {string.Join(", ", days)}");
        }

        var inputSolutionInfo = GetInputRoutine();

        if (!problemInfoPairs.TryGetValue(inputSolutionInfo, out var selectedType))
        {
            Console.WriteLine("Failed to get solution info for input...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Running {inputSolutionInfo.Year}/12/{inputSolutionInfo.Day}...");
        var problemSolution = Activator.CreateInstance(selectedType) as IProblemSolution;

        try
        {
            problemSolution?.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Oops! Encountered a {e.GetType().Name}");
        }

        Console.ReadKey();
        return;

        SolutionInfo GetInputRoutine()
        {
            while (true)
            {
                Console.WriteLine("Enter the year and day to run i.e. \"2024 5\":");
                string? input = null;
                var inputRegex = new Regex(@"^(\d*)\s(\d*)$", RegexOptions.Compiled);
                while (input is null or [])
                {
                    input = Console.ReadLine();
                    if (input is not null && inputRegex.IsMatch(input))
                    {
                        break;
                    }
                    Console.WriteLine("Invalid input");
                }

                var match = inputRegex.Match(input);
                var year = match.Groups[1].Value;
                var day = match.Groups[2].Value;
                var solutionInfo = new SolutionInfo(year, day);
                if (problemInfoPairs.ContainsKey(solutionInfo))
                {
                    return solutionInfo;
                }
            }
        }
    }
    
    private record SolutionInfo(string Year, string Day);
}