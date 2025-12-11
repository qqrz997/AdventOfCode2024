using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2025.Day11;

public class Day11 : IProblemSolution
{
    public void Run()
    {
        var input = ProblemInputs.GetInput().GetLines();
        var regex = new Regex(@"^([a-z]{3})\:\s(.*)$", RegexOptions.Compiled);
        var connections = input
            .Select(line => regex.Match(line))
            .Select(m => (Start: m.Groups[1].Value, Outputs: m.Groups[2].Value.Split(' ')))
            .ToDictionary(tuple => tuple.Start, tuple => tuple.Outputs);
        
        Console.WriteLine(PartOne(connections));
        Console.WriteLine(PartTwo(connections));
    }

    private static int PartOne(Dictionary<string, string[]> connections)
    {
        int total = 0;
        var outputQueue = new Queue<string>();
        if (!connections.TryGetValue("you", out var connection)) return 0;
        foreach (string output in connection)
        {
            outputQueue.Enqueue(output);
        }
        while (outputQueue.Count > 0)
        {
            string start = outputQueue.Dequeue();
            if (start == "out")
            {
                total++;
                continue;
            }
            foreach (string output in connections[start])
            {
                outputQueue.Enqueue(output);
            }
        }
        return total;
    }

    private static long PartTwo(Dictionary<string, string[]> connections)
    {
        Dictionary<(string, bool, bool), long> visited = [];
        return Find("svr", false, false);
        long Find(string input, bool dacSeen, bool fftSeen)
        {
            if (input == "out")
            {
                return dacSeen && fftSeen ? 1 : 0;
            }

            return connections[input].Sum(output =>
            {
                if (visited.TryGetValue((output, dacSeen, fftSeen), out var val))
                {
                    return val;
                }

                var res = Find(output, dacSeen || output == "dac", fftSeen || output == "fft");
                visited[(output, dacSeen, fftSeen)] = res;
                return res;
            });
        }
    }
}