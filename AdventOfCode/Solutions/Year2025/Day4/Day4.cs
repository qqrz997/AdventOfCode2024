using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2025.Day4;

public class Day4 : IProblemSolution
{
    public void Run()
    {
        var input = ProblemInputs.GetInput()
            .GetLines()
            .Select(line => line.ToCharArray())
            .ToArray();
    
        Console.WriteLine(PartOne(input));
        Console.WriteLine(PartTwo(input));
    }

    private static int PartOne(char[][] input)
    {
        int sum = 0;
        for (int y = 0; y < input.Length; y++)
        {
            var line =  input[y];
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] != '@')
                {
                    continue;
                }

                int found = 0;
                if (y - 1 >= 0 && x - 1 >= 0 && input[y - 1][x - 1] == '@')
                {
                    found++;
                }

                if (y - 1 >= 0 && input[y - 1][x] == '@')
                {
                    found++;
                }

                if (y - 1 >= 0 && x + 1 < line.Length && input[y - 1][x + 1] == '@')
                {
                    found++;
                }

                if (x - 1 >= 0 && input[y][x - 1] == '@')
                {
                    found++;
                }

                if (found >= 4)
                {
                    continue;
                }

                if (x + 1 < line.Length && input[y][x + 1] == '@')
                {
                    found++;
                }

                if (found >= 4)
                {
                    continue;
                }

                if (y + 1 < input.Length && x - 1 >= 0 && input[y + 1][x - 1] == '@')
                {
                    found++;
                }

                if (found >= 4)
                {
                    continue;
                }

                if (y + 1 < input.Length && input[y + 1][x] == '@')
                {
                    found++;
                }

                if (found >= 4)
                {
                    continue;
                }

                if (y + 1 < input.Length && x + 1 < line.Length && input[y + 1][x + 1] == '@')
                {
                    found++;
                }

                if (found >= 4)
                {
                    continue;
                }

                sum++;
            }
        }
        return sum;
    }

    private static int PartTwo(char[][] input)
    {
        int sum = 0;
        
        while (true)
        {
            bool found = false;
            for (int y = 0; y < input.Length; y++)
            {
                var line = input[y];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] != '@')
                    {
                        continue;
                    }

                    int adjacent = 0;
                    if (y - 1 >= 0 && x - 1 >= 0 && input[y - 1][x - 1] == '@')
                    {
                        adjacent++;
                    }

                    if (y - 1 >= 0 && input[y - 1][x] == '@')
                    {
                        adjacent++;
                    }

                    if (y - 1 >= 0 && x + 1 < line.Length && input[y - 1][x + 1] == '@')
                    {
                        adjacent++;
                    }

                    if (x - 1 >= 0 && input[y][x - 1] == '@')
                    {
                        adjacent++;
                    }

                    if (adjacent >= 4)
                    {
                        continue;
                    }

                    if (x + 1 < line.Length && input[y][x + 1] == '@')
                    {
                        adjacent++;
                    }

                    if (adjacent >= 4)
                    {
                        continue;
                    }

                    if (y + 1 < input.Length && x - 1 >= 0 && input[y + 1][x - 1] == '@')
                    {
                        adjacent++;
                    }

                    if (adjacent >= 4)
                    {
                        continue;
                    }

                    if (y + 1 < input.Length && input[y + 1][x] == '@')
                    {
                        adjacent++;
                    }

                    if (adjacent >= 4)
                    {
                        continue;
                    }

                    if (y + 1 < input.Length && x + 1 < line.Length && input[y + 1][x + 1] == '@')
                    {
                        adjacent++;
                    }

                    if (adjacent >= 4)
                    {
                        continue;
                    }

                    input[y][x] = '.';
                    sum++;
                    found = true;
                }
            }

            if (!found)
            {
                break;
            }
        }

        return sum;
    }
}