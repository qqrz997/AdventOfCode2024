using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2025.Day10;

public class Day10 : IProblemSolution
{
    public void Run()
    {
        var input = ProblemInputs.GetInput().GetLines().Select(ParseMachine).ToArray();
    
        Console.WriteLine(PartOne(input));
        Console.WriteLine(PartTwo(input));
    }

    private static int PartOne(Machine[] machines) => machines
        .Sum(machine => GetPowerSet(machine.Buttons)
            .OrderBy(set => set.Length)
            .Select(set => set.Select(button => button.Connections).ToArray())
            .First(connections => IsValidCombination(connections, machine))
            .Length);

    private static int PartTwo(Machine[] input)
    {
        throw new NotImplementedException();
    }

    private static Machine ParseMachine(string input)
    {
        List<bool> combination = [];
        List<Button> buttons = [];
        List<int> joltages = [];
        for (int i = 1; i < input.Length; i++)
        {
            if (input[i - 1] == '[')
            {
                while (input[i] != ']')
                {
                    combination.Add(input[i] == '#');
                    i++;
                }
            }
            else if (input[i] == '(')
            {
                var button = new List<int>();
                while (input[i] != ')')
                {
                    if (int.TryParse(input[i].ToString(), out int buttonNumber))
                    {
                        button.Add(buttonNumber);
                    }
                    i++;
                }
                buttons.Add(new(button.ToArray()));
            }
            else if (input[i] == '{')
            {
                int end = input.IndexOf('}');
                var joltageStrings = input.Substring(i, end - i).Split(',');
                foreach (var joltageString in joltageStrings)
                {
                    if (int.TryParse(joltageString, out int joltage))
                    {
                        joltages.Add(joltage);
                    }
                }
            }
        }

        return new(combination.ToArray(), buttons.ToArray(), joltages.ToArray());
    }
    
    public static T[][] GetPowerSet<T>(T[] seq)
    {
        var powerSet = new T[1 << seq.Length][];
        powerSet[0] = [];

        for (int i = 0; i < seq.Length; i++)
        {
            var cur = seq[i];
            int count = 1 << i;
            for (int j = 0; j < count; j++)
            {
                var source = powerSet[j];
                var destination = powerSet[count + j] = new T[source.Length + 1];
                for (int q = 0; q < source.Length; q++)
                    destination[q] = source[q];
                destination[source.Length] = cur;
            }
        }
        return powerSet;
    }

    private static bool IsValidCombination(int[][] connections, Machine machine)
    {
        bool[] combination = new bool[machine.Combination.Length];
        foreach (var (key, count) in connections
                     .SelectMany(x => x) // collect all occurrences of a connection
                     .GroupBy(x => x)
                     .Select(g => (g.Key, g.Count())))
        {
            combination[key] = count % 2 == 1;
        }
        return combination.SequenceEqual(machine.Combination);
    }
    
    private class Machine
    {
        public Machine(bool[] combination, Button[] buttons, int[] joltages)
        {
            Combination = combination;
            Buttons = buttons;
            Joltages = joltages;
        }

        public bool[] Combination { get; }
        public Button[] Buttons { get; }
        public int[] Joltages { get; }
    }
    
    private class Button(int[] connections)
    {
        public int[] Connections { get; } = connections;
    }
}