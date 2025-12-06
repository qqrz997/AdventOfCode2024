using System.Text;
using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2025.Day6;

public class Day6 : IProblemSolution
{
    private enum OperationType { None, Add, Mult }

    public void Run()
    {
        var lines = ProblemInputs.GetInput().GetLines();
        var numbers = lines.SkipLast(1).Select(ParseNumberLine).ToArray();
        var operations = ParseOperationLine(lines.Last());
        
        Console.WriteLine(PartOne(numbers, operations));
        Console.WriteLine(PartTwo(lines));
    }

    private static long PartOne(int[][] numbers, OperationType[] operations)
    {
        long result = 0L;
        for (int i = 0; i < operations.Length; i++)
        {
            var operationType = operations[i];
            long sum = 0L;
            foreach (var row in numbers)
            {
                if (sum == 0L) sum = row[i];
                else if (operationType == OperationType.Add) sum += row[i];
                else if (operationType == OperationType.Mult) sum *= row[i];
            }
            result += sum;
        }
        return result;
    }

    private static long PartTwo(string[] lines)
    {
        long sum = 0L;
        
        int height = lines.Length;
        int width = lines[0].Length;
        var bottomLine = lines[height - 1];

        var numberList = new List<int>();
        var operation = OperationType.None;
        
        for (int x = 0; x < width; x++)
        {
            char bottomChar = x < bottomLine.Length ? bottomLine[x] : ' ';
            if (bottomChar is '*')
            {
                ProcessNumberList();
                operation = OperationType.Mult;
            }
            else if (bottomChar is '+')
            {
                ProcessNumberList();
                operation = OperationType.Add;
            }

            var columnBuilder = new StringBuilder();
            for (int y = 0; y < height - 1; y++)
            {
                var currentChar = lines[y][x];
                if (char.IsNumber(currentChar)) columnBuilder.Append(currentChar);
            }
            if (columnBuilder.Length > 0)
            { 
                numberList.Add(int.Parse(columnBuilder.ToString()));
            }
        }

        ProcessNumberList();
        return sum;

        void ProcessNumberList()
        {
            sum += CalculateProblem(numberList, operation);
            numberList.Clear();
        }
    }

    private static int[] ParseNumberLine(string line) =>
        ParseNumberStrings(line.Split(' ')).ToArray();

    private static IEnumerable<int> ParseNumberStrings(IEnumerable<string> seq)
    {
        foreach (var s in seq) if (int.TryParse(s, out var n)) yield return n;
    }

    private static OperationType[] ParseOperationLine(string line) => line
        .Select(ParseChar)
        .Where(op => op != OperationType.None)
        .ToArray();

    private static OperationType ParseChar(char c) => c switch
    {
        '+' => OperationType.Add,
        '*' => OperationType.Mult,
        _ => OperationType.None
    };

    private static long CalculateProblem(IEnumerable<int> numbers, OperationType type)
    {
        if (type == OperationType.None) return 0;
        var product = 0L;
        foreach (var number in numbers)
        {
            if (product == 0L)  product = number;
            else if (type == OperationType.Add) product += number;
            else if (type == OperationType.Mult) product *= number;
        }
        return product;
    }
}