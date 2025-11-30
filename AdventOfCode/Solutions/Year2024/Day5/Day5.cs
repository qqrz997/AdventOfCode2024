using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2024.Day5;

internal class Day5 : IProblemSolution
{
    public void Run()
    {
        var input = ProblemInputs.GetInput().GetLines();
        
        var updates = input
            .Where(s => s.Contains(','))
            .Select(s => s.Split(',').Select(int.Parse).ToArray())
            .ToArray();
        
        var rules = new Dictionary<int, HashSet<int>>();
        
        foreach ((int first, int second) in input
            .Where(s => s.Contains('|'))
            .Select(s => 
                (int.Parse($"{s[0]}{s[1]}"),
                 int.Parse($"{s[3]}{s[4]}"))))
        {
            if (rules.TryGetValue(first, out var value)) value.Add(second);
            else rules.Add(first, [second]);
        }
        
        Console.WriteLine(PartOne(updates, rules));
        Console.WriteLine(PartTwo(updates, rules));
    }

    private static int PartOne(int[][] updates, Dictionary<int, HashSet<int>> rules) => updates
        .Where(update => IsValidUpdate(update, rules))
        .Select(arr => arr[arr.Length / 2])
        .Sum();

    private static int PartTwo(int[][] updates, Dictionary<int, HashSet<int>> rules) => updates
        .Where(update => !IsValidUpdate(update, rules))
        .Select(update => OrderUpdate(update, rules))
        .Select(arr => arr[arr.Length / 2])
        .Sum();
         
    private static bool IsValidUpdate(int[] update, Dictionary<int, HashSet<int>> rules)
    {
        for (int i = 0; i < update.Length - 1; i++)
        {
            if (!rules[update[i]].Contains(update[i + 1])) return false;
        }
        return true;
    }

    private static int[] OrderUpdate(int[] unordered, Dictionary<int, HashSet<int>> rules)
    {
        while (!IsValidUpdate(unordered, rules))
        {
            for (int i = 0; i < unordered.Length - 1; i++)
            {
                if (!rules[unordered[i]].Contains(unordered[i + 1]))
                {
                    (unordered[i], unordered[i + 1]) = (unordered[i + 1], unordered[i]);
                }
            }
        }
        return unordered;
    }
}