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

    private static int PartOne(char[][] grid)
    {
        int sum = 0;
        int height = grid.Length;
        int width = grid[0].Length;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[y][x] == '@' && IsAccessible(grid, width, height, x, y))
                {
                    sum++;
                }
            }
        }
        return sum;
    }

    private static int PartTwo(char[][] grid)
    {
        int sum = 0;
        int height = grid.Length;
        int width = grid[0].Length;
        while (true)
        {
            bool found = false;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (grid[y][x] == '@' && IsAccessible(grid, width, height, x, y))
                    {
                        grid[y][x] = '.';
                        sum++;
                        found = true;
                    }
                }
            }
            if (!found) break;
        }

        return sum;
    }

    private static (int Y, int X)[] Coords { get; } =
        [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)];
    
    private static bool IsAccessible(char[][] grid, int w, int h, int x, int y)
    {
        int adjacent = 0;
        foreach (var (cY, cX) in Coords)
        {
            var checkY = y + cY;
            var checkX = x + cX;
            if (checkY < 0 || checkX < 0 || checkY >= h || checkX >= w) 
                continue;
            if (grid[checkY][checkX] == '@')
                adjacent++;
            if (adjacent >= 4)
                return false;
        }
        return true;
    }
}