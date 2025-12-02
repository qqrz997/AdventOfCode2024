using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2025.Day1;

public class Day1 : IProblemSolution
{
    public void Run()
    {
        var input = ProblemInputs.GetInput()
            .GetLines()
            .Select(line => new Instruction(
                line[0] == 'L' ? Direction.Left : Direction.Right,
                int.Parse(line[1..])))
            .ToArray();

        Console.WriteLine(PartOne(input));
        Console.WriteLine(PartTwo(input));
    }

    private static int PartOne(IEnumerable<Instruction> instructions)
    {
        int dial = 50;
        int count = 0;
        foreach (var (direction, num) in instructions)
        {
            int val = num % 100;
            dial = direction is Direction.Left ? dial - val : dial + val;
            if (dial < 0)
                dial += 100;
            if (dial > 99)
                dial -= 100;
            if (dial == 0)
                count++;
        }
        return count;
    }

    private static int PartTwo(IEnumerable<Instruction> instructions)
    {
        int dial = 50;
        int count = 0;
        foreach (var (direction, num) in instructions)
        {
            for (int i = 0; i < num; i++)
            {
                dial = direction is Direction.Left ? dial - 1 : dial + 1;
                if (dial > 99)
                    dial = 0;
                if (dial < 0)
                    dial = 99;
                if (dial == 0)
                    count++;
            }
        }
        return count;
    }

    private record Instruction(Direction Direction, int Number);

    private enum Direction
    {
        Left,
        Right
    }
}